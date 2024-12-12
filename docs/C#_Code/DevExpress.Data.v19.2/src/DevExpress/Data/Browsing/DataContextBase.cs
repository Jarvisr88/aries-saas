namespace DevExpress.Data.Browsing
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public class DataContextBase : IDisposable
    {
        private Dictionary<DataContextBase.HashObj, DataBrowser> browserDictionary;

        public DataContextBase();
        public DataContextBase(bool suppressListFilling);
        protected virtual void AddToHashtable(DataContextBase.HashObj key, DataBrowser dataBrowser);
        private static bool AnyPropertyStartsWith(PropertyDescriptorCollection properties, string propName);
        public virtual void Clear();
        private DataBrowser CreateDataBrowser(DataPair data);
        private DataBrowser CreateDataBrowser(DataPair data, DataBrowser parent, PropertyDescriptor prop);
        protected virtual ListBrowser CreateListBrowser(DataPair data, ListControllerBase listController);
        protected virtual ListControllerBase CreateListCotroller();
        protected virtual RelatedListBrowser CreateRelatedListBrowser(DataPair data, DataBrowser parent, PropertyDescriptor prop, ListControllerBase listController);
        public void Dispose();
        protected virtual void EnsureCalculatedFields();
        protected static object ForceList(object dataSource);
        public string GetColumnName(object dataSource, string dataMember);
        public string GetColumnName(object dataSource, string dataMember, bool suppressException);
        public DataBrowser GetDataBrowser(object dataSource, string dataMember, bool suppressException);
        private DataBrowser GetDataBrowserInternal(object dataSource, string dataMember);
        protected virtual DataBrowser GetDataBrowserInternal(object dataSource, string dataMember, bool suppressException);
        protected DataBrowser GetDataBrowserInternalCore(object dataSource, string dataMember, bool suppressException);
        private DataBrowser GetDataBrowserRecurcive(object dataSource, string parentDataMember, string dataMember, DataBrowser parentBrowser, ref int maxCheckedBrowser);
        protected IDictionaryEnumerator GetEnumerator();
        public PropertyDescriptorCollection GetItemProperties(object dataSource, string dataMember);
        public PropertyDescriptorCollection GetItemProperties(object dataSource, string dataMember, Predicate<Type> match);
        public PropertyDescriptorCollection GetItemProperties(object dataSource, string dataMember, Type[] types);
        public PropertyDescriptorCollection GetListItemProperties(object dataSource, string dataMember);
        public string GetListName(object dataSource);
        public string GetListName(object dataSource, string dataMember);
        protected static string GetNameFromTypedList(ITypedList dataSource);
        private PropertyDescriptor GetProperty(object dataSource, string dataMember, bool suppressException);
        public Type GetPropertyType(object dataSource, string dataMember);
        private static string Join(string prefix, string dataMember);
        public void LoadState(object state);
        public object SaveState();
        protected virtual bool ShouldExpand(DataBrowser dataBrowser);
        [IteratorStateMachine(typeof(DataContextBase.<ToEnumerable>d__41))]
        private static IEnumerable<PropertyDescriptor> ToEnumerable(PropertyDescriptorCollection properties);

        public bool IsDisposed { get; private set; }

        public DataBrowser this[object dataSource, string dataMember] { get; }

        public DataBrowser this[object dataSource] { get; }

        protected DataBrowser this[DataContextBase.HashObj hashObj] { get; }

        protected bool SuppressListFilling { get; private set; }

        [CompilerGenerated]
        private sealed class <ToEnumerable>d__41 : IEnumerable<PropertyDescriptor>, IEnumerable, IEnumerator<PropertyDescriptor>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private PropertyDescriptor <>2__current;
            private int <>l__initialThreadId;
            private PropertyDescriptorCollection properties;
            public PropertyDescriptorCollection <>3__properties;
            private int <i>5__1;

            [DebuggerHidden]
            public <ToEnumerable>d__41(int <>1__state);
            private bool MoveNext();
            [DebuggerHidden]
            IEnumerator<PropertyDescriptor> IEnumerable<PropertyDescriptor>.GetEnumerator();
            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator();
            [DebuggerHidden]
            void IEnumerator.Reset();
            [DebuggerHidden]
            void IDisposable.Dispose();

            PropertyDescriptor IEnumerator<PropertyDescriptor>.Current { [DebuggerHidden] get; }

            object IEnumerator.Current { [DebuggerHidden] get; }
        }

        protected class HashObj
        {
            private WeakReference weakRef;
            private string dataMember;
            private int hashCode;

            public HashObj(object dataSource, string dataMember);
            public override bool Equals(object obj);
            public override int GetHashCode();
        }

        private class StateItem
        {
            public DevExpress.Data.Browsing.DataBrowser DataBrowser { get; set; }

            public DataContextBase.HashObj Key { get; set; }

            public object State { get; set; }
        }
    }
}

