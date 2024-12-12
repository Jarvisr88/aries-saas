namespace DevExpress.Xpf.LayoutControl.UIAutomation
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.LayoutControl;
    using System;
    using System.Windows;
    using System.Windows.Automation;
    using System.Windows.Automation.Peers;
    using System.Windows.Automation.Provider;
    using System.Windows.Controls;

    public abstract class ScrollablePanelAutomationPeer<T> : LayoutControlBaseAutomationPeer<T>, System.Windows.Automation.Provider.IScrollProvider where T: LayoutControlBase
    {
        protected ScrollablePanelAutomationPeer(T owner) : base(owner)
        {
        }

        public override object GetPattern(PatternInterface patternInterface) => 
            (patternInterface != PatternInterface.Scroll) ? base.GetPattern(patternInterface) : this;

        void System.Windows.Automation.Provider.IScrollProvider.Scroll(ScrollAmount horizontalAmount, ScrollAmount verticalAmount)
        {
            if (!base.IsEnabled())
            {
                throw new ElementNotEnabledException();
            }
            bool flag = horizontalAmount != ScrollAmount.NoAmount;
            bool flag2 = verticalAmount != ScrollAmount.NoAmount;
            if ((flag && !this.IScrollProvider.HorizontallyScrollable) || (flag2 && !this.IScrollProvider.VerticallyScrollable))
            {
                throw new InvalidOperationException("OperationCannotBePerformed");
            }
            double pageSize = this.Controller.HorzScrollParams.PageSize;
            double num2 = this.Controller.VertScrollParams.PageSize;
            switch (horizontalAmount)
            {
                case ScrollAmount.LargeDecrement:
                    pageSize = -pageSize;
                    break;

                case ScrollAmount.SmallDecrement:
                    pageSize = -16.0;
                    break;

                case ScrollAmount.NoAmount:
                    pageSize = 0.0;
                    break;

                case ScrollAmount.LargeIncrement:
                    break;

                case ScrollAmount.SmallIncrement:
                    pageSize = -16.0;
                    break;

                default:
                    throw new InvalidOperationException("OperationCannotBePerformed");
            }
            switch (verticalAmount)
            {
                case ScrollAmount.LargeDecrement:
                    num2 = -num2;
                    break;

                case ScrollAmount.SmallDecrement:
                    num2 = -16.0;
                    break;

                case ScrollAmount.NoAmount:
                    num2 = 0.0;
                    break;

                case ScrollAmount.LargeIncrement:
                    break;

                case ScrollAmount.SmallIncrement:
                    num2 = -16.0;
                    break;

                default:
                    throw new InvalidOperationException("OperationCannotBePerformed");
            }
            if (flag | flag2)
            {
                base.Owner.SetOffset(new Point(base.Owner.Offset.X + pageSize, base.Owner.Offset.Y + num2));
            }
        }

        void System.Windows.Automation.Provider.IScrollProvider.SetScrollPercent(double horizontalPercent, double verticalPercent)
        {
            ScrollParams horzScrollParams = this.Controller.HorzScrollParams;
            ScrollParams vertScrollParams = this.Controller.VertScrollParams;
            this.Controller.Scroll(Orientation.Horizontal, ((horzScrollParams.Max - horzScrollParams.PageSize) * horizontalPercent) * 0.01, false, true);
            this.Controller.Scroll(Orientation.Vertical, ((vertScrollParams.Max - vertScrollParams.PageSize) * verticalPercent) * 0.01, false, true);
        }

        private LayoutControllerBase Controller =>
            base.Owner.Controller;

        double System.Windows.Automation.Provider.IScrollProvider.HorizontalScrollPercent =>
            this.Controller.HorzScrollParams.RelativePosition * 100.0;

        double System.Windows.Automation.Provider.IScrollProvider.HorizontalViewSize =>
            this.Controller.HorzScrollParams.RelativePageSize * 100.0;

        bool System.Windows.Automation.Provider.IScrollProvider.HorizontallyScrollable =>
            this.Controller.HorzScrollParams.Enabled;

        private System.Windows.Automation.Provider.IScrollProvider IScrollProvider =>
            this;

        double System.Windows.Automation.Provider.IScrollProvider.VerticalScrollPercent =>
            this.Controller.VertScrollParams.RelativePosition * 100.0;

        double System.Windows.Automation.Provider.IScrollProvider.VerticalViewSize =>
            this.Controller.VertScrollParams.RelativePageSize * 100.0;

        bool System.Windows.Automation.Provider.IScrollProvider.VerticallyScrollable =>
            this.Controller.VertScrollParams.Enabled;
    }
}

