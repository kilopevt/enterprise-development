namespace BeautySalon.Domain.Entities;

/// <summary>
/// Запись клиента на услугу к мастеру.
/// </summary>
public class Appointment
{
    /// <summary>
    /// Уникальный идентификатор.
    /// </summary>
    public required string Id { get; set; }

    /// <summary>
    /// Дата и время начала записи.
    /// </summary>
    public DateTime StartTime { get; set; }

    /// <summary>
    /// Является ли клиент постоянным.
    /// </summary>
    public bool IsRegularClient { get; set; }

    /// <summary>
    /// Идентификатор клиента.
    /// </summary>
    public required string ClientId { get; set; }

    /// <summary>
    /// Клиент, записанный на услугу.
    /// </summary>
    public required Client Client { get; set; }

    /// <summary>
    /// Идентификатор мастера.
    /// </summary>
    public required string MasterId { get; set; }

    /// <summary>
    /// Мастер, оказывающий услугу.
    /// </summary>
    public required Master Master { get; set; }

    /// <summary>
    /// Идентификатор услуги.
    /// </summary>
    public required string ServiceId { get; set; }

    /// <summary>
    /// Оказываемая услуга.
    /// </summary>
    public required Service Service { get; set; }
}