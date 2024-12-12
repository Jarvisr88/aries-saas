namespace DevExpress.XtraPrinting.Native.MarkupText
{
    using DevExpress.Utils;
    using DevExpress.Utils.Text;
    using DevExpress.XtraPrinting;
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public class PrintingStringInfo : StringInfoBase
    {
        private BrickStringFormat stringFormat;

        public PrintingStringInfo(DevExpress.XtraPrinting.BrickStyle brickStyle);
        public override bool GetIsEllipsisTrimming();
        public override StringFormat GetStringFormat();

        public DevExpress.XtraPrinting.BrickStyle BrickStyle { get; private set; }

        public override bool RightToLeft { get; }

        public override DevExpress.Utils.WordWrap WordWrap { get; }

        public override VertAlignment VAlignment { get; }

        public override HorzAlignment HAlignment { get; }

        public override System.Drawing.Font Font { get; }
    }
}

