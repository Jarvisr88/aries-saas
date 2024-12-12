namespace DevExpress.Utils.Filtering.Internal
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Runtime.InteropServices;

    internal interface ICustomUIFilterCriteriaParser
    {
        bool TryParse(CriteriaOperator criteria, out object[] values);
    }
}

