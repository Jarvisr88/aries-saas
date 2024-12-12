namespace DevExpress.Xpf.Printing.PreviewControl.Native.Editing
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.DocumentViewer;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Editors.Settings;
    using DevExpress.Xpf.Editors.Validation;
    using DevExpress.Xpf.Printing.Themes;
    using DevExpress.XtraPrinting;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Input;

    public class TextInplaceEditor : DocumentInplaceEditorBase
    {
        private readonly EditingProvider editingProvider;
        private bool isStandardEditor;

        public TextInplaceEditor(DocumentInplaceEditorOwner owner, TextInplaceEditorColumn column) : base(owner, column, column.EditingField)
        {
            this.editingProvider = new EditingProvider();
            this.UpdateBorder();
        }

        private void AssignEditTemplateToCharacterComb(CharacterCombEdit edit)
        {
            DataTemplate template = this.EditorColumn.EditorTemplateSelector.With<DataTemplateSelector, DataTemplate>(x => x.SelectTemplate(this.EditingField, null));
            if (template != null)
            {
                try
                {
                    TextEdit edit2 = TemplateHelper.LoadFromTemplate<TextEdit>(template);
                    edit.MaskType = edit2.MaskType;
                    edit.Mask = edit2.Mask;
                    edit.MaskUseAsDisplayFormat = edit2.MaskUseAsDisplayFormat;
                }
                catch
                {
                }
            }
        }

        public override bool CommitEditor(bool closeEditor = false) => 
            this.IsEditorVisible ? base.CommitEditor(closeEditor) : true;

        protected override IBaseEdit CreateEditor(BaseEditSettings settings)
        {
            this.isStandardEditor = true;
            IBaseEdit edit = base.CreateEditor(settings);
            edit.SetInplaceEditingProvider(this.editingProvider);
            return edit;
        }

        protected override DocumentInplaceEditorBase.InplaceEditorInitialData CreateInitialData() => 
            new TextEditorInitialData(this.EditingField.EditValue);

        protected override void InitializeBaseEdit(IBaseEdit newEdit, InplaceEditorBase.BaseEditSourceType newBaseEditSourceType)
        {
            BaseEdit input = newEdit as BaseEdit;
            this.UpdateValue(this.InitialData.Value, input.EditValueType);
            base.InitializeBaseEdit(newEdit, newBaseEditSourceType);
            input.Do<BaseEdit>(delegate (BaseEdit baseEdit) {
                baseEdit.HorizontalContentAlignment = this.EditorColumn.DefaultHorizontalAlignment;
                baseEdit.VerticalContentAlignment = this.EditorColumn.DefaultVerticalAlignment;
                Binding binding = new Binding("EditorPadding");
                binding.Source = this.EditorColumn;
                binding.Mode = BindingMode.OneWay;
                baseEdit.SetBinding(FrameworkElement.MarginProperty, binding);
                baseEdit.Foreground = this.EditorColumn.Foreground;
                if (this.EditorColumn.FontSize > 1.0)
                {
                    Binding binding2 = new Binding("FontSize");
                    binding2.Source = this.EditorColumn;
                    binding2.Mode = BindingMode.OneWay;
                    baseEdit.SetBinding(Control.FontSizeProperty, binding2);
                }
                if (this.EditorColumn.FontFamily != null)
                {
                    baseEdit.FontFamily = this.EditorColumn.FontFamily;
                }
                baseEdit.FontWeight = this.EditingField.Brick.Style.Font.Bold ? FontWeights.Bold : FontWeights.Normal;
                baseEdit.FontStyle = this.EditingField.Brick.Style.Font.Italic ? FontStyles.Italic : FontStyles.Normal;
                (baseEdit as TextEdit).Do<TextEdit>(delegate (TextEdit x) {
                    x.TextDecorations = new TextDecorationCollection();
                    if (this.EditingField.Brick.Style.Font.Strikeout)
                    {
                        x.TextDecorations.Add(TextDecorations.Strikethrough);
                    }
                    if (this.EditingField.Brick.Style.Font.Underline)
                    {
                        x.TextDecorations.Add(TextDecorations.Underline);
                    }
                });
                (baseEdit as CharacterCombEdit).Do<CharacterCombEdit>(delegate (CharacterCombEdit x) {
                    this.AssignEditTemplateToCharacterComb(x);
                    CharacterCombBrick brick = (CharacterCombBrick) this.EditingField.Brick;
                    x.CharacterCombInfo = brick.GetCellInfo(brick.Style);
                    x.Brick = brick;
                    x.PageIndex = this.EditingField.PageIndex;
                    Binding binding1 = new Binding("ZoomFactor");
                    binding1.Source = base.DocumentPresenter.BehaviorProvider;
                    x.SetBinding(CharacterCombEdit.ZoomProperty, binding1);
                });
            });
        }

        protected override void OnGotFocus(RoutedEventArgs e)
        {
            base.OnGotFocus(e);
            base.editCore.Focus();
        }

        private void OnZoomFactorChanged(object sender, ZoomChangedEventArgs e)
        {
            this.EditorColumn.RaiseAppearancePropertiesChanged();
        }

        protected override bool PostEditorCore()
        {
            if (!base.HasAccessToCellValue)
            {
                return true;
            }
            if ((this.ValidationError == null) && string.IsNullOrEmpty(string.Empty))
            {
                try
                {
                    this.EditingField.EditValue = (base.editCore as TextEdit).Return<TextEdit, object>(x => (this.EditingField.Brick.TextValue == null) ? x.DisplayText : x.EditValue, () => base.EditableValue);
                    base.DocumentPresenter.BehaviorProvider.ZoomChanged -= new EventHandler<ZoomChangedEventArgs>(this.OnZoomFactorChanged);
                    return true;
                }
                catch (Exception exception)
                {
                    this.ValidationError = new BaseValidationError(exception.Message, exception);
                    BaseEditHelper.SetValidationError((DependencyObject) base.editCore, this.ValidationError);
                }
            }
            return false;
        }

        protected override DataTemplate SelectTemplate()
        {
            if (!(this.EditingField.Brick is CharacterCombBrick))
            {
                return this.EditorColumn.EditorTemplateSelector.With<DataTemplateSelector, DataTemplate>(x => x.SelectTemplate(this.EditingField, null));
            }
            EditingFieldThemeKeyExtension resourceKey = new EditingFieldThemeKeyExtension();
            resourceKey.ResourceKey = "DevExpress_Xpf_Printing_PredefinedCharacterCombResourceKey";
            return (base.FindResource(resourceKey) as DataTemplate);
        }

        protected override void SetEdit(IBaseEdit value)
        {
            base.DocumentPresenter.BehaviorProvider.ZoomChanged += new EventHandler<ZoomChangedEventArgs>(this.OnZoomFactorChanged);
            base.SetEdit(value);
            if (value != null)
            {
                value.EditMode = EditMode.InplaceActive;
            }
        }

        private void UpdateBorder()
        {
            base.Border.Background = this.EditorColumn.Background;
            base.Border.BorderBrush = this.EditorColumn.BorderBrush;
            if (this.EditingField.Brick is CharacterCombBrick)
            {
                base.Border.BorderThickness = new Thickness(0.0);
            }
            else
            {
                Binding binding = new Binding("BorderThickness");
                binding.Source = this.EditorColumn;
                binding.Mode = BindingMode.OneWay;
                base.Border.SetBinding(Border.BorderThicknessProperty, binding);
            }
        }

        private void UpdateValue(object value, Type type)
        {
            if (this.EditorColumn.IsMultiValue && this.isStandardEditor)
            {
                this.InitialData.Value = this.EditingField.Brick.Text;
            }
            if (type != null)
            {
                try
                {
                    this.InitialData.Value = Convert.ChangeType(value, type);
                }
                catch (Exception)
                {
                    this.InitialData.Value = null;
                }
            }
        }

        public override void ValidateEditorCore()
        {
            base.ValidateEditorCore();
            if ((base.editCore == null) || ((base.Edit == null) || base.Edit.DoValidate()))
            {
                this.ValidationError = null;
            }
            else
            {
                BaseValidationError validationError = BaseEditHelper.GetValidationError((DependencyObject) base.editCore);
                this.ValidationError = validationError;
            }
        }

        private BaseValidationError ValidationError { get; set; }

        protected TextInplaceEditorColumn EditorColumn =>
            (TextInplaceEditorColumn) base.EditorColumn;

        protected TextEditingField EditingField =>
            (TextEditingField) base.EditingField;

        protected TextEditorInitialData InitialData =>
            (TextEditorInitialData) base.InitialData;

        public class EditingProvider : IInplaceEditingProvider
        {
            public bool HandleScrollNavigation(Key key, ModifierKeys keys) => 
                true;

            public bool HandleTextNavigation(Key key, ModifierKeys keys) => 
                true;
        }

        protected class TextEditorInitialData : DocumentInplaceEditorBase.InplaceEditorInitialData
        {
            public TextEditorInitialData(object value) : base(value)
            {
            }
        }
    }
}

