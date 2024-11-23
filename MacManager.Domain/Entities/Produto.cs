using MacManager.Domain.ValueObjects;
using System.Text.Json.Serialization;

namespace MacManager.Domain.Entities
{
    //Representacao poco ( classe anemica) da entidade de Produto, algumas marcacoes sao para esconder o ID no body dos requests, ou fazer a conversao automatica do Enum no swagger.
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