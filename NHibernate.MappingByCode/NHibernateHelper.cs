using System.Configuration;
using System.Reflection;
using NHibernate.Cfg;
using NHibernate.Connection;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Mapping.ByCode;
using NHibernate.Tool.hbm2ddl;
using Configuration = NHibernate.Cfg.Configuration;

namespace NHibernate.MappingByCode
{
    public class NHibernateHelper
    {
        private static ISessionFactory factory = RecuperaConfiguracao().BuildSessionFactory();

        public static Configuration RecuperaConfiguracao()
        {
            Configuration cfg = new Configuration();
            var mapper = new ModelMapper();
            mapper.AddMappings(Assembly.GetExecutingAssembly().GetExportedTypes());

            string conn = ConfigurationManager.ConnectionStrings["Conexao"].ConnectionString;

            cfg.DataBaseIntegration(c =>
            {
                c.ConnectionProvider<DriverConnectionProvider>();
                c.Dialect<MsSql2012Dialect>();
                c.Driver<SqlClientDriver>();
                c.ConnectionString = conn;
                c.SchemaAction = SchemaAutoAction.Update;
            });
            cfg.AddMapping(mapper.CompileMappingForAllExplicitlyAddedEntities());
            return cfg;
        }

        public static ISession AbreSessao()
        {
            return factory.OpenSession();
        }

        public static void GeraSchema()
        {
            Configuration cfg = RecuperaConfiguracao();
            new SchemaExport(cfg).Create(false, true);
        }

    }
}
