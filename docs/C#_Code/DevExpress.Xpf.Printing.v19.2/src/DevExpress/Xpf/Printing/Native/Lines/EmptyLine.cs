namespace DevExpress.Xpf.Printing.Native.Lines
{
    using System;
    using System.Windows.Controls;

    internal class EmptyLine : DesignLine
    {
        private Control content;

        public EmptyLine()
        {
            UserControl control1 = new UserControl();
            control1.Height = 4.0;
            control1.IsTabStop = false;
            control1.IsHitTestVisible = false;
            this.content = control1;
        }

        public override Control Content =>
            this.content;
    }
}

