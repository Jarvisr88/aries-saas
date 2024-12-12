namespace DevExpress.Xpf.Editors.RangeControl
{
    using System;

    public class EditRange
    {
        private object start;
        private object end;

        public EditRange()
        {
        }

        public EditRange(object start, object end)
        {
            this.Start = start;
            this.End = end;
        }

        public override bool Equals(object obj)
        {
            EditRange range = obj as EditRange;
            return ((range != null) ? ((range.start == this.Start) && (range.End == this.End)) : false);
        }

        public override int GetHashCode() => 
            base.GetHashCode();

        public object Start
        {
            get => 
                this.start;
            private set => 
                this.start = value;
        }

        public object End
        {
            get => 
                this.end;
            private set => 
                this.end = value;
        }
    }
}

