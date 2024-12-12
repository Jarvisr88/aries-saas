namespace DevExpress.DataAccess.Native
{
    using DevExpress.Data;
    using System;
    using System.Collections.Generic;

    public class ParameterSupplier : IParameterSupplierBase
    {
        private readonly IEnumerable<IParameter> parameters;

        public ParameterSupplier(IEnumerable<IParameter> parameters)
        {
            this.parameters = parameters;
        }

        IEnumerable<IParameter> IParameterSupplierBase.GetIParameters() => 
            this.parameters;
    }
}

