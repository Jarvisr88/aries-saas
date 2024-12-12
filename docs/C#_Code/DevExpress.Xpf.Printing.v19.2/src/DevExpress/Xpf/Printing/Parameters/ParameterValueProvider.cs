namespace DevExpress.Xpf.Printing.Parameters
{
    using DevExpress.Data;
    using DevExpress.Utils;
    using DevExpress.Xpf.Printing.Parameters.Models;
    using DevExpress.XtraReports.Parameters;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ParameterValueProvider : IParameterEditorValueProvider
    {
        private readonly IEnumerable<ParameterModel> parameterModels;

        public ParameterValueProvider(IEnumerable<ParameterModel> parameterModels)
        {
            this.parameterModels = parameterModels;
        }

        public object GetValue(IParameter parameterIdentity)
        {
            Guard.ArgumentNotNull(parameterIdentity, "parameterIdentity");
            ParameterModel model = this.parameterModels.FirstOrDefault<ParameterModel>(x => ReferenceEquals(x.Parameter, parameterIdentity));
            return model?.Value;
        }
    }
}

