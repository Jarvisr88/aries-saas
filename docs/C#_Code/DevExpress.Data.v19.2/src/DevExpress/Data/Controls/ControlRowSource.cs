namespace DevExpress.Data.Controls
{
    using DevExpress.Data;
    using DevExpress.Data.Helpers;
    using DevExpress.Utils;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq.Expressions;
    using System.Runtime.CompilerServices;

    [DXToolboxItem(false), ToolboxTabName("DX.19.2: Data & Analytics"), ToolboxBitmap(typeof(ResFinder), "Bitmaps256.ControlRowSource.bmp"), Description("A data source that provides visible or selected data-aware control rows as a data source.")]
    public class ControlRowSource : Component, IListSource, ITypedList, ISupportInitialize, IDXCloneable, INotifyPropertyChanged
    {
        private IList rowsCore;
        private Type rowTypeCore;
        private IControlRowSource controlCore;
        private DevExpress.Data.Controls.ControlRows controlRowsCore;
        private int initializing;
        private static readonly object propertyChanged;

        public event PropertyChangedEventHandler PropertyChanged;

        static ControlRowSource();
        public ControlRowSource();
        protected virtual IList CreateList();
        object IDXCloneable.DXClone();
        protected override void Dispose(bool disposing);
        protected virtual object DXClone();
        protected virtual ControlRowSource DXCloneCreate();
        private static Type GetBindingSourceItemType(object dataSource);
        private IEnumerable GetControlRows();
        private static string GetListName(IControlRowSource source, PropertyDescriptor[] listAccessors);
        private static PropertyDescriptorCollection GetRowProperties(IControlRowSource source, PropertyDescriptor[] listAccessors);
        private static Type GetRowType(IControlRowSource source);
        private static Type GetSourceType(IControlRowSource source);
        protected virtual void OnInitialized();
        protected void RaisePropertyChangedChanged(string propertyName);
        public void ReloadRows(bool raiseList);
        private void ReloadRows(Type prevRowType);
        private void Source_Changed(object sender, ControlRowSourceChangedEventArgs e);
        protected virtual void SubscribeControlEvents();
        IList IListSource.GetList();
        void ISupportInitialize.BeginInit();
        void ISupportInitialize.EndInit();
        PropertyDescriptorCollection ITypedList.GetItemProperties(PropertyDescriptor[] listAccessors);
        string ITypedList.GetListName(PropertyDescriptor[] listAccessors);
        protected virtual void UnsubscribeControlEvents();
        private static object UnwrapBindingSource(object source);

        bool IListSource.ContainsListCollection { get; }

        private IList List { get; }

        protected internal Type ControlRowType { get; }

        [Description("Gets or sets the control that provides its data rows to another control."), Category("Data"), RefreshProperties(RefreshProperties.All), DefaultValue((string) null)]
        public IControlRowSource Control { get; set; }

        [Description("Gets or sets the type of rows that the source control provides."), Category("Data"), RefreshProperties(RefreshProperties.All), DefaultValue(1)]
        public DevExpress.Data.Controls.ControlRows ControlRows { get; set; }

        private sealed class DataViewRows : ControlRowSource.RowsBase<DataRowView>, ITypedList
        {
            private System.Data.DataView dataViewCore;

            public DataViewRows(ControlRowSource rowSource);
            private System.Data.DataView GetDataView();
            private System.Data.DataView GetDataView(object source);
            PropertyDescriptorCollection ITypedList.GetItemProperties(PropertyDescriptor[] listAccessors);
            string ITypedList.GetListName(PropertyDescriptor[] listAccessors);

            private System.Data.DataView DataView { get; }

            [Serializable, CompilerGenerated]
            private sealed class <>c
            {
                public static readonly ControlRowSource.DataViewRows.<>c <>9;
                public static Func<DataTable, DataView> <>9__5_0;

                static <>c();
                internal DataView <GetDataView>b__5_0(DataTable t);
            }
        }

        private sealed class Rows<T> : ControlRowSource.RowsBase<T>, ITypedList
        {
            public Rows(ControlRowSource rowSource);
            PropertyDescriptorCollection ITypedList.GetItemProperties(PropertyDescriptor[] listAccessors);
            string ITypedList.GetListName(PropertyDescriptor[] listAccessors);
        }

        private abstract class RowsBase<T> : ReadOnlyCollection<T>, INotifyCollectionChanged, IDisposable
        {
            private readonly ControlRowSource rowSource;
            private static readonly NotifyCollectionChangedEventArgs resetArgs;
            private static readonly object collectionChanged;

            event NotifyCollectionChangedEventHandler INotifyCollectionChanged.CollectionChanged;

            static RowsBase();
            protected RowsBase(ControlRowSource rowSource);
            protected object GetSource();
            internal void RaiseResetCollection();
            private bool Reload(IEnumerable controlRows);
            void IDisposable.Dispose();
        }

        private static class RowsCache
        {
            internal static readonly PropertyDescriptorCollection EmptyProperties;
            internal static readonly IList EmptyRows;
            private static readonly IDictionary<Type, Func<ControlRowSource, IList>> createCache;
            private static readonly ParameterExpression pRowSource;
            private static readonly IDictionary<Type, Func<IList, IEnumerable, bool>> reloadCache;
            private static readonly ParameterExpression pList;
            private static readonly ParameterExpression pControlRows;

            static RowsCache();
            internal static IList Create(ControlRowSource rowSource, Type rowType, Type sourceType);
            private static Type EnsureRowsType(Type sourceType, Type type);
            private static bool IsExcelDataViewRow(Type sourceType, Type type);
            private static bool IsSqlResultTableRow(Type sourceType, Type type);
            internal static bool Reload(IList rows, Type type, IEnumerable controlRows);

            [Serializable, CompilerGenerated]
            private sealed class <>c
            {
                public static readonly ControlRowSource.RowsCache.<>c <>9;

                static <>c();
                internal IList <.cctor>b__12_0(ControlRowSource rs);
            }
        }

        private sealed class TypedListRows<T> : ControlRowSource.RowsBase<T>, ITypedList
        {
            private ITypedList typedListCore;

            public TypedListRows(ControlRowSource rowSource);
            PropertyDescriptorCollection ITypedList.GetItemProperties(PropertyDescriptor[] listAccessors);
            string ITypedList.GetListName(PropertyDescriptor[] listAccessors);

            private ITypedList TypedList { get; }
        }
    }
}

