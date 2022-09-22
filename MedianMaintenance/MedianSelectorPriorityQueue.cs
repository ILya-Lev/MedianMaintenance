namespace MedianMaintenance;

public class MedianSelectorPriorityQueue<T> : MedianSelectorBase<T> where T : IComparable<T>
{
    private readonly PriorityQueue<T, T> _leftDesc = new(new DescendingComparer<T>());
    private readonly PriorityQueue<T, T> _rightAsc = new();

    public MedianSelectorPriorityQueue(Func<T, T, T> averageCalculator) : base(averageCalculator) { }

    protected override T FindMedian(T item)
    {
        Store(item);//O(lnN)
        Rebalance();//O(lnN)
        return GetMiddle();//O(1)
    }

    private void Store(T item)
    {
        if (_leftDesc.Count == 0 || _leftDesc.Peek().CompareTo(item) >= 0)
            _leftDesc.Enqueue(item ,item);
        else
            _rightAsc.Enqueue(item ,item);
    }

    private void Rebalance()
    {
        while (_leftDesc.Count - _rightAsc.Count > 1)
        {
            var item = _leftDesc.Dequeue();
            _rightAsc.Enqueue(item, item);
        }
        
        while (_rightAsc.Count - _leftDesc.Count > 1)
        {
            var item = _rightAsc.Dequeue();
            _leftDesc.Enqueue(item, item);
        }
    }

    private T GetMiddle()
    {
        if (_leftDesc.Count > _rightAsc.Count)
            return _leftDesc.Peek();
        if (_leftDesc.Count < _rightAsc.Count)
            return _rightAsc.Peek();

        return _averageCalculator(_leftDesc.Peek(), _rightAsc.Peek());
    }

    private class DescendingComparer<TVal> : IComparer<TVal> where TVal : IComparable<TVal>
    {
        public int Compare(TVal? x, TVal? y) => new { x, y } switch
        {
            { x: null, y: null } => 0,
            { x: null, y: _ } => -1,
            { x: _, y: null } => 1,
            { x: var lhs, y: var rhs } => rhs.CompareTo(lhs),
        };
    }
}