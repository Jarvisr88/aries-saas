namespace DevExpress.Xpf.Core.FilteringUI
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Media;

    internal static class OperatorItemSubstitutionInfoProviders
    {
        private static MultiFilterModelItem CreateMultiFilterModelItem<TID>(Func<TID, BaseEditSettings[], MultiFilterModelItem> createItem, OperatorItemBase operatorItem, TID id) where TID: class
        {
            MultiFilterModelItem item = createItem(id, operatorItem.EditSettings);
            Func<FilterModelBase, DataTemplate> selectTemplate = (operatorItem.OperandTemplate == null) ? item.SelectTemplate : _ => operatorItem.OperandTemplate;
            string caption = operatorItem.Caption;
            string displayName = caption;
            if (caption == null)
            {
                string local1 = caption;
                displayName = item.DisplayName;
            }
            ImageSource image = operatorItem.Image;
            ImageSource icon = image;
            if (image == null)
            {
                ImageSource local2 = image;
                icon = item.Icon;
            }
            return new MultiFilterModelItem(displayName, item.Factory, icon, item.FormatCondition, selectTemplate);
        }

        internal static OperatorItemSubstitutionInfoProvider<IExcelStyleOperatorMenuItemIdentity, ExcelStyleFilterElementOperatorItem> ExcelStyle(Func<IExcelStyleOperatorMenuItemIdentity, BaseEditSettings[], MultiFilterModelItem> createItem) => 
            new OperatorItemSubstitutionInfoProvider<IExcelStyleOperatorMenuItemIdentity, ExcelStyleFilterElementOperatorItem>(<>c.<>9__1_0 ??= delegate (IdentifiedOperatorMenuItem<IExcelStyleOperatorMenuItemIdentity, MultiFilterModelItem> x) {
                ExcelStyleFilterElementOperatorItem item1 = new ExcelStyleFilterElementOperatorItem(x.ID, null);
                item1.Caption = x.Value.DisplayName;
                item1.Image = x.Value.Icon;
                return item1;
            }, <>c.<>9__1_1 ??= (x, _) => x, <>c.<>9__1_2 ??= x => string.Empty, <>c.<>9__1_3 ??= x => x.ID, x => CreateMultiFilterModelItem<IExcelStyleOperatorMenuItemIdentity>(createItem, x, x.ID));

        internal static OperatorItemSubstitutionInfoProvider<IFilterEditorOperatorMenuItemIdentity, FilterEditorOperatorItem> FilterEditor(Func<IFilterEditorOperatorMenuItemIdentity, BaseEditSettings[], MultiFilterModelItem> createItem) => 
            new OperatorItemSubstitutionInfoProvider<IFilterEditorOperatorMenuItemIdentity, FilterEditorOperatorItem>(<>c.<>9__0_0 ??= delegate (IdentifiedOperatorMenuItem<IFilterEditorOperatorMenuItemIdentity, MultiFilterModelItem> x) {
                FilterEditorOperatorItem item1 = new FilterEditorOperatorItem(x.ID, null);
                item1.Caption = x.Value.DisplayName;
                item1.Image = x.Value.Icon;
                return item1;
            }, <>c.<>9__0_1 ??= delegate (FilterEditorOperatorItem x, string newGroupName) {
                x.GroupName = newGroupName;
                return x;
            }, <>c.<>9__0_2 ??= x => x.GroupName, <>c.<>9__0_3 ??= x => x.ID, x => CreateMultiFilterModelItem<IFilterEditorOperatorMenuItemIdentity>(createItem, x, x.ID));

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly OperatorItemSubstitutionInfoProviders.<>c <>9 = new OperatorItemSubstitutionInfoProviders.<>c();
            public static Func<IdentifiedOperatorMenuItem<IFilterEditorOperatorMenuItemIdentity, MultiFilterModelItem>, FilterEditorOperatorItem> <>9__0_0;
            public static Func<FilterEditorOperatorItem, string, FilterEditorOperatorItem> <>9__0_1;
            public static Func<FilterEditorOperatorItem, string> <>9__0_2;
            public static Func<FilterEditorOperatorItem, IFilterEditorOperatorMenuItemIdentity> <>9__0_3;
            public static Func<IdentifiedOperatorMenuItem<IExcelStyleOperatorMenuItemIdentity, MultiFilterModelItem>, ExcelStyleFilterElementOperatorItem> <>9__1_0;
            public static Func<ExcelStyleFilterElementOperatorItem, string, ExcelStyleFilterElementOperatorItem> <>9__1_1;
            public static Func<ExcelStyleFilterElementOperatorItem, string> <>9__1_2;
            public static Func<ExcelStyleFilterElementOperatorItem, IExcelStyleOperatorMenuItemIdentity> <>9__1_3;

            internal ExcelStyleFilterElementOperatorItem <ExcelStyle>b__1_0(IdentifiedOperatorMenuItem<IExcelStyleOperatorMenuItemIdentity, MultiFilterModelItem> x)
            {
                ExcelStyleFilterElementOperatorItem item1 = new ExcelStyleFilterElementOperatorItem(x.ID, null);
                item1.Caption = x.Value.DisplayName;
                item1.Image = x.Value.Icon;
                return item1;
            }

            internal ExcelStyleFilterElementOperatorItem <ExcelStyle>b__1_1(ExcelStyleFilterElementOperatorItem x, string _) => 
                x;

            internal string <ExcelStyle>b__1_2(ExcelStyleFilterElementOperatorItem x) => 
                string.Empty;

            internal IExcelStyleOperatorMenuItemIdentity <ExcelStyle>b__1_3(ExcelStyleFilterElementOperatorItem x) => 
                x.ID;

            internal FilterEditorOperatorItem <FilterEditor>b__0_0(IdentifiedOperatorMenuItem<IFilterEditorOperatorMenuItemIdentity, MultiFilterModelItem> x)
            {
                FilterEditorOperatorItem item1 = new FilterEditorOperatorItem(x.ID, null);
                item1.Caption = x.Value.DisplayName;
                item1.Image = x.Value.Icon;
                return item1;
            }

            internal FilterEditorOperatorItem <FilterEditor>b__0_1(FilterEditorOperatorItem x, string newGroupName)
            {
                x.GroupName = newGroupName;
                return x;
            }

            internal string <FilterEditor>b__0_2(FilterEditorOperatorItem x) => 
                x.GroupName;

            internal IFilterEditorOperatorMenuItemIdentity <FilterEditor>b__0_3(FilterEditorOperatorItem x) => 
                x.ID;
        }
    }
}

