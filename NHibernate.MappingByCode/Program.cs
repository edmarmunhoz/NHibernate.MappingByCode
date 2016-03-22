using System;
using NHibernate.MappingByCode.Model;

namespace NHibernate.MappingByCode
{
    class Program
    {
        static void Main(string[] args)
        {
            Categoria categoria1 = new Categoria()
            {
                Descricao = ".Net"
            };

            Categoria categoria2 = new Categoria()
            {
                Descricao = "Banco de dados"
            };

            Produto produto1 = new Produto()
            {
                Descricao = "C# e orientação a objetos",
                Preco = 100,
                Categoria = categoria1
            };

            Produto produto2 = new Produto()
            {
                Descricao = "C# avançado",
                Preco = 200,
                Categoria = categoria1
            };

            Produto produto3 = new Produto()
            {
                Descricao = "SQL Server",
                Preco = 200,
                Categoria = categoria2
            };

            ISession sessao = NHibernateHelper.AbreSessao();

            sessao.BeginTransaction();
            sessao.Save(categoria1);
            sessao.Save(categoria2);
            sessao.Save(produto1);
            sessao.Save(produto2);
            sessao.Save(produto3);
            sessao.Transaction.Commit();

            var produtos = sessao.Get<Produto>(1);

            Console.ReadKey();
        }
    }
}
