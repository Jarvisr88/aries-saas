namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;
    using System.Drawing;
    using System.IO;

    public class PdfRectangle : PdfObject
    {
        private float left;
        private float bottom;
        private float right;
        private float top;

        public PdfRectangle() : this(0f, 0f, 0f, 0f)
        {
        }

        public PdfRectangle(float left, float bottom, float right, float top)
        {
            this.left = left;
            this.bottom = bottom;
            this.right = right;
            this.top = top;
        }

        private PdfArray GetArray() => 
            new PdfArray { 
                new PdfDouble((double) this.left),
                new PdfDouble((double) this.bottom),
                new PdfDouble((double) this.right),
                new PdfDouble((double) this.top)
            };

        protected override void WriteContent(StreamWriter writer)
        {
            this.GetArray().WriteToStream(writer);
        }

        public float Left
        {
            get => 
                this.left;
            set
            {
                if ((value != this.left) && (value >= 0f))
                {
                    this.left = value;
                }
            }
        }

        public float Bottom
        {
            get => 
                this.bottom;
            set
            {
                if ((value != this.bottom) && (value >= 0f))
                {
                    this.bottom = value;
                }
            }
        }

        public float Right
        {
            get => 
                this.right;
            set
            {
                if ((value != this.right) && (value >= 0f))
                {
                    this.right = value;
                }
            }
        }

        public float Top
        {
            get => 
                this.top;
            set
            {
                if ((value != this.top) && (value >= 0f))
                {
                    this.top = value;
                }
            }
        }

        public SizeF Size =>
            new SizeF(this.right - this.left, this.top - this.bottom);
    }
}

