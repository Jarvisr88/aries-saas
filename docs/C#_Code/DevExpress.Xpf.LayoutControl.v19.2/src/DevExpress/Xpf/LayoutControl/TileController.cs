namespace DevExpress.Xpf.LayoutControl
{
    using DevExpress.Xpf.Core;
    using System;

    public class TileController : ControlControllerBase
    {
        public TileController(IControl control) : base(control)
        {
        }

        internal void InvokeClick()
        {
            this.OnClick();
        }

        private void OnClick()
        {
            this.ITile.Click();
        }

        protected override void OnMouseLeftButtonUp(DXMouseButtonEventArgs e)
        {
            bool isMouseLeftButtonDown = base.IsMouseLeftButtonDown;
            base.OnMouseLeftButtonUp(e);
            if (isMouseLeftButtonDown)
            {
                this.OnClick();
                e.Handled = true;
            }
        }

        public DevExpress.Xpf.LayoutControl.ITile ITile =>
            base.Control as DevExpress.Xpf.LayoutControl.ITile;
    }
}

