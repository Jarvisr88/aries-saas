namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Runtime.CompilerServices;

    public class EditorRenderCheckBox : RenderCheckBox
    {
        public EditorRenderCheckBox()
        {
            base.RenderTemplateSelector = new EditorRenderCheckBoxTemplateSelector();
        }

        protected override FrameworkRenderElementContext CreateContextInstance() => 
            new EditorRenderCheckBoxContext(this);

        protected override void OnApplyTemplate(FrameworkRenderElementContext context)
        {
            base.OnApplyTemplate(context);
            EditorRenderCheckBoxContext context2 = context as EditorRenderCheckBoxContext;
            if ((context2 != null) && (context2.DisplayMode != CheckEditDisplayMode.Check))
            {
                Func<FrameworkRenderElementContext, bool> predicate = <>c.<>9__2_0;
                if (<>c.<>9__2_0 == null)
                {
                    Func<FrameworkRenderElementContext, bool> local1 = <>c.<>9__2_0;
                    predicate = <>c.<>9__2_0 = x => x.Name == "PART_Image";
                }
                FrameworkRenderElementContext context3 = RenderTreeHelper.FindDescendant(context2, predicate);
                RenderImageContext context4 = context3 as RenderImageContext;
                if (context4 != null)
                {
                    context4.Source = context2.Glyph;
                }
                else
                {
                    RenderContentPresenterContext context5 = context3 as RenderContentPresenterContext;
                    if (context5 != null)
                    {
                        context5.ContentTemplate = context2.GlyphTemplate;
                        context5.Content = context2.Glyph;
                    }
                }
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly EditorRenderCheckBox.<>c <>9 = new EditorRenderCheckBox.<>c();
            public static Func<FrameworkRenderElementContext, bool> <>9__2_0;

            internal bool <OnApplyTemplate>b__2_0(FrameworkRenderElementContext x) => 
                x.Name == "PART_Image";
        }
    }
}

