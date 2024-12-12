namespace ActiproSoftware.MarkupLabel
{
    using ActiproSoftware.WinUICore;
    using System;
    using System.Drawing;

    public class MarkupLabelAnchorElement : MarkupLabelElement
    {
        private string #rte;

        public MarkupLabelAnchorElement() : base(#G.#eg(0x2ba7))
        {
        }

        public override MarkupLabelCssData GetDefaultCssData()
        {
            MarkupLabelCssData data1 = new MarkupLabelCssData();
            MarkupLabelCssData data2 = new MarkupLabelCssData();
            data2.Color = Color.Blue;
            MarkupLabelCssData local1 = data2;
            MarkupLabelCssData local2 = data2;
            local2.TextDecoration = MarkupLabelTextDecoration.Underline;
            return local2;
        }

        public override Color GetForeColor(UIElementDrawState drawState) => 
            !(this.CssData.Color.Color != Color.Empty) ? base.Parent.GetForeColor(drawState) : (((drawState & UIElementDrawState.Pressed) != UIElementDrawState.Pressed) ? base.CssData.Color : Color.Red);

        public string HRef
        {
            get => 
                this.#rte;
            set => 
                this.#rte = value;
        }
    }
}

