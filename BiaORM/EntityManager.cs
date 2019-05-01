using BiaORM.Attributes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiaORM
{
    public class EntityManager
    {
        public T CreateOne<T>(DataTable dataTable, QueryTypes queryType)
        {
            T obj = Activator.CreateInstance<T>();

            foreach (var p in obj.GetType().GetProperties().Where(p => !p.GetGetMethod().GetParameters().Any()))
            {
                if (p != null && p.CanWrite)
                {
                    string name = p.Name;

                    Attribute[] attributes = Attribute.GetCustomAttributes(p);

                    if (!Attribute.IsDefined(p, typeof(AttIgnoreField)))
                    {
                        if (dataTable.Rows[0][p.Name] != DBNull.Value)
                        {
                            p.SetValue(obj, dataTable.Rows[0][p.Name]);
                        }
                    }
                }
            }

            return obj;
        }
        public List<T> CreateList<T>(DataTable dataTable, QueryTypes queryType)
        {
            List<T> list = Activator.CreateInstance<List<T>>();

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                T obj = Activator.CreateInstance<T>();

                foreach (var p in obj.GetType().GetProperties().Where(p => !p.GetGetMethod().GetParameters().Any()))
                {
                    if (p != null && p.CanWrite)
                    {
                        string name = p.Name;

                        Attribute[] attributes = Attribute.GetCustomAttributes(p);

                        if (!Attribute.IsDefined(p, typeof(AttIgnoreField)))
                        {
                            if (dataTable.Rows[0][p.Name] != DBNull.Value)
                            {
                                p.SetValue(obj, dataTable.Rows[i][p.Name]);
                            }
                        }
                    }

                    list.Add(obj);
                }
            }

            return list;
        }
    }
}
