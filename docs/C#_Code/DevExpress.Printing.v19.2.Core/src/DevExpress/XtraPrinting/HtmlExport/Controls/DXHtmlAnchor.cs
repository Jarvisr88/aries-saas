namespace DevExpress.XtraPrinting.HtmlExport.Controls
{
    using System;

    public class DXHtmlAnchor : DXHtmlContainerControl
    {
        public DXHtmlAnchor() : base(DXHtmlTextWriterTag.A)
        {
        }

        public virtual bool CausesValidation
        {
            get
            {
                object obj2 = this.ViewState["CausesValidation"];
                return ((obj2 == null) || ((bool) obj2));
            }
            set => 
                this.ViewState["CausesValidation"] = value;
        }

        public string HRef
        {
            get
            {
                string str = base.Attributes["href"];
                return ((str != null) ? str : string.Empty);
            }
            set => 
                base.Attributes["href"] = MapStringAttributeToString(value);
        }

        public string Name
        {
            get
            {
                string str = base.Attributes["name"];
                return ((str != null) ? str : string.Empty);
            }
            set => 
                base.Attributes["name"] = MapStringAttributeToString(value);
        }

        public string Target
        {
            get
            {
                string str = base.Attributes["target"];
                return ((str != null) ? str : string.Empty);
            }
            set => 
                base.Attributes["target"] = MapStringAttributeToString(value);
        }

        public string Title
        {
            get
            {
                string str = base.Attributes["title"];
                return ((str != null) ? str : string.Empty);
            }
            set => 
                base.Attributes["title"] = MapStringAttributeToString(value);
        }

        public virtual string ValidationGroup
        {
            get
            {
                string str = (string) this.ViewState["ValidationGroup"];
                return ((str == null) ? string.Empty : str);
            }
            set => 
                this.ViewState["ValidationGroup"] = value;
        }
    }
}

