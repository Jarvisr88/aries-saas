namespace DevExpress.XtraPrinting.HtmlExport.Controls
{
    using DevExpress.XtraPrinting.HtmlExport.Native;
    using System;
    using System.Globalization;

    public class DXHtmlImage : DXHtmlGenericControl
    {
        public DXHtmlImage() : base(DXHtmlTextWriterTag.Img)
        {
        }

        public virtual DXWebImageAlign ImageAlign
        {
            get
            {
                object obj2 = this.ViewState["ImageAlign"];
                return ((obj2 == null) ? DXWebImageAlign.NotSet : ((DXWebImageAlign) obj2));
            }
            set
            {
                if ((value < DXWebImageAlign.NotSet) || (value > DXWebImageAlign.TextTop))
                {
                    throw new ArgumentOutOfRangeException("value");
                }
                this.ViewState["ImageAlign"] = value;
            }
        }

        public string Alt
        {
            get
            {
                string str = base.Attributes["alt"];
                return ((str != null) ? str : string.Empty);
            }
            set => 
                base.Attributes["alt"] = MapStringAttributeToString(value);
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

        public string HeightStr
        {
            get => 
                base.Attributes["height"] ?? string.Empty;
            set => 
                base.Attributes["height"] = MapStringAttributeToString(value);
        }

        public string Src
        {
            get => 
                base.Attributes["src"] ?? string.Empty;
            set => 
                base.Attributes["src"] = MapStringAttributeToString(value);
        }

        public string WidthStr
        {
            get => 
                base.Attributes["width"] ?? string.Empty;
            set => 
                base.Attributes["width"] = MapStringAttributeToString(value);
        }
    }
}

