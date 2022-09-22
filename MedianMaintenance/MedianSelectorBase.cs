namespace MedianMaintenance;

public abstract class MedianSelectorBase<T> : IMedianSelector<T> where T : IComparable<T>
{
    protected readonly Func<T, T, T> _averageCalculator;

    protected MedianSelectorBase(Func<T, T, T> averageCalculator)
        => _averageCalculator = averageCalculator;

    public IEnumerable<T> ExtractMedian(IEnumerable<T> source)
    {
        foreach (var item in source)
        {
            yield return FindMedian(item);
        }
    }

    protected abstract T FindMedian(T item);
}