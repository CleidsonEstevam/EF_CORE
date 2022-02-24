using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pedidos.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pedidos.Data.Configurations
{
    class PedidoConfiguration : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            builder.ToTable("Pedidos");
            builder.HasKey(p => p.Id);
            //Devolve a hora atual do sistema
            builder.Property(p => p.DataInicio).HasDefaultValueSql("GETDATE()").ValueGeneratedOnAdd();
            builder.Property(p => p.Status).HasConversion<string>();
            builder.Property(p => p.TipoFrete).HasConversion<int>();
            builder.Property(p => p.Observacao).HasColumnType("VARCHAR(512)");

            //Faendo relacionamento
            builder.HasMany(p => p.Items)
                 .WithOne(p => p.Pedido)
                 //Ao apagar o cabeçalho de pedido os itens támbém serão deletados
                 .OnDelete(DeleteBehavior.Cascade);   //.Restrict obrigaria excluir primeiro itens depois o cabeçalho
        }
    }
}
