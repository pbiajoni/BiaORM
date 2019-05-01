using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiaORM.MySQL
{
    public class Field
    {
        string _name;
        string _value;

        public Field()
        {

        }

        public string Name
        {
            get
            {
                return this._name;
            }

            set
            {
                this._name = value;
            }
        }

        public string Value
        {
            get
            {
                return this._value;
            }

            set
            {
                this._value = value;
            }
        }

        public Field(string name, string value)
        {
            this._name = name;
            this._value = value;
        }
    }
}
