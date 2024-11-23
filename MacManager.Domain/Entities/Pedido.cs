using MacManager.Domain.ValueObjects;
using System.Text.Json.Serialization;

namespace MacManager.Domain.Entities
{
    //Representacao poco ( classe anemica) da entidade de Pedido, algumas marcacoes sao para esconder o ID no body dos requests, ou fazer a conversao automatica do Enum no swagger.
    public class Pedido
    {
        [JsonIgnore]
        public int Id { get; set; }

        // Lista de produtos associados ao pedido
        public List<PedidoProduto> PedidoProdutos { get; set; } = new List<PedidoProduto>(); /* auto inicializacao, desde o .net 8.0 ( ACHO ) da pra startar com [] também */

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public StatusPedido StatusPedido { get; set; }
        public DateTime DataDoPedido { get; set; }
        public DateTime? DataConclusaoPedido { get; set; } = null;
    }
}