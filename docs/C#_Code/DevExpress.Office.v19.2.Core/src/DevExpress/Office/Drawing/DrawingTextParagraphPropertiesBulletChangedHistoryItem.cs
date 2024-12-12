namespace DevExpress.Office.Drawing
{
    using DevExpress.Office.History;
    using System;

    public class DrawingTextParagraphPropertiesBulletChangedHistoryItem : DrawingHistoryItem<DrawingTextParagraphProperties, IDrawingBullet>
    {
        private DrawingBulletType type;

        public DrawingTextParagraphPropertiesBulletChangedHistoryItem(DrawingTextParagraphProperties owner, DrawingBulletType type, IDrawingBullet oldValue, IDrawingBullet newValue) : base(owner.DocumentModel.MainPart, owner, oldValue, newValue)
        {
            this.type = type;
        }

        protected override void RedoCore()
        {
            base.Owner.SetBulletCore(this.type, base.NewValue);
        }

        public override void Register(IHistoryWriter writer, bool undone)
        {
            base.Register(writer, undone);
            writer.RegisterObject(base.OldValue);
            writer.RegisterObject(base.NewValue);
        }

        protected override void UndoCore()
        {
            base.Owner.SetBulletCore(this.type, base.OldValue);
        }

        public override void Write(IHistoryWriter writer)
        {
            base.Write(writer);
            writer.Write((byte) this.type);
            writer.Write(writer.GetObjectId(base.OldValue));
            writer.Write(writer.GetObjectId(base.NewValue));
        }

        public override void WriteObjects(IHistoryWriter writer, bool undone)
        {
            writer.WriteObject(undone ? base.NewValue : base.OldValue);
        }
    }
}

