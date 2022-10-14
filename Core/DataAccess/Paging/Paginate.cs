namespace Core.DataAccess.Paging;

// internal
public class Paginate<T> : IPaginate<T>
{
    // private
    public int From { get; }
    public int Index { get; }
    public int Size { get; }
    public int Count { get; }
    public int Pages { get; }
    public IList<T> Items { get; }
    public bool HasPrevious
    {
        get
        {
            return Index - From > 0;
        }
    }
    public bool HasNext => Index - From + 1 < Pages;

    // Readonly property'leri sadece constructor içerisinde set edebiliriz.
    public Paginate(IEnumerable<T> source, int index, int size, int from)
    {
        // (T[])source casting başarısız olursa exception fırlatır.
        // source as T[] casting başarısız olursa null döner.
        T[] enumerable = source as T[] ?? source.ToArray();

        if (from > index)
            throw new ArgumentException($"indexFrom: {from} > pageIndex: {index}, must indexFrom <= pageIndex");

        Index = index;
        Size = size;
        From = from;
        if (source is IQueryable<T> queryable)
        {
            //var queryable = source as IQueryable<T>;
            Count = queryable.Count();
            Items = queryable.Skip((Index - From) * Size).Take(Size).ToList();
        }
        else
        {
            Count = enumerable.Count();
            Items = enumerable.Skip((Index - From) * Size).Take(Size).ToList();
        }
        // int / int = cast edilmiş int
        // 6 / 4 = 1.5 iken cast ile ondalıklı kısım atılacak 1 sonucu geri dönücek.
        // int / double = double
        // 6 / 4.0 = 1.5
        Pages = Convert.ToInt32(Math.Ceiling(Count / Convert.ToDouble(Size)));
    }

    public Paginate()
    {
        Items = Array.Empty<T>();
    }
}