namespace BeItmoBackend.Data.NeuralNetworks.Models;

public class EmotionsStatus
{
    public float Score { get; set; }
    public string Emotion { get; set; } = string.Empty;
}