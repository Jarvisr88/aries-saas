namespace ActiproSoftware.MarkupLabel
{
    using ActiproSoftware.WinUICore;
    using System;

    public class MarkupLabelTextElement : MarkupLabelElement
    {
        private string #Hd;

        public MarkupLabelTextElement() : base(null)
        {
        }

        protected internal override void CreateUIElement(IUIElement parentUIElement)
        {
            // Invalid method body.
        }

        public override string ToString() => 
            this.#Hd;

        public string Text
        {
            get => 
                this.#Hd;
            set => 
                this.#Hd = value;
        }
    }
}

