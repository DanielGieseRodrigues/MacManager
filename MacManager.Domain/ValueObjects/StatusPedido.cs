namespace MacManager.Domain.ValueObjects
{
    //Representacao do andamento do pedido, no inicio ia usar apenas a Data, mas pensei no conceito da recusa e achei legal aplicar.
    public enum StatusPedido
    {
        Aberto,
        Recusado,
        Fechado
    }
}
