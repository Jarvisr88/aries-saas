namespace DevExpress.Xpf.Editors
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors.Flyout.Native;
    using DevExpress.Xpf.Editors.Popups;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls.Primitives;
    using System.Windows.Media;

    public class PopupResizingStrategyBase
    {
        public PopupResizingStrategyBase(PopupBaseEdit editor)
        {
            this.Editor = editor;
        }

        private bool GetDropOpposite()
        {
            Func<FrameworkElement, double> func1 = <>c.<>9__47_0;
            if (<>c.<>9__47_0 == null)
            {
                Func<FrameworkElement, double> local1 = <>c.<>9__47_0;
                func1 = <>c.<>9__47_0 = x => x.ActualHeight;
            }
            return (((this.PopupChild - ((FrameworkElement) func1).Return<FrameworkElement, double>(((Func<FrameworkElement, double>) (<>c.<>9__47_1 ??= () => 0.0)), (<>c.<>9__47_1 ??= () => 0.0))) <= 0.0) ? (this.AvailableDownHeight < this.AvailableUpHeight) : false);
        }

        private Size GetIndent()
        {
            PopupBorderControl popupChild = this.PopupChild as PopupBorderControl;
            if (popupChild == null)
            {
                return Size.Empty;
            }
            double contentWidth = popupChild.ContentWidth;
            double contentHeight = popupChild.ContentHeight;
            if (contentWidth.IsNaN() || contentHeight.IsNaN())
            {
                FrameworkElement content = popupChild.Content as FrameworkElement;
                contentWidth = content.DesiredSize.Width;
                contentHeight = content.DesiredSize.Height;
            }
            Size desiredSize = popupChild.DesiredSize;
            return new Size(Math.Max((double) 0.0, (double) (desiredSize.Width - contentWidth)), Math.Max((double) 0.0, (double) (desiredSize.Height - contentHeight)));
        }

        private bool GetIsLeft() => 
            (DropDownOptionsHelper.GetActualAlignment() != DropDownAlignment.Right) ? ((this.AvailableLeftWidth >= this.GetPopupWidth()) || (this.AvailableRightWidth < this.AvailableLeftWidth)) : ((this.AvailableRightWidth < this.GetPopupWidth()) && (this.AvailableRightWidth < this.AvailableLeftWidth));

        public PlacementMode GetPlacement() => 
            !this.DropOpposite ? PlacementMode.Bottom : PlacementMode.Top;

        public double GetPopupHeight(double offset) => 
            this.DropOpposite ? ((this.RootRect.Top >= (this.PopupRect.Top + offset)) ? (this.PopupRect.Bottom - this.RootRect.Top) : this.GetPopupHeightInternal(offset)) : ((this.RootRect.Bottom <= (this.PopupRect.Bottom + offset)) ? (this.RootRect.Bottom - this.PopupRect.Top) : this.GetPopupHeightInternal(offset));

        private double GetPopupHeightInternal(double offset) => 
            !this.DropOpposite ? Math.Max((double) (this.Editor.PopupHeight + offset), (double) 0.0) : Math.Max((double) (this.Editor.PopupHeight - offset), (double) 0.0);

        private double GetPopupWidth()
        {
            double? nullable2;
            double? nullable1;
            if (this.Editor == null)
            {
                return 0.0;
            }
            if (!double.IsNaN(this.Editor.PopupWidth))
            {
                return this.Editor.PopupWidth;
            }
            EditorPopupBase popup = this.Editor.Popup;
            if (popup == null)
            {
                EditorPopupBase local1 = popup;
                nullable2 = null;
                nullable1 = nullable2;
            }
            else
            {
                UIElement child = popup.Child;
                if (child != null)
                {
                    nullable1 = new double?(child.DesiredSize.Width);
                }
                else
                {
                    UIElement local2 = child;
                    nullable2 = null;
                    nullable1 = nullable2;
                }
            }
            double? nullable = nullable1;
            return ((nullable != null) ? nullable.GetValueOrDefault() : 0.0);
        }

        public double GetPopupWidth(double offset)
        {
            if (!this.IsLeft)
            {
                if (this.AvailableRightWidth < (this.PopupRect.Width + offset))
                {
                    return this.AvailableRightWidth;
                }
            }
            else if (this.AvailableLeftWidth < (this.PopupRect.Width + offset))
            {
                return this.AvailableLeftWidth;
            }
            return this.GetPopupWidthInternal(offset);
        }

        private double GetPopupWidthInternal(double offset) => 
            Math.Max((double) (this.Editor.PopupWidth + offset), (double) 0.0);

        public void UpdateDropOpposite()
        {
            this.Indent = this.GetIndent();
            this.DropOpposite = this.GetDropOpposite();
            this.IsLeft = this.GetIsLeft();
        }

        protected FrameworkElement PopupChild
        {
            get
            {
                Func<EditorPopupBase, FrameworkElement> evaluator = <>c.<>9__1_0;
                if (<>c.<>9__1_0 == null)
                {
                    Func<EditorPopupBase, FrameworkElement> local1 = <>c.<>9__1_0;
                    evaluator = <>c.<>9__1_0 = popup => popup.Child as FrameworkElement;
                }
                return this.Editor.Popup.Return<EditorPopupBase, FrameworkElement>(evaluator, null);
            }
        }

        protected internal bool DropOpposite { get; set; }

        protected internal bool IsLeft { get; set; }

        protected PopupBaseEdit Editor { get; private set; }

        protected FrameworkElement Root =>
            VisualTreeHelper.GetChild(LayoutHelper.GetRoot(this.Editor), 0) as FrameworkElement;

        protected virtual bool IsRootRTL =>
            false;

        internal bool IsRTL =>
            this.Editor.FlowDirection == FlowDirection.RightToLeft;

        private bool SameFlowDirection =>
            (!this.IsRTL || !this.IsRootRTL) ? (!this.IsRTL && !this.IsRootRTL) : true;

        private double AvailableUpHeight
        {
            get
            {
                bool sameFlowDirection = this.SameFlowDirection;
                return Math.Max((double) (this.EditorRect.Top - this.RootRect.Top), (double) 0.0);
            }
        }

        private double AvailableDownHeight
        {
            get
            {
                bool sameFlowDirection = this.SameFlowDirection;
                return Math.Max((double) (this.RootRect.Bottom - this.EditorRect.Bottom), (double) 0.0);
            }
        }

        public double ActualAvailableHeight =>
            !this.DropOpposite ? (this.AvailableDownHeight - this.Indent.Height) : (this.AvailableUpHeight - this.Indent.Height);

        public double ActualAvailableWidth =>
            this.IsLeft ? (this.AvailableLeftWidth - this.Indent.Width) : (this.AvailableRightWidth - this.Indent.Width);

        private double AvailableRightWidth =>
            this.IsRTL ? (this.EditorRect.Right - this.RootRect.Left) : (this.RootRect.Right - this.EditorRect.Left);

        private double AvailableLeftWidth =>
            this.IsRTL ? (this.RootRect.Right - this.EditorRect.Left) : (this.EditorRect.Right - this.RootRect.Left);

        private Size Indent { get; set; }

        protected virtual Rect PopupRect =>
            new Rect(PopupScreenHelper.GetPopupScreenPoint(this.Editor), new Size(this.PopupChild.ActualWidth, this.PopupChild.ActualHeight));

        protected internal virtual Rect RootRect =>
            ScreenHelper.GetScreenRect(ScreenHelper.GetScaledRect(TranslateHelper.ToScreen(this.Editor, TranslateHelper.TranslateBounds(this.Editor, this.Editor))).Center());

        protected virtual Rect EditorRect =>
            new Rect(PopupScreenHelper.GetPopupScreenPoint(this.Editor), new Size(this.Editor.ActualWidth, this.Editor.ActualHeight));

        public Size Restrictions =>
            new Size(Math.Max(this.AvailableLeftWidth, this.AvailableRightWidth), Math.Max(this.AvailableDownHeight, this.AvailableUpHeight));

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PopupResizingStrategyBase.<>c <>9 = new PopupResizingStrategyBase.<>c();
            public static Func<EditorPopupBase, FrameworkElement> <>9__1_0;
            public static Func<FrameworkElement, double> <>9__47_0;
            public static Func<double> <>9__47_1;

            internal FrameworkElement <get_PopupChild>b__1_0(EditorPopupBase popup) => 
                popup.Child as FrameworkElement;

            internal double <GetDropOpposite>b__47_0(FrameworkElement x) => 
                x.ActualHeight;

            internal double <GetDropOpposite>b__47_1() => 
                0.0;
        }
    }
}

