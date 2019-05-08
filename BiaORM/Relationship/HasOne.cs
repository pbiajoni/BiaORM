using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiaORM.Relationship
{
    public class HasOne
    {
        public HasOne(ITable table, string foreignKey)
        {
            Table = table ?? throw new ArgumentNullException(nameof(table));
            ForeignKey = foreignKey ?? throw new ArgumentNullException(nameof(foreignKey));
            this.As = null;
        }

        public HasOne(ITable table, string foreignKey, string @as) : this(table, foreignKey)
        {
            As = @as ?? throw new ArgumentNullException(nameof(@as));
        }

        public ITable Table { get; set; }
        public string ForeignKey { get; set; }
        public string As { get; set; }
    }
}
