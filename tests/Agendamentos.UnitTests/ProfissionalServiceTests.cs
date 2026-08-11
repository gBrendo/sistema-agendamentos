using Agendamentos.Application.Interfaces;
using Agendamentos.Application.Services;
using Agendamentos.Domain.Entities;
using FluentAssertions;
using Moq;
using Xunit;

namespace Agendamentos.UnitTests;

public class ProfissionalServiceTests
{
    [Fact]
    public async Task CriarProfissional_ComDadosValidos_DeveChamarRepositorioParaAdicionar()
    {
        // Arrange
        var repositorioMock = new Mock<IProfissionalRepository>();
        var service = new ProfissionalService(repositorioMock.Object);

        // Act
        await service.CriarProfissionalAsync("Dra. Marina", "Odontologia");

        // Assert
        repositorioMock.Verify(r => r.AdicionarAsync(It.IsAny<Profissional>()), Times.Once);
    }
    [Fact]
    public async Task ObterProfissionalPorId_QuandoExiste_DeveRetornarProfissional()
    {
        var profissionalExistente = new Profissional("Dr. Carlos", "Cardiologia");

        var repositorioMock = new Mock<IProfissionalRepository>();
        repositorioMock
            .Setup(r => r.ObterPorIdAsync(profissionalExistente.Id))
            .ReturnsAsync(profissionalExistente);

        var service = new ProfissionalService(repositorioMock.Object);

        var resultado = await service.ObterProfissionalPorIdAsync(profissionalExistente.Id);

        resultado.Should().NotBeNull();
        resultado!.Nome.Should().Be("Dr. Carlos");
    }

    [Fact]
    public async Task AtualizarProfissional_QuandoExiste_DeveChamarRepositorioParaAtualizar()
    {
        var profissionalExistente = new Profissional("Dr. Carlos", "Cardiologia");

        var repositorioMock = new Mock<IProfissionalRepository>();
        repositorioMock
            .Setup(r => r.ObterPorIdAsync(profissionalExistente.Id))
            .ReturnsAsync(profissionalExistente);

        var service = new ProfissionalService(repositorioMock.Object);

        await service.AtualizarProfissionalAsync(profissionalExistente.Id, "Dr. Carlos Souza", "Cardiologia Pediatrica");

        repositorioMock.Verify(r => r.AtualizarAsync(profissionalExistente), Times.Once);
    }

    [Fact]
    public async Task DeletarProfissional_QuandoExiste_DeveChamarRepositorioParaRemover()
    {
        var profissionalExistente = new Profissional("Dr. Carlos", "Cardiologia");

        var repositorioMock = new Mock<IProfissionalRepository>();
        repositorioMock
            .Setup(r => r.ObterPorIdAsync(profissionalExistente.Id))
            .ReturnsAsync(profissionalExistente);

        var service = new ProfissionalService(repositorioMock.Object);

        await service.DeletarProfissionalAsync(profissionalExistente.Id);

        repositorioMock.Verify(r => r.RemoverAsync(profissionalExistente), Times.Once);
    }
}