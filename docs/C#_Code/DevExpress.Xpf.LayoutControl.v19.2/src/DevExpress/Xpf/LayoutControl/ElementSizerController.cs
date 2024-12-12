namespace DevExpress.Xpf.LayoutControl
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Interop;

    public class ElementSizerController : ControlControllerBase
    {
        private bool _IsSizing;

        public event EventHandler ElementSizeChanging;

        public event EventHandler IsSizingChanged;

        public ElementSizerController(DevExpress.Xpf.LayoutControl.IElementSizer control) : base(control)
        {
        }

        protected override void DragAndDrop(Point p)
        {
            base.DragAndDrop(p);
            this.ElementSize = this.OriginalElementSize + this.GetElementSizeChange(base.Control.MapPoint(p, null));
        }

        protected override void EndDragAndDrop(bool accept)
        {
            base.EndDragAndDrop(accept);
            if (!accept)
            {
                this.ElementSize = this.OriginalIsElementAutoSize ? double.NaN : this.OriginalElementSize;
            }
            this.IsSizing = false;
        }

        protected double GetElementSizeChange(Point absoluteDragPoint)
        {
            double x;
            if (this.Orientation == System.Windows.Controls.Orientation.Horizontal)
            {
                x = absoluteDragPoint.Y - this.AbsoluteStartDragPoint.Y;
            }
            else
            {
                x = absoluteDragPoint.X - this.AbsoluteStartDragPoint.X;
                if (base.Control.FlowDirection == FlowDirection.RightToLeft)
                {
                    x = -x;
                }
            }
            if ((this.IElementSizer.Side == DevExpress.Xpf.Core.Side.Left) || (this.IElementSizer.Side == DevExpress.Xpf.Core.Side.Top))
            {
                x = -x;
            }
            if (!BrowserInteropHelper.IsBrowserHosted)
            {
                x = PresentationSource.FromVisual(base.Control).CompositionTarget.TransformFromDevice.Transform(new Point(x, 0.0)).X;
            }
            return x;
        }

        protected virtual bool IsUsingSizingStep() => 
            this.IElementSizer.UseSizingStep && !Keyboard2.IsControlPressed;

        protected override void OnDragAndDropKeyDown(DXKeyEventArgs e)
        {
            base.OnDragAndDropKeyDown(e);
            if ((e.Key == Key.LeftCtrl) || (e.Key == Key.RightCtrl))
            {
                this.UpdateElementSize();
            }
        }

        protected override void OnDragAndDropKeyUp(DXKeyEventArgs e)
        {
            base.OnDragAndDropKeyUp(e);
            if ((e.Key == Key.LeftCtrl) || (e.Key == Key.RightCtrl))
            {
                this.UpdateElementSize();
            }
        }

        protected virtual void OnElementSizeChanging()
        {
            if (this.ElementSizeChanging != null)
            {
                this.ElementSizeChanging(base.Control, EventArgs.Empty);
            }
        }

        protected virtual void OnIsSizingChanged()
        {
            if (this.IsSizingChanged != null)
            {
                this.IsSizingChanged(base.Control, EventArgs.Empty);
            }
        }

        protected override void OnMouseDoubleClick(DXMouseButtonEventArgs e)
        {
            base.OnMouseDoubleClick(e);
            this.ElementSize = (!this.IElementSizer.CollapseOnDoubleClick || !this.IsElementAutoSize) ? double.NaN : 0.0;
            e.Handled = true;
        }

        protected override void StartDragAndDrop(Point p)
        {
            this.AbsoluteStartDragPoint = base.Control.MapPoint(base.StartDragPoint, null);
            this.OriginalElementSize = this.ElementSize;
            this.OriginalIsElementAutoSize = this.IsElementAutoSize;
            this.LastElementSize = double.NaN;
            this.IsSizing = true;
            base.StartDragAndDrop(p);
        }

        protected void UpdateElementSize()
        {
            if (this.IElementSizer.UseSizingStep && !double.IsNaN(this.LastElementSize))
            {
                this.ElementSize = this.LastElementSize;
            }
        }

        protected override bool WantsDragAndDrop(Point p, out DragAndDropController controller)
        {
            controller = null;
            return (this.IElementSizer.Element != null);
        }

        public double ElementSize
        {
            get
            {
                if (!this.IsElementAutoSize)
                {
                    return ((this.Orientation == System.Windows.Controls.Orientation.Horizontal) ? this.IElementSizer.Element.GetRealHeight() : this.IElementSizer.Element.GetRealWidth());
                }
                Size visualSize = this.IElementSizer.Element.GetVisualSize();
                return ((this.Orientation == System.Windows.Controls.Orientation.Horizontal) ? visualSize.Height : visualSize.Width);
            }
            set
            {
                double elementSize = this.ElementSize;
                value = Math.Max(0.0, value);
                this.LastElementSize = value;
                if (this.IsUsingSizingStep())
                {
                    value = Math.Round((double) (value / ((double) ElementSizer.SizingStep))) * ElementSizer.SizingStep;
                }
                if (this.Orientation == System.Windows.Controls.Orientation.Horizontal)
                {
                    this.IElementSizer.Element.Height = value;
                }
                else
                {
                    this.IElementSizer.Element.Width = value;
                }
                if (this.ElementSize != elementSize)
                {
                    this.OnElementSizeChanging();
                }
            }
        }

        public DevExpress.Xpf.LayoutControl.IElementSizer IElementSizer =>
            base.IControl as DevExpress.Xpf.LayoutControl.IElementSizer;

        public bool IsSizing
        {
            get => 
                this._IsSizing;
            protected set
            {
                if (this.IsSizing != value)
                {
                    this._IsSizing = value;
                    this.OnIsSizingChanged();
                }
            }
        }

        protected bool IsElementAutoSize =>
            double.IsNaN((this.Orientation == System.Windows.Controls.Orientation.Horizontal) ? this.IElementSizer.Element.Height : this.IElementSizer.Element.Width);

        protected double LastElementSize { get; private set; }

        protected System.Windows.Controls.Orientation Orientation =>
            this.IElementSizer.Side.GetOrientation();

        protected Point AbsoluteStartDragPoint { get; private set; }

        protected override bool IsImmediateDragAndDrop =>
            true;

        protected double OriginalElementSize { get; private set; }

        protected bool OriginalIsElementAutoSize { get; private set; }
    }
}

