using Agendamentos.Application.Interfaces;
using Agendamentos.Domain.Entities;

namespace Agendamentos.Application.Services;

public class ClienteService
{
    private readonly IClienteRepository _clienteRepository;

    public ClienteService(IClienteRepository clienteRepository)
    {
        _clienteRepository = clienteRepository;
    }

    public async Task CriarClienteAsync(string nome, string email)
    {
        var cliente = new Cliente(nome, email);
        await _clienteRepository.AdicionarAsync(cliente);
    }
}