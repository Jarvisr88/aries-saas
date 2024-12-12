namespace DevExpress.Xpo.DB.Helpers
{
    using System;
    using System.Globalization;

    public class ProcessParameterInvariantCulture : IFormatProvider, ICustomFormatter
    {
        private ProcessParameter processParameter;

        public ProcessParameterInvariantCulture(ProcessParameter processParameter)
        {
            this.processParameter = processParameter;
        }

        public string Format(string format, object arg, IFormatProvider formatProvider) => 
            !(arg is string) ? ((this.processParameter == null) ? (!(arg is IFormattable) ? arg.ToString() : ((IFormattable) arg).ToString(format, CultureInfo.InvariantCulture)) : this.processParameter(arg)) : ((string) arg);

        public object GetFormat(Type formatType) => 
            !(formatType == typeof(ICustomFormatter)) ? null : this;
    }
}

