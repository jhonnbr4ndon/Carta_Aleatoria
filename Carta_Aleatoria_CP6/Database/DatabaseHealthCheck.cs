using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Carta_Aleatoria_CP6.Database;

public class DatabaseHealthCheck : IHealthCheck
{
    private readonly CartaDbContext _dbContext;

    public DatabaseHealthCheck(CartaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        try
        {
            // Verificar se é possível executar uma consulta simples ao banco de dados
            var cartas = _dbContext.Cartas.FirstOrDefault();
            if (cartas != null)
            {
                return Task.FromResult(HealthCheckResult.Healthy("A aplicação está funcionando corretamente."));
            }
            else
            {
                return Task.FromResult(HealthCheckResult.Unhealthy("Não foi possível recuperar dados do banco de dados."));
            }
        }
        catch (Exception ex)
        {
            return Task.FromResult(HealthCheckResult.Unhealthy($"Erro ao conectar-se ao banco de dados: {ex.Message}"));
        }
    }
}