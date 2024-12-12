namespace DevExpress.Data.Browsing
{
    using DevExpress.Data.Filtering.Helpers;
    using System;

    public interface IEvaluatorPropertyHandler
    {
        object GetValue(EvaluatorProperty property);
    }
}

