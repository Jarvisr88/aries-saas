namespace DevExpress.Office.Utils
{
    using System;

    public class DrawingBooleanProtectionProperties : OfficeDrawingBooleanPropertyBase
    {
        private const int lockGroup = 1;
        private const int lockAdjustHandles = 2;
        private const int lockText = 4;
        private const int lockVertices = 8;
        private const int lockCropping = 0x10;
        private const int lockSelect = 0x20;
        private const int lockPosition = 0x40;
        private const int lockAspectRatio = 0x80;
        private const int lockRotation = 0x100;
        private const int lockUngroup = 0x200;
        private const int useLockGroup = 0x10000;
        private const int useLockAdjustHandles = 0x20000;
        private const int useLockText = 0x40000;
        private const int useLockVertices = 0x80000;
        private const int useLockCropping = 0x100000;
        private const int useLockSelect = 0x200000;
        private const int useLockPosition = 0x400000;
        private const int useLockAspectRatio = 0x800000;
        private const int useLockRotation = 0x1000000;
        private const int useLockUngroup = 0x2000000;
        private const int defaultFlags = 0xe10080;

        public DrawingBooleanProtectionProperties()
        {
            base.Value = 0xe10080;
        }

        public override bool Complex =>
            false;

        public bool LockGroup
        {
            get => 
                base.GetFlag(1);
            set => 
                base.SetFlag(1, value);
        }

        public bool LockAdjustHandles
        {
            get => 
                base.GetFlag(2);
            set => 
                base.SetFlag(2, value);
        }

        public bool LockText
        {
            get => 
                base.GetFlag(4);
            set => 
                base.SetFlag(4, value);
        }

        public bool LockVertices
        {
            get => 
                base.GetFlag(8);
            set => 
                base.SetFlag(8, value);
        }

        public bool LockCropping
        {
            get => 
                base.GetFlag(0x10);
            set => 
                base.SetFlag(0x10, value);
        }

        public bool LockSelect
        {
            get => 
                base.GetFlag(0x20);
            set => 
                base.SetFlag(0x20, value);
        }

        public bool LockPosition
        {
            get => 
                base.GetFlag(0x40);
            set => 
                base.SetFlag(0x40, value);
        }

        public bool LockAspectRatio
        {
            get => 
                base.GetFlag(0x80);
            set => 
                base.SetFlag(0x80, value);
        }

        public bool LockRotation
        {
            get => 
                base.GetFlag(0x100);
            set => 
                base.SetFlag(0x100, value);
        }

        public bool LockUngroup
        {
            get => 
                base.GetFlag(0x200);
            set => 
                base.SetFlag(0x200, value);
        }

        public bool UseLockGroup
        {
            get => 
                base.GetFlag(0x10000);
            set => 
                base.SetFlag(0x10000, value);
        }

        public bool UseLockAdjustHandles
        {
            get => 
                base.GetFlag(0x20000);
            set => 
                base.SetFlag(0x20000, value);
        }

        public bool UseLockText
        {
            get => 
                base.GetFlag(0x40000);
            set => 
                base.SetFlag(0x40000, value);
        }

        public bool UseLockVertices
        {
            get => 
                base.GetFlag(0x80000);
            set => 
                base.SetFlag(0x80000, value);
        }

        public bool UseLockCropping
        {
            get => 
                base.GetFlag(0x100000);
            set => 
                base.SetFlag(0x100000, value);
        }

        public bool UseLockSelect
        {
            get => 
                base.GetFlag(0x200000);
            set => 
                base.SetFlag(0x200000, value);
        }

        public bool UseLockPosition
        {
            get => 
                base.GetFlag(0x400000);
            set => 
                base.SetFlag(0x400000, value);
        }

        public bool UseLockAspectRatio
        {
            get => 
                base.GetFlag(0x800000);
            set => 
                base.SetFlag(0x800000, value);
        }

        public bool UseLockRotation
        {
            get => 
                base.GetFlag(0x1000000);
            set => 
                base.SetFlag(0x1000000, value);
        }

        public bool UseLockUngroup
        {
            get => 
                base.GetFlag(0x2000000);
            set => 
                base.SetFlag(0x2000000, value);
        }
    }
}

