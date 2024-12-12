namespace DevExpress.Export.Xl
{
    using System;
    using System.Drawing.Printing;
    using System.Runtime.CompilerServices;

    public class XlPageSetup
    {
        private int scale = 100;
        private int copies = 1;
        private int firstPageNumber = 1;
        private int fitToWidth = 1;
        private int fitToHeight = 1;
        private int horizontalDpi = 600;
        private int verticalDpi = 600;

        public XlPageSetup()
        {
            this.PaperKind = System.Drawing.Printing.PaperKind.Letter;
            this.CommentsPrintMode = XlCommentsPrintMode.None;
            this.AutomaticFirstPageNumber = true;
            this.UsePrinterDefaults = true;
        }

        internal bool IsDefault() => 
            (this.PaperKind == System.Drawing.Printing.PaperKind.Letter) && ((this.CommentsPrintMode == XlCommentsPrintMode.None) && ((this.ErrorsPrintMode == XlErrorsPrintMode.Displayed) && ((this.PagePrintOrder == XlPagePrintOrder.DownThenOver) && ((this.PageOrientation == XlPageOrientation.Default) && ((this.Scale == 100) && (!this.BlackAndWhite && (!this.Draft && (this.AutomaticFirstPageNumber && (this.UsePrinterDefaults && ((this.Copies == 1) && ((this.FirstPageNumber == 1) && ((this.FitToWidth == 1) && ((this.FitToHeight == 1) && ((this.HorizontalDpi == 600) && (this.VerticalDpi == 600)))))))))))))));

        public System.Drawing.Printing.PaperKind PaperKind { get; set; }

        public XlCommentsPrintMode CommentsPrintMode { get; set; }

        public XlErrorsPrintMode ErrorsPrintMode { get; set; }

        public XlPagePrintOrder PagePrintOrder { get; set; }

        public XlPageOrientation PageOrientation { get; set; }

        public int Scale
        {
            get => 
                this.scale;
            set
            {
                if ((value < 10) || (value > 400))
                {
                    throw new ArgumentOutOfRangeException("Scale out of range 10%...400%");
                }
                this.scale = value;
            }
        }

        public bool BlackAndWhite { get; set; }

        public bool Draft { get; set; }

        public bool AutomaticFirstPageNumber { get; set; }

        public bool UsePrinterDefaults { get; set; }

        public bool FitToPage { get; set; }

        public int Copies
        {
            get => 
                this.copies;
            set
            {
                if ((value < 1) || (value > 0x7fff))
                {
                    throw new ArgumentOutOfRangeException("Copies out of range 1...32767");
                }
                this.copies = value;
            }
        }

        public int FirstPageNumber
        {
            get => 
                this.firstPageNumber;
            set
            {
                if ((value < -32768) || (value > 0x7fff))
                {
                    throw new ArgumentOutOfRangeException($"FirstPageNumber out of range {(short) (-32768)}...{(short) 0x7fff}");
                }
                this.firstPageNumber = value;
            }
        }

        public int FitToWidth
        {
            get => 
                this.fitToWidth;
            set
            {
                if ((value < 0) || (value > 0x7fff))
                {
                    throw new ArgumentOutOfRangeException("FitToWidth out of range 0...32767");
                }
                this.fitToWidth = value;
            }
        }

        public int FitToHeight
        {
            get => 
                this.fitToHeight;
            set
            {
                if ((value < 0) || (value > 0x7fff))
                {
                    throw new ArgumentOutOfRangeException("FitToHeight out of range 0...32767");
                }
                this.fitToHeight = value;
            }
        }

        public int HorizontalDpi
        {
            get => 
                this.horizontalDpi;
            set
            {
                if ((value < 1) || (value > 0xffff))
                {
                    throw new ArgumentOutOfRangeException("HorizontalDpi out of range 1...65535");
                }
                this.horizontalDpi = value;
            }
        }

        public int VerticalDpi
        {
            get => 
                this.verticalDpi;
            set
            {
                if ((value < 1) || (value > 0xffff))
                {
                    throw new ArgumentOutOfRangeException("VerticalDpi out of range 1...65535");
                }
                this.verticalDpi = value;
            }
        }
    }
}

