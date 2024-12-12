namespace DevExpress.Xpf.LayoutControl
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public abstract class LayoutProviderBase
    {
        private LayoutParametersBase _Parameters;

        public LayoutProviderBase(ILayoutModelBase model)
        {
            this.Model = model;
        }

        public Size Arrange(FrameworkElements items, Rect bounds, Rect viewPortBounds, LayoutParametersBase parameters)
        {
            this.Parameters = parameters;
            return this.OnArrange(items, bounds, viewPortBounds);
        }

        public virtual void CopyLayoutInfo(FrameworkElement from, FrameworkElement to)
        {
            to.Width = from.Width;
            to.Height = from.Height;
            to.MinWidth = from.DesiredSize.Width - (from.Margin.Left + from.Margin.Right);
            to.MinHeight = from.DesiredSize.Height - (from.Margin.Top + from.Margin.Bottom);
            to.MaxWidth = from.MaxWidth;
            to.MaxHeight = from.MaxHeight;
            to.Margin = from.Margin;
            to.HorizontalAlignment = from.HorizontalAlignment;
            to.VerticalAlignment = from.VerticalAlignment;
        }

        public Size Measure(FrameworkElements items, Size maxSize, LayoutParametersBase parameters)
        {
            this.Parameters = parameters;
            return this.OnMeasure(items, maxSize);
        }

        protected virtual Size OnArrange(FrameworkElements items, Rect bounds, Rect viewPortBounds) => 
            bounds.Size();

        protected virtual Size OnMeasure(FrameworkElements items, Size maxSize) => 
            new Size(0.0, 0.0);

        protected virtual void OnParametersChanged()
        {
        }

        public virtual void UpdateChildBounds(FrameworkElement child, ref Rect bounds)
        {
        }

        public virtual void UpdateScrollableAreaBounds(ref Rect bounds)
        {
        }

        protected ILayoutModelBase Model { get; private set; }

        protected LayoutParametersBase Parameters
        {
            get => 
                this._Parameters;
            private set
            {
                this._Parameters = value;
                this.OnParametersChanged();
            }
        }
    }
}

