namespace DevExpress.Xpf.Editors.Internal
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Helpers;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class DefaultButtonInfoTemplateSelector : RenderTemplateSelector
    {
        private RenderTemplate SelectSpinButtonTemplate(FrameworkElement chrome, SpinButtonInfo spinInfo) => 
            ThemeHelper.GetResourceProvider(chrome).GetRenderSpinButtonTemplate(chrome);

        public override RenderTemplate SelectTemplate(FrameworkElement chrome, FrameworkRenderElementContext context1, object content)
        {
            Func<RenderContentControlContext, object> evaluator = <>c.<>9__0_0;
            if (<>c.<>9__0_0 == null)
            {
                Func<RenderContentControlContext, object> local1 = <>c.<>9__0_0;
                evaluator = <>c.<>9__0_0 = x => x.Content;
            }
            object obj2 = (content as RenderContentControlContext).With<RenderContentControlContext, object>(evaluator);
            InplaceResourceProvider resourceProvider = ThemeHelper.GetResourceProvider(chrome);
            SpinButtonInfo spinInfo = obj2 as SpinButtonInfo;
            if (spinInfo != null)
            {
                return this.SelectSpinButtonTemplate(chrome, spinInfo);
            }
            Func<ButtonInfo, RenderTemplate> func2 = <>c.<>9__0_1;
            if (<>c.<>9__0_1 == null)
            {
                Func<ButtonInfo, RenderTemplate> local2 = <>c.<>9__0_1;
                func2 = <>c.<>9__0_1 = x => x.RenderTemplate;
            }
            RenderTemplate local3 = (obj2 as ButtonInfo).With<ButtonInfo, RenderTemplate>(func2);
            RenderTemplate renderButtonTemplate = local3;
            if (local3 == null)
            {
                RenderTemplate local4 = local3;
                renderButtonTemplate = resourceProvider.GetRenderButtonTemplate(chrome);
            }
            return renderButtonTemplate;
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DefaultButtonInfoTemplateSelector.<>c <>9 = new DefaultButtonInfoTemplateSelector.<>c();
            public static Func<RenderContentControlContext, object> <>9__0_0;
            public static Func<ButtonInfo, RenderTemplate> <>9__0_1;

            internal object <SelectTemplate>b__0_0(RenderContentControlContext x) => 
                x.Content;

            internal RenderTemplate <SelectTemplate>b__0_1(ButtonInfo x) => 
                x.RenderTemplate;
        }
    }
}

