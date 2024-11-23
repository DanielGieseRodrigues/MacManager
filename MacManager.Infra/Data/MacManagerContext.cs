using MacManager.Domain.Entities;
using MacManager.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace MacManager.Infra.Data
{
    //Contexto padrao do EF, nada de muito novo por aqui, classe de relacionamentos , seeding, etc.
    //Obs : Chamei de MacManager em relacao ao MCDonalds, um nome ficticio apenas para nosso projetinho. 
    public class MacManagerContext : DbContext
    {
        public MacManagerContext(DbContextOptions<MacManagerContext> options) : base(options) { }

        //DbSets da aplicacao, da pra pensar neles como nossas tables classicas do sql.
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<PedidoProduto> PedidoProdutos { get; set; }


        //Fazendo o seeding dos dados iniciais, tentei por bastante produto para ficar legal de usar.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PedidoProduto>()
                .HasKey(pp => new { pp.PedidoId, pp.ProdutoId });

            modelBuilder.Entity<PedidoProduto>()
                .HasOne(pp => pp.Pedido)
                .WithMany(p => p.PedidoProdutos)
                .HasForeignKey(pp => pp.PedidoId);

            modelBuilder.Entity<PedidoProduto>()
                .HasOne(pp => pp.Produto)
                .WithMany()
                .HasForeignKey(pp => pp.ProdutoId);

            //Seed inicial, adicionei alguns ao exemplo inicial fornecido para maior dinamica.
            modelBuilder.Entity<Produto>().HasData(
                //Fritos
                new Produto { Id = 1, Nome = "Porção de batata frita", AreaCozinha = AreaCozinha.Fritos, Valor = 15 },
                new Produto { Id = 2, Nome = "Coxinha de frango", AreaCozinha = AreaCozinha.Fritos, Valor = 10 },
                new Produto { Id = 3, Nome = "Pastel de carne", AreaCozinha = AreaCozinha.Fritos, Valor = 12 },

                //Grelhados
                new Produto { Id = 4, Nome = "Porco grelhado", AreaCozinha = AreaCozinha.Grelhados, Valor = 50 },
                new Produto { Id = 5, Nome = "Frango grelhado", AreaCozinha = AreaCozinha.Grelhados, Valor = 35 },
                new Produto { Id = 6, Nome = "Picanha grelhado", AreaCozinha = AreaCozinha.Grelhados, Valor = 98 },

                //Saladas
                new Produto { Id = 7, Nome = "Salada de batatas", AreaCozinha = AreaCozinha.Saladas, Valor = 20 },
                new Produto { Id = 8, Nome = "Salada de frios", AreaCozinha = AreaCozinha.Saladas, Valor = 18 },
                new Produto { Id = 9, Nome = "Salada verde", AreaCozinha = AreaCozinha.Saladas, Valor = 15 },

                //Bebidas
                new Produto { Id = 10, Nome = "Refrigerante", AreaCozinha = AreaCozinha.Bebidas, Valor = 8 },
                new Produto { Id = 11, Nome = "Suco de laranja", AreaCozinha = AreaCozinha.Bebidas, Valor = 10 },
                new Produto { Id = 12, Nome = "Vitamina de banana", AreaCozinha = AreaCozinha.Bebidas, Valor = 6 },

                //Sobremesas
                new Produto { Id = 13, Nome = "Sorvete", AreaCozinha = AreaCozinha.Sobremesas, Valor = 12 },
                new Produto { Id = 14, Nome = "Torta alemã", AreaCozinha = AreaCozinha.Sobremesas, Valor = 18 },
                new Produto { Id = 15, Nome = "Cuca", AreaCozinha = AreaCozinha.Sobremesas, Valor = 10 });
        }
    }
}
