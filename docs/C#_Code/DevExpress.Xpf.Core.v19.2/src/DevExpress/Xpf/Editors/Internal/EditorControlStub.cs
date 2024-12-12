namespace DevExpress.Xpf.Editors.Internal
{
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class EditorControlStub : ControlEx
    {
        protected const string ContentPresenterName = "PART_ContentPresenter";
        protected const string EditCoreName = "PART_Editor";
        protected const string ImagePresenterName = "PART_Image";
        public static readonly DependencyProperty IsTextEditableProperty;
        public static readonly DependencyProperty IsReadOnlyProperty;
        public static readonly DependencyProperty EditValueProperty;
        public static readonly DependencyProperty IsCheckedProperty;
        public static readonly DependencyProperty SelectedItemProperty;
        public static readonly DependencyProperty SelectedIndexProperty;
        public static readonly DependencyProperty DisplayTextProperty;
        public static readonly DependencyProperty HighlightedTextProperty;
        public static readonly DependencyProperty HighlightedTextCriteriaProperty;
        public static readonly DependencyProperty ItemTemplateSelectorProperty;
        public static readonly DependencyProperty ItemTemplateProperty;
        public static readonly DependencyProperty StyleSettingsProperty;

        static EditorControlStub()
        {
            Type ownerType = typeof(EditorControlStub);
            HighlightedTextProperty = DependencyPropertyManager.Register("HighlightedText", typeof(string), ownerType, new PropertyMetadata(null, (o, args) => ((EditorControlStub) o).HighlightedTextChanged(args.NewValue)));
            HighlightedTextCriteriaProperty = DependencyPropertyManager.Register("HighlightedTextCriteria", typeof(DevExpress.Xpf.Editors.HighlightedTextCriteria), ownerType, new PropertyMetadata(DevExpress.Xpf.Editors.HighlightedTextCriteria.StartsWith));
            IsTextEditableProperty = DependencyPropertyManager.Register("IsTextEditable", typeof(bool), ownerType, new PropertyMetadata(false));
            IsReadOnlyProperty = DependencyPropertyManager.Register("IsReadOnly", typeof(bool), ownerType, new PropertyMetadata(false, new PropertyChangedCallback(EditorControlStub.IsReadOnlyChanged)));
            EditValueProperty = DependencyPropertyManager.Register("EditValue", typeof(object), ownerType, new PropertyMetadata(null));
            IsCheckedProperty = DependencyPropertyManager.Register("IsChecked", typeof(object), ownerType, new PropertyMetadata(null));
            SelectedItemProperty = DependencyPropertyManager.Register("SelectedItem", typeof(object), ownerType, new PropertyMetadata(null, new PropertyChangedCallback(EditorControlStub.SelectedItemChanged)));
            SelectedIndexProperty = DependencyPropertyManager.Register("SelectedIndex", typeof(object), ownerType, new PropertyMetadata(null));
            DisplayTextProperty = DependencyPropertyManager.Register("DisplayText", typeof(object), ownerType, new PropertyMetadata(null, (o, args) => ((EditorControlStub) o).DisplayTextChanged(args.NewValue)));
            ItemTemplateSelectorProperty = DependencyPropertyManager.Register("ItemTemplateSelector", typeof(DataTemplateSelector), ownerType, new PropertyMetadata(null, new PropertyChangedCallback(EditorControlStub.ItemTemplateSelectorChanged)));
            ItemTemplateProperty = DependencyPropertyManager.Register("ItemTemplate", typeof(DataTemplate), ownerType, new PropertyMetadata(null, new PropertyChangedCallback(EditorControlStub.ItemTemplateChanged)));
            Control.VerticalContentAlignmentProperty.OverrideMetadata(ownerType, new FrameworkPropertyMetadata(new PropertyChangedCallback(EditorControlStub.VerticalContentAlignmentChanged)));
            Control.HorizontalContentAlignmentProperty.OverrideMetadata(ownerType, new FrameworkPropertyMetadata(new PropertyChangedCallback(EditorControlStub.HorizontalContentAlignmentChanged)));
            StyleSettingsProperty = DependencyPropertyManager.Register("StyleSettings", typeof(BaseEditStyleSettings), ownerType, new PropertyMetadata(null));
        }

        private void AssignContent()
        {
            if (this.ContentPresenter != null)
            {
                this.ContentPresenter.Content = this.SelectedItem;
            }
        }

        private void AssignContentTemplate()
        {
            if (this.ContentPresenter != null)
            {
                this.ContentPresenter.ContentTemplate = this.ItemTemplate;
            }
        }

        private void AssignContentTemplateSelector()
        {
            if (this.ContentPresenter != null)
            {
                this.ContentPresenter.ContentTemplateSelector = this.ItemTemplateSelector;
            }
        }

        private void AssignHorizontalContentAlignment()
        {
            if (this.ContentPresenter != null)
            {
                this.ContentPresenter.HorizontalAlignment = base.HorizontalContentAlignment;
            }
            TextBlock editCore = this.EditCore as TextBlock;
            if (editCore != null)
            {
                editCore.TextAlignment = base.HorizontalContentAlignment.ToTextAlignment();
            }
        }

        private void AssignImage()
        {
            if (this.ImagePresenter != null)
            {
                this.ImagePresenter.DataContext = this.SelectedItem;
            }
        }

        private void AssignProperties()
        {
            this.AssignReadOnly();
            this.AssignText();
            this.AssignContent();
            this.AssignContentTemplate();
            this.AssignContentTemplateSelector();
            this.AssignHorizontalContentAlignment();
            this.AssignVerticalContentAlignment();
            this.AssignImage();
            this.AssignTokenEditor();
        }

        private void AssignReadOnly()
        {
            if (this.EditCore != null)
            {
                TokenEditor editCore = this.EditCore as TokenEditor;
                if (editCore != null)
                {
                    editCore.IsReadOnly = this.IsReadOnly;
                    editCore.IsTextEditable = this.IsTextEditable;
                }
                else
                {
                    TextBox box = this.EditCore as TextBox;
                    if (box != null)
                    {
                        box.IsReadOnly = this.IsReadOnly;
                    }
                }
            }
        }

        private void AssignText()
        {
            if (this.EditCore != null)
            {
                TokenEditor editCore = this.EditCore as TokenEditor;
                if (editCore != null)
                {
                    editCore.SetEditValue(this.EditValue);
                }
                else
                {
                    TextBlock textBlock = this.EditCore as TextBlock;
                    if (textBlock != null)
                    {
                        TextBlockService.UpdateTextBlock(textBlock, this.DisplayText as string, this.HighlightedText, this.HighlightedTextCriteria);
                    }
                    else
                    {
                        this.EditCore.SetValue(TextBlock.TextProperty, this.DisplayText);
                    }
                }
            }
        }

        private void AssignTokenEditor()
        {
            if (this.EditCore != null)
            {
                TokenEditor editCore = this.EditCore as TokenEditor;
                BaseLookUpStyleSettings styleSettings = this.StyleSettings as BaseLookUpStyleSettings;
                if ((editCore != null) && (styleSettings != null))
                {
                    this.AssingnTokenEditorFromStyleSettings(editCore, styleSettings);
                }
            }
        }

        private void AssignVerticalContentAlignment()
        {
            if (this.ContentPresenter != null)
            {
                this.ContentPresenter.VerticalAlignment = base.VerticalContentAlignment;
            }
            TextBlock editCore = this.EditCore as TextBlock;
            if (editCore != null)
            {
                editCore.VerticalAlignment = base.VerticalContentAlignment;
            }
        }

        private void AssingnTokenEditorFromStyleSettings(TokenEditor tokenEditor, BaseLookUpStyleSettings settings)
        {
            tokenEditor.ShowTokenButtons = settings.GetShowTokenButtons();
            tokenEditor.EnableTokenWrapping = settings.GetEnableTokenWrapping();
            tokenEditor.TokenBorderTemplate = settings.GetTokenBorderTemplate();
            tokenEditor.NewTokenPosition = settings.GetNewTokenPosition();
            tokenEditor.TokenTextTrimming = settings.GetTokenTextTrimming();
            tokenEditor.TokenMaxWidth = settings.GetTokenMaxWidth();
            tokenEditor.AllowEditTokens = settings.GetAllowEditTokens();
            tokenEditor.NewTokenText = settings.GetNewTokenText();
        }

        protected virtual void DisplayTextChanged(object newValue)
        {
            this.AssignText();
        }

        protected virtual void HighlightedTextChanged(object newValue)
        {
            this.AssignText();
        }

        private static void HorizontalContentAlignmentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((EditorControlStub) d).AssignHorizontalContentAlignment();
        }

        protected virtual void ImageVisibilityChanged(Visibility oldValue, Visibility newValue)
        {
            this.AssignImage();
        }

        private static void IsReadOnlyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((EditorControlStub) d).AssignReadOnly();
        }

        private static void ItemTemplateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((EditorControlStub) d).AssignContentTemplate();
        }

        private static void ItemTemplateSelectorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((EditorControlStub) d).AssignContentTemplateSelector();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.ContentPresenter = base.GetTemplateChild("PART_ContentPresenter") as System.Windows.Controls.ContentPresenter;
            this.EditCore = base.GetTemplateChild("PART_Editor") as FrameworkElement;
            this.ImagePresenter = base.GetTemplateChild("PART_Image") as DevExpress.Xpf.Editors.Internal.ImagePresenter;
            this.AssignProperties();
        }

        private static void SelectedItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((EditorControlStub) d).AssignContent();
        }

        private static void VerticalContentAlignmentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((EditorControlStub) d).AssignVerticalContentAlignment();
        }

        public string HighlightedText
        {
            get => 
                (string) base.GetValue(HighlightedTextProperty);
            set => 
                base.SetValue(HighlightedTextProperty, value);
        }

        public DevExpress.Xpf.Editors.HighlightedTextCriteria HighlightedTextCriteria
        {
            get => 
                (DevExpress.Xpf.Editors.HighlightedTextCriteria) base.GetValue(HighlightedTextCriteriaProperty);
            set => 
                base.SetValue(HighlightedTextCriteriaProperty, value);
        }

        public bool IsTextEditable
        {
            get => 
                (bool) base.GetValue(IsTextEditableProperty);
            set => 
                base.SetValue(IsTextEditableProperty, value);
        }

        public bool IsReadOnly
        {
            get => 
                (bool) base.GetValue(IsReadOnlyProperty);
            set => 
                base.SetValue(IsReadOnlyProperty, value);
        }

        public object EditValue
        {
            get => 
                base.GetValue(EditValueProperty);
            set => 
                base.SetValue(EditValueProperty, value);
        }

        public object IsChecked
        {
            get => 
                base.GetValue(IsCheckedProperty);
            set => 
                base.SetValue(IsCheckedProperty, value);
        }

        public object SelectedItem
        {
            get => 
                base.GetValue(SelectedItemProperty);
            set => 
                base.SetValue(SelectedItemProperty, value);
        }

        public object SelectedIndex
        {
            get => 
                base.GetValue(SelectedIndexProperty);
            set => 
                base.SetValue(SelectedIndexProperty, value);
        }

        public object DisplayText
        {
            get => 
                base.GetValue(DisplayTextProperty);
            set => 
                base.SetValue(DisplayTextProperty, value);
        }

        public DataTemplate ItemTemplate
        {
            get => 
                (DataTemplate) base.GetValue(ItemTemplateProperty);
            set => 
                base.SetValue(ItemTemplateProperty, value);
        }

        public DataTemplateSelector ItemTemplateSelector
        {
            get => 
                (DataTemplateSelector) base.GetValue(ItemTemplateSelectorProperty);
            set => 
                base.SetValue(ItemTemplateSelectorProperty, value);
        }

        public BaseEditStyleSettings StyleSettings
        {
            get => 
                (BaseEditStyleSettings) base.GetValue(StyleSettingsProperty);
            set => 
                base.SetValue(StyleSettingsProperty, value);
        }

        protected System.Windows.Controls.ContentPresenter ContentPresenter { get; private set; }

        protected FrameworkElement EditCore { get; private set; }

        protected DevExpress.Xpf.Editors.Internal.ImagePresenter ImagePresenter { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly EditorControlStub.<>c <>9 = new EditorControlStub.<>c();

            internal void <.cctor>b__15_0(DependencyObject o, DependencyPropertyChangedEventArgs args)
            {
                ((EditorControlStub) o).HighlightedTextChanged(args.NewValue);
            }

            internal void <.cctor>b__15_1(DependencyObject o, DependencyPropertyChangedEventArgs args)
            {
                ((EditorControlStub) o).DisplayTextChanged(args.NewValue);
            }
        }
    }
}

