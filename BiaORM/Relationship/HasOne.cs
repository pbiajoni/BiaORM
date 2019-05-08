using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiaORM.Relationship
{
    public class HasOne
    {
        public HasOne(ITable table, Type type, string foreignKey, string @as)
        {
            Table = table ?? throw new ArgumentNullException(nameof(table));
            ForeignKey = foreignKey ?? throw new ArgumentNullException(nameof(foreignKey));
            As = @as ?? throw new ArgumentNullException(nameof(@as));

        }

        string _tableName { get; set; }
        public ITable Table { get; set; }
        public string ForeignKey { get; set; }
        public string As { get; set; }
    }
}
