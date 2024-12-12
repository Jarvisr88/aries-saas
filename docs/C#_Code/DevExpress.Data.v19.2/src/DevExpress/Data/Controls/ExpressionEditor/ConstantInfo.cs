namespace DevExpress.Data.Controls.ExpressionEditor
{
    using System;

    public class ConstantInfo : ItemInfoBase
    {
        internal const string DefaultCategoryName = "Constants";

        public ConstantInfo();
        public ConstantInfo(ConstantInfo other);
        public ConstantInfo(string category);
    }
}

