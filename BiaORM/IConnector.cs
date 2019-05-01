using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiaORM
{
    interface IConnector
    {
        void Initialize();
        void Connect();
        DataTable Select(string cmdSQL);
        void Commit();
        void CloseConnection();
        void RollBack();
        bool OpenConnection();
        void ExecuteTransaction(string cmdSQL);
        int ExecuteTransaction(string cmdSQL, string fieldReturn);
        bool Exists(string tableName, string fieldName, string value, int Id = 0);
        bool HasRows(string cmdSQL);
    }
}
