using BenchmarkDotNet.Attributes;
using MedianMaintenance;

public class Benchmarks
{
    private readonly Random _generator = new Random(DateTime.UtcNow.Millisecond);

    [Benchmark]
    [ArgumentsSource(nameof(Data))]
    public ICollection<int> GetMediansQuickSort(IEnumerable<int> source) =>
        new MedianSelectorQuickSort<int>(averageCalculator: (x, y) => (x + y) / 2)
            .ExtractMedian(source)
            .ToArray();

    [Benchmark]
    [ArgumentsSource(nameof(Data))]
    public ICollection<int> GetMediansInsertionSort(IEnumerable<int> source) =>
        new MedianSelectorInsertionSort<int>(averageCalculator: (x, y) => (x + y) / 2)
            .ExtractMedian(source)
            .ToArray();
    
    [Benchmark]
    [ArgumentsSource(nameof(Data))]
    public ICollection<int> GetMediansPriorityQueue(IEnumerable<int> source) =>
        new MedianSelectorPriorityQueue<int>(averageCalculator: (x, y) => (x + y) / 2)
            .ExtractMedian(source)
            .ToArray();

    public int[][] Data() => new int[][]
    {
        Enumerable.Range(1, 10_000).Select(_ => _generator.Next(1, 1_000_000)).ToArray(),
        Enumerable.Range(1, 10_000).Select(_ => _generator.Next(1, 1_000)).ToArray(),
        Enumerable.Range(1, 10_000).ToArray(),
        Enumerable.Range(1, 10_000).Reverse().ToArray(),
    };
}