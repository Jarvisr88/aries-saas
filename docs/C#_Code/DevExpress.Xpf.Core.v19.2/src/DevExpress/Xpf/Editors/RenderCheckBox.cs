namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Core.Native;
    using System;

    public class RenderCheckBox : RenderButton
    {
        public const string PaddingTargetName = "PART_PaddingTarget";

        public RenderCheckBox()
        {
            base.RenderTemplateSelector = new RenderCheckBoxTemplateSelector();
        }

        protected override FrameworkRenderElementContext CreateContextInstance() => 
            new RenderCheckBoxContext(this);

        protected override FrameworkRenderElementContext GetPaddingTarget(RenderControlContext context)
        {
            FrameworkRenderElementContext element = context.InnerNamescope.GetElement("PART_PaddingTarget");
            return ((element == null) ? base.GetPaddingTarget(context) : element);
        }
    }
}

