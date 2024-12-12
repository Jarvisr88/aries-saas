namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows.Input;

    public class VariadicFilterModel : CriteriaConverterFilterModelBase<ValueData[]>
    {
        private readonly ObservableCollectionCore<FilterModelValueItem> itemsCore;
        private readonly OperandListObserver<FilterModelBase> operandListObserver;
        private readonly Func<int, EditSettingsInfo> createEditSettings;
        private readonly int? fixedOperandCount;
        private readonly FilterModelValueItemInfo info;

        internal VariadicFilterModel(FilterModelClient client, FilterModelValueItemInfo info, CriteriaConverter<ValueData[]> converter, OperandListObserver<FilterModelBase> operandListObserver = null, int? fixedOperandCount = new int?(), Func<int, EditSettingsInfo> createEditSettings = null) : base(client, converter)
        {
            this.fixedOperandCount = fixedOperandCount;
            Func<int, EditSettingsInfo> func1 = createEditSettings;
            if (createEditSettings == null)
            {
                Func<int, EditSettingsInfo> local1 = createEditSettings;
                func1 = _ => EditSettingsInfoFactory.Default(base.Column);
            }
            this.createEditSettings = func1;
            OperandListObserver<FilterModelBase> observer1 = operandListObserver;
            if (operandListObserver == null)
            {
                OperandListObserver<FilterModelBase> local2 = operandListObserver;
                observer1 = OperandListObserver<FilterModelBase>.Empty();
            }
            this.operandListObserver = observer1;
            this.info = info;
            this.itemsCore = new ObservableCollectionCore<FilterModelValueItem>();
            this.itemsCore.CollectionChanged += new NotifyCollectionChangedEventHandler(this.OnItemsCollectionChanged);
            this.<Items>k__BackingField = new ReadOnlyObservableCollection<FilterModelValueItem>(this.itemsCore);
            bool? useCommandManager = null;
            this.<AddCommand>k__BackingField = new DelegateCommand(new Action(this.AddNewValue), () => fixedOperandCount == null, useCommandManager);
        }

        private void AddNewValue()
        {
            this.operandListObserver.OnAdding(this);
            this.itemsCore.Add(this.CreateModelItem(null, this.itemsCore.Count));
            base.ApplyFilter();
        }

        protected internal override ValueData[] CreateConverterValue()
        {
            Func<FilterModelValueItem, ValueData> selector = <>c.<>9__16_0;
            if (<>c.<>9__16_0 == null)
            {
                Func<FilterModelValueItem, ValueData> local1 = <>c.<>9__16_0;
                selector = <>c.<>9__16_0 = item => item.ToValueData();
            }
            return this.itemsCore.Select<FilterModelValueItem, ValueData>(selector).ToArray<ValueData>();
        }

        private FilterModelValueItem CreateModelItem(ValueData value, int index)
        {
            <>c__DisplayClass13_0 class_;
            FilterModelValueItem item = x => new FilterModelValueItem(this.client.GetColumn().Type, index, this.createEditSettings(index), this.info, new Action(class_.ApplyFilter), new DelegateCommand(delegate {
                this.RemoveItem(x.Value);
            }, () => class_.fixedOperandCount == null, null)).WithReturnValue<FilterModelValueItem>();
            item.Update(value);
            return item;
        }

        private void OnItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            for (int i = 0; i < this.itemsCore.Count; i++)
            {
                this.itemsCore[i].Index = i;
            }
        }

        private void RemoveItem(FilterModelValueItem item)
        {
            this.operandListObserver.OnRemoving(this);
            this.itemsCore.Remove(item);
            base.ApplyFilter();
        }

        protected override void UpdateFromConverterValue(ValueData[] values)
        {
            int? fixedOperandCount = this.fixedOperandCount;
            if (((values != null) ? values.Length : 0) < ((fixedOperandCount != null) ? fixedOperandCount.GetValueOrDefault() : 0))
            {
                Func<int, ValueData> selector = <>c.<>9__17_0;
                if (<>c.<>9__17_0 == null)
                {
                    Func<int, ValueData> local1 = <>c.<>9__17_0;
                    selector = <>c.<>9__17_0 = _ => ValueData.NullValue;
                }
                values = values.Concat<ValueData>(Enumerable.Range(0, this.fixedOperandCount.Value - values.Length).Select<int, ValueData>(selector)).ToArray<ValueData>();
            }
            if (values != null)
            {
                Func<FilterModelValueItem, ValueData> selector = <>c.<>9__17_1;
                if (<>c.<>9__17_1 == null)
                {
                    Func<FilterModelValueItem, ValueData> local2 = <>c.<>9__17_1;
                    selector = <>c.<>9__17_1 = x => x.ToValueData();
                }
                if (!values.SequenceEqual<ValueData>(this.itemsCore.Select<FilterModelValueItem, ValueData>(selector)))
                {
                    this.itemsCore.Assign(values.Select<ValueData, FilterModelValueItem>(new Func<ValueData, int, FilterModelValueItem>(this.CreateModelItem)).ToList<FilterModelValueItem>());
                }
            }
        }

        public ICommand AddCommand { get; }

        public IList<FilterModelValueItem> Items { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly VariadicFilterModel.<>c <>9 = new VariadicFilterModel.<>c();
            public static Func<FilterModelValueItem, ValueData> <>9__16_0;
            public static Func<int, ValueData> <>9__17_0;
            public static Func<FilterModelValueItem, ValueData> <>9__17_1;

            internal ValueData <CreateConverterValue>b__16_0(FilterModelValueItem item) => 
                item.ToValueData();

            internal ValueData <UpdateFromConverterValue>b__17_0(int _) => 
                ValueData.NullValue;

            internal ValueData <UpdateFromConverterValue>b__17_1(FilterModelValueItem x) => 
                x.ToValueData();
        }
    }
}

