using Agendamentos.Domain.Entities;

namespace Agendamentos.Application.Interfaces;

public interface IAgendamentoRepository
{
    Task AdicionarAsync(Agendamento agendamento);
    Task<bool> ExisteConflitoAsync(Guid profissionalId, DateTime dataHora);
}