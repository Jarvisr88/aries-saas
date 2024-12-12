namespace DevExpress.Utils.Filtering
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public sealed class FilterGroupAttribute : BaseFilterLookupAttribute
    {
        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public static readonly FilterGroupAttribute Implicit = new FilterGroupAttribute();

        private FilterGroupAttribute()
        {
        }

        public FilterGroupAttribute(string groupingOrChildren)
        {
            if (string.IsNullOrEmpty(groupingOrChildren))
            {
                throw new ArgumentException("groupingOrChildren");
            }
            char[] separator = new char[] { ',', ';', ' ', '\t' };
            this.Grouping = groupingOrChildren.Split(separator, StringSplitOptions.RemoveEmptyEntries);
        }

        [Browsable(false)]
        internal static string[] Ensure(string[] grouping, string path)
        {
            if (Array.IndexOf<string>(grouping, path) != -1)
            {
                return grouping;
            }
            string[] array = new string[] { path };
            grouping.CopyTo(array, 1);
            return array;
        }

        [Browsable(false)]
        internal bool Equals(FilterGroupAttribute attribute) => 
            !ReferenceEquals(attribute, this) ? ((attribute != null) ? this.Grouping.Equals(attribute.Grouping, StringComparer.Ordinal) : false) : true;

        private bool IsRootedBy(string origin) => 
            string.Equals(this.Grouping[0], origin, StringComparison.Ordinal);

        internal bool IsValid(string origin) => 
            (this.Grouping.Length != 0) ? ((this.Grouping.Length != 1) || !this.IsRootedBy(origin)) : false;

        public bool IsImplicit =>
            ReferenceEquals(this, Implicit);

        public GroupUIEditorType EditorType { get; set; }

        [Browsable(false)]
        public string[] Grouping { get; private set; }
    }
}

