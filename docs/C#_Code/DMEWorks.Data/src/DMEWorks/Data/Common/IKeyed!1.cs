namespace DMEWorks.Data.Common
{
    public interface IKeyed<TKey>
    {
        TKey Key { get; }
    }
}

