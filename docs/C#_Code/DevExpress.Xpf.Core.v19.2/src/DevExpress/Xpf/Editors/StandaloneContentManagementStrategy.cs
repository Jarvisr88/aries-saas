namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Media;

    public class StandaloneContentManagementStrategy : ContentManagementStrategyBase
    {
        public StandaloneContentManagementStrategy(BaseEdit edit) : base(edit)
        {
        }

        public override Size ArrangeOverride(Size arrangeSize) => 
            base.Edit.ArrangeOverrideStandaloneMode(arrangeSize);

        public override Visual GetVisualChild(int index) => 
            base.Edit.GetVisualChildStandaloneMode(index);

        public override Size MeasureOverride(Size constraint) => 
            base.Edit.MeasureOverrideStandaloneMode(constraint);

        internal void OnApplyContentTemplate(EditorControl content)
        {
            this.SubscribeEditEvents(content);
        }

        public override void OnEditorApplyTemplate()
        {
            this.ContentCore = base.Edit.GetTemplateChildInternal<EditorControl>("PART_Content");
            if (this.ContentCore != null)
            {
                this.ContentCore.Owner = base.Edit;
                this.ContentCore.DataContext = base.Edit;
            }
            this.SubscribeEditEvents(null);
            base.Edit.ErrorPresenterStandalone = base.Edit.GetTemplateChildInternal<ContentControl>("PART_ErrorPresenter");
        }

        private void SubscribeEditEvents(EditorControl content)
        {
            content ??= this.ContentCore;
            if (content != null)
            {
                base.Edit.EditCore = content.GetEditCore();
            }
        }

        public override void UpdateButtonPanels()
        {
        }

        public override void UpdateErrorPresenter()
        {
            base.Edit.UpdateStandaloneErrorPresenter();
        }

        private EditorControl ContentCore { get; set; }

        public override int VisualChildrenCount =>
            base.Edit.VisualChildrenCountStandaloneMode;
    }
}

