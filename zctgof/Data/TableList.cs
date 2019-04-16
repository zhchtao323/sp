using System;
using System.Collections.Generic;

using System.Text;
using System.Reflection;
using System.Data;
using System.Collections;

namespace ZCT.Data
{

    /// <summary>
    /// 实现TAble 到LIst间的转换，DataRow 到业务实体
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TableList<T> where T : new()
    {
        #region 数据到实体或实体集合
        /// <summary>
        /// Table 转换到list
        /// </summary>
        /// <param name="dt"></param>
        ///  <param name="col"></param>
        /// <returns></returns>
        public static List<T> TableToList(DataTable dt,bool bs)
        {
            if (dt == null)
            { return null; }
            List<T> list = new List<T>();
            foreach (DataRow dr in dt.Rows)
            {
                T t = new T();
                Type modelType = t.GetType();
                foreach (PropertyInfo p in modelType.GetProperties())
                {
                    if (bs)//采用类特性方式
                    {
                        object[] tt1 = p.GetCustomAttributes(typeof(ColumnAttribute), false);
                        ColumnAttribute col = tt1[0] as ColumnAttribute;
                        if (col!=null)
                        {
                            string colname = col.Name;
                            p.SetValue(t, GetDefaultValue(dr[colname], p.PropertyType), null);//用制定的行名
                            //p.SetValue(t, GetDefaultValue(dr[p.Name], p.PropertyType), null);
                        }
                    }
                    else
                    {
                        DataColumn dc= dt.Columns[p.Name];
                        if (dc!= null)
                        {
                            p.SetValue(t, GetDefaultValue(dr[p.Name], p.PropertyType), null);
                        }
                    }
                }
                list.Add(t);
            }
            return list;
        }
        /// <summary>
        /// dataRow 转换到对象
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public static T ConvertModel(DataRow dr)
        {
            T t = new T();
            Type modelType = t.GetType();
            foreach (PropertyInfo p in modelType.GetProperties())
            {
                 DataColumn dc= dr.Table.Columns[p.Name];
                 if (dc != null)
                 {
                     p.SetValue(t, GetDefaultValue(dr[p.Name], p.PropertyType), null);
                 }
            }
            return t;
        }

        private static object GetDefaultValue(object obj, Type type)
        {
            if (obj == DBNull.Value)
            {
                obj = default(object);
            }
            else
            {
                obj = Convert.ChangeType(obj, type);
            }
            return obj;
        }
        #endregion

        #region 实体类到数据
        /// <summary>
        /// 实体集合类 转换到 Table
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entitys"></param>
        /// <returns></returns>
        public static DataTable ToDataTable<T>(List<T> entitys)
        {
            //检查实体集合不能为空
            if (entitys == null || entitys.Count < 1)
            {
                throw new Exception("需转换的集合为空");
            }
            //取出第一个实体的所有Propertie
            Type entityType = entitys[0].GetType();
            PropertyInfo[] entityProperties = entityType.GetProperties();
            //生成DataTable的structure
            //生产代码中，应将生成的DataTable结构Cache起来，此处略
            DataTable dt = new DataTable();
            for (int i = 0; i < entityProperties.Length; i++)
            {
                dt.Columns.Add(entityProperties[i].Name, entityProperties[i].PropertyType);
            }
            //将所有entity添加到DataTable中
            foreach (object entity in entitys)
            {
                //检查所有的的实体都为同一类型
                if (entity.GetType() != entityType)
                {
                    throw new Exception("要转换的集合元素类型不一致");
                }
                object[] entityValues = new object[entityProperties.Length];
                for (int i = 0; i < entityProperties.Length; i++)
                {
                    entityValues[i] = entityProperties[i].GetValue(entity, null);
                }
                dt.Rows.Add(entityValues);
                //dt.LoadDataRow(entityValues);//实际上，当有必要更新现有的行（如果已经存在一行的话）或者添加行（如果尚且不存在行的话）时，LoadDataRow   最有用。如果找到了行的键值，LoadDataRow   将更新该行的值，而不是插入新行。 
            }
            return dt;
        }
        /// <summary>
        /// 实体集合类 改变到 Table中 没测试
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entitys"></param>
        /// <returns></returns>
        public static void ToDataTable<T>(List<T> entitys ,ref DataTable dt)
        {
            //检查实体集合不能为空
            if (entitys == null || entitys.Count < 1)
            {
                throw new Exception("需转换的集合为空");
            }
            //取出第一个实体的所有Propertie
            Type entityType = entitys[0].GetType();
            PropertyInfo[] entityProperties = entityType.GetProperties();
            //生成DataTable的structure
            //生产代码中，应将生成的DataTable结构Cache起来，此处略
          
            //for (int i = 0; i < entityProperties.Length; i++)
            //{
            //    dt.Columns.Add(entityProperties[i].Name, entityProperties[i].PropertyType);
            //}
            //将所有entity添加到DataTable中
            foreach (object entity in entitys)
            {
                //检查所有的的实体都为同一类型
                if (entity.GetType() != entityType)
                {
                    throw new Exception("要转换的集合元素类型不一致");
                }
                DataRow dr = dt.NewRow();
                //object[] entityValues = new object[entityProperties.Length];
                for (int i = 0; i < entityProperties.Length; i++)
                {
                   // entityValues[i] = entityProperties[i].GetValue(entity, null);
                    dr[entityProperties[i].Name] = entityProperties[i].GetValue(entity, null);
                }
                dt.LoadDataRow(dr.ItemArray,true);
                //dt.Rows.Add(entityValues);
               // dt.LoadDataRow(entityValues);//实际上，当有必要更新现有的行（如果已经存在一行的话）或者添加行（如果尚且不存在行的话）时，LoadDataRow   最有用。如果找到了行的键值，LoadDataRow   将更新该行的值，而不是插入新行。 
            }            
        }
       
        /// <summary>
        /// 将泛型集合类转换成DataTable
        /// </summary>
        /// <typeparam name="T">集合项类型</typeparam>
        /// <param name="list">集合</param>
        /// <param name="propertyName">需要返回的列的列名</param>
        /// <returns>数据集(表)</returns>
        public static DataTable ToDataTable<T>(IList<T> list, params string[] propertyName)
        {
            List<string> propertyNameList = new List<string>();
            if (propertyName != null)
                propertyNameList.AddRange(propertyName);

            DataTable result = new DataTable();
            if (list.Count > 0)
            {
                PropertyInfo[] propertys = list[0].GetType().GetProperties();
                foreach (PropertyInfo pi in propertys)
                {
                    if (propertyNameList.Count == 0)
                    {
                        result.Columns.Add(pi.Name, pi.PropertyType);
                    }
                    else
                    {
                        if (propertyNameList.Contains(pi.Name))
                            result.Columns.Add(pi.Name, pi.PropertyType);
                    }
                }

                for (int i = 0; i < list.Count; i++)
                {
                    ArrayList tempList = new ArrayList();
                    foreach (PropertyInfo pi in propertys)
                    {
                        if (propertyNameList.Count == 0)
                        {
                            object obj = pi.GetValue(list[i], null);
                            tempList.Add(obj);
                        }
                        else
                        {
                            if (propertyNameList.Contains(pi.Name))
                            {
                                object obj = pi.GetValue(list[i], null);
                                tempList.Add(obj);
                            }
                        }
                    }
                    object[] array = tempList.ToArray();
                    result.LoadDataRow(array, true);
                }
            }
            return result;
        }
        #endregion

    }
}
