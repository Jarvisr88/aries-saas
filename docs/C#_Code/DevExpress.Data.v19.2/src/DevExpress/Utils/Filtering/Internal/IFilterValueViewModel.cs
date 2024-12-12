namespace DevExpress.Utils.Filtering.Internal
{
    using DevExpress.Data.Filtering;

    public interface IFilterValueViewModel
    {
        CriteriaOperator CreateFilterCriteria();
    }
}

