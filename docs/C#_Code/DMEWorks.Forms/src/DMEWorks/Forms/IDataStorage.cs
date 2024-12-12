namespace DMEWorks.Forms
{
    using System;

    public interface IDataStorage
    {
        void AcceptChanges();
        void RejectChanges();

        object OriginalValue { get; }

        object Value { get; set; }

        Type DataType { get; }
    }
}

