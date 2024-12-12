namespace DevExpress.Data.Helpers
{
    using DevExpress.Data;
    using DevExpress.Data.Filtering;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;

    public abstract class ServerModeFlatBase : IListServer, IList, ICollection, IEnumerable
    {
        private ServerModeFlatBase.ConfigurationInfo _CurrentConfiguration;
        private EventHandler<ServerModeInconsistencyDetectedEventArgs> _InconsistencyDetected;
        private EventHandler<ServerModeExceptionThrownEventArgs> _ExceptionThrown;

        event EventHandler<ServerModeExceptionThrownEventArgs> IListServer.ExceptionThrown;

        event EventHandler<ServerModeInconsistencyDetectedEventArgs> IListServer.InconsistencyDetected;

        protected ServerModeFlatBase();
        void IListServer.Apply(CriteriaOperator filterCriteria, ICollection<ServerModeOrderDescriptor[]> sortInfo, int groupCount, ICollection<ServerModeSummaryDescriptor> groupSummaryInfo, ICollection<ServerModeSummaryDescriptor> totalSummaryInfo);
        int IListServer.FindIncremental(CriteriaOperator expression, string value, int startIndex, bool searchUp, bool ignoreStartRow, bool allowLoop);
        IList IListServer.GetAllFilteredAndSortedRows();
        List<ListSourceGroupInfo> IListServer.GetGroupInfo(ListSourceGroupInfo parentGroup);
        int IListServer.GetRowIndexByKey(object key);
        object IListServer.GetRowKey(int index);
        List<object> IListServer.GetTotalSummary();
        object[] IListServer.GetUniqueColumnValues(CriteriaOperator valuesExpression, int maxCount, CriteriaOperator filterExpression, bool ignoreAppliedFilter);
        int IListServer.LocateByExpression(CriteriaOperator expression, int startIndex, bool searchUp);
        int IListServer.LocateByValue(CriteriaOperator expression, object value, int startIndex, bool searchUp);
        bool IListServer.PrefetchRows(ListSourceGroupInfo[] groupsToPrefetch, CancellationToken cancellationToken);
        void IListServer.Refresh();
        protected virtual int? FindIncremental(CriteriaOperator expression, string value, int startIndex, bool searchUp, bool ignoreStartRow, bool allowLoop);
        public abstract int GetCount();
        protected virtual int? GetRowIndexByKey(object key);
        protected virtual object GetRowKey(int index);
        protected virtual IDictionary<ServerModeSummaryDescriptor, object> GetTotalSummary();
        protected virtual object[] GetUniqueColumnValues(CriteriaOperator valuesExpression, int maxCount, CriteriaOperator filterExpression, bool ignoreAppliedFilter);
        protected virtual int? LocateByExpression(CriteriaOperator expression, int startIndex, bool searchUp);
        protected virtual int? LocateByValue(CriteriaOperator expression, object value, int startIndex, bool searchUp);
        protected abstract void OnCurrentConfigurationChanged();
        protected virtual void OnCurrentConfigurationChanging(ServerModeFlatBase.ConfigurationInfo newInfo);
        protected virtual void OnExceptionThrown(ServerModeExceptionThrownEventArgs e);
        protected virtual void OnInconsistencyDetected(ServerModeInconsistencyDetectedEventArgs e);
        protected virtual bool PrefetchRows(CancellationToken cancellationToken);
        public void RaiseExceptionThrown(Exception exception);
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public void RaiseInconsistencyDetected(Exception exception);
        public void RaiseInconsistencyDetected(string message = null);
        public virtual void Refresh();
        void ICollection.CopyTo(Array array, int index);
        IEnumerator IEnumerable.GetEnumerator();
        int IList.Add(object value);
        void IList.Clear();
        bool IList.Contains(object value);
        int IList.IndexOf(object value);
        void IList.Insert(int index, object value);
        void IList.Remove(object value);
        void IList.RemoveAt(int index);

        public ServerModeFlatBase.ConfigurationInfo CurrentConfiguration { get; }

        bool IList.IsFixedSize { get; }

        bool IList.IsReadOnly { get; }

        object IList.this[int index] { get; set; }

        int ICollection.Count { get; }

        bool ICollection.IsSynchronized { get; }

        object ICollection.SyncRoot { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ServerModeFlatBase.<>c <>9;
            public static Func<ServerModeOrderDescriptor[], IEnumerable<ServerModeOrderDescriptor>> <>9__6_0;

            static <>c();
            internal IEnumerable<ServerModeOrderDescriptor> <DevExpress.Data.IListServer.Apply>b__6_0(ServerModeOrderDescriptor[] t);
        }

        public class ConfigurationInfo
        {
            private CriteriaOperator _FilterCriteria;
            private ServerModeOrderDescriptor[] _SortInfo;
            private ServerModeSummaryDescriptor[] _TotalSummaryInfo;

            internal ConfigurationInfo(CriteriaOperator filterCriteria, ServerModeOrderDescriptor[] sortInfo, ServerModeSummaryDescriptor[] totalSummaryInfo);

            public CriteriaOperator FilterCriteria { get; }

            public ServerModeOrderDescriptor[] SortInfo { get; }

            public ServerModeSummaryDescriptor[] TotalSummaryInfo { get; }
        }
    }
}

