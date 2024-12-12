namespace DevExpress.Data.Controls.ExpressionEditor
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class ColumnInfo : ItemInfoBase
    {
        private readonly List<ColumnInfo> children;
        internal const string DefaultCategoryName = "Columns";
        private readonly ColumnInfo other;

        public ColumnInfo();
        public ColumnInfo(ColumnInfo other);
        public ColumnInfo(string category);
        internal string GetFullName();

        public System.Type Type { get; set; }

        public ColumnInfo Parent { get; set; }

        public virtual List<ColumnInfo> Children { get; }
    }
}

