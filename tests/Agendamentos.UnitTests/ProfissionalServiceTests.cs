using Agendamentos.Application.Interfaces;
using Agendamentos.Application.Services;
using Agendamentos.Domain.Entities;
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
}