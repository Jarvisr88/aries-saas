namespace DevExpress.Office
{
    using System;

    public interface ISupportsCopyFrom<T>
    {
        void CopyFrom(T value);
    }
}

