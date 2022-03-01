using Microsoft.EntityFrameworkCore;
using Pedidos.Domain;
using Pedidos.Enums;
using System;

namespace Pedidos
{
    class Program
    {
        static void Main(string[] args)
        {
           InserirDados();
        }

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
            //opções para inserir registros com entity
            using var db = new Data.ApplicationContext();
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
    }
}
