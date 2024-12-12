namespace DevExpress.Data
{
    using System.Collections.Generic;

    public interface IParameterSupplierBase
    {
        IEnumerable<IParameter> GetIParameters();
    }
}

