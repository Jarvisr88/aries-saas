namespace DevExpress.Office.Internal
{
    using DevExpress.Office;

    public interface ICacheSupport<T> : ICloneable<T>, ISupportsCopyFrom<T>, ISupportsSizeOf
    {
    }
}

