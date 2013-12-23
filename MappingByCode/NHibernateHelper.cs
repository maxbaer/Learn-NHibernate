using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MappingByCode.Domain;
using MappingByCode.Mappings;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Mapping.ByCode;
using NHibernate.Tool.hbm2ddl;

namespace MappingByCode
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
                _configuration = new Configuration();
                _configuration.DataBaseIntegration(db =>
                    {
                        db.ConnectionString = "Data Source=MAXBAER;Initial Catalog=TestNHibernate;Integrated Security=True";
                        db.Dialect<MsSql2012Dialect>();
                    });
                var mapping = GetMappings();
                _configuration.AddDeserializedMapping(mapping,"NHSchemaTest");
                SchemaMetadataUpdater.QuoteTableAndColumns(_configuration);
                _sessionFactory = _configuration.BuildSessionFactory();
                new SchemaExport(_configuration).Execute(false,true,false);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static HbmMapping GetMappings()
        {
            var mapper = new ModelMapper();
            //mapper.AddMapping(Assembly.GetAssembly(typeof(AnimalMap)).GetExportedTypes());
            var mapping = mapper.CompileMappingForAllExplicitlyAddedEntities();
            return mapping;
        }

        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }
    }
}
