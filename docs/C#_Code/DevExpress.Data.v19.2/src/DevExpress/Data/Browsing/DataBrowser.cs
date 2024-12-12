namespace DevExpress.Data.Browsing
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public class DataBrowser
    {
        private static readonly object notSet;
        private bool isClosed;
        private object dataSource;
        private bool isValidDataSource;
        protected bool suppressListFilling;
        protected EventHandler onCurrentChangedHandler;
        protected EventHandler onPositionChangedHandler;

        public event EventHandler CurrentChanged;

        public event EventHandler PositionChanged;

        static DataBrowser();
        protected DataBrowser();
        protected DataBrowser(bool suppressListFilling);
        internal DataBrowser(object dataSource);
        internal DataBrowser(object dataSource, bool suppressListFilling);
        [IteratorStateMachine(typeof(DataBrowser.<AllParents>d__55))]
        public IEnumerable<DataBrowser> AllParents();
        protected internal virtual void Close();
        public PropertyDescriptor FindItemProperty(string name, bool ignoreCase);
        public virtual PropertyDescriptorCollection GetItemProperties();
        internal virtual PropertyDescriptorCollection GetItemProperties(PropertyDescriptor[] listAccessors);
        public virtual string GetListName();
        protected internal virtual string GetListName(PropertyDescriptorCollection listAccessors);
        public PropertyDescriptor[] GetPropertyPath(string path);
        protected object GetPropertyValue(PropertyDescriptor prop, object obj);
        public DataBrowser GetRootBrowser();
        public virtual object GetValue();
        protected virtual void InvalidateDataSource();
        protected virtual bool IsStandardType(Type propType);
        public virtual void LoadState(object state);
        protected virtual void OnCurrentChanged(EventArgs e);
        public void RaiseCurrentChanged();
        protected virtual object RetrieveDataSource();
        public virtual object SaveState();
        protected virtual void SetDataSource(object value);

        public bool IsValidDataSource { get; }

        public virtual object DataSource { get; }

        protected bool DataSourceIsSet { get; }

        public virtual object Current { get; }

        public virtual int Position { get; set; }

        public virtual int Count { get; }

        public virtual Type DataSourceType { get; }

        public bool IsClosed { get; }

        public bool HasLastPosition { get; }

        public virtual DataBrowser Parent { get; }

        [CompilerGenerated]
        private sealed class <AllParents>d__55 : IEnumerable<DataBrowser>, IEnumerable, IEnumerator<DataBrowser>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private DataBrowser <>2__current;
            private int <>l__initialThreadId;
            public DataBrowser <>4__this;
            private DataBrowser <parent>5__1;

            [DebuggerHidden]
            public <AllParents>d__55(int <>1__state);
            private bool MoveNext();
            [DebuggerHidden]
            IEnumerator<DataBrowser> IEnumerable<DataBrowser>.GetEnumerator();
            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator();
            [DebuggerHidden]
            void IEnumerator.Reset();
            [DebuggerHidden]
            void IDisposable.Dispose();

            DataBrowser IEnumerator<DataBrowser>.Current { [DebuggerHidden] get; }

            object IEnumerator.Current { [DebuggerHidden] get; }
        }
    }
}

