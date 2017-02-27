using System;
using System.Web;
using NHibernate;
using NHibernate.Cfg;
using System.IO;

namespace BPMS.Data
{
    public class SQLWatcher : EmptyInterceptor
    {
        public override NHibernate.SqlCommand.SqlString OnPrepareStatement(NHibernate.SqlCommand.SqlString sql)
        {
            System.Diagnostics.Debug.WriteLine("sql语句:" + sql);
            return base.OnPrepareStatement(sql);
        }
    }

    public class NHibernateHelper
    {
        public static ISessionFactory sessionFactory;

        public static string ConfigFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "NH.config");

        static NHibernateHelper()
        {
            Configuration config = new NHibernate.Cfg.Configuration().Configure(ConfigFilePath);
            sessionFactory = config.BuildSessionFactory();
        }
        public static ISession OpenSession()
        {
            return sessionFactory.OpenSession(new SQLWatcher());
        }

        public static void CloseSessionFactory()
        {
            if (sessionFactory != null)
            {
                sessionFactory.Close();
            }
        }
    }
}
