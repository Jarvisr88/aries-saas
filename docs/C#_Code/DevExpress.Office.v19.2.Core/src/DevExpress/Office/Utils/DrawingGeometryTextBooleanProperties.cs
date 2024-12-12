namespace DevExpress.Office.Utils
{
    using System;

    public class DrawingGeometryTextBooleanProperties : OfficeDrawingBooleanPropertyBase
    {
        private const int strikeThrough = 1;
        private const int smallCaps = 2;
        private const int shadow = 4;
        private const int underline = 8;
        private const int italic = 0x10;
        private const int bold = 0x20;
        private const int dxMeasure = 0x40;
        private const int normalize = 0x80;
        private const int bestFit = 0x100;
        private const int shrinkFit = 0x200;
        private const int stretch = 0x400;
        private const int tight = 0x800;
        private const int kern = 0x1000;
        private const int vertical = 0x2000;
        private const int haveText = 0x4000;
        private const int reverseRows = 0x8000;
        private const int useStrikeThrough = 0x10000;
        private const int useSmallCaps = 0x20000;
        private const int useShadow = 0x40000;
        private const int useUnderline = 0x80000;
        private const int useItalic = 0x100000;
        private const int useBold = 0x200000;
        private const int useDxMeasure = 0x400000;
        private const int useNormalize = 0x800000;
        private const int useBestFit = 0x1000000;
        private const int useShrinkFit = 0x2000000;
        private const int useStretch = 0x4000000;
        private const int useTight = 0x8000000;
        private const int useKern = 0x10000000;
        private const int useVertical = 0x20000000;
        private const int useHaveText = 0x40000000;
        private const int useReverseRows = -2147483648;

        public DrawingGeometryTextBooleanProperties()
        {
            base.Value = 0;
        }

        public override bool Complex =>
            false;

        public bool StrikeThrough
        {
            get => 
                base.GetFlag(1);
            set => 
                base.SetFlag(1, value);
        }

        public bool SmallCaps
        {
            get => 
                base.GetFlag(2);
            set => 
                base.SetFlag(2, value);
        }

        public bool Shadow
        {
            get => 
                base.GetFlag(4);
            set => 
                base.SetFlag(4, value);
        }

        public bool Underline
        {
            get => 
                base.GetFlag(8);
            set => 
                base.SetFlag(8, value);
        }

        public bool Italic
        {
            get => 
                base.GetFlag(0x10);
            set => 
                base.SetFlag(0x10, value);
        }

        public bool Bold
        {
            get => 
                base.GetFlag(0x20);
            set => 
                base.SetFlag(0x20, value);
        }

        public bool DxMeasure
        {
            get => 
                base.GetFlag(0x40);
            set => 
                base.SetFlag(0x40, value);
        }

        public bool Normalize
        {
            get => 
                base.GetFlag(0x80);
            set => 
                base.SetFlag(0x80, value);
        }

        public bool BestFit
        {
            get => 
                base.GetFlag(0x100);
            set => 
                base.SetFlag(0x100, value);
        }

        public bool ShrinkFit
        {
            get => 
                base.GetFlag(0x200);
            set => 
                base.SetFlag(0x200, value);
        }

        public bool Stretch
        {
            get => 
                base.GetFlag(0x400);
            set => 
                base.SetFlag(0x400, value);
        }

        public bool Tight
        {
            get => 
                base.GetFlag(0x800);
            set => 
                base.SetFlag(0x800, value);
        }

        public bool Kern
        {
            get => 
                base.GetFlag(0x1000);
            set => 
                base.SetFlag(0x1000, value);
        }

        public bool Vertical
        {
            get => 
                base.GetFlag(0x2000);
            set => 
                base.SetFlag(0x2000, value);
        }

        public bool HaveText
        {
            get => 
                base.GetFlag(0x4000);
            set => 
                base.SetFlag(0x4000, value);
        }

        public bool ReverseRows
        {
            get => 
                base.GetFlag(0x8000);
            set => 
                base.SetFlag(0x8000, value);
        }

        public bool UseStrikeThrough
        {
            get => 
                base.GetFlag(0x10000);
            set => 
                base.SetFlag(0x10000, value);
        }

        public bool UseSmallCaps
        {
            get => 
                base.GetFlag(0x20000);
            set => 
                base.SetFlag(0x20000, value);
        }

        public bool UseShadow
        {
            get => 
                base.GetFlag(0x40000);
            set => 
                base.SetFlag(0x40000, value);
        }

        public bool UseUnderline
        {
            get => 
                base.GetFlag(0x80000);
            set => 
                base.SetFlag(0x80000, value);
        }

        public bool UseItalic
        {
            get => 
                base.GetFlag(0x100000);
            set => 
                base.SetFlag(0x100000, value);
        }

        public bool UseBold
        {
            get => 
                base.GetFlag(0x200000);
            set => 
                base.SetFlag(0x200000, value);
        }

        public bool UseDxMeasure
        {
            get => 
                base.GetFlag(0x400000);
            set => 
                base.SetFlag(0x400000, value);
        }

        public bool UseNormalize
        {
            get => 
                base.GetFlag(0x800000);
            set => 
                base.SetFlag(0x800000, value);
        }

        public bool UseBestFit
        {
            get => 
                base.GetFlag(0x1000000);
            set => 
                base.SetFlag(0x1000000, value);
        }

        public bool UseShrinkFit
        {
            get => 
                base.GetFlag(0x2000000);
            set => 
                base.SetFlag(0x2000000, value);
        }

        public bool UseStretch
        {
            get => 
                base.GetFlag(0x4000000);
            set => 
                base.SetFlag(0x4000000, value);
        }

        public bool UseTight
        {
            get => 
                base.GetFlag(0x8000000);
            set => 
                base.SetFlag(0x8000000, value);
        }

        public bool UseKern
        {
            get => 
                base.GetFlag(0x10000000);
            set => 
                base.SetFlag(0x10000000, value);
        }

        public bool UseVertical
        {
            get => 
                base.GetFlag(0x20000000);
            set => 
                base.SetFlag(0x20000000, value);
        }

        public bool UseHaveText
        {
            get => 
                base.GetFlag(0x40000000);
            set => 
                base.SetFlag(0x40000000, value);
        }

        public bool UseReverseRows
        {
            get => 
                base.GetFlag(-2147483648);
            set => 
                base.SetFlag(-2147483648, value);
        }
    }
}

