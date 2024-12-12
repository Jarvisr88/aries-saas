namespace ActiproSoftware.MarkupLabel
{
    using #g;
    using #H;
    using #Qqe;
    using ActiproSoftware.Drawing;
    using ActiproSoftware.WinUICore;
    using System;
    using System.Collections;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Xml;

    [DefaultEvent("LinkClicked"), DefaultProperty("Text"), ToolboxBitmap(typeof(ActiproSoftware.MarkupLabel.MarkupLabel)), ToolboxItem(true)]
    public class MarkupLabel : UIControl
    {
        private HybridDictionary #gte = new HybridDictionary();
        private Size #hte = Size.Empty;
        private #Pqe #ite = new #Pqe();
        private bool #jte = true;
        private int #x5b = 0x7fffffff;
        private MarkupLabelElement #W6;
        private System.Drawing.StringFormat #kte;

        [Category("Action"), Description("Occurs when an image needs to be downloaded for a MarkupLabelImageElement.")]
        public event MarkupLabelDownloadImageEventHandler DownloadImage;

        [Category("Action"), Description("Occurs after a MarkupLabelAnchorElement is clicked.")]
        public event MarkupLabelLinkClickEventHandler LinkClick;

        internal int #2we(string #S7b, float #KK, bool #R7b, bool #Y7b, bool #7Zf, bool #8Zf) => 
            #S7b.GetHashCode() ^ ((((((int) (#KK * 100f)) + (#R7b ? 0x2000000 : 0)) + (#Y7b ? 0x1000000 : 0)) + (#7Zf ? 0x4000000 : 0)) + (#8Zf ? 0x8000000 : 0));

        private void #3we(MarkupLabelUIElement #irb)
        {
            #irb.CachedSize = Size.Empty;
            if (#irb.Children != null)
            {
                foreach (MarkupLabelUIElement element in #irb.Children)
                {
                    this.#3we(element);
                }
            }
        }

        private void #4we()
        {
            XmlDocument document = new XmlDocument();
            try
            {
                document.PreserveWhitespace = true;
                document.LoadXml(#G.#eg(0x2b50) + base.Text + #G.#eg(0x2b59));
            }
            catch (Exception exception)
            {
                throw new ApplicationException(#G.#eg(0x2b66) + exception.Message);
            }
            this.#W6 = new MarkupLabelBodyElement(this);
            this.#5we((XmlElement) document.ChildNodes[0], this.#W6);
            this.Children.Clear();
            this.#W6.CreateUIElement(this);
        }

        private void #5we(XmlElement #BTc, MarkupLabelElement #Txf)
        {
            MarkupLabelElement objA = #Txf;
            string str = #BTc.Name.ToLower();
            if (str != null)
            {
                uint num = #r.#g2i(str);
                if (num <= 0x84e72504)
                {
                    if (num <= 0x401a63f7)
                    {
                        if (num != 0x290182c1)
                        {
                            if ((num == 0x401a63f7) && (str == #G.#eg(0x2bb6)))
                            {
                                objA = new MarkupLabelEmphasisElement();
                            }
                        }
                        else if (str == #G.#eg(0x2bc5))
                        {
                            objA = new MarkupLabelSpanElement();
                        }
                    }
                    else if (num == 0x4f2bc4b5)
                    {
                        if (str == #G.#eg(0x2bac))
                        {
                            objA = new MarkupLabelLineBreakElement();
                        }
                    }
                    else if ((num == 0x84e72504) && (str == #G.#eg(0x2bc0)))
                    {
                        objA = new MarkupLabelImageElement();
                        if (#BTc.Attributes[#G.#eg(0x2be5)] != null)
                        {
                            ((MarkupLabelImageElement) objA).Src = #BTc.Attributes[#G.#eg(0x2be5)].InnerText;
                        }
                        if (#BTc.Attributes[#G.#eg(0x2bea)] != null)
                        {
                            ((MarkupLabelImageElement) objA).Align = #BTc.Attributes[#G.#eg(0x2bea)].InnerText;
                        }
                    }
                }
                else if (num <= 0xe40c292c)
                {
                    if (num == 0xc51f5d7a)
                    {
                        if (str == #G.#eg(0x2bce))
                        {
                            objA = new MarkupLabelStrongElement();
                        }
                    }
                    else if ((num == 0xe40c292c) && (str == #G.#eg(0x2ba7)))
                    {
                        objA = new MarkupLabelAnchorElement();
                        if (#BTc.Attributes[#G.#eg(0x2bdc)] != null)
                        {
                            ((MarkupLabelAnchorElement) objA).HRef = #BTc.Attributes[#G.#eg(0x2bdc)].InnerText;
                        }
                    }
                }
                else if (num == 0xe70c2de5)
                {
                    if (str == #G.#eg(0x2bb1))
                    {
                        objA = new MarkupLabelBoldElement();
                    }
                }
                else if (num != 0xec0c35c4)
                {
                    if ((num == 0xf00c3c10) && (str == #G.#eg(0x2bd7)))
                    {
                        objA = new MarkupLabelUnderlineElement();
                    }
                }
                else if (str == #G.#eg(0x2bbb))
                {
                    objA = new MarkupLabelItalicElement();
                }
            }
            if (!ReferenceEquals(objA, #Txf))
            {
                if (#BTc.Attributes[#G.#eg(0x2bf3)] != null)
                {
                    objA.ClassName = #BTc.Attributes[#G.#eg(0x2bf3)].InnerText;
                }
                if (#BTc.Attributes[#G.#eg(0x2bfc)] != null)
                {
                    objA.Style = #BTc.Attributes[#G.#eg(0x2bfc)].InnerText;
                }
                if (#BTc.Attributes[#G.#eg(0x2c01)] != null)
                {
                    objA.Style = #BTc.Attributes[#G.#eg(0x2c01)].InnerText;
                }
                #Txf.Children.Add(objA);
                objA.Parent = #Txf;
            }
            foreach (XmlNode node in #BTc.ChildNodes)
            {
                this.#6we(node, objA);
            }
        }

        private void #6we(XmlNode #BTc, MarkupLabelElement #Txf)
        {
            XmlNodeType nodeType = #BTc.NodeType;
            if (nodeType == XmlNodeType.Element)
            {
                this.#5we((XmlElement) #BTc, #Txf);
            }
            else if ((nodeType == XmlNodeType.Text) || ((nodeType - XmlNodeType.Whitespace) <= XmlNodeType.Element))
            {
                MarkupLabelTextElement element = new MarkupLabelTextElement {
                    Text = #BTc.InnerText
                };
                #Txf.Children.Add(element);
                element.Parent = #Txf;
            }
        }

        internal void #7we(MarkupLabelDownloadImageEventArgs #yhb)
        {
            this.OnDownloadImage(#yhb);
        }

        internal void #8we(MarkupLabelLinkClickEventArgs #yhb)
        {
            this.OnLinkClick(#yhb);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            if (this.#jte)
            {
                this.#ite.#pmc(this.ClientRectangle, this.Children);
                this.#jte = false;
            }
            return finalSize;
        }

        protected override IList CreateChildren() => 
            new UIElementCollection(this);

        protected override void Dispose(bool disposing)
        {
            this.Dispose(disposing);
            if (disposing)
            {
                if (this.#gte != null)
                {
                    using (IEnumerator enumerator = this.#gte.Values.GetEnumerator())
                    {
                        while (enumerator.MoveNext())
                        {
                            ((System.Drawing.Font) enumerator.Current).Dispose();
                        }
                    }
                    this.#gte.Clear();
                    this.#gte = null;
                }
                if (this.#kte != null)
                {
                    this.#kte.Dispose();
                    this.#kte = null;
                }
            }
        }

        public Size GetPreferredSize() => 
            this.#ite.GetPreferredSize(new Rectangle(0, 0, this.#x5b, 0x7fffffff), this.Children);

        protected virtual void OnDownloadImage(MarkupLabelDownloadImageEventArgs e)
        {
            if (this.#lte != null)
            {
                this.#lte(this, e);
            }
        }

        protected sealed override void OnFontChanged(EventArgs e)
        {
            if (2 != 0)
            {
                e.OnFontChanged((EventArgs) this);
            }
            else
            {
                EventArgs local2 = e;
            }
            foreach (MarkupLabelUIElement element in this.Children)
            {
                this.#3we(element);
            }
            this.#jte = true;
            base.Invalidate(InvalidationLevels.ElementAndChildren, InvalidationTypes.All);
        }

        protected virtual void OnLinkClick(MarkupLabelLinkClickEventArgs e)
        {
            if (this.#mte != null)
            {
                this.#mte(this, e);
            }
        }

        protected override void OnResize(EventArgs e)
        {
            if (this.Size != this.#hte)
            {
                this.#jte = true;
                this.#hte = base.Size;
            }
            base.OnResize(e);
        }

        internal HybridDictionary FontCache =>
            this.#gte;

        internal System.Drawing.StringFormat StringFormat
        {
            get
            {
                if (this.#kte == null)
                {
                    this.#kte = DrawingHelper.GetStringFormat(StringAlignment.Near, StringAlignment.Near, StringTrimming.None, false, false);
                    this.#kte.FormatFlags |= StringFormatFlags.NoClip;
                }
                return this.#kte;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true)]
        public override Color BackColor
        {
            get => 
                this.BackColor;
            set => 
                this.BackColor = value;
        }

        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true)]
        public override Image BackgroundImage
        {
            get => 
                this.BackgroundImage;
            set => 
                this.BackgroundImage = value;
        }

        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true)]
        public override System.Drawing.Font Font
        {
            get => 
                this.Font;
            set => 
                this.Font = value;
        }

        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true)]
        public override Color ForeColor
        {
            get => 
                this.ForeColor;
            set => 
                this.ForeColor = value;
        }

        public int MaxWidth
        {
            get => 
                this.#x5b;
            set => 
                this.#x5b = value;
        }

        [Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public MarkupLabelElement RootElement =>
            this.#W6;

        [Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override string Text
        {
            get => 
                this.Text;
            set
            {
                if (this.Text != value)
                {
                    if (value == null)
                    {
                        base.Text = value;
                    }
                    else
                    {
                        base.Text = value.Replace(#G.#eg(0x2c0a), #G.#eg(0x2c13));
                        this.#4we();
                    }
                    this.#jte = true;
                    base.Invalidate(InvalidationLevels.ElementAndChildren, InvalidationTypes.All);
                }
            }
        }
    }
}

