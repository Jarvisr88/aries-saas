namespace DevExpress.Xpf.Docking
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class RequestUniqueNameEventArgs : RoutedEventArgs
    {
        public RequestUniqueNameEventArgs(ISerializableItem item, ICollection<string> existingNames) : base(DockLayoutManager.RequestUniqueNameEvent)
        {
            this.Item = item;
            this.ExistingNames = existingNames;
        }

        public ISerializableItem Item { get; private set; }

        public ICollection<string> ExistingNames { get; private set; }
    }
}

