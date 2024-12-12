namespace DevExpress.Utils.Filtering.Internal
{
    using DevExpress.Utils.Filtering;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class FilterPropertyAttribute : FilterAttribute
    {
        public FilterPropertyAttribute() : this(true)
        {
        }

        public FilterPropertyAttribute(bool isFilterProperty = true)
        {
            this.IsFilterProperty = isFilterProperty;
        }

        public bool IsFilterProperty { get; private set; }
    }
}

