namespace DevExpress.Xpf.Docking
{
    using DevExpress.Xpf.Layout.Core;
    using System;

    internal interface IDockController2010 : IDockController, IActiveItemOwner, IDisposable
    {
        bool DockAsDocument(BaseLayoutItem item, BaseLayoutItem target, DockType type);
    }
}

