using Agendamentos.Application.Interfaces;
using Agendamentos.Domain.Entities;
using FluentAssertions;
using Moq;
using Xunit;

using Agendamentos.Application.Services;


namespace Agendamentos.UnitTests;

public class ClienteServiceTests
{
    [Fact]
    public async Task CriarCliente_ComDadosValidos_DeveChamarRepositorioParaAdicionar()
    {
        // Arrange
        var repositorioMock = new Mock<IClienteRepository>();
        var service = new ClienteService(repositorioMock.Object);

        // Act
        await service.CriarClienteAsync("João Silva", "joao@email.com");

        // Assert
        repositorioMock.Verify(r => r.AdicionarAsync(It.IsAny<Cliente>()), Times.Once);
    }
    [Fact]
    public async Task CriarCliente_ComNomeVazio_DeveLancarArgumentException()
    {
        // Arrange
        var repositorioMock = new Mock<IClienteRepository>();
        var service = new ClienteService(repositorioMock.Object);

        // Act + Assert
        await Assert.ThrowsAsync<ArgumentException>(() =>
            service.CriarClienteAsync("", "email@teste.com"));
    }
}