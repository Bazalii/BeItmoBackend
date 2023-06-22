using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using BeItmoBackend.Core.Exceptions;
using BeItmoBackend.Core.NeuralNetworks.Repositories;
using BeItmoBackend.Data.NeuralNetworks.Models;
using Microsoft.Extensions.Configuration;

namespace BeItmoBackend.Data.NeuralNetworks.HttpClients;

public class NeuralNetworkConnectionHttpProvider : INeuralNetworkConnectionProvider
{
    private readonly string _host;
    private readonly HttpClient _httpClient;

    public NeuralNetworkConnectionHttpProvider(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _host = configuration["NeuralNetworkServiceHost"]!;
    }

    public async Task<int> GetEmotionsStatus(string message, CancellationToken cancellationToken)
    {
        var request = new HttpRequestMessage(HttpMethod.Post,
                                             $"{_host}understandingEmotions/predictEmotion/");
        request.Content =
            new StringContent(JsonSerializer.Serialize(new { message }), Encoding.UTF8, "application/json");

        var response = await _httpClient.SendAsync(request, cancellationToken);

        var emotionsStatus =
            await response.Content.ReadFromJsonAsync<EmotionsStatus>(cancellationToken: cancellationToken) ??
            throw new ServiceUnavailableException("Neural network service is unavailable!");

        return Convert.ToInt16(emotionsStatus.Score);
    }
}