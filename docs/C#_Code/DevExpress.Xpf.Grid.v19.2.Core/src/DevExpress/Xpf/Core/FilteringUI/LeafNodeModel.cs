namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Filtering.Helpers;
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.Native;
    using DevExpress.Utils;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    public sealed class LeafNodeModel : NodeModelBase
    {
        private readonly NodeClient nodeClient;
        private readonly Locker selectedPropertyChangeLocker = new Locker();
        private Func<IReadOnlyCollection<string>> getParameters;
        private Func<AllowedOperandTypes> getAllowedOperandTypes;
        private DelegateCommand removeCommandCore;

        private LeafNodeModel(NodeClient nodeClient)
        {
            Guard.ArgumentNotNull(nodeClient, "nodeClient");
            this.nodeClient = nodeClient;
            this.Properties = new PropertySelectorModelBase[0];
            this.ShowSearchPanel = true;
        }

        private static CriteriaOperator ChangeOperandPropertyName(CriteriaOperator filter, string newPropertyName, Type newPropertyType)
        {
            if (filter is FunctionOperator)
            {
                if (FormatConditionFiltersHelper.GetAppliedFormatConditionFilterInfo((FunctionOperator) filter) != null)
                {
                    return null;
                }
                if (FormatConditionFiltersHelper.GetTopBottomFilterInfo((FunctionOperator) filter) != null)
                {
                    return null;
                }
            }
            Func<ValueData, bool> isAssignableFrom = delegate (ValueData valueData) {
                Func<object, bool> <>9__1;
                if (valueData == null)
                {
                    return false;
                }
                Func<object, bool> func2 = <>9__1;
                if (<>9__1 == null)
                {
                    Func<object, bool> local1 = <>9__1;
                    func2 = <>9__1 = value => (value == null) || newPropertyType.IsAssignableFrom(value.GetType());
                }
                return valueData.Match<bool>(func2, <>c.<>9__20_2 ??= propertyName => false, <>c.<>9__20_3 ??= parameter => false, <>c.<>9__20_4 ??= function => false, null);
            };
            OperandProperty newOperandProperty = new OperandProperty(newPropertyName);
            NotOperatorMapper<CriteriaOperator> not = <>c.<>9__20_13;
            if (<>c.<>9__20_13 == null)
            {
                NotOperatorMapper<CriteriaOperator> local1 = <>c.<>9__20_13;
                not = <>c.<>9__20_13 = (NotOperatorMapper<CriteriaOperator>) (x => new UnaryOperator(UnaryOperatorType.Not, x));
            }
            NullMapper<CriteriaOperator> @null = <>c.<>9__20_14;
            if (<>c.<>9__20_14 == null)
            {
                NullMapper<CriteriaOperator> local2 = <>c.<>9__20_14;
                @null = <>c.<>9__20_14 = (NullMapper<CriteriaOperator>) (() => null);
            }
            return filter.MapExtended<CriteriaOperator>((_, valueData, type) => new BinaryOperator(newOperandProperty, valueData.If<ValueData>(isAssignableFrom).ToCriteria(), type).ToOption<CriteriaOperator>(), ((UnaryOperatorMapper<CriteriaOperator>) ((_, type) => new UnaryOperator(type, newOperandProperty))), delegate (string _, ValueData[] valuesData) {
                IEnumerable<CriteriaOperator> enumerable1;
                if (!valuesData.All<ValueData>(isAssignableFrom))
                {
                    OperandValue[] valueArray1 = new OperandValue[] { new OperandValue(null) };
                    enumerable1 = valueArray1;
                }
                else
                {
                    Func<ValueData, CriteriaOperator> selector = <>c.<>9__20_8;
                    if (<>c.<>9__20_8 == null)
                    {
                        Func<ValueData, CriteriaOperator> local1 = <>c.<>9__20_8;
                        selector = <>c.<>9__20_8 = x => x.ToCriteria();
                    }
                    enumerable1 = valuesData.Select<ValueData, CriteriaOperator>(selector);
                }
                return new InOperator(newOperandProperty, enumerable1).ToOption<CriteriaOperator>();
            }, (_, beginValueData, endValueData) => ((!isAssignableFrom(beginValueData) || !isAssignableFrom(endValueData)) ? new BetweenOperator(newOperandProperty, new OperandValue(null), new OperandValue(null)).ToOption<CriteriaOperator>() : new BetweenOperator(newOperandProperty, beginValueData.ToCriteria(), endValueData.ToCriteria()).ToOption<CriteriaOperator>()), delegate (string propertyName, ValueData[] valuesData, FunctionOperatorType functionType) {
                Attributed<ValueDataRange> attributed = CriteriaConverterFactory.TryGetRangeFromSubstitutedValuesData(propertyName, valuesData, functionType);
                if (attributed != null)
                {
                    return BetweenDatesHelper.CreateBetweenDatesFunction(newOperandProperty, attributed.Value.From, attributed.Value.To).ToOption<CriteriaOperator>();
                }
                Attributed<ValueData[]> attributed2 = CriteriaConverterFactory.TryGetPropertyValuesFromSubstitutedValuesData(propertyName, valuesData, functionType);
                if (attributed2 != null)
                {
                    return BetweenDatesHelper.CreateIsOnDatesFunction(newOperandProperty, attributed2.Value).ToOption<CriteriaOperator>();
                }
                Func<ValueData, CriteriaOperator> selector = null;
                selector = !valuesData.All<ValueData>(isAssignableFrom) ? (<>c.<>9__20_12 ??= __ => new OperandValue(null)) : (<>c.<>9__20_11 ??= x => new OperandValue(x.ToValue()));
                return new FunctionOperator(functionType, newOperandProperty.Yield<OperandProperty>().Concat<CriteriaOperator>(valuesData.Select<ValueData, CriteriaOperator>(selector))).ToOption<CriteriaOperator>();
            }, null, null, not, null, @null);
        }

        internal static LeafNodeModel Create(NodeClient nodeClient, CriteriaOperator filter) => 
            new LeafNodeModel(nodeClient).If<LeafNodeModel>(node => node.Update(filter));

        private FilterModelClient CreateFilterModelClient(string propertyName)
        {
            FilterModelClient client = this.nodeClient.CreateFilterModelClient(propertyName, () => this);
            Action<Lazy<CriteriaOperator>> applyFilter = client.ApplyFilter;
            return client.Update(delegate (Lazy<CriteriaOperator> x) {
                applyFilter(x);
                if (this.removeCommandCore == null)
                {
                    DelegateCommand removeCommandCore = this.removeCommandCore;
                }
                else
                {
                    this.removeCommandCore.RaiseCanExecuteChanged();
                }
                CriteriaOperator filter = this.BuildMinimizedFilter(null);
                this.UpdatePropertyAndParameterSelectors(this.nodeClient.GetColumns(filter), filter);
            }, null, null);
        }

        private MultiFilterModel CreateMultiFilterModel(string propertyName, CriteriaOperator filter)
        {
            OperandListObserver<FilterModelBase> observer = new OperandListObserver<FilterModelBase>(delegate (FilterModelBase _) {
                this.nodeClient.OperandListObserver.OnAdding(this);
            }, delegate (FilterModelBase _) {
                this.nodeClient.OperandListObserver.OnRemoving(this);
            });
            MultiFilterModel model = MultiFilterModelFactory.CreateFilterEditorModel(this.CreateFilterModelClient(propertyName), new FilterModelValueItemInfo(() => this.getAllowedOperandTypes(), () => this.Properties.ToReadOnlyCollection<PropertySelectorModelBase>(), () => this.getParameters(), () => this.ShowSearchPanel), observer, menuItems => this.nodeClient.SubstituteOperatorMenuItems(menuItems, filter, propertyName), this.nodeClient.SelectTemplate);
            model.ShowCounts = this.nodeClient.ShowCounts;
            return model;
        }

        private static PropertySelectorModelBase[] CreatePropertySelectorModels(IList<FieldItem> columns) => 
            new HierarchyToSelfReferenceSourceConverter<FieldItem, PropertySelectorModelBase>(<>c.<>9__22_0 ??= delegate (FieldItem source, int id, int parentID) {
                DataTemplateSelector captionSelector = source.CaptionTemplateSelector ?? source.CaptionTemplate.With<DataTemplate, ActualTemplateSelectorWrapper>((<>c.<>9__22_1 ??= x => new ActualTemplateSelectorWrapper(null, x)));
                return ((source.Children.Count != 0) ? ((PropertySelectorModelBase) PropertySelectorBandModel.CreateSelfReferenceModel(source.Caption, captionSelector, id, parentID)) : ((PropertySelectorModelBase) PropertySelectorColumnModel.CreateSelfReferenceModel(source.FieldName, source.Caption, captionSelector, source.SelectedCaptionTemplateSelector ?? source.SelectedCaptionTemplate.With<DataTemplate, ActualTemplateSelectorWrapper>((<>c.<>9__22_2 ??= x => new ActualTemplateSelectorWrapper(null, x))), id, parentID)));
            }, <>c.<>9__22_3 ??= x => x.Children).Convert(columns);

        private static string GetOperandPropertyName(CriteriaOperator filter)
        {
            Func<string, ValueData, BinaryOperatorType, Option<string>> binary = <>c.<>9__14_0;
            if (<>c.<>9__14_0 == null)
            {
                Func<string, ValueData, BinaryOperatorType, Option<string>> local1 = <>c.<>9__14_0;
                binary = <>c.<>9__14_0 = (name, _, __) => name.ToOption<string>();
            }
            Func<string, ValueData[], Option<string>> func2 = <>c.<>9__14_2 ??= (name, _) => name.ToOption<string>();
            if (<>c.<>9__14_5 == null)
            {
                Func<string, ValueData[], Option<string>> local6 = <>c.<>9__14_2 ??= (name, _) => name.ToOption<string>();
                func2 = (Func<string, ValueData[], Option<string>>) (<>c.<>9__14_5 = name => name);
            }
            return filter.MapExtended<string>(binary, (<>c.<>9__14_1 ??= (name, _) => name), ((Func<string, ValueData[], Option<string>>) (<>c.<>9__14_3 ??= (name, _, __) => name.ToOption<string>())), null, null, ((GroupOperatorMapper<string>) <>c.<>9__14_5), ((GroupOperatorMapper<string>) (<>c.<>9__14_4 ??= (name, _, __) => name.ToOption<string>())), ((NotOperatorMapper<string>) func2), (<>c.<>9__14_6 ??= ((FallbackMapper<string>) (_ => null))), null);
        }

        internal Type GetPropertySelectorColumnModelType(PropertySelectorColumnModel model = null)
        {
            PropertySelectorColumnModel selectedProperty = model;
            if (model == null)
            {
                PropertySelectorColumnModel local1 = model;
                selectedProperty = this.SelectedProperty;
            }
            return this.nodeClient.GetColumnType(selectedProperty.Name);
        }

        private void OnMultiModelChanged()
        {
            if (this.removeCommandCore == null)
            {
                DelegateCommand removeCommandCore = this.removeCommandCore;
            }
            else
            {
                this.removeCommandCore.RaiseCanExecuteChanged();
            }
        }

        private void OnSelectedPropertyChanged()
        {
            if (!this.selectedPropertyChangeLocker.IsLocked)
            {
                string name = this.SelectedProperty.Name;
                CriteriaOperator filter = ChangeOperandPropertyName(this.BuildMinimizedFilter(null), name, this.GetPropertySelectorColumnModelType(this.SelectedProperty));
                MultiFilterModel model = this.CreateMultiFilterModel(name, filter);
                model.Update(model.CanUpdate(filter) ? filter : null);
                this.MultiModel = model;
            }
        }

        internal static PropertySelectorColumnModel SelectProperty(IEnumerable<PropertySelectorModelBase> properties, string name) => 
            (name != null) ? (properties.OfType<PropertySelectorColumnModel>().FirstOrDefault<PropertySelectorColumnModel>(x => (x.Name == name)) ?? PropertySelectorColumnModel.CreateStandAloneModel(name)) : null;

        private bool Update(CriteriaOperator filter)
        {
            Lazy<NodeClientColumnsInfo> lazy = new Lazy<NodeClientColumnsInfo>(() => this.nodeClient.GetColumns(filter));
            IEnumerable<string> enumerable = Enumerable.Empty<string>();
            if (!filter.ReferenceEqualsNull())
            {
                Func<string, IEnumerable<string>> evaluator = <>c.<>9__12_1;
                if (<>c.<>9__12_1 == null)
                {
                    Func<string, IEnumerable<string>> local1 = <>c.<>9__12_1;
                    evaluator = <>c.<>9__12_1 = x => x.Yield<string>();
                }
                enumerable = GetOperandPropertyName(filter).Return<string, IEnumerable<string>>(evaluator, <>c.<>9__12_2 ??= () => Enumerable.Empty<string>());
            }
            else
            {
                IEnumerable<string> enumerable1;
                if (this.nodeClient.DefaultColumnName != null)
                {
                    enumerable1 = this.nodeClient.DefaultColumnName.Yield<string>();
                }
                else
                {
                    Func<FieldItem, IEnumerable<FieldItem>> getItems = <>c.<>9__12_3;
                    if (<>c.<>9__12_3 == null)
                    {
                        Func<FieldItem, IEnumerable<FieldItem>> local3 = <>c.<>9__12_3;
                        getItems = <>c.<>9__12_3 = fi => fi.Children;
                    }
                    Func<FieldItem, string> selector = <>c.<>9__12_4;
                    if (<>c.<>9__12_4 == null)
                    {
                        Func<FieldItem, string> local4 = <>c.<>9__12_4;
                        selector = <>c.<>9__12_4 = x => x.FieldName;
                    }
                    Func<string, bool> predicate = <>c.<>9__12_5;
                    if (<>c.<>9__12_5 == null)
                    {
                        Func<string, bool> local5 = <>c.<>9__12_5;
                        predicate = <>c.<>9__12_5 = x => x != null;
                    }
                    enumerable1 = lazy.Value.Columns.Flatten<FieldItem>(getItems).Select<FieldItem, string>(selector).Where<string>(predicate);
                }
                enumerable = enumerable1;
            }
            MultiFilterModel multiModel = (from propertyName in enumerable select this.CreateMultiFilterModel(propertyName, filter)).FirstOrDefault<MultiFilterModel>(m => m.CanSelectItem(filter));
            if (multiModel == null)
            {
                return false;
            }
            this.UpdatePropertyAndParameterSelectors(lazy.Value, filter);
            this.selectedPropertyChangeLocker.DoLockedAction<PropertySelectorColumnModel>(delegate {
                PropertySelectorColumnModel model;
                this.SelectedProperty = model = SelectProperty(this.Properties, multiModel.PropertyName);
                return model;
            });
            multiModel.Update(filter);
            this.MultiModel = multiModel;
            return true;
        }

        private void UpdatePropertyAndParameterSelectors(NodeClientColumnsInfo info, CriteriaOperator filter)
        {
            this.Properties = CreatePropertySelectorModels(info.Columns);
            this.getParameters = () => this.nodeClient.GetParameters(filter);
            this.getAllowedOperandTypes = () => this.nodeClient.GetAllowedOperandTypes(filter);
            this.ShowSearchPanel = info.AllowSearch;
        }

        public MultiFilterModel MultiModel
        {
            get => 
                base.GetProperty<MultiFilterModel>(System.Linq.Expressions.Expression.Lambda<Func<MultiFilterModel>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(LeafNodeModel)), (MethodInfo) methodof(LeafNodeModel.get_MultiModel)), new ParameterExpression[0]));
            private set => 
                base.SetProperty<MultiFilterModel>(System.Linq.Expressions.Expression.Lambda<Func<MultiFilterModel>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(LeafNodeModel)), (MethodInfo) methodof(LeafNodeModel.get_MultiModel)), new ParameterExpression[0]), value, new Action(this.OnMultiModelChanged));
        }

        public PropertySelectorColumnModel SelectedProperty
        {
            get => 
                base.GetProperty<PropertySelectorColumnModel>(System.Linq.Expressions.Expression.Lambda<Func<PropertySelectorColumnModel>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(LeafNodeModel)), (MethodInfo) methodof(LeafNodeModel.get_SelectedProperty)), new ParameterExpression[0]));
            set => 
                base.SetProperty<PropertySelectorColumnModel>(System.Linq.Expressions.Expression.Lambda<Func<PropertySelectorColumnModel>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(LeafNodeModel)), (MethodInfo) methodof(LeafNodeModel.get_SelectedProperty)), new ParameterExpression[0]), value, new Action(this.OnSelectedPropertyChanged));
        }

        public IList<PropertySelectorModelBase> Properties
        {
            get => 
                base.GetProperty<IList<PropertySelectorModelBase>>(System.Linq.Expressions.Expression.Lambda<Func<IList<PropertySelectorModelBase>>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(LeafNodeModel)), (MethodInfo) methodof(LeafNodeModel.get_Properties)), new ParameterExpression[0]));
            private set => 
                base.SetProperty<IList<PropertySelectorModelBase>>(System.Linq.Expressions.Expression.Lambda<Func<IList<PropertySelectorModelBase>>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(LeafNodeModel)), (MethodInfo) methodof(LeafNodeModel.get_Properties)), new ParameterExpression[0]), value);
        }

        public bool ShowSearchPanel
        {
            get => 
                base.GetProperty<bool>(System.Linq.Expressions.Expression.Lambda<Func<bool>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(LeafNodeModel)), (MethodInfo) methodof(LeafNodeModel.get_ShowSearchPanel)), new ParameterExpression[0]));
            private set => 
                base.SetProperty<bool>(System.Linq.Expressions.Expression.Lambda<Func<bool>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(LeafNodeModel)), (MethodInfo) methodof(LeafNodeModel.get_ShowSearchPanel)), new ParameterExpression[0]), value);
        }

        public override ICommand RemoveCommand
        {
            get
            {
                this.removeCommandCore ??= new DelegateCommand(delegate {
                    this.nodeClient.RemoveNode(this);
                }, delegate {
                    string name;
                    PropertySelectorColumnModel selectedProperty = this.SelectedProperty;
                    if (selectedProperty != null)
                    {
                        name = selectedProperty.Name;
                    }
                    else
                    {
                        PropertySelectorColumnModel local1 = selectedProperty;
                        name = null;
                    }
                    return this.nodeClient.CanExecuteRemoveAction(new Lazy<CriteriaOperator>(() => this.BuildEvaluableFilter(null)), name);
                }, false);
                return this.removeCommandCore;
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly LeafNodeModel.<>c <>9 = new LeafNodeModel.<>c();
            public static Func<string, IEnumerable<string>> <>9__12_1;
            public static Func<IEnumerable<string>> <>9__12_2;
            public static Func<FieldItem, IEnumerable<FieldItem>> <>9__12_3;
            public static Func<FieldItem, string> <>9__12_4;
            public static Func<string, bool> <>9__12_5;
            public static Func<string, ValueData, BinaryOperatorType, Option<string>> <>9__14_0;
            public static UnaryOperatorMapper<string> <>9__14_1;
            public static Func<string, ValueData[], Option<string>> <>9__14_2;
            public static Func<string, ValueData, ValueData, Option<string>> <>9__14_3;
            public static Func<string, ValueData[], FunctionOperatorType, Option<string>> <>9__14_4;
            public static NotOperatorMapper<string> <>9__14_5;
            public static FallbackMapper<string> <>9__14_6;
            public static Func<string, bool> <>9__20_2;
            public static Func<string, bool> <>9__20_3;
            public static Func<LocalDateTimeFunction, bool> <>9__20_4;
            public static Func<ValueData, CriteriaOperator> <>9__20_8;
            public static Func<ValueData, CriteriaOperator> <>9__20_11;
            public static Func<ValueData, CriteriaOperator> <>9__20_12;
            public static NotOperatorMapper<CriteriaOperator> <>9__20_13;
            public static NullMapper<CriteriaOperator> <>9__20_14;
            public static Func<DataTemplate, ActualTemplateSelectorWrapper> <>9__22_1;
            public static Func<DataTemplate, ActualTemplateSelectorWrapper> <>9__22_2;
            public static Func<FieldItem, int, int, PropertySelectorModelBase> <>9__22_0;
            public static Func<FieldItem, IList<FieldItem>> <>9__22_3;

            internal CriteriaOperator <ChangeOperandPropertyName>b__20_11(ValueData x) => 
                new OperandValue(x.ToValue());

            internal CriteriaOperator <ChangeOperandPropertyName>b__20_12(ValueData __) => 
                new OperandValue(null);

            internal CriteriaOperator <ChangeOperandPropertyName>b__20_13(CriteriaOperator x) => 
                new UnaryOperator(UnaryOperatorType.Not, x);

            internal CriteriaOperator <ChangeOperandPropertyName>b__20_14() => 
                null;

            internal bool <ChangeOperandPropertyName>b__20_2(string propertyName) => 
                false;

            internal bool <ChangeOperandPropertyName>b__20_3(string parameter) => 
                false;

            internal bool <ChangeOperandPropertyName>b__20_4(LocalDateTimeFunction function) => 
                false;

            internal CriteriaOperator <ChangeOperandPropertyName>b__20_8(ValueData x) => 
                x.ToCriteria();

            internal PropertySelectorModelBase <CreatePropertySelectorModels>b__22_0(FieldItem source, int id, int parentID)
            {
                DataTemplateSelector captionSelector = source.CaptionTemplateSelector ?? source.CaptionTemplate.With<DataTemplate, ActualTemplateSelectorWrapper>((<>9__22_1 ??= x => new ActualTemplateSelectorWrapper(null, x)));
                return ((source.Children.Count != 0) ? ((PropertySelectorModelBase) PropertySelectorBandModel.CreateSelfReferenceModel(source.Caption, captionSelector, id, parentID)) : ((PropertySelectorModelBase) PropertySelectorColumnModel.CreateSelfReferenceModel(source.FieldName, source.Caption, captionSelector, source.SelectedCaptionTemplateSelector ?? source.SelectedCaptionTemplate.With<DataTemplate, ActualTemplateSelectorWrapper>((<>9__22_2 ??= x => new ActualTemplateSelectorWrapper(null, x))), id, parentID)));
            }

            internal ActualTemplateSelectorWrapper <CreatePropertySelectorModels>b__22_1(DataTemplate x) => 
                new ActualTemplateSelectorWrapper(null, x);

            internal ActualTemplateSelectorWrapper <CreatePropertySelectorModels>b__22_2(DataTemplate x) => 
                new ActualTemplateSelectorWrapper(null, x);

            internal IList<FieldItem> <CreatePropertySelectorModels>b__22_3(FieldItem x) => 
                x.Children;

            internal Option<string> <GetOperandPropertyName>b__14_0(string name, ValueData _, BinaryOperatorType __) => 
                name.ToOption<string>();

            internal string <GetOperandPropertyName>b__14_1(string name, UnaryOperatorType _) => 
                name;

            internal Option<string> <GetOperandPropertyName>b__14_2(string name, ValueData[] _) => 
                name.ToOption<string>();

            internal Option<string> <GetOperandPropertyName>b__14_3(string name, ValueData _, ValueData __) => 
                name.ToOption<string>();

            internal Option<string> <GetOperandPropertyName>b__14_4(string name, ValueData[] _, FunctionOperatorType __) => 
                name.ToOption<string>();

            internal string <GetOperandPropertyName>b__14_5(string name) => 
                name;

            internal string <GetOperandPropertyName>b__14_6(CriteriaOperator _) => 
                null;

            internal IEnumerable<string> <Update>b__12_1(string x) => 
                x.Yield<string>();

            internal IEnumerable<string> <Update>b__12_2() => 
                Enumerable.Empty<string>();

            internal IEnumerable<FieldItem> <Update>b__12_3(FieldItem fi) => 
                fi.Children;

            internal string <Update>b__12_4(FieldItem x) => 
                x.FieldName;

            internal bool <Update>b__12_5(string x) => 
                x != null;
        }
    }
}

