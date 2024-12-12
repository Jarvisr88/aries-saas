namespace ActiproSoftware.MarkupLabel
{
    using System;

    public class MarkupLabelStrongElement : MarkupLabelElement
    {
        public MarkupLabelStrongElement() : base(#G.#eg(0x2bce))
        {
        }

        public override MarkupLabelCssData GetDefaultCssData()
        {
            MarkupLabelCssData data1 = new MarkupLabelCssData();
            MarkupLabelCssData data2 = new MarkupLabelCssData();
            data2.FontWeight = MarkupLabelFontWeight.Bold;
            return data2;
        }
    }
}

