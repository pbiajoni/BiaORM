using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiaORM.Relationship
{
    public class HasOne
    {
        /// <summary>
        /// Nome da coluna local que tem a Id da referência
        /// </summary>
        public string LocalRelationName { get; set; }
        /// <summary>
        /// Nome da tabela remota
        /// </summary>
        public string TagetTableName { get; set; }
        /// <summary>
        /// Alias da tabela remota
        /// </summary>
        public string As { get; set; }
    }
}
