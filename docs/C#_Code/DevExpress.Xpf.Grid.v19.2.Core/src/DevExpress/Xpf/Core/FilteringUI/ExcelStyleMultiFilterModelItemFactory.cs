namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Settings;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    internal sealed class ExcelStyleMultiFilterModelItemFactory : MultiFilterModelItemFactoryBase<IExcelStyleOperatorMenuItemIdentity, ExcelStyleOperatorMenuItemIdentityFactory>
    {
        public ExcelStyleMultiFilterModelItemFactory(OperandListObserver<FilterModelBase> operandListObserver, Func<FilterModelBase, DataTemplate> selectTemplate, ExcelStyleOperatorMenuItemIdentityFactory identityFactory) : base(null, operandListObserver, selectTemplate, identityFactory)
        {
        }

        public override MultiFilterModelItem Create(IExcelStyleOperatorMenuItemIdentity id, BaseEditSettings[] editSettings = null) => 
            id.Match<MultiFilterModelItem>(x => this.CreateBuiltIn(x)(editSettings), z => this.CreateCustom(z)(editSettings));

        protected override void RegisterBuiltInOperators(FilterModelValueItemInfo info, Dictionary<IExcelStyleOperatorMenuItemIdentity, Func<BaseEditSettings[], MultiFilterModelItem>> builtInOperators)
        {
            base.RegisterBuiltInOperators(info, builtInOperators);
            Func<BaseEditSettings[], OperandRestoreAdapterFactory> factory = <>c.<>9__2_0;
            if (<>c.<>9__2_0 == null)
            {
                Func<BaseEditSettings[], OperandRestoreAdapterFactory> local1 = <>c.<>9__2_0;
                factory = <>c.<>9__2_0 = _ => OperandRestoreAdapterFactories.FormatConditions();
            }
            builtInOperators.Add(base.identityFactory.CreateFormatConditions(), this.CreateBuiltIn(EditorStringId.FormatConditionFilters, factory, null));
            Func<BaseEditSettings[], OperandRestoreAdapterFactory> func2 = <>c.<>9__2_1;
            if (<>c.<>9__2_1 == null)
            {
                Func<BaseEditSettings[], OperandRestoreAdapterFactory> local2 = <>c.<>9__2_1;
                func2 = <>c.<>9__2_1 = _ => OperandRestoreAdapterFactories.Predefined();
            }
            builtInOperators.Add(base.identityFactory.CreatePredefined(), this.CreateBuiltIn(EditorStringId.PredefinedFilters, func2, null));
            Func<BaseEditSettings[], OperandRestoreAdapterFactory> func3 = <>c.<>9__2_2;
            if (<>c.<>9__2_2 == null)
            {
                Func<BaseEditSettings[], OperandRestoreAdapterFactory> local3 = <>c.<>9__2_2;
                func3 = <>c.<>9__2_2 = _ => OperandRestoreAdapterFactories.DateTimeOperators();
            }
            builtInOperators.Add(base.identityFactory.CreateDateOperators(), this.CreateBuiltIn(EditorStringId.DateIntervalsMenuCaption, func3, null));
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ExcelStyleMultiFilterModelItemFactory.<>c <>9 = new ExcelStyleMultiFilterModelItemFactory.<>c();
            public static Func<BaseEditSettings[], OperandRestoreAdapterFactory> <>9__2_0;
            public static Func<BaseEditSettings[], OperandRestoreAdapterFactory> <>9__2_1;
            public static Func<BaseEditSettings[], OperandRestoreAdapterFactory> <>9__2_2;

            internal OperandRestoreAdapterFactory <RegisterBuiltInOperators>b__2_0(BaseEditSettings[] _) => 
                OperandRestoreAdapterFactories.FormatConditions();

            internal OperandRestoreAdapterFactory <RegisterBuiltInOperators>b__2_1(BaseEditSettings[] _) => 
                OperandRestoreAdapterFactories.Predefined();

            internal OperandRestoreAdapterFactory <RegisterBuiltInOperators>b__2_2(BaseEditSettings[] _) => 
                OperandRestoreAdapterFactories.DateTimeOperators();
        }
    }
}

