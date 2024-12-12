namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class EditorControl : Control
    {
        static EditorControl()
        {
            Type forType = typeof(EditorControl);
            FrameworkElement.DataContextProperty.OverrideMetadata(forType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits, new PropertyChangedCallback(EditorControl.DataContextChanged), new CoerceValueCallback(EditorControl.CoerceDataContext)));
        }

        public EditorControl()
        {
            base.Focusable = false;
        }

        private static object CoerceDataContext(DependencyObject d, object value) => 
            (value != null) ? ((value is DependencyObject) ? value : new DependencyObject()) : value;

        private void DataContextChanged(DependencyObject dataContext)
        {
            if (this.Owner != null)
            {
                this.Owner.UpdateDataContext(dataContext);
            }
        }

        private static void DataContextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((EditorControl) d).DataContextChanged((DependencyObject) e.NewValue);
        }

        internal FrameworkElement GetEditCore() => 
            base.GetTemplateChild("PART_Editor") as FrameworkElement;

        protected override Size MeasureOverride(Size constraint) => 
            MeasurePixelSnapperHelper.MeasureOverride(base.MeasureOverride(constraint), SnapperType.Ceil);

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            if (this.ContentManagementStrategy != null)
            {
                this.ContentManagementStrategy.OnApplyContentTemplate(this);
            }
        }

        internal BaseEdit Owner { get; set; }

        private StandaloneContentManagementStrategy ContentManagementStrategy =>
            (this.Owner != null) ? ((StandaloneContentManagementStrategy) this.Owner.ContentManagementStrategy) : null;
    }
}

