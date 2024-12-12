namespace DevExpress.Xpf.Editors.Helpers
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class ItemsProviderRowLoadedEventArgs : ItemsProviderChangedEventArgs
    {
        public ItemsProviderRowLoadedEventArgs(object handle, int controllerRow, object value = null)
        {
            this.Handle = handle;
            this.ControllerRow = controllerRow;
            this.Value = value;
        }

        public object Handle { get; private set; }

        public object Value { get; private set; }

        public int ControllerRow { get; private set; }
    }
}

