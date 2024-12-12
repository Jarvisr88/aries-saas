namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public class PdfType1FontCompactFontPrivateData : PdfType1FontPrivateData
    {
        public const int DefaultBlueFuzz = 1;
        public const double DefaultDefaultWidthX = 0.0;
        public const double DefaultNominalWidthX = 0.0;
        private IList<byte[]> subrs;
        private double defaultWidthX;
        private double nominalWidthX;

        public PdfType1FontCompactFontPrivateData()
        {
            base.BlueFuzz = 1;
            base.Validate();
        }

        protected internal override bool Patch()
        {
            if (this.subrs == null)
            {
                return false;
            }
            int count = this.subrs.Count;
            if (count == 0)
            {
                this.subrs = null;
                return true;
            }
            bool flag = false;
            for (int i = 0; i < count; i++)
            {
                if (this.subrs[i].Length == 0)
                {
                    this.subrs[i] = new byte[] { 11 };
                    flag = true;
                }
            }
            return flag;
        }

        public IList<byte[]> Subrs
        {
            get => 
                this.subrs;
            internal set => 
                this.subrs = value;
        }

        public double DefaultWidthX
        {
            get => 
                this.defaultWidthX;
            internal set => 
                this.defaultWidthX = value;
        }

        public double NominalWidthX
        {
            get => 
                this.nominalWidthX;
            internal set => 
                this.nominalWidthX = value;
        }
    }
}

