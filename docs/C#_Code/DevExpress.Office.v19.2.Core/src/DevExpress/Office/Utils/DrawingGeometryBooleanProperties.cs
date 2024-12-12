namespace DevExpress.Office.Utils
{
    using System;

    public class DrawingGeometryBooleanProperties : OfficeDrawingBooleanPropertyBase
    {
        private const int FillFlag = 1;
        private const int FillShapeShapeFlag = 2;
        private const int GTextFlag = 4;
        private const int LineFlag = 8;
        private const int F3DFlag = 0x10;
        private const int ShadowFlag = 0x20;
        private const int SoftEdgeFlag = 0x80;
        private const int GlowFlag = 0x100;
        private const int ReflectionFlag = 0x200;
        private const int UseFillFlag = 0x10000;
        private const int UseFillShapeShapeFlag = 0x20000;
        private const int UseGTextFlag = 0x40000;
        private const int UseLineFlag = 0x80000;
        private const int UseF3DFlag = 0x100000;
        private const int UseShadowFlag = 0x200000;
        private const int UseSoftEdgeFlag = 0x800000;
        private const int UseGlowFlag = 0x1000000;
        private const int UseReflectionFlag = 0x2000000;

        public override bool Complex =>
            false;

        public bool Fill
        {
            get => 
                base.GetFlag(1);
            set => 
                base.SetFlag(1, value);
        }

        public bool FillShadeShape
        {
            get => 
                base.GetFlag(2);
            set => 
                base.SetFlag(2, value);
        }

        public bool GText
        {
            get => 
                base.GetFlag(4);
            set => 
                base.SetFlag(4, value);
        }

        public bool Line
        {
            get => 
                base.GetFlag(8);
            set => 
                base.SetFlag(8, value);
        }

        public bool F3D
        {
            get => 
                base.GetFlag(0x10);
            set => 
                base.SetFlag(0x10, value);
        }

        public bool Shadow
        {
            get => 
                base.GetFlag(0x20);
            set => 
                base.SetFlag(0x20, value);
        }

        public bool SoftEdge
        {
            get => 
                base.GetFlag(0x80);
            set => 
                base.SetFlag(0x80, value);
        }

        public bool Glow
        {
            get => 
                base.GetFlag(0x100);
            set => 
                base.SetFlag(0x100, value);
        }

        public bool Reflection
        {
            get => 
                base.GetFlag(0x200);
            set => 
                base.SetFlag(0x200, value);
        }

        public bool UseFill
        {
            get => 
                base.GetFlag(0x10000);
            set => 
                base.SetFlag(0x10000, value);
        }

        public bool UseFillShadeShape
        {
            get => 
                base.GetFlag(0x20000);
            set => 
                base.SetFlag(0x20000, value);
        }

        public bool UseGText
        {
            get => 
                base.GetFlag(0x40000);
            set => 
                base.SetFlag(0x40000, value);
        }

        public bool UseLine
        {
            get => 
                base.GetFlag(0x80000);
            set => 
                base.SetFlag(0x80000, value);
        }

        public bool UseF3D
        {
            get => 
                base.GetFlag(0x100000);
            set => 
                base.SetFlag(0x100000, value);
        }

        public bool UseShadow
        {
            get => 
                base.GetFlag(0x200000);
            set => 
                base.SetFlag(0x200000, value);
        }

        public bool UseSoftEdge
        {
            get => 
                base.GetFlag(0x800000);
            set => 
                base.SetFlag(0x800000, value);
        }

        public bool UseGlow
        {
            get => 
                base.GetFlag(0x1000000);
            set => 
                base.SetFlag(0x1000000, value);
        }

        public bool UseReflection
        {
            get => 
                base.GetFlag(0x2000000);
            set => 
                base.SetFlag(0x2000000, value);
        }
    }
}

