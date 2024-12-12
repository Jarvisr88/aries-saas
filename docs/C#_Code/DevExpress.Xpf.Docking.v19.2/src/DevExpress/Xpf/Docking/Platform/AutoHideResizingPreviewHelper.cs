namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Windows;
    using System.Windows.Controls;

    internal class AutoHideResizingPreviewHelper : BaseResizingPreviewHelper
    {
        public AutoHideResizingPreviewHelper(LayoutView view) : base(view)
        {
        }

        protected override unsafe Rect GetAdornerBounds(Point change)
        {
            change = new Point(change.X - base.StartPoint.X, change.Y - base.StartPoint.Y);
            Rect rect = new Rect(base.InitialBounds.TopLeft(), base.InitialBounds.BottomRight());
            if (!rect.IsEmpty)
            {
                switch (this.Dock)
                {
                    case System.Windows.Controls.Dock.Left:
                    {
                        Rect* rectPtr1 = &rect;
                        rectPtr1.Width += change.X;
                        break;
                    }
                    case System.Windows.Controls.Dock.Top:
                    {
                        Rect* rectPtr2 = &rect;
                        rectPtr2.Height += change.Y;
                        break;
                    }
                    case System.Windows.Controls.Dock.Right:
                        RectHelper.SetLeft(ref rect, rect.Left + change.X);
                        break;

                    case System.Windows.Controls.Dock.Bottom:
                        RectHelper.SetTop(ref rect, rect.Top + change.Y);
                        break;

                    default:
                        break;
                }
            }
            return rect;
        }

        protected override Rect GetInitialAdornerBounds() => 
            base.GetItemBounds(base.Element);

        protected override Rect GetInitialWindowBounds()
        {
            Point screenPoint = base.View.ClientToScreen(base.StartPoint);
            return base.CorrectBounds(base.BoundsHelper.CalcBounds(screenPoint));
        }

        protected override UIElement GetUIElement()
        {
            AutoHideResizePointer pointer1 = new AutoHideResizePointer();
            pointer1.Dock = this.Dock;
            return pointer1;
        }

        protected override Rect GetWindowBounds(Point change)
        {
            Point screenPoint = base.View.ClientToScreen(change);
            return base.CorrectBounds(base.BoundsHelper.CalcBounds(screenPoint));
        }

        private System.Windows.Controls.Dock Dock =>
            this.AutoHidePaneElement.DockType;

        private DevExpress.Xpf.Docking.Platform.AutoHidePaneElement AutoHidePaneElement =>
            base.Element as DevExpress.Xpf.Docking.Platform.AutoHidePaneElement;

        protected override FloatingMode Mode =>
            base.Owner.EnableWin32Compatibility ? FloatingMode.Window : base.Mode;
    }
}

