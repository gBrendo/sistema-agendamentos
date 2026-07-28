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
    public async Task<Cliente?> ObterClientePorEmailAsync(string email)
    {
        return await _clienteRepository.ObterPorEmailAsync(email);
    }
    public async Task DeletarClienteAsync(string email)
    {
        var cliente = await _clienteRepository.ObterPorEmailAsync(email);

        if (cliente is null)
            throw new InvalidOperationException("Cliente não encontrado.");

        await _clienteRepository.RemoverAsync(cliente);
    }
    public async Task AtualizarClienteAsync(string emailAtual, string novoNome, string novoEmail)
    {
        var cliente = await _clienteRepository.ObterPorEmailAsync(emailAtual);

        if (cliente is null)
            throw new InvalidOperationException("Cliente não encontrado.");

        cliente.AtualizarDados(novoNome, novoEmail);

        await _clienteRepository.AtualizarAsync(cliente);    
    }
}