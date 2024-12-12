namespace DevExpress.Office.Drawing
{
    using DevExpress.Office.DrawingML;
    using System;

    public class Scene3DPropertiesInfoIndexChangeHistoryItem : Scene3DPropertiesHistoryItem
    {
        public Scene3DPropertiesInfoIndexChangeHistoryItem(Scene3DProperties obj) : base(obj)
        {
        }

        protected override void RedoCore()
        {
            base.Object.SetIndexCore(Scene3DProperties.InfoIndexAccessor, base.NewIndex, base.ChangeActions);
        }

        protected override void UndoCore()
        {
            base.Object.SetIndexCore(Scene3DProperties.InfoIndexAccessor, base.OldIndex, base.ChangeActions);
        }
    }
}

