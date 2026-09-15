using Agendamentos.Application.Interfaces;
using Agendamentos.Domain.Entities;
using Agendamentos.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Agendamentos.Infrastructure.Repositories;

public class AgendamentoRepositorio : IAgendamentoRepository
{
    private readonly AgendamentosDbContext _context;

    public AgendamentoRepositorio(AgendamentosDbContext context)
    {
        _context = context;
    }

    public async Task AdicionarAsync(Agendamento agendamento)
    {
        _context.Agendamentos.Add(agendamento);
        await _context.SaveChangesAsync();
    }

    public async Task<Agendamento?> ObterPorIdAsync(Guid id)
    {
        return await _context.Agendamentos
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task AtualizarAsync(Agendamento agendamento)
    {
        _context.Agendamentos.Update(agendamento);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExisteConflitoAsync(Guid profissionalId, DateTime dataHora)
    {
        return await _context.Agendamentos
            .AnyAsync(a =>
                a.ProfissionalId == profissionalId &&
                a.DataHora == dataHora &&
                a.Status == StatusAgendamento.Confirmado);
    }
}