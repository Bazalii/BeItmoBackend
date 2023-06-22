namespace BeItmoBackend.Data.Happiness.Models;

public class HappinessCheckpointDbModel
{
    public Guid Id { get; set; }
    public int UserId { get; set; }
    public DateOnly Date { get; set; }
    public string Note { get; set; } = string.Empty;
    public int Score { get; set; }
}