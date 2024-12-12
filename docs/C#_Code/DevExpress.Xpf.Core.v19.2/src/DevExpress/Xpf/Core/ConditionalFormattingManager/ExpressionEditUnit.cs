namespace DevExpress.Xpf.Core.ConditionalFormattingManager
{
    using DevExpress.Data.Filtering;
    using DevExpress.Xpf.Core.ConditionalFormatting;
    using System;
    using System.Windows;

    public abstract class ExpressionEditUnit : BaseEditUnit
    {
        private DevExpress.Xpf.Core.ConditionalFormatting.Format format;

        protected ExpressionEditUnit()
        {
        }

        internal virtual CriteriaOperator GetActualExpressionCriteriaOperator() => 
            CriteriaOperator.TryParse(base.Expression, new object[0]);

        public override Freezable GetFormat() => 
            this.Format;

        public DevExpress.Xpf.Core.ConditionalFormatting.Format Format
        {
            get => 
                this.format;
            set
            {
                if (!ReferenceEquals(this.format, value))
                {
                    this.format = value;
                }
                base.RegisterPropertyModification("Format");
            }
        }

        public override bool CanApplyToRow =>
            true;
    }
}

