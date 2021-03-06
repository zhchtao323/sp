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
    /// <summary>
    /// SqliteBuilder is a wrapper singlton class on top of SQLite ADO 2.0 implementation for c#
    /// </summary>
    public delegate void OnSynchronizedD(SynchObject obj);
    public interface ISqlBuilder
    {
        /// <summary>
        /// Starts to synchronize added objects
        /// </summary>
        /// <param name="start">true to satrt, false to stop</param>
        /// <param name="miliseconds">Time interval between synchrinizations</param>
        void startSynchronization(bool start, int miliseconds);
        /// <summary>
        /// Add object for synchrinization
        /// </summary>
        /// <param name="obj">object to synchrinize</param>
        /// <param name="type">synchrinization type ( Update or Select)</param>
        /// <returns>return key that assigned to the object</returns>
        int addSynchObject(object obj, SynchType type);
        /// <summary>
        /// Deletes synchrinizatin object from the list
        /// </summary>
        /// <param name="key">registretion key that returned by addSynchObject</param>
        void deleteSynchObject(int key);
        /// <summary>
        /// Establish database connection
        /// </summary>
        /// <param name="db_factory">Generic implementation for particular database</param>
        /// <param name="connection_string">Connection string for particular database</param>
        void connect(IDBFactory db_factory, string connection_string);
        /// <summary>
        /// Disconnect from the database
        /// </summary>
        void diconnect();
        /// <summary>
        /// Executes query from the ASCII file. 
        /// </summary>
        /// <param name="file_name">path to the file</param>
        /// <param name="delimiter">Delimiter that is used to separate each query within file</param>
        void executeFromFile(string file_name, char delimiter);
        /// <summary>
        /// Check whether query returns some value
        /// </summary>
        /// <param name="query"></param>
        /// <returns>Return true if item exisst, otherwise false</returns>
        bool isItemExist(string query);
        /// <summary>
        /// Finds where object obj exist in database
        /// </summary>
        /// <param name="obj">Lookup object</param>
        /// <returns>true if found false otherwise</returns>
        bool isItemExist(object obj);
        /// <summary>
        /// Return list of strings [ select name from tblPetNames]
        /// </summary>
        /// <param name="q">query to execute</param>
        /// <returns>return list of strings</returns>
        string[] getStringList(string q);
        /// <summary>
        /// creates table using data from object
        /// </summary>
        /// <param name="obj">object to create table for</param>
        void createTable(object obj);
        /// <summary>
        /// Verify whther table exist, if not create a new one
        /// </summary>
        /// <param name="obj"></param>
        void verifyTableExists(object obj);
        /// <summary>
        /// Create an insert statement from the object obj and insert it
        /// </summary>
        /// <param name="obj"></param>
        void insert(object obj);
        /// <summary>
        /// Itarate through the IEnumarable list and insert every object into database
        /// </summary>
        /// <param name="list">List to iterate through</param>
        void insert(IEnumerable list);
        /// <summary>
        /// Update object in database
        /// </summary>
        /// <param name="obj"></param>
        void update(object obj);
        /// <summary>
        /// Delete object from database
        /// </summary>
        /// <param name="obj"></param>
        void delete(object obj);
        /// <summary>
        /// select object from database
        /// </summary>
        /// <param name="obj"></param>
        void select(object obj);
        /// <summary>
        /// Select list of type typeof(obj) from database
        /// </summary>
        /// <param name="list">List to populate</param>
        /// <param name="obj">object of this type will be created</param>
        void select(IEnumerable list, object obj);
        /// <summary>
        /// Select list of type typeof(obj) from database - all fields are populated
        /// </summary>
        /// <param name="list">List to populate</param>
        /// <param name="obj">object of this type will be created</param>
        /// <param name="query">Specify which fields will be populated</param>
        void select(IEnumerable list, object obj, string query);
        /// <summary>
        /// Executes select query
        /// </summary>
        /// <param name="query"> Query to execute</param>
        /// <returns>SQLiteDataReader</returns>
        IDataReader select(string query);
        /// <summary>
        /// Returns insert sql statement for the given object
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        string getInsert(object obj);
        /// <summary>
        /// Returns update sql statement for the given object
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        string getUpdate(object obj);
        /// <summary>
        /// Returns delete sql statement for the given object
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        string getDelete(object obj);
        /// <summary>
        /// Returns select sql statement for the given object
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        string getSelect(object obj);
        /// <summary>
        /// Returns Create Table sql statement for the given object
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        string getCreateTable(object obj);
        string getTableXml(string tbl_name);


    }
}
