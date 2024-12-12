namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public class PdfCompactFontFormatPrivateDictIndexWriter
    {
        private readonly List<PdfCompactFontFormatDictIndexOperator> operators = new List<PdfCompactFontFormatDictIndexOperator>();
        private readonly PdfCompactFontFormatPrivateDictIndexSubrsOperator subrsOperator;
        private int dataLength;

        public PdfCompactFontFormatPrivateDictIndexWriter(PdfType1FontPrivateData data)
        {
            PdfType1FontGlyphZone[] blueValues = data.BlueValues;
            if (blueValues != null)
            {
                this.operators.Add(new PdfCompactFontFormatPrivateDictIndexBlueValuesOperator(blueValues));
            }
            blueValues = data.OtherBlues;
            if (blueValues != null)
            {
                this.operators.Add(new PdfCompactFontFormatPrivateDictIndexOtherBluesOperator(blueValues));
            }
            blueValues = data.FamilyBlues;
            if (blueValues != null)
            {
                this.operators.Add(new PdfCompactFontFormatPrivateDictIndexFamilyBluesOperator(blueValues));
            }
            blueValues = data.FamilyOtherBlues;
            if (blueValues != null)
            {
                this.operators.Add(new PdfCompactFontFormatPrivateDictIndexFamilyOtherBluesOperator(blueValues));
            }
            double blueScale = data.BlueScale;
            if (blueScale != 0.039625)
            {
                this.operators.Add(new PdfCompactFontFormatPrivateDictIndexBlueScaleOperator(blueScale));
            }
            blueScale = data.BlueShift;
            if (blueScale != 7.0)
            {
                this.operators.Add(new PdfCompactFontFormatPrivateDictIndexBlueShiftOperator(blueScale));
            }
            int blueFuzz = data.BlueFuzz;
            if (blueFuzz != 1)
            {
                this.operators.Add(new PdfCompactFontFormatPrivateDictIndexBlueFuzzOperator(blueFuzz));
            }
            double? stdHW = data.StdHW;
            if (stdHW != null)
            {
                this.operators.Add(new PdfCompactFontFormatPrivateDictIndexStdHWOperator(stdHW.Value));
            }
            stdHW = data.StdVW;
            if (stdHW != null)
            {
                this.operators.Add(new PdfCompactFontFormatPrivateDictIndexStdVWOperator(stdHW.Value));
            }
            double[] stemSnapH = data.StemSnapH;
            if (stemSnapH != null)
            {
                this.operators.Add(new PdfCompactFontFormatPrivateDictIndexStemSnapHOperator(stemSnapH));
            }
            stemSnapH = data.StemSnapV;
            if (stemSnapH != null)
            {
                this.operators.Add(new PdfCompactFontFormatPrivateDictIndexStemSnapVOperator(stemSnapH));
            }
            bool forceBold = data.ForceBold;
            if (forceBold)
            {
                this.operators.Add(new PdfCompactFontFormatPrivateDictIndexForceBoldOperator(forceBold));
            }
            stdHW = data.ForceBoldThreshold;
            if (stdHW != null)
            {
                this.operators.Add(new PdfCompactFontFormatPrivateDictIndexForceBoldThresholdOperator(stdHW.Value));
            }
            PdfType1FontCompactFontPrivateData data2 = data as PdfType1FontCompactFontPrivateData;
            if (data2 != null)
            {
                blueFuzz = data2.LanguageGroup;
                if (blueFuzz != 0)
                {
                    this.operators.Add(new PdfCompactFontFormatPrivateDictIndexLanguageGroupOperator(blueFuzz));
                }
                blueScale = data2.ExpansionFactor;
                if (blueScale != 0.06)
                {
                    this.operators.Add(new PdfCompactFontFormatPrivateDictIndexExpansionFactorOperator(blueScale));
                }
                IList<byte[]> subrs = data2.Subrs;
                if (subrs == null)
                {
                    this.subrsOperator = null;
                }
                else
                {
                    this.subrsOperator = new PdfCompactFontFormatPrivateDictIndexSubrsOperator(subrs);
                    this.operators.Add(this.subrsOperator);
                }
                blueScale = data2.DefaultWidthX;
                if (blueScale != 0.0)
                {
                    this.operators.Add(new PdfCompactFontFormatPrivateDictIndexDefaultWidthXOperator(blueScale));
                }
                blueScale = data2.NominalWidthX;
                if (blueScale != 0.0)
                {
                    this.operators.Add(new PdfCompactFontFormatPrivateDictIndexNominalWidthXOperator(blueScale));
                }
            }
            if (this.subrsOperator == null)
            {
                this.CalcDataLength();
            }
            else
            {
                while (true)
                {
                    this.CalcDataLength();
                    if (!this.subrsOperator.UpdateOffset(this.dataLength))
                    {
                        return;
                    }
                }
            }
        }

        private void CalcDataLength()
        {
            this.dataLength = 0;
            foreach (PdfCompactFontFormatDictIndexOperator @operator in this.operators)
            {
                this.dataLength += @operator.GetSize(null);
            }
        }

        public void Write(PdfBinaryStream stream)
        {
            foreach (PdfCompactFontFormatDictIndexOperator @operator in this.operators)
            {
                @operator.Write(null, stream);
            }
            if (this.subrsOperator != null)
            {
                this.subrsOperator.WriteData(stream);
            }
        }

        public int DataLength =>
            this.dataLength;

        public int SubrsLength =>
            (this.subrsOperator == null) ? 0 : this.subrsOperator.DataLength;
    }
}

