namespace DevExpress.Mvvm.UI
{
    using System;
    using System.Runtime.CompilerServices;

    public class MapItem
    {
        public MapItem()
        {
        }

        public MapItem(object source, object target)
        {
            this.Source = source;
            this.Target = target;
        }

        public object Source { get; set; }

        public object Target { get; set; }
    }
}

