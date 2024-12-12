namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Editors.Settings;
    using DevExpress.Xpf.Editors.Validation;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Input;

    public interface IBaseEdit : IInputElement
    {
        event RoutedEventHandler EditorActivated;

        event EditValueChangedEventHandler EditValueChanged;

        event RoutedEventHandler Loaded;

        event RoutedEventHandler Unloaded;

        void BeginInit();
        void BeginInit(bool callBase);
        void ClearError();
        void ClearValue(DependencyProperty dp);
        bool DoValidate();
        void EndInit();
        void EndInit(bool callBase, bool shouldSync = true);
        void FlushPendingEditActions();
        void ForceInitialize(bool callBase);
        string GetDisplayText(object editValue, bool applyFormatting);
        bool GetShowEditorButtons();
        object GetValue(DependencyProperty d);
        bool IsActivatingKey(Key key, ModifierKeys modifiers);
        bool IsChildElement(IInputElement element, DependencyObject root = null);
        bool NeedsKey(Key key, ModifierKeys modifiers);
        void ProcessActivatingKey(Key key, ModifierKeys modifiers);
        void RaiseEvent(RoutedEventArgs e);
        void SelectAll();
        BindingExpressionBase SetBinding(DependencyProperty dp, BindingBase binding);
        void SetInplaceEditingProvider(IInplaceEditingProvider provider);
        void SetSettings(BaseEditSettings settings);
        void SetShowEditorButtons(bool show);
        void UpdateDisplayText();
        void UpdateErrorTooltip();

        bool ShouldDisableExcessiveUpdatesInInplaceInactiveMode { get; set; }

        bool? DisableExcessiveUpdatesInInplaceInactiveMode { get; set; }

        string DisplayText { get; }

        DevExpress.Xpf.Editors.EditMode EditMode { get; set; }

        object EditValue { get; set; }

        bool IsReadOnly { get; set; }

        bool IsEditorActive { get; }

        object DataContext { get; set; }

        ControlTemplate DisplayTemplate { get; set; }

        ControlTemplate EditTemplate { get; set; }

        HorizontalAlignment HorizontalContentAlignment { get; set; }

        VerticalAlignment VerticalContentAlignment { get; set; }

        DevExpress.Xpf.Editors.Validation.InvalidValueBehavior InvalidValueBehavior { get; set; }

        IValueConverter DisplayTextConverter { get; set; }

        string DisplayFormatString { get; set; }

        BaseEditSettings Settings { get; }

        bool CanAcceptFocus { get; set; }

        double MaxWidth { get; set; }

        bool HasValidationError { get; }

        bool ShowEditorButtons { get; set; }

        BaseValidationError ValidationError { get; set; }

        DataTemplate ErrorToolTipContentTemplate { get; set; }

        DataTemplate ValidationErrorTemplate { get; set; }

        bool IsPrintingMode { get; set; }

        bool IsValueChanged { get; set; }

        FrameworkElement EditCore { get; }

        string NullText { get; set; }

        object NullValue { get; set; }

        bool ShowNullText { get; set; }

        bool ShowNullTextForEmptyValue { get; set; }
    }
}

