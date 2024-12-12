namespace DMEWorks.Forms
{
    using System;

    [Serializable, CLSCompliant(true)]
    public class FilteredGridAppearance : GridAppearanceBase
    {
        protected internal FilteredGridAppearance(FilteredGrid grid) : base(grid)
        {
        }
    }
}

