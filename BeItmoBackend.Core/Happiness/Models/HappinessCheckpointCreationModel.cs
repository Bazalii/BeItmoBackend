namespace BeItmoBackend.Core.Happiness.Models;

public class HappinessCheckpointCreationModel
{
    public int UserId { get; set; }
    public string Note { get; set; } = string.Empty;
    public int Score { get; set; }
}