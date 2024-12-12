namespace DevExpress.Xpf.Core.FilteringUI
{
    using System;
    using System.Runtime.CompilerServices;

    public class ExcelStyleFilterElementQueryOperatorsEventArgs : QueryOperatorsEventArgsBase<ExcelStyleFilterElementOperatorItem>
    {
        internal ExcelStyleFilterElementQueryOperatorsEventArgs(string fieldName) : base(fieldName)
        {
        }

        public ExcelStyleFilterElementOperatorItemList Operators { get; set; }
    }
}

