namespace ActiproSoftware.MarkupLabel
{
    using System;

    public class MarkupLabelLinkClickEventArgs : EventArgs
    {
        private MarkupLabelAnchorElement #irb;

        public MarkupLabelLinkClickEventArgs(MarkupLabelAnchorElement element)
        {
            this.#irb = element;
        }

        public MarkupLabelAnchorElement Element =>
            this.#irb;
    }
}

