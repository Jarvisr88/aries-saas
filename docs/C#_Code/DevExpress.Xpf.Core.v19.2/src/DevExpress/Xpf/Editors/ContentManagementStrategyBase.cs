namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Windows;
    using System.Windows.Media;

    public abstract class ContentManagementStrategyBase
    {
        private WeakReference edit;

        protected ContentManagementStrategyBase(BaseEdit edit)
        {
            this.edit = new WeakReference(edit);
        }

        public abstract Size ArrangeOverride(Size arrangeSize);
        public abstract Visual GetVisualChild(int index);
        public abstract Size MeasureOverride(Size constraint);
        public abstract void OnEditorApplyTemplate();
        public abstract void UpdateButtonPanels();
        public virtual void UpdateErrorPresenter()
        {
        }

        protected BaseEdit Edit =>
            (BaseEdit) this.edit.Target;

        public abstract int VisualChildrenCount { get; }
    }
}

