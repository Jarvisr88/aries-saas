namespace DevExpress.XtraPrinting.HtmlExport.Controls
{
    using System;

    public class DXHtmlTableRow : DXHtmlContainerControl
    {
        private DXHtmlTableCellCollection cells;

        public DXHtmlTableRow() : base(DXHtmlTextWriterTag.Tr)
        {
        }

        protected override DXWebControlCollection CreateControlCollection() => 
            new DXHtmlTableCellControlCollection(this);

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

        public virtual DXHtmlTableCellCollection Cells
        {
            get
            {
                this.cells ??= new DXHtmlTableCellCollection(this);
                return this.cells;
            }
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

        public string VAlign
        {
            get
            {
                string str = base.Attributes["valign"];
                return ((str != null) ? str : string.Empty);
            }
            set => 
                base.Attributes["valign"] = MapStringAttributeToString(value);
        }

        protected class DXHtmlTableCellControlCollection : DXWebControlCollection
        {
            internal DXHtmlTableCellControlCollection(DXWebControlBase owner) : base(owner)
            {
            }

            public override void Add(DXWebControlBase child)
            {
                if (!(child is DXHtmlTableCell))
                {
                    throw new ArgumentException("Cannot_Have_Children_Of_Type");
                }
                base.Add(child);
            }

            public override void AddAt(int index, DXWebControlBase child)
            {
                if (!(child is DXHtmlTableCell))
                {
                    throw new ArgumentException("Cannot_Have_Children_Of_Type");
                }
                base.AddAt(index, child);
            }
        }
    }
}

