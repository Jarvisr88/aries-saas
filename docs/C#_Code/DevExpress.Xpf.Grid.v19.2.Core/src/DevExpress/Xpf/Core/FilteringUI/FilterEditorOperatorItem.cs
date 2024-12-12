namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Utils;
    using DevExpress.Xpf.Editors.Settings;
    using DevExpress.Xpf.Grid;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class FilterEditorOperatorItem : OperatorItemBase
    {
        public FilterEditorOperatorItem(FilterEditorOperatorType type, BaseEditSettings[] editSettings = null) : this(new BuiltInFilterEditorOperatorMenuItemIdentity(type), editSettings)
        {
        }

        internal FilterEditorOperatorItem(IFilterEditorOperatorMenuItemIdentity id, BaseEditSettings[] editSettings) : base(editSettings)
        {
            Guard.ArgumentNotNull(id, "id");
            this.<ID>k__BackingField = id;
        }

        public FilterEditorOperatorItem(string customFunctionName, BaseEditSettings[] editSettings = null) : this(new CustomOperatorMenuItemIdentity(customFunctionName), editSettings)
        {
        }

        public FormatConditionBase FormatCondition
        {
            get
            {
                Func<BuiltInFilterEditorOperatorMenuItemIdentity, FormatConditionBase> builtIn = <>c.<>9__1_0;
                if (<>c.<>9__1_0 == null)
                {
                    Func<BuiltInFilterEditorOperatorMenuItemIdentity, FormatConditionBase> local1 = <>c.<>9__1_0;
                    builtIn = <>c.<>9__1_0 = (Func<BuiltInFilterEditorOperatorMenuItemIdentity, FormatConditionBase>) (_ => null);
                }
                return this.ID.Match<FormatConditionBase>(builtIn, (<>c.<>9__1_1 ??= ((Func<PredefinedFilterEditorOperatorMenuItemIdentity, FormatConditionBase>) (_ => null))), (<>c.<>9__1_2 ??= x => x.Source), (<>c.<>9__1_3 ??= ((Func<CustomOperatorMenuItemIdentity, FormatConditionBase>) (_ => null))));
            }
        }

        public string PredefinedFilterName
        {
            get
            {
                Func<BuiltInFilterEditorOperatorMenuItemIdentity, string> builtIn = <>c.<>9__3_0;
                if (<>c.<>9__3_0 == null)
                {
                    Func<BuiltInFilterEditorOperatorMenuItemIdentity, string> local1 = <>c.<>9__3_0;
                    builtIn = <>c.<>9__3_0 = _ => string.Empty;
                }
                return this.ID.Match<string>(builtIn, (<>c.<>9__3_1 ??= x => x.Name), (<>c.<>9__3_2 ??= _ => string.Empty), (<>c.<>9__3_3 ??= _ => string.Empty));
            }
        }

        public override string CustomFunctionName
        {
            get
            {
                Func<BuiltInFilterEditorOperatorMenuItemIdentity, string> builtIn = <>c.<>9__5_0;
                if (<>c.<>9__5_0 == null)
                {
                    Func<BuiltInFilterEditorOperatorMenuItemIdentity, string> local1 = <>c.<>9__5_0;
                    builtIn = <>c.<>9__5_0 = _ => string.Empty;
                }
                return this.ID.Match<string>(builtIn, (<>c.<>9__5_1 ??= _ => string.Empty), (<>c.<>9__5_2 ??= _ => string.Empty), (<>c.<>9__5_3 ??= x => x.Name));
            }
        }

        public string GroupName { get; set; }

        public FilterEditorOperatorType? OperatorType
        {
            get
            {
                Func<BuiltInFilterEditorOperatorMenuItemIdentity, FilterEditorOperatorType?> builtIn = <>c.<>9__11_0;
                if (<>c.<>9__11_0 == null)
                {
                    Func<BuiltInFilterEditorOperatorMenuItemIdentity, FilterEditorOperatorType?> local1 = <>c.<>9__11_0;
                    builtIn = <>c.<>9__11_0 = x => new FilterEditorOperatorType?(x.Type);
                }
                return this.ID.Match<FilterEditorOperatorType?>(builtIn, (<>c.<>9__11_1 ??= ((Func<PredefinedFilterEditorOperatorMenuItemIdentity, FilterEditorOperatorType?>) (_ => null))), (<>c.<>9__11_2 ??= ((Func<FormatConditionFilterEditorOperatorMenuItemIdentity, FilterEditorOperatorType?>) (_ => null))), (<>c.<>9__11_3 ??= ((Func<CustomOperatorMenuItemIdentity, FilterEditorOperatorType?>) (_ => null))));
            }
        }

        internal IFilterEditorOperatorMenuItemIdentity ID { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly FilterEditorOperatorItem.<>c <>9 = new FilterEditorOperatorItem.<>c();
            public static Func<BuiltInFilterEditorOperatorMenuItemIdentity, FormatConditionBase> <>9__1_0;
            public static Func<PredefinedFilterEditorOperatorMenuItemIdentity, FormatConditionBase> <>9__1_1;
            public static Func<FormatConditionFilterEditorOperatorMenuItemIdentity, FormatConditionBase> <>9__1_2;
            public static Func<CustomOperatorMenuItemIdentity, FormatConditionBase> <>9__1_3;
            public static Func<BuiltInFilterEditorOperatorMenuItemIdentity, string> <>9__3_0;
            public static Func<PredefinedFilterEditorOperatorMenuItemIdentity, string> <>9__3_1;
            public static Func<FormatConditionFilterEditorOperatorMenuItemIdentity, string> <>9__3_2;
            public static Func<CustomOperatorMenuItemIdentity, string> <>9__3_3;
            public static Func<BuiltInFilterEditorOperatorMenuItemIdentity, string> <>9__5_0;
            public static Func<PredefinedFilterEditorOperatorMenuItemIdentity, string> <>9__5_1;
            public static Func<FormatConditionFilterEditorOperatorMenuItemIdentity, string> <>9__5_2;
            public static Func<CustomOperatorMenuItemIdentity, string> <>9__5_3;
            public static Func<BuiltInFilterEditorOperatorMenuItemIdentity, FilterEditorOperatorType?> <>9__11_0;
            public static Func<PredefinedFilterEditorOperatorMenuItemIdentity, FilterEditorOperatorType?> <>9__11_1;
            public static Func<FormatConditionFilterEditorOperatorMenuItemIdentity, FilterEditorOperatorType?> <>9__11_2;
            public static Func<CustomOperatorMenuItemIdentity, FilterEditorOperatorType?> <>9__11_3;

            internal string <get_CustomFunctionName>b__5_0(BuiltInFilterEditorOperatorMenuItemIdentity _) => 
                string.Empty;

            internal string <get_CustomFunctionName>b__5_1(PredefinedFilterEditorOperatorMenuItemIdentity _) => 
                string.Empty;

            internal string <get_CustomFunctionName>b__5_2(FormatConditionFilterEditorOperatorMenuItemIdentity _) => 
                string.Empty;

            internal string <get_CustomFunctionName>b__5_3(CustomOperatorMenuItemIdentity x) => 
                x.Name;

            internal FormatConditionBase <get_FormatCondition>b__1_0(BuiltInFilterEditorOperatorMenuItemIdentity _) => 
                null;

            internal FormatConditionBase <get_FormatCondition>b__1_1(PredefinedFilterEditorOperatorMenuItemIdentity _) => 
                null;

            internal FormatConditionBase <get_FormatCondition>b__1_2(FormatConditionFilterEditorOperatorMenuItemIdentity x) => 
                x.Source;

            internal FormatConditionBase <get_FormatCondition>b__1_3(CustomOperatorMenuItemIdentity _) => 
                null;

            internal FilterEditorOperatorType? <get_OperatorType>b__11_0(BuiltInFilterEditorOperatorMenuItemIdentity x) => 
                new FilterEditorOperatorType?(x.Type);

            internal FilterEditorOperatorType? <get_OperatorType>b__11_1(PredefinedFilterEditorOperatorMenuItemIdentity _) => 
                null;

            internal FilterEditorOperatorType? <get_OperatorType>b__11_2(FormatConditionFilterEditorOperatorMenuItemIdentity _) => 
                null;

            internal FilterEditorOperatorType? <get_OperatorType>b__11_3(CustomOperatorMenuItemIdentity _) => 
                null;

            internal string <get_PredefinedFilterName>b__3_0(BuiltInFilterEditorOperatorMenuItemIdentity _) => 
                string.Empty;

            internal string <get_PredefinedFilterName>b__3_1(PredefinedFilterEditorOperatorMenuItemIdentity x) => 
                x.Name;

            internal string <get_PredefinedFilterName>b__3_2(FormatConditionFilterEditorOperatorMenuItemIdentity _) => 
                string.Empty;

            internal string <get_PredefinedFilterName>b__3_3(CustomOperatorMenuItemIdentity _) => 
                string.Empty;
        }
    }
}

