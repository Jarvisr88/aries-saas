namespace DevExpress.Office.Import.OpenXml
{
    using System;
    using System.Runtime.CompilerServices;

    public class VmlFormulaNamedValues
    {
        public VmlFormulaNamedValues()
        {
            this.AdjustValues = new int?[8];
        }

        public int?[] AdjustValues { get; private set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public int CenterX { get; set; }

        public int CenterY { get; set; }

        public int LimoX { get; set; }

        public int LimoY { get; set; }

        public bool LineDrawn { get; set; }

        public int PixelLineWidth { get; set; }

        public int PixelWidth { get; set; }

        public int PixelHeight { get; set; }

        public int EmuWidth { get; set; }

        public int EmuHeight { get; set; }

        public int EmuWidth2 { get; set; }

        public int EmuHeight2 { get; set; }

        public int CoordOriginX { get; set; }

        public int CoordOriginY { get; set; }
    }
}

