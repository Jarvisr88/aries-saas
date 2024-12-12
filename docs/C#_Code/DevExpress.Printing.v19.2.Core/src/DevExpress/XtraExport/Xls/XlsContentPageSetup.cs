namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Export.Xl;
    using DevExpress.Office.Utils;
    using System;
    using System.Drawing.Printing;
    using System.IO;
    using System.Runtime.CompilerServices;

    public class XlsContentPageSetup : XlsContentBase
    {
        private System.Drawing.Printing.PaperKind ConvertToPaperKind(int value) => 
            ((value < 0) || (value > 0x76)) ? System.Drawing.Printing.PaperKind.Custom : ((System.Drawing.Printing.PaperKind) value);

        public override int GetSize() => 
            0x22;

        public override void Read(XlReader reader, int size)
        {
            this.PaperKind = this.ConvertToPaperKind(reader.ReadInt16());
            this.Scale = reader.ReadInt16();
            this.FirstPageNumber = reader.ReadInt16();
            this.FitToWidth = reader.ReadInt16();
            this.FitToHeight = reader.ReadInt16();
            short num = reader.ReadInt16();
            bool flag = Convert.ToBoolean((int) (num & 4));
            this.PagePrintOrder = ((XlPagePrintOrder) num) & XlPagePrintOrder.OverThenDown;
            this.PageOrientation = !Convert.ToBoolean((int) (num & 2)) ? XlPageOrientation.Landscape : XlPageOrientation.Portrait;
            this.BlackAndWhite = Convert.ToBoolean((int) (num & 8));
            this.Draft = Convert.ToBoolean((int) (num & 0x10));
            bool flag2 = Convert.ToBoolean((int) (num & 0x20));
            if (Convert.ToBoolean((int) (num & 0x40)))
            {
                this.PageOrientation = XlPageOrientation.Default;
            }
            this.UseFirstPageNumber = Convert.ToBoolean((int) (num & 0x80));
            this.CommentsPrintMode = (XlCommentsPrintMode) ((num & 0x200) >> 9);
            this.ErrorsPrintMode = (XlErrorsPrintMode) ((num & 0xc00) >> 10);
            this.HorizontalDpi = reader.ReadInt16();
            this.VerticalDpi = reader.ReadInt16();
            double num2 = reader.ReadDouble();
            if ((num2 < 0.0) || (num2 >= 49.0))
            {
                num2 = 0.3;
            }
            this.HeaderMargin = num2;
            num2 = reader.ReadDouble();
            if ((num2 < 0.0) || (num2 >= 49.0))
            {
                num2 = 0.3;
            }
            this.FooterMargin = num2;
            if (!flag2)
            {
                this.CommentsPrintMode = XlCommentsPrintMode.None;
            }
            this.Copies = reader.ReadInt16();
            if (flag)
            {
                this.PaperKind = System.Drawing.Printing.PaperKind.Letter;
                this.Scale = 100;
                this.HorizontalDpi = 600;
                this.VerticalDpi = 600;
                this.Copies = 1;
                this.PageOrientation = XlPageOrientation.Default;
            }
            if (this.HorizontalDpi < 1)
            {
                this.HorizontalDpi = 600;
            }
            this.VerticalDpi ??= this.HorizontalDpi;
            if (this.VerticalDpi < 1)
            {
                this.VerticalDpi = 600;
            }
            if (this.Copies < 1)
            {
                this.Copies = 1;
            }
            if (!this.UseFirstPageNumber)
            {
                this.FirstPageNumber = 1;
            }
            if (this.FirstPageNumber < 1)
            {
                this.FirstPageNumber = 1;
            }
        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write((short) this.PaperKind);
            writer.Write((short) this.Scale);
            writer.Write((short) this.FirstPageNumber);
            writer.Write((short) this.FitToWidth);
            writer.Write((short) this.FitToHeight);
            int pagePrintOrder = (short) this.PagePrintOrder;
            if (this.PageOrientation == XlPageOrientation.Portrait)
            {
                pagePrintOrder |= 2;
            }
            if (this.BlackAndWhite)
            {
                pagePrintOrder |= 8;
            }
            if (this.Draft)
            {
                pagePrintOrder |= 0x10;
            }
            if (this.CommentsPrintMode != XlCommentsPrintMode.None)
            {
                pagePrintOrder |= 0x20;
            }
            if (this.PageOrientation == XlPageOrientation.Default)
            {
                pagePrintOrder |= 0x40;
            }
            if (this.UseFirstPageNumber)
            {
                pagePrintOrder |= 0x80;
            }
            pagePrintOrder = (pagePrintOrder | (((short) this.CommentsPrintMode) << 9)) | (((short) this.ErrorsPrintMode) << 10);
            writer.Write((short) pagePrintOrder);
            writer.Write((short) this.HorizontalDpi);
            writer.Write((short) this.VerticalDpi);
            writer.Write(this.HeaderMargin);
            writer.Write(this.FooterMargin);
            writer.Write((short) this.Copies);
        }

        public System.Drawing.Printing.PaperKind PaperKind { get; set; }

        public int Scale { get; set; }

        public int FirstPageNumber { get; set; }

        public int FitToWidth { get; set; }

        public int FitToHeight { get; set; }

        public XlPagePrintOrder PagePrintOrder { get; set; }

        public XlCommentsPrintMode CommentsPrintMode { get; set; }

        public XlErrorsPrintMode ErrorsPrintMode { get; set; }

        public XlPageOrientation PageOrientation { get; set; }

        public bool BlackAndWhite { get; set; }

        public bool Draft { get; set; }

        public bool UseFirstPageNumber { get; set; }

        public int HorizontalDpi { get; set; }

        public int VerticalDpi { get; set; }

        public double HeaderMargin { get; set; }

        public double FooterMargin { get; set; }

        public int Copies { get; set; }
    }
}

