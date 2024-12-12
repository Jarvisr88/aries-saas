namespace DevExpress.Office.Drawing
{
    using DevExpress.Office.DrawingML;
    using System;

    public class Scene3DCameraRotationInfoIndexChangeHistoryItem : Scene3DPropertiesHistoryItem
    {
        public Scene3DCameraRotationInfoIndexChangeHistoryItem(Scene3DProperties obj) : base(obj)
        {
        }

        protected override void RedoCore()
        {
            base.Object.SetIndexCore(Scene3DProperties.CameraRotationInfoIndexAccessor, base.NewIndex, base.ChangeActions);
        }

        protected override void UndoCore()
        {
            base.Object.SetIndexCore(Scene3DProperties.CameraRotationInfoIndexAccessor, base.OldIndex, base.ChangeActions);
        }
    }
}

