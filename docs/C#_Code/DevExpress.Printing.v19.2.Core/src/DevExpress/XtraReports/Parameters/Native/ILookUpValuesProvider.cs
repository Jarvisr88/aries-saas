namespace DevExpress.XtraReports.Parameters.Native
{
    using DevExpress.XtraReports.Parameters;
    using System.Threading.Tasks;

    public interface ILookUpValuesProvider
    {
        Task<IEnumerable<ParameterLookUpValuesContainer>> GetLookUpValues(Parameter changedParameter, IParameterEditorValueProvider editorValueProvider);
    }
}

