namespace DevExpress.Xpf.Core.FilteringUI
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    internal class CheckedValuesInfo
    {
        public readonly List<object> CheckedValues;
        public readonly Func<List<object>> GetUncheckedValues;
        public readonly List<IndeterminateValueInfo> IndeterminateValues;

        public CheckedValuesInfo(List<object> checkedValues, Func<List<object>> getUncheckedValues, List<IndeterminateValueInfo> indeterminateValues = null)
        {
            this.CheckedValues = checkedValues;
            this.GetUncheckedValues = getUncheckedValues;
            List<IndeterminateValueInfo> list1 = indeterminateValues;
            if (indeterminateValues == null)
            {
                List<IndeterminateValueInfo> local1 = indeterminateValues;
                list1 = new List<IndeterminateValueInfo>();
            }
            this.IndeterminateValues = list1;
        }
    }
}

