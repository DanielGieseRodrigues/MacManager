using MacManager.Domain.ValueObjects;
using System.Text.Json.Serialization;

namespace MacManager.Domain.Entities
{
    public class Pedido
    {
        [JsonIgnore]
        public int Id { get; set; }

        // Lista de produtos associados ao pedido
        public List<PedidoProduto> PedidoProdutos { get; set; } = new List<PedidoProduto>();

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public StatusPedido StatusPedido { get; set; }
        public DateTime DataDoPedido { get; set; }
        public DateTime? DataConclusaoPedido { get; set; } = null;
    }
}