using MacManager.Domain.ValueObjects;
using System.Text.Json.Serialization;

namespace MacManager.Domain.Entities
{
    public class Produto
    {
        [JsonIgnore]
        public int Id { get; set; }

        public required string Nome { get; set; }
        public decimal Valor { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public AreaCozinha AreaCozinha { get; set; }
    }
}