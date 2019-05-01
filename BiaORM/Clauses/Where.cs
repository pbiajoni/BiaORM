using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiaORM.Clauses
{
    public class Where
    {
        public ConcatenatorTypes ConcatenatorType { get; set; }
        public OperatorTypes OperatorType { get; set; }
        public string FieldName { get; set; }
        public string Value { get; set; }
        public bool UseParentheses { get; set; }
        public bool QuotationMark { get; set; }
        public string Query
        {
            get
            {
                string query = "";
                switch (this.OperatorType)
                {
                    case OperatorTypes.Equal:
                        query = " " + this.FieldName + " =" + (this.QuotationMark ? " '" + Value + "' " : " " + Value + " ");
                        break;
                    case OperatorTypes.Like:
                        query = " " + this.FieldName + " like '%" + Value + "%' ";
                        break;
                    case OperatorTypes.BiggerThan:
                        query = " " + this.FieldName + " >" + (this.QuotationMark ? " '" + Value + "' " : " " + Value + " ");
                        break;
                    case OperatorTypes.SmallerThan:
                        query = " " + this.FieldName + " <" + (this.QuotationMark ? " '" + Value + "' " : " " + Value + " ");
                        break;
                    case OperatorTypes.StartsWith:
                        query = " " + this.FieldName + " like '%" + Value + "' ";
                        break;
                    case OperatorTypes.EndsWith:
                        query = " " + this.FieldName + " like '" + Value + "%' ";
                        break;
                    default:
                        throw new Exception("Operator type is missing");
                }

                if (this.UseParentheses)
                {
                    query = " (" + query + ") ";
                }

                if (this.ConcatenatorType == ConcatenatorTypes.None)
                {
                    return query;
                }

                return " " + this.ConcatenatorType.ToString().ToLower() + query;
            }
        }

        public Where()
        {

        }

        public Where(OperatorTypes operatorType, string fieldName, string value, bool quotationMark = true)
        {
            OperatorType = operatorType;
            ConcatenatorType = ConcatenatorTypes.None;
            FieldName = fieldName ?? throw new ArgumentNullException(nameof(fieldName));
            Value = value ?? throw new ArgumentNullException(nameof(value));
            QuotationMark = quotationMark;
        }

        public Where(ConcatenatorTypes concatenatorType, OperatorTypes operatorType, string fieldName, string value, bool quotationMark = true)
        {
            OperatorType = operatorType;
            ConcatenatorType = concatenatorType;
            FieldName = fieldName ?? throw new ArgumentNullException(nameof(fieldName));
            Value = value ?? throw new ArgumentNullException(nameof(value));
            QuotationMark = quotationMark;
        }

        public Where(ConcatenatorTypes concatenatorType, bool useParentheses, OperatorTypes operatorType, string fieldName, string value, bool quotationMark = true)
        {
            OperatorType = operatorType;
            ConcatenatorType = concatenatorType;
            UseParentheses = useParentheses;
            FieldName = fieldName ?? throw new ArgumentNullException(nameof(fieldName));
            Value = value ?? throw new ArgumentNullException(nameof(value));
            QuotationMark = quotationMark;
        }
    }
}
