using Carta_Aleatoria_CP6.Database;
using Carta_Aleatoria_CP6.Entity;
using Microsoft.EntityFrameworkCore;

namespace Carta_Aleatoria_CP6.Controller;

public static class CartaController
{
    public static void AddEndpointsCarta(this WebApplication app)
    {
        var rotasCarta = app.MapGroup("carta");
        
        
        rotasCarta.MapPost("", async (CartaRequest request, CartaDbContext context) =>
        {
            var novaCarta = new Carta(request.Nome);

            await context.Cartas.AddAsync(novaCarta);
            await context.SaveChangesAsync();
            return Results.Ok(new CartaDto(novaCarta.Id, novaCarta.Nome));
        });
        
        rotasCarta.MapGet("", async (CartaDbContext context) =>
        {
            var cartas = await context.Cartas
                .Select(carta => new CartaDto(carta.Id, carta.Nome))
                .ToListAsync();
            return cartas;
        });

        rotasCarta.MapPut("{id}", async (Guid id,CartaRequest request, CartaDbContext context) =>
        {
            var carta = await context.Cartas.SingleOrDefaultAsync(carta => carta.Id == id);
            
            if(carta == null) 
                return Results.NotFound();

            carta.AtualizarNome(request.Nome);

            await context.SaveChangesAsync();
            return Results.Ok(new CartaDto(carta.Id, carta.Nome));
        });
     
        rotasCarta.MapDelete("{id}", async (Guid id, CartaDbContext context) =>
        {
            var carta = await context.Cartas.SingleOrDefaultAsync(carta => carta.Id == id);

            if (carta == null)
                return Results.NotFound();

            context.Cartas.Remove(carta);
            await context.SaveChangesAsync();
            return Results.Ok();
        });
        
        rotasCarta.MapGet("randomCard", async (CartaDbContext context) =>
        {
            var totalCartas = await context.Cartas.CountAsync();
    
            if (totalCartas == 0)
                return Results.NotFound(); // Retorna NotFound se não houver cartas no banco de dados

            var random = new Random();
            var randomIndex = random.Next(totalCartas); // Gera um índice aleatório dentro do intervalo total de cartas

            var carta = await context.Cartas
                .OrderBy(carta => carta.Id) // Ordena as cartas por ID (ou outro campo único)
                .Skip(randomIndex) // Pula para o índice aleatório
                .Take(1) // Seleciona apenas uma carta
                .Select(carta => new CartaDto(carta.Id, carta.Nome))
                .FirstOrDefaultAsync();

            if (carta == null)
                return Results.NotFound(); // Retorna NotFound se não encontrar a carta

            return Results.Ok(carta);
        });
    }
    
}