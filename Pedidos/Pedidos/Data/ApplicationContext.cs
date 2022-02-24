
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
            modelBuilder.Entity<Cliente>(p=>
            {
                p.ToTable("Clientes");
                p.HasKey(p => p.Id);
                //COnfigurando as Propriedades
                p.Property(p => p.Nome).HasColumnType("VARCHAR(80)").IsRequired();
                p.Property(p => p.Telefone).HasColumnType("CHAR(11)");
                p.Property(p => p.CEP).HasColumnType("CHAR(8)").IsRequired();
                p.Property(p => p.Estado).HasColumnType("CHAR(2)").IsRequired();
                //Por a propriedade ser uma string o Entity cria como padrão Varchar
                p.Property(p => p.Cidade).HasMaxLength(60).IsRequired();

                //Criando indice da coluna
                p.HasIndex(i => i.Telefone).HasName("IDX_CLIENTE_TELEFONE");
            });

            modelBuilder.Entity<Produto>(p =>
            {
                p.ToTable("Produtos");
                p.HasKey(p => p.Id);
                p.Property(p => p.CodigoDeBarras).HasColumnType("VARCHAR(14)").IsRequired();
                p.Property(p => p.Descricao).HasColumnType("VARCHAR(60)");
                p.Property(p => p.Valor).IsRequired();
                //Qual tipo o ENUM vai ser gravado na base de dados
                p.Property(p => p.TipoProduto).HasConversion<string>();
            });

            modelBuilder.Entity<Pedido>(p =>
            {
                p.ToTable("Pedidos");
                p.HasKey(p => p.Id);
                //Devolve a hora atual do sistema
                p.Property(p => p.DataInicio).HasDefaultValueSql("GETDATE()").ValueGeneratedOnAdd();
                p.Property(p => p.Status).HasConversion<string>();
                p.Property(p => p.TipoFrete).HasConversion<int>();
                p.Property(p => p.Observacao).HasColumnType("VARCHAR(512)");

                //Faendo relacionamento
                p.HasMany(p => p.Items)
                     .WithOne(p => p.Pedido)
                     //Ao apagar o cabeçalho de pedido os itens támbém serão deletados
                     .OnDelete(DeleteBehavior.Cascade);   //.Restrict obrigaria excluir primeiro itens depois o cabeçalho
            });

            modelBuilder.Entity<PedidoItem>(p =>
            {
                p.ToTable("PedidoItens");
                p.HasKey(p => p.Id); 
                p.Property(p => p.Quantidade).HasDefaultValue(1).IsRequired();
                p.Property(p => p.Valor).IsRequired();
                p.Property(p => p.Desconto).IsRequired();

            });
        }
    }
}
