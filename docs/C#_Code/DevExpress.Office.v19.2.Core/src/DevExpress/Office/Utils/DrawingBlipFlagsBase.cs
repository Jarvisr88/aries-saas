namespace DevExpress.Office.Utils
{
    using System;

    public abstract class DrawingBlipFlagsBase : OfficeDrawingIntPropertyBase
    {
        private const int flagFile = 1;
        private const int flagUrl = 2;
        private const int flagDontSave = 4;
        private const int flagLinkToFile = 8;

        protected DrawingBlipFlagsBase()
        {
        }

        public override bool Complex =>
            false;

        public bool IsComment =>
            base.Value == 0;

        public bool IsFile
        {
            get => 
                base.GetFlag(1);
            set => 
                base.SetFlag(1, value);
        }

        public bool IsUrl
        {
            get => 
                base.GetFlag(2);
            set => 
                base.SetFlag(2, value);
        }

        public bool DontSave
        {
            get => 
                base.GetFlag(4);
            set => 
                base.SetFlag(4, value);
        }

        public bool LinkToFile
        {
            get => 
                base.GetFlag(8);
            set => 
                base.SetFlag(8, value);
        }
    }
}

