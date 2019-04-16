using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.IO;
//using System.Data.SQLite;
using System.Data;
using System.Reflection;
using System.Timers;
using System.Data.OleDb;
using System.Xml;

namespace ZCT.Data.SqlBuilder
{

    public class SqlBuilder : ISqlBuilder
    {
        private Dictionary<string, string> m_Values;
        private Dictionary<string, string> m_Conditions;
        private IDbConnection m_Connection;
        static private SqlBuilder instance;
        private Dictionary<int, SynchObject> m_SynchObjectList;
        private Timer m_Timer;
        public OnSynchronizedD OnSynchronized;
        private IDBFactory m_dbFactory;
        #region Private Methods
        private SqlBuilder()
        {
            m_Values = new Dictionary<string, string>();
            m_Conditions = new Dictionary<string, string>();
            m_SynchObjectList = new Dictionary<int, SynchObject>();
            m_Timer = new Timer();
            m_Timer.Enabled = false;
            m_Timer.Elapsed += new ElapsedEventHandler(OnElapsed);
        }
        private void OnElapsed(object obj, ElapsedEventArgs args)
        {
            m_Timer.Enabled = false;
            foreach (int key in m_SynchObjectList.Keys)
            {
                SynchObject o = m_SynchObjectList[key];
                if (o.SynchronizationType == SynchType.SELECT)
                    select(o.SynchronizationObject as object);
                else
                    update(obj);
                if (OnSynchronized != null)
                    OnSynchronized.Invoke(o);
            }
            m_Timer.Enabled = true;
        }
        private void clear()
        {
            m_Values.Clear();
            m_Conditions.Clear();
        }
        private void addValue(string name, string value)
        {
            m_Values[name] = value;
        }
        private void addCondition(string name, string value)
        {
            m_Conditions[name] = value;
        }
        private int execute(string query)
        {
            int res = 0;
            using (IDbTransaction dbTrans = m_Connection.BeginTransaction())
            {

                using (IDbCommand cmd = m_Connection.CreateCommand())
                {
                    cmd.CommandText = query;
                    res = cmd.ExecuteNonQuery();
                }
                dbTrans.Commit();
            }
            return res;
        }
        private bool isTableExist(string tbl_name)
        {
            return m_dbFactory.IsTableExist(string.Format("select count(*) from sqlite_master where tbl_name = '{0}'", tbl_name));
        }

        private bool isTableExist(object obj)
        {
            dbPropertyInfoManager mgr = dbPropertyInfoManager.Instance;
            mgr.setObject(obj);
            return isTableExist(mgr.tableName);
        }
        #endregion
        public string getTableXml(string tbl_name)
        {
            IDbDataAdapter adapter = m_dbFactory.CreateAdaptor(string.Format("select * from {0}", tbl_name));//new SQLiteDataAdapter(string.Format("select * from {0}", tbl_name),(SQLiteConnection)m_Connection);
            DataSet data_set = new DataSet();
            adapter.Fill(data_set);
            XmlDataDocument doc = new XmlDataDocument(data_set);
            return doc.OuterXml;
        }
        public string getTableXml(object obj)
        {
            dbPropertyInfoManager mgr = dbPropertyInfoManager.Instance;
            mgr.setObject(obj);
            return getTableXml(mgr.tableName);
        }
        public string getInsert(object obj)
        {
            dbPropertyInfoManager mgr = dbPropertyInfoManager.Instance;
            mgr.setObject(obj);
            StringBuilder b = new StringBuilder();
            foreach (string field in m_Values.Keys)
            {
                b.Append(field + ",");
            }
            string fields = string.Format("({0})", b.ToString(0, b.Length - 1));

            b = new StringBuilder();
            foreach (string v in m_Values.Values)
            {
                b.Append(v + ",");
            }
            string values = string.Format("({0});", b.ToString(0, b.Length - 1));

            string sql = string.Format("INSERT INTO {0} {1} VALUES {2}", mgr.tableName, fields, values);
            clear();
            return sql;
        }
        public string getUpdate(object obj)
        {
            dbPropertyInfoManager mgr = dbPropertyInfoManager.Instance;
            mgr.setObject(obj);
            StringBuilder b = new StringBuilder();
            string set = "", cond = "";
            foreach (string field in m_Values.Keys)
            {
                b.Append(string.Format("{0}={1},", field, m_Values[field]));
            }
            set = b.ToString(0, b.Length - 1);
            b = new StringBuilder();
            if (m_Conditions.Count > 0)
            {
                foreach (string field in m_Conditions.Keys)
                {
                    b.Append(string.Format("{0}={1} AND ", field, m_Conditions[field]));
                }
                cond = b.ToString(0, b.Length - 3);

            }
            string query;
            if (m_Conditions.Count > 0)
                query = string.Format("UPDATE {0} SET {1} WHERE {2};", mgr.tableName, set, cond);
            else
                query = string.Format("UPDATE {0} SET {1};", mgr.tableName, set);
            clear();

            return query;
        }
        public string getDelete(object obj)
        {
            dbPropertyInfoManager mgr = dbPropertyInfoManager.Instance;
            mgr.setObject(obj);
            StringBuilder b = new StringBuilder();
            string cond = "";
            if (m_Conditions.Count > 0)
            {
                foreach (string field in m_Conditions.Keys)
                {
                    b.Append(string.Format("{0}={1} AND", field, m_Values[field]));
                }
                cond = b.ToString(0, b.Length - 3);

            }
            string query;
            if (m_Conditions.Count > 0)
                query = string.Format("DELETE {0} WHERE {1} ;", mgr.tableName, cond);
            else
                query = string.Format("DELETE {0};", mgr.tableName);
            clear();
            return query;
        }
        public string getSelect(object obj)
        {
            dbPropertyInfoManager mgr = dbPropertyInfoManager.Instance;
            mgr.setObject(obj);
            StringBuilder b = new StringBuilder();
            string set = "", cond = "";
            foreach (string field in m_Values.Keys)
            {
                b.Append(string.Format("{0},", field));
            }
            if (b.Length > 1)
                set = b.ToString(0, b.Length - 1);

            if (m_Conditions.Count > 0)
            {
                foreach (string field in m_Conditions.Keys)
                {
                    b.Append(string.Format("{0}={1} AND ", field, m_Conditions[field]));
                }
                cond = b.ToString(0, b.Length - 4);

            }
            string query;
            if (m_Values.Count == 0)
                set = "*";


            if (m_Conditions.Count > 0)
                query = string.Format("SELECT {0} FROM {1} WHERE {2} ;", set, mgr.tableName, cond);
            else
                query = string.Format("SELECT {0} FROM {1};", set, mgr.tableName);

            clear();
            return query;
        }
        public string getCreateTable(object obj)
        {
            dbPropertyInfoManager mgr = dbPropertyInfoManager.Instance;
            mgr.setObject(obj);
            StringBuilder flds = new StringBuilder();
            StringBuilder pk = new StringBuilder();
            bool use_pk = false;

            flds.Append(string.Format("CREATE TABLE [{0}]", mgr.tableName));
            flds.Append("(");

            foreach (dbPropertyInfo p in mgr)
            {

                if (p.isKey == true)
                {
                    if (p.isAutoInc == true)
                    {
                        flds.Append(string.Format("{0} INTEGER PRIMARY KEY AUTOINCREMENT,", p.fieldName));
                        use_pk = true;
                        continue;
                    }
                    else
                        pk.Append(p.fieldName + ",");
                }
                if (p.defaultValue.Length > 0)
                    flds.Append(string.Format("{0} {1},", p.fieldName, p.fieldType));
                else
                    flds.Append(string.Format("{0} {1} {2},", p.fieldName, p.fieldType, p.defaultValue));
            }
            string prim_keys = "";
            if (use_pk == false)
            {
                if (pk.Length > 0)
                    prim_keys = string.Format("PRIMARY KEY({0})", pk.ToString(0, pk.Length - 1));
            }
            string sql;
            if (prim_keys.Length > 0)
                sql = string.Format("{0}{1})", flds.ToString(), prim_keys);
            else
                sql = string.Format("{0})", flds.ToString(0, flds.Length - 2));

            return sql;
        }
        public void startSynchronization(bool start, int miliseconds)
        {
            if (start)
            {
                m_Timer.Interval = miliseconds;
                m_Timer.Enabled = true;
            }
            else
                m_Timer.Enabled = false;
        }
        public int addSynchObject(object obj, SynchType type)
        {
            int key = System.Guid.NewGuid().ToString().GetHashCode();
            SynchObject so = new SynchObject(obj, type, key);
            m_SynchObjectList[key] = so;
            return key;
        }
        public void deleteSynchObject(int key)
        {
            try
            {
                m_SynchObjectList.Remove(key);
            }
            catch (Exception e)
            {
                throw new SqlBuilderException(e.ToString());
            }
        }
        static public SqlBuilder Instance
        {
            get
            {
                try
                {
                    if (instance == null)
                        instance = new SqlBuilder();
                }
                catch (Exception e)
                {
                    throw new SqlBuilderException(e.ToString());
                }
                return instance;
            }
        }

        public void connect(IDBFactory db_factory, string connection_string)
        {
            m_dbFactory = db_factory;
            m_Connection = m_dbFactory.CreateConnection(connection_string);
            m_Connection.Open();
            dbPropertyInfoManager.Instance.setDbFactory(m_dbFactory);
        }
        public void diconnect()
        {
            try
            {
                m_Connection.Close();
            }
            catch (DataException e)
            {
                throw new SqlBuilderException(e.ToString());
            }
        }
        public void executeFromFile(string file_name, char delimiter)
        {
            StreamReader r = new StreamReader(file_name);
            string line = r.ReadToEnd();
            string[] query = line.Split(new char[1] { delimiter }, StringSplitOptions.RemoveEmptyEntries);
            if (query == null || query.Length == 0)
                throw new DataException(string.Format("File {0} is empty", file_name));
            foreach (string q in query)
            {
                execute(q + ";");
            }
        }
        public bool isItemExist(string query)
        {
            int count = 0;
            try
            {
                using (IDbCommand cmd = m_Connection.CreateCommand())
                {
                    cmd.CommandText = query;
                    using (IDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            count = reader.GetInt32(0);
                        }
                        reader.Close();
                    }
                }
            }
            catch (DataException e)
            {
                throw new SqlBuilderException(e.ToString());
            }
            return count > 0;
        }
        public bool isItemExist(object obj)
        {
            dbPropertyInfoManager mgr = dbPropertyInfoManager.Instance;
            mgr.setObject(obj);

            addValue("count(*)", "");
            foreach (dbPropertyInfo p in mgr)
            {
                if (p.isKey)
                    addCondition(p.fieldName, p.getValue());
            }
            using (IDataReader r = select(getSelect(obj)))//executeSelect(mgr.tableName))
            {
                if (r.Read())
                {
                    return r.GetInt32(0) > 0;
                }
                r.Close();
            }
            return false;
        }
        public string[] getStringList(string q)
        {
            List<string> list = new List<string>();
            string[] arr = null;
            using (IDbCommand cmd = m_Connection.CreateCommand())
            {
                cmd.CommandText = q;
                using (IDataReader reader = cmd.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        list.Add(reader.GetString(0));
                    }
                    reader.Close();
                }
            }
            arr = new string[list.Count];
            list.CopyTo(arr);
            return arr;
        }
        public void createTable(object obj)
        {
            execute(getCreateTable(obj));
        }
        public void verifyTableExists(object obj)
        {
            if (!isTableExist(obj))
                createTable(obj);
        }


        public void insert(object obj)
        {
            dbPropertyInfoManager mgr = dbPropertyInfoManager.Instance;
            mgr.setObject(obj);
            foreach (dbPropertyInfo p in mgr)
            {
                if (p.isAutoInc == false)
                    addValue(p.fieldName, p.getValue());
            }
            execute(getInsert(obj));
        }
        public void insert(IEnumerable list)
        {
            foreach (object o in list)
            {
                insert(o);
            }
        }
        public void update(object obj)
        {
            dbPropertyInfoManager mgr = dbPropertyInfoManager.Instance;
            mgr.setObject(obj);

            foreach (dbPropertyInfo p in mgr)
            {

                if (p.isKey == false)
                {
                    addValue(p.fieldName, p.getValue());
                }
                else
                {
                    addCondition(p.fieldName, p.getValue());
                }

            }
            execute(getUpdate(obj));
        }
        public void delete(object obj)
        {
            dbPropertyInfoManager mgr = dbPropertyInfoManager.Instance;
            mgr.setObject(obj);
            foreach (dbPropertyInfo p in mgr)
            {

                if (p.isKey == true)
                {
                    addCondition(p.fieldName, p.getValue());
                }


            }
            execute(getDelete(obj));
        }
        public void select(object obj)
        {
            dbPropertyInfoManager mgr = dbPropertyInfoManager.Instance;
            mgr.setObject(obj);
            foreach (dbPropertyInfo p in mgr)
            {
                if (p.isKey)
                {
                    addCondition(p.fieldName, p.getValue());
                }
            }
            using (IDataReader r = select(getSelect(obj)))
            {
                if (r.Read())
                {
                    for (int i = 0; i < r.FieldCount; i++)
                    {
                        mgr.setValue(r.GetName(i), r.GetValue(i));
                    }
                }
                r.Close();
            }
        }
        public void select(IEnumerable list, object obj)
        {

            dbPropertyInfoManager mgr = dbPropertyInfoManager.Instance;
            mgr.setObject(obj);
            string query = string.Format("select * from {0}", mgr.tableName);
            select(list, obj, query);
        }
        public void select(IEnumerable list, object obj, string query)
        {

            dbPropertyInfoManager mgr = dbPropertyInfoManager.Instance;

            IDataReader r = select(query);
            while (r.Read())
            {
                object newObject = Activator.CreateInstance(obj.GetType());
                mgr.setObject(newObject);

                for (int i = 0; i < r.FieldCount; i++)
                {
                    mgr.setValue(r.GetName(i), r.GetValue(i));
                }
                IList ilist = (IList)list;
                ilist.Add(newObject);
            }
        }
        public IDataReader select(string query)
        {
            IDataReader r = null;
            using (IDbCommand cmd = m_Connection.CreateCommand())
            {
                cmd.CommandText = query;
                r = cmd.ExecuteReader();
            }
            return r;
        }
    }
  
}
