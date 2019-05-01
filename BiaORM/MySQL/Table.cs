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

            if (this.Fields == null)
            {
                this.Fields = new List<Field>();
            }
        }

        public Table(MyConnection mySQL, string tableName)
        {
            this._tableName = tableName;
            this.MySQLConnection = mySQL;

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

            if (this.Fields == null)
            {
                this.Fields = new List<Field>();
            }
        }

        public T FindOne<T>(int id)
        {
            Where where = new Where(OperatorTypes.Equal, "id", id.ToString());
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
            bool first = true;

            foreach(Where where in clauses)
            {
                string query = where.Query;

                if (first)
                {
                    if (where.UseParentheses)
                    {
                        
                    }
                }
                else
                {

                }    
            }


            T obj = Activator.CreateInstance<T>();
            return obj;
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
    }
}
