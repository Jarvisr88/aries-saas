namespace DevExpress.Office.Utils
{
    using System;

    public class DrawingShadowType : OfficeDrawingIntPropertyBase
    {
        public const MsoShadowType DefaultValue = MsoShadowType.MsoShadowOffset;

        public DrawingShadowType()
        {
            base.Value = 0;
        }

        public DrawingShadowType(MsoShadowType shadowType)
        {
            base.Value = (int) shadowType;
        }

        public override bool Complex =>
            false;

        public MsoShadowType ShadowType
        {
            get => 
                (MsoShadowType) base.Value;
            set => 
                base.Value = (int) value;
        }
    }
}

