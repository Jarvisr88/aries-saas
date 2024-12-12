namespace ActiproSoftware.MarkupLabel
{
    using System;

    public class MarkupLabelUnderlineElement : MarkupLabelElement
    {
        public MarkupLabelUnderlineElement() : base(#G.#eg(0x2bd7))
        {
        }

        public override MarkupLabelCssData GetDefaultCssData()
        {
            MarkupLabelCssData data1 = new MarkupLabelCssData();
            MarkupLabelCssData data2 = new MarkupLabelCssData();
            data2.TextDecoration = MarkupLabelTextDecoration.Underline;
            return data2;
        }
    }
}

