namespace DevExpress.Office.Localization.Internal
{
    using DevExpress.Utils.Localization;
    using System;

    public abstract class OfficeLocalizerBase<T> : XtraLocalizer<T> where T: struct
    {
        protected OfficeLocalizerBase()
        {
        }
    }
}

