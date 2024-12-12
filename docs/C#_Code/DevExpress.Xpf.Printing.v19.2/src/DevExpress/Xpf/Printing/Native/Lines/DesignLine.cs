namespace DevExpress.Xpf.Printing.Native.Lines
{
    using System;
    using System.Windows.Controls;

    internal abstract class DesignLine : LineBase
    {
        protected DesignLine()
        {
        }

        protected override void Dispose(bool disposing)
        {
        }

        public override void RefreshContent()
        {
        }

        public override void SetText(string text)
        {
        }

        public override Label Header =>
            null;
    }
}

