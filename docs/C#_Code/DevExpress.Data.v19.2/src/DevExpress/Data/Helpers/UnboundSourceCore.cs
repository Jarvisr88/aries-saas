namespace DevExpress.Data.Helpers
{
    using DevExpress.Data;
    using DevExpress.Data.Filtering.Helpers;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq.Expressions;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class UnboundSourceCore : ITypedList, IBindingList, IList, ICollection, IEnumerable
    {
        public static bool SuppressValueNeededTypeValidation;
        private readonly List<UnboundSourceCore.Row> InnerList;
        private PropertyDescriptorCollection pdc;
        public bool AllowEdit;
        public bool AllowNew;
        public bool AllowRemove;

        public event ListChangedEventHandler ListChanged;

        public event EventHandler<UnboundSourceListChangedEventArgs> UnboundSourceListChanged;

        public event EventHandler<UnboundSourceListChangedEventArgs> UnboundSourceListChanging;

        public event EventHandler<UnboundSourceValueNeededEventArgs> ValueNeeded;

        public event EventHandler<UnboundSourceValuePushedEventArgs> ValuePushed;

        public UnboundSourceCore();
        public UnboundSourceCore(IEnumerable<UnboundSourceCore.PropertyDescriptorDescriptor> descriptorsDescriptors);
        public int Add(bool isTriggeredByComponentApi);
        public void Change(bool isTriggeredByComponentApi, int row, string propertyName = null);
        public void Clear(bool isTriggeredByComponentApi);
        private PropertyDescriptor Create(UnboundSourceCore.PropertyDescriptorDescriptor src, int propertyIndex);
        private PropertyDescriptorCollection CreatePropertyDescriptors(IEnumerable<UnboundSourceCore.PropertyDescriptorDescriptor> descriptorsDescriptors);
        public PropertyDescriptor FindPropertyDescriptorByName(string propertyName);
        public PropertyDescriptorCollection GetItemProperties(PropertyDescriptor[] listAccessors);
        public string GetListName(PropertyDescriptor[] listAccessors);
        internal object Indexer(int index);
        private int IndexOf(UnboundSourceCore.Row value);
        private int IndexOfObject(object value);
        public void InsertAt(bool isTriggeredByComponentApi, int position);
        public void Move(bool isTriggeredByComponentApi, int from, int to);
        private void RaiseChanged(UnboundSourceListChangedEventArgs e);
        private void RaiseChanging(UnboundSourceListChangedEventArgs e);
        private UnboundSourceListChangedEventArgs RaiseChanging(bool isTriggeredByComponentApi, ListChangedType lct, int newIndex = -1, int oldIndex = -1);
        public void Reconfigure(bool isTriggeredByComponentApi, IEnumerable<UnboundSourceCore.PropertyDescriptorDescriptor> newDescriptorsDescriptors, int rowsAfterReconfigure = 0);
        public void RemoveAt(bool isTriggeredByComponentApi, int index);
        private void Renumber(int firstToRenumber, int? whereToStopRenumber = new int?());
        public void Reset(bool isTriggeredByComponentApi, int countAfterReset = 0);
        private void ResizeCore(int newCount);
        public void SetRowCount(bool isTriggeredByComponentApi, int count);
        void ICollection.CopyTo(Array array, int index);
        IEnumerator IEnumerable.GetEnumerator();
        int IList.Add(object value);
        void IList.Clear();
        bool IList.Contains(object value);
        int IList.IndexOf(object value);
        void IList.Insert(int index, object value);
        void IList.Remove(object value);
        void IList.RemoveAt(int index);
        void IBindingList.AddIndex(PropertyDescriptor property);
        object IBindingList.AddNew();
        void IBindingList.ApplySort(PropertyDescriptor property, ListSortDirection direction);
        int IBindingList.Find(PropertyDescriptor property, object key);
        void IBindingList.RemoveIndex(PropertyDescriptor property);
        void IBindingList.RemoveSort();

        bool IBindingList.AllowEdit { get; }

        bool IBindingList.AllowNew { get; }

        bool IBindingList.AllowRemove { get; }

        bool IBindingList.IsSorted { get; }

        ListSortDirection IBindingList.SortDirection { get; }

        PropertyDescriptor IBindingList.SortProperty { get; }

        bool IBindingList.SupportsChangeNotification { get; }

        bool IBindingList.SupportsSearching { get; }

        bool IBindingList.SupportsSorting { get; }

        bool IList.IsFixedSize { get; }

        bool IList.IsReadOnly { get; }

        object IList.this[int index] { get; set; }

        public int Count { get; }

        bool ICollection.IsSynchronized { get; }

        object ICollection.SyncRoot { get; }

        public class PropertyDescriptorDescriptor
        {
            public string Name;
            public Attribute[] Attributes;
            public Type PropertyType;
            public object UserTag;
            public bool IsReadOnly;
        }

        protected class Row
        {
            public int CurrentIndexOf;
        }

        public class UnboundSourcePropertyDescriptor : PropertyDescriptor, PropertyDescriptorCriteriaCompilationSupport.IHelper
        {
            private readonly bool _IsReadOnly;
            private readonly Type _PropertyType;
            private readonly object _Tag;
            private readonly int _PropertyIndex;
            private readonly UnboundSourceCore Owner;
            private readonly Delegate _FastGetterInvoker;
            private readonly Action<object, int, string> _ValidatorInvoker;
            private UnboundSourceValueNeededEventArgs typedVNEA;
            private UnboundSourceValueNeededEventArgs.Untyped untypedVNEA;

            public UnboundSourcePropertyDescriptor(string name, Attribute[] attrs, Type propertyType, bool _IsReadOnly, object _Tag, int propertyIndex, UnboundSourceCore _Owner);
            public override bool CanResetValue(object component);
            Delegate PropertyDescriptorCriteriaCompilationSupport.IHelper.TryGetFastGetter(out Type rowType, out Type valueType);
            Expression PropertyDescriptorCriteriaCompilationSupport.IHelper.TryMakeFastExpression(Expression baseExpression);
            private T FastGetter<T>(UnboundSourceCore.Row r);
            public override object GetValue(object component);
            private object ObjectGetter(UnboundSourceCore.Row r);
            public override void ResetValue(object component);
            public override void SetValue(object component, object value);
            public override bool ShouldSerializeValue(object component);
            private void ValidateValue<B>(object value, int rowIndex, string propertyName);

            public override Type ComponentType { get; }

            public override bool IsReadOnly { get; }

            public override Type PropertyType { get; }

            public abstract class GetterAndValidatorBuilder : GenericInvoker<Func<UnboundSourceCore.UnboundSourcePropertyDescriptor, Tuple<Delegate, Action<object, int, string>>>, UnboundSourceCore.UnboundSourcePropertyDescriptor.GetterAndValidatorBuilder.Impl<object, object>>
            {
                protected GetterAndValidatorBuilder();

                public class Impl<UnboxedType, BoxedType> : UnboundSourceCore.UnboundSourcePropertyDescriptor.GetterAndValidatorBuilder
                {
                    protected override Func<UnboundSourceCore.UnboundSourcePropertyDescriptor, Tuple<Delegate, Action<object, int, string>>> CreateInvoker();
                    private Func<UnboundSourceCore.Row, UnboxedType> FastGetterCreator(UnboundSourceCore.UnboundSourcePropertyDescriptor pd);
                    private Action<object, int, string> ValidatorCreator(UnboundSourceCore.UnboundSourcePropertyDescriptor pd);
                }
            }
        }
    }
}

