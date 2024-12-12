namespace DevExpress.Xpf.Printing
{
    using DevExpress.Xpf.Bars.Internal;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.DocumentViewer;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Media;

    public class PreviewPageControl : PageControl, IVisualOwner, IInputElement, ILogicalOwner
    {
        public static readonly DependencyProperty PagesProperty = DependencyPropertyManager.Register("Pages", typeof(IEnumerable<PageViewModel>), typeof(PreviewPageControl), new FrameworkPropertyMetadata(null));
        private FrameworkElement activeEditor;

        event RoutedEventHandler ILogicalOwner.Loaded
        {
            add
            {
                base.Loaded += value;
            }
            remove
            {
                base.Loaded -= value;
            }
        }

        public PreviewPageControl()
        {
            this.VisualContainer = new VisualChildrenContainer(this, this);
            this.LogicalContainer = new LogicalChildrenContainer(this);
            base.Focusable = false;
        }

        public void AddEditor(FrameworkElement editor, Func<Rect> rectHandler, double angle)
        {
            this.activeEditor = editor;
            this.VisualContainer.AddChild(editor);
            this.LogicalContainer.AddLogicalChild(editor);
            editor.Tag = rectHandler;
            base.InvalidateMeasure();
        }

        protected override Size ArrangeOverride(Size arrangeBounds)
        {
            if (this.activeEditor != null)
            {
                Rect finalRect = ((Func<Rect>) this.activeEditor.Tag)();
                this.activeEditor.Arrange(finalRect);
            }
            return base.ArrangeOverride(arrangeBounds);
        }

        void ILogicalOwner.AddChild(object child)
        {
            base.AddLogicalChild(child);
        }

        void ILogicalOwner.RemoveChild(object child)
        {
            base.RemoveLogicalChild(child);
        }

        void IVisualOwner.AddChild(Visual child)
        {
            base.AddVisualChild(child);
        }

        void IVisualOwner.RemoveChild(Visual child)
        {
            base.RemoveVisualChild(child);
        }

        protected override DependencyObject GetContainerForItemOverride() => 
            new PreviewPageControlItem();

        protected override Visual GetVisualChild(int index) => 
            (index < base.VisualChildrenCount) ? base.GetVisualChild(index) : this.VisualContainer.GetVisualChild(index - base.VisualChildrenCount);

        protected override bool IsItemItsOwnContainerOverride(object item) => 
            item is PreviewPageControlItem;

        protected override Size MeasureOverride(Size constraint)
        {
            if (this.activeEditor != null)
            {
                Size availableSize = ((Func<Rect>) this.activeEditor.Tag)().Size;
                this.activeEditor.Measure(availableSize);
            }
            return base.MeasureOverride(constraint);
        }

        public void RemoveEditor()
        {
            if (this.activeEditor != null)
            {
                this.LogicalContainer.RemoveLogicalChild(this.activeEditor);
                this.VisualContainer.RemoveChild(this.activeEditor);
            }
        }

        public IEnumerable<PageViewModel> Pages
        {
            get => 
                (IEnumerable<PageViewModel>) base.GetValue(PagesProperty);
            set => 
                base.SetValue(PagesProperty, value);
        }

        protected override int VisualChildrenCount =>
            base.VisualChildrenCount + this.VisualContainer.VisualChildrenCount;

        protected override IEnumerator LogicalChildren
        {
            get
            {
                IEnumerator[] args = new IEnumerator[] { this.VisualContainer.GetEnumerator(), base.LogicalChildren, this.LogicalContainer.GetEnumerator() };
                return new MergedEnumerator(args);
            }
        }

        private VisualChildrenContainer VisualContainer { get; set; }

        private LogicalChildrenContainer LogicalContainer { get; set; }

        bool ILogicalOwner.IsLoaded =>
            base.IsLoaded;

        double ILogicalOwner.ActualWidth =>
            base.ActualWidth;

        double ILogicalOwner.ActualHeight =>
            base.ActualHeight;
    }
}

