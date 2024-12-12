namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Runtime.CompilerServices;

    public class RangeEditRange
    {
        public RangeEditRange()
        {
        }

        public RangeEditRange(object selectionStart, object selectionEnd)
        {
            this.SelectionStart = selectionStart;
            this.SelectionEnd = selectionEnd;
        }

        public override bool Equals(object obj)
        {
            RangeEditRange range = obj as RangeEditRange;
            return ((range != null) ? ((range.SelectionStart == this.SelectionStart) && (range.SelectionEnd == this.SelectionEnd)) : false);
        }

        public override int GetHashCode() => 
            ((this.SelectionStart != null) ? this.SelectionStart.GetHashCode() : 0x35) ^ ((this.SelectionEnd != null) ? this.SelectionEnd.GetHashCode() : 0x6f);

        public object SelectionStart { get; set; }

        public object SelectionEnd { get; set; }
    }
}

