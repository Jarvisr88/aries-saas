namespace DevExpress.Office.Drawing
{
    using DevExpress.Office.DrawingML;
    using System;

    public class Scene3DLightRigRotationInfoIndexChangeHistoryItem : Scene3DPropertiesHistoryItem
    {
        public Scene3DLightRigRotationInfoIndexChangeHistoryItem(Scene3DProperties obj) : base(obj)
        {
        }

        protected override void RedoCore()
        {
            base.Object.SetIndexCore(Scene3DProperties.LightRigRotationInfoIndexAccessor, base.NewIndex, base.ChangeActions);
        }

        protected override void UndoCore()
        {
            base.Object.SetIndexCore(Scene3DProperties.LightRigRotationInfoIndexAccessor, base.OldIndex, base.ChangeActions);
        }
    }
}

