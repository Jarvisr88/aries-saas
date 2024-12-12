namespace DMEWorks.Forms
{
    using DMEWorks.Forms.Properties;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public class DmeMdiParentForm : DmeForm
    {
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected override Control.ControlCollection CreateControlsInstance() => 
            new Form.ControlCollection(this);

        public class ControlCollection : Form.ControlCollection
        {
            private Hashtable hash;

            public ControlCollection(Form owner) : base(owner)
            {
            }

            public override void Add(Control value)
            {
                base.Add(value);
                if (value is MdiClient)
                {
                    value.Paint += new PaintEventHandler(this.MdiClientPaint);
                    value.Resize += new EventHandler(this.MdiClientResize);
                    this.hash ??= new Hashtable();
                    MdiClientNativeWindow window = new MdiClientNativeWindow(value);
                    this.hash.Add(value, window);
                }
            }

            private void MdiClientPaint(object sender, PaintEventArgs e)
            {
                MdiClient client = sender as MdiClient;
                if (client != null)
                {
                    Rectangle bounds = client.Bounds;
                    Image image = Resources.Logo80;
                    if (((image.Width + 100) <= bounds.Width) && ((image.Height + 100) <= bounds.Height))
                    {
                        e.Graphics.DrawImage(image, (bounds.Right - image.Width) - 50, (bounds.Bottom - image.Height) - 50, image.Width, image.Height);
                    }
                }
            }

            private void MdiClientResize(object sender, EventArgs e)
            {
                MdiClient client = sender as MdiClient;
                if (client != null)
                {
                    client.Invalidate();
                }
            }

            public override void Remove(Control value)
            {
                if (value is MdiClient)
                {
                    value.Paint -= new PaintEventHandler(this.MdiClientPaint);
                    value.Resize -= new EventHandler(this.MdiClientResize);
                    if (this.hash != null)
                    {
                        this.hash.Remove(value);
                        if (this.hash.Keys.Count <= 0)
                        {
                            this.hash = null;
                        }
                    }
                }
                base.Remove(value);
            }

            public class MdiClientNativeWindow : NativeWindow
            {
                private Control _control;
                public const int WM_HSCROLL = 0x114;
                public const int WM_VSCROLL = 0x115;

                public MdiClientNativeWindow(Control control)
                {
                    this._control = control;
                    if (!this._control.IsDisposed && this._control.IsHandleCreated)
                    {
                        base.AssignHandle(this._control.Handle);
                    }
                    this._control.HandleDestroyed += new EventHandler(this.OnHandleDestroyed);
                    this._control.HandleCreated += new EventHandler(this.OnHandleCreated);
                }

                ~MdiClientNativeWindow()
                {
                    this._control.HandleDestroyed -= new EventHandler(this.OnHandleDestroyed);
                    this._control.HandleCreated -= new EventHandler(this.OnHandleCreated);
                    this._control = null;
                }

                [DllImport("user32.dll", CharSet=CharSet.Auto, ExactSpelling=true)]
                public static extern bool InvalidateRect(IntPtr hWnd, IntPtr lprect, bool erase);
                private void OnHandleCreated(object sender, EventArgs e)
                {
                    base.AssignHandle(this._control.Handle);
                }

                private void OnHandleDestroyed(object sender, EventArgs e)
                {
                    this.ReleaseHandle();
                }

                protected override void WndProc(ref Message m)
                {
                    base.WndProc(ref m);
                    if ((m.Msg == 0x114) || (m.Msg == 0x115))
                    {
                        InvalidateRect(base.Handle, IntPtr.Zero, false);
                    }
                }
            }
        }
    }
}

