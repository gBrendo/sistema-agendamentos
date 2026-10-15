namespace Agendamentos.Api.DTOs;

public record CriarAgendamentoRequest(Guid ClienteId, Guid ProfissionalId, DateTime DataHora);