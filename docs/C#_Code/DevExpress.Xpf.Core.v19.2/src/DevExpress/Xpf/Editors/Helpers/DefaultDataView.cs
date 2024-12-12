namespace DevExpress.Xpf.Editors.Helpers
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public abstract class DefaultDataView : PlainDataView
    {
        protected DefaultDataView(bool selectNullValue, object serverSource, string valueMember, string displayMember) : base(selectNullValue, serverSource, valueMember, displayMember)
        {
        }

        public abstract CurrentDataView CreateCurrentDataView(object handle, IList<GroupingInfo> groups, IList<SortingInfo> sorts, string filterCriteria, string displayFilterCriteria, bool caseSensitiveFilter);
        protected override DataAccessor CreateEditorsDataAccessorInstance() => 
            new DataAccessor();

        protected override void DisposeInternal()
        {
            base.DisposeInternal();
            this.UnsubscribeFromEvents();
        }

        public object GetItemByIndex(int index) => 
            base.GetItemByProxy(base.View[index]);

        protected override void InitializeView(object source)
        {
            IList list = (IList) source;
            base.SetView(this.CreateDataProxyViewCache(list));
        }

        public virtual bool ProcessChangeFilter(string toString) => 
            true;

        public virtual void ProcessFindIncremental(ItemsProviderFindIncrementalCompletedEventArgs e)
        {
        }

        public virtual bool ProcessInconsistencyDetected() => 
            true;

        public void ProcessListChanged(ListChangedEventArgs e)
        {
            this.ProcessChangeSource(e);
            this.RaiseListChanged(e);
        }

        public int ListSourceCount =>
            this.ListSource.Count;

        private IList ListSource =>
            base.ListSource as IList;

        public IEnumerable<string> AvailableColumns
        {
            get
            {
                Func<PropertyDescriptor, string> selector = <>c.<>9__7_0;
                if (<>c.<>9__7_0 == null)
                {
                    Func<PropertyDescriptor, string> local1 = <>c.<>9__7_0;
                    selector = <>c.<>9__7_0 = x => x.Name;
                }
                return base.DataAccessor.Descriptors.Select<PropertyDescriptor, string>(selector);
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DefaultDataView.<>c <>9 = new DefaultDataView.<>c();
            public static Func<PropertyDescriptor, string> <>9__7_0;

            internal string <get_AvailableColumns>b__7_0(PropertyDescriptor x) => 
                x.Name;
        }
    }
}

