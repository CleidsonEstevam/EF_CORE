using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pedidos.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pedidos.Data.Configurations
{
    public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("Clientes");
            builder.HasKey(p => p.Id);
            //COnfigurando as Propriedades
            builder.Property(p => p.Nome).HasColumnType("VARCHAR(80)").IsRequired();
            builder.Property(p => p.Telefone).HasColumnType("CHAR(11)");
            builder.Property(p => p.CEP).HasColumnType("CHAR(8)").IsRequired();
            builder.Property(p => p.Estado).HasColumnType("CHAR(2)").IsRequired();
            //Por a propriedade ser uma string o Entity cria como padrão Varchar
            builder.Property(p => p.Cidade).HasMaxLength(60).IsRequired();

            //Criando indice da coluna
            builder.HasIndex(i => i.Telefone).HasName("IDX_CLIENTE_TELEFONE");
        }
    }
}
