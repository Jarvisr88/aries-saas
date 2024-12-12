namespace DevExpress.Office.Drawing
{
    using DevExpress.Office.History;
    using System;

    public class DrawingTextSpacingInfoIndexChangeHistoryItem : DrawingTextParagraphPropertiesHistoryItem
    {
        private int index;

        public DrawingTextSpacingInfoIndexChangeHistoryItem(DrawingTextParagraphProperties obj, int index) : base(obj)
        {
            this.index = index;
        }

        public override void Read(IHistoryReader reader)
        {
            base.Read(reader);
            this.index = reader.ReadInt32();
        }

        protected override void RedoCore()
        {
            base.Object.SetIndexCore((DrawingTextSpacingInfoIndexAccessor) DrawingTextParagraphProperties.TextParagraphPropertiesIndexAccessors[this.index], base.NewIndex, base.ChangeActions);
        }

        protected override void UndoCore()
        {
            base.Object.SetIndexCore((DrawingTextSpacingInfoIndexAccessor) DrawingTextParagraphProperties.TextParagraphPropertiesIndexAccessors[this.index], base.OldIndex, base.ChangeActions);
        }

        public override void Write(IHistoryWriter writer)
        {
            base.Write(writer);
            writer.Write(this.index);
        }
    }
}

