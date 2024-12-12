namespace ActiproSoftware.WinUICore
{
    using ActiproSoftware.ComponentModel;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows.Forms;

    [DesignTimeVisible(false), ToolboxItem(false)]
    public class UIElement : LogicalTreeNodeBase, IDisposable, IUIElement, ILogicalTreeNode, IInputElement
    {
        private Rectangle #Bo;
        private System.Drawing.Size #XK;
        private bool #w5j;
        private bool #7se;
        private bool #x5j;
        private bool #y5j;
        private bool #8se;
        private bool #zte;
        private bool #z5j = true;
        private bool #A5j = true;
        private IUIElement #bte;
        private Rectangle #B5j;
        private System.Drawing.Size #C5j;
        private Visibility #Ucc;
        private Point #D5j;

        [Category("Behavior"), Description("Occurs when the object is clicked.")]
        public event MouseEventHandler Click;

        [Category("Behavior"), Description("Occurs when the object is double-clicked.")]
        public event MouseEventHandler DoubleClick;

        [Category("Mouse"), Description("Occurs when the mouse pointer is over the object and a mouse button is pressed.")]
        public event MouseEventHandler MouseDown;

        [Category("Mouse"), Description("Occurs when the mouse pointer enters the object.")]
        public event MouseEventHandler MouseEnter;

        [Category("Mouse"), Description("Occurs when the mouse pointer hovers over the object.")]
        public event MouseEventHandler MouseHover;

        [Category("Mouse"), Description("Occurs when the mouse pointer leaves the object.")]
        public event MouseEventHandler MouseLeave;

        [Category("Mouse"), Description("Occurs when the mouse pointer is over the object and a mouse button is released.")]
        public event MouseEventHandler MouseMove;

        [Category("Mouse"), Description("Occurs when the mouse pointer is over the object and a mouse button is released.")]
        public event MouseEventHandler MouseUp;

        [Category("Mouse"), Description("Occurs when the mouse wheel moves while the object has focus.")]
        public event MouseEventHandler MouseWheel;

        [Category("Layout"), Description("Occurs when the object is resized.")]
        public event EventHandler Resize;

        private void #cxe(MouseEventArgs #yhb)
        {
            this.OnClick(#yhb);
        }

        private void #dxe(MouseEventArgs #yhb)
        {
            this.OnDoubleClick(#yhb);
        }

        private void #E5j()
        {
            if (!this.#w5j && (!this.#y5j && this.#8se))
            {
                this.#K5j(false);
            }
        }

        private void #exe(MouseEventArgs #yhb)
        {
            this.OnMouseDown(#yhb);
        }

        private void #fxe(MouseEventArgs #yhb)
        {
            this.OnMouseEnter(#yhb);
        }

        private void #G5j(Rectangle #cdd, bool #H5j)
        {
            this.#D5j = #cdd.Location;
            System.Drawing.Size size = #H5j ? this.ArrangeOverride(#cdd.Size) : #cdd.Size;
            Point point = #M5j(this);
            this.#Bo = new Rectangle(point.X + this.#D5j.X, point.Y + this.#D5j.Y, size.Width, size.Height);
            if (this.#Bo.Size != size)
            {
                this.OnResize(new EventArgs());
            }
        }

        private void #gxe(MouseEventArgs #yhb)
        {
            this.OnMouseHover(#yhb);
        }

        private void #hxe(MouseEventArgs #yhb)
        {
            this.OnMouseLeave(#yhb);
        }

        private void #I5j(bool #J5j)
        {
            if (!this.#7se)
            {
                if (#J5j)
                {
                    IUIControl control = ((ILogicalTreeNode) this).FindAncestor(typeof(IUIControl)) as IUIControl;
                    if ((control != null) && control.IsPaintValid)
                    {
                        control.AddToInvalidatedRegion(this.#Bo);
                    }
                }
            }
            else if (!this.#w5j)
            {
                if (!this.#z5j)
                {
                    IUIElement parent = this.Parent as IUIElement;
                    if (parent == null)
                    {
                        if (#J5j)
                        {
                            this.Invalidate();
                        }
                    }
                    else if (#J5j)
                    {
                        parent.InvalidateArrange();
                    }
                    else
                    {
                        parent.Invalidate(InvalidationLevels.Element, InvalidationTypes.Arrange);
                    }
                }
                this.#7se = false;
            }
        }

        private void #ixe(MouseEventArgs #yhb)
        {
            this.OnMouseMove(#yhb);
        }

        private void #jxe(MouseEventArgs #yhb)
        {
            this.OnMouseUp(#yhb);
        }

        private void #K5j(bool #J5j)
        {
            if (!this.#8se)
            {
                if (#J5j)
                {
                    IUIControl control = ((ILogicalTreeNode) this).FindAncestor(typeof(IUIControl)) as IUIControl;
                    if ((control != null) && control.IsPaintValid)
                    {
                        control.AddToInvalidatedRegion(this.#Bo);
                    }
                }
            }
            else if (!this.#y5j)
            {
                if (!this.#A5j)
                {
                    IUIElement parent = this.Parent as IUIElement;
                    if (parent == null)
                    {
                        if (#J5j && !this.#w5j)
                        {
                            this.Invalidate();
                        }
                    }
                    else if (#J5j)
                    {
                        parent.InvalidateMeasure();
                    }
                    else
                    {
                        parent.Invalidate(InvalidationLevels.Element, InvalidationTypes.Measure);
                    }
                }
                this.#8se = false;
            }
        }

        private void #kxe(MouseEventArgs #yhb)
        {
            this.OnMouseWheel(#yhb);
        }

        internal static unsafe Point #M5j(IUIElement #uK)
        {
            Point point = new Point();
            if (#uK != null)
            {
                for (IUIElement element = #uK.Parent as IUIElement; (element != null) && !(element is Control); element = element.Parent as IUIElement)
                {
                    Point* pointPtr1 = &point;
                    pointPtr1.X += element.VisualOffset.X;
                    Point* pointPtr2 = &point;
                    pointPtr2.Y += element.VisualOffset.Y;
                }
            }
            return point;
        }

        private PointHitTestResult #Pwe(PointHitTestParameters #LZf)
        {
            if (this.Children != null)
            {
                for (int i = this.Children.Count - 1; i >= 0; i--)
                {
                    IUIElement element = this.Children[i] as IUIElement;
                    if ((element.Visibility == Visibility.Visible) && element.ContainsLocation(#LZf.Point))
                    {
                        return new PointHitTestResult(element, #LZf.Point);
                    }
                }
            }
            return new PointHitTestResult(null, #LZf.Point);
        }

        private UIElementDrawState #q8d() => 
            UIElementDrawState.None;

        private PointHitTestResult #Qwe(PointHitTestParameters #LZf)
        {
            PointHitTestResult result = ((IUIElement) this).HitTest(#LZf);
            if (result.Element == null)
            {
                return result;
            }
            PointHitTestResult result2 = result.Element.HitTestRecursive(#LZf);
            return ((result2.Element != null) ? result2 : result);
        }

        private void #Swe()
        {
            this.#zte = false;
            if (this.Children != null)
            {
                for (int i = this.Children.Count - 1; i >= 0; i--)
                {
                    (this.Children[i] as IUIElement).NotifyMouseLeaveEvent();
                }
            }
            if (this.InvalidateOnMouseEvents)
            {
                this.Invalidate();
            }
        }

        private void #Xwe(IUIElement #irb, MouseEventArgs #yhb)
        {
            if (!ReferenceEquals(this.#bte, #irb))
            {
                if ((this.#bte != null) && (this.#bte is IInputElement))
                {
                    ((IInputElement) this.#bte).RaiseMouseLeaveEvent(#yhb);
                }
                this.#bte = #irb;
                if ((this.#bte != null) && (this.#bte is IInputElement))
                {
                    ((IInputElement) this.#bte).RaiseMouseEnterEvent(#yhb);
                }
            }
        }

        protected bool AddPendingGraphicsInversion(Rectangle bounds)
        {
            IUIControl control = (IUIControl) ((ILogicalTreeNode) this).FindAncestor(System.Type.GetTypeFromHandle(typeof(IUIControl).TypeHandle));
            return ((control != null) && control.AddPendingGraphicsInversion(bounds));
        }

        protected internal bool AddPendingScrollOperation(Rectangle bounds, int xAmount, int yAmount)
        {
            IUIControl control = (IUIControl) ((ILogicalTreeNode) this).FindAncestor(System.Type.GetTypeFromHandle(typeof(IUIControl).TypeHandle));
            return ((control != null) && control.AddPendingScrollOperation(bounds, xAmount, yAmount));
        }

        protected bool AddPendingScrollOperation(Rectangle bounds, Orientation orientation, int amount)
        {
            IUIControl control = (IUIControl) ((ILogicalTreeNode) this).FindAncestor(System.Type.GetTypeFromHandle(typeof(IUIControl).TypeHandle));
            return ((control != null) && control.AddPendingScrollOperation(bounds, orientation, amount));
        }

        public void Arrange(Rectangle finalRect)
        {
            if (!this.#8se || this.#A5j)
            {
                try
                {
                    this.#x5j = true;
                    using (Graphics graphics = this.CreateGraphics())
                    {
                        if (graphics != null)
                        {
                            if (this.#A5j)
                            {
                                this.Measure(graphics, finalRect.Size);
                            }
                            else
                            {
                                this.Measure(graphics, this.#C5j);
                            }
                        }
                    }
                }
                finally
                {
                    this.#x5j = false;
                }
            }
            if ((this.#7se && !this.#z5j) && !(this.#B5j != finalRect))
            {
                try
                {
                    this.#w5j = true;
                    this.#G5j(new Rectangle(finalRect.Location, this.Bounds.Size), false);
                }
                finally
                {
                    this.#w5j = false;
                }
            }
            else
            {
                try
                {
                    this.#z5j = false;
                    this.#w5j = true;
                    this.#G5j(finalRect, true);
                }
                finally
                {
                    this.#w5j = false;
                    this.#B5j = finalRect;
                    this.#7se = true;
                }
                this.PostArrangeOverride();
            }
        }

        protected virtual System.Drawing.Size ArrangeOverride(System.Drawing.Size finalSize)
        {
            if (this.Children != null)
            {
                foreach (IUIElement element in this.Children)
                {
                    System.Drawing.Size desiredSize = element.DesiredSize;
                    element.Arrange(new Rectangle(this.#Bo.Left, this.#Bo.Top, element.DesiredSize.Width, desiredSize.Height));
                }
            }
            return finalSize;
        }

        public void CaptureMouse()
        {
            IUIControl control = ((ILogicalTreeNode) this).FindAncestor(System.Type.GetTypeFromHandle(typeof(IUIControl).TypeHandle)) as IUIControl;
            if (control != null)
            {
                control.MouseCaptureElement = this;
                control.Capture = true;
            }
        }

        public virtual bool ContainsLocation(Point location)
        {
            Rectangle bounds = this.Bounds;
            return bounds.Contains(location);
        }

        public Graphics CreateGraphics()
        {
            Control control = ((ILogicalTreeNode) this).FindAncestor(System.Type.GetTypeFromHandle(typeof(Control).TypeHandle)) as Control;
            return control?.CreateGraphics();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if ((this.Children != null) && (this.Children is IDisposable))
                {
                    ((IDisposable) this.Children).Dispose();
                }
                this.Parent = null;
            }
            base.Dispose(disposing);
        }

        public virtual Cursor GetCursor(Point point) => 
            null;

        public void Invalidate()
        {
            this.Invalidate(this.#Bo);
        }

        public void Invalidate(Rectangle rect)
        {
            Control control = ((ILogicalTreeNode) this).FindAncestor(System.Type.GetTypeFromHandle(typeof(Control).TypeHandle)) as Control;
            if (control != null)
            {
                if (control is IUIControl)
                {
                    ((IUIControl) control).AddToInvalidatedRegion(rect);
                }
                else
                {
                    control.Invalidate(rect);
                }
            }
        }

        public void Invalidate(InvalidationLevels levels, InvalidationTypes types)
        {
            IUIElement parent = this.Parent as IUIElement;
            if ((parent == null) && ((levels & InvalidationLevels.All) == InvalidationLevels.All))
            {
                levels = InvalidationLevels.ElementAndChildren;
            }
            if ((levels & InvalidationLevels.Children) == InvalidationLevels.Children)
            {
                IList children = this.Children;
                if (children != null)
                {
                    InvalidationTypes types2 = types & InvalidationTypes.Measure;
                    if (levels == InvalidationLevels.Children)
                    {
                        types2 = types;
                    }
                    if (types2 != 0)
                    {
                        using (IEnumerator enumerator = children.GetEnumerator())
                        {
                            while (enumerator.MoveNext())
                            {
                                ((IUIElement) enumerator.Current).Invalidate(InvalidationLevels.ElementAndChildren, types2);
                            }
                        }
                    }
                }
            }
            if ((levels & InvalidationLevels.Element) == InvalidationLevels.Element)
            {
                if ((types & InvalidationTypes.Measure) == InvalidationTypes.Measure)
                {
                    this.#K5j(false);
                }
                if ((types & InvalidationTypes.Arrange) == InvalidationTypes.Arrange)
                {
                    this.#I5j(false);
                }
            }
            if ((parent != null) && ((levels & InvalidationLevels.All) == InvalidationLevels.All))
            {
                parent.Invalidate(InvalidationLevels.All, types);
            }
            else if ((parent != null) && ((levels & InvalidationLevels.TopLevelParent) == InvalidationLevels.TopLevelParent))
            {
                parent.Invalidate(InvalidationLevels.Element | InvalidationLevels.TopLevelParent, types);
            }
            else if ((parent != null) && ((levels & InvalidationLevels.Parent) == InvalidationLevels.Parent))
            {
                parent.Invalidate(InvalidationLevels.Element, types);
            }
            else if ((levels & InvalidationLevels.Element) == InvalidationLevels.Element)
            {
                if ((types & InvalidationTypes.Layout) == InvalidationTypes.Layout)
                {
                    this.UpdateLayout();
                }
                if ((types & InvalidationTypes.Paint) == InvalidationTypes.Paint)
                {
                    this.Invalidate();
                }
            }
        }

        public void InvalidateArrange()
        {
            this.#I5j(true);
        }

        public void InvalidateMeasure()
        {
            this.#K5j(true);
        }

        public void Measure(Graphics g, System.Drawing.Size availableSize)
        {
            if (!this.#8se || (this.#A5j || (this.#C5j != availableSize)))
            {
                bool flag = this.#A5j;
                System.Drawing.Size size = this.#XK;
                System.Drawing.Size size2 = new System.Drawing.Size();
                try
                {
                    this.#A5j = false;
                    this.#I5j(false);
                    this.#y5j = true;
                    size2 = this.MeasureOverride(g, availableSize);
                }
                finally
                {
                    this.#y5j = false;
                    this.#C5j = availableSize;
                    this.#8se = true;
                    this.#XK = size2;
                }
                if (!flag && (!this.#x5j && (size != this.#XK)))
                {
                    IUIElement parent = this.Parent as IUIElement;
                    if (parent != null)
                    {
                        parent.NotifyChildDesiredSizeChanged();
                    }
                }
            }
        }

        protected virtual System.Drawing.Size MeasureOverride(Graphics g, System.Drawing.Size availableSize)
        {
            IList children = this.Children;
            if (children != null)
            {
                using (IEnumerator enumerator = children.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        ((IUIElement) enumerator.Current).Measure(g, availableSize);
                    }
                }
            }
            return new System.Drawing.Size(0, 0);
        }

        protected virtual void OnClick(MouseEventArgs e)
        {
            PointHitTestResult result = ((IUIElement) this).HitTest(new PointHitTestParameters(new Point(e.X, e.Y)));
            if (!this.IsMouseCaptured && ((result.Element != null) && (result.Element is IInputElement)))
            {
                ((IInputElement) result.Element).RaiseClickEvent(e);
            }
            if (this.#X0d != null)
            {
                this.#X0d(this, e);
            }
        }

        protected virtual void OnDoubleClick(MouseEventArgs e)
        {
            PointHitTestResult result = ((IUIElement) this).HitTest(new PointHitTestParameters(new Point(e.X, e.Y)));
            if (!this.IsMouseCaptured && ((result.Element != null) && (result.Element is IInputElement)))
            {
                ((IInputElement) result.Element).RaiseDoubleClickEvent(e);
            }
            if (this.#Ate != null)
            {
                this.#Ate(this, e);
            }
        }

        protected virtual void OnMouseDown(MouseEventArgs e)
        {
            HandledMouseEventArgs args = e as HandledMouseEventArgs;
            if ((args == null) || !args.Handled)
            {
                PointHitTestResult result = ((IUIElement) this).HitTest(new PointHitTestParameters(new Point(e.X, e.Y)));
                if (this.CaptureMouseWhenPressed && (result.Element == null))
                {
                    this.CaptureMouse();
                }
                if (!this.IsMouseCaptured && ((result.Element != null) && (result.Element is IInputElement)))
                {
                    ((IInputElement) result.Element).RaiseMouseDownEvent(e);
                    if ((args != null) && args.Handled)
                    {
                        return;
                    }
                }
                if (this.InvalidateOnMouseEvents)
                {
                    this.Invalidate();
                }
                if (this.#Bte != null)
                {
                    this.#Bte(this, e);
                }
            }
        }

        protected virtual void OnMouseEnter(MouseEventArgs e)
        {
            this.#zte = true;
            if (this.InvalidateOnMouseEvents)
            {
                this.Invalidate();
            }
            if (this.#Cte != null)
            {
                this.#Cte(this, e);
            }
        }

        protected virtual void OnMouseHover(MouseEventArgs e)
        {
            PointHitTestResult result = ((IUIElement) this).HitTest(new PointHitTestParameters(new Point(e.X, e.Y)));
            if (!this.IsMouseCaptured && ((result.Element != null) && (result.Element is IInputElement)))
            {
                ((IInputElement) result.Element).RaiseMouseHoverEvent(e);
            }
            if (this.#Dte != null)
            {
                this.#Dte(this, e);
            }
        }

        protected virtual void OnMouseLeave(MouseEventArgs e)
        {
            this.#zte = false;
            this.#Xwe(null, e);
            if (this.InvalidateOnMouseEvents)
            {
                this.Invalidate();
            }
            if (this.#Ete != null)
            {
                this.#Ete(this, e);
            }
            ((IUIElement) this).NotifyMouseLeaveEvent();
        }

        protected virtual void OnMouseMove(MouseEventArgs e)
        {
            bool isMouseCaptured = this.IsMouseCaptured;
            if (isMouseCaptured && (this.Parent != null))
            {
                IUIControl control = ((ILogicalTreeNode) this).FindAncestor(typeof(IUIControl)) as IUIControl;
                if (control != null)
                {
                    PointHitTestResult result2 = control.HitTestRecursive(new PointHitTestParameters(new Point(e.X, e.Y)));
                    bool flag = (result2.Element != null) && ReferenceEquals(((ILogicalTreeNode) this).GetCommonAncestor(result2.Element), this);
                    if (flag != this.#zte)
                    {
                        if (flag)
                        {
                            this.OnMouseEnter(e);
                        }
                        else
                        {
                            this.OnMouseLeave(e);
                        }
                    }
                }
            }
            PointHitTestResult result = ((IUIElement) this).HitTest(new PointHitTestParameters(new Point(e.X, e.Y)));
            this.#Xwe(result.Element, e);
            if (!isMouseCaptured && ((result.Element != null) && (result.Element is IInputElement)))
            {
                ((IInputElement) result.Element).RaiseMouseMoveEvent(e);
            }
            if (this.#Fte != null)
            {
                this.#Fte(this, e);
            }
        }

        protected virtual void OnMouseUp(MouseEventArgs e)
        {
            bool isMouseCaptured = this.IsMouseCaptured;
            if (isMouseCaptured)
            {
                this.ReleaseMouseCapture();
            }
            PointHitTestResult result = ((IUIElement) this).HitTest(new PointHitTestParameters(new Point(e.X, e.Y)));
            if (!isMouseCaptured && ((result.Element != null) && (result.Element is IInputElement)))
            {
                ((IInputElement) result.Element).RaiseMouseUpEvent(e);
            }
            if (this.InvalidateOnMouseEvents)
            {
                this.Invalidate();
            }
            if (this.#Gte != null)
            {
                this.#Gte(this, e);
            }
        }

        protected virtual void OnMouseWheel(MouseEventArgs e)
        {
            PointHitTestResult result = ((IUIElement) this).HitTest(new PointHitTestParameters(new Point(e.X, e.Y)));
            if (!this.IsMouseCaptured && ((result.Element != null) && (result.Element is IInputElement)))
            {
                ((IInputElement) result.Element).RaiseMouseWheelEvent(e);
            }
            if (this.#Hte != null)
            {
                this.#Hte(this, e);
            }
        }

        protected virtual void OnRender(PaintEventArgs e)
        {
        }

        protected virtual void OnRenderChildElements(PaintEventArgs e)
        {
            if (this.Children != null)
            {
                RectangleF clipBounds = e.Graphics.ClipBounds;
                int num = 0;
                while (true)
                {
                    if (num >= this.Children.Count)
                    {
                        e.Graphics.SetClip(clipBounds);
                        break;
                    }
                    IUIElement element = this.Children[num] as IUIElement;
                    if (element.Visibility == Visibility.Visible)
                    {
                        RectangleF rect = RectangleF.Intersect(clipBounds, element.ClipBounds);
                        if ((rect.Width > 0f) && (rect.Height > 0f))
                        {
                            e.Graphics.SetClip(rect);
                            if (element is Control)
                            {
                                ((Control) element).Update();
                            }
                            else
                            {
                                element.Render(e);
                            }
                        }
                    }
                    num++;
                }
            }
        }

        protected virtual void OnResize(EventArgs e)
        {
            if (this.#Ite != null)
            {
                this.#Ite(this, e);
            }
        }

        protected virtual void PostArrangeOverride()
        {
        }

        public void ReleaseMouseCapture()
        {
            IUIControl control = ((ILogicalTreeNode) this).FindAncestor(System.Type.GetTypeFromHandle(typeof(IUIControl).TypeHandle)) as IUIControl;
            if ((control != null) && ReferenceEquals(control.MouseCaptureElement, this))
            {
                control.MouseCaptureElement = null;
                control.Capture = false;
            }
        }

        public void Render(PaintEventArgs e)
        {
            this.OnRender(e);
            this.OnRenderChildElements(e);
        }

        public GeneralTransform TransformToAncestor(IUIElement ancestor) => 
            TransformToAncestor(this, ancestor);

        internal static GeneralTransform TransformToAncestor(IUIElement #ql, IUIElement #Hxf)
        {
            Point translation = new Point();
            for (IUIElement element = #ql; (element != null) && !ReferenceEquals(element, #Hxf); element = element.Parent as IUIElement)
            {
                translation.Offset(element.VisualOffset.X, element.VisualOffset.Y);
            }
            return new GeneralTransform(translation);
        }

        public GeneralTransform TransformToDescendant(IUIElement descendant) => 
            TransformToDescendant(this, descendant);

        internal static GeneralTransform TransformToDescendant(IUIElement #ql, IUIElement #Ixf)
        {
            Point translation = new Point();
            for (IUIElement element = #Ixf; (element != null) && !ReferenceEquals(element, #ql); element = element.Parent as IUIElement)
            {
                translation.Offset(-element.VisualOffset.X, -element.VisualOffset.Y);
            }
            return new GeneralTransform(translation);
        }

        public virtual void UpdateLayout()
        {
            if (!this.#8se)
            {
                Graphics g = this.CreateGraphics();
                if (g != null)
                {
                    try
                    {
                        this.Measure(g, this.#Bo.Size);
                    }
                    finally
                    {
                        g.Dispose();
                    }
                }
            }
            if (!this.#7se)
            {
                this.Arrange(this.#B5j);
            }
        }

        private bool ActiproSoftware.WinUICore.IInputElement.IsMouseDirectlyOver =>
            this.#zte;

        private Visibility ActiproSoftware.WinUICore.IUIElement.Visibility
        {
            get => 
                this.#Ucc;
            set
            {
                if (this.#Ucc != value)
                {
                    this.#Ucc = value;
                    IUIElement parent = this.Parent as IUIElement;
                    if (parent != null)
                    {
                        parent.NotifyChildDesiredSizeChanged();
                    }
                    this.Invalidate();
                }
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int ActualHeight =>
            this.#Bo.Height;

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int ActualWidth =>
            this.#Bo.Width;

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Rectangle Bounds =>
            this.#Bo;

        protected virtual bool CaptureMouseWhenPressed =>
            false;

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual Rectangle ClipBounds =>
            this.#Bo;

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public System.Drawing.Size DesiredSize =>
            this.#XK;

        protected virtual bool InvalidateOnMouseEvents =>
            false;

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsArrangeValid =>
            this.#7se;

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsMeasureValid =>
            this.#8se;

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsMouseCaptured
        {
            get
            {
                IUIControl control = ((ILogicalTreeNode) this).FindAncestor(System.Type.GetTypeFromHandle(typeof(IUIControl).TypeHandle)) as IUIControl;
                return ((control != null) && ReferenceEquals(control.MouseCaptureElement, this));
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsRightToLeft
        {
            get
            {
                Control control = ((ILogicalTreeNode) this).FindAncestor(System.Type.GetTypeFromHandle(typeof(Control).TypeHandle)) as Control;
                return ((control != null) ? (control.RightToLeft == RightToLeft.Yes) : false);
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public System.Drawing.Size Size =>
            this.#Bo.Size;

        public Point VisualOffset =>
            this.#D5j;
    }
}

