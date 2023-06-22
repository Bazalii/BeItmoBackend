using BeItmoBackend.Core.UserAnalytics.UserStatistics.Models;
using MathNet.Numerics.Distributions;

namespace BeItmoBackend.Core.UserAnalytics;

public class BanditsAlgorithm
{
    private readonly List<UserStatistic> _statistics = new();
    private readonly Random _random;

    public BanditsAlgorithm(List<UserStatistic> statistics)
    {
        _random = new Random();

        foreach (var statistic in statistics)
        {
            _statistics.Add(statistic);
        }
    }

    public Guid GetRecommendationId()
    {
        var relevantIndex = GetRelevantIndex(false);

        return _statistics[relevantIndex]
            .TypeValueId;
    }

    public Guid GetSomethingNewId()
    {
        var relevantIndex = GetRelevantIndex(true);

        return _statistics[relevantIndex]
            .TypeValueId;
    }

    private int GetRelevantIndex(bool minOptimizationType)
    {
        var samples = GetSamples();
        var optimizationSample = minOptimizationType ? samples.Min() : samples.Max();
        var relevantIndexes = new List<int>();

        for (int i = 0; i < samples.Length; i++)
        {
            if (samples[i] == optimizationSample) relevantIndexes.Add(i);
        }

        return relevantIndexes[_random.Next(0, relevantIndexes.Count)];
    }

    private double[] GetSamples()
    {
        var samples = new double[_statistics.Count];

        for (var i = 0; i < _statistics.Count; i++)
        {
            var currentStatistic = _statistics[i];

            var dist = new Beta(currentStatistic.PrizeCounter,
                                currentStatistic.TapCounter - currentStatistic.PrizeCounter + 2);

            samples[i] = dist.Sample();
        }

        return samples;
    }
}