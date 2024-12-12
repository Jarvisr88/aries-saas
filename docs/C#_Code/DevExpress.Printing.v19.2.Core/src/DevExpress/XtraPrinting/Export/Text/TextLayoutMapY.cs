namespace DevExpress.XtraPrinting.Export.Text
{
    using DevExpress.XtraPrinting.Export;
    using System;
    using System.Collections;

    internal class TextLayoutMapY : TextLayoutMap
    {
        public TextLayoutMapY(LayoutControlCollection layoutControls) : base(layoutControls)
        {
        }

        protected override void DecomposeCompoundControl(int groupIndex, ref int index, TextBrickViewData control)
        {
            for (int i = 1; i < control.Texts.Count; i++)
            {
                base.GetGroup(++groupIndex).Add(control.GetLayoutItem(i));
            }
        }

        protected override bool ItemInGroup(TextBrickViewData control, TextLayoutGroup group) => 
            control.Bounds.Y <= group.MinCenterY;

        protected override IComparer PrimaryControlComparer =>
            TextBrickViewData.YComparer.Instance;

        protected override IComparer SecondaryItemComparer =>
            TextLayoutItem.XComparer.Instance;
    }
}

