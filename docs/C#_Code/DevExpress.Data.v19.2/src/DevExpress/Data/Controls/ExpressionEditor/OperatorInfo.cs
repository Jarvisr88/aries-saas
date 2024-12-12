namespace DevExpress.Data.Controls.ExpressionEditor
{
    using System;

    public class OperatorInfo : ItemInfoBase
    {
        internal const string DefaultCategoryName = "Operators";

        public OperatorInfo();
        public OperatorInfo(OperatorInfo other);
        public OperatorInfo(string category);
    }
}

