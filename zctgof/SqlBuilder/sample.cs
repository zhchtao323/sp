//using System;
//using System.Collections.Generic;
//using System.Collections;
//using System.Text;
//using System.IO;
////using System.Data.SQLite;
//using System.Data;
//using System.Reflection;
//using System.Timers;
//using System.Data.OleDb;
//using System.Xml;

//namespace ZCT.Data.SqlBuilder
//{
//    [dbTable("tblTest")]
//    class Test
//    {
//        private int m_Id;
//        private string m_Name;
//        private double m_Number;
        
//        [db(true,true,true,"")]
//        public int Id
//        {
//            get { return m_Id; }
//            set { m_Id = value; }
//        }
//        [db(false, false, false, "")]
//        public string Name
//        {
//            get { return m_Name; }
//            set { m_Name = value; }
//        }
//        [db(false, false, false, "")]
//        public double Number
//        {
//            get { return m_Number; }
//            set { m_Number = value; }
//        }
//    }


//     class testSample
//    {

//        public void ex()
//        {
//        SqlBuilder builder = SqlBuilder.Instance;

//            //build connection string
//            string cur_dir = Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
//            string db_path = string.Format("{0}\\my_db.db3", cur_dir);
//            string connection = string.Format("Data Source={0};Version=3;New=True;Compress=True;", db_path);

//            //connect to a specific database
//            SqlBuilder.Instance.connect(new SqliteDbFactory(), connection);
            
//            Test test = new Test();
//            //Verify whether table exists - if not create from the object 'test'
//            builder.verifyTableExists(test);
//            //insert new record
//            test.Name = "Joe";
//            test.Number = 124.56f;
//            builder.insert(test);

//            //select record
//            test.Id = 1;
//            builder.select(test);

//            //update record
//            test.Name = "Rob";
//            test.Number = 12345.56f;
//            builder.update(test);

//            //Get insert string
//            builder.getInsert(test);
//            //Get update string
//            builder.getUpdate(test);
//            //Get delete string
//            builder.getDelete(test);
//            //Get select string
//            builder.getSelect(test);
//            //Get CreateTable string
//            builder.getCreateTable(test);

           
//            //Build list of test objects
//            List<Test> list = new List<Test>();
//            for (int i = 0; i < 10; i++)
//            {
//                Test t = new Test();
//                t.Name = string.Format("test{0}", i);
//                t.Number = i;
//            }
//            //insert list into database
//            builder.insert(list);
//            //select list from database
//            list.Clear();
//            builder.select(list, new Test());//equivalent to select * from tblTest
//            //another select list
//            builder.select(list, new Test(), "select Name,Number from tblTest");


//            //Autosynchronization mechanism
//            //Test
//            test = new Test();
//            test.Name = "dog";
//            test.Number = 15;
//            builder.insert(test);

//            builder.OnSynchronized += new OnSynchronizedD(OnSynchronized);
//            int key = builder.addSynchObject(test, SynchType.SELECT);
//            builder.startSynchronization(true, 1000);
//            builder.startSynchronization(false, 0);
//            builder.deleteSynchObject(key);
//        }
//        public void OnSynchronized(SynchObject obj)
//        {
//            Test t = (Test)obj.SynchronizationObject;
//        }     

//    }

//}
