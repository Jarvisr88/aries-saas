namespace DevExpress.Data.Controls.ExpressionEditor
{
    using DevExpress.Data;
    using System;
    using System.Runtime.CompilerServices;

    public class ParameterInfo : ItemInfoBase
    {
        internal const string DefaultCategoryName = "Parameters";

        public ParameterInfo();
        public ParameterInfo(ParameterInfo other);
        public ParameterInfo(IParameter parameter);
        public ParameterInfo(string category);

        public System.Type Type { get; set; }
    }
}

