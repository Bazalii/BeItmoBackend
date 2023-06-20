namespace BeItmoBackend.Web.Happiness.Models;

public class HappinessCheckpointCreationRequest
{
    public int UserId { get; set; }
    public string Note { get; set; } = string.Empty;
    public int Score { get; set; }
}