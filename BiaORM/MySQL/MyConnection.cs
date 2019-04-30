using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiaORM.MySQL
{
    public class MyConnection : IConnector
    {
        public MyConnection()
        {

        }

        public void Initialize()
        {
            throw new NotImplementedException();
        }

        public T Query<T>(string cmd)
        {
            throw new NotImplementedException();
        }
    }
}
