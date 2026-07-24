using Agendamentos.Domain.Entities;

namespace Agendamentos.Application.Interfaces;

public interface IClienteRepository
{
    Task AdicionarAsync(Cliente cliente);
    Task<Cliente?> ObterPorEmailAsync(string email);
    Task RemoverAsync(Cliente cliente);
}