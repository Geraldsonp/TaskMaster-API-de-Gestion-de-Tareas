using Issues.Manager.Domain.Entities;

namespace Issues.Manager.Domain.ValueObjects;

public class OperationResult
{
    public bool IsSuccess { get; set; }
    public string? ErrorMessage { get; set; }
    public BaseEntity? ObjectFromOperation { get; set; } = null;
}