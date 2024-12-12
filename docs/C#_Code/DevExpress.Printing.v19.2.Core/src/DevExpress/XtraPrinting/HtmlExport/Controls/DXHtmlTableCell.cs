namespace DevExpress.XtraPrinting.HtmlExport.Controls
{
    using DevExpress.XtraPrinting.HtmlExport;
    using DevExpress.XtraPrinting.HtmlExport.Native;
    using System;
    using System.Globalization;

    public class DXHtmlTableCell : DXHtmlContainerControl
    {
        public DXHtmlTableCell() : base(DXHtmlTextWriterTag.Td)
        {
        }

        public DXHtmlTableCell(DXHtmlTextWriterTag tag) : base(tag)
        {
        }

        protected override void RenderEndTag(DXHtmlTextWriter writer)
        {
            base.RenderEndTag(writer);
            writer.WriteLine();
        }

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

        public int ColSpan
        {
            get
            {
                string s = base.Attributes["colspan"];
                return ((s != null) ? int.Parse(s, CultureInfo.InvariantCulture) : -1);
            }
            set => 
                base.Attributes["colspan"] = MapIntegerAttributeToString(value);
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

        public bool NoWrap
        {
            get
            {
                string str = base.Attributes["nowrap"];
                return ((str != null) ? str.Equals("nowrap") : false);
            }
            set
            {
                if (value)
                {
                    base.Attributes["nowrap"] = "nowrap";
                }
                else
                {
                    base.Attributes["nowrap"] = null;
                }
            }
        }

        public int RowSpan
        {
            get
            {
                string s = base.Attributes["rowspan"];
                return ((s != null) ? int.Parse(s, CultureInfo.InvariantCulture) : -1);
            }
            set => 
                base.Attributes["rowspan"] = MapIntegerAttributeToString(value);
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
    }
}

