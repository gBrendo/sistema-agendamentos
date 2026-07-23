namespace Agendamentos.Domain.Entities;

public class Cliente
{
    public Guid Id { get; private set; }
    public string Nome { get; private set; }
    public string Email { get; private set; }

    private Cliente() { } // exigido pelo EF Core depois

    public Cliente(string nome, string email)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("Nome é obrigatório.");

        if (string.IsNullOrWhiteSpace(email) || !email.Contains('@'))
            throw new ArgumentException("Email inválido.");

        Id = Guid.NewGuid();
        Nome = nome;
        Email = email;
    }
}