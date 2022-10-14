namespace Core.DataAccess.Paging
{
    // internal
    public interface IPaginate<T>
    {
        // default public
        int From { get; }
        int Index { get; }
        int Size { get; }
        int Count { get; }
        int Pages { get; }
        IList<T> Items { get; }
        bool HasPrevious { get; }
        bool HasNext { get; }
    }
}
