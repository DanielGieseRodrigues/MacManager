﻿using MacManager.Application.Interfaces;
using MacManager.Application.Interfaces.Repositories;
using MacManager.Domain.Entities;

namespace MacManager.Application.UseCases.Produtos.AdicionarProdutoUseCase
{
    public class AdicionarProdutoUseCase : IUseCaseHandler<AdicionarProdutoRequest, AdicionarProdutoResponse>
    {
        private readonly IProdutoRepository _produtoRepository;

        public AdicionarProdutoUseCase(IProdutoRepository pedidoRepository)
        {
            _produtoRepository = pedidoRepository;
        }

        public async Task<AdicionarProdutoResponse> HandleAsync(AdicionarProdutoRequest request)
        {
            if (request.Produto == null)
            {
                return new AdicionarProdutoResponse
                {
                    Sucesso = false,
                    Produto = new Produto { Nome = "Produto não informado." },
                    Mensagem = "Produto não informado. Erro ao inserir produto na base de dados."
                };
            }

            await _produtoRepository.AdicionarAsync(request.Produto);

            return new AdicionarProdutoResponse
            {
                Sucesso = true,
                Mensagem = "Produto adicionado com sucesso.",
                Produto = request.Produto
            };
        }
    }
}