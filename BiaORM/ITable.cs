using BiaORM.Clauses;
using BiaORM.MySQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiaORM
{
    public interface ITable
    {
        void AddField(Field field);
        void AddField(string name, DateTime dateTime);
        void AddField(string name, decimal value);
        void AddField(string name, string value);
        bool FieldExists(string name);
        T FindOne<T>(List<Where> clauses);
        T FindOne<T>(string pk);
        T FindOne<T>(Where where);
        string InsertOrUpdate<T>(T entity);
    }
}
