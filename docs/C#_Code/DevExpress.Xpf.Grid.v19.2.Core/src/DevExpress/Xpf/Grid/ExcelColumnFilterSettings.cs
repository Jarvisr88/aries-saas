namespace DevExpress.Xpf.Grid
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class ExcelColumnFilterSettings
    {
        public IList<object> FilterItems { get; set; }

        public ExcelColumnFilterType? AllowedFilterTypes { get; set; }

        public ExcelColumnFilterType? DefaultFilterType { get; set; }

        public ClauseType? DefaultRulesFilterType { get; set; }

        internal TrackChangesList<object> TrackChangesList =>
            this.FilterItems as TrackChangesList<object>;

        internal bool IsChanged =>
            (this.TrackChangesList == null) || this.TrackChangesList.IsChanged;
    }
}

