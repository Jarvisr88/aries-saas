namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public abstract class JBIG2SegmentData
    {
        private readonly JBIG2SegmentHeader header;
        private readonly JBIG2Image image;
        private JBIG2StreamHelper streamHelper;

        protected JBIG2SegmentData(JBIG2Image image)
        {
            this.image = image;
        }

        protected JBIG2SegmentData(JBIG2StreamHelper streamHelper, JBIG2SegmentHeader header, JBIG2Image image) : this(image)
        {
            this.header = header;
            this.streamHelper = streamHelper;
            if (this.CacheData)
            {
                this.DoCacheData(header.DataLength);
            }
        }

        public static JBIG2SegmentData Create(JBIG2StreamHelper streamHelper, JBIG2SegmentHeader header, JBIG2Image image)
        {
            int num = header.Flags & 0x3f;
            if (num <= 0x10)
            {
                if (num == 0)
                {
                    return new JBIG2SymbolDictionary(streamHelper, header, image);
                }
                switch (num)
                {
                    case 4:
                    case 6:
                    case 7:
                        return new JBIG2TextRegion(streamHelper, header, image);

                    case 5:
                        break;

                    default:
                        if (num != 0x10)
                        {
                            break;
                        }
                        return new JBIG2PatternDictionary(streamHelper, header, image);
                }
            }
            else
            {
                switch (num)
                {
                    case 20:
                    case 0x16:
                    case 0x17:
                        return new JBIG2HalftoneRegion(streamHelper, header, image);

                    case 0x15:
                        break;

                    default:
                        switch (num)
                        {
                            case 0x24:
                            case 0x25:
                            case 0x29:
                            case 0x2c:
                            case 0x2d:
                            case 0x2e:
                            case 0x2f:
                            case 0x31:
                            case 50:
                            case 0x33:
                            case 0x34:
                                break;

                            case 0x26:
                            case 0x27:
                                return new JBIG2GenericRegion(streamHelper, header, image);

                            case 40:
                            case 0x2a:
                            case 0x2b:
                                return new JBIG2RefinementRegion(streamHelper, header, image);

                            case 0x30:
                                return new JBIG2PageInfo(streamHelper, header, image);

                            case 0x35:
                                return new JBIG2HuffmanTableSegment(streamHelper, header, image);

                            default:
                                if (num != 0x3e)
                                {
                                }
                                break;
                        }
                        break;
                }
            }
            return new JBIG2UnknownSegment(streamHelper, header, image);
        }

        protected void DoCacheData(int dataLength)
        {
            this.streamHelper = this.streamHelper.ReadData((long) dataLength);
        }

        private List<T> GetReferredSegment<T>() where T: JBIG2SegmentData
        {
            List<T> list = new List<T>();
            for (int i = 0; i < this.Header.ReferredToSegments.Count; i++)
            {
                JBIG2SegmentHeader header;
                if (!this.Image.Segments.TryGetValue(this.Header.ReferredToSegments[i], out header))
                {
                    this.Image.GlobalSegments.TryGetValue(this.Header.ReferredToSegments[i], out header);
                }
                if (header != null)
                {
                    T data = header.Data as T;
                    if (data != null)
                    {
                        list.Add(data);
                    }
                }
            }
            return list;
        }

        protected List<JBIG2SymbolDictionary> GetSDReferred() => 
            this.GetReferredSegment<JBIG2SymbolDictionary>();

        protected List<JBIG2HuffmanTableSegment> GetUserDefinedHuffmanTables() => 
            this.GetReferredSegment<JBIG2HuffmanTableSegment>();

        public virtual void Process()
        {
        }

        public JBIG2SegmentHeader Header =>
            this.header;

        public JBIG2Image Image =>
            this.image;

        protected JBIG2StreamHelper StreamHelper =>
            this.streamHelper;

        protected virtual bool CacheData =>
            true;
    }
}

