namespace DevExpress.Data.Browsing
{
    using DevExpress.Data.Filtering.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    internal class ParametersEvaluatorPropertyHandler : IEvaluatorPropertyHandler
    {
        public const string Name = "Parameters";

        public ParametersEvaluatorPropertyHandler(IEnumerable<IParameter> parameters);
        public static ParametersEvaluatorPropertyHandler Find(IDictionary<string, IEvaluatorPropertyHandler> handlers);
        public object GetValue(EvaluatorProperty property);
        public object GetValue(string parameterName);

        protected IEnumerable<IParameter> Parameters { get; }
    }
}

