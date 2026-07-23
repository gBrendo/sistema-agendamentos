namespace Agendamentos.Domain.Entities;

public enum StatusAgendamento
{
    Confirmado,
    Cancelado
}

public class Agendamento
{
    public Guid Id { get; private set; }
    public Guid ClienteId { get; private set; }
    public Guid ProfissionalId { get; private set; }
    public DateTime DataHora { get; private set; }
    public StatusAgendamento Status { get; private set; }

    private const int HorasMinimasParaCancelamento = 2;

    private Agendamento() { } // exigido pelo EF Core

    public Agendamento(Guid clienteId, Guid profissionalId, DateTime dataHora)
    {
        if (dataHora <= DateTime.Now)
            throw new ArgumentException("Não é possível agendar em uma data no passado.");

        Id = Guid.NewGuid();
        ClienteId = clienteId;
        ProfissionalId = profissionalId;
        DataHora = dataHora;
        Status = StatusAgendamento.Confirmado;
    }

    public void Cancelar()
    {
        if (Status == StatusAgendamento.Cancelado)
            throw new InvalidOperationException("Agendamento já está cancelado.");

        var horasRestantes = (DataHora - DateTime.Now).TotalHours;

        if (horasRestantes < HorasMinimasParaCancelamento)
            throw new InvalidOperationException(
                $"Cancelamento só é permitido com {HorasMinimasParaCancelamento}h de antecedência.");

        Status = StatusAgendamento.Cancelado;
    }
}