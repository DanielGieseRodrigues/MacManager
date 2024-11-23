using MacManager.Application.Interfaces;
using MacManager.Application.Interfaces.Repositories;
using MacManager.Application.UseCases.Pedidos;
using MacManager.Application.UseCases.Pedidos.AdicionarPedidoUseCase;
using MacManager.Application.UseCases.Pedidos.FecharPedidoUseCase;
using MacManager.Application.UseCases.Pedidos.ListarPedido;
using MacManager.Application.UseCases.Pedidos.ListarPedidosUseCase;
using MacManager.Application.UseCases.Produtos.AdicionarProdutoUseCase;
using MacManager.Application.UseCases.Produtos.EditarProdutoUseCase;
using MacManager.Application.UseCases.Produtos.ExcluirProdutoUseCase;
using MacManager.Application.UseCases.Produtos.ListarProdutosUseCase;
using MacManager.Infra.Data;
using MacManager.Infra.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MacManager.Infra
{
    public static class ServiceCollection
    {
        //Service collection padrao, metodo de extensao .net recebendo iservicecollection, por questoes de separação e infraestrutura.
        public static void AdicionarServiceCollection(this IServiceCollection services)
        {
            // Configuração do DbContext usando EF InMemory (poderia ser um banco de dados real, um codefirst da vida, ou etc)
            services.AddDbContext<MacManagerContext>(options =>
                options.UseInMemoryDatabase("MacManagerDb"));

            // Registro dos Use Cases de Produtos
            services.AddScoped<IUseCaseHandler<AdicionarProdutoRequest, AdicionarProdutoResponse>, AdicionarProdutoUseCase>();
            services.AddScoped<IUseCaseHandler<EditarProdutoRequest, EditarProdutoResponse>, EditarProdutoUseCase>();
            services.AddScoped<IUseCaseHandler<ExcluirProdutoRequest, ExcluirProdutoResponse>, ExcluirProdutoUseCase>();
            services.AddScoped<IUseCaseHandler<ListarProdutosRequest, ListarProdutosResponse>, ListarProdutosUseCase>();

            // Registro dos Use Cases de Pedidos
            services.AddScoped<IUseCaseHandler<AdicionarPedidoRequest, AdicionarPedidoResponse>, AdicionarPedidoUseCase>();
            services.AddScoped<IUseCaseHandler<FecharPedidoRequest, FecharPedidoResponse>, FecharPedidoUseCase>();
            services.AddScoped<IUseCaseHandler<ListarPedidosRequest, ListarPedidosResponse>, ListarPedidosUseCase>();
            services.AddScoped<IUseCaseHandler<ListarPedidosPorAreaRequest, ListarPedidosPorAreaResponse>, ListarPedidosPorAreaUseCase>();

            // Registro de repositórios de dados
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IPedidoRepository, PedidoRepository>();
            services.AddScoped<IPedidoProdutoRepository, PedidoProdutoRepository>();
        }
    }
}
