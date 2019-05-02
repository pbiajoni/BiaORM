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
        public string Pk { get; set; }
        EntityManager entityManager = new EntityManager();
        string _tableName;
        public string TableName { get { return this._tableName; } set { this._tableName = value; } }
        public List<Field> Fields { get; internal set; }
        public MyConnection MySQLConnection { get; set; }
        bool _createInfo;
        bool _updateInfo;
        int _owner_id;

        public Table(string tableName)
        {
            this._tableName = tableName;
            this.Pk = "Id";
            if (this.Fields == null)
            {
                this.Fields = new List<Field>();
            }
        }

        public Table(MyConnection mySQL, string tableName)
        {
            this._tableName = tableName;
            this.MySQLConnection = mySQL;
            this.Pk = "Id";

            if (this.Fields == null)
            {
                this.Fields = new List<Field>();
            }
        }

        public Table(MyConnection mySQL, string tableName, bool createInfo, bool updateInfo, int owner_id)
        {
            this._tableName = tableName;
            this.MySQLConnection = mySQL;
            this._createInfo = createInfo;
            this._updateInfo = updateInfo;
            this._owner_id = owner_id;
            this.Pk = "Id";

            if (this.Fields == null)
            {
                this.Fields = new List<Field>();
            }
        }

        public Table(MyConnection mySQL, string tableName, bool createInfo, bool updateInfo, int owner_id, string pk)
        {
            this._tableName = tableName;
            this.MySQLConnection = mySQL;
            this._createInfo = createInfo;
            this._updateInfo = updateInfo;
            this._owner_id = owner_id;
            this.Pk = pk;

            if (this.Fields == null)
            {
                this.Fields = new List<Field>();
            }
        }

        public T FindOne<T>(string pk)
        {
            Where where = new Where(OperatorTypes.Equal, this.Pk, pk);
            return this.FindOne<T>(where);
        }

        public T FindOne<T>(Where where)
        {
            List<Where> wheres = new List<Where>();
            wheres.Add(where);
            return this.FindOne<T>(wheres);
        }

        public T FindOne<T>(List<Where> clauses)
        {
            string str_where = "";

            foreach (Where where in clauses)
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

        public bool FieldExists(string name)
        {
            return this.Fields.Any(x => x.Name == name);
        }

        public void AddField(string name, DateTime dateTime)
        {
            string date = dateTime.ToDBFormat();
            this.AddField(name, date);
        }

        public void AddField(string name, decimal value)
        {
            string decimalValue = value.ToDB();
            this.AddField(name, decimalValue);
        }

        public void AddField(string name, string value)
        {
            this.AddField(new Field(name, value));
        }

        public void AddField(Field field)
        {
            if (this.FieldExists(field.Name))
            {
                throw new Exception("O campo " + field.Name + " já existe");
            }

            this.Fields.Add(field);
        }

        public string InsertOrUpdate<T>(T entity)
        {
            string pk = entityManager.GetPkValue<T>(this.Pk, entity);

            if (String.IsNullOrEmpty(pk) || pk == "0")
            {
                return MySQLConnection.ExecuteTransaction(entityManager.InsertQuery<T>(this._tableName, this.Pk, entity), pk);
            }
            else
            {
                MySQLConnection.ExecuteTransaction(entityManager.UpdateQuery<T>(this._tableName, pk, entity));
                return pk;
            }

        }
    }
}
