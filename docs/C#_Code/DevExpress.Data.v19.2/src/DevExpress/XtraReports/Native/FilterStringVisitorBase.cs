namespace DevExpress.XtraReports.Native
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Collections.Generic;

    public abstract class FilterStringVisitorBase : ClientCriteriaVisitorBase
    {
        private readonly List<string> propertyElements;
        private readonly List<string> resultNames;

        protected FilterStringVisitorBase();
        private string ConvertName(string name);
        protected abstract string ConvertNameCore(string name);
        private string GetNameAtCurrentPosition(string propertyName);
        private string GetNameWithUpDepth(string propertyName, int upDepth);
        private string GetPropertyName(string propertyName, int upDepth);
        private int GetUpDepth(ref string propertyName);
        private string SkipPrefix(string displayName, int upDepth);
        public override CriteriaOperator Visit(AggregateOperand theOperand);
        public override CriteriaOperator Visit(OperandProperty theOperand);

        public IList<string> ResultNames { get; }
    }
}

