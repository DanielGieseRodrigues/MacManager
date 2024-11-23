using MacManager.Domain.ValueObjects;
using System.Text.Json.Serialization;

namespace MacManager.Application.UseCases.Pedidos.FecharPedidoUseCase
{
    public class FecharPedidoRequest
    {
        [JsonIgnore]
        public int PedidoId { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))] /* marcacao para swagger aceitar tambem a versao escrita do valor.*/
        public StatusPedido AtualizacaoStatus { get; set; }
    }
}
