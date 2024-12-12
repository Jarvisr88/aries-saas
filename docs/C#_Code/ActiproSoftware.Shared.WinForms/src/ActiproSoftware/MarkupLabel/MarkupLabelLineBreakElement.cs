namespace ActiproSoftware.MarkupLabel
{
    using ActiproSoftware.WinUICore;
    using System;

    public class MarkupLabelLineBreakElement : MarkupLabelElement
    {
        public MarkupLabelLineBreakElement() : base(#G.#eg(0x2bac))
        {
        }

        protected internal override void CreateUIElement(IUIElement parentUIElement)
        {
            MarkupLabelUIElement element = new MarkupLabelLineBreakUIElement(this);
            parentUIElement.Children.Add(element);
        }
    }
}

