namespace DevExpress.Xpf.Core
{
    using System;

    public static class DXMessageBoxFactory
    {
        public static void RegisterMessageBoxCreator(DXMessageBoxCreator creator)
        {
            DXMessageBox.creator = creator;
        }
    }
}

