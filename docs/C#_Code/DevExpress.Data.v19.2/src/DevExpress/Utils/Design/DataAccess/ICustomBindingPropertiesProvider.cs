namespace DevExpress.Utils.Design.DataAccess
{
    using System.Collections.Generic;

    public interface ICustomBindingPropertiesProvider
    {
        IEnumerable<ICustomBindingProperty> GetCustomBindingProperties();
    }
}

