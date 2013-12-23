using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using XMLmapping.Model;

namespace XMLmapping
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
                if (_sessionFactory == null)
                {
                    InitializeSessionFactory();
                }
                return _sessionFactory;
            }
        }

        private static void InitializeSessionFactory()
        {
            try
            {
                _configuration = new Configuration();
                _configuration.Configure();
                _configuration.AddAssembly(typeof (Product).Assembly);
                _sessionFactory = _configuration.BuildSessionFactory();

                new SchemaExport(_configuration).Execute(false,true,false);
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
