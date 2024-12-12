namespace DevExpress.Office.NumberConverters
{
    using System;

    public abstract class DigitInfo
    {
        private INumericsProvider provider;
        private long number;

        protected DigitInfo(INumericsProvider provider, long number)
        {
            this.provider = provider;
            this.number = number;
        }

        public string ConvertToString() => 
            this.GetNumerics()[(int) ((IntPtr) this.number)];

        protected internal abstract string[] GetNumerics();

        public abstract DigitType Type { get; }

        public INumericsProvider Provider
        {
            get => 
                this.provider;
            set => 
                this.provider = value;
        }

        public long Number =>
            this.number;
    }
}

