namespace DevExpress.Data
{
    using DevExpress.Data.Filtering;
    using System;

    public class ServerModeOrderDescriptor
    {
        public readonly CriteriaOperator SortExpression;
        public readonly CriteriaOperator AuxExpression;
        public readonly bool IsDesc;

        public ServerModeOrderDescriptor(CriteriaOperator sortExpression, bool isDesc);
        public ServerModeOrderDescriptor(CriteriaOperator sortExpression, bool isDesc, CriteriaOperator auxExpression);
        public override string ToString();
        public static string ToString(ServerModeOrderDescriptor[] descrs);

        public string SortPropertyName { get; }
    }
}

