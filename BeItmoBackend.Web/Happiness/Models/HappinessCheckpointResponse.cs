namespace BeItmoBackend.Web.Happiness.Models;

public class HappinessCheckpointResponse
{
    public Guid Id { get; set; }
    public int UserId { get; set; }
    public DateOnly Date { get; set; }
    public string Note { get; set; } = string.Empty;
    public int Score { get; set; }
}