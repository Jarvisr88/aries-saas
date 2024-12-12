namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Core.Native;
    using System;

    public class EditorRenderButton : RenderButton
    {
        protected override FrameworkRenderElementContext CreateContextInstance() => 
            new EditorRenderButtonContext(this);

        protected override void OnApplyTemplate(FrameworkRenderElementContext context)
        {
            EditorRenderButtonContext templatedParent = (EditorRenderButtonContext) context;
            ButtonInfoBase dataContext = context.DataContext as ButtonInfoBase;
            if (dataContext != null)
            {
                dataContext.BeginInit();
                dataContext.FindContent(templatedParent);
                dataContext.EndInit();
            }
            base.OnApplyTemplate(context);
        }
    }
}

