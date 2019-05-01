using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiaORM.MySQL
{
    public static class Extensions
    {
        
        public static TConvert ConvertTo<TConvert>(this object entity) where TConvert : new()
        {
            var convertProperties = TypeDescriptor.GetProperties(typeof(TConvert)).Cast<PropertyDescriptor>();
            var entityProperties = TypeDescriptor.GetProperties(entity).Cast<PropertyDescriptor>();

            var convert = new TConvert();

            foreach (var entityProperty in entityProperties)
            {
                var property = entityProperty;
                var convertProperty = convertProperties.FirstOrDefault(prop => prop.Name == property.Name);
                if (convertProperty != null)
                {
                    convertProperty.SetValue(convert, Convert.ChangeType(entityProperty.GetValue(entity), convertProperty.PropertyType));
                }
            }

            return convert;
        }
        public static string ToDBFormat(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd H:mm:ss");
        }

        public static string ToDB(this decimal number)
        {
            return number.ToString().Replace("R$", "").Replace(",", ".").Trim();
        }
    }
}
