namespace DevExpress.XtraPrinting.Drawing
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    internal class ImageItemIdChangingEventArgs : CancelEventArgs
    {
        public ImageItemIdChangingEventArgs(string id)
        {
            this.Id = id;
        }

        public string Id { get; private set; }
    }
}

