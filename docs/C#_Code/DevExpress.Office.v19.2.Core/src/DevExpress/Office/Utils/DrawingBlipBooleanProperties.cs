namespace DevExpress.Office.Utils
{
    using System;

    public class DrawingBlipBooleanProperties : OfficeDrawingBooleanPropertyBase
    {
        private const int pictureActive = 1;
        private const int pictureBiLevel = 2;
        private const int pictureGray = 4;
        private const int noHitTestPicture = 8;
        private const int looping = 0x10;
        private const int rewind = 0x20;
        private const int picturePreserveGrays = 0x40;
        private const int usePictureActive = 0x10000;
        private const int usePictureBiLevel = 0x20000;
        private const int usePictureGray = 0x40000;
        private const int useNoHitTestPicture = 0x80000;
        private const int useLooping = 0x100000;
        private const int useRewind = 0x200000;
        private const int usePicturePreserveGrays = 0x400000;

        public override bool Complex =>
            false;

        public bool UsePicturePreserveGrays
        {
            get => 
                base.GetFlag(0x400000);
            set => 
                base.SetFlag(0x400000, value);
        }

        public bool UseRewind
        {
            get => 
                base.GetFlag(0x200000);
            set => 
                base.SetFlag(0x200000, value);
        }

        public bool UseLooping
        {
            get => 
                base.GetFlag(0x100000);
            set => 
                base.SetFlag(0x100000, value);
        }

        public bool UseNoHitTestPicture
        {
            get => 
                base.GetFlag(0x80000);
            set => 
                base.SetFlag(0x80000, value);
        }

        public bool UsePictureGray
        {
            get => 
                base.GetFlag(0x40000);
            set => 
                base.SetFlag(0x40000, value);
        }

        public bool UsePictureBiLevel
        {
            get => 
                base.GetFlag(0x20000);
            set => 
                base.SetFlag(0x20000, value);
        }

        public bool UsePictureActive
        {
            get => 
                base.GetFlag(0x10000);
            set => 
                base.SetFlag(0x10000, value);
        }

        public bool PicturePreserveGrays
        {
            get => 
                base.GetFlag(0x40);
            set => 
                base.SetFlag(0x40, value);
        }

        public bool Rewind
        {
            get => 
                base.GetFlag(0x20);
            set => 
                base.SetFlag(0x20, value);
        }

        public bool Looping
        {
            get => 
                base.GetFlag(0x10);
            set => 
                base.SetFlag(0x10, value);
        }

        public bool NoHitTestPicture
        {
            get => 
                base.GetFlag(8);
            set => 
                base.SetFlag(8, value);
        }

        public bool PictureGray
        {
            get => 
                base.GetFlag(4);
            set => 
                base.SetFlag(4, value);
        }

        public bool PictureBiLevel
        {
            get => 
                base.GetFlag(2);
            set => 
                base.SetFlag(2, value);
        }

        public bool PictureActive
        {
            get => 
                base.GetFlag(1);
            set => 
                base.SetFlag(1, value);
        }
    }
}

