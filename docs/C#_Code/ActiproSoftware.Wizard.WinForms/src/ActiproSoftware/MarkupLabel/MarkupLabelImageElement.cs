namespace ActiproSoftware.MarkupLabel
{
    using ActiproSoftware.WinUICore;
    using System;

    public class MarkupLabelImageElement : MarkupLabelElement
    {
        private string #xte;
        private string #yte;

        public MarkupLabelImageElement() : base(#G.#eg(0x2bc0))
        {
        }

        protected internal override void CreateUIElement(IUIElement parentUIElement)
        {
            MarkupLabelDownloadImageEventArgs args = new MarkupLabelDownloadImageEventArgs(this);
            this.MarkupLabel.#7we(args);
            MarkupLabelImageUIElement element = new MarkupLabelImageUIElement(this, args.Image);
            parentUIElement.Children.Add(element);
        }

        public string Align
        {
            get => 
                this.#xte;
            set => 
                this.#xte = value.Trim().ToLower();
        }

        public string Src
        {
            get => 
                this.#yte;
            set => 
                this.#yte = value;
        }
    }
}

