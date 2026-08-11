using Agendamentos.Application.Interfaces;
using Agendamentos.Domain.Entities;

namespace Agendamentos.Application.Services;

public class ProfissionalService
{
    private readonly IProfissionalRepository _profissionalRepository;

    public ProfissionalService(IProfissionalRepository profissionalRepository)
    {
        _profissionalRepository = profissionalRepository;
    }

    public async Task CriarProfissionalAsync(string nome, string especialidade)
    {
        var profissional = new Profissional(nome, especialidade);
        await _profissionalRepository.AdicionarAsync(profissional);
    }
}