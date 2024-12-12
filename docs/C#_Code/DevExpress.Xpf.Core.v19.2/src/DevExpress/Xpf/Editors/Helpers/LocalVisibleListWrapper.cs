namespace DevExpress.Xpf.Editors.Helpers
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class LocalVisibleListWrapper : VisibleListWrapper
    {
        private readonly CurrentDataView dataView;

        public LocalVisibleListWrapper(CurrentDataView dataView)
        {
            this.dataView = dataView;
            dataView.ListChanged += new ListChangedEventHandler(this.DataViewListChanged);
        }

        protected override bool ContainsInternal(object value) => 
            base.IndexOf(value) > -1;

        protected override void CopyToInternal(Array array, int index)
        {
            for (int i = index; i < base.Count; i++)
            {
                array.SetValue(base[i], i);
            }
        }

        private void DataViewListChanged(object sender, ListChangedEventArgs e)
        {
            base.SelectionLocker.DoLockedAction(delegate {
                if (e.ListChangedType != ListChangedType.ItemChanged)
                {
                    this.RaiseListChanged(e);
                }
                else if (!this.EventLocker.IsLocked)
                {
                    this.RaiseListChanged(new ListChangedEventArgs(ListChangedType.ItemMoved, e.NewIndex, e.NewIndex));
                }
            });
        }

        protected override void DisposeInternal()
        {
            this.dataView.ListChanged -= new ListChangedEventHandler(this.DataViewListChanged);
        }

        protected override int GetCountInternal() => 
            this.dataView.VisibleRowCount;

        protected override IEnumerator GetEnumeratorInternal()
        {
            Func<DataProxy, object> selector = <>c.<>9__5_0;
            if (<>c.<>9__5_0 == null)
            {
                Func<DataProxy, object> local1 = <>c.<>9__5_0;
                selector = <>c.<>9__5_0 = x => x.f_component;
            }
            return this.dataView.Select<DataProxy, object>(selector).GetEnumerator();
        }

        protected override object IndexerGetInternal(int index) => 
            this.dataView[index].f_component;

        protected override int IndexOfInternal(object value) => 
            this.dataView.IndexOf(value);

        protected override void RefreshInternal()
        {
            base.RaiseListChanged(new ListChangedEventArgs(ListChangedType.Reset, null));
        }

        public CurrentDataView DataView =>
            this.dataView;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly LocalVisibleListWrapper.<>c <>9 = new LocalVisibleListWrapper.<>c();
            public static Func<DataProxy, object> <>9__5_0;

            internal object <GetEnumeratorInternal>b__5_0(DataProxy x) => 
                x.f_component;
        }
    }
}

