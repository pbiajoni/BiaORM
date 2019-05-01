using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiaORM
{
    public enum QueryTypes
    {
        SELECT = 4,
        UPDATE = 3,
        DELETE = 2,
        ALL = 1,
        NONE = 0
    }
    public enum CryptTypes
    {
        MD5 = 0
    }

    public enum OperatorTypes
    {
        Equal = 0,
        Like = 1,
        BiggerThan = 2,
        SmallerThan = 3,
        StartsWith = 4,
        EndsWith = 5
    }

    public enum ConcatenatorTypes
    {
        None = 0,
        Or = 1,
        And = 2
    }
}
