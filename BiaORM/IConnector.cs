using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiaORM
{
    public interface IConnector
    {
        void Initialize();
        DataTable Select(string cmdSQL);
        bool Exists(string tableName, string fieldName, string value, string Id = null);
        void Commit();
        void CloseConnection();
        void RollBack();
        bool OpenConnection();
        void ExecuteTransaction(string cmdSQL);
        string ExecuteTransaction(string cmdSQL, string fieldReturn);
        bool HasRows(string cmdSQL);
        List<T> Query<T>(string cmd);
        ITable GetTable(string tableName);
    }
}
