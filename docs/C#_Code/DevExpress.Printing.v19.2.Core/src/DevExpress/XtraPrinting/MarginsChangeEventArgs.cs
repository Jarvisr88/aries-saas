namespace DevExpress.XtraPrinting
{
    using System;

    public class MarginsChangeEventArgs : EventArgs
    {
        private MarginSide side;
        private float value;

        internal MarginsChangeEventArgs(MarginSide side, float value)
        {
            this.value = value;
            this.side = side;
        }

        public MarginSide Side =>
            this.side;

        public float Value
        {
            get => 
                this.value;
            set => 
                this.value = value;
        }
    }
}

