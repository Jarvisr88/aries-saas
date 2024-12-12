namespace DevExpress.Office.Utils
{
    using System;

    public class DrawingTextBooleanProperties : OfficeDrawingBooleanPropertyBase
    {
        private const int fitShapeToText = 2;
        private const int autoTextMargins = 8;
        private const int selectText = 0x10;
        private const int useFitShapeToText = 0x20000;
        private const int useAutoTextMargins = 0x80000;
        private const int useSelectText = 0x100000;

        public DrawingTextBooleanProperties()
        {
            base.Value = 0x80008;
        }

        public override void Execute(OfficeArtPropertiesBase owner)
        {
            IOfficeArtProperties properties = owner as IOfficeArtProperties;
            if (properties != null)
            {
                properties.UseFitShapeToText = this.UseFitShapeToText;
                properties.FitShapeToText = this.FitShapeToText;
            }
        }

        public override bool Complex =>
            false;

        public bool FitShapeToText
        {
            get => 
                base.GetFlag(2);
            set => 
                base.SetFlag(2, value);
        }

        public bool AutoTextMargins
        {
            get => 
                base.GetFlag(8);
            set => 
                base.SetFlag(8, value);
        }

        public bool SelectText
        {
            get => 
                base.GetFlag(0x10);
            set => 
                base.SetFlag(0x10, value);
        }

        public bool UseFitShapeToText
        {
            get => 
                base.GetFlag(0x20000);
            set => 
                base.SetFlag(0x20000, value);
        }

        public bool UseAutoTextMargins
        {
            get => 
                base.GetFlag(0x80000);
            set => 
                base.SetFlag(0x80000, value);
        }

        public bool UseSelectText
        {
            get => 
                base.GetFlag(0x100000);
            set => 
                base.SetFlag(0x100000, value);
        }
    }
}

