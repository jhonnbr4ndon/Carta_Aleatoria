namespace Carta_Aleatoria_CP6.Entity;

public class Carta
{
    public Guid Id { get; init; }
    public String Nome { get; set; }
    public string Categoria { get; set; }
    public decimal Preco { get; set; }
    public int Quantidade { get; set; }
    
    public Carta(string nome, string categoria, decimal preco, int quantidade)
    {
        Id = Guid.NewGuid();
        Nome = nome;
        Categoria = categoria;
        Preco = preco;
        Quantidade = quantidade;
    }

    public void AtualizarCarta(string nome, string categoria, decimal preco, int quantidade)
    {
        Nome = nome;
        Categoria = categoria;
        Preco = preco;
        Quantidade = quantidade;
    }
}