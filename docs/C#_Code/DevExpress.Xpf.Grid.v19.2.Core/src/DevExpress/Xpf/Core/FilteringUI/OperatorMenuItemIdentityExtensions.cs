namespace DevExpress.Xpf.Core.FilteringUI
{
    using System;
    using System.Runtime.CompilerServices;

    internal static class OperatorMenuItemIdentityExtensions
    {
        public static T Match<T>(this IExcelStyleOperatorMenuItemIdentity op, Func<BuiltInExcelStyleOperatorMenuItemIdentity, T> builtIn, Func<CustomOperatorMenuItemIdentity, T> custom)
        {
            if (op is BuiltInExcelStyleOperatorMenuItemIdentity)
            {
                return builtIn((BuiltInExcelStyleOperatorMenuItemIdentity) op);
            }
            if (!(op is CustomOperatorMenuItemIdentity))
            {
                throw new InvalidOperationException();
            }
            return custom((CustomOperatorMenuItemIdentity) op);
        }

        public static T Match<T>(this IFilterEditorOperatorMenuItemIdentity op, Func<BuiltInFilterEditorOperatorMenuItemIdentity, T> builtIn, Func<PredefinedFilterEditorOperatorMenuItemIdentity, T> predefined, Func<FormatConditionFilterEditorOperatorMenuItemIdentity, T> formatCondition, Func<CustomOperatorMenuItemIdentity, T> custom)
        {
            switch (op)
            {
                case (BuiltInFilterEditorOperatorMenuItemIdentity _):
                    return builtIn((BuiltInFilterEditorOperatorMenuItemIdentity) op);
                    break;
            }
            if (op is PredefinedFilterEditorOperatorMenuItemIdentity)
            {
                return predefined((PredefinedFilterEditorOperatorMenuItemIdentity) op);
            }
            if (op is FormatConditionFilterEditorOperatorMenuItemIdentity)
            {
                return formatCondition((FormatConditionFilterEditorOperatorMenuItemIdentity) op);
            }
            if (!(op is CustomOperatorMenuItemIdentity))
            {
                throw new InvalidOperationException();
            }
            return custom((CustomOperatorMenuItemIdentity) op);
        }
    }
}

