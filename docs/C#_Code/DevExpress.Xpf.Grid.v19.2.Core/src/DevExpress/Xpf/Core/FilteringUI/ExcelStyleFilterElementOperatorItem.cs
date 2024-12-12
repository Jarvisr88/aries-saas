namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Utils;
    using DevExpress.Xpf.Editors.Settings;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class ExcelStyleFilterElementOperatorItem : OperatorItemBase
    {
        public ExcelStyleFilterElementOperatorItem(ExcelStyleFilterElementOperatorType type, BaseEditSettings[] editSettings = null) : this(new BuiltInExcelStyleOperatorMenuItemIdentity(type), editSettings)
        {
        }

        internal ExcelStyleFilterElementOperatorItem(IExcelStyleOperatorMenuItemIdentity id, BaseEditSettings[] editSettings) : base(editSettings)
        {
            Guard.ArgumentNotNull(id, "id");
            this.<ID>k__BackingField = id;
        }

        public ExcelStyleFilterElementOperatorItem(string customFunctionName, BaseEditSettings[] editSettings = null) : this(new CustomOperatorMenuItemIdentity(customFunctionName), editSettings)
        {
        }

        public override string CustomFunctionName
        {
            get
            {
                Func<BuiltInExcelStyleOperatorMenuItemIdentity, string> builtIn = <>c.<>9__1_0;
                if (<>c.<>9__1_0 == null)
                {
                    Func<BuiltInExcelStyleOperatorMenuItemIdentity, string> local1 = <>c.<>9__1_0;
                    builtIn = <>c.<>9__1_0 = _ => string.Empty;
                }
                return this.ID.Match<string>(builtIn, (<>c.<>9__1_1 ??= x => x.Name));
            }
        }

        public ExcelStyleFilterElementOperatorType? OperatorType
        {
            get
            {
                Func<BuiltInExcelStyleOperatorMenuItemIdentity, ExcelStyleFilterElementOperatorType?> builtIn = <>c.<>9__3_0;
                if (<>c.<>9__3_0 == null)
                {
                    Func<BuiltInExcelStyleOperatorMenuItemIdentity, ExcelStyleFilterElementOperatorType?> local1 = <>c.<>9__3_0;
                    builtIn = <>c.<>9__3_0 = x => new ExcelStyleFilterElementOperatorType?(x.Type);
                }
                return this.ID.Match<ExcelStyleFilterElementOperatorType?>(builtIn, (<>c.<>9__3_1 ??= ((Func<CustomOperatorMenuItemIdentity, ExcelStyleFilterElementOperatorType?>) (x => null))));
            }
        }

        internal IExcelStyleOperatorMenuItemIdentity ID { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ExcelStyleFilterElementOperatorItem.<>c <>9 = new ExcelStyleFilterElementOperatorItem.<>c();
            public static Func<BuiltInExcelStyleOperatorMenuItemIdentity, string> <>9__1_0;
            public static Func<CustomOperatorMenuItemIdentity, string> <>9__1_1;
            public static Func<BuiltInExcelStyleOperatorMenuItemIdentity, ExcelStyleFilterElementOperatorType?> <>9__3_0;
            public static Func<CustomOperatorMenuItemIdentity, ExcelStyleFilterElementOperatorType?> <>9__3_1;

            internal string <get_CustomFunctionName>b__1_0(BuiltInExcelStyleOperatorMenuItemIdentity _) => 
                string.Empty;

            internal string <get_CustomFunctionName>b__1_1(CustomOperatorMenuItemIdentity x) => 
                x.Name;

            internal ExcelStyleFilterElementOperatorType? <get_OperatorType>b__3_0(BuiltInExcelStyleOperatorMenuItemIdentity x) => 
                new ExcelStyleFilterElementOperatorType?(x.Type);

            internal ExcelStyleFilterElementOperatorType? <get_OperatorType>b__3_1(CustomOperatorMenuItemIdentity x) => 
                null;
        }
    }
}

