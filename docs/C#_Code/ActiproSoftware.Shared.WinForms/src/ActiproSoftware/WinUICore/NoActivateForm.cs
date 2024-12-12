namespace ActiproSoftware.WinUICore
{
    using #aXd;
    using #H;
    using System;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public abstract class NoActivateForm : Form
    {
        private IntPtr #s0d = IntPtr.Zero;

        private void #E8d(object #xhb, EventArgs #yhb)
        {
            this.#s0d = ((Control) #xhb).Handle;
        }

        public NoActivateForm()
        {
            base.SetStyle(ControlStyles.ResizeRedraw, true);
            base.SetStyle(ControlStyles.UserMouse, true);
            base.UpdateStyles();
            base.ControlBox = false;
            base.FormBorderStyle = FormBorderStyle.None;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            this.MinimumSize = new Size(1, 1);
            base.Name = #G.#eg(0x47);
            base.ShowInTaskbar = false;
            base.SizeGripStyle = SizeGripStyle.Hide;
            base.StartPosition = FormStartPosition.Manual;
            this.Text = #G.#eg(0x47);
            base.TabStop = false;
            base.TopLevel = true;
            base.TopMost = true;
            base.Visible = false;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
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

        protected override void SetVisibleCore(bool visible)
        {
            if (visible)
            {
                #Bi.#bA(this.Handle, 4);
            }
            base.SetVisibleCore(visible);
            if ((this.ActiveToplevelHwnd != IntPtr.Zero) && visible)
            {
                #Bi.#yAb(new HandleRef(this, base.Handle), -8, new HandleRef(null, this.ActiveToplevelHwnd));
            }
        }

        protected override void WndProc(ref Message m)
        {
            int msg = m.Msg;
            if (msg <= 0x81)
            {
                if (msg == 0x24)
                {
                    base.WndProc(ref m);
                    #Bi.#Blb lParam = (#Bi.#Blb) m.GetLParam(typeof(#Bi.#Blb));
                    lParam.#Yqb = new #Bi.#Ei(1, 1);
                    Marshal.StructureToPtr(lParam, m.LParam, false);
                    return;
                }
                if (msg == 0x81)
                {
                    this.ActiveToplevelHwnd = #Bi.#Exe();
                }
            }
            else
            {
                if (msg == 0x84)
                {
                    m.Result = new IntPtr(-1);
                    return;
                }
                if ((msg == 0x86) && (m.WParam != IntPtr.Zero))
                {
                    HandleRef ref1 = new HandleRef(null, this.ActiveToplevelHwnd);
                    #Bi.#9z(ref1, 0x86, (IntPtr) 1, #Bi.#T4d);
                    #Bi.#Hbe(ref1, null, #Bi.#U4d, 0x401);
                    m.WParam = (IntPtr) 1;
                    base.DefWndProc(ref m);
                    return;
                }
            }
            base.WndProc(ref m);
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
    }
}

