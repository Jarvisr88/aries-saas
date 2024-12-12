namespace DevExpress.Utils.Localization.Internal
{
    using DevExpress.Utils.Localization;
    using System;
    using System.Collections.Generic;

    public static class XtraLocalizierHelper<T> where T: struct
    {
        public static IDictionary<T, string> GetStringTable(XtraLocalizer<T> localizer) => 
            localizer.StringTable;
    }
}

