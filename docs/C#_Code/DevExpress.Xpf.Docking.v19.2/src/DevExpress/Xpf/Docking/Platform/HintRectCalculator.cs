namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Windows;
    using System.Windows.Controls;

    internal class HintRectCalculator
    {
        public static Rect Calc(DockLayoutElementDragInfo dragInfo, DockHintHitInfo hitInfo) => 
            ((hitInfo == null) || (dragInfo.Item == null)) ? Rect.Empty : (!(dragInfo.DropTarget is AutoHideTrayElement) ? (!(dragInfo.DropTarget is AutoHidePaneElement) ? SelectStrategy(hitInfo).Calc(dragInfo, hitInfo) : Rect.Empty) : Rect.Empty);

        private static BaseCalcStrategy SelectStrategy(DockHintHitInfo hitInfo) => 
            !hitInfo.IsCenter ? (!hitInfo.IsHideButton ? ((BaseCalcStrategy) new RootHint()) : ((BaseCalcStrategy) new HideHint())) : ((BaseCalcStrategy) new CenterHint());

        private abstract class BaseCalcStrategy
        {
            protected BaseCalcStrategy()
            {
            }

            public abstract Rect Calc(DockLayoutElementDragInfo dragInfo, DockHintHitInfo hitInfo);
        }

        private class CenterHint : HintRectCalculator.BaseCalcStrategy
        {
            public override Rect Calc(DockLayoutElementDragInfo dragInfo, DockHintHitInfo hitInfo)
            {
                Rect targetRect;
                BaseLayoutItem target = dragInfo.Target;
                if ((!target.GetIsDocumentHost() || (hitInfo.IsTabButton || ((hitInfo.DockType == DockType.Fill) || !((LayoutGroup) target).HasNotCollapsedItems))) || (target.Parent == null))
                {
                    targetRect = dragInfo.TargetRect;
                }
                else
                {
                    target = target.Parent;
                    targetRect = ElementHelper.GetRect(((IDockLayoutElement) dragInfo.DropTarget).Container);
                }
                return (!DockLayoutManagerParameters.UseLegacyDockPreviewCalculator ? DockPreviewAdvCalculator.DockPreviewItem(dragInfo.Item, target, hitInfo.DockType) : DockHelper.GetDockRect(targetRect, DockPreviewCalculator.DockPreviewItem(targetRect, dragInfo.Item, target, hitInfo.DockType), hitInfo.DockType));
            }
        }

        private class HideHint : HintRectCalculator.BaseCalcStrategy
        {
            public override Rect Calc(DockLayoutElementDragInfo dragInfo, DockHintHitInfo hitInfo)
            {
                Rect rect = ElementHelper.GetRect(dragInfo.View.LayoutRoot);
                bool flag = hitInfo.DockType.ToOrientation() != Orientation.Horizontal;
                Size preferredSize = new Size(flag ? rect.Width : 20.0, flag ? 20.0 : rect.Height);
                return DockHelper.GetDockRect(rect, preferredSize, hitInfo.DockType);
            }
        }

        private class RootHint : HintRectCalculator.BaseCalcStrategy
        {
            public override Rect Calc(DockLayoutElementDragInfo dragInfo, DockHintHitInfo hitInfo)
            {
                if (!DockLayoutManagerParameters.UseLegacyDockPreviewCalculator)
                {
                    return DockPreviewAdvCalculator.DockPreviewItem(dragInfo.Item, dragInfo.Target.GetRoot(), hitInfo.DockType);
                }
                Rect rect = ElementHelper.GetRect(dragInfo.View.LayoutRoot);
                return DockHelper.GetDockRect(rect, DockPreviewCalculator.DockPreviewGroup(rect, dragInfo.Item, dragInfo.Target.GetRoot(), hitInfo.DockType), hitInfo.DockType);
            }
        }
    }
}

