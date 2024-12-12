namespace DevExpress.Office.Utils
{
    using System;

    public class DrawingShapeBooleanProperties : OfficeDrawingBooleanPropertyBase
    {
        private const int background = 1;
        private const int lockShapeType = 8;
        private const int preferRelativeResize = 0x10;
        private const int flipVOverride = 0x40;
        private const int flipHOverride = 0x80;
        private const int useBackground = 0x10000;
        private const int useLockShapeType = 0x80000;
        private const int usePreferRelativeResize = 0x100000;
        private const int useFlipVOverride = 0x400000;
        private const int useFlipHOverride = 0x800000;

        public DrawingShapeBooleanProperties()
        {
            base.Value = 0x10001;
        }

        public override bool Complex =>
            false;

        public bool IsBackground
        {
            get => 
                base.GetFlag(1);
            set => 
                base.SetFlag(1, value);
        }

        public bool LockShapeType
        {
            get => 
                base.GetFlag(8);
            set => 
                base.SetFlag(8, value);
        }

        public bool PreferRelativeResize
        {
            get => 
                base.GetFlag(0x10);
            set => 
                base.SetFlag(0x10, value);
        }

        public bool FlipVOverride
        {
            get => 
                base.GetFlag(0x40);
            set => 
                base.SetFlag(0x40, value);
        }

        public bool FlipHOverride
        {
            get => 
                base.GetFlag(0x80);
            set => 
                base.SetFlag(0x80, value);
        }

        public bool UseBackground
        {
            get => 
                base.GetFlag(0x10000);
            set => 
                base.SetFlag(0x10000, value);
        }

        public bool UseLockShapeType
        {
            get => 
                base.GetFlag(0x80000);
            set => 
                base.SetFlag(0x80000, value);
        }

        public bool UsePreferRelativeResize
        {
            get => 
                base.GetFlag(0x100000);
            set => 
                base.SetFlag(0x100000, value);
        }

        public bool UseFlipVOverride
        {
            get => 
                base.GetFlag(0x400000);
            set => 
                base.SetFlag(0x400000, value);
        }

        public bool UseFlipHOverride
        {
            get => 
                base.GetFlag(0x800000);
            set => 
                base.SetFlag(0x800000, value);
        }
    }
}

