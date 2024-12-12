namespace DevExpress.XtraPrinting.Native.LayoutAdjustment
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Collections.Generic;

    public class VisualBrickLayoutData : BrickLayoutDataBase
    {
        private List<ILayoutData> childrenData;

        public VisualBrickLayoutData(VisualBrick brick, float dpi);
        protected virtual void AddToChildrenData(VisualBrick item, List<ILayoutData> childrenData, float dpi);
        private float CalculateContainerHeight(bool isShrinkablePanel, bool panelCanGrow);
        private static float GetInitialHeight(ILayoutData layoutData);
        public override void UpdateViewBounds();

        public override bool NeedAdjust { get; }

        public override List<ILayoutData> ChildrenData { get; }
    }
}

