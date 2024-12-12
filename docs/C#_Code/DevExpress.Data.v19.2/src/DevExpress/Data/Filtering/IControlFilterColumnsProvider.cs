namespace DevExpress.Data.Filtering
{
    using System.ComponentModel;

    public interface IControlFilterColumnsProvider
    {
        PropertyDescriptorCollection GetColumnDescriptors();
    }
}

