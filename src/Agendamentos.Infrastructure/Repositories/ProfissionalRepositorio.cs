using Agendamentos.Application.Interfaces;
using Agendamentos.Domain.Entities;
using Agendamentos.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Agendamentos.Infrastructure.Repositories;

public class ProfissionalRepositorio : IProfissionalRepository
{
    private readonly AgendamentosDbContext _context;

    public ProfissionalRepositorio(AgendamentosDbContext context)
    {
        _context = context;
    }

    public async Task AdicionarAsync(Profissional profissional)
    {
        _context.Profissionais.Add(profissional);
        await _context.SaveChangesAsync();
    }

    public async Task<Profissional?> ObterPorIdAsync(Guid id)
    {
        return await _context.Profissionais
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task AtualizarAsync(Profissional profissional)
    {
        _context.Profissionais.Update(profissional);
        await _context.SaveChangesAsync();
    }

    public async Task RemoverAsync(Profissional profissional)
    {
        _context.Profissionais.Remove(profissional);
        await _context.SaveChangesAsync();
    }
}