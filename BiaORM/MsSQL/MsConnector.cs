﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiaORM.MsSQL
{
    public class MsConnector : IConnector
    {
        public void AddTable(string tableName)
        {
            throw new NotImplementedException();
        }

        public void AddTable(string tableName, bool createInfo, bool updateInfo, int owner_id)
        {
            throw new NotImplementedException();
        }

        public void AddTable(string tableName, bool createInfo, bool updateInfo, int owner_id, string pk)
        {
            throw new NotImplementedException();
        }

        public void CloseConnection()
        {
            throw new NotImplementedException();
        }

        public void Commit()
        {
            throw new NotImplementedException();
        }

        public void ExecuteTransaction(string cmdSQL)
        {
            throw new NotImplementedException();
        }

        public string ExecuteTransaction(string cmdSQL, string fieldReturn)
        {
            throw new NotImplementedException();
        }

        public bool Exists(string tableName, string fieldName, string value, string Id = null)
        {
            throw new NotImplementedException();
        }

        public ITable GetTable(string tableName)
        {
            throw new NotImplementedException();
        }

        public bool HasRows(string cmdSQL)
        {
            throw new NotImplementedException();
        }

        public void Initialize()
        {
            throw new NotImplementedException();
        }

        public bool OpenConnection()
        {
            throw new NotImplementedException();
        }

        public List<T> Query<T>(string cmd)
        {
            throw new NotImplementedException();
        }

        public void RollBack()
        {
            throw new NotImplementedException();
        }

        public DataTable Select(string cmdSQL)
        {
            throw new NotImplementedException();
        }
    }
}
