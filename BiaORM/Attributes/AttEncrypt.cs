using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiaORM.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class AttEncrypt : Attribute
    {
        public CryptTypes CryptType { get; set; }

        public AttEncrypt(CryptTypes cryptType)
        {
            this.CryptType = cryptType;
        }
    }
}
