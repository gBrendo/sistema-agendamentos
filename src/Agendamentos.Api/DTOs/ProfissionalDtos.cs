namespace Agendamentos.Api.DTOs;

public record CriarProfissionalRequest(string Nome, string Especialidade);

public record ProfissionalResponse(Guid Id, string Nome, string Especialidade);