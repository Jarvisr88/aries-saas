namespace DevExpress.Xpf.PdfViewer
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Bars.Internal;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.DocumentViewer;
    using DevExpress.Xpf.Editors.Flyout;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;

    public class PdfPageControl : PageControl, IVisualOwner, IInputElement, ILogicalOwner
    {
        public static readonly DependencyProperty PagesProperty;
        public static readonly DependencyProperty IsSelectedProperty;
        private FrameworkElement editor;
        private ContentControl flyoutContentControl;
        private FlyoutControl flyoutControl;

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

        static PdfPageControl()
        {
            Type ownerType = typeof(PdfPageControl);
            PagesProperty = DependencyProperty.Register("Pages", typeof(IEnumerable<PdfPageViewModel>), ownerType, new FrameworkPropertyMetadata(null));
            IsSelectedProperty = DependencyProperty.Register("IsSelected", typeof(bool), ownerType, new PropertyMetadata(false));
        }

        public PdfPageControl()
        {
            base.DefaultStyleKey = typeof(PdfPageControl);
            this.VCContainer = new VisualChildrenContainer(this, this);
            this.LCContainer = new LogicalChildrenContainer(this);
            base.RequestBringIntoView += new RequestBringIntoViewEventHandler(this.PdfPageControl_RequestBringIntoView);
            FlyoutControl control1 = new FlyoutControl();
            control1.AllowMoveAnimation = false;
            control1.AnimationDuration = TimeSpan.Zero;
            control1.HorizontalAlignment = HorizontalAlignment.Center;
            this.flyoutControl = control1;
            ContentControl control2 = new ContentControl();
            control2.IsTabStop = false;
            this.flyoutContentControl = control2;
            this.flyoutContentControl.Content = this.flyoutControl;
            this.flyoutControl.PlacementTarget = this.flyoutContentControl;
            this.VCContainer.AddChild(this.flyoutContentControl);
            this.LCContainer.AddLogicalChild(this.flyoutContentControl);
        }

        public void AddEditor(FrameworkElement editor, Func<Rect> rectHandler, double angle)
        {
            this.editor = editor;
            this.VCContainer.AddChild(editor);
            this.LCContainer.AddLogicalChild(editor);
            editor.Tag = new Tuple<Func<Rect>, double>(rectHandler, angle);
            base.InvalidateMeasure();
        }

        protected override Size ArrangeOverride(Size arrangeBounds)
        {
            if (this.editor != null)
            {
                Tuple<Func<Rect>, double> tuple = (Tuple<Func<Rect>, double>) this.editor.Tag;
                double angle = tuple.Item2;
                this.editor.RenderTransform = this.CalcRenderTransform(angle, tuple.Item1());
            }
            Func<Rect> tag = this.flyoutContentControl.Tag as Func<Rect>;
            if (tag != null)
            {
                this.flyoutContentControl.Arrange(tag());
            }
            return base.ArrangeOverride(arrangeBounds);
        }

        private TransformGroup CalcRenderTransform(double angle, Rect rect)
        {
            Size size = ((angle % 180.0) == 0.0) ? rect.Size : new Size(rect.Size.Height, rect.Size.Width);
            this.editor.Arrange(new Rect(rect.TopLeft, size));
            this.editor.RenderTransform = new RotateTransform(angle);
            TransformGroup group = new TransformGroup();
            RotateTransform transform = new RotateTransform(angle);
            group.Children.Add(transform);
            Point point = this.CalcTranslatePoint(size, angle);
            TranslateTransform transform2 = new TranslateTransform(point.X, point.Y);
            group.Children.Add(transform2);
            return group;
        }

        private Point CalcTranslatePoint(Size arrangeSize, double angle) => 
            !angle.AreClose(0.0) ? (!angle.AreClose(90.0) ? (!angle.AreClose(180.0) ? (!angle.AreClose(270.0) ? new Point(0.0, 0.0) : new Point(0.0, arrangeSize.Width)) : new Point(arrangeSize.Width, arrangeSize.Height)) : new Point(arrangeSize.Height, 0.0)) : new Point(0.0, 0.0);

        private SuperTipControl CreateSuperTipControl(string title, string text)
        {
            SuperTip superTip = new SuperTip();
            if (!string.IsNullOrEmpty(title))
            {
                SuperTipHeaderItem item1 = new SuperTipHeaderItem();
                item1.Content = title;
                SuperTipHeaderItem item2 = item1;
                superTip.Items.Add(item2);
            }
            SuperTipItem item3 = new SuperTipItem();
            item3.Content = text;
            SuperTipItem item = item3;
            superTip.Items.Add(item);
            return new SuperTipControl(superTip);
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
            new PdfPageControlItem();

        protected override Visual GetVisualChild(int index) => 
            (index < base.VisualChildrenCount) ? base.GetVisualChild(index) : this.VCContainer.GetVisualChild(index - base.VisualChildrenCount);

        public bool HasEditor() => 
            this.editor != null;

        public void HidePopup()
        {
            this.flyoutControl.IsOpen = false;
        }

        protected override bool IsItemItsOwnContainerOverride(object item) => 
            item is PdfPageControlItem;

        protected override Size MeasureOverride(Size constraint)
        {
            if (this.editor != null)
            {
                Tuple<Func<Rect>, double> tuple = (Tuple<Func<Rect>, double>) this.editor.Tag;
                Rect rect = tuple.Item1();
                Size availableSize = ((tuple.Item2 % 180.0) == 0.0) ? rect.Size : new Size(rect.Size.Height, rect.Size.Width);
                this.editor.Measure(availableSize);
            }
            Func<Rect> tag = this.flyoutContentControl.Tag as Func<Rect>;
            if (tag != null)
            {
                this.flyoutContentControl.Measure(tag().Size);
            }
            return base.MeasureOverride(constraint);
        }

        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);
            if (this.Pages != null)
            {
                this.Pages.ForEach<PdfPageViewModel>(delegate (PdfPageViewModel page) {
                    Action<IEnumerable<PdfElement>> <>9__1;
                    Action<IEnumerable<PdfElement>> action = <>9__1;
                    if (<>9__1 == null)
                    {
                        Action<IEnumerable<PdfElement>> local1 = <>9__1;
                        action = <>9__1 = delegate (IEnumerable<PdfElement> x) {
                            Action<PdfElement> <>9__2;
                            Action<PdfElement> action2 = <>9__2;
                            if (<>9__2 == null)
                            {
                                Action<PdfElement> local1 = <>9__2;
                                action2 = <>9__2 = element => element.Render(dc, this.RenderSize);
                            }
                            x.ForEach<PdfElement>(action2);
                        };
                    }
                    page.RenderContent.Do<IEnumerable<PdfElement>>(action);
                });
            }
        }

        private void PdfPageControl_RequestBringIntoView(object sender, RequestBringIntoViewEventArgs e)
        {
            e.Handled = true;
        }

        public void RemoveEditor()
        {
            if (this.editor != null)
            {
                this.LCContainer.RemoveLogicalChild(this.editor);
                this.VCContainer.RemoveChild(this.editor);
            }
        }

        public void ShowPopup(string title, string text, Func<Rect> controlRectHandler)
        {
            this.flyoutContentControl.Tag = controlRectHandler;
            base.InvalidateMeasure();
            if (controlRectHandler().IsInside(Mouse.GetPosition(this)))
            {
                SuperTipControl control = this.CreateSuperTipControl(title, text);
                this.flyoutControl.Content = control;
                this.flyoutControl.IsOpen = true;
            }
        }

        public IEnumerable<PdfPageViewModel> Pages
        {
            get => 
                (IEnumerable<PdfPageViewModel>) base.GetValue(PagesProperty);
            set => 
                base.SetValue(PagesProperty, value);
        }

        public bool IsSelected
        {
            get => 
                (bool) base.GetValue(IsSelectedProperty);
            set => 
                base.SetValue(IsSelectedProperty, value);
        }

        private VisualChildrenContainer VCContainer { get; set; }

        private LogicalChildrenContainer LCContainer { get; set; }

        protected override int VisualChildrenCount =>
            base.VisualChildrenCount + this.VCContainer.VisualChildrenCount;

        protected override IEnumerator LogicalChildren
        {
            get
            {
                IEnumerator[] args = new IEnumerator[] { this.VCContainer.GetEnumerator(), base.LogicalChildren, this.LCContainer.GetEnumerator() };
                return new MergedEnumerator(args);
            }
        }

        bool ILogicalOwner.IsLoaded =>
            base.IsLoaded;

        double ILogicalOwner.ActualWidth =>
            base.ActualWidth;

        double ILogicalOwner.ActualHeight =>
            base.ActualHeight;
    }
}

