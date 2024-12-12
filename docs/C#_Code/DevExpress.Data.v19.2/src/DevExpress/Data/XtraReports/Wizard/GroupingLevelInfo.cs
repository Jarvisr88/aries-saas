namespace DevExpress.Data.XtraReports.Wizard
{
    using DevExpress.Data.XtraReports.DataProviders;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    [EditorBrowsable(EditorBrowsableState.Never), Obsolete("Use the DevExpress.XtraReports.Wizards.GroupingLevelInfo class from the DevExpress.XtraReports assembly instead.")]
    public class GroupingLevelInfo
    {
        private string displayName;

        public GroupingLevelInfo(ColumnInfo[] columns);
        public bool ContainsColumn(string columnName);
        public override bool Equals(object obj);
        public override int GetHashCode();

        public string DisplayName { get; }

        public ColumnInfo[] Columns { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly GroupingLevelInfo.<>c <>9;
            public static Func<ColumnInfo, string> <>9__2_0;

            static <>c();
            internal string <get_DisplayName>b__2_0(ColumnInfo c);
        }
    }
}

