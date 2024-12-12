namespace DevExpress.Xpf.LayoutControl
{
    using System;
    using System.Runtime.CompilerServices;

    public class LayoutControlNewItemInfo
    {
        public LayoutControlNewItemInfo(string label, object data)
        {
            this.Label = label;
            this.Data = data;
        }

        public object Data { get; private set; }

        public string Label { get; private set; }
    }
}

