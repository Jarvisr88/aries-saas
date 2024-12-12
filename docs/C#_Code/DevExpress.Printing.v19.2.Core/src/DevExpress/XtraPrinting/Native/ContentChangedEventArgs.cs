namespace DevExpress.XtraPrinting.Native
{
    using System;

    public class ContentChangedEventArgs
    {
        public readonly bool Guaranteed;

        public ContentChangedEventArgs(bool guaranteed)
        {
            this.Guaranteed = guaranteed;
        }
    }
}

