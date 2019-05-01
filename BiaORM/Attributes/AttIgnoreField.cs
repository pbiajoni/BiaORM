using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiaORM.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class AttIgnoreField : Attribute
    {
        public QueryTypes QueryType;
        public AttIgnoreField()
        {

        }

        public AttIgnoreField(QueryTypes queryType)
        {
            this.QueryType = queryType;
        }
    }
}
