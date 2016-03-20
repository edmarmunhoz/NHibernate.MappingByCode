using System.Collections.Generic;

namespace NHibernate.MappingByCode.Model
{
    public class Categoria
    {
        public virtual int Id { get; set; }
        public virtual string Descricao { get; set; }
        public virtual IList<Produto> Produtos { get; set; }

        public Categoria()
        {
            Produtos = new List<Produto>();
        }
    }
}
