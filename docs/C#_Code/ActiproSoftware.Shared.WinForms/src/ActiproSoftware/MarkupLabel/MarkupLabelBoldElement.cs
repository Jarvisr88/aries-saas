namespace ActiproSoftware.MarkupLabel
{
    using System;

    public class MarkupLabelBoldElement : MarkupLabelElement
    {
        public MarkupLabelBoldElement() : base(#G.#eg(0x2bb1))
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

