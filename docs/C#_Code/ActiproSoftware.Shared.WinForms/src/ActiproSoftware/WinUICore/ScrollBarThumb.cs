namespace ActiproSoftware.WinUICore
{
    using ActiproSoftware.Drawing;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class ScrollBarThumb : UIElement
    {
        private Point #3Jd;
        private Point #Qce;
        private Range #Ive;
        private bool #Cl;
        private int #fd;

        private bool #pye(int #Zn, int #0n)
        {
            this.#fd = (this.ScrollBar.Orientation != Orientation.Horizontal) ? (base.Bounds.Top - #0n) : (base.Bounds.Left - #Zn);
            this.#Qce = new Point(#Zn, #0n);
            this.#Ive = this.ScrollBar.#hye();
            if (this.#Ive.Min < this.#Ive.Max)
            {
                this.#Cl = true;
                this.ScrollBar.#kye(EventArgs.Empty);
            }
            return this.#Cl;
        }

        private void #qye(int #Zn, int #0n)
        {
            if (this.#Cl)
            {
                if (this.ScrollBar.Orientation == Orientation.Horizontal)
                {
                    int num = Math.Max(this.#Ive.Min, Math.Min(this.#Ive.Max, #Zn + this.#fd));
                    this.ScrollBar.#nye(num, true);
                }
                else
                {
                    int num2 = Math.Max(this.#Ive.Min, Math.Min(this.#Ive.Max, #0n + this.#fd));
                    this.ScrollBar.#nye(num2, true);
                }
            }
        }

        internal bool #rye()
        {
            if (!this.#Cl)
            {
                return false;
            }
            this.#Cl = false;
            this.ScrollBar.#jye(EventArgs.Empty);
            return true;
        }

        public UIElementDrawState GetDrawState()
        {
            UIElementDrawState none = UIElementDrawState.None;
            if (this.IsMouseDirectlyOver)
            {
                none |= UIElementDrawState.Hot;
            }
            if (this.IsMouseCaptured)
            {
                none |= UIElementDrawState.Pressed;
            }
            if (!this.ScrollBar.Enabled)
            {
                none |= UIElementDrawState.Disabled;
            }
            return none;
        }

        protected override Size MeasureOverride(Graphics g, Size availableSize)
        {
            double num = 1.0;
            if (((this.ScrollBar.Maximum - this.ScrollBar.Minimum) + this.ScrollBar.LargeChangeCore) > 0)
            {
                num = ((double) this.ScrollBar.LargeChangeCore) / ((double) ((this.ScrollBar.Maximum - this.ScrollBar.Minimum) + this.ScrollBar.LargeChangeCore));
            }
            return ((this.ScrollBar.Orientation != Orientation.Horizontal) ? new Size(availableSize.Width, Math.Max(7, (int) (num * availableSize.Height))) : new Size(Math.Max(7, (int) (num * availableSize.Width)), availableSize.Height));
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.#pye(e.X, e.Y);
            }
            base.OnMouseDown(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (this.#Cl)
            {
                Point point = this.ScrollBar.PointToScreen(new Point(e.X, e.Y));
                if (point != this.#3Jd)
                {
                    this.#3Jd = point;
                    this.#qye(e.X, e.Y);
                }
            }
            base.OnMouseMove(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            bool local1 = this.#rye();
            this.OnMouseUp(e);
        }

        protected override void OnRender(PaintEventArgs e)
        {
            this.ScrollBar.RendererResolved.DrawScrollBarThumb(e, base.Bounds, this);
        }

        protected override bool CaptureMouseWhenPressed =>
            true;

        protected override bool InvalidateOnMouseEvents =>
            true;

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ActiproSoftware.WinUICore.ScrollBar ScrollBar =>
            this.Parent as ActiproSoftware.WinUICore.ScrollBar;
    }
}

