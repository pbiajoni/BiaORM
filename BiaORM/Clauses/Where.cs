using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiaORM.Clauses
{
    public class Where
    {
        public OperatorTypes OperatorType { get; set; }
        public string FieldName { get; set; }
        public string Value { get; set; }
        public string Query
        {
            get
            {
                switch (this.OperatorType)
                {
                    case OperatorTypes.Equal:
                        return " " + this.FieldName + " = " + Value + " ";
                    case OperatorTypes.Like:
                        return " " + this.FieldName + " like %" + Value + "% ";
                    case OperatorTypes.BiggerThan:
                        return " " + this.FieldName + " > " + Value + " ";
                    case OperatorTypes.SmallerThan:
                        return " " + this.FieldName + " < " + Value + " ";
                    case OperatorTypes.StartsWith:
                        return " " + this.FieldName + " like %" + Value + " ";
                    case OperatorTypes.EndsWith:
                        return " " + this.FieldName + " like " + Value + "% ";
                    default:
                        return "0";
                }
            }
        }

        public Where()
        {

        }

        public Where(OperatorTypes operatorType, string fieldName, string value)
        {
            OperatorType = operatorType;
            FieldName = fieldName ?? throw new ArgumentNullException(nameof(fieldName));
            Value = value ?? throw new ArgumentNullException(nameof(value));
        }
    }
}
