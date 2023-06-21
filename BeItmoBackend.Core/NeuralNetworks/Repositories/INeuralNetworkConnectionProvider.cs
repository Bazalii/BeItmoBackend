namespace BeItmoBackend.Core.NeuralNetworks.Repositories;

public interface INeuralNetworkConnectionProvider
{
    Task<int> GetEmotionsStatus(string message, CancellationToken cancellationToken);
}