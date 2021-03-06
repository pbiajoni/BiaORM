﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiaORM.MySQL
{
    public class MyConnection : IConnector
    {
        public delegate void OnExecuteQueryEventHandler(string query);
        public event OnExecuteQueryEventHandler OnExecuteQuery;

        public delegate void OnOpenConnectionEventHandler();
        public event OnOpenConnectionEventHandler OnOpenConnection;

        public delegate void OnCloseConnectionEventHandler();
        public event OnCloseConnectionEventHandler OnCloseConnection;

        public delegate void OnCommitedEventHandler();
        public event OnCommitedEventHandler OnCommited;

        public delegate void OnRollBackEventHandler();
        public event OnRollBackEventHandler OnRollBack;

        EntityManager entityManager = new EntityManager();
        MySqlConnection MySQLConn = new MySqlConnection();
        MySqlTransaction MySQLTran;

        private string _connectionString;

        public List<Table> Tables { get; internal set; }

        public string Username { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }
        public string Server { get; set; }
        public string Database { get; set; }
        public bool IncludeSecurityAsserts { get; set; }
        public MyConnection()
        {

        }

        public MyConnection(string connectionString)
        {
            this._connectionString = connectionString;

            CreateTableList();
        }
        public MyConnection(string username, string password, string server, string database)
        {
            Username = username ?? throw new ArgumentNullException(nameof(username));
            Password = password ?? throw new ArgumentNullException(nameof(password));
            Port = 3306;
            Server = server ?? throw new ArgumentNullException(nameof(server));
            Database = database ?? throw new ArgumentNullException(nameof(database));

            CreateTableList();
        }
        public MyConnection(string username, string password, int port, string server, string database)
        {
            Username = username ?? throw new ArgumentNullException(nameof(username));
            Password = password ?? throw new ArgumentNullException(nameof(password));
            Port = port;
            Server = server ?? throw new ArgumentNullException(nameof(server));
            Database = database ?? throw new ArgumentNullException(nameof(database));

            CreateTableList();
        }

        void ValidateTable(string tableName)
        {
            if (this.Tables.Any(x => x.TableName == tableName))
            {
                throw new Exception("Table " + tableName + " already exists in table list");
            }
        }

        public ITable GetTable(string tableName)
        {
            return (Table)this.Tables.Where(x => x.TableName == tableName).FirstOrDefault();
        }

        public void AddTable(Table table)
        {
            ValidateTable(table.TableName);
            this.Tables.Add(table);
        }
        public void AddTable(string tableName)
        {
            ValidateTable(tableName);
            Table table = new Table(this, tableName);
            this.Tables.Add(table);
        }

        public void AddTable(string tableName, bool createInfo, bool updateInfo, int owner_id)
        {
            ValidateTable(tableName);
            Table table = new Table(this, tableName, createInfo, updateInfo, owner_id);
            this.Tables.Add(table);
        }

        public void AddTable(string tableName, bool createInfo, bool updateInfo, int owner_id, string pk)
        {
            ValidateTable(tableName);
            Table table = new Table(this, tableName, createInfo, updateInfo, owner_id, pk);
            this.Tables.Add(table);
        }

        void CreateTableList()
        {
            if (this.Tables == null)
            {
                this.Tables = new List<Table>();
            }
        }

        string getQuery(string cmdSQL, string chavePrimaria)
        {

            string cmd = null;
            string[] q = cmdSQL.Split(' ');

            if (q[0].ToLower() == "insert")
            {
                cmd = "select " + chavePrimaria + " from " + q[2] + " order by " + chavePrimaria + " DESC limit 1;";
            }

            if (q[0].ToLower() == "update")
            {
                cmd = "select " + chavePrimaria + " from " + q[1] + " order by " + chavePrimaria + " DESC limit 1;";
            }

            return cmd;
        }

        public void Initialize()
        {
            if (String.IsNullOrEmpty(this._connectionString))
            {
                string include = "";

                if (this.IncludeSecurityAsserts)
                {
                    include = ",includesecurityassets=True";
                }

                this._connectionString = String.Format(@"allow zero datetime=false;allow user variables=true;Connect Timeout=30;SERVER={0};PORT={1};UID={2};PASSWORD={3};DATABASE={4}{5};", Server, Port, Username, Password, Database, include);
            }
            else
            {
                throw new Exception("The connection string is already initialized");
            }
        }

        public DataTable Select(string cmdSQL)
        {
            Console.WriteLine(cmdSQL);

            if (this.OnExecuteQuery != null)
            {
                this.OnExecuteQuery(cmdSQL);
            }

            MySqlConnectionStringBuilder myCommString = new MySqlConnectionStringBuilder(_connectionString);
            MySqlConnection Conn = new MySqlConnection(myCommString.ConnectionString);

            try
            {
                Conn.Open();
                try { Conn.BeginTransaction(IsolationLevel.ReadUncommitted); }
                catch (Exception er) { }
                MySqlCommand c = new MySqlCommand(cmdSQL, Conn);
                c.CommandTimeout = 600;
                MySqlDataAdapter da = new MySqlDataAdapter(c);
                DataTable dt = new DataTable();
                da.Fill(dt);
                Conn.Close();

                return dt;
            }
            catch (MySqlException er)
            {
                this.RollBack();

                if (Conn.State != ConnectionState.Closed)
                {
                    Conn.Close();
                }

                throw er;
            }
        }

        public void Commit()
        {
            if (MySQLConn.State != ConnectionState.Closed)
            {
                MySQLTran.Commit();
                Console.WriteLine("commited");

                if (this.OnCommited != null)
                {
                    this.OnCommited();
                }
            }

            if (MySQLConn.State != ConnectionState.Closed)
            {
                this.CloseConnection();
            }
        }

        public void CloseConnection()
        {
            if (MySQLConn.State != ConnectionState.Closed)
            {
                MySQLConn.Close();
                Console.WriteLine("conection closed");

                if (this.OnCloseConnection != null)
                {
                    this.OnCloseConnection();
                }
            }
        }
        public void RollBack()
        {

            if (MySQLConn.State != ConnectionState.Closed)
            {
                MySQLTran.Rollback();
                Console.WriteLine("rollback");

                if (this.OnRollBack != null)
                {
                    this.OnRollBack();
                }
            }

            if (MySQLConn.State != ConnectionState.Closed)
            {
                this.CloseConnection();
            }
        }

        public bool OpenConnection()
        {
            try
            {

                if (MySQLConn.State == ConnectionState.Closed)
                {
                    if (string.IsNullOrEmpty(_connectionString))
                    {
                        throw new Exception("BiaORM MySQL is not Initialized");
                    }

                    MySQLConn = new MySqlConnection(_connectionString);
                    MySQLConn.Open();

                    MySQLTran = MySQLConn.BeginTransaction(IsolationLevel.ReadUncommitted);

                    if (MySQLConn.State == ConnectionState.Open)
                    {
                        Console.WriteLine("connection is open");
                        if (this.OnOpenConnection != null)
                        {
                            this.OnOpenConnection();
                        }

                        return true;
                    }
                    else if (MySQLConn.State == ConnectionState.Closed)
                    {
                        return false;
                    }

                    return false;
                }

                return true;
            }
            catch (MySqlException es)
            {
                throw es;
            }

        }

        public void ExecuteTransaction(string cmdSQL)
        {
            try
            {
                OpenConnection();
                Console.WriteLine(cmdSQL);

                if (this.OnExecuteQuery != null)
                {
                    this.OnExecuteQuery(cmdSQL);
                }

                MySqlCommand c = new MySqlCommand(cmdSQL, MySQLConn);
                c.CommandTimeout = 3600;
                c.Transaction = MySQLTran;
                c.ExecuteNonQuery();
            }
            catch (MySqlException mException)
            {
                this.RollBack();
                string errorMessage = ErrorsList.GetMessage(mException.Number);
                Exception exception = null;

                if (!string.IsNullOrEmpty(errorMessage))
                {
                    exception = new Exception(errorMessage, mException);
                }
                else
                {
                    exception = new Exception(mException.Message, mException);
                }

                Console.WriteLine(exception.ToString());
                throw exception;
            }
        }

        /// <summary>
        /// Retorna a Id do registro criado
        /// </summary>
        /// <param name="cmdSQL"></param>
        /// <param name="fieldReturn"></param>
        /// <returns></returns>
        public string ExecuteTransaction(string cmdSQL, string fieldReturn)
        {
            ExecuteTransaction(cmdSQL);
            DataTable dt = Select(getQuery(cmdSQL, fieldReturn));

            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0][fieldReturn].ToString();
            }

            return null;
        }

        public bool Exists(string tableName, string fieldName, string value, string Id = null)
        {
            string stmt = "";

            if (Id != null)
            {
                stmt = " and Id <> '" + Id + "' ";
            }

            string cmd = "select Id from " + tableName + " where " + fieldName + " = '" + value + "' " + stmt + ";";
            return HasRows(cmd);
        }

        public bool HasRows(string cmdSQL)
        {
            DataTable dt = Select(cmdSQL);

            if (dt.Rows.Count > 0)
            {
                return true;
            }

            return false;
        }

        public List<T> Query<T>(string cmd)
        {
            DataTable dt = this.Select(cmd);
            return entityManager.CreateList<T>(dt, QueryTypes.SELECT);
        }
    }
}
