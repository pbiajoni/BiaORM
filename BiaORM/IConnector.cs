using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiaORM
{
    interface IConnector
    {
        void Initialize();
        T Query<T>(string cmd);
    }
}
