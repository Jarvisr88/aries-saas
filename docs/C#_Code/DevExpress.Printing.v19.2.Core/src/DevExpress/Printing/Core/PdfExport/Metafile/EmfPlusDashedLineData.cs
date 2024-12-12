namespace DevExpress.Printing.Core.PdfExport.Metafile
{
    using System;
    using System.Runtime.CompilerServices;

    public class EmfPlusDashedLineData
    {
        public EmfPlusDashedLineData(MetaReader reader)
        {
            uint num = reader.ReadUInt32();
            this.DashPattern = new float[num];
            for (int i = 0; i < num; i++)
            {
                this.DashPattern[i] = reader.ReadSingle();
            }
        }

        public float[] DashPattern { get; set; }
    }
}

