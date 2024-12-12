namespace DevExpress.Xpf.Editors.Internal
{
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public abstract class ToggleSwitchLayoutStrategyBase
    {
        protected const string CheckedStateContentName = "PART_CheckedStateContent";
        protected const string UncheckedStateContentName = "PART_UncheckedStateContent";
        protected const string SwitchPanelName = "PART_SwitchPanel";

        protected ToggleSwitchLayoutStrategyBase()
        {
        }

        public abstract Size Arrange(Size finalSize, IFrameworkRenderElementContext context);
        protected FrameworkRenderElementContext FindElement(IFrameworkRenderElementContext context, string name)
        {
            for (int i = 0; i < context.RenderChildrenCount; i++)
            {
                FrameworkRenderElementContext renderChild = context.GetRenderChild(i);
                if (renderChild.Name == name)
                {
                    return renderChild;
                }
            }
            return null;
        }

        protected FrameworkRenderElementContext FindSwitch(IFrameworkRenderElementContext context) => 
            this.FindElement(context, "PART_SwitchPanel");

        protected bool HasCheckedContent() => 
            (this.Owner.CheckedStateContent != null) || (this.Owner.Content != null);

        protected bool HasUncheckedContent() => 
            (this.Owner.UncheckedStateContent != null) || (this.Owner.Content != null);

        protected bool IsCheckedContent(FrameworkRenderElementContext child) => 
            child.Name == "PART_CheckedStateContent";

        protected bool IsSwitch(FrameworkRenderElementContext child) => 
            child.Name == "PART_SwitchPanel";

        protected bool IsUncheckedContent(FrameworkRenderElementContext child) => 
            child.Name == "PART_UncheckedStateContent";

        public abstract Size Measure(Size availableSize, IFrameworkRenderElementContext context);
        protected unsafe Size MeasureBase(Size availableSize, IFrameworkRenderElementContext context)
        {
            Size size = new Size();
            double num = 0.0;
            for (int i = 0; i < context.RenderChildrenCount; i++)
            {
                FrameworkRenderElementContext renderChild = context.GetRenderChild(i);
                renderChild.Measure(availableSize);
                if (!this.IsSwitch(renderChild))
                {
                    num = Math.Max(num, renderChild.DesiredSize.Width);
                }
                else
                {
                    Size* sizePtr1 = &size;
                    sizePtr1.Width += renderChild.DesiredSize.Width;
                }
                size.Height = Math.Max(size.Height, renderChild.DesiredSize.Height);
            }
            Size* sizePtr2 = &size;
            sizePtr2.Width += num;
            return size;
        }

        public ToggleSwitch Owner { get; set; }

        protected bool IsIndeterminateState =>
            this.Owner.IsChecked == null;
    }
}

