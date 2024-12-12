namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Office.Utils;
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;

    public class XlsContentWorkbookBool : XlsContentBase
    {
        public override int GetSize() => 
            2;

        public override void Read(XlReader reader, int size)
        {
            ushort num = reader.ReadUInt16();
            this.NotSaveExternalLinksValues = (num & 1) != 0;
            this.HasEnvelope = (num & 4) != 0;
            this.EnvelopeVisible = (num & 8) != 0;
            this.EnvelopeInitDone = (num & 0x10) != 0;
            this.UpdateLinksMode = (XlsUpdateLinksMode) ((num & 0x60) >> 5);
            this.HideBordersOfUnselTables = (num & 0x100) != 0;
        }

        public override void Write(BinaryWriter writer)
        {
            ushort num = 0;
            if (this.NotSaveExternalLinksValues)
            {
                num = (ushort) (num | 1);
            }
            if (this.HasEnvelope || (this.EnvelopeVisible || this.EnvelopeInitDone))
            {
                num = (ushort) (num | 4);
            }
            if (this.EnvelopeVisible)
            {
                num = (ushort) (num | 8);
            }
            if (this.EnvelopeInitDone)
            {
                num = (ushort) (num | 0x10);
            }
            num = (ushort) (num | ((ushort) (((int) this.UpdateLinksMode) << 5)));
            if (this.HideBordersOfUnselTables)
            {
                num = (ushort) (num | 0x100);
            }
            writer.Write(num);
        }

        public bool NotSaveExternalLinksValues { get; set; }

        public bool HasEnvelope { get; set; }

        public bool EnvelopeVisible { get; set; }

        public bool EnvelopeInitDone { get; set; }

        public XlsUpdateLinksMode UpdateLinksMode { get; set; }

        public bool HideBordersOfUnselTables { get; set; }
    }
}

