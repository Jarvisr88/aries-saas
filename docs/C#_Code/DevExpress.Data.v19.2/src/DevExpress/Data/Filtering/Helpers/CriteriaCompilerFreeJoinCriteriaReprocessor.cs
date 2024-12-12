namespace DevExpress.Data.Filtering.Helpers
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    public class CriteriaCompilerFreeJoinCriteriaReprocessor : ClientCriteriaLazyPatcherBase
    {
        private readonly int CutOffDepth;
        private readonly IList<OperandParameter> Map;

        private CriteriaCompilerFreeJoinCriteriaReprocessor(int cutOffDepth, IList<OperandParameter> map);
        public static CriteriaOperator Process(CriteriaOperator op, out OperandParameter[] tbdValues);
        private static CriteriaOperator Process(int depth, CriteriaOperator op, IList<OperandParameter> mappings);
        private CriteriaOperator ProcessProperty(OperandProperty prop);
        private CriteriaOperator SubProcess(int cutOffDepth, CriteriaOperator criteriaOperator);
        public override CriteriaOperator Visit(AggregateOperand theOperand);
        public override CriteriaOperator Visit(JoinOperand theOperand);
        public override CriteriaOperator Visit(OperandProperty theOperand);
    }
}

