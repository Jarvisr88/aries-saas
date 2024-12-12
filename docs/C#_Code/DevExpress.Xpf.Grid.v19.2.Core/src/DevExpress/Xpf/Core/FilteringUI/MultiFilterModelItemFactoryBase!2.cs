namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Data.Filtering;
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core.FilteringUI.Native;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Settings;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;

    internal abstract class MultiFilterModelItemFactoryBase<TID, TFactory> where TID: class where TFactory: OperatorMenuItemIdentityFactoryBase<TID>
    {
        protected readonly Func<FilterModelBase, DataTemplate> selectTemplate;
        private readonly OperandListObserver<FilterModelBase> operandListObserver;
        protected readonly TFactory identityFactory;
        private readonly Dictionary<TID, Func<BaseEditSettings[], MultiFilterModelItem>> builtInOperators;

        public MultiFilterModelItemFactoryBase(FilterModelValueItemInfo info, OperandListObserver<FilterModelBase> operandListObserver, Func<FilterModelBase, DataTemplate> selectTemplate, TFactory identityFactory)
        {
            Func<FilterModelBase, DataTemplate> func1 = selectTemplate;
            if (selectTemplate == null)
            {
                Func<FilterModelBase, DataTemplate> local1 = selectTemplate;
                func1 = <>c<TID, TFactory>.<>9__4_0;
                if (<>c<TID, TFactory>.<>9__4_0 == null)
                {
                    Func<FilterModelBase, DataTemplate> local2 = <>c<TID, TFactory>.<>9__4_0;
                    func1 = <>c<TID, TFactory>.<>9__4_0 = (Func<FilterModelBase, DataTemplate>) (_ => null);
                }
            }
            this.selectTemplate = func1;
            this.operandListObserver = operandListObserver;
            this.identityFactory = identityFactory;
            this.builtInOperators = new Dictionary<TID, Func<BaseEditSettings[], MultiFilterModelItem>>();
            this.RegisterBuiltInOperators(info, this.builtInOperators);
            this.RegisterPredefinedFormatConditionOperators(this.builtInOperators);
        }

        private static Func<BaseEditSettings[], OperandRestoreAdapterFactory> BinaryFactory(FilterModelValueItemInfo info, BinaryOperatorType type) => 
            MultiFilterModelItemFactoryBase<TID, TFactory>.BinaryFactory(info, CriteriaConverterFactory.CreateBinary(type));

        private static Func<BaseEditSettings[], OperandRestoreAdapterFactory> BinaryFactory(FilterModelValueItemInfo info, FunctionOperatorType type) => 
            MultiFilterModelItemFactoryBase<TID, TFactory>.BinaryFactory(info, CriteriaConverterFactory.CreateBinaryFunction(type));

        private static Func<BaseEditSettings[], OperandRestoreAdapterFactory> BinaryFactory(FilterModelValueItemInfo info, CriteriaConverter<ValueData> converter) => 
            editSettings => OperandRestoreAdapterFactories.Binary(client => new BinaryFilterModel(client, info, converter, MultiFilterModelItemFactoryBase<TID, TFactory>.CreateSettings(client, 0, editSettings)));

        public abstract MultiFilterModelItem Create(TID id, BaseEditSettings[] editSettings = null);
        protected Func<BaseEditSettings[], MultiFilterModelItem> CreateBuiltIn(TID id)
        {
            Func<BaseEditSettings[], MultiFilterModelItem> func = null;
            return (!this.builtInOperators.TryGetValue(id, out func) ? (<>c<TID, TFactory>.<>9__9_0 ??= ((Func<BaseEditSettings[], MultiFilterModelItem>) (_ => null))) : func);
        }

        protected Func<BaseEditSettings[], MultiFilterModelItem> CreateBuiltIn(EditorStringId displayNameId, Func<BaseEditSettings[], OperandRestoreAdapterFactory> factory, string imageName) => 
            editSettings => new MultiFilterModelItem(EditorLocalizer.GetString(displayNameId), factory(editSettings), FilterImageProvider.GetImage(imageName), null, ((MultiFilterModelItemFactoryBase<TID, TFactory>) this).selectTemplate);

        protected Func<BaseEditSettings[], MultiFilterModelItem> CreateCustom(CustomOperatorMenuItemIdentity id) => 
            delegate (BaseEditSettings[] editSettings) {
                string name = id.Name;
                return new MultiFilterModelItem(name, ((MultiFilterModelItemFactoryBase<TID, TFactory>) this).CreateCustomFactory(name)(editSettings), null, null, ((MultiFilterModelItemFactoryBase<TID, TFactory>) this).selectTemplate);
            };

        private Func<BaseEditSettings[], OperandRestoreAdapterFactory> CreateCustomFactory(string customName) => 
            delegate (BaseEditSettings[] editSettings) {
                Func<FilterModelClient, FilterModelBase> <>9__2;
                ICustomFunctionOperatorBrowsable customFunctionOperatorBrowsable = CustomFunctionHelper.GetCustomFunctionOperatorBrowsable(customName);
                if (customFunctionOperatorBrowsable == null)
                {
                    throw new InvalidOperationException($"The {customName} is not registered. Register it with the CriteriaOperator.RegisterCustomFunction method.");
                }
                if ((customFunctionOperatorBrowsable.MinOperandCount != customFunctionOperatorBrowsable.MaxOperandCount) || !customFunctionOperatorBrowsable.IsValidOperandCount(customFunctionOperatorBrowsable.MaxOperandCount))
                {
                    throw new InvalidOperationException("You cannot edit a custom function with unequal the MinOperandCount and MaxOperandCount properties.");
                }
                int operandCountIncludingPropertyValue = customFunctionOperatorBrowsable.MaxOperandCount;
                if (operandCountIncludingPropertyValue == 0)
                {
                    Func<FilterModelClient, FilterModelBase> <>9__1;
                    Func<FilterModelClient, FilterModelBase> func3 = <>9__1;
                    if (<>9__1 == null)
                    {
                        Func<FilterModelClient, FilterModelBase> local1 = <>9__1;
                        func3 = <>9__1 = client => ConstantFilterModel.CreateFilter(client, new FunctionOperator(customName, Enumerable.Empty<CriteriaOperator>()));
                    }
                    return OperandRestoreAdapterFactories.Default(func3);
                }
                if (operandCountIncludingPropertyValue != 1)
                {
                    return (operandCountIncludingPropertyValue != 2) ? ((operandCountIncludingPropertyValue != 3) ? OperandRestoreAdapterFactories.Default(client => new VariadicFilterModel(client, null, CriteriaConverterFactory.CreateCustomVariadicFunction(customName), ((MultiFilterModelItemFactoryBase<TID, TFactory>) this).operandListObserver, new int?(operandCountIncludingPropertyValue - 1), index => MultiFilterModelItemFactoryBase<TID, TFactory>.CreateSettings(client, index, editSettings))) : OperandRestoreAdapterFactories.Ternary(client => new TernaryFilterModel(client, null, CriteriaConverterFactory.CreateCustomTernaryFunction(customName), MultiFilterModelItemFactoryBase<TID, TFactory>.CreateSettings(client, 0, editSettings), MultiFilterModelItemFactoryBase<TID, TFactory>.CreateSettings(client, 1, editSettings)))) : OperandRestoreAdapterFactories.Binary(client => new BinaryFilterModel(client, null, CriteriaConverterFactory.CreateCustomBinaryFunction(customName), MultiFilterModelItemFactoryBase<TID, TFactory>.CreateSettings(client, 0, editSettings)));
                }
                Func<FilterModelClient, FilterModelBase> createModel = <>9__2;
                if (<>9__2 == null)
                {
                    Func<FilterModelClient, FilterModelBase> local2 = <>9__2;
                    createModel = <>9__2 = client => ConstantFilterModel.CreateFilter(client, new FunctionOperator(customName, new CriteriaOperator[] { new OperandProperty(client.PropertyName) }));
                }
                return OperandRestoreAdapterFactories.Default(createModel);
            };

        private static EditSettingsInfo CreateSettings(FilterModelClient client, int index, BaseEditSettings[] editSettings)
        {
            if ((editSettings == null) || ((index < 0) || (index >= editSettings.Length)))
            {
                return EditSettingsInfoFactory.Default(client.GetColumn());
            }
            Func<BaseEditSettings, EditSettingsInfo> evaluator = <>c<TID, TFactory>.<>9__20_0;
            if (<>c<TID, TFactory>.<>9__20_0 == null)
            {
                Func<BaseEditSettings, EditSettingsInfo> local1 = <>c<TID, TFactory>.<>9__20_0;
                evaluator = <>c<TID, TFactory>.<>9__20_0 = settings => EditSettingsInfo.CreateUserDefined(settings);
            }
            EditSettingsInfo local2 = editSettings[index].With<BaseEditSettings, EditSettingsInfo>(evaluator);
            EditSettingsInfo local4 = local2;
            if (local2 == null)
            {
                EditSettingsInfo local3 = local2;
                local4 = EditSettingsInfoFactory.Default(client.GetColumn());
            }
            return local4;
        }

        protected virtual void RegisterBuiltInOperators(FilterModelValueItemInfo info, Dictionary<TID, Func<BaseEditSettings[], MultiFilterModelItem>> builtInOperators)
        {
            builtInOperators.Add(this.identityFactory.CreateBinary(BinaryOperatorType.Equal), this.CreateBuiltIn(EditorStringId.FilterClauseEquals, MultiFilterModelItemFactoryBase<TID, TFactory>.BinaryFactory(info, BinaryOperatorType.Equal), "Equals"));
            builtInOperators.Add(this.identityFactory.CreateBinary(BinaryOperatorType.NotEqual), this.CreateBuiltIn(EditorStringId.FilterClauseDoesNotEqual, MultiFilterModelItemFactoryBase<TID, TFactory>.BinaryFactory(info, BinaryOperatorType.NotEqual), "DoesNotEqual"));
            builtInOperators.Add(this.identityFactory.CreateBinary(BinaryOperatorType.Greater), this.CreateBuiltIn(EditorStringId.FilterClauseGreater, MultiFilterModelItemFactoryBase<TID, TFactory>.BinaryFactory(info, BinaryOperatorType.Greater), "Greater"));
            builtInOperators.Add(this.identityFactory.CreateBinary(BinaryOperatorType.GreaterOrEqual), this.CreateBuiltIn(EditorStringId.FilterClauseGreaterOrEqual, MultiFilterModelItemFactoryBase<TID, TFactory>.BinaryFactory(info, BinaryOperatorType.GreaterOrEqual), "GreaterOrEqual"));
            builtInOperators.Add(this.identityFactory.CreateBinary(BinaryOperatorType.Less), this.CreateBuiltIn(EditorStringId.FilterClauseLess, MultiFilterModelItemFactoryBase<TID, TFactory>.BinaryFactory(info, BinaryOperatorType.Less), "Less"));
            builtInOperators.Add(this.identityFactory.CreateBinary(BinaryOperatorType.LessOrEqual), this.CreateBuiltIn(EditorStringId.FilterClauseLessOrEqual, MultiFilterModelItemFactoryBase<TID, TFactory>.BinaryFactory(info, BinaryOperatorType.LessOrEqual), "LessOrEqual"));
            builtInOperators.Add(this.identityFactory.CreateBetween(), this.CreateBuiltIn(EditorStringId.FilterClauseBetween, MultiFilterModelItemFactoryBase<TID, TFactory>.TernaryFactory(info, CriteriaConverterFactory.CreateBetween()), "Between"));
            builtInOperators.Add(this.identityFactory.CreateNotBetween(), this.CreateBuiltIn(EditorStringId.FilterClauseNotBetween, MultiFilterModelItemFactoryBase<TID, TFactory>.TernaryFactory(info, CriteriaConverterFactory.CreateNotBetween()), "NotBetween"));
            builtInOperators.Add(this.identityFactory.CreateIsNull(), this.CreateBuiltIn(EditorStringId.FilterClauseIsNull, MultiFilterModelItemFactoryBase<TID, TFactory>.UnaryFactory(new Func<FilterModelClient, FilterModel>(ConstantFilterModel.CreateIsNull)), "IsNull"));
            builtInOperators.Add(this.identityFactory.CreateIsNotNull(), this.CreateBuiltIn(EditorStringId.FilterClauseIsNotNull, MultiFilterModelItemFactoryBase<TID, TFactory>.UnaryFactory(new Func<FilterModelClient, FilterModel>(ConstantFilterModel.CreateIsNotNull)), "IsNotNull"));
            builtInOperators.Add(this.identityFactory.CreateFunction(FunctionOperatorType.Contains), this.CreateBuiltIn(EditorStringId.FilterClauseContains, MultiFilterModelItemFactoryBase<TID, TFactory>.BinaryFactory(info, FunctionOperatorType.Contains), "Contains"));
            builtInOperators.Add(this.identityFactory.CreateDoesNotContain(), this.CreateBuiltIn(EditorStringId.FilterClauseDoesNotContain, MultiFilterModelItemFactoryBase<TID, TFactory>.BinaryFactory(info, CriteriaConverterFactory.CreateNotContains()), "DoesNotContain"));
            builtInOperators.Add(this.identityFactory.CreateFunction(FunctionOperatorType.StartsWith), this.CreateBuiltIn(EditorStringId.FilterCriteriaToStringFunctionStartsWith, MultiFilterModelItemFactoryBase<TID, TFactory>.BinaryFactory(info, FunctionOperatorType.StartsWith), "BeginsWith"));
            builtInOperators.Add(this.identityFactory.CreateFunction(FunctionOperatorType.EndsWith), this.CreateBuiltIn(EditorStringId.FilterClauseEndsWith, MultiFilterModelItemFactoryBase<TID, TFactory>.BinaryFactory(info, FunctionOperatorType.EndsWith), "EndsWith"));
            builtInOperators.Add(this.identityFactory.CreateFunction(FunctionOperatorType.IsNullOrEmpty), this.CreateBuiltIn(EditorStringId.FilterClauseIsNullOrEmpty, MultiFilterModelItemFactoryBase<TID, TFactory>.UnaryFunctionFactory(FunctionOperatorType.IsNullOrEmpty), "IsNullOrEmpty"));
            builtInOperators.Add(this.identityFactory.CreateIsNotNullOrEmpty(), this.CreateBuiltIn(EditorStringId.FilterClauseIsNotNullOrEmpty, MultiFilterModelItemFactoryBase<TID, TFactory>.UnaryFactory(new Func<FilterModelClient, FilterModel>(ConstantFilterModel.IsNotNullOrEmpty)), "IsNotNullOrEmpty"));
            builtInOperators.Add(this.identityFactory.CreateIsOnDate(), this.CreateBuiltIn(EditorStringId.FilterClauseIsOn, MultiFilterModelItemFactoryBase<TID, TFactory>.BinaryFactory(info, CriteriaConverterFactory.CreateIsOnDate()), "Equals"));
            builtInOperators.Add(this.identityFactory.CreateIsNotOnDate(), this.CreateBuiltIn(EditorStringId.FilterClauseIsNotOn, MultiFilterModelItemFactoryBase<TID, TFactory>.BinaryFactory(info, CriteriaConverterFactory.CreateIsNotOnDate()), "DoesNotEqual"));
            builtInOperators.Add(this.identityFactory.CreateLike(), this.CreateBuiltIn(EditorStringId.FilterClauseLike, MultiFilterModelItemFactoryBase<TID, TFactory>.BinaryFactory(info, CriteriaConverterFactory.CreateLike()), "Like"));
            builtInOperators.Add(this.identityFactory.CreateNotLike(), this.CreateBuiltIn(EditorStringId.FilterClauseNotLike, MultiFilterModelItemFactoryBase<TID, TFactory>.BinaryFactory(info, CriteriaConverterFactory.CreateNotLike()), "NotLike"));
            builtInOperators.Add(this.identityFactory.CreateBetweenDates(), this.CreateBuiltIn(EditorStringId.FilterClauseBetweenDates, MultiFilterModelItemFactoryBase<TID, TFactory>.TernaryFactory(info, CriteriaConverterFactory.CreateBetweenDates()), "Between"));
            builtInOperators.Add(this.identityFactory.CreateFunction(FunctionOperatorType.IsJanuary), this.CreateBuiltIn(EditorStringId.FilterClauseIsJanuary, MultiFilterModelItemFactoryBase<TID, TFactory>.UnaryFunctionFactory(FunctionOperatorType.IsJanuary), null));
            builtInOperators.Add(this.identityFactory.CreateFunction(FunctionOperatorType.IsFebruary), this.CreateBuiltIn(EditorStringId.FilterClauseIsFebruary, MultiFilterModelItemFactoryBase<TID, TFactory>.UnaryFunctionFactory(FunctionOperatorType.IsFebruary), null));
            builtInOperators.Add(this.identityFactory.CreateFunction(FunctionOperatorType.IsMarch), this.CreateBuiltIn(EditorStringId.FilterClauseIsMarch, MultiFilterModelItemFactoryBase<TID, TFactory>.UnaryFunctionFactory(FunctionOperatorType.IsMarch), null));
            builtInOperators.Add(this.identityFactory.CreateFunction(FunctionOperatorType.IsApril), this.CreateBuiltIn(EditorStringId.FilterClauseIsApril, MultiFilterModelItemFactoryBase<TID, TFactory>.UnaryFunctionFactory(FunctionOperatorType.IsApril), null));
            builtInOperators.Add(this.identityFactory.CreateFunction(FunctionOperatorType.IsMay), this.CreateBuiltIn(EditorStringId.FilterClauseIsMay, MultiFilterModelItemFactoryBase<TID, TFactory>.UnaryFunctionFactory(FunctionOperatorType.IsMay), null));
            builtInOperators.Add(this.identityFactory.CreateFunction(FunctionOperatorType.IsJune), this.CreateBuiltIn(EditorStringId.FilterClauseIsJune, MultiFilterModelItemFactoryBase<TID, TFactory>.UnaryFunctionFactory(FunctionOperatorType.IsJune), null));
            builtInOperators.Add(this.identityFactory.CreateFunction(FunctionOperatorType.IsJuly), this.CreateBuiltIn(EditorStringId.FilterClauseIsJuly, MultiFilterModelItemFactoryBase<TID, TFactory>.UnaryFunctionFactory(FunctionOperatorType.IsJuly), null));
            builtInOperators.Add(this.identityFactory.CreateFunction(FunctionOperatorType.IsAugust), this.CreateBuiltIn(EditorStringId.FilterClauseIsAugust, MultiFilterModelItemFactoryBase<TID, TFactory>.UnaryFunctionFactory(FunctionOperatorType.IsAugust), null));
            builtInOperators.Add(this.identityFactory.CreateFunction(FunctionOperatorType.IsSeptember), this.CreateBuiltIn(EditorStringId.FilterClauseIsSeptember, MultiFilterModelItemFactoryBase<TID, TFactory>.UnaryFunctionFactory(FunctionOperatorType.IsSeptember), null));
            builtInOperators.Add(this.identityFactory.CreateFunction(FunctionOperatorType.IsOctober), this.CreateBuiltIn(EditorStringId.FilterClauseIsOctober, MultiFilterModelItemFactoryBase<TID, TFactory>.UnaryFunctionFactory(FunctionOperatorType.IsOctober), null));
            builtInOperators.Add(this.identityFactory.CreateFunction(FunctionOperatorType.IsNovember), this.CreateBuiltIn(EditorStringId.FilterClauseIsNovember, MultiFilterModelItemFactoryBase<TID, TFactory>.UnaryFunctionFactory(FunctionOperatorType.IsNovember), null));
            builtInOperators.Add(this.identityFactory.CreateFunction(FunctionOperatorType.IsDecember), this.CreateBuiltIn(EditorStringId.FilterClauseIsDecember, MultiFilterModelItemFactoryBase<TID, TFactory>.UnaryFunctionFactory(FunctionOperatorType.IsDecember), null));
        }

        private void RegisterPredefinedFormatCondition(Dictionary<TID, Func<BaseEditSettings[], MultiFilterModelItem>> builtInOperators, PredefinedFormatConditionType type, EditorStringId editorStringId, string imageName)
        {
            builtInOperators.Add(this.identityFactory.CreatePredefinedFormatCondition(type), this.CreateBuiltIn(editorStringId, _ => OperandRestoreAdapterFactories.PredefinedFormatCondition(type), imageName));
        }

        private void RegisterPredefinedFormatConditionOperators(Dictionary<TID, Func<BaseEditSettings[], MultiFilterModelItem>> builtInOperators)
        {
            this.RegisterPredefinedFormatCondition(builtInOperators, PredefinedFormatConditionType.Top, EditorStringId.PredefinedFormatConditionTop, "Top");
            this.RegisterPredefinedFormatCondition(builtInOperators, PredefinedFormatConditionType.Bottom, EditorStringId.PredefinedFormatConditionBottom, "Bottom");
            this.RegisterPredefinedFormatCondition(builtInOperators, PredefinedFormatConditionType.AboveAverage, EditorStringId.PredefinedFormatConditionAboveAverage, "AboveAverage");
            this.RegisterPredefinedFormatCondition(builtInOperators, PredefinedFormatConditionType.BelowAverage, EditorStringId.PredefinedFormatConditionBelowAverage, "BelowAverage");
            this.RegisterPredefinedFormatCondition(builtInOperators, PredefinedFormatConditionType.Unique, EditorStringId.PredefinedFormatConditionUnique, "Unique");
            this.RegisterPredefinedFormatCondition(builtInOperators, PredefinedFormatConditionType.Duplicate, EditorStringId.PredefinedFormatConditionDuplicate, "Duplicate");
        }

        private static Func<BaseEditSettings[], OperandRestoreAdapterFactory> TernaryFactory(FilterModelValueItemInfo info, CriteriaConverter<Tuple<ValueData, ValueData>> converter) => 
            editSettings => OperandRestoreAdapterFactories.Ternary(client => new TernaryFilterModel(client, info, converter, MultiFilterModelItemFactoryBase<TID, TFactory>.CreateSettings(client, 0, editSettings), MultiFilterModelItemFactoryBase<TID, TFactory>.CreateSettings(client, 1, editSettings)));

        private static Func<BaseEditSettings[], OperandRestoreAdapterFactory> UnaryFactory(Func<FilterModelClient, FilterModel> createModel) => 
            _ => OperandRestoreAdapterFactories.Default(createModel);

        protected static Func<BaseEditSettings[], OperandRestoreAdapterFactory> UnaryFunctionFactory(FunctionOperatorType type) => 
            MultiFilterModelItemFactoryBase<TID, TFactory>.UnaryFactory(client => ConstantFilterModel.CreateFunction(client, type));

        protected Func<BaseEditSettings[], OperandRestoreAdapterFactory> VariadicFactory(FilterModelValueItemInfo info, CriteriaConverter<ValueData[]> converter) => 
            editSettings => OperandRestoreAdapterFactories.Variadic(delegate (FilterModelClient client) {
                int? fixedOperandCount = null;
                Func<int, EditSettingsInfo> createEditSettings = null;
                if ((editSettings != null) && (editSettings.Length != 0))
                {
                    fixedOperandCount = new int?(editSettings.Length);
                    createEditSettings = index => MultiFilterModelItemFactoryBase<TID, TFactory>.CreateSettings(client, index, editSettings);
                }
                return new VariadicFilterModel(client, info, converter, ((MultiFilterModelItemFactoryBase<TID, TFactory>) this).operandListObserver, fixedOperandCount, createEditSettings);
            });

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly MultiFilterModelItemFactoryBase<TID, TFactory>.<>c <>9;
            public static Func<FilterModelBase, DataTemplate> <>9__4_0;
            public static Func<BaseEditSettings[], MultiFilterModelItem> <>9__9_0;
            public static Func<BaseEditSettings, EditSettingsInfo> <>9__20_0;

            static <>c()
            {
                MultiFilterModelItemFactoryBase<TID, TFactory>.<>c.<>9 = new MultiFilterModelItemFactoryBase<TID, TFactory>.<>c();
            }

            internal DataTemplate <.ctor>b__4_0(FilterModelBase _) => 
                null;

            internal MultiFilterModelItem <CreateBuiltIn>b__9_0(BaseEditSettings[] _) => 
                null;

            internal EditSettingsInfo <CreateSettings>b__20_0(BaseEditSettings settings) => 
                EditSettingsInfo.CreateUserDefined(settings);
        }
    }
}

