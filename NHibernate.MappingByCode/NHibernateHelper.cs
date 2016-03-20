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
    /// <summary>
    /// Helper quer fornece métodos para trabalhar com o NHibernate.
    /// </summary>
    public class NHibernateHelper
    {
        /// <summary>
        /// Propriedade estática do tipo ISessionFactory.
        /// </summary>
        private static ISessionFactory factory = RecuperaConfiguracao().BuildSessionFactory();

        /// <summary>
        /// Método estático que retorna a configuração do NHibernate para aplicação.
        /// </summary>
        /// <returns>Retorna obejto do tipo Configuration.</returns>
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

        /// <summary>
        /// Método estático utilizado para abrir uma sessão do NHibernate.
        /// </summary>
        /// <returns>Retorna um objeto do tipo ISession.</returns>
        public static ISession AbreSessao()
        {
            return factory.OpenSession();
        }

        /// <summary>
        /// Método estático utilizado para gerar a estrutura do banco de dados.
        /// </summary>
        public static void GeraSchema()
        {
            Configuration cfg = RecuperaConfiguracao();
            new SchemaExport(cfg).Create(false, true);
        }

    }
}
