namespace DevExpress.Data.Filtering.Helpers
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    public class EvaluatorPropertyCache
    {
        private Dictionary<OperandProperty, EvaluatorProperty> store;

        public EvaluatorPropertyCache();

        public EvaluatorProperty this[OperandProperty property] { get; }
    }
}

