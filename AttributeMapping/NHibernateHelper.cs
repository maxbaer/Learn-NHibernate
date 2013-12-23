using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Mapping.Attributes;
using NHibernate.Tool.hbm2ddl;


namespace AttributeMapping
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
                var stream = new MemoryStream();
                HbmSerializer.Default.Validate = true;
                HbmSerializer.Default.Serialize(stream, typeof (Osoba).Assembly);
                stream.Position = 0;
                _configuration = new Configuration();
                _configuration.Configure();
                _configuration.AddInputStream(stream);
                _sessionFactory = _configuration.BuildSessionFactory();
                new SchemaExport(_configuration).Execute(false, true, false);
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
