namespace DevExpress.Utils.Filtering.Internal
{
    using DevExpress.Data.Filtering;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class EndUserFilteringMetricViewModel : IEndUserFilteringMetricViewModel, INotifyPropertyChanged, IDisposable
    {
        private readonly IEndUserFilteringMetric metricCore;
        private readonly IEndUserFilteringMetricViewModelValueBox valueBoxCore;
        private int valueChangedLock;
        internal const string FilterCriteriaNotify = ".FilterCriteria";
        private WeakReference parentViewModelRef;
        private WeakReference surrogateViewModelRef;
        private Lazy<CriteriaOperator> filterCriteriaCore;

        public event PropertyChangedEventHandler PropertyChanged;

        public EndUserFilteringMetricViewModel(IEndUserFilteringMetric metric, IEndUserFilteringMetricViewModelValueBox valueBox);
        protected virtual CriteriaOperator CreateFilterCriteria();
        void IEndUserFilteringMetricViewModel.EnsureValueType();
        IDisposable IEndUserFilteringMetricViewModel.LockValue();
        protected virtual CriteriaOperator GetFilterCriteria(IValueViewModel value);
        internal IEndUserFilteringViewModel GetFilteringViewModel();
        private object GetParentViewModel();
        private object GetSurrogateParentViewModel();
        public Func<T, bool> GetWhereClause<T>();
        internal static bool IsFilterCriteriaNotify(PropertyChangedEventArgs e, out string path);
        internal static bool IsFilterCriteriaNotify(string propertyName, out string path);
        protected virtual void OnParentViewModelChanged();
        protected virtual void OnValueChanged();
        protected void RaisePropertyChanged(string propertyName);
        private void ResetCriteriaOperator();
        void IDisposable.Dispose();
        private void Value_Changed(object sender, EventArgs e);

        protected object ParentViewModel { get; set; }

        public IEndUserFilteringMetric Metric { get; }

        public IMetricAttributesQuery Query { get; }

        public bool HasValue { get; }

        public IValueViewModel Value { get; }

        public Type ValueType { get; }

        public CriteriaOperator FilterCriteria { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly EndUserFilteringMetricViewModel.<>c <>9;
            public static Action<IDisposable> <>9__3_0;
            public static Func<WeakReference, object> <>9__15_0;
            public static Func<WeakReference, object> <>9__17_0;
            public static Func<IValueViewModel, bool> <>9__28_0;
            public static Func<IFilterValueViewModel, CriteriaOperator> <>9__37_0;

            static <>c();
            internal bool <get_HasValue>b__28_0(IValueViewModel x);
            internal CriteriaOperator <GetFilterCriteria>b__37_0(IFilterValueViewModel fvm);
            internal object <GetParentViewModel>b__15_0(WeakReference r);
            internal object <GetSurrogateParentViewModel>b__17_0(WeakReference r);
            internal void <System.IDisposable.Dispose>b__3_0(IDisposable disposable);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__38<T>
        {
            public static readonly EndUserFilteringMetricViewModel.<>c__38<T> <>9;
            public static Func<T, bool> <>9__38_0;

            static <>c__38();
            internal bool <GetWhereClause>b__38_0(T e);
        }

        private sealed class ValueChangedLockToken : IDisposable
        {
            private readonly EndUserFilteringMetricViewModel metricViewModel;

            public ValueChangedLockToken(EndUserFilteringMetricViewModel metricViewModel);
            public void Dispose();
        }
    }
}

