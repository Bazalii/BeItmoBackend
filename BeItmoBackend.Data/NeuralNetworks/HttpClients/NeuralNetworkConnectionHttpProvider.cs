using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using BeItmoBackend.Core.Exceptions;
using BeItmoBackend.Core.NeuralNetworks.Repositories;
using BeItmoBackend.Data.NeuralNetworks.Models;

namespace BeItmoBackend.Data.NeuralNetworks.HttpClients;

public class NeuralNetworkConnectionHttpProvider : INeuralNetworkConnectionProvider
{
    //TODO get BasePath from configuration
    private const string BasePath = "http://localhost:8000/";
    private readonly HttpClient _httpClient;

    public NeuralNetworkConnectionHttpProvider(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<int> GetEmotionsStatus(string message, CancellationToken cancellationToken)
    {
        var request = new HttpRequestMessage(HttpMethod.Post,
                                             $"{BasePath}understandingEmotions/predictEmotion/");
        request.Content =
            new StringContent(JsonSerializer.Serialize(new { message }), Encoding.UTF8, "application/json");

        var response = await _httpClient.SendAsync(request, cancellationToken);

        var emotionsStatus =
            await response.Content.ReadFromJsonAsync<EmotionsStatus>(cancellationToken: cancellationToken) ??
            throw new ServiceUnavailableException("Neural network service is unavailable!");

        return Convert.ToInt16(emotionsStatus.Score);
    }
}