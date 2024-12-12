namespace DevExpress.Xpf.LayoutControl
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Windows;

    public class LayoutGroupController : LayoutControllerBase
    {
        public LayoutGroupController(DevExpress.Xpf.LayoutControl.ILayoutGroup control) : base(control)
        {
        }

        protected override bool CanItemDragAndDrop() => 
            false;

        public virtual FrameworkElement GetItem(Point p, bool ignoreLayoutGroups, bool ignoreLocking)
        {
            FrameworkElement control = (FrameworkElement) base.IPanel.ChildAt(p, true, true, true);
            if (((control == null) && !ignoreLayoutGroups) && RectHelper.New(base.Control.GetSize()).Contains(p))
            {
                return base.Control;
            }
            if (!ignoreLocking && this.ILayoutGroup.IsLocked)
            {
                return base.Control;
            }
            if (control.IsLayoutGroup())
            {
                control = ((DevExpress.Xpf.LayoutControl.ILayoutGroup) control).GetItem(base.Control.MapPoint(p, control), ignoreLayoutGroups, ignoreLocking);
            }
            if ((control == null) && (!this.ILayoutGroup.IsBorderless && !this.ILayoutGroup.IsRoot))
            {
                control = base.Control;
            }
            return control;
        }

        public override FrameworkElement GetMoveableItem(Point p) => 
            this.GetItem(p, true, base.Control.IsInDesignTool());

        public override bool IsScrollable() => 
            false;

        public DevExpress.Xpf.LayoutControl.ILayoutGroup ILayoutGroup =>
            base.IControl as DevExpress.Xpf.LayoutControl.ILayoutGroup;

        protected LayoutGroupProvider LayoutProvider =>
            (LayoutGroupProvider) base.LayoutProvider;
    }
}

