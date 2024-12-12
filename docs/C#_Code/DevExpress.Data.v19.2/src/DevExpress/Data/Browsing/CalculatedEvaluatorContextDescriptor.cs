namespace DevExpress.Data.Browsing
{
    using DevExpress.Data.Filtering.Helpers;
    using DevExpress.XtraReports.Native;
    using DevExpress.XtraReports.UI;
    using System;
    using System.Collections.Generic;

    public class CalculatedEvaluatorContextDescriptor : XREvaluatorContextDescriptor
    {
        private readonly FieldType propertyType;

        public CalculatedEvaluatorContextDescriptor(ICalculatedField calculatedField, DataContext dataContext, IDictionary<string, IEvaluatorPropertyHandler> handlers);
        public CalculatedEvaluatorContextDescriptor(IEnumerable<IParameter> parameters, ICalculatedField calculatedField, DataContext dataContext);
        private object GetCastedResult(int result);
        public override object GetPropertyValue(object source, EvaluatorProperty propertyPath);
    }
}

