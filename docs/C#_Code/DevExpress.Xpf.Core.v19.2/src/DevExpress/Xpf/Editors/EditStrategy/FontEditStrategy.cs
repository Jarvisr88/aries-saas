namespace DevExpress.Xpf.Editors.EditStrategy
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Services;
    using DevExpress.Xpf.Editors.Settings;
    using DevExpress.Xpf.Editors.Validation.Native;
    using System;
    using System.Collections;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Media;

    public class FontEditStrategy : ComboBoxEditStrategy
    {
        public FontEditStrategy(FontEdit editor) : base(editor)
        {
        }

        public virtual FontFamily CoerceFont(FontFamily baseValue) => 
            (FontFamily) this.CoerceValue(FontEdit.FontProperty, baseValue);

        public bool Confirm(object newValue) => 
            MessageBox.Show(string.Format(this.GetConfirmationDialogMessage(), newValue), this.GetConfirmationDialogCaption(), MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK;

        protected override object CreateEditableItem(int index, ChangeTextItem item) => 
            (index >= 0) ? base.CreateEditableItem(index, item) : this.CreateEditableItemForNonexistentValue(index, base.ValueContainer.EditValue, item);

        protected override EditorSpecificValidator CreateEditorValidatorService() => 
            new FontEditValidator(this, base.SelectorUpdater, this.Editor);

        public virtual void FontChanged(FontFamily oldValue, FontFamily newValue)
        {
            if (!base.ShouldLockUpdate)
            {
                base.SyncWithValue(FontEdit.FontProperty, oldValue, newValue);
            }
        }

        private string GetConfirmationDialogCaption() => 
            EditorLocalizer.GetString(EditorStringId.ConfirmationDialogCaption);

        private string GetConfirmationDialogMessage() => 
            EditorLocalizer.GetString(EditorStringId.ConfirmationDialogMessage);

        private object GetEditValueFromFont(object baseValue) => 
            baseValue;

        private object GetFontFromEditValue(object baseValue)
        {
            if ((baseValue is IList) && (((IList) baseValue).Count > 0))
            {
                baseValue = ((IList) baseValue)[0];
            }
            return this.GetFontFromValue(baseValue);
        }

        private FontFamily GetFontFromValue(object value)
        {
            if (value == null)
            {
                return null;
            }
            Func<LookUpEditableItem, object> evaluator = <>c.<>9__7_0;
            if (<>c.<>9__7_0 == null)
            {
                Func<LookUpEditableItem, object> local1 = <>c.<>9__7_0;
                evaluator = <>c.<>9__7_0 = x => x.EditValue;
            }
            object obj2 = (value as LookUpEditableItem).Return<LookUpEditableItem, object>(evaluator, () => value);
            return ((obj2 != null) ? ((obj2 is FontFamily) ? ((FontFamily) obj2) : new FontFamily(obj2.ToString())) : null);
        }

        public bool IsUserInput(UpdateEditorSource updateSource) => 
            (updateSource == UpdateEditorSource.EnterKeyPressed) || (updateSource == UpdateEditorSource.LostFocus);

        private bool IsValueInItemsSource(object value) => 
            (value != null) && FontEditSettings.CachedFonts.Contains(this.GetFontFromValue(value));

        protected override void RegisterUpdateCallbacks()
        {
            base.RegisterUpdateCallbacks();
            base.PropertyUpdater.Register(FontEdit.FontProperty, baseValue => this.GetEditValueFromFont(baseValue), baseValue => this.GetFontFromEditValue(baseValue));
        }

        public bool ShouldConfirm(object value, UpdateEditorSource updateSource) => 
            this.Editor.AllowConfirmFontUseDialog && (!this.IsValueInItemsSource(value) && this.IsUserInput(updateSource));

        private FontEdit Editor =>
            base.Editor as FontEdit;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly FontEditStrategy.<>c <>9 = new FontEditStrategy.<>c();
            public static Func<LookUpEditableItem, object> <>9__7_0;

            internal object <GetFontFromValue>b__7_0(LookUpEditableItem x) => 
                x.EditValue;
        }
    }
}

