namespace DevExpress.Xpf.LayoutControl
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    [TemplatePart(Name="DeleteElement", Type=typeof(Button))]
    public class LayoutControlAvailableListBoxItem : ListBoxItem
    {
        public static readonly DependencyProperty DeleteElementVisibilityProperty = DependencyProperty.Register("DeleteElementVisibility", typeof(Visibility), typeof(LayoutControlAvailableListBoxItem), new PropertyMetadata(Visibility.Collapsed));
        private Point? _StartDragPoint;
        private const string DeleteElementName = "DeleteElement";

        public event EventHandler DeleteElementClick;

        public LayoutControlAvailableListBoxItem(LayoutControlAvailableItemsControl owner)
        {
            this.Owner = owner;
        }

        public override void OnApplyTemplate()
        {
            if (this.DeleteElement != null)
            {
                this.DeleteElement.Click -= new RoutedEventHandler(this.OnDeleteElementClick);
            }
            base.OnApplyTemplate();
            this.DeleteElement = base.GetTemplateChild("DeleteElement") as Button;
            if (this.DeleteElement != null)
            {
                this.DeleteElement.Click += new RoutedEventHandler(this.OnDeleteElementClick);
            }
        }

        protected virtual void OnDeleteElementClick()
        {
            if (this.DeleteElementClick != null)
            {
                this.DeleteElementClick(this, EventArgs.Empty);
            }
        }

        private void OnDeleteElementClick(object sender, RoutedEventArgs e)
        {
            this.OnDeleteElementClick();
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            base.OnMouseLeave(e);
            this.StartDragPoint = null;
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            this.StartDragPoint = new Point?(e.GetPosition(this));
            e.Handled = true;
            base.OnMouseLeftButtonDown(e);
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);
            this.StartDragPoint = null;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            Point? startDragPoint = this.StartDragPoint;
            if (startDragPoint != null)
            {
                startDragPoint = null;
                this.StartDragPoint = startDragPoint;
                VisualStateManager.GoToState(this, "Normal", false);
                this.StartDragAndDrop(e);
            }
        }

        protected void StartDragAndDrop(MouseEventArgs arguments)
        {
            if (this.Owner != null)
            {
                this.Owner.OnStartItemDragAndDrop(this, arguments);
            }
        }

        public Visibility DeleteElementVisibility
        {
            get => 
                (Visibility) base.GetValue(DeleteElementVisibilityProperty);
            set => 
                base.SetValue(DeleteElementVisibilityProperty, value);
        }

        protected Button DeleteElement { get; private set; }

        protected LayoutControlAvailableItemsControl Owner { get; private set; }

        protected Point? StartDragPoint
        {
            get => 
                this._StartDragPoint;
            private set
            {
                Point? startDragPoint = this.StartDragPoint;
                Point? nullable2 = value;
                if (!(((startDragPoint != null) == (nullable2 != null)) ? ((startDragPoint != null) ? (startDragPoint.GetValueOrDefault() == nullable2.GetValueOrDefault()) : true) : false))
                {
                    this._StartDragPoint = value;
                    if (this.StartDragPoint != null)
                    {
                        base.CaptureMouse();
                    }
                    else
                    {
                        base.ReleaseMouseCapture();
                    }
                }
            }
        }

        internal FrameworkElement Item { get; set; }
    }
}

