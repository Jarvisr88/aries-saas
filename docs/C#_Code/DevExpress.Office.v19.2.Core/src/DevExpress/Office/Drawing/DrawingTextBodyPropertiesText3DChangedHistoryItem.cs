﻿namespace DevExpress.Office.Drawing
{
    using DevExpress.Office.History;
    using System;

    public class DrawingTextBodyPropertiesText3DChangedHistoryItem : DrawingHistoryItem<DrawingTextBodyProperties, IDrawingText3D>
    {
        public DrawingTextBodyPropertiesText3DChangedHistoryItem(DrawingTextBodyProperties owner, IDrawingText3D oldValue, IDrawingText3D newValue) : base(owner.DocumentModel.MainPart, owner, oldValue, newValue)
        {
        }

        protected override void RedoCore()
        {
            base.Owner.SetText3DCore(base.NewValue);
        }

        public override void Register(IHistoryWriter writer, bool undone)
        {
            base.Register(writer, undone);
            writer.RegisterObject(base.OldValue);
            writer.RegisterObject(base.NewValue);
        }

        protected override void UndoCore()
        {
            base.Owner.SetText3DCore(base.OldValue);
        }

        public override void Write(IHistoryWriter writer)
        {
            base.Write(writer);
            writer.Write(writer.GetObjectId(base.OldValue));
            writer.Write(writer.GetObjectId(base.NewValue));
        }

        public override void WriteObjects(IHistoryWriter writer, bool undone)
        {
            writer.WriteObject(undone ? base.NewValue : base.OldValue);
        }
    }
}
