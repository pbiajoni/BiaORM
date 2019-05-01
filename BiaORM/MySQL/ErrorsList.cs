using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiaORM.MySQL
{
    public class ErrorsList
    {
        #region Methods
        public static string GetMessage(int errorCode)
        {
            List<Error> errors = new List<Error>();
            errors.Add(new Error() { Code = 1451, Message = "Este registro não pode ser removido pois contém dependências" });

            if (errors.Where(x => x.Code == errorCode).FirstOrDefault() == null)
            {
                return null;
            }

            return errors.Find(x => x.Code == errorCode).Message;
        }
        #endregion
    }
}
