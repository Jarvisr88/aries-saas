namespace #Xqe
{
    using ActiproSoftware.Drawing;
    using ActiproSoftware.WinUICore;
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    internal class #Wqe : UIElement
    {
        private static readonly Size #2ue = new Size(0x18, 0x18);
        private System.Drawing.Color #eUb;

        internal #Wqe(System.Drawing.Color color)
        {
            this.#eUb = color;
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
            if (ReferenceEquals(this.ColorPalettePicker.SelectedColorElement, this))
            {
                none |= UIElementDrawState.Selected;
            }
            return none;
        }

        protected override Size MeasureOverride(Graphics #nYf, Size #3se) => 
            #2ue;

        protected override void OnMouseDown(MouseEventArgs #yhb)
        {
            this.OnMouseDown(#yhb);
            if ((#yhb.Button == MouseButtons.Left) && (base.Bounds.Contains(#yhb.X, #yhb.Y) && (this.ColorPalettePicker != null)))
            {
                this.ColorPalettePicker.SelectedColorElement = this;
            }
        }

        protected override void OnRender(PaintEventArgs #yhb)
        {
            if ((this.GetDrawState() & UIElementDrawState.Selected) == UIElementDrawState.Selected)
            {
                SimpleBorder.Draw(#yhb.Graphics, new Rectangle(base.Bounds.Left + 1, base.Bounds.Top + 1, base.Bounds.Width - 2, base.Bounds.Height - 2), SimpleBorderStyle.Solid, WindowsColorScheme.WindowsDefault.BarButtonHotBack);
                SimpleBorder.Draw(#yhb.Graphics, base.Bounds, SimpleBorderStyle.Solid, WindowsColorScheme.WindowsDefault.BarButtonHotBorder);
            }
            Rectangle bounds = base.Bounds;
            bounds.Inflate(-2, -2);
            SimpleBorder.Draw(#yhb.Graphics, bounds, SimpleBorderStyle.Sunken, SystemColors.Control);
            bounds.Inflate(-2, -2);
            SolidBrush brush = new SolidBrush(this.#eUb);
            #yhb.Graphics.FillRectangle(brush, bounds);
            brush.Dispose();
        }

        protected override bool CaptureMouseWhenPressed =>
            true;

        public System.Drawing.Color Color =>
            this.#eUb;

        public ActiproSoftware.WinUICore.ColorPalettePicker ColorPalettePicker =>
            this.Parent as ActiproSoftware.WinUICore.ColorPalettePicker;

        protected override bool InvalidateOnMouseEvents =>
            true;
    }
}

