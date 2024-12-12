namespace DevExpress.XtraReports.Parameters
{
    using DevExpress.Data;
    using System;

    public interface IParameterEditorValueProvider
    {
        object GetValue(IParameter parameterIdentity);
    }
}

