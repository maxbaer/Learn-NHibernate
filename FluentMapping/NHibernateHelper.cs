using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentMapping.Domain;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using System.Configuration;
using NHibernate.Tool.hbm2ddl;


namespace FluentMapping
{
    public sealed class NHibernateHelper
    {
        private static ISessionFactory _sessionFactory = null;
        private static readonly object PadLock = new object();
        private static Configuration _configuration;

        private static ISessionFactory SessionFactory
        {
            get
            {
                lock (PadLock)
                {
                    if (_sessionFactory == null)
                    {
                        InitializeSessionFactory();
                    }
                    return _sessionFactory;
                }
            }
        }

        private static void InitializeSessionFactory()
        {
            try
            {
                _sessionFactory = Fluently.Configure()
                                          .Database(
                                              MsSqlConfiguration.MsSql2008.ConnectionString(
                                              "Data Source=MAXBAER;Initial Catalog=TestNHibernate;Integrated Security=True"))
                                          .Mappings(x => x.FluentMappings.AddFromAssemblyOf<Movie>())
                                          .ExposeConfiguration(cfg => new SchemaExport(cfg).Create(true, true))
                                          .BuildSessionFactory();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }

    }
}
