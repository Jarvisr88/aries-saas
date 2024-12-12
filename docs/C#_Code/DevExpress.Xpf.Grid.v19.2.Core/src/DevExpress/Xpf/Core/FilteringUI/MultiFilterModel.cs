namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Data.Filtering;
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;

    public class MultiFilterModel : FilterModelBase
    {
        private readonly FilterSelector<MultiFilterModelItem> selector;
        private readonly bool applyEmptyFilter;
        private OperandValuesRecord memorizedOperandValues;
        private Locker operandValuesLocker;
        private FilterModelOperandRestoreAdapter operandRestoreAdapter;

        internal MultiFilterModel(FilterSelector<MultiFilterModelItem> selector, FilterModelClient client, bool applyEmptyFilter) : base(client)
        {
            this.memorizedOperandValues = OperandValuesRecord.CreateEmpty();
            this.operandValuesLocker = new Locker();
            if (selector == null)
            {
                throw new ArgumentNullException();
            }
            this.selector = selector;
            this.applyEmptyFilter = applyEmptyFilter;
            this.Items = selector.Available.FlattenLeaves<MultiFilterModelItem, string>().ToArray<MultiFilterModelItem>();
            this.<ItemsMenu>k__BackingField = CreateItemsMenu(selector.Available, item => this.SelectedItem = item);
            this.ModelTemplateSelector = new MultiFilterModelTemplateSelector(delegate (object model) {
                MultiFilterModelItem selectedItem = this.SelectedItem;
                if (selectedItem != null)
                {
                    return selectedItem.SelectTemplate(model as FilterModelBase);
                }
                MultiFilterModelItem local1 = selectedItem;
                return null;
            });
        }

        internal override CriteriaOperator BuildFilter()
        {
            Func<FilterModelBase, CriteriaOperator> evaluator = <>c.<>9__32_0;
            if (<>c.<>9__32_0 == null)
            {
                Func<FilterModelBase, CriteriaOperator> local1 = <>c.<>9__32_0;
                evaluator = <>c.<>9__32_0 = x => x.BuildFilter();
            }
            return this.Model.With<FilterModelBase, CriteriaOperator>(evaluator);
        }

        internal override bool CanBuildFilterCore() => 
            this.Items.Length != 0;

        internal bool CanSelectItem(CriteriaOperator filter) => 
            this.selector.SelectItem(filter) != null;

        internal bool CanUpdate(CriteriaOperator filter)
        {
            MultiFilterModelItem item = this.selector.SelectItem(BetweenDatesHelper.SubstituteDateInRange(filter));
            return ((item != null) && this.Items.Contains<MultiFilterModelItem>(item));
        }

        private static MenuItemBase[] CreateItemsMenu(IEnumerable<Tree<MultiFilterModelItem, string>> containers, Action<MultiFilterModelItem> select) => 
            containers.Select<Tree<MultiFilterModelItem, string>, MenuItemBase>(delegate (Tree<MultiFilterModelItem, string> x) {
                Func<string, IList<Tree<MultiFilterModelItem, string>>, MenuItemBase> <>9__3;
                Func<MultiFilterModelItem, MenuItemBase> <>9__1;
                Func<MultiFilterModelItem, MenuItemBase> func4 = <>9__1;
                if (<>9__1 == null)
                {
                    Func<MultiFilterModelItem, MenuItemBase> local1 = <>9__1;
                    func4 = <>9__1 = leaf => new DevExpress.Xpf.Core.FilteringUI.MenuItem(leaf.DisplayName, leaf.Icon, leaf.FormatCondition, delegate {
                        select(leaf);
                    });
                }
                Func<string, IList<Tree<MultiFilterModelItem, string>>, MenuItemBase> func5 = <>9__3;
                if (<>9__3 == null)
                {
                    Func<string, IList<Tree<MultiFilterModelItem, string>>, MenuItemBase> local2 = <>9__3;
                    func5 = <>9__3 = (group, children) => new MenuItemGroup(group, null, CreateItemsMenu(children, select));
                }
                return x.Match<MenuItemBase>(func4, func5);
            }).ToArray<MenuItemBase>();

        private FilterModelClient CreateNestedFilterClient()
        {
            Action<Lazy<CriteriaOperator>> applyFilter = delegate (Lazy<CriteriaOperator> x) {
                if (this.applyEmptyFilter || !HasEmptyValues(x.Value))
                {
                    base.ApplyFilter(x.Select<CriteriaOperator, CriteriaOperator>(new Func<CriteriaOperator, CriteriaOperator>(BetweenDatesHelper.RemoveDateInRange)));
                }
                else
                {
                    Func<CriteriaOperator> valueFactory = <>c.<>9__11_1;
                    if (<>c.<>9__11_1 == null)
                    {
                        Func<CriteriaOperator> local1 = <>c.<>9__11_1;
                        valueFactory = <>c.<>9__11_1 = (Func<CriteriaOperator>) (() => null);
                    }
                    this.ApplyFilter(new Lazy<CriteriaOperator>(valueFactory));
                }
            };
            return base.client.Update(applyFilter, null, null);
        }

        private static bool HasEmptyValues(CriteriaOperator filter)
        {
            BinaryOperatorMapper<bool> binary = <>c.<>9__17_0;
            if (<>c.<>9__17_0 == null)
            {
                BinaryOperatorMapper<bool> local1 = <>c.<>9__17_0;
                binary = <>c.<>9__17_0 = (_, value, __) => IsEmptyValue(value);
            }
            InOperatorMapper<bool> @in = <>c.<>9__17_1;
            if (<>c.<>9__17_1 == null)
            {
                InOperatorMapper<bool> local2 = <>c.<>9__17_1;
                @in = <>c.<>9__17_1 = (_, values) => values.Any<object>(new Func<object, bool>(MultiFilterModel.IsEmptyValue));
            }
            FunctionOperatorMapper<bool> mapper3 = <>c.<>9__17_3 ??= (propertyName, values, type) => values.Any<object>(new Func<object, bool>(MultiFilterModel.IsEmptyValue));
            if (<>c.<>9__17_4 == null)
            {
                FunctionOperatorMapper<bool> local5 = <>c.<>9__17_3 ??= (propertyName, values, type) => values.Any<object>(new Func<object, bool>(MultiFilterModel.IsEmptyValue));
                mapper3 = (FunctionOperatorMapper<bool>) (<>c.<>9__17_4 = val => val);
            }
            return filter.Map<bool>(binary, null, @in, (<>c.<>9__17_2 ??= (_, beginValue, endValue) => (IsEmptyValue(beginValue) || IsEmptyValue(endValue))), null, null, ((GroupOperatorMapper<bool>) <>c.<>9__17_4), ((NotOperatorMapper<bool>) mapper3), (<>c.<>9__17_5 ??= _ => false), null);
        }

        private static bool IsEmptyValue(object value) => 
            (value == null) || ((value as string) == string.Empty);

        private void OnSelectedItemChanged(MultiFilterModelItem oldSelectedItem)
        {
            FilterModelOperandRestoreAdapter adapter1;
            FilterModelBase base1;
            FilterModelBase model = this.Model;
            this.UpdateMemorizedOperandValues();
            MultiFilterModelItem selectedItem = this.SelectedItem;
            if (selectedItem != null)
            {
                adapter1 = selectedItem.Factory(this.CreateNestedFilterClient());
            }
            else
            {
                MultiFilterModelItem local1 = selectedItem;
                adapter1 = null;
            }
            this.operandRestoreAdapter = adapter1;
            if (this.operandRestoreAdapter != null)
            {
                base1 = this.operandRestoreAdapter.Model;
            }
            else
            {
                FilterModelOperandRestoreAdapter operandRestoreAdapter = this.operandRestoreAdapter;
                base1 = null;
            }
            this.Model = base1;
            if (this.Model != null)
            {
                this.Model.ShowCounts = base.ShowCounts;
                this.Model.ShowAllLookUpFilterValues = base.ShowAllLookUpFilterValues;
                this.Model.AllowLiveDataShaping = base.AllowLiveDataShaping;
                if (model != null)
                {
                    this.Model.FilterValuesSortMode = model.FilterValuesSortMode;
                }
                this.operandRestoreAdapter.Restore(this.memorizedOperandValues);
                this.Model.ApplyFilter();
            }
        }

        protected override void UpdateAllowLiveDataShaping()
        {
            this.Model.Do<FilterModelBase>(x => x.AllowLiveDataShaping = base.AllowLiveDataShaping);
        }

        internal override Task UpdateCoreAsync()
        {
            if (this.Items.Length != 0)
            {
                this.memorizedOperandValues = OperandValuesRecord.CreateEmpty();
                CriteriaOperator arg = BetweenDatesHelper.SubstituteDateInRange(base.Filter);
                MultiFilterModelItem selectedItem = this.selector.SelectItem(arg);
                if (selectedItem == null)
                {
                    arg = null;
                    selectedItem = this.selector.SelectItem(arg);
                }
                if (selectedItem == null)
                {
                    throw new NotSupportedException();
                }
                this.operandValuesLocker.DoLockedAction<MultiFilterModelItem>(delegate {
                    MultiFilterModelItem item;
                    this.SelectedItem = item = selectedItem;
                    return item;
                });
                FilterModelBase model = this.Model;
                if (model == null)
                {
                    FilterModelBase local1 = model;
                }
                else
                {
                    model.Update(arg);
                }
            }
            return FilteringUIExtensions.CompletedTask;
        }

        private void UpdateMemorizedOperandValues()
        {
            if (!this.operandValuesLocker.IsLocked)
            {
                OperandValuesRecord local2;
                if (this.operandRestoreAdapter != null)
                {
                    local2 = this.operandRestoreAdapter.Save();
                }
                else
                {
                    FilterModelOperandRestoreAdapter operandRestoreAdapter = this.operandRestoreAdapter;
                    local2 = null;
                }
                OperandValuesRecord record = local2;
                if ((record != null) && !record.IsEmpty)
                {
                    this.memorizedOperandValues = record;
                }
            }
        }

        protected sealed override void UpdateShowAllLookUpFilterValues()
        {
            this.Model.Do<FilterModelBase>(x => x.ShowAllLookUpFilterValues = base.ShowAllLookUpFilterValues);
        }

        protected sealed override void UpdateShowCounts()
        {
            this.Model.Do<FilterModelBase>(x => x.ShowCounts = base.ShowCounts);
        }

        public MultiFilterModelItem[] Items { get; private set; }

        public MultiFilterModelItem SelectedItem
        {
            get => 
                base.GetProperty<MultiFilterModelItem>(System.Linq.Expressions.Expression.Lambda<Func<MultiFilterModelItem>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(MultiFilterModel)), (MethodInfo) methodof(MultiFilterModel.get_SelectedItem)), new ParameterExpression[0]));
            set => 
                base.SetProperty<MultiFilterModelItem>(System.Linq.Expressions.Expression.Lambda<Func<MultiFilterModelItem>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(MultiFilterModel)), (MethodInfo) methodof(MultiFilterModel.get_SelectedItem)), new ParameterExpression[0]), value, new Action<MultiFilterModelItem>(this.OnSelectedItemChanged));
        }

        public FilterModelBase Model
        {
            get => 
                base.GetProperty<FilterModelBase>(System.Linq.Expressions.Expression.Lambda<Func<FilterModelBase>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(MultiFilterModel)), (MethodInfo) methodof(MultiFilterModel.get_Model)), new ParameterExpression[0]));
            private set => 
                base.SetProperty<FilterModelBase>(System.Linq.Expressions.Expression.Lambda<Func<FilterModelBase>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(MultiFilterModel)), (MethodInfo) methodof(MultiFilterModel.get_Model)), new ParameterExpression[0]), value);
        }

        public DataTemplateSelector ModelTemplateSelector
        {
            get => 
                base.GetProperty<DataTemplateSelector>(System.Linq.Expressions.Expression.Lambda<Func<DataTemplateSelector>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(MultiFilterModel)), (MethodInfo) methodof(MultiFilterModel.get_ModelTemplateSelector)), new ParameterExpression[0]));
            private set => 
                base.SetProperty<DataTemplateSelector>(System.Linq.Expressions.Expression.Lambda<Func<DataTemplateSelector>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(MultiFilterModel)), (MethodInfo) methodof(MultiFilterModel.get_ModelTemplateSelector)), new ParameterExpression[0]), value);
        }

        public MenuItemBase[] ItemsMenu { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly MultiFilterModel.<>c <>9 = new MultiFilterModel.<>c();
            public static Func<CriteriaOperator> <>9__11_1;
            public static BinaryOperatorMapper<bool> <>9__17_0;
            public static InOperatorMapper<bool> <>9__17_1;
            public static BetweenOperatorMapper<bool> <>9__17_2;
            public static FunctionOperatorMapper<bool> <>9__17_3;
            public static NotOperatorMapper<bool> <>9__17_4;
            public static FallbackMapper<bool> <>9__17_5;
            public static Func<FilterModelBase, CriteriaOperator> <>9__32_0;

            internal CriteriaOperator <BuildFilter>b__32_0(FilterModelBase x) => 
                x.BuildFilter();

            internal CriteriaOperator <CreateNestedFilterClient>b__11_1() => 
                null;

            internal bool <HasEmptyValues>b__17_0(string _, object value, BinaryOperatorType __) => 
                MultiFilterModel.IsEmptyValue(value);

            internal bool <HasEmptyValues>b__17_1(string _, object[] values) => 
                values.Any<object>(new Func<object, bool>(MultiFilterModel.IsEmptyValue));

            internal bool <HasEmptyValues>b__17_2(string _, object beginValue, object endValue) => 
                MultiFilterModel.IsEmptyValue(beginValue) || MultiFilterModel.IsEmptyValue(endValue);

            internal bool <HasEmptyValues>b__17_3(string propertyName, object[] values, FunctionOperatorType type) => 
                values.Any<object>(new Func<object, bool>(MultiFilterModel.IsEmptyValue));

            internal bool <HasEmptyValues>b__17_4(bool val) => 
                val;

            internal bool <HasEmptyValues>b__17_5(CriteriaOperator _) => 
                false;
        }

        private sealed class MultiFilterModelTemplateSelector : DataTemplateSelector
        {
            private readonly Func<object, DataTemplate> selectTemplate;

            internal MultiFilterModelTemplateSelector(Func<object, DataTemplate> selectTemplate)
            {
                this.selectTemplate = selectTemplate;
            }

            public override DataTemplate SelectTemplate(object item, DependencyObject container) => 
                this.selectTemplate(item);
        }
    }
}

