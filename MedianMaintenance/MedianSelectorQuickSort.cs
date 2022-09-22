namespace MedianMaintenance;

public class MedianSelectorQuickSort<T> : MedianSelectorBase<T> where T : IComparable<T>
{
    private readonly List<T> _storage = new();

    public MedianSelectorQuickSort(Func<T, T, T> averageCalculator) : base(averageCalculator) { }

    protected override T FindMedian(T item)
    {
        _storage.Add(item);//O(1)
        _storage.Sort();//O(N*lnN)
        return GetMiddle();//Q(1)
    }

    private T GetMiddle()
    {
        var oddMiddle = _storage[(_storage.Count - 1) / 2];

        if (_storage.Count % 2 == 0)
            return _averageCalculator(oddMiddle, _storage[_storage.Count / 2]);

        return oddMiddle;
    }
}