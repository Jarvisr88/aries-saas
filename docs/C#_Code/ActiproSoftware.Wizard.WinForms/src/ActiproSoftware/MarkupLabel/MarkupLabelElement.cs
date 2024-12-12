namespace ActiproSoftware.MarkupLabel
{
    using #H;
    using ActiproSoftware.ComponentModel;
    using ActiproSoftware.WinUICore;
    using System;
    using System.Collections;
    using System.Drawing;
    using System.Text;

    public abstract class MarkupLabelElement : LogicalTreeNodeBase
    {
        private string #nte;
        private MarkupLabelCssData #ote;
        private string #pte;
        private string #Xqc;
        private MarkupLabelElementVerticalAlignment #qte = MarkupLabelElementVerticalAlignment.Baseline;

        public MarkupLabelElement(string elementName)
        {
            this.#pte = elementName;
            this.#ote = this.GetDefaultCssData();
        }

        protected override IList CreateChildren() => 
            new ArrayList();

        protected internal virtual void CreateUIElement(IUIElement parentUIElement)
        {
            using (IEnumerator enumerator = this.Children.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    ((MarkupLabelElement) enumerator.Current).CreateUIElement(parentUIElement);
                }
            }
        }

        public virtual Color GetBackColor(UIElementDrawState drawState) => 
            !(this.#ote.BackgroundColor != Color.Empty) ? this.Parent.GetBackColor(drawState) : this.#ote.BackgroundColor;

        public virtual MarkupLabelCssData GetDefaultCssData() => 
            new MarkupLabelCssData();

        public Font GetFont() => 
            this.GetFont(null, 0f, MarkupLabelFontWeight.Inherit, MarkupLabelFontStyle.Inherit, MarkupLabelTextDecoration.Inherit, UIElementDrawState.None);

        public virtual Font GetFont(string fontFamily, float fontSize, MarkupLabelFontWeight fontWeight, MarkupLabelFontStyle fontStyle, MarkupLabelTextDecoration textDecoration, UIElementDrawState drawState)
        {
            if ((fontFamily == null) || (fontFamily == #G.#eg(0x2c25)))
            {
                fontFamily = this.#ote.FontFamily;
            }
            if (fontSize == 0f)
            {
                fontSize = this.#ote.FontSize;
            }
            fontWeight ??= this.#ote.FontWeight;
            fontStyle ??= this.#ote.FontStyle;
            textDecoration ??= this.#ote.TextDecoration;
            return this.Parent.GetFont(fontFamily, fontSize, fontWeight, fontStyle, textDecoration, drawState);
        }

        public virtual Color GetForeColor(UIElementDrawState drawState) => 
            !(this.#ote.Color != Color.Empty) ? this.Parent.GetForeColor(drawState) : this.#ote.Color;

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            if (this.#pte != null)
            {
                builder.AppendFormat(#G.#eg(0x2e8b), this.#pte);
                builder.Append(#G.#eg(0x2e94));
            }
            foreach (MarkupLabelElement element in this.Children)
            {
                builder.Append(element.ToString());
            }
            if (this.#pte != null)
            {
                builder.AppendFormat(#G.#eg(0x2e99), this.#pte);
            }
            return builder.ToString();
        }

        internal bool IsInAnchor =>
            ((MarkupLabelAnchorElement) ((ILogicalTreeNode) this).FindAncestor(Type.GetTypeFromHandle(typeof(MarkupLabelAnchorElement).TypeHandle))) != null;

        public IList Children =>
            this.Children;

        public string ClassName
        {
            get => 
                this.#nte;
            set => 
                this.#nte = value;
        }

        public MarkupLabelCssData CssData =>
            this.#ote;

        public string ElementName =>
            this.#pte;

        public string Id
        {
            get => 
                this.#Xqc;
            set => 
                this.#Xqc = value;
        }

        public virtual ActiproSoftware.MarkupLabel.MarkupLabel MarkupLabel =>
            this.Parent?.MarkupLabel;

        public MarkupLabelElement Parent
        {
            get => 
                (MarkupLabelElement) this.Parent;
            set => 
                this.Parent = value;
        }

        public string Style
        {
            get => 
                this.#ote.ToString();
            set => 
                this.#ote = MarkupLabelCssData.Parse(this.#ote, value);
        }

        public MarkupLabelElementVerticalAlignment VerticalAlignment
        {
            get => 
                this.#qte;
            set => 
                this.#qte = value;
        }
    }
}

