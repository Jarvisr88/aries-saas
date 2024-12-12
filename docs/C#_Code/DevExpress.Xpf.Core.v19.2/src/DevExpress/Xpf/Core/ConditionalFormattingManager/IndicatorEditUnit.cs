namespace DevExpress.Xpf.Core.ConditionalFormattingManager
{
    using System;

    public abstract class IndicatorEditUnit : BaseEditUnit
    {
        private object minValue;
        private object maxValue;

        protected IndicatorEditUnit()
        {
        }

        public object MinValue
        {
            get => 
                this.minValue;
            set
            {
                if (this.minValue != value)
                {
                    this.minValue = value;
                }
                base.RegisterPropertyModification("MinValue");
            }
        }

        public object MaxValue
        {
            get => 
                this.maxValue;
            set
            {
                if (this.maxValue != value)
                {
                    this.maxValue = value;
                }
                base.RegisterPropertyModification("MaxValue");
            }
        }
    }
}

