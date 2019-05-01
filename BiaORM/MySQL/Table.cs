using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiaORM.MySQL
{
    public class Table : ITable
    {
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

        public void AddField(string vame, string value)
        {
            throw new NotImplementedException();
        }
    }
}
