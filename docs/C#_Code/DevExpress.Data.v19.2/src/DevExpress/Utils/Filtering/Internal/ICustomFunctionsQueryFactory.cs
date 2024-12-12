namespace DevExpress.Utils.Filtering.Internal
{
    using DevExpress.Data.Filtering;

    public interface ICustomFunctionsQueryFactory
    {
        DevExpress.Data.Filtering.CustomFunctionEventArgs Create(ICustomUIFilters filters);
    }
}

