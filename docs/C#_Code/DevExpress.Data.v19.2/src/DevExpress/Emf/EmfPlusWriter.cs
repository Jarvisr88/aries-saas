namespace DevExpress.Emf
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class EmfPlusWriter
    {
        private readonly IList<EmfRecord> metafileRecords;

        public EmfPlusWriter(EmfMetafile metafile)
        {
            this.metafileRecords = metafile.Records;
        }

        public void Write(Stream stream)
        {
            EmfContentWriter writer = new EmfContentWriter(stream);
            int count = this.metafileRecords.Count;
            foreach (EmfRecord record in this.metafileRecords)
            {
                record.Write(writer);
            }
        }
    }
}

