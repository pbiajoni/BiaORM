using BiaORM.MySQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiaORM.Examples.Classes
{
    public class School
    {
        public ITable Table { get; set; }
        public School()
        {
            this.Table = Global.DB.GetTable("schools");
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Owner { get; set; }

        public void CreateOrUpdate()
        {
            this.Table.InsertOrUpdate<School>(this);
        }
    }
}
