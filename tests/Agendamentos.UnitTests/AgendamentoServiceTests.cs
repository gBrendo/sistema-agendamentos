using Agendamentos.Application.Interfaces;
using Agendamentos.Application.Services;
using Agendamentos.Domain.Entities;
using FluentAssertions;
using Moq;
using Xunit;

namespace Agendamentos.UnitTests;

public class AgendamentoServiceTests
{
    [Fact]
    public async Task CriarAgendamento_QuandoProfissionalJaTemHorarioOcupado_DeveLancarInvalidOperationException()
    {
        // Arrange
        var clienteId = Guid.NewGuid();
        var profissionalId = Guid.NewGuid();
        var dataHora = DateTime.Now.AddDays(1);

        var repositorioMock = new Mock<IAgendamentoRepository>();
        repositorioMock
            .Setup(r => r.ExisteConflitoAsync(profissionalId, dataHora))
            .ReturnsAsync(true);

        var service = new AgendamentoService(repositorioMock.Object);

        // Act + Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() =>
            service.CriarAgendamentoAsync(clienteId, profissionalId, dataHora));
    }
    [Fact]
    public async Task CriarAgendamento_QuandoNaoHaConflito_DeveChamarRepositorioParaAdicionar()
    {
        // Arrange
        var clienteId = Guid.NewGuid();
        var profissionalId = Guid.NewGuid();
        var dataHora = DateTime.Now.AddDays(1);

        var repositorioMock = new Mock<IAgendamentoRepository>();
        repositorioMock
            .Setup(r => r.ExisteConflitoAsync(profissionalId, dataHora))
            .ReturnsAsync(false);

        var service = new AgendamentoService(repositorioMock.Object);

        // Act
        await service.CriarAgendamentoAsync(clienteId, profissionalId, dataHora);

        // Assert
        repositorioMock.Verify(r => r.AdicionarAsync(It.IsAny<Agendamento>()), Times.Once);
    }
}