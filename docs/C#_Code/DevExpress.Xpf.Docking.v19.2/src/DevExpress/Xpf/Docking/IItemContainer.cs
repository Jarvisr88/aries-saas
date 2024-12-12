namespace DevExpress.Xpf.Docking
{
    using System;

    internal interface IItemContainer
    {
        void ClearContainer(object item);
        void PrepareContainer(object item);
    }
}

