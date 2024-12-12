namespace System.ComponentModel
{
    using DevExpress.Utils.About;
    using System;

    public class DXWebToolboxItemAttribute : DXToolboxItemAttribute
    {
        public DXWebToolboxItemAttribute(bool defaultType) : this(defaultType ? DXToolboxItemKind.Regular : DXToolboxItemKind.Hidden)
        {
        }

        public DXWebToolboxItemAttribute(DXToolboxItemKind kind) : base(kind, "System.Web.UI.Design.WebControlToolboxItem, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")
        {
        }

        protected override int Check() => 
            Utility.IsOnlyWeb();

        public override object TypeId =>
            ToolboxItemAttribute.Default.TypeId;
    }
}

