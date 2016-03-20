using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.MappingByCode.Model;

namespace NHibernate.MappingByCode.Mapeamentos
{
    public class ProdutoMapping : ClassMapping<Produto>
    {
        public ProdutoMapping()
        {
            Table("Produto");
            Id(x => x.Id, x =>
            {
                x.Generator(Generators.Identity);
            });
            Property(x => x.Descricao, x =>
            {
                x.Length(50);
                x.NotNullable(true);
            });
            Property(x => x.Preco);

            ManyToOne(x => x.Categoria, x =>
            {
                x.Column("CategoriaId");
            });
        }
    }
}
