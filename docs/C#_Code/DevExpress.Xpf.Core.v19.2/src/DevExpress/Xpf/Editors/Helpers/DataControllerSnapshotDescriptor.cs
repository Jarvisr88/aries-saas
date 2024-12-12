namespace DevExpress.Xpf.Editors.Helpers
{
    using DevExpress.Data.Filtering;
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public class DataControllerSnapshotDescriptor
    {
        private Locker refreshedLocker = new Locker();

        public event EventHandler Refreshed;

        public DataControllerSnapshotDescriptor(object handle)
        {
            this.Handle = handle;
            this.Groups = new ObservableCollection<GroupingInfo>();
            this.Sorts = new ObservableCollection<SortingInfo>();
            this.Groups.CollectionChanged += (sender, args) => this.RaiseRefreshed();
            this.Sorts.CollectionChanged += (sender, args) => this.RaiseRefreshed();
        }

        private void RaiseRefreshed()
        {
            if (!this.refreshedLocker)
            {
                this.Refreshed.Do<EventHandler>(x => x(this, EventArgs.Empty));
            }
        }

        public void SetDisplayFilterCriteria(CriteriaOperator criteria)
        {
            Func<CriteriaOperator, string> evaluator = <>c.<>9__26_0;
            if (<>c.<>9__26_0 == null)
            {
                Func<CriteriaOperator, string> local1 = <>c.<>9__26_0;
                evaluator = <>c.<>9__26_0 = x => x.ToString();
            }
            string objB = criteria.With<CriteriaOperator, string>(evaluator);
            if (!Equals(this.DisplayFilterCriteria, objB))
            {
                this.DisplayFilterCriteria = objB;
                this.RaiseRefreshed();
            }
        }

        public void SetFilterCriteria(CriteriaOperator criteria)
        {
            Func<CriteriaOperator, string> evaluator = <>c.<>9__25_0;
            if (<>c.<>9__25_0 == null)
            {
                Func<CriteriaOperator, string> local1 = <>c.<>9__25_0;
                evaluator = <>c.<>9__25_0 = x => x.ToString();
            }
            string objB = criteria.With<CriteriaOperator, string>(evaluator);
            if (!Equals(this.FilterCriteria, objB))
            {
                this.FilterCriteria = objB;
                this.RaiseRefreshed();
            }
        }

        public void SetSorting(IEnumerable<SortingInfo> sorting)
        {
            <>c__DisplayClass28_0 class_;
            this.refreshedLocker.DoLockedActionIfNotLocked(delegate {
                this.Sorts.Clear();
                sorting.ForEach<SortingInfo>(x => class_.Sorts.Add(x));
            });
            this.RaiseRefreshed();
        }

        public object Handle { get; private set; }

        public ObservableCollection<GroupingInfo> Groups { get; private set; }

        public ObservableCollection<SortingInfo> Sorts { get; private set; }

        public string FilterCriteria { get; private set; }

        public string DisplayFilterCriteria { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DataControllerSnapshotDescriptor.<>c <>9 = new DataControllerSnapshotDescriptor.<>c();
            public static Func<CriteriaOperator, string> <>9__25_0;
            public static Func<CriteriaOperator, string> <>9__26_0;

            internal string <SetDisplayFilterCriteria>b__26_0(CriteriaOperator x) => 
                x.ToString();

            internal string <SetFilterCriteria>b__25_0(CriteriaOperator x) => 
                x.ToString();
        }
    }
}

