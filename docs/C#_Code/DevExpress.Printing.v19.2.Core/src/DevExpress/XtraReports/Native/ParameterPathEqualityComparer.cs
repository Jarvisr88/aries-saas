namespace DevExpress.XtraReports.Native
{
    using System;
    using System.Collections.Generic;

    public class ParameterPathEqualityComparer : IEqualityComparer<ParameterPath>
    {
        private static readonly ParameterPathEqualityComparer instance = new ParameterPathEqualityComparer();

        public bool Equals(ParameterPath x, ParameterPath y) => 
            string.Equals(x.Path, y.Path);

        public int GetHashCode(ParameterPath obj) => 
            obj.Path.GetHashCode();

        public static ParameterPathEqualityComparer Instance =>
            instance;
    }
}

