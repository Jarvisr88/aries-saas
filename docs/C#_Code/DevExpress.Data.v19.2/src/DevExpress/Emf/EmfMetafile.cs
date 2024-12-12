namespace DevExpress.Emf
{
    using System;
    using System.Collections.Generic;

    public class EmfMetafile
    {
        private readonly IList<EmfRecord> records;
        private readonly bool isVideoDisplay;
        private readonly EmfRectL bounds;

        public EmfMetafile(IList<EmfRecord> records, EmfRectL bounds) : this(records, true, bounds)
        {
        }

        private EmfMetafile(IList<EmfRecord> records, bool isVideoDisplay, EmfRectL bounds)
        {
            this.records = records;
            this.isVideoDisplay = isVideoDisplay;
            this.bounds = bounds;
        }

        public static EmfMetafile Create(byte[] data)
        {
            using (IEnumerator<EmfRecord> enumerator = new EmfMetafileParser(data).GetEnumerator())
            {
                if (enumerator.MoveNext())
                {
                    EmfMetafileHeaderRecord current = enumerator.Current as EmfMetafileHeaderRecord;
                    if ((current != null) && enumerator.MoveNext())
                    {
                        EmfCommentEmfPlusRecord record2 = enumerator.Current as EmfCommentEmfPlusRecord;
                        if ((record2 != null) && (record2.Records.Count > 0))
                        {
                            EmfPlusHeaderRecord record3 = record2.Records[0] as EmfPlusHeaderRecord;
                            if (record3 != null)
                            {
                                EmfMetafile metafile;
                                IList<EmfRecord> records = new List<EmfRecord>(current.RecordsCount) {
                                    current,
                                    record2
                                };
                                while (true)
                                {
                                    if (!enumerator.MoveNext())
                                    {
                                        metafile = new EmfMetafile(records, record3.IsVideoDisplay, current.Bounds);
                                    }
                                    else
                                    {
                                        EmfRecord item = enumerator.Current;
                                        if (item != null)
                                        {
                                            records.Add(item);
                                            continue;
                                        }
                                        metafile = null;
                                    }
                                    break;
                                }
                                return metafile;
                            }
                        }
                    }
                }
            }
            return null;
        }

        public IList<EmfRecord> Records =>
            this.records;

        public bool IsVideoDisplay =>
            this.isVideoDisplay;

        public EmfRectL Bounds =>
            this.bounds;
    }
}

