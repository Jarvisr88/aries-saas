namespace DevExpress.Xpf.Editors.Helpers
{
    using DevExpress.Data.Filtering.Helpers;
    using System;

    public interface IDisplayCriteriaGeneratorNamesSourceEx : IDisplayCriteriaGeneratorNamesSource
    {
        T WithDateRangeProcessing<T>(Func<T> func);
    }
}

