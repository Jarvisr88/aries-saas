namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Xpf.Editors.Settings;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    internal static class MultiFilterModelFactory
    {
        private static OperatorMenuItemFactory<MultiFilterModelItem, TID> CreateFactory<TID, TFactory, TOP>(MultiFilterModelItemFactoryBase<TID, TFactory> itemFactory, Func<OperatorMenuItemsSubstitutionInfo<TOP>, OperatorMenuItemsSubstitutionInfo<TOP>> substituteOperatorMenuItems, Func<Func<TID, BaseEditSettings[], MultiFilterModelItem>, OperatorItemSubstitutionInfoProvider<TID, TOP>> provider) where TID: class where TFactory: OperatorMenuItemIdentityFactoryBase<TID> where TOP: class
        {
            OperatorMenuItemFactory<MultiFilterModelItem, TID> component = new OperatorMenuItemFactoryImplementation<MultiFilterModelItem, TID>(id => itemFactory.Create(id, null)).Factory;
            return ((substituteOperatorMenuItems == null) ? component : new MultiFilterOperatorMenuSubstitutionDecorator<TID, TOP>(component, substituteOperatorMenuItems, provider((id, es) => itemFactory.Create(id, es))).Factory);
        }

        public static MultiFilterModel CreateFilterEditorModel(FilterModelClient client, FilterModelValueItemInfo info, OperandListObserver<FilterModelBase> observer, Func<OperatorMenuItemsSubstitutionInfo<FilterEditorOperatorItem>, OperatorMenuItemsSubstitutionInfo<FilterEditorOperatorItem>> substituteOperatorMenuItems, Func<FilterModelBase, DataTemplate> selectTemplate)
        {
            FilterEditorOperatorMenuItemIdentityFactory identityFactory = new FilterEditorOperatorMenuItemIdentityFactory();
            Func<Func<IFilterEditorOperatorMenuItemIdentity, BaseEditSettings[], MultiFilterModelItem>, OperatorItemSubstitutionInfoProvider<IFilterEditorOperatorMenuItemIdentity, FilterEditorOperatorItem>> provider = <>c.<>9__0_0;
            if (<>c.<>9__0_0 == null)
            {
                Func<Func<IFilterEditorOperatorMenuItemIdentity, BaseEditSettings[], MultiFilterModelItem>, OperatorItemSubstitutionInfoProvider<IFilterEditorOperatorMenuItemIdentity, FilterEditorOperatorItem>> local1 = <>c.<>9__0_0;
                provider = <>c.<>9__0_0 = createItem => OperatorItemSubstitutionInfoProviders.FilterEditor(createItem);
            }
            return new MultiFilterModel(new FilterEditorOperatorMenuBuilder<MultiFilterModelItem>(CreateFactory<IFilterEditorOperatorMenuItemIdentity, FilterEditorOperatorMenuItemIdentityFactory, FilterEditorOperatorItem>(new FilterEditorMultiFilterModelItemFactory(info, observer, selectTemplate, identityFactory), substituteOperatorMenuItems, provider), GetInfo(client), identityFactory).Selector, client, true);
        }

        public static MultiFilterModel CreateMultiElementModel(FilterModelClient client, Func<OperatorMenuItemsSubstitutionInfo<ExcelStyleFilterElementOperatorItem>, OperatorMenuItemsSubstitutionInfo<ExcelStyleFilterElementOperatorItem>> substituteOperatorMenuItems, Func<FilterModelBase, DataTemplate> selectTemplate = null)
        {
            ExcelStyleOperatorMenuItemIdentityFactory identityFactory = new ExcelStyleOperatorMenuItemIdentityFactory();
            Func<Func<IExcelStyleOperatorMenuItemIdentity, BaseEditSettings[], MultiFilterModelItem>, OperatorItemSubstitutionInfoProvider<IExcelStyleOperatorMenuItemIdentity, ExcelStyleFilterElementOperatorItem>> provider = <>c.<>9__1_0;
            if (<>c.<>9__1_0 == null)
            {
                Func<Func<IExcelStyleOperatorMenuItemIdentity, BaseEditSettings[], MultiFilterModelItem>, OperatorItemSubstitutionInfoProvider<IExcelStyleOperatorMenuItemIdentity, ExcelStyleFilterElementOperatorItem>> local1 = <>c.<>9__1_0;
                provider = <>c.<>9__1_0 = createItem => OperatorItemSubstitutionInfoProviders.ExcelStyle(createItem);
            }
            return new MultiFilterModel(new MultiElementOperatorMenuBuilder<MultiFilterModelItem>(CreateFactory<IExcelStyleOperatorMenuItemIdentity, ExcelStyleOperatorMenuItemIdentityFactory, ExcelStyleFilterElementOperatorItem>(new ExcelStyleMultiFilterModelItemFactory(null, selectTemplate, identityFactory), substituteOperatorMenuItems, provider), GetInfo(client), identityFactory).Selector, client, false);
        }

        private static FilterSelectorBuildInfo GetInfo(FilterModelClient client) => 
            ColumnConfigurationInfoCalculator.Calculate(client);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly MultiFilterModelFactory.<>c <>9 = new MultiFilterModelFactory.<>c();
            public static Func<Func<IFilterEditorOperatorMenuItemIdentity, BaseEditSettings[], MultiFilterModelItem>, OperatorItemSubstitutionInfoProvider<IFilterEditorOperatorMenuItemIdentity, FilterEditorOperatorItem>> <>9__0_0;
            public static Func<Func<IExcelStyleOperatorMenuItemIdentity, BaseEditSettings[], MultiFilterModelItem>, OperatorItemSubstitutionInfoProvider<IExcelStyleOperatorMenuItemIdentity, ExcelStyleFilterElementOperatorItem>> <>9__1_0;

            internal OperatorItemSubstitutionInfoProvider<IFilterEditorOperatorMenuItemIdentity, FilterEditorOperatorItem> <CreateFilterEditorModel>b__0_0(Func<IFilterEditorOperatorMenuItemIdentity, BaseEditSettings[], MultiFilterModelItem> createItem) => 
                OperatorItemSubstitutionInfoProviders.FilterEditor(createItem);

            internal OperatorItemSubstitutionInfoProvider<IExcelStyleOperatorMenuItemIdentity, ExcelStyleFilterElementOperatorItem> <CreateMultiElementModel>b__1_0(Func<IExcelStyleOperatorMenuItemIdentity, BaseEditSettings[], MultiFilterModelItem> createItem) => 
                OperatorItemSubstitutionInfoProviders.ExcelStyle(createItem);
        }
    }
}

