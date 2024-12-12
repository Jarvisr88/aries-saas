namespace DevExpress.Data.Filtering.Helpers
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Collections.Generic;

    public abstract class CriteriaToStringWithParametersProcessor : CriteriaToStringBase
    {
        public const string ParameterPrefix = "?";
        protected readonly List<OperandValue> Parameters;

        protected CriteriaToStringWithParametersProcessor();
        public override CriteriaToStringVisitResult Visit(OperandValue operand);
    }
}

