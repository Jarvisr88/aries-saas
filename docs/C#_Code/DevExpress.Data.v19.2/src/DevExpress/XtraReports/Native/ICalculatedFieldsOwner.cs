namespace DevExpress.XtraReports.Native
{
    using System;
    using System.Collections.Generic;

    [Obsolete("Use IParameterSupplierBase interface instead")]
    public interface ICalculatedFieldsOwner
    {
        IEnumerable<IParameter> Parameters { get; }
    }
}

