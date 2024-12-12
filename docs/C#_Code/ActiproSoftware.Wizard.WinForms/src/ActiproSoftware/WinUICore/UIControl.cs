namespace ActiproSoftware.WinUICore
{
    using #Fqe;
    using #H;
    using #Xqe;
    using ActiproSoftware.ComponentModel;
    using ActiproSoftware.Drawing;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public abstract class UIControl : ScrollableControl, IDisposable, IUIControl, IUIElement, ILogicalTreeNode
    {
        private IDoubleBufferCanvas #Vk;
        private IList #bj;
        private Size #XK;
        private Rectangle #4se = Rectangle.Empty;
        private Rectangle #6se = Rectangle.Empty;
        private bool #w5j;
        private bool #7se;
        private bool #x5j;
        private bool #y5j;
        private bool #8se;
        private bool #z5j = true;
        private bool #A5j = true;
        private bool #Nqk;
        private MouseButtons #9se;
        private IUIElement #ate;
        private IUIElement #bte;
        private byte #cte;
        private IUIElement #Seb;
        private Rectangle #dte = Rectangle.Empty;
        private ArrayList #ete;
        private bool #fte = true;
        private Rectangle #B5j;
        private Size #C5j;
        private Visibility #Ucc;
        private Point #D5j;

        private Graphics #0we() => 
            this.CreateGraphics();

        private void #E5j()
        {
            if (!this.#w5j && (!this.#y5j && this.#8se))
            {
                this.#K5j(false);
            }
        }

        private object #ewe(System.Type #4Ub) => 
            (this.#Seb != null) ? (!this.IsInstanceOfType(this.#Seb) ? this.#Seb.FindAncestor(#4Ub) : this.#Seb) : null;

        private ILogicalTreeNode #fwe(ILogicalTreeNode #Ld)
        {
            if ((#Ld.Parent != null) && (this.#Seb != null))
            {
                if (ReferenceEquals(#Ld, this))
                {
                    return #Ld;
                }
                if (((ILogicalTreeNode) this).IsAncestorOf(#Ld))
                {
                    return this;
                }
                if (((ILogicalTreeNode) this).IsDescendantOf(#Ld))
                {
                    return #Ld;
                }
                for (ILogicalTreeNode node = #Ld; node.Parent != null; node = node.Parent)
                {
                    if (node.Parent.IsAncestorOf(this))
                    {
                        return node.Parent;
                    }
                }
            }
            return null;
        }

        private void #G5j(Rectangle #cdd, bool #H5j)
        {
            this.#D5j = #cdd.Location;
            Size size = #H5j ? this.ArrangeOverride(#cdd.Size) : #cdd.Size;
            bool flag1 = base.Bounds.Size != size;
            Point point = UIElement.#M5j(this);
            Rectangle rectangle = new Rectangle(point.X + this.#D5j.X, point.Y + this.#D5j.Y, size.Width, size.Height);
            if (base.Bounds != rectangle)
            {
                base.Bounds = rectangle;
            }
        }

        private bool #gwe(ILogicalTreeNode #Ld)
        {
            ILogicalTreeNode parent;
            if (0 != 0)
            {
                ILogicalTreeNode local1 = #Ld;
            }
            else
            {
                parent = #Ld;
            }
            while (parent.Parent != null)
            {
                parent = parent.Parent;
                if (ReferenceEquals(parent, this))
                {
                    return true;
                }
            }
            return false;
        }

        private bool #hwe(ILogicalTreeNode #Ld) => 
            #Ld.IsAncestorOf(this);

        private void #I5j(bool #J5j)
        {
            if (!this.#7se)
            {
                if (#J5j && this.IsPaintValid)
                {
                    this.AddToInvalidatedRegion();
                }
            }
            else if (!this.#w5j)
            {
                if (!this.#z5j)
                {
                    if (this.#Seb == null)
                    {
                        if (#J5j)
                        {
                            base.Invalidate();
                        }
                    }
                    else if (#J5j)
                    {
                        this.#Seb.InvalidateArrange();
                    }
                    else
                    {
                        this.#Seb.Invalidate(InvalidationLevels.Element, InvalidationTypes.Arrange);
                    }
                }
                this.#7se = false;
            }
        }

        private bool #Iwe(Rectangle #Bo) => 
            this.AddPendingGraphicsInversion(#Bo);

        private bool #Jwe(Rectangle #Bo, int #Pve, int #Qve) => 
            this.AddPendingScrollOperation(#Bo, #Pve, #Qve);

        private bool #Jwe(Rectangle #Bo, Orientation #k1d, int #6Zf) => 
            this.AddPendingScrollOperation(#Bo, #k1d, #6Zf);

        private void #K5j(bool #J5j)
        {
            if (!this.#8se)
            {
                if (#J5j && this.IsPaintValid)
                {
                    this.AddToInvalidatedRegion();
                }
            }
            else if (!this.#y5j)
            {
                if (!this.#A5j)
                {
                    if (this.#Seb == null)
                    {
                        if (#J5j && !this.#w5j)
                        {
                            base.Invalidate();
                        }
                    }
                    else if (#J5j)
                    {
                        this.#Seb.InvalidateMeasure();
                    }
                    else
                    {
                        this.#Seb.Invalidate(InvalidationLevels.Element, InvalidationTypes.Measure);
                    }
                }
                this.#8se = false;
            }
        }

        private void #Kwe(Rectangle #Fi)
        {
            this.AddToInvalidatedRegion(#Fi);
        }

        private void #L5j(Graphics #nYf)
        {
            if (this.Visible && (this.#Ucc != Visibility.Collapsed))
            {
                bool isInUIElementTree = this.IsInUIElementTree;
                if ((#nYf != null) && !this.#8se)
                {
                    this.Measure(#nYf, (!isInUIElementTree || this.#A5j) ? base.ClientSize : this.#C5j);
                }
                if (!this.#7se && !this.#w5j)
                {
                    this.Arrange((!isInUIElementTree || this.#z5j) ? base.Bounds : this.#B5j);
                }
            }
        }

        private PointHitTestResult #Pwe(PointHitTestParameters #LZf)
        {
            if (this.#bj != null)
            {
                for (int i = this.#bj.Count - 1; i >= 0; i--)
                {
                    IUIElement element = this.#bj[i] as IUIElement;
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

        private void #Rwe(Control #lp)
        {
            foreach (Control control in #lp.Controls)
            {
                this.#Rwe(control);
                control.Invalidate();
            }
        }

        private void #Swe()
        {
            if (this.#bj != null)
            {
                for (int i = this.#bj.Count - 1; i >= 0; i--)
                {
                    (this.#bj[i] as IUIElement).NotifyMouseLeaveEvent();
                }
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

        private void #Ywe(PaintEventArgs #yhb)
        {
            Rectangle clipRectangle = #yhb.ClipRectangle;
            if (!this.#4se.IsEmpty)
            {
                clipRectangle = Rectangle.Union(clipRectangle, this.#4se);
            }
            #yhb.Graphics.SetClip(clipRectangle);
            if (this.#cte > 0)
            {
                this.AddToInvalidatedRegion(clipRectangle);
            }
            else if ((clipRectangle.Width != 0) && (clipRectangle.Height != 0))
            {
                this.#6se = Rectangle.Empty;
                this.#4se = Rectangle.Empty;
                if (!this.UseExtendedDoubleBuffering)
                {
                    this.Render(#yhb);
                }
                else
                {
                    if ((this.#Vk.Graphics == null) && (this.#ete != null))
                    {
                        this.#fte = false;
                    }
                    this.#Vk.PrepareGraphics(#yhb);
                    Graphics graphics = this.#Vk.Graphics;
                    if (this.#ete != null)
                    {
                        if (this.#fte)
                        {
                            Region region = new Region(graphics.ClipBounds);
                            foreach (#Zqe zqe in this.#ete)
                            {
                                Rectangle destinationBounds = zqe.DestinationBounds;
                                this.#Vk.Copy(zqe.CopySourceBounds, zqe.CopyDestinationLocation);
                                region.Exclude(destinationBounds);
                            }
                            graphics.Clip = region;
                            region.Dispose();
                            clipRectangle = Rectangle.Ceiling(graphics.ClipBounds);
                        }
                        this.#fte = true;
                        this.#ete.Clear();
                        this.#ete = null;
                    }
                    if (base.GetStyle(ControlStyles.SupportsTransparentBackColor) && (this.BackColor.A < 0xff))
                    {
                        base.OnPaintBackground(new PaintEventArgs(graphics, clipRectangle));
                        graphics.Transform = graphics.Transform;
                    }
                    this.Render(new PaintEventArgs(graphics, clipRectangle));
                    if (this.#dte != Rectangle.Empty)
                    {
                        this.#dte = Rectangle.Intersect(this.#dte, #yhb.ClipRectangle);
                        if ((this.#dte.Width > 0) && (this.#dte.Height > 0))
                        {
                            this.#Vk.Invert(this.#dte);
                        }
                        this.#dte = Rectangle.Empty;
                    }
                    this.#Vk.Flush();
                }
            }
        }

        private void #Zwe(Control #lp)
        {
            if (#lp is IUIControl)
            {
                ((IUIControl) #lp).ResetDoubleBufferCanvas(false);
            }
            Control.ControlCollection controls = #lp.Controls;
            if (controls != null)
            {
                for (int i = controls.Count - 1; i >= 0; i--)
                {
                    this.#Zwe(controls[i]);
                }
            }
        }

        public UIControl()
        {
            base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            base.SetStyle(ControlStyles.ContainerControl, false);
            base.SetStyle(ControlStyles.DoubleBuffer, !this.UseExtendedDoubleBuffering);
            base.SetStyle(ControlStyles.ResizeRedraw, true);
            base.SetStyle(ControlStyles.Selectable, false);
            base.SetStyle(ControlStyles.UserPaint, true);
            IDoubleBufferCanvas canvas1 = (Environment.OSVersion.Platform == PlatformID.Win32Windows) ? ((IDoubleBufferCanvas) new DoubleBufferCanvas(this)) : ((IDoubleBufferCanvas) new #Eqe(this));
            this.#Vk = canvas1;
            this.#bj = this.CreateChildren();
        }

        protected internal bool AddPendingGraphicsInversion(Rectangle bounds)
        {
            this.#dte = bounds;
            return true;
        }

        protected internal bool AddPendingScrollOperation(Rectangle bounds, int xAmount, int yAmount)
        {
            bool flag;
            if ((!this.#fte && (this.#ete != null)) || (this.#cte > 0))
            {
                this.AddToInvalidatedRegion(bounds);
                return false;
            }
            #Zqe zqe = new #Zqe(bounds, xAmount, yAmount);
            if (this.#ete == null)
            {
                this.#ete = new ArrayList();
                this.#fte = true;
            }
            using (IEnumerator enumerator = this.#ete.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        #Zqe current = (#Zqe) enumerator.Current;
                        if (!bounds.IntersectsWith(current.SourceBounds) && !bounds.IntersectsWith(current.DestinationBounds))
                        {
                            continue;
                        }
                        this.AddToInvalidatedRegion(bounds);
                        this.#fte = false;
                        flag = false;
                    }
                    else
                    {
                        this.#ete.Add(zqe);
                        this.AddToInvalidatedRegion(zqe.SourceBounds);
                        return true;
                    }
                    break;
                }
            }
            return flag;
        }

        protected internal bool AddPendingScrollOperation(Rectangle bounds, Orientation orientation, int amount)
        {
            bool flag;
            if ((!this.#fte && (this.#ete != null)) || (this.#cte > 0))
            {
                this.AddToInvalidatedRegion(bounds);
                return false;
            }
            #Zqe zqe = new #Zqe(bounds, orientation, amount);
            if (this.#ete == null)
            {
                this.#ete = new ArrayList();
                this.#fte = true;
            }
            using (IEnumerator enumerator = this.#ete.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        #Zqe current = (#Zqe) enumerator.Current;
                        if (!bounds.IntersectsWith(current.SourceBounds) && !bounds.IntersectsWith(current.DestinationBounds))
                        {
                            continue;
                        }
                        this.AddToInvalidatedRegion(bounds);
                        this.#fte = false;
                        flag = false;
                    }
                    else
                    {
                        this.#ete.Add(zqe);
                        this.AddToInvalidatedRegion(zqe.SourceBounds);
                        return true;
                    }
                    break;
                }
            }
            return flag;
        }

        public void AddToInvalidatedRegion()
        {
            this.AddToInvalidatedRegion(this.ClientRectangle);
        }

        public void AddToInvalidatedRegion(Rectangle rect)
        {
            if ((rect.Width != 0) || (rect.Height != 0))
            {
                this.#6se = !this.#6se.IsEmpty ? Rectangle.Union(this.#6se, rect) : rect;
                this.#4se = !this.#4se.IsEmpty ? Rectangle.Union(this.#4se, this.#6se) : this.#6se;
                if (this.#cte == 0)
                {
                    base.Invalidate(this.#6se, true);
                    this.#6se = Rectangle.Empty;
                }
            }
        }

        public void Arrange(Rectangle finalRect)
        {
            if (!this.#8se || this.#A5j)
            {
                try
                {
                    this.#x5j = true;
                    Bitmap bitmap = null;
                    Graphics g = null;
                    if (this.UseControlGraphicsForMeasure && !base.IsDisposed)
                    {
                        g = base.CreateGraphics();
                    }
                    else
                    {
                        try
                        {
                            g = Graphics.FromImage(new Bitmap(1, 1));
                        }
                        catch
                        {
                        }
                    }
                    if (g != null)
                    {
                        try
                        {
                            if (!this.#A5j && (this.#Seb != null))
                            {
                                this.Measure(g, this.#C5j);
                            }
                            else
                            {
                                this.Measure(g, finalRect.Size);
                            }
                        }
                        finally
                        {
                            g.Dispose();
                            if (bitmap != null)
                            {
                                bitmap.Dispose();
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
                    this.#G5j(new Rectangle(finalRect.Location, base.Bounds.Size), false);
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
            }
        }

        protected virtual Size ArrangeOverride(Size finalSize)
        {
            if (this.#bj != null)
            {
                foreach (IUIElement element in this.#bj)
                {
                    Rectangle clientRectangle = base.ClientRectangle;
                    Size desiredSize = element.DesiredSize;
                    element.Arrange(new Rectangle(base.ClientRectangle.Left, clientRectangle.Top, element.DesiredSize.Width, desiredSize.Height));
                }
            }
            return finalSize;
        }

        public virtual bool ContainsLocation(Point location)
        {
            Rectangle bounds = this.Bounds;
            return bounds.Contains(location);
        }

        protected virtual IList CreateChildren() => 
            null;

        protected override void Dispose(bool disposing)
        {
            this.Dispose(disposing);
            if (disposing)
            {
                if ((this.#bj != null) && (this.#bj is IDisposable))
                {
                    ((IDisposable) this.#bj).Dispose();
                }
                ((ILogicalTreeNode) this).Parent = null;
                if (this.#Vk != null)
                {
                    this.#Vk.Dispose();
                    this.#Vk = null;
                }
            }
        }

        public virtual Cursor GetCursor(Point point) => 
            null;

        public void Invalidate(InvalidationLevels levels, InvalidationTypes types)
        {
            if ((this.#Seb == null) && ((levels & InvalidationLevels.All) == InvalidationLevels.All))
            {
                levels = InvalidationLevels.ElementAndChildren;
            }
            if (((levels & InvalidationLevels.Children) == InvalidationLevels.Children) && (this.#bj != null))
            {
                InvalidationTypes types2 = types & InvalidationTypes.Measure;
                if (levels == InvalidationLevels.Children)
                {
                    types2 = types;
                }
                if (types2 != 0)
                {
                    using (IEnumerator enumerator = this.#bj.GetEnumerator())
                    {
                        while (enumerator.MoveNext())
                        {
                            ((IUIElement) enumerator.Current).Invalidate(InvalidationLevels.ElementAndChildren, types2);
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
            if ((this.#Seb != null) && ((levels & InvalidationLevels.All) == InvalidationLevels.All))
            {
                this.#Seb.Invalidate(InvalidationLevels.All, types);
            }
            else if ((this.#Seb != null) && ((levels & InvalidationLevels.TopLevelParent) == InvalidationLevels.TopLevelParent))
            {
                this.#Seb.Invalidate(InvalidationLevels.Element | InvalidationLevels.TopLevelParent, types);
            }
            else if ((this.#Seb != null) && ((levels & InvalidationLevels.Parent) == InvalidationLevels.Parent))
            {
                this.#Seb.Invalidate(InvalidationLevels.Element, types);
            }
            else if ((levels & InvalidationLevels.Element) == InvalidationLevels.Element)
            {
                if ((types & InvalidationTypes.Layout) == InvalidationTypes.Layout)
                {
                    this.UpdateLayout();
                }
                if ((types & InvalidationTypes.Paint) == InvalidationTypes.Paint)
                {
                    this.#Rwe(this);
                    this.AddToInvalidatedRegion();
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

        public void Measure(Graphics g, Size availableSize)
        {
            if (!this.#8se || (this.#A5j || (this.#C5j != availableSize)))
            {
                bool flag = this.#A5j;
                Size size = this.#XK;
                Size size2 = new Size();
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
                if (!flag && (!this.#x5j && ((size != this.#XK) && (this.#Seb != null))))
                {
                    this.#Seb.NotifyChildDesiredSizeChanged();
                }
            }
        }

        protected virtual Size MeasureOverride(Graphics g, Size availableSize)
        {
            IList children = ((ILogicalTreeNode) this).Children;
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
            return new Size(0, 0);
        }

        protected override void OnClick(EventArgs e)
        {
            this.SuspendPainting();
            Point point = this.PointToClient(Control.MousePosition);
            MouseEventArgs args = new MouseEventArgs(Control.MouseButtons, 0, point.X, point.Y, 0);
            if (this.#ate != null)
            {
                if (this.#ate is IInputElement)
                {
                    ((IInputElement) this.#ate).RaiseClickEvent(args);
                }
            }
            else
            {
                PointHitTestResult result = ((IUIElement) this).HitTest(new PointHitTestParameters(point));
                if ((result.Element != null) && (result.Element is IInputElement))
                {
                    ((IInputElement) result.Element).RaiseClickEvent(args);
                }
            }
            base.OnClick(e);
            this.ResumePainting();
        }

        protected override void OnDoubleClick(EventArgs e)
        {
            this.SuspendPainting();
            Point point = this.PointToClient(Control.MousePosition);
            MouseEventArgs args = new MouseEventArgs(this.#9se, 0, point.X, point.Y, 0);
            if (this.#ate != null)
            {
                if (this.#ate is IInputElement)
                {
                    ((IInputElement) this.#ate).RaiseDoubleClickEvent(args);
                }
            }
            else
            {
                PointHitTestResult result = ((IUIElement) this).HitTest(new PointHitTestParameters(point));
                if ((result.Element != null) && (result.Element is IInputElement))
                {
                    ((IInputElement) result.Element).RaiseDoubleClickEvent(args);
                }
            }
            base.OnDoubleClick(e);
            this.ResumePainting();
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            this.OnHandleCreated(e);
            this.ResetDoubleBufferCanvas(false);
        }

        protected override void OnLayout(LayoutEventArgs e)
        {
            if (!this.#w5j && (this.#Ucc != Visibility.Collapsed))
            {
                bool isInUIElementTree = this.IsInUIElementTree;
                if (!isInUIElementTree)
                {
                    this.#K5j(false);
                    this.#I5j(false);
                }
                this.Arrange((!isInUIElementTree || this.#z5j) ? base.Bounds : this.#B5j);
            }
            base.OnLayout(e);
        }

        protected override void OnLocationChanged(EventArgs e)
        {
            this.OnLocationChanged(e);
            if (!this.IsInUIElementTree && !this.#z5j)
            {
                this.#B5j.Location = base.Location;
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            HandledMouseEventArgs args = e as HandledMouseEventArgs;
            if (args == null)
            {
                args = new HandledMouseEventArgs(e.Button, e.Clicks, e.X, e.Y, e.Delta);
            }
            else if (args.Handled)
            {
                return;
            }
            this.SuspendPainting();
            if (this.#ate != null)
            {
                if (this.#ate is IInputElement)
                {
                    ((IInputElement) this.#ate).RaiseMouseDownEvent(args);
                }
            }
            else
            {
                PointHitTestResult result = ((IUIElement) this).HitTest(new PointHitTestParameters(new Point(args.X, args.Y)));
                if ((result.Element != null) && (result.Element is IInputElement))
                {
                    ((IInputElement) result.Element).RaiseMouseDownEvent(args);
                }
            }
            if (!args.Handled)
            {
                base.OnMouseDown(args);
            }
            this.ResumePainting();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            this.SuspendPainting();
            this.OnMouseEnter(e);
            this.ResumePainting();
        }

        protected override void OnMouseHover(EventArgs e)
        {
            this.SuspendPainting();
            Point point = this.PointToClient(Control.MousePosition);
            MouseEventArgs args = new MouseEventArgs(Control.MouseButtons, 0, point.X, point.Y, 0);
            if (this.#ate != null)
            {
                if (this.#ate is IInputElement)
                {
                    ((IInputElement) this.#ate).RaiseMouseHoverEvent(args);
                }
            }
            else
            {
                PointHitTestResult result = ((IUIElement) this).HitTest(new PointHitTestParameters(point));
                if ((result.Element != null) && (result.Element is IInputElement))
                {
                    ((IInputElement) result.Element).RaiseMouseHoverEvent(args);
                }
            }
            base.OnMouseHover(e);
            this.ResumePainting();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            this.SuspendPainting();
            if (this.#ate == null)
            {
                Point point = base.PointToClient(Control.MousePosition);
                MouseEventArgs args = new MouseEventArgs(Control.MouseButtons, 0, point.X, point.Y, 0);
                this.#Xwe(null, args);
            }
            base.OnMouseLeave(e);
            ((IUIElement) this).NotifyMouseLeaveEvent();
            base.Cursor = null;
            this.ResumePainting();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            this.SuspendPainting();
            if (this.#ate != null)
            {
                if (this.#ate is IInputElement)
                {
                    if (this.#ate is UIElement)
                    {
                        base.Cursor = ((UIElement) this.#ate).GetCursor(new Point(e.X, e.Y));
                    }
                    ((IInputElement) this.#ate).RaiseMouseMoveEvent(e);
                }
            }
            else
            {
                PointHitTestResult result = ((IUIElement) this).HitTestRecursive(new PointHitTestParameters(new Point(e.X, e.Y)));
                base.Cursor = ((result.Element == null) || !(result.Element is UIElement)) ? this.GetCursor(new Point(e.X, e.Y)) : ((UIElement) result.Element).GetCursor(new Point(e.X, e.Y));
                result = ((IUIElement) this).HitTest(new PointHitTestParameters(new Point(e.X, e.Y)));
                this.#Xwe(result.Element, e);
                if ((result.Element != null) && (result.Element is IInputElement))
                {
                    ((IInputElement) result.Element).RaiseMouseMoveEvent(e);
                }
            }
            base.OnMouseMove(e);
            base.ResetMouseEventArgs();
            this.ResumePainting();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            this.SuspendPainting();
            this.#9se = e.Button;
            if (this.#ate != null)
            {
                if (this.#ate is IInputElement)
                {
                    ((IInputElement) this.#ate).RaiseMouseUpEvent(e);
                }
            }
            else
            {
                PointHitTestResult result = ((IUIElement) this).HitTest(new PointHitTestParameters(new Point(e.X, e.Y)));
                if ((result.Element != null) && (result.Element is IInputElement))
                {
                    ((IInputElement) result.Element).RaiseMouseUpEvent(e);
                }
            }
            base.OnMouseUp(e);
            this.ResumePainting();
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            this.SuspendPainting();
            if (this.#ate != null)
            {
                if (this.#ate is IInputElement)
                {
                    ((IInputElement) this.#ate).RaiseMouseWheelEvent(e);
                }
            }
            else
            {
                PointHitTestResult result = ((IUIElement) this).HitTest(new PointHitTestParameters(new Point(e.X, e.Y)));
                if ((result.Element != null) && (result.Element is IInputElement))
                {
                    ((IInputElement) result.Element).RaiseMouseWheelEvent(e);
                }
            }
            base.OnMouseWheel(e);
            this.ResumePainting();
        }

        protected sealed override void OnPaint(PaintEventArgs e)
        {
            if (!this.#Nqk)
            {
                try
                {
                    this.#Nqk = true;
                    this.#Ywe(e);
                }
                finally
                {
                    this.#Nqk = false;
                }
            }
        }

        protected sealed override void OnPaintBackground(PaintEventArgs e)
        {
            if (e != null)
            {
                this.#L5j(e.Graphics);
            }
            if (!this.UseExtendedDoubleBuffering)
            {
                base.OnPaintBackground(e);
            }
        }

        protected virtual void OnParentChanged()
        {
        }

        protected virtual void OnRender(PaintEventArgs e)
        {
            SolidBrush brush = new SolidBrush(this.BackColor);
            e.Graphics.FillRectangle(brush, base.ClientRectangle);
            brush.Dispose();
        }

        protected virtual void OnRenderChildElements(PaintEventArgs e)
        {
            if (this.#bj != null)
            {
                RectangleF clipBounds = e.Graphics.ClipBounds;
                int num = 0;
                while (true)
                {
                    if (num >= this.#bj.Count)
                    {
                        e.Graphics.SetClip(clipBounds);
                        break;
                    }
                    IUIElement element = this.#bj[num] as IUIElement;
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

        protected override void OnResize(EventArgs e)
        {
            this.ResetDoubleBufferCanvas(false);
            this.OnResize(e);
        }

        protected override void OnRightToLeftChanged(EventArgs e)
        {
            this.Invalidate(InvalidationLevels.ElementAndChildren, InvalidationTypes.Arrange);
            this.OnRightToLeftChanged(e);
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            this.ResetDoubleBufferCanvas(!this.Visible);
            base.OnVisibleChanged(e);
        }

        public void Render(PaintEventArgs e)
        {
            this.OnRender(e);
            this.OnRenderChildElements(e);
        }

        public void ResetDoubleBufferCanvas(bool recurse)
        {
            if (recurse)
            {
                this.#Zwe(this);
            }
            else if (this.#Vk != null)
            {
                this.#Vk.Reset();
            }
        }

        public void ResumePainting()
        {
            if (this.#cte > 0)
            {
                this.#cte = (byte) (this.#cte - 1);
            }
            if ((this.#cte == 0) && !this.#6se.IsEmpty)
            {
                base.Invalidate(this.#6se);
                this.#6se = Rectangle.Empty;
            }
        }

        public void SuspendPainting()
        {
            this.#cte = (byte) Math.Min(this.#cte + 1, 0xff);
        }

        public GeneralTransform TransformToAncestor(IUIElement ancestor) => 
            UIElement.TransformToAncestor(this, ancestor);

        public GeneralTransform TransformToDescendant(IUIElement descendant) => 
            UIElement.TransformToDescendant(this, descendant);

        public virtual void UpdateLayout()
        {
            if (!this.Disposing && !this.IsDisposed)
            {
                if (!this.#8se)
                {
                    Bitmap bitmap = null;
                    Graphics g = null;
                    if (this.UseControlGraphicsForMeasure && !base.IsDisposed)
                    {
                        g = base.CreateGraphics();
                    }
                    else
                    {
                        try
                        {
                            g = Graphics.FromImage(new Bitmap(1, 1));
                        }
                        catch
                        {
                        }
                    }
                    if (g != null)
                    {
                        try
                        {
                            this.Measure(g, base.ClientSize);
                        }
                        finally
                        {
                            g.Dispose();
                            if (bitmap != null)
                            {
                                bitmap.Dispose();
                            }
                        }
                    }
                }
                if (!this.#7se && (this.#Ucc != Visibility.Collapsed))
                {
                    this.Arrange((!this.IsInUIElementTree || this.#z5j) ? base.Bounds : this.#B5j);
                    base.PerformLayout();
                }
            }
        }

        private IList ActiproSoftware.ComponentModel.ILogicalTreeNode.Children =>
            this.#bj;

        private ILogicalTreeNode ActiproSoftware.ComponentModel.ILogicalTreeNode.Parent
        {
            get => 
                this.#Seb;
            set
            {
                if ((value != null) && !(value is IUIElement))
                {
                    throw new ApplicationException(string.Format(#G.#eg(0x57d), value.GetType().FullName));
                }
                this.#Seb = (IUIElement) value;
                this.OnParentChanged();
            }
        }

        private IUIElement ActiproSoftware.WinUICore.IUIControl.MouseCaptureElement
        {
            get => 
                this.MouseCaptureElement;
            set => 
                this.MouseCaptureElement = value;
        }

        private bool ActiproSoftware.WinUICore.IUIControl.Capture
        {
            get => 
                this.Capture;
            set => 
                this.Capture = value;
        }

        private Rectangle ActiproSoftware.WinUICore.IUIElement.Bounds =>
            this.Bounds;

        private Size ActiproSoftware.WinUICore.IUIElement.Size =>
            this.Size;

        private Visibility ActiproSoftware.WinUICore.IUIElement.Visibility
        {
            get => 
                this.#Ucc;
            set
            {
                if (this.#Ucc != value)
                {
                    this.#Ucc = value;
                    if (this.#Seb != null)
                    {
                        this.#Seb.NotifyChildDesiredSizeChanged();
                    }
                    base.Invalidate();
                }
            }
        }

        private Point ActiproSoftware.WinUICore.IUIElement.VisualOffset =>
            this.#D5j;

        private bool IsInUIElementTree =>
            this.#Seb != null;

        internal IUIElement MouseCaptureElement
        {
            get => 
                this.#ate;
            set => 
                this.#ate = value;
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int ActualHeight
        {
            get
            {
                Rectangle bounds = this.Bounds;
                return bounds.Height;
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int ActualWidth
        {
            get
            {
                Rectangle bounds = this.Bounds;
                return bounds.Width;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public override Color BackColor
        {
            get => 
                this.BackColor;
            set => 
                this.BackColor = value;
        }

        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public override Image BackgroundImage
        {
            get => 
                this.BackgroundImage;
            set => 
                this.BackgroundImage = value;
        }

        protected virtual bool CaptureMouseWhenPressed =>
            false;

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual Rectangle ClipBounds =>
            this.ClientRectangle;

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Size DesiredSize =>
            this.#XK;

        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public override System.Drawing.Font Font
        {
            get => 
                this.Font;
            set => 
                this.Font = value;
        }

        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public override Color ForeColor
        {
            get => 
                this.ForeColor;
            set => 
                this.ForeColor = value;
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsArrangeValid =>
            this.#7se;

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsMeasureValid =>
            this.#8se;

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsPaintValid =>
            this.#6se.IsEmpty;

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsRightToLeft =>
            this.RightToLeft == RightToLeft.Yes;

        protected MouseButtons LastMouseUpButton =>
            this.#9se;

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool PaintingSuspended =>
            this.#cte > 0;

        protected virtual bool UseControlGraphicsForMeasure =>
            true;

        protected virtual bool UseExtendedDoubleBuffering =>
            true;
    }
}

