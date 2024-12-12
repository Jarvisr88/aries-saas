namespace DevExpress.XtraReports.Parameters
{
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;

    internal class LookUpValueValueComparer : IEqualityComparer<LookUpValue>
    {
        public bool Equals(LookUpValue x, LookUpValue y) => 
            ((x == null) || (y == null)) ? ((x == null) && ReferenceEquals(y, null)) : (ReferenceEquals(x, y) || ((x.Description == y.Description) && Equals(x.Value, y.Value)));

        public int GetHashCode(LookUpValue obj) => 
            HashCodeHelper.CalculateGeneric<object, string>(obj.Value, obj.Description);
    }
}

