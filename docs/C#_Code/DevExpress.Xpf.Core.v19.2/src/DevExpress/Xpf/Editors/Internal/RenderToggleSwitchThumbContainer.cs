namespace DevExpress.Xpf.Editors.Internal
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Windows;

    public class RenderToggleSwitchThumbContainer : RenderDecorator
    {
        private double offset;

        protected override Size ArrangeOverride(Size finalSize, IFrameworkRenderElementContext context)
        {
            RenderToggleSwitchThumbContainerContext context2 = (RenderToggleSwitchThumbContainerContext) context;
            if (base.Child == null)
            {
                return base.ArrangeOverride(finalSize, context);
            }
            Size desiredSize = context2.Child.DesiredSize;
            double num = finalSize.Width - desiredSize.Width;
            double num2 = 0.0;
            double x = Math.Max(Math.Min(context2.Offset * (num - num2), num), num2);
            context2.Child.Arrange(new Rect(new Point(x, 0.0), new Size(desiredSize.Width, finalSize.Height)));
            return finalSize;
        }

        protected override FrameworkRenderElementContext CreateContextInstance() => 
            new RenderToggleSwitchThumbContainerContext(this);

        protected override Size MeasureOverride(Size availableSize, IFrameworkRenderElementContext context)
        {
            Size size = base.MeasureOverride(availableSize, context);
            RenderToggleSwitchThumbContainerContext context2 = (RenderToggleSwitchThumbContainerContext) context;
            context2.ThumbWidth = context2.Child.DesiredSize.Width;
            return size;
        }

        public double Offset
        {
            get => 
                this.offset;
            set => 
                base.SetProperty<double>(ref this.offset, value);
        }
    }
}

