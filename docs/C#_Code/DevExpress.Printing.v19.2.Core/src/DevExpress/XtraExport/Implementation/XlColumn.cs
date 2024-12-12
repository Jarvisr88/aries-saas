namespace DevExpress.XtraExport.Implementation
{
    using DevExpress.Export.Xl;
    using System;
    using System.Runtime.CompilerServices;

    public class XlColumn : IXlColumn, IDisposable
    {
        private XlSheet sheet;
        private int widthInPixels = -1;

        public XlColumn(XlSheet sheet)
        {
            this.sheet = sheet;
        }

        public void ApplyFormatting(XlCellFormatting formatting)
        {
            if (formatting != null)
            {
                this.Formatting ??= new XlCellFormatting();
                this.Formatting.MergeWith(formatting);
            }
        }

        public void Dispose()
        {
            this.sheet = null;
        }

        public int ColumnIndex { get; set; }

        public XlCellFormatting Formatting { get; set; }

        public bool IsHidden { get; set; }

        public bool IsCollapsed { get; set; }

        public int OutlineLevel { get; set; }

        public int WidthInPixels
        {
            get => 
                this.widthInPixels;
            set
            {
                if (value < 0)
                {
                    value = -1;
                }
                this.widthInPixels = value;
            }
        }

        public float WidthInCharacters
        {
            get
            {
                if (this.widthInPixels < 0)
                {
                    return -0.08f;
                }
                float num = ColumnWidthConverter.PixelsToCharactersWidth((float) this.widthInPixels, this.sheet.DefaultMaxDigitWidthInPixels);
                return ((num < 1.71) ? ColumnWidthConverter.PixelsToCharactersWidth((float) (this.widthInPixels - (((double) (5f * num)) / 1.71)), this.sheet.DefaultMaxDigitWidthInPixels) : ColumnWidthConverter.PixelsToCharactersWidth((float) (this.widthInPixels - 5), this.sheet.DefaultMaxDigitWidthInPixels));
            }
            set
            {
                if (value < 0f)
                {
                    this.widthInPixels = -1;
                }
                else
                {
                    this.widthInPixels = ColumnWidthConverter.CharactersWidthToPixels(value, this.sheet.DefaultMaxDigitWidthInPixels);
                }
            }
        }
    }
}

