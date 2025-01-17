﻿namespace DevExpress.Office.Drawing
{
    using DevExpress.Office.History;
    using System;

    public class Shape3DPropertiesPresetTypeHistoryItem : DrawingHistoryItem<ShapeBevel3DProperties, PresetBevelType>
    {
        public Shape3DPropertiesPresetTypeHistoryItem(ShapeBevel3DProperties owner, PresetBevelType oldValue, PresetBevelType newValue) : base(owner.DocumentModel.MainPart, owner, oldValue, newValue)
        {
        }

        protected override void RedoCore()
        {
            base.Owner.SetPresetCore(base.NewValue);
        }

        protected override void UndoCore()
        {
            base.Owner.SetPresetCore(base.OldValue);
        }

        public override void Write(IHistoryWriter writer)
        {
            base.Write(writer);
            writer.Write((byte) base.OldValue);
            writer.Write((byte) base.NewValue);
        }
    }
}

