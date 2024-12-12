namespace #Fqe
{
    using #aXd;
    using ActiproSoftware.Drawing;
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    internal class #Eqe : IDisposable, IDoubleBufferCanvas
    {
        private Rectangle #4qe;
        private Control #lp;
        private System.Drawing.Graphics #5qe;
        private IntPtr #6qe = IntPtr.Zero;
        private IntPtr #7qe = IntPtr.Zero;
        private IntPtr #8qe = IntPtr.Zero;
        private IntPtr #Qqb = IntPtr.Zero;

        public #Eqe(Control control)
        {
            this.#lp = control;
        }

        public void Copy(Rectangle #3Zf, Point #4Zf)
        {
            #Bi.#xxe(this.#8qe, #4Zf.X, #4Zf.Y, #3Zf.Width, #3Zf.Height, this.#8qe, #3Zf.Left, #3Zf.Top, 0xcc0020);
        }

        private void CreateGraphics()
        {
            this.#Qqb = this.#lp.Handle;
            this.#7qe = #Bi.#7oe(this.#Qqb);
            this.#8qe = #Bi.#zxe(this.#7qe);
            this.#6qe = #Bi.#yxe(this.#7qe, this.#lp.ClientSize.Width, this.#lp.ClientSize.Height);
            #Bi.#bpe(this.#8qe, this.#6qe);
            if (this.#8qe != IntPtr.Zero)
            {
                this.#5qe = System.Drawing.Graphics.FromHdc(this.#8qe);
            }
            else
            {
                this.Reset();
            }
        }

        public void Dispose()
        {
            this.Reset();
            this.#lp = null;
        }

        public void Flush()
        {
            #Bi.#xxe(this.#7qe, this.#4qe.Left, this.#4qe.Top, this.#4qe.Width, this.#4qe.Height, this.#8qe, this.#4qe.Left, this.#4qe.Top, 0xcc0020);
        }

        public void Invert(Rectangle #Bo)
        {
            #Bi.#xxe(this.#8qe, #Bo.Left, #Bo.Top, #Bo.Width, #Bo.Height, this.#8qe, #Bo.X, #Bo.Y, 0x550009);
        }

        public void PrepareGraphics(PaintEventArgs #yhb)
        {
            this.#4qe = #yhb.ClipRectangle;
            if (this.#5qe == null)
            {
                this.CreateGraphics();
            }
            this.#5qe.Clip = #yhb.Graphics.Clip;
        }

        public void Reset()
        {
            try
            {
                if (this.#5qe != null)
                {
                    this.#5qe.Dispose();
                    this.#5qe = null;
                }
            }
            catch
            {
            }
            try
            {
                if (this.#6qe != IntPtr.Zero)
                {
                    #Bi.#5oe(this.#6qe);
                    this.#6qe = IntPtr.Zero;
                }
            }
            catch
            {
            }
            try
            {
                if (this.#8qe != IntPtr.Zero)
                {
                    #Bi.#5oe(this.#8qe);
                    this.#8qe = IntPtr.Zero;
                }
            }
            catch
            {
            }
            try
            {
                if (this.#7qe != IntPtr.Zero)
                {
                    #Bi.#9oe(this.#Qqb, this.#7qe);
                    this.#7qe = IntPtr.Zero;
                }
            }
            catch
            {
            }
        }

        public System.Drawing.Graphics Graphics =>
            this.#5qe;
    }
}

