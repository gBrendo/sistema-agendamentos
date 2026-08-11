namespace Agendamentos.Domain.Entities;

public class Profissional
{
    public Guid Id { get; private set; }
    public string Nome { get; private set; }
    public string Especialidade { get; private set; }

    private Profissional() { } // exigido pelo EF Core

    public Profissional(string nome, string especialidade)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("Nome é obrigatório.");

        if (string.IsNullOrWhiteSpace(especialidade))
            throw new ArgumentException("Especialidade é obrigatória.");

        Id = Guid.NewGuid();
        Nome = nome;
        Especialidade = especialidade;
    }
    public void AtualizarDados(string nome, string especialidade)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("Nome é obrigatório.");

        if (string.IsNullOrWhiteSpace(especialidade))
            throw new ArgumentException("Especialidade é obrigatória.");

        Nome = nome;
        Especialidade = especialidade;
    }
}