namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Shapes;

    internal class SelectedItemValueRenderer
    {
        private UIElement element;
        private readonly LookUpEditBase edit;
        private readonly Locker renderLocker = new Locker();

        public SelectedItemValueRenderer(LookUpEditBase edit)
        {
            this.edit = edit;
        }

        private FlowDirection GetElementFlowDirection()
        {
            FlowDirection direction = (FlowDirection) this.Element.GetValue(FrameworkElement.FlowDirectionProperty);
            DependencyObject parent = VisualTreeHelper.GetParent(this.element);
            return ((parent == null) ? direction : ((FlowDirection) parent.GetValue(FrameworkElement.FlowDirectionProperty)));
        }

        protected virtual void OnElementLayoutUpdated(object sender, EventArgs e)
        {
            Rectangle selectedItemValue = (Rectangle) this.edit.SelectedItemValue;
            if (selectedItemValue != null)
            {
                selectedItemValue.Width = this.Element.RenderSize.Width;
                selectedItemValue.Height = this.Element.RenderSize.Height;
                VisualBrush fill = (VisualBrush) selectedItemValue.Fill;
                fill.Viewbox = new Rect(this.Element.RenderSize);
                fill.Viewport = new Rect(this.Element.RenderSize);
            }
        }

        public void Render()
        {
            this.renderLocker.DoLockedActionIfNotLocked(new Action(this.RenderCore));
        }

        private void RenderCore()
        {
            if (this.Element != null)
            {
                Transform transform = null;
                if (this.GetElementFlowDirection() == FlowDirection.RightToLeft)
                {
                    MatrixTransform transform1 = new MatrixTransform();
                    transform1.Matrix = new Matrix(-1.0, 0.0, 0.0, 1.0, this.Element.RenderSize.Width, 0.0);
                    transform = transform1;
                }
                VisualBrush brush1 = new VisualBrush(this.Element);
                brush1.Stretch = Stretch.None;
                brush1.ViewboxUnits = BrushMappingMode.Absolute;
                brush1.ViewportUnits = BrushMappingMode.Absolute;
                VisualBrush brush = brush1;
                brush.Viewbox = new Rect(this.Element.RenderSize);
                brush.Viewport = new Rect(this.Element.RenderSize);
                brush.Transform = transform;
                Rectangle rectangle1 = new Rectangle();
                rectangle1.Fill = brush;
                rectangle1.Width = this.Element.RenderSize.Width;
                rectangle1.Height = this.Element.RenderSize.Height;
                this.edit.SelectedItemValue = rectangle1;
            }
        }

        public UIElement Element
        {
            get => 
                this.element;
            set
            {
                if (!ReferenceEquals(this.Element, value))
                {
                    if (this.Element != null)
                    {
                        this.Element.LayoutUpdated -= new EventHandler(this.OnElementLayoutUpdated);
                    }
                    this.element = value;
                    if (this.Element != null)
                    {
                        this.Element.LayoutUpdated += new EventHandler(this.OnElementLayoutUpdated);
                    }
                }
            }
        }
    }
}

