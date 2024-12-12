namespace DevExpress.Xpf.Printing
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class ZoomFitModeItem : ZoomItemBase
    {
        public ZoomFitModeItem(DevExpress.Xpf.Printing.ZoomFitMode zoomFitMode)
        {
            this.ZoomFitMode = zoomFitMode;
        }

        [Description("Gets the zoom fit mode of ZoomFitModeItem.")]
        public DevExpress.Xpf.Printing.ZoomFitMode ZoomFitMode { get; private set; }

        [Description("Gets the text that is displayed in Document Preview for ZoomFitModeItem.")]
        public override string DisplayedText =>
            PrintingLocalizer.GetString((PrintingStringId) Enum.Parse(typeof(PrintingStringId), "ZoomTo" + this.ZoomFitMode.ToString(), false));
    }
}

