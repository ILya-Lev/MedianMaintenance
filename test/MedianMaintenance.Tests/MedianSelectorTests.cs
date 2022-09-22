using FluentAssertions;
using Xunit.Abstractions;

namespace MedianMaintenance.Tests;

[Trait("Category", "Unit")]
public class MedianSelectorTests
{
    private readonly ITestOutputHelper _output;
    private readonly MedianSelectorQuickSort<double> _quickSortSelector;
    private readonly MedianSelectorInsertionSort<double> _insertionSortSelector;
    private readonly MedianSelectorPriorityQueue<double> _priorityQueueSelector;

    public MedianSelectorTests(ITestOutputHelper output)
    {
        _output = output;

        _quickSortSelector = new MedianSelectorQuickSort<double>(TakeAvg);
        _insertionSortSelector = new MedianSelectorInsertionSort<double>(TakeAvg);
        _priorityQueueSelector = new MedianSelectorPriorityQueue<double>(TakeAvg);
    }

    private double TakeAvg(double lhs, double rhs) => (lhs + rhs) / 2.0;

    private static readonly double[] _numbers = new[] { 5.0, 2, 1, 4, 3 };
    private static readonly double[] _expectedMedians = new[] { 5.0, 3.5, 2, 3, 3 };

    [Fact]
    public void QuickSort_SelectMedian_InitialSample()
    {
        var medians = _quickSortSelector.ExtractMedian(_numbers);
        medians.Should().BeEquivalentTo(_expectedMedians);
    }

    [Fact]
    public void InsertionSort_SelectMedian_InitialSample()
    {
        var medians = _insertionSortSelector.ExtractMedian(_numbers);
        medians.Should().BeEquivalentTo(_expectedMedians);
    }

    [Fact]
    public void PriorityQueue_SelectMedian_InitialSample()
    {
        var medians = _priorityQueueSelector.ExtractMedian(_numbers);
        medians.Should().BeEquivalentTo(_expectedMedians);
    }

    [Fact]
    public void SelectMedian_RandomNumbers_AllMethodsSameResult()
    {
        var generator = new Random(DateTime.UtcNow.Millisecond);
        var source = Enumerable.Range(1, 10).Select(_ => (double)generator.Next(1, 100)).ToArray();

        var quickSort = _quickSortSelector.ExtractMedian(source).ToArray();
        var insertionSort = _insertionSortSelector.ExtractMedian(source).ToArray();
        var priorityQueue = _priorityQueueSelector.ExtractMedian(source).ToArray();

        for (int i = 0; i < source.Length; i++)
        {
            _output.WriteLine($"s: {source[i]}, qs: {quickSort[i]}, is: {insertionSort[i]}, pq: {priorityQueue[i]}");
        }

        quickSort.Should()
            .BeEquivalentTo(insertionSort)
            .And.BeEquivalentTo(priorityQueue);
    }
}