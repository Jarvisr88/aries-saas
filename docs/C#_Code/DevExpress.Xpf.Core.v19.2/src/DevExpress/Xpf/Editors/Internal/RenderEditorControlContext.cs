namespace DevExpress.Xpf.Editors.Internal
{
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors;
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public class RenderEditorControlContext : RenderRealControlContext
    {
        public RenderEditorControlContext(RenderRealControl factory) : base(factory)
        {
        }

        private EditorControlStub EditorControl =>
            base.Control as EditorControlStub;

        public bool IsReadOnly
        {
            get => 
                this.EditorControl.IsReadOnly;
            set => 
                this.EditorControl.IsReadOnly = value;
        }

        public object EditValue
        {
            get => 
                this.EditorControl.EditValue;
            set => 
                this.EditorControl.EditValue = value;
        }

        public object IsChecked
        {
            get => 
                this.EditorControl.IsChecked;
            set => 
                this.EditorControl.IsChecked = value;
        }

        public object SelectedItem
        {
            get => 
                this.EditorControl.SelectedItem;
            set => 
                this.EditorControl.SelectedItem = value;
        }

        public object SelectedIndex
        {
            get => 
                this.EditorControl.SelectedIndex;
            set => 
                this.EditorControl.SelectedIndex = value;
        }

        public string HighlightedText
        {
            get => 
                this.EditorControl.HighlightedText;
            set => 
                this.EditorControl.HighlightedText = value;
        }

        public DevExpress.Xpf.Editors.HighlightedTextCriteria HighlightedTextCriteria
        {
            get => 
                this.EditorControl.HighlightedTextCriteria;
            set => 
                this.EditorControl.HighlightedTextCriteria = value;
        }

        public object DisplayText
        {
            get => 
                this.EditorControl.DisplayText;
            set => 
                this.EditorControl.DisplayText = value;
        }

        public object RealDataContext
        {
            get => 
                this.EditorControl.DataContext;
            set => 
                this.EditorControl.DataContext = value;
        }

        public DataTemplate ItemTemplate
        {
            get => 
                this.EditorControl.ItemTemplate;
            set => 
                this.EditorControl.ItemTemplate = value;
        }

        public DataTemplateSelector ItemTemplateSelector
        {
            get => 
                this.EditorControl.ItemTemplateSelector;
            set => 
                this.EditorControl.ItemTemplateSelector = value;
        }

        public bool IsTextEditable
        {
            get => 
                this.EditorControl.IsTextEditable;
            set => 
                this.EditorControl.IsTextEditable = value;
        }

        public BaseEditStyleSettings StyleSettings
        {
            get => 
                this.EditorControl.StyleSettings;
            set => 
                this.EditorControl.StyleSettings = value;
        }
    }
}

