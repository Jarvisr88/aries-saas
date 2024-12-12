namespace DevExpress.Xpf.Editors.Internal
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Editors.Settings;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class InplaceBaseEditTemplateSelector : RenderTemplateSelector
    {
        private static RenderTemplate GetTextEditInplaceInactiveTemplate(FrameworkElement chrome, EditorContent editor)
        {
            BaseEditSettings settings = editor.Settings;
            CheckEditSettings settings2 = settings as CheckEditSettings;
            InplaceResourceProvider resourceProvider = ThemeHelper.GetResourceProvider(chrome);
            if (editor.DisplayTemplate == null)
            {
                Func<LookUpEditSettingsBase, bool> evaluator = <>c.<>9__1_0;
                if (<>c.<>9__1_0 == null)
                {
                    Func<LookUpEditSettingsBase, bool> local1 = <>c.<>9__1_0;
                    evaluator = <>c.<>9__1_0 = x => (x.IsTextEditable == null) ? false : (!x.ApplyItemTemplateToSelectedItem ? false : !x.IsTextEditable.Value);
                }
                if (!(editor.Settings as LookUpEditSettingsBase).If<LookUpEditSettingsBase>(evaluator).ReturnSuccess<LookUpEditSettingsBase>())
                {
                    if (settings2 != null)
                    {
                        return resourceProvider.GetCheckEditInplaceInactiveTemplate(chrome);
                    }
                    bool showEditorButtons = editor.ShowEditorButtons;
                    return ((((settings is ButtonEditSettings) & showEditorButtons) || (editor.ShowBorder || ((editor.Error != null) || !editor.ShowText))) ? resourceProvider.GetCommonBaseEditInplaceInactiveTemplate(chrome) : resourceProvider.GetTextEditInplaceInactiveTemplate(chrome));
                }
            }
            return resourceProvider.GetCommonBaseEditInplaceInactiveTemplateWithDisplayTemplate(chrome);
        }

        public override RenderTemplate SelectTemplate(FrameworkElement chrome, FrameworkRenderElementContext context, object content)
        {
            if (content == null)
            {
                return base.SelectTemplate(chrome, context, null);
            }
            InplaceResourceProvider resourceProvider = ThemeHelper.GetResourceProvider(chrome);
            EditorContent editor = (EditorContent) content;
            return ((editor.EditMode == EditMode.InplaceInactive) ? GetTextEditInplaceInactiveTemplate(chrome, editor) : resourceProvider.GetTextEditInplaceActiveTemplate(chrome));
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly InplaceBaseEditTemplateSelector.<>c <>9 = new InplaceBaseEditTemplateSelector.<>c();
            public static Func<LookUpEditSettingsBase, bool> <>9__1_0;

            internal bool <GetTextEditInplaceInactiveTemplate>b__1_0(LookUpEditSettingsBase x) => 
                (x.IsTextEditable == null) ? false : (!x.ApplyItemTemplateToSelectedItem ? false : !x.IsTextEditable.Value);
        }
    }
}

