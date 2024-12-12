namespace DevExpress.Xpf.Core
{
    using DevExpress.Utils.Localization;
    using System;

    public abstract class DXLocalizer<T> : XtraLocalizer<T> where T: struct
    {
        protected DXLocalizer()
        {
        }
    }
}

