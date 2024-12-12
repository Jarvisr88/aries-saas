namespace DMEWorks.Forms
{
    using System;
    using System.ComponentModel;

    [Serializable, CLSCompliant(true)]
    public class SearchableGridAppearance : GridAppearanceBase
    {
        protected internal SearchableGridAppearance(SearchableGrid grid) : base(grid)
        {
        }

        [Category("Appearance"), DefaultValue(true)]
        public bool FilterVisible
        {
            get => 
                ((SearchableGrid) base.searchableGrid).FilterVisible;
            set => 
                ((SearchableGrid) base.searchableGrid).FilterVisible = value;
        }

        [Category("Appearance"), DefaultValue(true)]
        public bool ManageColumnsVisible
        {
            get => 
                ((SearchableGrid) base.searchableGrid).ManageColumnsVisible;
            set => 
                ((SearchableGrid) base.searchableGrid).ManageColumnsVisible = value;
        }
    }
}

