namespace Agendamentos.Api.DTOs;

public record CriarClienteRequest(string Nome, string Email);

public record ClienteResponse(Guid Id, string Nome, string Email);