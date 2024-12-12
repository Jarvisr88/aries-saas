namespace DevExpress.Printing.Core.PdfExport.Metafile
{
    using System;
    using System.Runtime.CompilerServices;

    public class EmfPlusCompoundLineData
    {
        public EmfPlusCompoundLineData(MetaReader reader)
        {
            uint num = reader.ReadUInt32();
            this.CompoundLineData = new float[num];
            for (int i = 0; i < num; i++)
            {
                this.CompoundLineData[i] = reader.ReadSingle();
            }
        }

        public float[] CompoundLineData { get; set; }
    }
}

