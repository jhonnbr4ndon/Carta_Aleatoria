namespace Carta_Aleatoria_CP6.Entity;

public class Carta
{
    public Guid Id { get; init; }
    public String Nome { get; set; }
    
    public Carta(String nome)
    {
        Nome = nome;
        Id = Guid.NewGuid();
    }

    public void AtualizarNome(string nome)
    {
        Nome = nome;
    }
}