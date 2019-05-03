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
        ITable table = new Table(Global.DB, "schools");
        public School()
        {

        }

        public int Id { get; set; }
        public string Name { get; set; }

        public void Create()
        {
            this.table.InsertOrUpdate<School>(this);
        }
    }
}
