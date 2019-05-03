using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiaORM.Relationship
{
    public class HasOne
    {
        public HasOne()
        {
        }

        public HasOne(string targetTableName, Type targetEntityType, string fkFieldName, string PropertyName)
        {
            TargetTableName = targetTableName ?? throw new ArgumentNullException(nameof(targetTableName));
            FkFieldName = fkFieldName ?? throw new ArgumentNullException(nameof(fkFieldName));
            IEnumerablePropertyName = PropertyName ?? throw new ArgumentNullException(nameof(PropertyName));
            TargetEntityType = targetEntityType ?? throw new ArgumentNullException(nameof(targetEntityType));
        }

        public string TargetTableName { get; set; }
        public string FkFieldName { get; set; }
        public string IEnumerablePropertyName { get; set; }

        public Type TargetEntityType { get; set; }
    }
}
