namespace DevExpress.Emf
{
    using System;
    using System.Collections.Generic;

    public class EmfPlusBuilder : IEmfMetafileBuilder
    {
        private const int dpi = 0x60;
        private const int emfRecordsSize = 0x7c;
        private readonly DXRectangleF bounds;
        private readonly IList<EmfPlusRecord> records = new List<EmfPlusRecord>();
        private int size;

        public EmfPlusBuilder(DXRectangleF bounds)
        {
            this.bounds = bounds;
            this.AppendRecord(new EmfPlusHeaderRecord(0x60, 0x60));
        }

        public void AppendRecord(EmfPlusRecord record)
        {
            this.size += record.RecordSize;
            this.records.Add(record);
        }

        private static int ConvertToThousandsMillimeters(int value) => 
            (int) ((((double) value) / 96.0) * 2540.0);

        public EmfMetafile GetEmfMetafile()
        {
            IList<EmfRecord> records = new List<EmfRecord>();
            EmfPlusEofRecord record = new EmfPlusEofRecord(0);
            int recordsSize = this.size + record.RecordSize;
            int x = (int) this.bounds.X;
            int y = (int) this.bounds.Y;
            int num4 = x + ((int) this.bounds.Width);
            int num5 = y + ((int) this.bounds.Height);
            EmfRectL frame = new EmfRectL(ConvertToThousandsMillimeters(x), ConvertToThousandsMillimeters(y), ConvertToThousandsMillimeters(num4), ConvertToThousandsMillimeters(num5));
            EmfRectL bounds = new EmfRectL(x, y, num4, num5);
            records.Add(new EmfMetafileHeaderRecord(bounds, frame, recordsSize + 0x7c, new EmfSizeL(0x780, 0x780), new EmfSizeL(0x1fc, 0x1fc)));
            List<EmfPlusRecord> list2 = new List<EmfPlusRecord>(this.records) {
                record
            };
            records.Add(new EmfCommentEmfPlusRecord(list2, recordsSize));
            records.Add(new EmfEofRecord());
            return new EmfMetafile(records, bounds);
        }
    }
}

