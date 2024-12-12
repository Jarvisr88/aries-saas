namespace DevExpress.Data
{
    using System;

    internal class NullCurrentSupport : IDataControllerCurrentSupport
    {
        internal static NullCurrentSupport Default;

        static NullCurrentSupport();
        void IDataControllerCurrentSupport.OnCurrentControllerRowChanged(CurrentRowEventArgs e);
        void IDataControllerCurrentSupport.OnCurrentControllerRowObjectChanged(CurrentRowChangedEventArgs e);
    }
}

