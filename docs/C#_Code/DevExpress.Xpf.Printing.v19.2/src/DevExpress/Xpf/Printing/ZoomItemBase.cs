namespace DevExpress.Xpf.Printing
{
    using System;
    using System.ComponentModel;

    public abstract class ZoomItemBase
    {
        protected ZoomItemBase()
        {
        }

        public override string ToString() => 
            this.DisplayedText;

        [Description("Gets the text representation of a ZoomValueItem in Print Preview.")]
        public abstract string DisplayedText { get; }
    }
}

