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
    [Fact]
    public async Task CriarCliente_ComEmailInvalido_DeveLancarArgumentException()
    {
        // Arrange
        var repositorioMock = new Mock<IClienteRepository>();
        var service = new ClienteService(repositorioMock.Object);

        // Act + Assert
        await Assert.ThrowsAsync<ArgumentException>(() =>
            service.CriarClienteAsync("Teste Nome", "testeemailhotmail.com"));
    }
    [Fact]
    public async Task ObterClientePorEmail_QuandoExiste_DeveRetornarCliente()
    {
        // Arrange
        var clienteExistente = new Cliente("Maria Souza", "maria@email.com");

        var repositorioMock = new Mock<IClienteRepository>();
        repositorioMock
            .Setup(r => r.ObterPorEmailAsync("maria@email.com"))
            .ReturnsAsync(clienteExistente);

        var service = new ClienteService(repositorioMock.Object);

        // Act
        var resultado = await service.ObterClientePorEmailAsync("maria@email.com");

        // Assert
        resultado.Should().NotBeNull();
        resultado!.Email.Should().Be("maria@email.com");
    }
    [Fact]
    public async Task DeletarCliente_QuandoExiste_DeveChamarRepositorioParaRemover()
    {
        // Arrange
        var clienteExistente = new Cliente("Carlos Lima", "carlos@email.com");

        var repositorioMock = new Mock<IClienteRepository>();
        repositorioMock
            .Setup(r => r.ObterPorEmailAsync("carlos@email.com"))
            .ReturnsAsync(clienteExistente);

        var service = new ClienteService(repositorioMock.Object);

        // Act
        await service.DeletarClienteAsync("carlos@email.com");

        // Assert
        repositorioMock.Verify(r => r.RemoverAsync(clienteExistente), Times.Once);
    }
}