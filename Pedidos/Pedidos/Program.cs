using Microsoft.EntityFrameworkCore;
using Pedidos.Domain;
using Pedidos.Enums;
using System;
using System.Linq;

namespace Pedidos
{
    class Program
    {
        static void Main(string[] args)
        {
            //InserirDados();
            //InserirDadosEmMassa();
            ConsultarDados();
        }

        #region "Insert"
        private static void InserirDados()
        {
            var produto = new Produto
            {
                Descricao = "Produto Teste",
                CodigoDeBarras = "12345678910",
                Valor = 10m,
                TipoProduto = TipoProduto.MercadoriaRevenda,
                Ativo = true
            };
            
            using var db = new Data.ApplicationContext();
            //opções para inserir registros com entity
            //1:
            //db.Produtos.Add(produto);
            //2:Metodo generico
            //db.Set<Produto>().Add(produto);
            //3: Forçando rastreio
            //db.Entry(produto).State = EntityState.Added;
            //4:
            db.Add(produto);

            var registros = db.SaveChanges();
            Console.WriteLine(registros);
        }

        private static void InserirDadosEmMassa() 
        {
            var produto = new Produto
            {
                Descricao = "Produto Teste",
                CodigoDeBarras = "12345678910",
                Valor = 10m,
                TipoProduto = TipoProduto.MercadoriaRevenda,
                Ativo = true
            };
            var cliente = new Cliente
            {
                Nome = "Rafael Almeida",
                CEP = "99999000",
                Cidade = "Itapuí",
                Estado = "SP",
                Telefone = "14988888888",
            };
        var listaDeClientes = new[]
            {
            new Cliente
            {
                Nome = "Teste1",
                CEP = "99999000",
                Cidade = "Itapuí",
                Estado = "SP",
                Telefone = "14988888888",
            },
             new Cliente
            {
                Nome = "Teste2",
                CEP = "99999000",
                Cidade = "Itapuí",
                Estado = "SP",
                Telefone = "14988888888",
            },
         };

            using var db = new Data.ApplicationContext();
            //opções para inserir registros  em massa com entity
            //1: adiciona mais de um objeto por vez
            //db.AddRange(produto, cliente); 
            //2:adiciona uma lista
            db.Clientes.AddRange(listaDeClientes);
            var registros = db.SaveChanges();
            Console.WriteLine(registros);
        }
        #endregion

        #region "Consultar"
        private static void ConsultarDados() 
        {
            using var db = new Data.ApplicationContext();
            //var consultarPorSintaxe = (from c in db.Clientes where c.Id > 0 select c).ToList();
            var consultaPorMetodo = db.Clientes
                .Where(p => p.Id > 0)
                .OrderBy(p => p.Id)
                //Para que o Entity não salve as consultas em memória quando usando o FIND
                //.AsNoTracking()
                .ToList();
            foreach (var cliente in consultaPorMetodo) 
            {
                Console.WriteLine($"Consultando Cliente: {cliente.Id}");
                //Find busca pimeiro no mapeamento da memoria para depois ir ao banco
                //db.Clientes.Find(cliente.Id);
                db.Clientes.FirstOrDefault(p => p.Id == cliente.Id);
            }

        }
        #endregion
    }
}
