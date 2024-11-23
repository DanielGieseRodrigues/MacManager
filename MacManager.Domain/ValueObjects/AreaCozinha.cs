namespace MacManager.Domain.ValueObjects
{
    //Representacao fisica e imutavel de areas de cozinha dado a regra de negocio, num mundo real seria melhor isso ser uma tabela
    //mesmo para maior escabilidade, ai seria so inserir um registro, mas enfim, fica legal assim tbm para demonstrar a separacao dos valueObjects :)
    public enum AreaCozinha
    {
        Fritos,
        Grelhados,
        Saladas,
        Bebidas,
        Sobremesas
    }
}
