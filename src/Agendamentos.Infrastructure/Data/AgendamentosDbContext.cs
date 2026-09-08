using Agendamentos.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Agendamentos.Infrastructure.Data;

public class AgendamentosDbContext : DbContext
{
    public AgendamentosDbContext(DbContextOptions<AgendamentosDbContext> options)
        : base(options)
    {
    }

    public DbSet<Cliente> Clientes => Set<Cliente>();
    public DbSet<Profissional> Profissionais => Set<Profissional>();
    public DbSet<Agendamento> Agendamentos => Set<Agendamento>();
}