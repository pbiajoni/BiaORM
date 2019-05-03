using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiaORM
{
    public class AcceptableTypes
    {
        public static bool IsAcceptable(Type type)
        {
            Console.WriteLine(type.ToString());

            if(type == typeof(string))
            {
                return true;
            }

            if(type == typeof(int))
            {
                return true;
            }

            if(type == typeof(DateTime))
            {
                return true;
            }

            if (type == typeof(decimal))
            {
                return true;
            }

            if (type == typeof(bool))
            {
                return true;
            }

            return false;
        }
    }
}
