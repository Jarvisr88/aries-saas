namespace ActiproSoftware.WinUICore
{
    using #aXd;
    using #H;
    using ActiproSoftware.Drawing;
    using System;
    using System.Collections;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Windows.Forms;

    public class PopupControl : Form
    {
        private static bool #9ue = true;
        private IntPtr #s0d = IntPtr.Zero;
        private bool #ave = true;
        private int #bve;
        private ArrayList #cve = new ArrayList();
        private System.Windows.Forms.Timer #dve;
        private bool #eve = true;
        private Control #cK;
        private bool #ive;
        private IntPtr #jve = IntPtr.Zero;
        private PopupControl #kve;
        private bool #lve;

        public event EventHandler Displayed;

        public event EventHandler Displaying;

        public event EventHandler Hidden;

        public event EventHandler Hiding;

        private PopupControl #0xe(IntPtr #Qqb)
        {
            if (#Qqb == IntPtr.Zero)
            {
                return null;
            }
            Control control = FromHandle(#Qqb);
            return ((control != null) ? (control.TopLevelControl as PopupControl) : null);
        }

        private PopupControl #1xe()
        {
            PopupControl objA = this;
            while (objA.ParentPopup != null)
            {
                if (ReferenceEquals(objA, objA.ParentPopup))
                {
                    return objA;
                }
                objA = objA.ParentPopup;
            }
            return objA;
        }

        private bool #2xe(IntPtr #Qqb)
        {
            PopupControl objB = this.#0xe(#Qqb);
            if (objB != null)
            {
                PopupControl parentPopup = this.ParentPopup;
                byte num = 0;
                while (parentPopup != null)
                {
                    num = (byte) (num + 1);
                    if (num == 0xff)
                    {
                        return false;
                    }
                    if (ReferenceEquals(parentPopup, objB))
                    {
                        return true;
                    }
                    parentPopup = parentPopup.ParentPopup;
                }
            }
            return false;
        }

        private bool #3xe(IntPtr #Qqb)
        {
            PopupControl control = this.#0xe(#Qqb);
            if (control != null)
            {
                PopupControl parentPopup = control.ParentPopup;
                byte num = 0;
                while (parentPopup != null)
                {
                    num = (byte) (num + 1);
                    if (num == 0xff)
                    {
                        return false;
                    }
                    if (ReferenceEquals(parentPopup, this))
                    {
                        return true;
                    }
                    parentPopup = parentPopup.ParentPopup;
                }
            }
            return false;
        }

        private void #9xe()
        {
            if ((this.#dve != null) && this.#dve.Enabled)
            {
                this.#dve.Enabled = false;
                this.#dve.Interval = this.#bve;
                this.#dve.Enabled = true;
            }
        }

        private void #E8d(object #xhb, EventArgs #yhb)
        {
            this.#s0d = ((Control) #xhb).Handle;
        }

        private void #Ibi()
        {
            if (this.#jve != IntPtr.Zero)
            {
                this.ActiveToplevelHwnd = this.#jve;
            }
            else
            {
                this.ActiveToplevelHwnd = #Bi.#Exe();
            }
        }

        private void #Txe(object #xhb, EventArgs #yhb)
        {
            this.#dve.Enabled = false;
            this.HidePopup();
        }

        private void #Vxe()
        {
            this.#Wxe();
            this.#dve = new System.Windows.Forms.Timer();
            this.#dve.Interval = this.#bve;
            this.#dve.Tick += new EventHandler(this.#Txe);
            this.#dve.Enabled = true;
        }

        private void #Wxe()
        {
            if (this.#dve != null)
            {
                if (0 == 0)
                {
                    this.#dve.Enabled = false;
                }
                else
                {
                    System.Windows.Forms.Timer local2 = this.#dve;
                }
                this.#dve.Dispose();
                this.#dve = null;
            }
        }

        private void #Xxe()
        {
            PopupControl control1 = this.#1xe();
            PopupControl control2 = this.#1xe();
            control2.#Yxe();
            control2.HidePopup();
        }

        private void #Yxe()
        {
            foreach (PopupControl control in this.ChildPopups)
            {
                if (!ReferenceEquals(control, this) && control.Visible)
                {
                    control.HidePopup();
                }
            }
        }

        private void #Zxe(IntPtr #v0f)
        {
            if ((#v0f == IntPtr.Zero) && !this.#lve)
            {
                this.#Xxe();
            }
        }

        public PopupControl()
        {
            base.ControlBox = false;
            base.FormBorderStyle = FormBorderStyle.None;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            this.MinimumSize = new Size(1, 1);
            base.Name = #G.#eg(0x61);
            base.ShowInTaskbar = false;
            base.SizeGripStyle = SizeGripStyle.Hide;
            base.StartPosition = FormStartPosition.Manual;
            this.Text = null;
            base.TopLevel = true;
            base.Visible = false;
            base.UpdateStyles();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.#Wxe();
                IntPtr activeToplevelHwnd = this.ActiveToplevelHwnd;
                if (activeToplevelHwnd != IntPtr.Zero)
                {
                    Control control = FromHandle(activeToplevelHwnd);
                    if (control != null)
                    {
                        control.HandleCreated -= new EventHandler(this.#E8d);
                    }
                }
            }
            base.Dispose(disposing);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public virtual void HidePopup()
        {
            if (this.Visible)
            {
                this.#Wxe();
                this.OnHiding(EventArgs.Empty);
                base.Hide();
                this.OnHidden(EventArgs.Empty);
            }
        }

        public void NegotiateLocation(Rectangle exclusionBounds, int offset)
        {
            this.NegotiateLocation(exclusionBounds, Corner.LowerLeft, Orientation.Vertical, offset);
        }

        public void NegotiateLocation(Rectangle exclusionBounds, Corner exclusionCorner, Orientation orientation, int offset)
        {
            Point point;
            Screen primaryScreen = Screen.FromRectangle(exclusionBounds);
            Size size = base.Size;
            if (this.RightToLeft == RightToLeft.Yes)
            {
                switch (exclusionCorner)
                {
                    case Corner.UpperLeft:
                        exclusionCorner = Corner.UpperRight;
                        break;

                    case Corner.UpperRight:
                        exclusionCorner = Corner.UpperLeft;
                        break;

                    case Corner.LowerRight:
                        exclusionCorner = Corner.LowerLeft;
                        break;

                    default:
                        exclusionCorner = Corner.LowerRight;
                        break;
                }
            }
            if (orientation == Orientation.Horizontal)
            {
                switch (exclusionCorner)
                {
                    case Corner.UpperLeft:
                        if (((exclusionBounds.Left - offset) - size.Width) >= primaryScreen.WorkingArea.Left)
                        {
                            point = new Point((exclusionBounds.Left - offset) - size.Width, exclusionBounds.Top);
                        }
                        else
                        {
                            point = new Point(exclusionBounds.Right + offset, exclusionBounds.Top);
                        }
                        break;

                    case Corner.UpperRight:
                        if (((exclusionBounds.Right + offset) + size.Width) <= primaryScreen.WorkingArea.Right)
                        {
                            point = new Point(exclusionBounds.Right + offset, exclusionBounds.Top);
                        }
                        else
                        {
                            point = new Point((exclusionBounds.Left - offset) - size.Width, exclusionBounds.Top);
                        }
                        break;

                    case Corner.LowerRight:
                        if (((exclusionBounds.Right + offset) + size.Width) <= primaryScreen.WorkingArea.Right)
                        {
                            point = new Point(exclusionBounds.Right + offset, exclusionBounds.Bottom - size.Height);
                        }
                        else
                        {
                            point = new Point((exclusionBounds.Left - offset) - size.Width, exclusionBounds.Bottom - size.Height);
                        }
                        break;

                    default:
                        if (((exclusionBounds.Left - offset) - size.Width) >= primaryScreen.WorkingArea.Left)
                        {
                            point = new Point((exclusionBounds.Left - offset) - size.Width, exclusionBounds.Bottom - size.Height);
                        }
                        else
                        {
                            point = new Point(exclusionBounds.Right + offset, exclusionBounds.Bottom - size.Height);
                        }
                        break;
                }
            }
            else
            {
                switch (exclusionCorner)
                {
                    case Corner.UpperLeft:
                        if (((exclusionBounds.Top - offset) - size.Height) >= primaryScreen.WorkingArea.Top)
                        {
                            point = new Point(exclusionBounds.Left, (exclusionBounds.Top - offset) - size.Height);
                        }
                        else
                        {
                            point = new Point(exclusionBounds.Left, exclusionBounds.Bottom + offset);
                        }
                        break;

                    case Corner.UpperRight:
                        if (((exclusionBounds.Top - offset) - size.Height) >= primaryScreen.WorkingArea.Top)
                        {
                            point = new Point(exclusionBounds.Right - size.Width, (exclusionBounds.Top - offset) - size.Height);
                        }
                        else
                        {
                            point = new Point(exclusionBounds.Right - size.Width, exclusionBounds.Bottom + offset);
                        }
                        break;

                    case Corner.LowerRight:
                        if (((exclusionBounds.Bottom + offset) + size.Height) <= primaryScreen.WorkingArea.Bottom)
                        {
                            point = new Point(exclusionBounds.Right - size.Width, exclusionBounds.Bottom + offset);
                        }
                        else
                        {
                            point = new Point(exclusionBounds.Right - size.Width, (exclusionBounds.Top - offset) - size.Height);
                        }
                        break;

                    default:
                        if (((exclusionBounds.Bottom + offset) + size.Height) <= primaryScreen.WorkingArea.Bottom)
                        {
                            point = new Point(exclusionBounds.Left, exclusionBounds.Bottom + offset);
                        }
                        else
                        {
                            point = new Point(exclusionBounds.Left, (exclusionBounds.Top - offset) - size.Height);
                        }
                        break;
                }
            }
            Rectangle rectangle = new Rectangle(point, size);
            primaryScreen ??= Screen.PrimaryScreen;
            if (rectangle.Left < primaryScreen.WorkingArea.Left)
            {
                rectangle.Offset(primaryScreen.WorkingArea.Left - rectangle.Left, 0);
            }
            else if (rectangle.Right > primaryScreen.WorkingArea.Right)
            {
                rectangle.Offset(primaryScreen.WorkingArea.Right - rectangle.Right, 0);
            }
            if (rectangle.Top < primaryScreen.WorkingArea.Top)
            {
                rectangle.Offset(0, primaryScreen.WorkingArea.Top - rectangle.Top);
            }
            else if (rectangle.Bottom > primaryScreen.WorkingArea.Bottom)
            {
                rectangle.Offset(0, primaryScreen.WorkingArea.Bottom - rectangle.Bottom);
            }
            if (offset < 0)
            {
                exclusionBounds.Inflate(offset, offset);
            }
            if (rectangle.IntersectsWith(exclusionBounds))
            {
                if (orientation == Orientation.Horizontal)
                {
                    rectangle.Y = ((exclusionBounds.Top - primaryScreen.WorkingArea.Top) <= (primaryScreen.WorkingArea.Bottom - exclusionBounds.Bottom)) ? Math.Min((int) (primaryScreen.WorkingArea.Bottom - rectangle.Height), (int) (exclusionBounds.Bottom + offset)) : Math.Max(primaryScreen.WorkingArea.Top, (exclusionBounds.Top - rectangle.Height) - offset);
                }
                else
                {
                    rectangle.X = ((exclusionBounds.Left - primaryScreen.WorkingArea.Left) <= (primaryScreen.WorkingArea.Right - exclusionBounds.Right)) ? Math.Min((int) (primaryScreen.WorkingArea.Right - rectangle.Width), (int) (exclusionBounds.Right + offset)) : Math.Max(primaryScreen.WorkingArea.Left, (exclusionBounds.Left - rectangle.Width) - offset);
                }
            }
            base.Location = rectangle.Location;
        }

        protected virtual void OnDisplayed(EventArgs e)
        {
            if (this.#mve != null)
            {
                this.#mve(this, e);
            }
        }

        protected virtual void OnDisplaying(EventArgs e)
        {
            if (this.#nve != null)
            {
                this.#nve(this, e);
            }
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            try
            {
                this.#lve = true;
                this.OnHandleCreated(e);
            }
            finally
            {
                this.#lve = false;
            }
        }

        protected virtual void OnHidden(EventArgs e)
        {
            if (this.#ove != null)
            {
                this.#ove(this, e);
            }
        }

        protected virtual void OnHiding(EventArgs e)
        {
            if (this.#pve != null)
            {
                this.#pve(this, e);
            }
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            if (!this.Visible)
            {
                this.#Yxe();
            }
            base.OnVisibleChanged(e);
        }

        protected override bool ProcessCmdKey(ref Message m, Keys keyData)
        {
            if ((this.#lve || ((keyData & Keys.Alt) != Keys.Alt)) && ((keyData & Keys.KeyCode) != Keys.Escape))
            {
                return base.ProcessCmdKey(ref m, keyData);
            }
            this.HidePopup();
            return true;
        }

        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            Rectangle rect = new Rectangle(x, y, width, height);
            Rectangle workingArea = Screen.GetWorkingArea(rect);
            if (workingArea.Contains(rect))
            {
                base.SetBoundsCore(x, y, width, height, specified);
            }
            else
            {
                rect.Size = new Size(Math.Min(workingArea.Width - 2, rect.Width), Math.Min(workingArea.Height - 2, rect.Height));
                if (rect.Right > workingArea.Right)
                {
                    rect.X = workingArea.Right - rect.Width;
                }
                else if (rect.Left < workingArea.Left)
                {
                    rect.X = workingArea.Left;
                }
                if (rect.Bottom > workingArea.Bottom)
                {
                    rect.Y = (workingArea.Bottom - 1) - rect.Height;
                }
                else if (rect.Left < workingArea.Left)
                {
                    rect.Y = workingArea.Top;
                }
                base.SetBoundsCore(rect.X, rect.Y, rect.Width, rect.Height, specified);
            }
        }

        protected override void SetVisibleCore(bool visible)
        {
            try
            {
                this.#lve = true;
                if (visible)
                {
                    #Bi.#bA(this.Handle, 4);
                }
                base.SetVisibleCore(visible);
                PopupControl control = this.#1xe();
                if (visible && this.#ive)
                {
                    this.#ive = false;
                    #Bi.#aA(base.Handle, new IntPtr(base.TopMost ? -1 : 0), 0, 0, 0, 0, 0x513);
                }
                if (((control.ActiveToplevelHwnd != IntPtr.Zero) && visible) && !this.#ave)
                {
                    #Bi.#yAb(new HandleRef(this, base.Handle), -8, new HandleRef(null, control.ActiveToplevelHwnd));
                }
            }
            finally
            {
                this.#lve = false;
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public virtual void ShowPopup(Control owner, bool activate)
        {
            if (!this.Visible)
            {
                this.DialogResult = DialogResult.None;
                this.OwnerControl = owner;
                this.#Ibi();
                this.OnDisplaying(EventArgs.Empty);
                base.Show();
                if (activate)
                {
                    base.Focus();
                }
                this.OnDisplayed(EventArgs.Empty);
                if (this.#bve > 0)
                {
                    this.#Vxe();
                }
            }
        }

        protected override void WndProc(ref Message m)
        {
            int msg = m.Msg;
            if (msg == 0x24)
            {
                base.WndProc(ref m);
                #Bi.#Blb lParam = (#Bi.#Blb) m.GetLParam(typeof(#Bi.#Blb));
                lParam.#Yqb = new #Bi.#Ei(1, 1);
                Marshal.StructureToPtr(lParam, m.LParam, false);
            }
            else
            {
                if (msg == 0x81)
                {
                    this.#Ibi();
                }
                else if (msg == 0x86)
                {
                    if (m.WParam != IntPtr.Zero)
                    {
                        #Bi.#9z(new HandleRef(null, this.ActiveToplevelHwnd), 0x86, (IntPtr) 1, #Bi.#T4d);
                        m.WParam = (IntPtr) 1;
                        base.DefWndProc(ref m);
                        return;
                    }
                    if (!this.#ave)
                    {
                        PopupControl control = this.#1xe();
                        if (control == null)
                        {
                            base.WndProc(ref m);
                            return;
                        }
                        control.#Zxe(m.LParam);
                    }
                    else if (!this.#lve)
                    {
                        base.DefWndProc(ref m);
                        if (m.LParam == IntPtr.Zero)
                        {
                            this.#Xxe();
                            return;
                        }
                        if (this.#3xe(m.LParam))
                        {
                            return;
                        }
                        if (this.#2xe(m.LParam))
                        {
                            this.#0xe(m.LParam).#Yxe();
                            return;
                        }
                        if (base.Handle != m.LParam)
                        {
                            this.#Xxe();
                            return;
                        }
                    }
                }
                base.WndProc(ref m);
            }
        }

        private IntPtr ActiveToplevelHwnd
        {
            get => 
                this.#s0d;
            set
            {
                if (this.#s0d != value)
                {
                    if (this.#s0d != IntPtr.Zero)
                    {
                        Control control = FromHandle(this.#s0d);
                        if (control != null)
                        {
                            control.HandleCreated -= new EventHandler(this.#E8d);
                        }
                    }
                    this.#s0d = value;
                    if (this.#s0d != IntPtr.Zero)
                    {
                        Control control2 = FromHandle(this.#s0d);
                        if (control2 != null)
                        {
                            control2.HandleCreated += new EventHandler(this.#E8d);
                        }
                    }
                }
            }
        }

        private ArrayList ChildPopups =>
            this.#cve;

        private bool IsTopmostPopup =>
            this.ParentPopup == null;

        private Control OwnerControl
        {
            get => 
                this.#cK;
            set
            {
                this.#cK = value;
                if (this.#cK == null)
                {
                    this.#jve = IntPtr.Zero;
                    this.ParentPopup = null;
                }
                else
                {
                    Control topLevelControl = this.#cK.TopLevelControl;
                    if (topLevelControl is Form)
                    {
                        if (this.#jve != topLevelControl.Handle)
                        {
                            this.#ive = true;
                        }
                        this.#jve = topLevelControl.Handle;
                    }
                    else
                    {
                        this.#jve = IntPtr.Zero;
                    }
                    if (topLevelControl is PopupControl)
                    {
                        this.ParentPopup = (PopupControl) this.#cK.TopLevelControl;
                    }
                    else
                    {
                        this.ParentPopup = null;
                    }
                }
            }
        }

        private PopupControl ParentPopup
        {
            get => 
                this.#kve;
            set
            {
                if (ReferenceEquals(value, this))
                {
                    throw new ArgumentException(#G.#eg(0x72), #G.#eg(0xa3));
                }
                if (!ReferenceEquals(this.#kve, value))
                {
                    if (this.#kve != null)
                    {
                        this.#kve.ChildPopups.Remove(this);
                    }
                    this.#kve = value;
                    if (this.#kve != null)
                    {
                        this.#kve.ChildPopups.Add(this);
                    }
                }
            }
        }

        protected virtual bool AutoClose
        {
            get => 
                this.#ave;
            set => 
                this.#ave = value;
        }

        protected override System.Windows.Forms.CreateParams CreateParams
        {
            get
            {
                System.Windows.Forms.CreateParams createParams = this.CreateParams;
                createParams.ExStyle |= 0x8000000;
                if (this.DropShadowEnabled && (Environment.OSVersion.Version >= new Version(5, 1)))
                {
                    createParams.ClassStyle |= 0x20000;
                }
                createParams.Parent = this.#jve;
                return createParams;
            }
        }

        public bool DropShadowEnabled
        {
            get => 
                this.#eve && #9ue;
            set
            {
                if (this.#eve != value)
                {
                    this.#eve = value;
                    if (base.IsHandleCreated)
                    {
                        base.RecreateHandle();
                    }
                }
            }
        }
    }
}

