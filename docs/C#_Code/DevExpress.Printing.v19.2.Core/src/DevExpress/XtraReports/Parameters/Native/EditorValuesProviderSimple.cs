namespace DevExpress.XtraReports.Parameters.Native
{
    using DevExpress.Data;
    using DevExpress.XtraReports.Parameters;
    using System;
    using System.Collections.Generic;

    public class EditorValuesProviderSimple : IParameterEditorValueProvider
    {
        private readonly Dictionary<IParameter, object> parameters;

        public EditorValuesProviderSimple(Dictionary<IParameter, object> parameters)
        {
            this.parameters = parameters;
        }

        public object GetValue(IParameter parameterIdentity)
        {
            object obj2 = null;
            this.parameters.TryGetValue(parameterIdentity, out obj2);
            return obj2;
        }
    }
}

