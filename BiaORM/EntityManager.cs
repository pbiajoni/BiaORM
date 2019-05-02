using BiaORM.Attributes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BiaORM
{
    public class EntityManager
    {

        public string GetPkValue<T>(string pkName, T entity)
        {
            foreach (var p in entity.GetType().GetProperties().Where(p => !p.GetGetMethod().GetParameters().Any()))
            {
                if (p.Name.ToLower() == pkName.ToLower())
                {
                    var value = p.GetValue(entity, null);
                    return value.ToString();
                }
            }

            return null;
        }

        public string UpdateQuery<T>(string tableName, string pkName, T entity)
        {
            string cmd = "update " + tableName + " set ";

            foreach (var p in entity.GetType().GetProperties().Where(p => !p.GetGetMethod().GetParameters().Any()))
            {
                if ((p != null && p.CanRead) && (p.Name.ToLower() != pkName.ToLower()))
                {
                    bool ignore = false;

                    if (Attribute.IsDefined(p, typeof(AttIgnoreField)))
                    {
                        if (GetIgnoreFieldQueryType(p) == QueryTypes.UPDATE)
                        {
                            ignore = true;
                        }
                    }

                    if (!ignore)
                    {
                        string piValue = p.GetValue(entity, null).ToString();

                        if (Attribute.IsDefined(p, typeof(AttEncrypt)))
                        {
                            CryptTypes cryptType = GetCryptType(p);

                            if (cryptType == CryptTypes.MD5)
                            {
                                cmd += " " + p.Name.ToLower() + "=md5('" + piValue + "'),";
                            }
                        }
                        else
                        {
                            cmd += " " + p.Name.ToLower() + "='" + piValue + "',";
                        }
                    }
                }
            }

            cmd = cmd.TrimEnd(',') + " where " + pkName + " = '" + GetPkValue<T>(pkName, entity) + "';";
            return cmd;
        }
        public string InsertQuery<T>(string tableName, string pkName, T entity)
        {
            string cmd = "insert into " + tableName + " (";
            string fields = "";
            string values = "";

            foreach (var p in entity.GetType().GetProperties().Where(p => !p.GetGetMethod().GetParameters().Any()))
            {
                if ((p != null && p.CanRead) && (p.Name.ToLower() != pkName.ToLower()))
                {
                    bool ignore = false;

                    if (Attribute.IsDefined(p, typeof(AttIgnoreField)))
                    {
                        if (GetIgnoreFieldQueryType(p) == QueryTypes.INSERT)
                        {
                            ignore = true;
                        }
                    }

                    if (!ignore)
                    {
                        fields += "'" + p.Name.ToLower() + "',";
                        string piValue = p.GetValue(entity, null).ToString();

                        if (Attribute.IsDefined(p, typeof(AttEncrypt)))
                        {
                            CryptTypes cryptType = GetCryptType(p);

                            if (cryptType == CryptTypes.MD5)
                            {
                                values += "md5('" + piValue + "'),";
                            }
                        }
                        else
                        {
                            values += "'" + piValue + "',";
                        }
                    }
                }
            }

            fields = fields.TrimEnd(',');
            values = values.TrimEnd(',');

            cmd += fields + ") values (" + values + ");";

            return cmd;
        }

        QueryTypes GetIgnoreFieldQueryType(PropertyInfo pi)
        {
            if (Attribute.IsDefined(pi, typeof(AttIgnoreField)))
            {
                AttIgnoreField attIgnoreField = pi.GetCustomAttribute<AttIgnoreField>();
                return attIgnoreField.QueryType;
            }

            return QueryTypes.NONE;
        }

        CryptTypes GetCryptType(PropertyInfo pi)
        {
            if (Attribute.IsDefined(pi, typeof(AttEncrypt)))
            {
                AttEncrypt attEncrypt = pi.GetCustomAttribute<AttEncrypt>();
                return attEncrypt.CryptType;
            }

            return CryptTypes.None;
        }

        T GetBiaORMAtrribute<T>(PropertyInfo pi)
        {
            T obj = Activator.CreateInstance<T>();

            if (obj.GetType() == typeof(AttIgnoreField))
            {
                AttIgnoreField attIgnoreField = (AttIgnoreField)(object)obj;
            }

            return obj;
        }

        public T CreateOne<T>(DataTable dataTable, QueryTypes queryType)
        {
            T obj = Activator.CreateInstance<T>();

            foreach (var p in obj.GetType().GetProperties().Where(p => !p.GetGetMethod().GetParameters().Any()))
            {
                if (p != null && p.CanWrite)
                {
                    string name = p.Name;
                    bool ignore = false;
                    Attribute[] attributes = Attribute.GetCustomAttributes(p);

                    foreach (Attribute attribute in attributes)
                    {
                        if (attribute.GetType() == typeof(AttIgnoreField))
                        {
                            AttIgnoreField attIgnoreField = (AttIgnoreField)attribute;

                            if (queryType == attIgnoreField.QueryType)
                            {
                                ignore = true;
                            }
                        }
                    }

                    if (!ignore && (dataTable.Rows[0][p.Name] != DBNull.Value))
                    {
                        p.SetValue(obj, dataTable.Rows[0][p.Name]);
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
                        bool ignore = false;
                        Attribute[] attributes = Attribute.GetCustomAttributes(p);

                        foreach (Attribute attribute in attributes)
                        {
                            if (attribute.GetType() == typeof(AttIgnoreField))
                            {
                                AttIgnoreField attIgnoreField = (AttIgnoreField)attribute;

                                if (queryType == attIgnoreField.QueryType)
                                {
                                    ignore = true;
                                }
                            }
                        }

                        if (!ignore && (dataTable.Rows[i][p.Name] != DBNull.Value))
                        {
                            p.SetValue(obj, dataTable.Rows[i][p.Name]);
                        }
                    }

                    list.Add(obj);
                }
            }

            return list;
        }
    }
}
