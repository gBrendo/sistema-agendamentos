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
    public async Task<Profissional?> ObterProfissionalPorIdAsync(Guid id)
    {
        return await _profissionalRepository.ObterPorIdAsync(id);
    }
    public async Task AtualizarProfissionalAsync(Guid id, string novoNome, string novaEspecialidade)
    {
        var profissional = await _profissionalRepository.ObterPorIdAsync(id);

        if (profissional is null)
            throw new InvalidOperationException("Profissional não encontrado.");

        profissional.AtualizarDados(novoNome, novaEspecialidade);

        await _profissionalRepository.AtualizarAsync(profissional);
    }

    public async Task DeletarProfissionalAsync(Guid id)
    {
        var profissional = await _profissionalRepository.ObterPorIdAsync(id);

        if (profissional is null)
            throw new InvalidOperationException("Profissional não encontrado.");

        await _profissionalRepository.RemoverAsync(profissional);
    }
}