namespace DevExpress.Office.Utils
{
    using System;

    public class DrawingFillShadeType : OfficeDrawingIntPropertyBase
    {
        private const int color = 1;
        private const int gamma = 2;
        private const int sigma = 4;
        private const int band = 8;
        private const int oneColor = 0x10;

        public DrawingFillShadeType()
        {
            base.Value = 3;
        }

        public override bool Complex =>
            false;

        public bool Color
        {
            get => 
                base.GetFlag(1);
            set => 
                base.SetFlag(1, value);
        }

        public bool Gamma
        {
            get => 
                base.GetFlag(2);
            set => 
                base.SetFlag(2, value);
        }

        public bool Sigma
        {
            get => 
                base.GetFlag(4);
            set => 
                base.SetFlag(4, value);
        }

        public bool Band
        {
            get => 
                base.GetFlag(8);
            set => 
                base.SetFlag(8, value);
        }

        public bool OneColor
        {
            get => 
                base.GetFlag(0x10);
            set => 
                base.SetFlag(0x10, value);
        }
    }
}

