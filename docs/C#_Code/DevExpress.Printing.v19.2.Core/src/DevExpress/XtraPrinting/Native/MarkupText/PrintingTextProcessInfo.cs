namespace DevExpress.XtraPrinting.Native.MarkupText
{
    using DevExpress.Utils.Text;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using System;

    public class PrintingTextProcessInfo : TextProcessInfoBase
    {
        public readonly BrickStyle Style;
        public readonly IMeasurer Measurer;

        public PrintingTextProcessInfo(BrickStyle style, IMeasurer measurer);
    }
}

