namespace DevExpress.XtraExport.Implementation
{
    using DevExpress.Export.Xl;
    using DevExpress.Utils;
    using System;
    using System.Globalization;
    using System.Text;

    internal abstract class XlBinaryOperator : IXlFormulaParameter
    {
        private IXlFormulaParameter left;
        private IXlFormulaParameter right;

        protected XlBinaryOperator(IXlFormulaParameter left, IXlFormulaParameter right)
        {
            Guard.ArgumentNotNull(left, "left");
            Guard.ArgumentNotNull(right, "right");
            this.left = left;
            this.right = right;
        }

        public string ToString(CultureInfo culture)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(this.left.ToString(culture));
            builder.Append(this.ToStringCore(culture));
            builder.Append(this.right.ToString(culture));
            return builder.ToString();
        }

        protected abstract string ToStringCore(CultureInfo culture);

        public IXlFormulaParameter Left =>
            this.left;

        public IXlFormulaParameter Right =>
            this.right;

        public abstract short TypeCode { get; }
    }
}

