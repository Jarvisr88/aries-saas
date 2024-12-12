namespace DevExpress.Utils.Filtering.Internal
{
    using DevExpress.Data.Filtering;
    using System;

    public interface ICustomFunctionsQueryContext
    {
        void RaiseQueryCustomFunctions(DevExpress.Data.Filtering.CustomFunctionEventArgs e);
    }
}

