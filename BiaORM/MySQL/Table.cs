using BiaORM.Clauses;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiaORM.MySQL
{
    public class Table : ITable
    {

        public string PkName { get; set; }
        EntityManager entityManager = new EntityManager();
        string _tableName;
        public string TableName { get { return this._tableName; } set { this._tableName = value; } }
        public MyConnection MySQLConnection { get; set; }
        bool _createInfo;
        bool _updateInfo;
        int _owner_id;

        public Table(string tableName)
        {
            this._tableName = tableName;
            this.PkName = "Id";
        }

        public Table(MyConnection mySQL, string tableName)
        {
            this._tableName = tableName;
            this.MySQLConnection = mySQL;
            this.PkName = "Id";
        }

        public Table(MyConnection mySQL, string tableName, bool createInfo, bool updateInfo, int owner_id)
        {
            this._tableName = tableName;
            this.MySQLConnection = mySQL;
            this._createInfo = createInfo;
            this._updateInfo = updateInfo;
            this._owner_id = owner_id;
            this.PkName = "Id";
        }

        public Table(MyConnection mySQL, string tableName, bool createInfo, bool updateInfo, int owner_id, string pk)
        {
            this._tableName = tableName;
            this.MySQLConnection = mySQL;
            this._createInfo = createInfo;
            this._updateInfo = updateInfo;
            this._owner_id = owner_id;
            this.PkName = pk;
        }

        public T FindOne<T>(string pk)
        {
            Where where = new Where(OperatorTypes.Equal, this.PkName, pk);
            return this.FindOne<T>(where);
        }

        public T FindOne<T>(Where where)
        {
            List<Where> wheres = new List<Where>();
            wheres.Add(where);
            return this.FindOne<T>(wheres);
        }

        public T FindOne<T>(List<Where> wheres)
        {
            string str_where = "";

            foreach (Where where in wheres)
            {
                string query = where.Query;
                str_where += query;
            }

            string cmd = "select * from " + this._tableName + " where " + str_where + " limit 1;";
            DataTable dt = MySQLConnection.Select(cmd);

            if (dt.Rows.Count > 0)
            {
                return entityManager.CreateOne<T>(dt, QueryTypes.SELECT);
            }


            return default(T);

        }


        public bool Exists(string fieldName, string value, string pkId = null)
        {
            return MySQLConnection.Exists(this.TableName, fieldName, value, pkId);
        }


        /// <summary>
        /// Returns the primary key value as string
        /// </summary>
        /// <typeparam name="T">Object Class</typeparam>
        /// <param name="entity">The Object</param>
        /// <returns></returns>
        public string InsertOrUpdate<T>(T entity)
        {
            string pk = entityManager.GetPkValue<T>(this.PkName, entity);

            if (String.IsNullOrEmpty(pk) || pk == "0")
            {
                return MySQLConnection.ExecuteTransaction(entityManager.InsertQuery<T>(this._tableName, this.PkName, entity), this.PkName);
            }
            else
            {
                MySQLConnection.ExecuteTransaction(entityManager.UpdateQuery<T>(this._tableName, this.PkName, entity));
                return pk;
            }

        }
       
    }
}
