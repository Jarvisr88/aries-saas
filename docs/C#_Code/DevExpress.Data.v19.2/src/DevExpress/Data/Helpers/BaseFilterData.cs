namespace DevExpress.Data.Helpers
{
    using DevExpress.Data;
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    public abstract class BaseFilterData : IDisposable
    {
        private Dictionary<object, BaseGridColumnInfo> columns;
        private string[] outlookNames;
        private CultureInfo culture;
        private DateTime sortStartTime;
        private DateTime sortStartWeek;
        private DateTime sortZeroTime;

        protected BaseFilterData();
        public virtual void Dispose();
        public string GetDisplayText(int listSourceIndex, DataColumnInfo info, object value);
        public virtual BaseGridColumnInfo GetInfo(DataColumnInfo info);
        protected virtual BaseGridColumnInfo GetInfoCore(object key);
        protected virtual object GetKey(DataColumnInfo column);
        public string GetOutlookLocaizedString(int id);
        protected virtual string[] GetOutlookLocalizedStrings();
        public abstract int GetSortIndex(object column);
        public virtual bool IsRequired(DataColumnInfo column);
        protected abstract void OnFillColumns();
        public void OnStart();

        protected Dictionary<object, BaseGridColumnInfo> Columns { get; }

        public DateTime SortStartTime { get; set; }

        public DateTime SortZeroTime { get; }

        public DateTime SortStartWeek { get; }

        public CultureInfo Culture { get; }

        public DateTimeFormatInfo DateTimeFormat { get; }

        public abstract int SortCount { get; }

        public abstract int GroupCount { get; }
    }
}

