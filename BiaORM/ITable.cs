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
        T FindOne<T>(List<Where> clauses);
        T FindOne<T>(string pk);
        T FindOne<T>(Where where);
        string InsertOrUpdate<T>(T entity);
        bool Exists(string fieldName, string value, string pkId = null);
    }
}
