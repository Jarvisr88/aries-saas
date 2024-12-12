namespace DevExpress.Xpf.Editors
{
    using System;
    using System.ComponentModel;

    public class TrackBarEditRange
    {
        private double selectionStart;
        private double selectionEnd;

        public TrackBarEditRange()
        {
        }

        public TrackBarEditRange(double selectionStart, double selectionEnd)
        {
            this.SelectionStart = selectionStart;
            this.SelectionEnd = selectionEnd;
        }

        public override bool Equals(object obj)
        {
            TrackBarEditRange range = obj as TrackBarEditRange;
            return ((range != null) ? ((range.selectionStart == this.SelectionStart) && (range.SelectionEnd == this.SelectionEnd)) : false);
        }

        public override int GetHashCode() => 
            base.GetHashCode();

        [Description("Gets the start of the range.")]
        public double SelectionStart
        {
            get => 
                this.selectionStart;
            internal set
            {
                if (this.selectionEnd < value)
                {
                    this.selectionEnd = value;
                }
                this.selectionStart = value;
            }
        }

        [Description("Gets the end of the range.")]
        public double SelectionEnd
        {
            get => 
                this.selectionEnd;
            internal set
            {
                if (this.selectionStart > value)
                {
                    this.selectionStart = value;
                }
                this.selectionEnd = value;
            }
        }
    }
}

