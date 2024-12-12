namespace ActiproSoftware.MarkupLabel
{
    using ActiproSoftware.ComponentModel;
    using ActiproSoftware.WinUICore;
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public abstract class MarkupLabelUIElement : UIElement
    {
        private Size #Jte;
        private MarkupLabelElement #irb;

        public MarkupLabelUIElement(MarkupLabelElement element)
        {
            this.#irb = element;
        }

        public override Cursor GetCursor(Point point) => 
            !this.#irb.IsInAnchor ? null : Cursors.Hand;

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
            return none;
        }

        protected override Size MeasureOverride(Graphics g, Size availableSize) => 
            new Size(0, 0);

        protected override void OnMouseUp(MouseEventArgs e)
        {
            this.OnMouseUp(e);
            if ((e.Button == MouseButtons.Left) && base.Bounds.Contains(e.X, e.Y))
            {
                MarkupLabelAnchorElement element = (MarkupLabelAnchorElement) ((ILogicalTreeNode) this.#irb).FindAncestor(typeof(MarkupLabelAnchorElement));
                if (element != null)
                {
                    this.#irb.MarkupLabel.#8we(new MarkupLabelLinkClickEventArgs(element));
                }
            }
        }

        internal Size CachedSize
        {
            get => 
                this.#Jte;
            set => 
                this.#Jte = value;
        }

        protected override bool CaptureMouseWhenPressed =>
            this.#irb.IsInAnchor;

        public virtual int Descent =>
            0;

        public MarkupLabelElement Element =>
            this.#irb;

        protected override bool InvalidateOnMouseEvents =>
            this.#irb.IsInAnchor;

        public virtual bool HardLineBreakBefore =>
            false;
    }
}

