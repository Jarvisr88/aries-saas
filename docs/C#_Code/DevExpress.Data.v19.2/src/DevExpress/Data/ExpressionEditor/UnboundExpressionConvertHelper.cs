namespace DevExpress.Data.ExpressionEditor
{
    using DevExpress.Data;
    using DevExpress.Data.Filtering;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public static class UnboundExpressionConvertHelper
    {
        private static CriteriaOperator ConvertCriteria(IDataColumnInfo columnInfo, string expression, bool fromEditor);
        private static string ConvertString(IDataColumnInfo columnInfo, string expression, bool fromEditor);
        public static string ConvertToCaption(IDataColumnInfo columnInfo, string expression);
        public static string ConvertToFields(IDataColumnInfo columnInfo, string expression);
        public static void ValidateExpressionFields(IDataColumnInfo columnInfo, string expression);

        private class DataColumnInfoAdapter : IBoundProperty
        {
            private IDataColumnInfo info;

            public DataColumnInfoAdapter(IDataColumnInfo info);
            private List<IBoundProperty> GetChildren(IBoundProperty parent);

            public string Name { get; }

            public string DisplayName { get; }

            public System.Type Type { get; }

            public bool HasChildren { get; }

            public List<IBoundProperty> ChildrenWithoutParent { get; }

            public List<IBoundProperty> Children { get; }

            public bool IsAggregate { get; }

            public bool IsList { get; }

            public IBoundProperty Parent { get; private set; }

            [Serializable, CompilerGenerated]
            private sealed class <>c
            {
                public static readonly UnboundExpressionConvertHelper.DataColumnInfoAdapter.<>c <>9;
                public static Func<IDataColumnInfo, bool> <>9__10_0;

                static <>c();
                internal bool <GetChildren>b__10_0(IDataColumnInfo i);
            }
        }
    }
}

