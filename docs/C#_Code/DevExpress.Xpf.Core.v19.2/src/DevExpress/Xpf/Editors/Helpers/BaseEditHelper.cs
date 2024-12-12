namespace DevExpress.Xpf.Editors.Helpers
{
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Native;
    using DevExpress.Xpf.Editors.Settings;
    using DevExpress.Xpf.Editors.Validation;
    using System;
    using System.Windows;
    using System.Windows.Input;

    public static class BaseEditHelper
    {
        public static readonly DependencyProperty AllowCheckListBoxItemProperty = DependencyProperty.RegisterAttached("AllowCheckListBoxItem", typeof(bool), typeof(BaseEditHelper), new PropertyMetadata(true));

        public static void ApplySettings(IBaseEdit edit, BaseEditSettings settings, IDefaultEditorViewInfo editorColumn)
        {
            if ((edit is IInplaceBaseEdit) && ((settings != null) && !ReferenceEquals(settings, edit.Settings)))
            {
                settings.ApplyToEdit(edit, true, editorColumn);
            }
        }

        public static void AssignViewInfoProperties(BaseEdit edit, BaseEditSettings settings, IDefaultEditorViewInfo defaultViewInfo)
        {
            settings.AssignViewInfoProperties(edit, defaultViewInfo);
        }

        public static void FlushPendingEditActions(IBaseEdit edit)
        {
            edit.FlushPendingEditActions();
        }

        public static bool GetAllowCheckListBoxItem(DependencyObject obj) => 
            (bool) obj.GetValue(AllowCheckListBoxItemProperty);

        public static IBaseEdit GetBaseEdit(IBaseEdit edit)
        {
            IInplaceBaseEdit edit2 = edit as IInplaceBaseEdit;
            return ((edit2 == null) ? edit : edit2.BaseEdit);
        }

        public static BaseEditSettings GetEditSettings(IBaseEdit edit) => 
            edit.Settings;

        public static object GetEditValue(IBaseEdit edit) => 
            !(edit is BaseEdit) ? edit.EditValue : ((BaseEdit) edit).PropertyProvider.EditValue;

        public static bool GetIsActivatingKey(IBaseEdit edit, KeyEventArgs e) => 
            edit.IsActivatingKey(GetKey(e), ModifierKeysHelper.GetKeyboardModifiers(e));

        public static bool GetIsChildElement(IBaseEdit edit, IInputElement element) => 
            edit.IsChildElement(element, null);

        public static bool GetIsValueChanged(IBaseEdit edit) => 
            edit.IsValueChanged;

        public static Key GetKey(KeyEventArgs e) => 
            (e.Key == Key.System) ? e.SystemKey : e.Key;

        public static bool GetNeedsKey(IBaseEdit edit, KeyEventArgs e) => 
            edit.NeedsKey(GetKey(e), ModifierKeysHelper.GetKeyboardModifiers(e));

        public static bool GetRequireDisplayTextSorting(BaseEditSettings editSettings) => 
            (editSettings != null) && editSettings.RequireDisplayTextSorting;

        public static bool GetShowEditorButtons(IBaseEdit edit) => 
            edit.GetShowEditorButtons();

        public static BaseValidationError GetValidationError(DependencyObject d)
        {
            IBaseEdit edit = d as IBaseEdit;
            return ((edit == null) ? BaseEdit.GetValidationError(d) : edit.ValidationError);
        }

        public static DependencyPropertyKey GetValidationErrorPropertyKey() => 
            BaseEdit.ValidationErrorPropertyKey;

        public static void ProcessActivatingKey(IBaseEdit edit, KeyEventArgs e)
        {
            edit.ProcessActivatingKey(GetKey(e), ModifierKeysHelper.GetKeyboardModifiers(e));
        }

        public static void RaiseDefaultButtonClick(ButtonEditSettings settings)
        {
            settings.RaiseDefaultButtonClick(settings, new RoutedEventArgs());
        }

        public static void ResetEditorCache(IBaseEdit edit)
        {
            BaseEdit edit2 = edit as BaseEdit;
            if (edit2 != null)
            {
                edit2.EditStrategy.PropertyUpdater.ResetSyncValue();
            }
        }

        public static void SetAllowCheckListBoxItem(DependencyObject obj, bool value)
        {
            obj.SetValue(AllowCheckListBoxItemProperty, value);
        }

        public static void SetCurrentValue(BaseEdit edit, DependencyProperty property, object value)
        {
            edit.SetCurrentValue(property, value);
        }

        public static void SetDisplayTextProvider(IBaseEdit edit, IDisplayTextProvider displayTextProvider)
        {
            InplaceBaseEdit edit2 = edit as InplaceBaseEdit;
            if (edit2 != null)
            {
                edit2.SetDisplayTextProvider(displayTextProvider);
            }
            else
            {
                BaseEdit edit3 = edit as BaseEdit;
                if (edit3 != null)
                {
                    edit3.SetDisplayTextProvider(displayTextProvider);
                }
            }
        }

        public static void SetEditValue(IBaseEdit edit, object editValue)
        {
            if ((editValue == null) && (edit.EditValue == null))
            {
                ResetEditorCache(edit);
            }
            edit.EditValue = editValue;
        }

        public static bool SetIsValueChanged(IBaseEdit edit, bool value)
        {
            bool flag;
            edit.IsValueChanged = flag = value;
            return flag;
        }

        public static void SetShowBorder(IBaseEdit edit, bool show)
        {
            if (edit is IInplaceBaseEdit)
            {
                ((IInplaceBaseEdit) edit).ShowBorder = show;
            }
            else if (edit is BaseEdit)
            {
                ((BaseEdit) edit).ShowBorderInInplaceMode = show;
            }
        }

        public static void SetShowBorderInInplaceInactiveMode(IBaseEdit edit, bool show)
        {
            if (edit is FrameworkElement)
            {
                ControlHelper.SetShowFocusedState((FrameworkElement) edit, show);
            }
        }

        public static void SetShowEditorButtons(IBaseEdit edit, bool show)
        {
            edit.SetShowEditorButtons(show);
        }

        public static void SetTextDecorations(DependencyObject d, TextDecorationCollection textDecorations)
        {
            IInplaceBaseEdit edit = d as IInplaceBaseEdit;
            if (edit != null)
            {
                edit.TextDecorations = textDecorations;
            }
            TextEdit edit2 = d as TextEdit;
            if (edit2 != null)
            {
                edit2.TextDecorations = textDecorations;
            }
        }

        public static void SetTotalDisplayTextProvider(IBaseEdit edit, IDisplayTextProvider displayTextProvider)
        {
            BaseEdit edit2 = edit as BaseEdit;
            if (edit2 != null)
            {
                edit2.SetTotalDisplayTextProvider(displayTextProvider);
            }
        }

        public static void SetValidationError(DependencyObject d, BaseValidationError error)
        {
            IBaseEdit edit = d as IBaseEdit;
            if (edit != null)
            {
                edit.ValidationError = error;
            }
            else
            {
                BaseEdit.SetValidationError(d, error);
            }
        }

        public static void SetValidationErrorTemplate(DependencyObject d, DataTemplate template)
        {
            IBaseEdit edit = d as IBaseEdit;
            if (edit != null)
            {
                edit.ValidationErrorTemplate = template;
            }
            else
            {
                BaseEdit.SetValidationErrorTemplate(d, template);
            }
        }

        public static void ToggleCheckEdit(CheckEdit edit)
        {
            edit.Toggle();
        }

        public static void UpdateErrorTooltip(IBaseEdit edit)
        {
            edit.UpdateErrorTooltip();
        }

        public static void UpdateHighlightingText(IBaseEdit edit, TextHighlightingProperties highlightingProperties)
        {
            highlightingProperties ??= SearchControlHelper.GetDefaultTextHighlightingProperties();
            string highlightedText = highlightingProperties.Text ?? string.Empty;
            HighlightedTextCriteria highlightedTextCriteria = LookUpEditHelper.GetHighlightedTextCriteria(highlightingProperties.FilterCondition);
            InplaceBaseEdit inplaceBaseEdit = edit as InplaceBaseEdit;
            if (inplaceBaseEdit != null)
            {
                UpdateInplaceBaseEditHighlighting(inplaceBaseEdit, highlightedText, highlightedTextCriteria);
            }
            else
            {
                ISupportTextHighlighting highlighting = edit as ISupportTextHighlighting;
                if (highlighting != null)
                {
                    highlighting.UpdateHighlightedText(highlightedText, highlightedTextCriteria);
                }
            }
        }

        private static void UpdateInplaceBaseEditHighlighting(InplaceBaseEdit inplaceBaseEdit, string highlightedText, HighlightedTextCriteria criteria)
        {
            TextEditSettings settings = inplaceBaseEdit.Settings as TextEditSettings;
            if (settings != null)
            {
                settings.HighlightedText = highlightedText;
                settings.HighlightedTextCriteria = criteria;
            }
        }
    }
}

