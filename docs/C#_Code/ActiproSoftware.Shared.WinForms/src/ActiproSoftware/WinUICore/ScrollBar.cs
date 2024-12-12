namespace ActiproSoftware.WinUICore
{
    using #H;
    using #Xqe;
    using ActiproSoftware.Drawing;
    using ActiproSoftware.Products.Shared;
    using ActiproSoftware.WinUICore.Commands;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows.Forms;

    [DefaultEvent("ValueChanged"), DefaultProperty("Value"), ToolboxBitmap(typeof(ActiproSoftware.WinUICore.ScrollBar)), ToolboxItem(true)]
    public class ScrollBar : UIControl, ISupportInitialize, ICommandTarget
    {
        private const int #qve = 400;
        internal const int #rve = 7;
        private const int #sve = 0x19;
        private int #tve;
        private CommandLinkCollection #ZYd = new CommandLinkCollection();
        private Point #uve;
        private ScrollBarButton #vve;
        private ICommandTarget #oZd;
        private bool #4Xd;
        private ScrollBarButton #wve;
        private bool #tZd;
        private int #xve = 10;
        private int #wD;
        private int #vD = 100;
        private System.Windows.Forms.Orientation #k1d;
        private IScrollBarRenderer #IZd;
        private ActiproSoftware.WinUICore.Commands.Command #yve;
        private int #zve = 1;
        private System.Windows.Forms.Timer #h7;
        private ScrollBarThumb #Ave;
        private ToolTip #7Xd;
        private int #Ld;
        private ActiproSoftware.WinUICore.Commands.Command #Bve = new ActiproSoftware.WinUICore.Commands.Command(ActiproSoftware.Products.Shared.SR.GetString(#G.#eg(0xac)));
        private ActiproSoftware.WinUICore.Commands.Command #Cve = new ActiproSoftware.WinUICore.Commands.Command(ActiproSoftware.Products.Shared.SR.GetString(#G.#eg(0xdd)));
        private ActiproSoftware.WinUICore.Commands.Command #Dve = new ActiproSoftware.WinUICore.Commands.Command(ActiproSoftware.Products.Shared.SR.GetString(#G.#eg(270)));
        private ActiproSoftware.WinUICore.Commands.Command #Eve = new ActiproSoftware.WinUICore.Commands.Command(ActiproSoftware.Products.Shared.SR.GetString(#G.#eg(0x13f)));

        [Category("Action"), Description("Occurs when a Command is executed.")]
        public event CommandEventHandler Command;

        [Category("Action"), Description("Occurs after a scroll event occurs.")]
        public event ScrollEventHandler Scrolled;

        [Category("Action"), Description("Occurs after thumb scrolling ends.")]
        public event EventHandler ThumbScrolled;

        [Category("Action"), Description("Occurs before thumb scrolling starts.")]
        public event EventHandler ThumbScrolling;

        [Category("Action"), Description("Occurs after the value of the Value property changes.")]
        public event EventHandler ValueChanged;

        private void #45d(bool #fV)
        {
            ActiproSoftware.WinUICore.ScrollBar bar;
            if (0 != 0)
            {
            }
            else
            {
                bar = this;
            }
            lock (bar)
            {
                if (#fV)
                {
                    if (!this.#4Xd)
                    {
                        this.#4Xd = true;
                        UIRendererManager.IncrementUsageCount(typeof(IScrollBarRenderer), new #Yqe());
                        UIRendererManager.RendererData rendererData = UIRendererManager.GetRendererData(typeof(IScrollBarRenderer));
                        if (rendererData != null)
                        {
                            rendererData.RendererPropertyChanged += new EventHandler(this.#J6d);
                        }
                    }
                }
                else if (this.#4Xd)
                {
                    this.#4Xd = false;
                    UIRendererManager.RendererData rendererData = UIRendererManager.GetRendererData(typeof(IScrollBarRenderer));
                    if (rendererData != null)
                    {
                        rendererData.RendererPropertyChanged -= new EventHandler(this.#J6d);
                    }
                    UIRendererManager.DecrementUsageCount(typeof(IScrollBarRenderer));
                }
            }
        }

        private void #7Yb(object #xhb, EventArgs #yhb)
        {
            this.#h7.Stop();
            this.#h7.Interval = 0x19;
            bool isMouseDirectlyOver = false;
            if (ReferenceEquals(this.#yve, this.#Cve))
            {
                isMouseDirectlyOver = this.#vve.IsMouseDirectlyOver;
            }
            else if (ReferenceEquals(this.#yve, this.#Eve))
            {
                isMouseDirectlyOver = this.#wve.IsMouseDirectlyOver;
            }
            else if (ReferenceEquals(this.#yve, this.#Bve) || ReferenceEquals(this.#yve, this.#Dve))
            {
                Point point = base.PointToClient(Control.MousePosition);
                PointHitTestResult result = ((IUIElement) this).HitTest(new PointHitTestParameters(point));
                if (base.ClientRectangle.Contains(point) && (result.Element == null))
                {
                    if (this.#k1d == System.Windows.Forms.Orientation.Horizontal)
                    {
                        if (point.X < this.#Ave.Bounds.Left)
                        {
                            isMouseDirectlyOver = ReferenceEquals(this.#yve, this.#Bve);
                        }
                        else if (point.X >= this.#Ave.Bounds.Right)
                        {
                            isMouseDirectlyOver = ReferenceEquals(this.#yve, this.#Dve);
                        }
                    }
                    else if (point.Y < this.#Ave.Bounds.Top)
                    {
                        isMouseDirectlyOver = ReferenceEquals(this.#yve, this.#Bve);
                    }
                    else if (point.Y >= this.#Ave.Bounds.Bottom)
                    {
                        isMouseDirectlyOver = ReferenceEquals(this.#yve, this.#Dve);
                    }
                }
            }
            if (isMouseDirectlyOver)
            {
                this.RaiseCommand(this.#yve);
            }
            this.#h7.Start();
        }

        private void #aye(object #xhb, EventArgs #yhb)
        {
            ActiproSoftware.Drawing.Range range = this.#hye();
            if (this.#k1d == System.Windows.Forms.Orientation.Horizontal)
            {
                int num = Math.Max(range.Min, Math.Min(range.Max, this.#uve.X));
                this.#nye(num, false);
            }
            else
            {
                int num2 = Math.Max(range.Min, Math.Min(range.Max, this.#uve.Y));
                this.#nye(num2, false);
            }
        }

        private void #bye(object #xhb, EventArgs #yhb)
        {
            bool local1 = this.RaiseCommand(this.#Bve);
        }

        private void #cye(object #xhb, EventArgs #yhb)
        {
            bool local1 = this.RaiseCommand(this.#Cve);
        }

        private void #dye(object #xhb, EventArgs #yhb)
        {
            bool local1 = this.RaiseCommand(this.#Dve);
        }

        private void #eye(object #xhb, EventArgs #yhb)
        {
            bool local1 = this.RaiseCommand(this.#Eve);
        }

        private void #fye(object #xhb, EventArgs #yhb)
        {
            this.Value = this.Maximum;
            this.#u5j(new ScrollEventArgs(ScrollEventType.Last, this.Value));
        }

        private void #gye(object #xhb, EventArgs #yhb)
        {
            this.Value = this.Minimum;
            this.#u5j(new ScrollEventArgs(ScrollEventType.First, this.Value));
        }

        internal ActiproSoftware.Drawing.Range #hye() => 
            (this.#k1d != System.Windows.Forms.Orientation.Horizontal) ? new ActiproSoftware.Drawing.Range(this.#vve.Bounds.Bottom, this.#wve.Bounds.Top - this.#Ave.Bounds.Height) : new ActiproSoftware.Drawing.Range(this.#vve.Bounds.Right, this.#wve.Bounds.Left - this.#Ave.Bounds.Width);

        private void #J6d(object #xhb, EventArgs #yhb)
        {
            this.Invalidate(InvalidationLevels.ElementAndChildren, InvalidationTypes.All);
        }

        internal void #jye(EventArgs #yhb)
        {
            this.OnThumbScrolled(#yhb);
            this.#u5j(new ScrollEventArgs(ScrollEventType.EndScroll, this.Value));
        }

        internal void #kye(EventArgs #yhb)
        {
            this.OnThumbScrolling(#yhb);
        }

        internal void #lye(ActiproSoftware.WinUICore.Commands.Command #POd)
        {
            this.#h7.Stop();
            this.#yve = #POd;
            this.RaiseCommand(this.#yve);
            if (ReferenceEquals(this.#yve, this.#Bve))
            {
                base.Invalidate(this.BarrelBeforeThumbBounds);
            }
            else if (ReferenceEquals(this.#yve, this.#Dve))
            {
                base.Invalidate(this.BarrelAfterThumbBounds);
            }
            this.#h7.Interval = 400;
            this.#h7.Start();
        }

        internal void #mye()
        {
            this.#h7.Stop();
            if (ReferenceEquals(this.#yve, this.#Bve))
            {
                base.Invalidate(this.BarrelBeforeThumbBounds);
            }
            else if (ReferenceEquals(this.#yve, this.#Dve))
            {
                base.Invalidate(this.BarrelAfterThumbBounds);
            }
            this.#yve = null;
        }

        internal void #nye(int #bVe, bool #v5j)
        {
            if (this.#k1d == System.Windows.Forms.Orientation.Horizontal)
            {
                float num = Math.Max(0f, Math.Min((float) 1f, (float) (((float) (#bVe - this.#vve.Bounds.Right)) / ((float) Math.Max(1, this.#tve - this.#Ave.Bounds.Width)))));
                this.Value = this.#wD + ((int) (num * (this.#vD - this.#wD)));
            }
            else
            {
                float num2 = Math.Max(0f, Math.Min((float) 1f, (float) (((float) (#bVe - this.#vve.Bounds.Bottom)) / ((float) Math.Max(1, this.#tve - this.#Ave.Bounds.Height)))));
                this.Value = this.#wD + ((int) (num2 * (this.#vD - this.#wD)));
            }
            this.#u5j(new ScrollEventArgs(#v5j ? ScrollEventType.ThumbTrack : ScrollEventType.ThumbPosition, this.Value));
        }

        internal void #oye(int #Zn, int #0n)
        {
            ContextMenu menu = new ContextMenu {
                MenuItems = { new MenuItem(ActiproSoftware.Products.Shared.SR.GetString(#G.#eg(0x170)), new EventHandler(this.#aye)) }
            };
            if (this.#k1d == System.Windows.Forms.Orientation.Horizontal)
            {
                menu.MenuItems.Add(new MenuItem(#G.#eg(0x5c)));
                menu.MenuItems.Add(new MenuItem(ActiproSoftware.Products.Shared.SR.GetString(#G.#eg(0x1a5)), new EventHandler(this.#gye)));
                menu.MenuItems.Add(new MenuItem(ActiproSoftware.Products.Shared.SR.GetString(#G.#eg(470)), new EventHandler(this.#fye)));
                menu.MenuItems.Add(new MenuItem(#G.#eg(0x5c)));
                menu.MenuItems.Add(new MenuItem(ActiproSoftware.Products.Shared.SR.GetString(#G.#eg(0x207)), new EventHandler(this.#bye)));
                menu.MenuItems.Add(new MenuItem(ActiproSoftware.Products.Shared.SR.GetString(#G.#eg(0x238)), new EventHandler(this.#dye)));
                menu.MenuItems.Add(new MenuItem(#G.#eg(0x5c)));
                menu.MenuItems.Add(new MenuItem(ActiproSoftware.Products.Shared.SR.GetString(#G.#eg(0x269)), new EventHandler(this.#cye)));
                menu.MenuItems.Add(new MenuItem(ActiproSoftware.Products.Shared.SR.GetString(#G.#eg(670)), new EventHandler(this.#eye)));
            }
            else
            {
                menu.MenuItems.Add(new MenuItem(#G.#eg(0x5c)));
                menu.MenuItems.Add(new MenuItem(ActiproSoftware.Products.Shared.SR.GetString(#G.#eg(0x2d3)), new EventHandler(this.#gye)));
                menu.MenuItems.Add(new MenuItem(ActiproSoftware.Products.Shared.SR.GetString(#G.#eg(0x2fc)), new EventHandler(this.#fye)));
                menu.MenuItems.Add(new MenuItem(#G.#eg(0x5c)));
                menu.MenuItems.Add(new MenuItem(ActiproSoftware.Products.Shared.SR.GetString(#G.#eg(0x329)), new EventHandler(this.#bye)));
                menu.MenuItems.Add(new MenuItem(ActiproSoftware.Products.Shared.SR.GetString(#G.#eg(0x356)), new EventHandler(this.#dye)));
                menu.MenuItems.Add(new MenuItem(#G.#eg(0x5c)));
                menu.MenuItems.Add(new MenuItem(ActiproSoftware.Products.Shared.SR.GetString(#G.#eg(0x387)), new EventHandler(this.#cye)));
                menu.MenuItems.Add(new MenuItem(ActiproSoftware.Products.Shared.SR.GetString(#G.#eg(0x3b8)), new EventHandler(this.#eye)));
            }
            this.#uve = new Point(#Zn, #0n);
            menu.Show(this, this.#uve);
        }

        private void #t6d()
        {
            this.#tZd = true;
        }

        private void #u5j(ScrollEventArgs #yhb)
        {
            this.OnScrolled(#yhb);
        }

        private void #u6d()
        {
            this.#tZd = false;
            this.Invalidate(InvalidationLevels.ElementAndChildren, InvalidationTypes.All);
        }

        public ScrollBar()
        {
            CommandLink commandLink = new CommandLink(this.#Cve);
            this.#ZYd.Add(commandLink);
            CommandLink link2 = new CommandLink(this.#Eve);
            this.#ZYd.Add(link2);
            this.#vve = new ScrollBarButton(commandLink);
            this.Children.Add(this.#vve);
            this.#wve = new ScrollBarButton(link2);
            this.Children.Add(this.#wve);
            this.#Ave = new ScrollBarThumb();
            this.Children.Add(this.#Ave);
            this.#h7 = new System.Windows.Forms.Timer();
            this.#h7.Tick += new EventHandler(this.#7Yb);
            this.#7Xd = new ToolTip();
            this.#7Xd.SetToolTip(this, null);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            double num = 1.0;
            if ((this.#vD - this.#wD) > 0)
            {
                num = ((double) this.#Ld) / ((double) (this.#vD - this.#wD));
            }
            Point location = new Point();
            Rectangle rectangle = new Rectangle(location, finalSize);
            if (this.#k1d == System.Windows.Forms.Orientation.Horizontal)
            {
                if (rectangle.Width >= (this.#vve.DesiredSize.Width + this.#wve.DesiredSize.Width))
                {
                    this.#vve.Arrange(new Rectangle(rectangle.Left, rectangle.Top, this.#vve.DesiredSize.Width, rectangle.Height));
                    this.#wve.Arrange(new Rectangle(rectangle.Right - this.#wve.DesiredSize.Width, rectangle.Top, this.#wve.DesiredSize.Width, rectangle.Height));
                }
                else
                {
                    int width = rectangle.Width / 2;
                    this.#vve.Arrange(new Rectangle(rectangle.Left, rectangle.Top, width, rectangle.Height));
                    this.#wve.Arrange(new Rectangle(width, rectangle.Top, rectangle.Width - width, rectangle.Height));
                }
                this.#tve = Math.Max(0, (finalSize.Width - this.#vve.DesiredSize.Width) - this.#wve.DesiredSize.Width);
                this.#Ave.Measure(null, new Size(this.#tve, finalSize.Height));
                int x = this.#vve.Bounds.Right + ((int) (num * (this.#tve - this.#Ave.DesiredSize.Width)));
                this.#Ave.Arrange(new Rectangle(x, rectangle.Top, this.#Ave.DesiredSize.Width, rectangle.Height));
                this.#Ave.Visibility = (rectangle.Width > (this.#vve.Bounds.Width + this.#wve.Bounds.Width)) ? Visibility.Visible : Visibility.Collapsed;
            }
            else
            {
                if (rectangle.Height >= (this.#vve.DesiredSize.Height + this.#wve.DesiredSize.Height))
                {
                    this.#vve.Arrange(new Rectangle(rectangle.Left, rectangle.Top, rectangle.Width, this.#vve.DesiredSize.Height));
                    this.#wve.Arrange(new Rectangle(rectangle.Left, rectangle.Bottom - this.#wve.DesiredSize.Height, rectangle.Width, this.#wve.DesiredSize.Height));
                }
                else
                {
                    int height = rectangle.Height / 2;
                    this.#vve.Arrange(new Rectangle(rectangle.Left, rectangle.Top, rectangle.Width, height));
                    this.#wve.Arrange(new Rectangle(rectangle.Left, height, rectangle.Width, rectangle.Height - height));
                }
                this.#tve = Math.Max(0, (finalSize.Height - this.#vve.DesiredSize.Height) - this.#wve.DesiredSize.Height);
                this.#Ave.Measure(null, new Size(finalSize.Width, this.#tve));
                int y = this.#vve.Bounds.Bottom + ((int) (num * (this.#tve - this.#Ave.DesiredSize.Height)));
                this.#Ave.Arrange(new Rectangle(rectangle.Left, y, rectangle.Width, this.#Ave.DesiredSize.Height));
                this.#Ave.Visibility = (rectangle.Height > (this.#vve.Bounds.Height + this.#wve.Bounds.Height)) ? Visibility.Visible : Visibility.Collapsed;
            }
            return finalSize;
        }

        protected override IList CreateChildren() => 
            new UIElementCollection(this);

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.#h7 != null))
            {
                this.#h7.Stop();
                this.#h7.Tick -= new EventHandler(this.#7Yb);
                this.#h7.Dispose();
                this.#h7 = null;
            }
            base.Dispose(disposing);
            if (disposing)
            {
                if (this.#IZd != null)
                {
                    this.#IZd.PropertyChanged -= new EventHandler(this.#J6d);
                    this.#IZd = null;
                }
                this.#45d(false);
            }
        }

        public static Sides GetSidesFromOrientation(System.Windows.Forms.Orientation orientation) => 
            (orientation != System.Windows.Forms.Orientation.Horizontal) ? Sides.Left : Sides.Top;

        protected override Size MeasureOverride(Graphics g, Size availableSize)
        {
            this.#vve.Measure(g, availableSize);
            this.#wve.Measure(g, availableSize);
            if (this.#k1d == System.Windows.Forms.Orientation.Horizontal)
            {
                this.#tve = Math.Max(0, (availableSize.Width - this.#vve.DesiredSize.Width) - this.#wve.DesiredSize.Width);
                this.#Ave.Measure(g, new Size(this.#tve, availableSize.Height));
            }
            else
            {
                this.#tve = Math.Max(0, (availableSize.Height - this.#vve.DesiredSize.Height) - this.#wve.DesiredSize.Height);
                this.#Ave.Measure(g, new Size(availableSize.Width, this.#tve));
            }
            return availableSize;
        }

        protected virtual void OnCommand(CommandEventArgs e)
        {
            if (this.#POd != null)
            {
                this.#POd(this, e);
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (this.#Ave.#rye())
            {
                this.Invalidate();
            }
            base.OnMouseDown(e);
            if ((e.Button != MouseButtons.Left) || !base.ClientRectangle.Contains(e.X, e.Y))
            {
                if (e.Button == MouseButtons.Right)
                {
                    this.#oye(e.X, e.Y);
                }
            }
            else if (((IUIElement) this).HitTest(new PointHitTestParameters(new Point(e.X, e.Y))).Element == null)
            {
                if (this.#k1d == System.Windows.Forms.Orientation.Horizontal)
                {
                    if (e.X < this.#Ave.Bounds.Left)
                    {
                        this.#lye(this.#Bve);
                    }
                    else if (e.X >= this.#Ave.Bounds.Right)
                    {
                        this.#lye(this.#Dve);
                    }
                }
                else if (e.Y < this.#Ave.Bounds.Top)
                {
                    this.#lye(this.#Bve);
                }
                else if (e.Y >= this.#Ave.Bounds.Bottom)
                {
                    this.#lye(this.#Dve);
                }
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            this.#mye();
            this.OnMouseUp(e);
        }

        protected override void OnRender(PaintEventArgs e)
        {
            this.RendererResolved.DrawScrollBarBackground(e, base.ClientRectangle, this);
        }

        protected virtual void OnScrolled(ScrollEventArgs e)
        {
            if (this.#t5j != null)
            {
                this.#t5j(this, e);
            }
        }

        protected virtual void OnThumbScrolled(EventArgs e)
        {
            if (this.#Fve != null)
            {
                this.#Fve(this, e);
            }
        }

        protected virtual void OnThumbScrolling(EventArgs e)
        {
            if (this.#Gve != null)
            {
                this.#Gve(this, e);
            }
        }

        protected virtual void OnValueChanged(EventArgs e)
        {
            if (this.#Hve != null)
            {
                this.#Hve(this, e);
            }
        }

        public virtual bool RaiseCommand(ActiproSoftware.WinUICore.Commands.Command command)
        {
            if (this.#oZd != null)
            {
                return this.#oZd.RaiseCommand(command);
            }
            if (command.Name == this.#Bve.Name)
            {
                this.Value = Math.Max(this.#wD, this.#Ld - this.#xve);
                this.#u5j(new ScrollEventArgs(ScrollEventType.LargeDecrement, this.Value));
            }
            else if (command.Name == this.#Cve.Name)
            {
                this.Value = Math.Max(this.#wD, this.#Ld - this.#zve);
                this.#u5j(new ScrollEventArgs(ScrollEventType.SmallDecrement, this.Value));
            }
            else if (command.Name == this.#Dve.Name)
            {
                this.Value = Math.Min(this.#vD, this.#Ld + this.#xve);
                this.#u5j(new ScrollEventArgs(ScrollEventType.LargeIncrement, this.Value));
            }
            else if (command.Name != this.#Eve.Name)
            {
                this.OnCommand(new CommandEventArgs(command));
            }
            else
            {
                this.Value = Math.Min(this.#vD, this.#Ld + this.#zve);
                this.#u5j(new ScrollEventArgs(ScrollEventType.SmallIncrement, this.Value));
            }
            return true;
        }

        public void SetData(int minimum, int maximum, int smallChange, int largeChange, bool enabled)
        {
            if (this.#wD != minimum)
            {
                this.#wD = minimum;
                if (maximum < minimum)
                {
                    maximum = minimum;
                }
                if (minimum > this.#Ld)
                {
                    this.#Ld = minimum;
                }
            }
            if (this.#vD != maximum)
            {
                this.#vD = maximum;
                if (minimum > maximum)
                {
                    minimum = maximum;
                }
                if (maximum < this.#Ld)
                {
                    this.#Ld = maximum;
                }
            }
            this.#zve = smallChange;
            this.#xve = largeChange;
            if (base.Enabled != enabled)
            {
                base.Enabled = enabled;
            }
            if ((((this.#wD != minimum) || ((this.#vD != maximum) || ((this.#zve == smallChange) || (this.#xve == largeChange)))) || (base.Enabled == enabled)) && (this.#Ave != null))
            {
                this.#Ave.InvalidateMeasure();
            }
        }

        internal int LargeChangeCore =>
            this.#xve;

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Rectangle BarrelBeforeThumbBounds =>
            (this.#k1d != System.Windows.Forms.Orientation.Horizontal) ? new Rectangle(base.ClientRectangle.Left, this.#vve.Bounds.Bottom, base.ClientRectangle.Width, this.#Ave.Bounds.Top - this.#vve.Bounds.Bottom) : new Rectangle(this.#vve.Bounds.Right, base.ClientRectangle.Top, this.#Ave.Bounds.Left - this.#vve.Bounds.Right, base.ClientRectangle.Height);

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Rectangle BarrelAfterThumbBounds =>
            (this.#k1d != System.Windows.Forms.Orientation.Horizontal) ? new Rectangle(base.ClientRectangle.Left, this.#Ave.Bounds.Bottom, base.ClientRectangle.Width, this.#wve.Bounds.Top - this.#Ave.Bounds.Bottom) : new Rectangle(this.#Ave.Bounds.Right, base.ClientRectangle.Top, this.#wve.Bounds.Left - this.#Ave.Bounds.Right, base.ClientRectangle.Height);

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public CommandLinkCollection CommandLinks =>
            this.#ZYd;

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ActiproSoftware.WinUICore.Commands.Command CurrentScrollCommand =>
            this.#yve;

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ActiproSoftware.WinUICore.Commands.Command DecreaseLargeCommand =>
            this.#Bve;

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ActiproSoftware.WinUICore.Commands.Command DecreaseSmallCommand =>
            this.#Cve;

        protected override Size DefaultSize =>
            new Size(300, SystemInformation.HorizontalScrollBarHeight);

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ICommandTarget ForwardCommandsTo
        {
            get => 
                this.#oZd;
            set => 
                this.#oZd = value;
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ActiproSoftware.WinUICore.Commands.Command IncreaseLargeCommand =>
            this.#Dve;

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ActiproSoftware.WinUICore.Commands.Command IncreaseSmallCommand =>
            this.#Eve;

        [Category("Behavior"), Description("The value to be added to or subtracted from to the Value property when the scroll box is moved a large distance."), DefaultValue(10)]
        public int LargeChange
        {
            get => 
                Math.Min(this.#xve, (this.#vD - this.#wD) + 1);
            set
            {
                if (this.#xve != value)
                {
                    if (value <= 0)
                    {
                        throw new ArgumentOutOfRangeException();
                    }
                    this.#xve = value;
                    if (this.#Ave != null)
                    {
                        this.#Ave.InvalidateMeasure();
                    }
                }
            }
        }

        [Category("Behavior"), Description("The upper limit of values of the scrollable range."), DefaultValue(100)]
        public int Maximum
        {
            get => 
                this.#vD;
            set
            {
                if (this.#vD != value)
                {
                    this.#vD = value;
                    if (this.#wD > value)
                    {
                        this.#wD = value;
                    }
                    if (value < this.#Ld)
                    {
                        this.Value = value;
                    }
                    if (this.#Ave != null)
                    {
                        this.#Ave.InvalidateMeasure();
                    }
                }
            }
        }

        [Category("Behavior"), Description("The lower limit of values of the scrollable range."), DefaultValue(0)]
        public int Minimum
        {
            get => 
                this.#wD;
            set
            {
                if (this.#wD != value)
                {
                    this.#wD = value;
                    if (this.#vD < value)
                    {
                        this.#vD = value;
                    }
                    if (value > this.#Ld)
                    {
                        this.Value = value;
                    }
                    if (this.#Ave != null)
                    {
                        this.#Ave.InvalidateMeasure();
                    }
                }
            }
        }

        [Category("Appearance"), Description("The orientation of the scrollbar."), DefaultValue(0)]
        public System.Windows.Forms.Orientation Orientation
        {
            get => 
                this.#k1d;
            set
            {
                if (this.#k1d != value)
                {
                    this.#k1d = value;
                    if (!this.#tZd)
                    {
                        base.Size = new Size(base.Height, base.Width);
                    }
                    base.InvalidateArrange();
                }
            }
        }

        [Category("Appearance"), Description("The control-specific IScrollBarRenderer used to render the control."), DefaultValue((string) null)]
        public IScrollBarRenderer Renderer
        {
            get => 
                this.#IZd;
            set
            {
                if (!ReferenceEquals(this.#IZd, value))
                {
                    if (this.#IZd != null)
                    {
                        this.#IZd.PropertyChanged -= new EventHandler(this.#J6d);
                    }
                    this.#IZd = value;
                    if (this.#IZd != null)
                    {
                        this.#45d(false);
                    }
                    if (this.#IZd != null)
                    {
                        this.#IZd.PropertyChanged += new EventHandler(this.#J6d);
                    }
                    base.Invalidate(InvalidationLevels.ElementAndChildren, InvalidationTypes.All);
                }
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IScrollBarRenderer RendererResolved
        {
            get
            {
                if (this.#IZd != null)
                {
                    return this.#IZd;
                }
                ActiproSoftware.WinUICore.ScrollBar bar = this;
                lock (bar)
                {
                    if (!this.#4Xd)
                    {
                        this.#45d(true);
                    }
                    return (IScrollBarRenderer) UIRendererManager.GetRendererData(typeof(IScrollBarRenderer)).Renderer;
                }
            }
        }

        [Category("Behavior"), Description("The value to be added to or subtracted from to the Value property when the scroll box is moved a small distance."), DefaultValue(1)]
        public int SmallChange
        {
            get => 
                Math.Min(this.#zve, this.LargeChange);
            set
            {
                if (this.#zve != value)
                {
                    if (value <= 0)
                    {
                        throw new ArgumentOutOfRangeException();
                    }
                    this.#zve = value;
                    if (this.#Ave != null)
                    {
                        this.#Ave.InvalidateMeasure();
                    }
                }
            }
        }

        [Category("Behavior"), Description("A numeric value that represents the current position of the scroll box on the scroll bar control."), DefaultValue(0)]
        public int Value
        {
            get => 
                this.#Ld;
            set
            {
                if (this.#Ld != value)
                {
                    this.#Ld = Math.Max(this.#wD, Math.Min(this.#vD, value));
                    base.ResumePainting();
                    Rectangle bounds = this.#Ave.Bounds;
                    base.InvalidateArrange();
                    this.OnValueChanged(EventArgs.Empty);
                }
            }
        }
    }
}

