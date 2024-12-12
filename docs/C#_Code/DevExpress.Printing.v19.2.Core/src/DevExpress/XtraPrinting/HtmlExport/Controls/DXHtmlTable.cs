namespace DevExpress.XtraPrinting.HtmlExport.Controls
{
    using DevExpress.XtraPrinting.HtmlExport;
    using System;
    using System.Globalization;

    public class DXHtmlTable : DXHtmlContainerControl
    {
        private DXHtmlTableRowCollection rows;

        public DXHtmlTable() : base(DXHtmlTextWriterTag.Table)
        {
        }

        protected override DXWebControlCollection CreateControlCollection() => 
            new DXHtmlTableRowControlCollection(this);

        public string Align
        {
            get
            {
                string str = base.Attributes["align"];
                return ((str != null) ? str : string.Empty);
            }
            set => 
                base.Attributes["align"] = MapStringAttributeToString(value);
        }

        public string BgColor
        {
            get
            {
                string str = base.Attributes["bgcolor"];
                return ((str != null) ? str : string.Empty);
            }
            set => 
                base.Attributes["bgcolor"] = MapStringAttributeToString(value);
        }

        public int Border
        {
            get
            {
                string s = base.Attributes["border"];
                return ((s != null) ? int.Parse(s, CultureInfo.InvariantCulture) : -1);
            }
            set => 
                base.Attributes["border"] = MapIntegerAttributeToString(value);
        }

        public string BorderColor
        {
            get
            {
                string str = base.Attributes["bordercolor"];
                return ((str != null) ? str : string.Empty);
            }
            set => 
                base.Attributes["bordercolor"] = MapStringAttributeToString(value);
        }

        public int CellPadding
        {
            get
            {
                string s = base.Attributes["cellpadding"];
                return ((s != null) ? int.Parse(s, CultureInfo.InvariantCulture) : -1);
            }
            set => 
                base.Attributes["cellpadding"] = MapIntegerAttributeToString(value);
        }

        public int CellSpacing
        {
            get
            {
                string s = base.Attributes["cellspacing"];
                return ((s != null) ? int.Parse(s, CultureInfo.InvariantCulture) : -1);
            }
            set => 
                base.Attributes["cellspacing"] = MapIntegerAttributeToString(value);
        }

        public string HeightStr
        {
            get
            {
                string str = base.Attributes["height"];
                return ((str != null) ? str : string.Empty);
            }
            set => 
                base.Attributes["height"] = MapStringAttributeToString(value);
        }

        public override string InnerHtml
        {
            get
            {
                throw new NotSupportedException("InnerHtml_not_supported");
            }
            set
            {
                throw new NotSupportedException("InnerHtml_not_supported");
            }
        }

        public override string InnerText
        {
            get
            {
                throw new NotSupportedException("InnerText_not_supported");
            }
            set
            {
                throw new NotSupportedException("InnerText_not_supported");
            }
        }

        public virtual DXHtmlTableRowCollection Rows
        {
            get
            {
                this.rows ??= new DXHtmlTableRowCollection(this);
                return this.rows;
            }
        }

        public string WidthStr
        {
            get
            {
                string str = base.Attributes["width"];
                return ((str != null) ? str : string.Empty);
            }
            set => 
                base.Attributes["width"] = MapStringAttributeToString(value);
        }

        protected class DXHtmlTableRowControlCollection : DXWebControlCollection
        {
            internal DXHtmlTableRowControlCollection(DXWebControlBase owner) : base(owner)
            {
            }

            public override void Add(DXWebControlBase child)
            {
                if (!this.IsValidChild(child))
                {
                    throw new ArgumentException("Cannot_Have_Children_Of_Type");
                }
                base.Add(child);
            }

            public override void AddAt(int index, DXWebControlBase child)
            {
                if (!this.IsValidChild(child))
                {
                    throw new ArgumentException("Cannot_Have_Children_Of_Type");
                }
                base.AddAt(index, child);
            }

            private bool IsValidChild(DXWebControlBase child)
            {
                if (child is DXHtmlTableRow)
                {
                    return true;
                }
                DXHtmlControl control = child as DXHtmlControl;
                return ((control != null) ? ((control.TagKey == DXHtmlTextWriterTag.Colgroup) || (control.TagKey == DXHtmlTextWriterTag.Col)) : false);
            }
        }
    }
}

