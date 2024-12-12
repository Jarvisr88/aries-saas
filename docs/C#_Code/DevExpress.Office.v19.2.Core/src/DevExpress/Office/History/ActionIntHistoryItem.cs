﻿namespace DevExpress.Office.History
{
    using DevExpress.Office;
    using System;

    public class ActionIntHistoryItem : ActionHistoryItem<int>
    {
        public ActionIntHistoryItem(IDocumentModelPart documentModelPart, int oldValue, int newValue, Action<int> setValueAction) : base(documentModelPart, oldValue, newValue, setValueAction)
        {
        }

        public override void Write(IHistoryWriter writer)
        {
            base.Write(writer);
            writer.Write(base.OldValue);
            writer.Write(base.NewValue);
        }
    }
}
