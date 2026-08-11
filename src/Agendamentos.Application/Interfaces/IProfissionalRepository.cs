using Agendamentos.Domain.Entities;

namespace Agendamentos.Application.Interfaces;

public interface IProfissionalRepository
{
    Task AdicionarAsync(Profissional profissional);
    Task<Profissional?> ObterPorIdAsync(Guid id);
}