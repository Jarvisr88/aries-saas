namespace DevExpress.Office.Utils
{
    using System;

    public class DrawingGroupShapeBooleanProperties : OfficeDrawingBooleanPropertyBase
    {
        private const int print = 1;
        private const int hidden = 2;
        private const int isButton = 8;
        private const int behindDocument = 0x20;
        private const int allowOverlap = 0x200;
        private const int horizRule = 0x800;
        private const int noShadeHR = 0x1000;
        private const int standardHR = 0x2000;
        private const int layoutInCell = 0x8000;
        private const int usePrint = 0x10000;
        private const int useHidden = 0x20000;
        private const int useIsButton = 0x80000;
        private const int useBehindDocument = 0x200000;
        private const int useAllowOverlap = 0x2000000;
        private const int useHorizRule = 0x8000000;
        private const int useNoShadeHR = 0x10000000;
        private const int useStandardHR = 0x20000000;
        private const int useLayoutInCell = -2147483648;

        public DrawingGroupShapeBooleanProperties()
        {
            base.Value = 0x220000;
        }

        public override void Execute(OfficeArtPropertiesBase owner)
        {
            IOfficeArtPropertiesBase base2 = (IOfficeArtPropertiesBase) owner;
            base2.IsBehindDoc = this.IsBehindDoc;
            base2.UseIsBehindDoc = this.UseBehindDocument;
            base2.LayoutInCell = this.LayoutInCell;
            base2.UseLayoutInCell = this.UseLayoutInCell;
            owner.HandleGroupShapeBooleanProperties(this);
        }

        public override bool Complex =>
            false;

        public bool Print
        {
            get => 
                base.GetFlag(1);
            set => 
                base.SetFlag(1, value);
        }

        public bool Hidden
        {
            get => 
                base.GetFlag(2);
            set => 
                base.SetFlag(2, value);
        }

        public bool IsButton
        {
            get => 
                base.GetFlag(8);
            set => 
                base.SetFlag(8, value);
        }

        public bool IsBehindDoc
        {
            get => 
                base.GetFlag(0x20);
            set => 
                base.SetFlag(0x20, value);
        }

        public bool LayoutInCell
        {
            get => 
                base.GetFlag(0x8000);
            set => 
                base.SetFlag(0x8000, value);
        }

        public bool AllowOverlap
        {
            get => 
                base.GetFlag(0x200);
            set => 
                base.SetFlag(0x200, value);
        }

        public bool HorizRule
        {
            get => 
                base.GetFlag(0x800);
            set => 
                base.SetFlag(0x800, value);
        }

        public bool NoShadeHR
        {
            get => 
                base.GetFlag(0x1000);
            set => 
                base.SetFlag(0x1000, value);
        }

        public bool StandardHR
        {
            get => 
                base.GetFlag(0x2000);
            set => 
                base.SetFlag(0x2000, value);
        }

        public bool UsePrint
        {
            get => 
                base.GetFlag(0x10000);
            set => 
                base.SetFlag(0x10000, value);
        }

        public bool UseHidden
        {
            get => 
                base.GetFlag(0x20000);
            set => 
                base.SetFlag(0x20000, value);
        }

        public bool UseIsButton
        {
            get => 
                base.GetFlag(0x80000);
            set => 
                base.SetFlag(0x80000, value);
        }

        public bool UseBehindDocument
        {
            get => 
                base.GetFlag(0x200000);
            set => 
                base.SetFlag(0x200000, value);
        }

        public bool UseLayoutInCell
        {
            get => 
                base.GetFlag(-2147483648);
            set => 
                base.SetFlag(-2147483648, value);
        }

        public bool UseAllowOverlap
        {
            get => 
                base.GetFlag(0x2000000);
            set => 
                base.SetFlag(0x2000000, value);
        }

        public bool UseHorizRule
        {
            get => 
                base.GetFlag(0x8000000);
            set => 
                base.SetFlag(0x8000000, value);
        }

        public bool UseNoShadeHR
        {
            get => 
                base.GetFlag(0x10000000);
            set => 
                base.SetFlag(0x10000000, value);
        }

        public bool UseStandardHR
        {
            get => 
                base.GetFlag(0x20000000);
            set => 
                base.SetFlag(0x20000000, value);
        }
    }
}

