using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.MappingByCode.Model;

namespace NHibernate.MappingByCode.Mapeamentos
{
    public class CategoriaMapping : ClassMapping<Categoria>
    {
        public CategoriaMapping()
        {
            Table("Categoria");
            Id(x => x.Id, x =>
            {
                x.Generator(Generators.Identity);
            });
            Property(x => x.Descricao, x =>
            {
                x.Length(50);
                x.NotNullable(true);
            });

            Bag(x => x.Produtos, x =>
            {
                x.Key(y =>
                {
                    y.Column("CategoriaId");
                });
            }, x => x.OneToMany());
        }
    }
}
