namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Data.Filtering;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Settings;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    internal sealed class FilterEditorMultiFilterModelItemFactory : MultiFilterModelItemFactoryBase<IFilterEditorOperatorMenuItemIdentity, FilterEditorOperatorMenuItemIdentityFactory>
    {
        public FilterEditorMultiFilterModelItemFactory(FilterModelValueItemInfo info, OperandListObserver<FilterModelBase> operandListObserver, Func<FilterModelBase, DataTemplate> selectTemplate, FilterEditorOperatorMenuItemIdentityFactory identityFactory) : base(info, operandListObserver, selectTemplate, identityFactory)
        {
        }

        public override MultiFilterModelItem Create(IFilterEditorOperatorMenuItemIdentity id, BaseEditSettings[] editSettings = null) => 
            id.Match<MultiFilterModelItem>(x => this.CreateBuiltIn(x)(editSettings), new Func<PredefinedFilterEditorOperatorMenuItemIdentity, MultiFilterModelItem>(this.CreatePredefined), new Func<FormatConditionFilterEditorOperatorMenuItemIdentity, MultiFilterModelItem>(this.CreateFormatCondition), y => this.CreateCustom(y)(editSettings));

        private MultiFilterModelItem CreateFormatCondition(FormatConditionFilterEditorOperatorMenuItemIdentity id) => 
            new MultiFilterModelItem(id.Name, OperandRestoreAdapterFactories.Default(client => ConstantFilterModel.CreateFilter(client, FormatConditionFiltersHelper.CreateFilter(client.PropertyName, id.Info, id.ApplyToRow, TopBottomFilterKind.Conditional))), null, id.Source, base.selectTemplate);

        private MultiFilterModelItem CreatePredefined(PredefinedFilterEditorOperatorMenuItemIdentity id)
        {
            string predefinedName = id.Name;
            return new MultiFilterModelItem(predefinedName, OperandRestoreAdapterFactories.Default(client => ConstantFilterModel.CreateFilter(client, PredefinedFiltersHelper.MakePredefinedFilterFunction(predefinedName, client.PropertyName))), null, null, base.selectTemplate);
        }

        protected override void RegisterBuiltInOperators(FilterModelValueItemInfo info, Dictionary<IFilterEditorOperatorMenuItemIdentity, Func<BaseEditSettings[], MultiFilterModelItem>> builtInOperators)
        {
            base.RegisterBuiltInOperators(info, builtInOperators);
            builtInOperators.Add(base.identityFactory.CreateAnyOf(), base.CreateBuiltIn(EditorStringId.FilterClauseAnyOf, base.VariadicFactory(info, CriteriaConverterFactory.CreateIsAnyOf()), "AnyOf"));
            builtInOperators.Add(base.identityFactory.CreateNoneOf(), base.CreateBuiltIn(EditorStringId.FilterClauseNoneOf, base.VariadicFactory(info, CriteriaConverterFactory.CreateNoneOf()), "NoneOf"));
            builtInOperators.Add(base.identityFactory.CreateIsOnDates(), base.CreateBuiltIn(EditorStringId.FilterClauseIsOnAnyOfTheFollowing, base.VariadicFactory(info, CriteriaConverterFactory.CreateIsOnDates()), "AnyOf"));
            builtInOperators.Add(base.identityFactory.CreateFunction(FunctionOperatorType.IsOutlookIntervalBeyondThisYear), base.CreateBuiltIn(EditorStringId.FilterClauseIsBeyondThisYear, UnaryFunctionFactory(FunctionOperatorType.IsOutlookIntervalBeyondThisYear), "IsBeyondThisYear"));
            builtInOperators.Add(base.identityFactory.CreateFunction(FunctionOperatorType.IsOutlookIntervalLaterThisYear), base.CreateBuiltIn(EditorStringId.FilterClauseIsLaterThisYear, UnaryFunctionFactory(FunctionOperatorType.IsOutlookIntervalLaterThisYear), "IsLaterThisYear"));
            builtInOperators.Add(base.identityFactory.CreateFunction(FunctionOperatorType.IsOutlookIntervalLaterThisMonth), base.CreateBuiltIn(EditorStringId.FilterClauseIsLaterThisMonth, UnaryFunctionFactory(FunctionOperatorType.IsOutlookIntervalLaterThisMonth), "IsLaterThisMonth"));
            builtInOperators.Add(base.identityFactory.CreateFunction(FunctionOperatorType.IsOutlookIntervalNextWeek), base.CreateBuiltIn(EditorStringId.FilterClauseIsNextWeek, UnaryFunctionFactory(FunctionOperatorType.IsOutlookIntervalNextWeek), "IsNextWeek"));
            builtInOperators.Add(base.identityFactory.CreateFunction(FunctionOperatorType.IsOutlookIntervalLaterThisWeek), base.CreateBuiltIn(EditorStringId.FilterClauseIsLaterThisWeek, UnaryFunctionFactory(FunctionOperatorType.IsOutlookIntervalLaterThisWeek), "IsLaterThisWeek"));
            builtInOperators.Add(base.identityFactory.CreateFunction(FunctionOperatorType.IsOutlookIntervalTomorrow), base.CreateBuiltIn(EditorStringId.FilterClauseIsTomorrow, UnaryFunctionFactory(FunctionOperatorType.IsOutlookIntervalTomorrow), "IsTomorrow"));
            builtInOperators.Add(base.identityFactory.CreateFunction(FunctionOperatorType.IsOutlookIntervalToday), base.CreateBuiltIn(EditorStringId.FilterClauseIsToday, UnaryFunctionFactory(FunctionOperatorType.IsOutlookIntervalToday), "IsToday"));
            builtInOperators.Add(base.identityFactory.CreateFunction(FunctionOperatorType.IsOutlookIntervalYesterday), base.CreateBuiltIn(EditorStringId.FilterClauseIsYesterday, UnaryFunctionFactory(FunctionOperatorType.IsOutlookIntervalYesterday), "IsYesterday"));
            builtInOperators.Add(base.identityFactory.CreateFunction(FunctionOperatorType.IsOutlookIntervalEarlierThisWeek), base.CreateBuiltIn(EditorStringId.FilterClauseIsEarlierThisWeek, UnaryFunctionFactory(FunctionOperatorType.IsOutlookIntervalEarlierThisWeek), "IsEarlierThisWeek"));
            builtInOperators.Add(base.identityFactory.CreateFunction(FunctionOperatorType.IsOutlookIntervalLastWeek), base.CreateBuiltIn(EditorStringId.FilterClauseIsLastWeek, UnaryFunctionFactory(FunctionOperatorType.IsOutlookIntervalLastWeek), "IsLastWeek"));
            builtInOperators.Add(base.identityFactory.CreateFunction(FunctionOperatorType.IsOutlookIntervalEarlierThisMonth), base.CreateBuiltIn(EditorStringId.FilterClauseIsEarlierThisMonth, UnaryFunctionFactory(FunctionOperatorType.IsOutlookIntervalEarlierThisMonth), "IsEarlierThisMonth"));
            builtInOperators.Add(base.identityFactory.CreateFunction(FunctionOperatorType.IsOutlookIntervalEarlierThisYear), base.CreateBuiltIn(EditorStringId.FilterClauseIsEarlierThisYear, UnaryFunctionFactory(FunctionOperatorType.IsOutlookIntervalEarlierThisYear), "IsEarlierThisYear"));
            builtInOperators.Add(base.identityFactory.CreateFunction(FunctionOperatorType.IsOutlookIntervalPriorThisYear), base.CreateBuiltIn(EditorStringId.FilterClauseIsPriorThisYear, UnaryFunctionFactory(FunctionOperatorType.IsOutlookIntervalPriorThisYear), "IsPriorThisYear"));
        }
    }
}

