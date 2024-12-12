namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;
    using System.Collections;
    using System.Drawing;
    using System.Reflection;

    public class PdfTextLines : IEnumerable
    {
        private string[] lines;
        private bool directionRightToLeft;
        private bool directionVertical;

        public PdfTextLines(string[] lines, StringFormat format)
        {
            this.lines = lines;
            this.directionRightToLeft = (format.FormatFlags & StringFormatFlags.DirectionRightToLeft) != 0;
            this.directionVertical = (format.FormatFlags & StringFormatFlags.DirectionVertical) != 0;
        }

        IEnumerator IEnumerable.GetEnumerator() => 
            !this.directionVertical ? ((IEnumerator) new ForwardEnumerator(this)) : (!this.directionRightToLeft ? ((IEnumerator) new BackwardEnumerator(this)) : ((IEnumerator) new ForwardEnumerator(this)));

        public int Count =>
            this.lines.Length;

        public string this[int index] =>
            this.lines[index];

        public class BackwardEnumerator : PdfTextLines.Enumerator
        {
            public BackwardEnumerator(PdfTextLines lines) : base(lines)
            {
            }

            protected override bool NextIndex(ref int index)
            {
                int num = index - 1;
                index = num;
                return (num >= 0);
            }

            protected override void ResetIndex(ref int index)
            {
                index = base.Lines.Count;
            }
        }

        public abstract class Enumerator : IEnumerator
        {
            private PdfTextLines lines;
            private int index;

            protected Enumerator(PdfTextLines lines)
            {
                this.lines = lines;
                ((IEnumerator) this).Reset();
            }

            protected abstract bool NextIndex(ref int index);
            protected abstract void ResetIndex(ref int index);
            bool IEnumerator.MoveNext() => 
                this.NextIndex(ref this.index);

            void IEnumerator.Reset()
            {
                this.ResetIndex(ref this.index);
            }

            protected PdfTextLines Lines =>
                this.lines;

            object IEnumerator.Current =>
                this.lines[this.index];
        }

        public class ForwardEnumerator : PdfTextLines.Enumerator
        {
            public ForwardEnumerator(PdfTextLines lines) : base(lines)
            {
            }

            protected override bool NextIndex(ref int index)
            {
                int num = index + 1;
                index = num;
                return (num < base.Lines.Count);
            }

            protected override void ResetIndex(ref int index)
            {
                index = -1;
            }
        }
    }
}

