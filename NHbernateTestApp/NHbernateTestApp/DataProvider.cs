using NHbernateTestApp;
using NHibernate;
using NHibernate.Metadata;
using NHibernate.Persister.Entity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;

namespace BPMS.Data
{
    public abstract class DataProvider<T> 
        where T : class
    {
        protected static Regex SqlKeywordRegex = new Regex("(update)|(insert)|(delete)", RegexOptions.IgnoreCase);

        public DataProvider()
        {
            this._lock = new object();
            this._loading = false;
            this._watch = new Stopwatch();

            this._meList = new List<MemberExpression>();
            //foreach (PropertyInfo property in typeof(T).GetProperties())
            //{
            //    this._meList.Add(Expression.Property(null, property));
            //}

            this._properties = typeof(T).GetProperties();
        }

        object _lock;
        bool _loading;
        Stopwatch _watch;

        PropertyInfo[] _properties;
        List<MemberExpression> _meList;

        public virtual void Insert(T model)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                session.Save(model);
                session.Flush();
            }
        }

        public virtual void Delete(T model)
        {
            if (model == null)
            {
                return;
            }

            using (ISession session = NHibernateHelper.OpenSession())
            {
                session.Delete(model);
                session.Flush();
            }
        }

        public virtual void Update(T model)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                session.Update(model);
                session.Flush();
            }
        }

        public virtual T Get(object id)
        {
            T model = default(T);
            using (ISession session = NHibernateHelper.OpenSession())
            {
                model = session.Load<T>(id);
            }

            return model;
        }

        public virtual ISession Lock(object id)
        {
            ISession session = NHibernateHelper.OpenSession();

            T model = session.Get<T>(id, LockMode.Write);

            return session;
        }

        public virtual List<T> GetTop(int count)
        {
            List<T> models = null;
            using (ISession session = NHibernateHelper.OpenSession())
            {
                models = session.QueryOver<T>()
                    .Take(count).List().ToList();
            }

            return models;
        }


        protected virtual object GetValueFromSqlDataReader(PropertyInfo prop, SqlDataReader reader)
        {
            object value = reader[prop.Name];
            if (value == DBNull.Value || value == null)
            {
                return null;
            }
            Type valueType = value.GetType();
            Type propertyType = ReflectionHelper.GetDefinitionType(prop.PropertyType);
            if (valueType == propertyType)
            {
                return value;
            }
            else if (valueType == typeof(Guid) && propertyType == typeof(string))
            {
                return value.ToString();
            }
            else if (valueType == typeof(int) && propertyType.IsEnum)
            {
                return Enum.Parse(propertyType, value.ToString());
            }
            return value;
        }

        public virtual List<T> SelectBySqlDataReader()
        {
            return SelectBySqlDataReader(null, "");
        }

        public virtual List<T> SelectBySqlDataReader(int? count, string additionalSql)
        {
            PropertyInfo[] properties = typeof(T).GetProperties();
            List<T> models = new List<T>();
            string sql = this.GetSelectSql(count, additionalSql);
            using (ISession session = NHibernateHelper.OpenSession())
            {
                SqlCommand cmd = new SqlCommand(sql, session.Connection as SqlConnection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    T model = Activator.CreateInstance<T>();
                    foreach (PropertyInfo prop in properties)
                    {
                        object value = this.GetValueFromSqlDataReader(prop, reader);
                        if (value != null)
                        {
                            prop.SetValue(model, value, null);
                        }
                    }
                    models.Add(model);
                    if (models.Count % 1000000 == 0)
                    {
                        Console.WriteLine(models.Count + " " + DateTime.Now);
                    }
                }
                reader.Close();
            }

            return models;
        }

        private string GetSelectSql(int? count, string additionalSql)
        {
            IClassMetadata metadata = NHibernateHelper.sessionFactory.GetClassMetadata(typeof(T));
            SingleTableEntityPersister entityPersister = metadata as SingleTableEntityPersister;
            string[] subclassColumnClosure = entityPersister.GetType().GetProperty("SubclassColumnClosure", BindingFlags.NonPublic | BindingFlags.Instance)
                .GetValue(entityPersister, null) as string[];
            List<string> selectColumns = new List<string>();
            string idColumn = entityPersister.IdentifierPropertyName;
            selectColumns.Add(idColumn);
            for (int i = 0; i < entityPersister.PropertyNames.Length; i++)
            {
                string propertyName = entityPersister.PropertyNames[i];
                string columnName = subclassColumnClosure[i];
                string selectColumn = columnName;
                if (propertyName != columnName)
                {
                    selectColumn = string.Format("{0} as {1}", columnName, propertyName);
                }
                selectColumns.Add(selectColumn);
            }
            string top = "";
            if (count.HasValue)
            {
                top = "top " + count;
            }

            string sql = string.Format("SELECT {0} {1} from {2} {3}", top, string.Join(",", selectColumns), entityPersister.TableName, additionalSql);

            return sql;
        }

        public virtual T SelectFirst(Expression<Func<T, bool>> expression)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                return session.QueryOver<T>().Where(expression).Take(1).SingleOrDefault();
            }
        }
    }
}
