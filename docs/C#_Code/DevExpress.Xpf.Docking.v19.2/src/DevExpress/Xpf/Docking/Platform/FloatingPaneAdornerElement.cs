namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Docking.UIAutomation;
    using DevExpress.Xpf.Docking.VisualElements;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Automation.Peers;
    using System.Windows.Controls;
    using System.Windows.Data;

    public class FloatingPaneAdornerElement : Decorator, IFloatingPane, ISupportAutoSize
    {
        public FloatingPaneAdornerElement(BaseFloatingContainer container)
        {
            this.FloatGroup = DockLayoutManager.GetLayoutItem(container) as DevExpress.Xpf.Docking.FloatGroup;
            this.Manager = DockLayoutManager.GetDockLayoutManager(container);
            WindowHelper.BindFlowDirection(this, this.Manager);
            base.UseLayoutRounding = true;
            this.FloatGroup.Forward(this, UIElement.IsEnabledProperty, "IsEnabled", BindingMode.OneWay);
        }

        Size ISupportAutoSize.FitToContent(Size availableSize) => 
            this.MeasureAutoSize(availableSize);

        public void EnsureFlowDirection()
        {
            WindowHelper.BindFlowDirectionIfNeeded(this, this.Manager);
        }

        private static Size GetAutoSize(System.Windows.SizeToContent sizeToContent, Size bounds, Size desiredSize)
        {
            Size size = bounds;
            switch (sizeToContent)
            {
                case System.Windows.SizeToContent.Width:
                    size.Width = desiredSize.Width;
                    break;

                case System.Windows.SizeToContent.Height:
                    size.Height = desiredSize.Height;
                    break;

                case System.Windows.SizeToContent.WidthAndHeight:
                    size = desiredSize;
                    break;

                default:
                    break;
            }
            return size;
        }

        private static Size GetConstraintSize(System.Windows.SizeToContent sizeToContent, Size bounds)
        {
            Size infinite = bounds;
            switch (sizeToContent)
            {
                case System.Windows.SizeToContent.Width:
                    infinite.Width = double.PositiveInfinity;
                    break;

                case System.Windows.SizeToContent.Height:
                    infinite.Height = double.PositiveInfinity;
                    break;

                case System.Windows.SizeToContent.WidthAndHeight:
                    infinite = SizeHelper.Infinite;
                    break;

                default:
                    break;
            }
            return infinite;
        }

        private Size MeasureAutoSize(Size availableSize)
        {
            Size constraintSize = GetConstraintSize(this.SizeToContent, availableSize);
            base.Measure(constraintSize);
            Size[] minSizes = new Size[] { base.DesiredSize, this.FloatGroup.ActualMinSize };
            Size desiredSize = MathHelper.MeasureMinSize(minSizes);
            Size floatSize = GetAutoSize(this.SizeToContent, availableSize, desiredSize);
            this.FloatGroup.UpdateAutoFloatingSize(floatSize);
            return floatSize;
        }

        protected override AutomationPeer OnCreateAutomationPeer() => 
            new FloatingPaneAdornerElementAutomationPeer(this);

        public DockLayoutManager Manager { get; private set; }

        public DevExpress.Xpf.Docking.FloatGroup FloatGroup { get; private set; }

        public System.Windows.SizeToContent SizeToContent
        {
            get
            {
                Func<DevExpress.Xpf.Docking.FloatGroup, System.Windows.SizeToContent> evaluator = <>c.<>9__12_0;
                if (<>c.<>9__12_0 == null)
                {
                    Func<DevExpress.Xpf.Docking.FloatGroup, System.Windows.SizeToContent> local1 = <>c.<>9__12_0;
                    evaluator = <>c.<>9__12_0 = x => x.SizeToContent;
                }
                return this.FloatGroup.Return<DevExpress.Xpf.Docking.FloatGroup, System.Windows.SizeToContent>(evaluator, (<>c.<>9__12_1 ??= () => System.Windows.SizeToContent.Manual));
            }
        }

        DockLayoutManager IFloatingPane.Manager =>
            this.Manager;

        DevExpress.Xpf.Docking.FloatGroup IFloatingPane.FloatGroup =>
            this.FloatGroup;

        bool ISupportAutoSize.IsAutoSize =>
            this.SizeToContent != System.Windows.SizeToContent.Manual;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly FloatingPaneAdornerElement.<>c <>9 = new FloatingPaneAdornerElement.<>c();
            public static Func<FloatGroup, SizeToContent> <>9__12_0;
            public static Func<SizeToContent> <>9__12_1;

            internal SizeToContent <get_SizeToContent>b__12_0(FloatGroup x) => 
                x.SizeToContent;

            internal SizeToContent <get_SizeToContent>b__12_1() => 
                SizeToContent.Manual;
        }
    }
}

