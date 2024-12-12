namespace DevExpress.XtraExport.Implementation
{
    using DevExpress.Export.Xl;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text;

    internal abstract class XlFunctionBase : IXlFormulaParameter
    {
        private readonly List<IXlFormulaParameter> parameters;

        protected XlFunctionBase(IEnumerable<IXlFormulaParameter> parameters)
        {
            this.parameters = new List<IXlFormulaParameter>(parameters);
        }

        public string ToString(CultureInfo culture)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(this.FunctionName);
            builder.Append("(");
            if (!this.IsValidParametersCount)
            {
                builder.Append("#VALUE!");
            }
            else
            {
                for (int i = 0; i < this.Parameters.Count; i++)
                {
                    if (i > 0)
                    {
                        builder.Append(",");
                    }
                    builder.Append(this.Parameters[i].ToString(culture));
                }
            }
            builder.Append(")");
            return builder.ToString();
        }

        public IList<IXlFormulaParameter> Parameters =>
            this.parameters;

        public abstract XlPtgDataType ParamType { get; }

        public abstract int FunctionCode { get; }

        protected abstract string FunctionName { get; }

        public virtual bool IsValidParametersCount =>
            this.parameters.Count > 0;
    }
}

