namespace DevExpress.Xpf.Core
{
    using DevExpress.Utils.Localization;
    using System;

    public abstract class DXResXLocalizer<T> : XtraResXLocalizer<T> where T: struct
    {
        protected DXResXLocalizer(XtraLocalizer<T> embeddedLocalizer) : base(embeddedLocalizer)
        {
        }
    }
}

