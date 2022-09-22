namespace MedianMaintenance;

public class MedianSelectorInsertionSort<T> : MedianSelectorBase<T> where T : IComparable<T>
{
    private readonly LinkedList<T> _storage = new();

    public MedianSelectorInsertionSort(Func<T, T, T> averageCalculator) : base(averageCalculator) { }

    protected override T FindMedian(T item)
    {
        Store(item);//O(N)
        return GetMiddle();//O(N)
    }

    /// <summary>
    /// stores item in a storage and keeps it sorted
    /// </summary>
    private void Store(T item)
    {
        if (_storage.Count == 0 || _storage.First!.Value.CompareTo(item) >= 0) 
        {
            _storage.AddFirst(item);
            return;
        }
        
        var current = _storage.First;
        while (current.Next is not null)
        {
            if (current.ValueRef.CompareTo(item) >= 0)
            {
                _storage.AddBefore(current, item);
                return;
            }

            current = current.Next;
        }

        if (_storage.Last!.ValueRef.CompareTo(item) >= 0)
            _storage.AddBefore(_storage.Last, item);
        else 
            _storage.AddLast(item);
    }

    private T GetMiddle()
    {
        var oddMiddle = _storage.First;
        for (int i = 0; i < (_storage.Count-1)/2; i++)
        {
            oddMiddle = oddMiddle!.Next;
        }

        if (_storage.Count % 2 == 0)
            return _averageCalculator(oddMiddle!.Value, oddMiddle.Next!.Value);
        
        return oddMiddle!.Value;
    }
}