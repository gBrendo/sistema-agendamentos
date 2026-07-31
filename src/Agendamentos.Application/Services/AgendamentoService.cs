using Agendamentos.Application.Interfaces;
using Agendamentos.Domain.Entities;

namespace Agendamentos.Application.Services;

public class AgendamentoService
{
    private readonly IAgendamentoRepository _agendamentoRepository;

    public AgendamentoService(IAgendamentoRepository agendamentoRepository)
    {
        _agendamentoRepository = agendamentoRepository;
    }

    public async Task CriarAgendamentoAsync(Guid clienteId, Guid profissionalId, DateTime dataHora)
    {
        var existeConflito = await _agendamentoRepository.ExisteConflitoAsync(profissionalId, dataHora);

        if (existeConflito)
            throw new InvalidOperationException("Este profissional já possui um agendamento nesse horário.");

        var agendamento = new Agendamento(clienteId, profissionalId, dataHora);

        await _agendamentoRepository.AdicionarAsync(agendamento);
    }
    public async Task CancelarAgendamentoAsync(Guid agendamentoId)
    {
        var agendamento = await _agendamentoRepository.ObterPorIdAsync(agendamentoId);

        if (agendamento is null)
            throw new InvalidOperationException("Agendamento não encontrado.");

        agendamento.Cancelar();

        await _agendamentoRepository.AtualizarAsync(agendamento);
    }
}