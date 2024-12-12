namespace DevExpress.Office.Utils
{
    using System;
    using System.Collections.Generic;

    public class OfficeDrawingPropertiesComparer : IComparer<IOfficeDrawingProperty>
    {
        public int Compare(IOfficeDrawingProperty x, IOfficeDrawingProperty y) => 
            x.Id.CompareTo(y.Id);
    }
}

