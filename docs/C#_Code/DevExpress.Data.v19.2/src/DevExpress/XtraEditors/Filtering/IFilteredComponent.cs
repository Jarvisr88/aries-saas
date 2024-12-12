namespace DevExpress.XtraEditors.Filtering
{
    using DevExpress.Data.Filtering;

    public interface IFilteredComponent : IFilteredComponentBase
    {
        IBoundPropertyCollection CreateFilterColumnCollection();
    }
}

