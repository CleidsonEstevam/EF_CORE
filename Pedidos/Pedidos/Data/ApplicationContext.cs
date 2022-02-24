﻿
using Microsoft.EntityFrameworkCore;
using Pedidos.Domain;

namespace Pedidos.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Pedido> Pedidos { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        {
            optionsBuilder.UseSqlServer("Data Source=CAsa\\SQLEXPRESS;Initial Catalog=Pedidos; Integrated Security=True");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //O fluent API vai varrer todo assembly em busca das classes
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);
        }
    }
}
