namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public class ResolutionItem
    {
        public ResolutionItem(Size res)
        {
            this.Resolution = res;
        }

        public bool IsGreater(ResolutionItem item) => 
            (this.Resolution.Width * this.Resolution.Height) > (item.Resolution.Width * item.Resolution.Height);

        public Size Resolution { get; private set; }

        public string DisplayText =>
            $"{this.Resolution.Width} x {this.Resolution.Height}";
    }
}

