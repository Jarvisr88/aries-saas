namespace ActiproSoftware.WinUICore
{
    using ActiproSoftware.WinUICore.Commands;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class ScrollBarButton : UIElement
    {
        private ActiproSoftware.WinUICore.Commands.CommandLink #kXd;

        public ScrollBarButton(ActiproSoftware.WinUICore.Commands.CommandLink commandLink)
        {
            this.#kXd = commandLink;
        }

        public UIElementDrawState GetDrawState()
        {
            UIElementDrawState none = UIElementDrawState.None;
            if (this.#kXd.Enabled && this.IsMouseDirectlyOver)
            {
                none |= UIElementDrawState.Hot;
            }
            if (this.IsMouseCaptured)
            {
                none |= UIElementDrawState.Pressed;
            }
            if (!this.#kXd.Enabled || !this.ScrollBar.Enabled)
            {
                none |= UIElementDrawState.Disabled;
            }
            return none;
        }

        protected override Size MeasureOverride(Graphics g, Size availableSize) => 
            new Size(SystemInformation.VerticalScrollBarWidth, SystemInformation.HorizontalScrollBarHeight);

        protected override void OnMouseDown(MouseEventArgs e)
        {
            this.OnMouseDown(e);
            if ((e.Button == MouseButtons.Left) && (base.Bounds.Contains(e.X, e.Y) && this.#kXd.Enabled))
            {
                this.ScrollBar.#lye(this.#kXd.Command);
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            this.ScrollBar.#mye();
            base.OnMouseUp(e);
        }

        protected override void OnRender(PaintEventArgs e)
        {
            this.ScrollBar.RendererResolved.DrawScrollBarButton(e, base.Bounds, this);
        }

        protected override bool CaptureMouseWhenPressed =>
            true;

        public ActiproSoftware.WinUICore.Commands.CommandLink CommandLink =>
            this.#kXd;

        protected override bool InvalidateOnMouseEvents =>
            true;

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ActiproSoftware.WinUICore.ScrollBar ScrollBar =>
            this.Parent as ActiproSoftware.WinUICore.ScrollBar;
    }
}

