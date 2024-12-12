namespace DevExpress.Office.Drawing
{
    using DevExpress.Office.History;
    using DevExpress.Office.Model;
    using System;

    public abstract class RectangleOffsetHistoryItem : HistoryItem
    {
        private RectangleOffset oldValue;
        private RectangleOffset newValue;
        private DrawingBlipFill blipFill;

        protected RectangleOffsetHistoryItem(DrawingBlipFill blipFill, RectangleOffset oldValue, RectangleOffset newValue) : base(blipFill.DocumentModel.MainPart)
        {
            this.blipFill = blipFill;
            this.oldValue = oldValue;
            this.newValue = newValue;
        }

        public override object GetTargetObject() => 
            this.blipFill;

        public override void Write(IHistoryWriter writer)
        {
            base.Write(writer);
            this.oldValue.Write(writer);
            this.newValue.Write(writer);
        }

        public DrawingBlipFill BlipFill =>
            this.blipFill;

        public RectangleOffset OldValue =>
            this.oldValue;

        public RectangleOffset NewValue =>
            this.newValue;
    }
}

