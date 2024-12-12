namespace DevExpress.Data
{
    using System;

    public interface IDataControllerCurrentSupport
    {
        void OnCurrentControllerRowChanged(CurrentRowEventArgs e);
        void OnCurrentControllerRowObjectChanged(CurrentRowChangedEventArgs e);
    }
}

