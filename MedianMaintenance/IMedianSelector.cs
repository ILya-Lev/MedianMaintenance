namespace MedianMaintenance;

public interface IMedianSelector<T> where T : IComparable<T>
{
    IEnumerable<T> ExtractMedian(IEnumerable<T> source);
}