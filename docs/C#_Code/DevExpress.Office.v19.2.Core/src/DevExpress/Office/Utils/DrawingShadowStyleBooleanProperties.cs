namespace DevExpress.Office.Utils
{
    using System;

    public class DrawingShadowStyleBooleanProperties : OfficeDrawingBooleanPropertyBase
    {
        private const int shadowObscured = 1;
        private const int shadow = 2;
        private const int useShadowObscured = 0x10000;
        private const int useShadow = 0x20000;

        public DrawingShadowStyleBooleanProperties()
        {
            base.Value = 0;
        }

        public override bool Complex =>
            false;

        public bool ShadowObscured
        {
            get => 
                base.GetFlag(1);
            set => 
                base.SetFlag(1, value);
        }

        public bool Shadow
        {
            get => 
                base.GetFlag(2);
            set => 
                base.SetFlag(2, value);
        }

        public bool UseShadowObscured
        {
            get => 
                base.GetFlag(0x10000);
            set => 
                base.SetFlag(0x10000, value);
        }

        public bool UseShadow
        {
            get => 
                base.GetFlag(0x20000);
            set => 
                base.SetFlag(0x20000, value);
        }
    }
}

