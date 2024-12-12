namespace DevExpress.XtraReports.Native
{
    using DevExpress.Data.Browsing;
    using DevExpress.Data.Filtering;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    public class DeserializationFilterStringVisitor : ClientCriteriaVisitorBase
    {
        private IExtensionsProvider rootComponent;
        private DataContext dataContext;
        private object dataSource;
        private string dataMember;
        private Stack<string> prefixElements;

        public DeserializationFilterStringVisitor(IExtensionsProvider rootComponent, DataContext dataContext, object dataSource, string dataMember);
        private Type DetectType(string dataMember);
        private string GetFullName(string property);
        private static void SplitDataMember(string dataMember, out string first, out string second);
        private void TryConvertOperand(OperandProperty leftOperand, CriteriaOperator criteriaOperator);
        public override CriteriaOperator Visit(AggregateOperand theOperand);
        public override CriteriaOperator Visit(BetweenOperator theOperator);
        public override CriteriaOperator Visit(BinaryOperator theOperator);
        public override CriteriaOperator Visit(InOperator theOperator);
    }
}

