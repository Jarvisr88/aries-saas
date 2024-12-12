namespace DevExpress.Emf
{
    using System;
    using System.IO;

    public abstract class EmfPlusRecord : EmfRecord
    {
        private const short emfPlusObjectIdMask = 0xff;
        protected const short CompressedFlagMask = 0x4000;
        protected const short SpecifiedColorFlagMask = -32768;
        private readonly short flags;

        protected EmfPlusRecord(short flags)
        {
            this.flags = flags;
        }

        public static EmfPlusRecord Create(EmfPlusRecordType type, short flags, byte[] content)
        {
            EmfPlusRecord record;
            using (EmfPlusReader reader = new EmfPlusReader(new MemoryStream(content)))
            {
                switch (type)
                {
                    case EmfPlusRecordType.EmfPlusHeader:
                        record = new EmfPlusHeaderRecord(flags, reader);
                        break;

                    case EmfPlusRecordType.EmfPlusEndOfFile:
                        record = new EmfPlusEofRecord(flags);
                        break;

                    case EmfPlusRecordType.EmfPlusGetDC:
                        record = new EmfPlusGetDCRecord(flags);
                        break;

                    case EmfPlusRecordType.EmfPlusMultiFormatStart:
                    case EmfPlusRecordType.EmfPlusMultiFormatSection:
                    case EmfPlusRecordType.EmfPlusMultiFormatEnd:
                    case EmfPlusRecordType.EmfPlusSetAntiAliasMode:
                    case EmfPlusRecordType.EmfPlusSetTextRenderingHint:
                    case EmfPlusRecordType.EmfPlusSetTextContrast:
                    case EmfPlusRecordType.EmfPlusSetInterpolationMode:
                    case EmfPlusRecordType.EmfPlusSetPixelOffsetMode:
                    case EmfPlusRecordType.EmfPlusSetCompositingQuality:
                    case EmfPlusRecordType.EmfPlusBeginContainer:
                    case EmfPlusRecordType.EmfPlusBeginContainerNoParams:
                    case EmfPlusRecordType.EmfPlusEndContainer:
                        record = new EmfPlusUnusedRecord();
                        break;

                    case EmfPlusRecordType.EmfPlusObject:
                        record = EmfPlusObjectRecordBase.Create(flags, reader);
                        break;

                    case EmfPlusRecordType.EmfPlusClear:
                        record = new EmfPlusClearRecord(flags, reader);
                        break;

                    case EmfPlusRecordType.EmfPlusFillRects:
                        record = new EmfPlusFillRectsRecord(flags, reader);
                        break;

                    case EmfPlusRecordType.EmfPlusDrawRects:
                        record = new EmfPlusDrawRectsRecord(flags, reader);
                        break;

                    case EmfPlusRecordType.EmfPlusFillPolygon:
                        record = new EmfPlusFillPolygonRecord(flags, reader);
                        break;

                    case EmfPlusRecordType.EmfPlusDrawLines:
                        record = new EmfPlusDrawLinesRecord(flags, reader);
                        break;

                    case EmfPlusRecordType.EmfPlusFillEllipse:
                        record = new EmfPlusFillEllipseRecord(flags, reader);
                        break;

                    case EmfPlusRecordType.EmfPlusDrawEllipse:
                        record = new EmfPlusDrawEllipseRecord(flags, reader);
                        break;

                    case EmfPlusRecordType.EmfPlusFillPie:
                        record = new EmfPlusFillPieRecord(flags, reader);
                        break;

                    case EmfPlusRecordType.EmfPlusDrawPie:
                        record = new EmfPlusDrawPieRecord(flags, reader);
                        break;

                    case EmfPlusRecordType.EmfPlusDrawArc:
                        record = new EmfPlusDrawArcRecord(flags, reader);
                        break;

                    case EmfPlusRecordType.EmfPlusFillPath:
                        record = new EmfPlusFillPathRecord(flags, reader);
                        break;

                    case EmfPlusRecordType.EmfPlusDrawPath:
                        record = new EmfPlusDrawPathRecord(flags, reader);
                        break;

                    case EmfPlusRecordType.EmfPlusDrawBeziers:
                        record = new EmfPlusDrawBeziersRecord(flags, reader);
                        break;

                    case EmfPlusRecordType.EmfPlusDrawImage:
                        record = new EmfPlusDrawImageRecord(flags, reader);
                        break;

                    case EmfPlusRecordType.EmfPlusDrawImagePoints:
                        record = new EmfPlusDrawImagePointsRecord(flags, reader);
                        break;

                    case EmfPlusRecordType.EmfPlusDrawString:
                        record = new EmfPlusDrawStringRecord(flags, reader);
                        break;

                    case EmfPlusRecordType.EmfPlusSave:
                        record = new EmfPlusSaveRecord(flags, reader);
                        break;

                    case EmfPlusRecordType.EmfPlusRestore:
                        record = new EmfPlusRestoreRecord(flags, reader);
                        break;

                    case EmfPlusRecordType.EmfPlusSetWorldTransform:
                        record = new EmfPlusSetWorldTransformRecord(flags, reader);
                        break;

                    case EmfPlusRecordType.EmfPlusResetWorldTransform:
                        record = new EmfPlusResetWorldTransformRecord(flags, reader);
                        break;

                    case EmfPlusRecordType.EmfPlusMultiplyWorldTransform:
                        record = new EmfPlusMultiplyWorldTransformRecord(flags, reader);
                        break;

                    case EmfPlusRecordType.EmfPlusTranslateWorldTransform:
                        record = new EmfPlusTranslateWorldTransformRecord(flags, reader);
                        break;

                    case EmfPlusRecordType.EmfPlusScaleWorldTransform:
                        record = new EmfPlusScaleWorldTransformRecord(flags, reader);
                        break;

                    case EmfPlusRecordType.EmfPlusRotateWorldTransform:
                        record = new EmfPlusRotateWorldTransformRecord(flags, reader);
                        break;

                    case EmfPlusRecordType.EmfPlusSetPageTransform:
                        record = new EmfPlusSetPageTransformRecord(flags, reader);
                        break;

                    case EmfPlusRecordType.EmfPlusResetClip:
                        record = new EmfPlusResetClipRecord(flags, reader);
                        break;

                    case EmfPlusRecordType.EmfPlusSetClipRect:
                        record = new EmfPlusSetClipRectRecord(flags, reader);
                        break;

                    case EmfPlusRecordType.EmfPlusSetClipPath:
                        record = new EmfPlusSetClipPathRecord(flags);
                        break;

                    case EmfPlusRecordType.EmfPlusSetClipRegion:
                        record = new EmfPlusSetClipRegionRecord(flags);
                        break;

                    case EmfPlusRecordType.EmfPlusDrawDriverString:
                        record = new EmfPlusDrawDriverStringRecord(flags, reader);
                        break;

                    default:
                        record = null;
                        break;
                }
            }
            return record;
        }

        protected static int GetPadding(int size)
        {
            int num = 4 - (size % 4);
            return ((num != 4) ? num : 0);
        }

        public override void Write(EmfContentWriter writer)
        {
            writer.Write((short) this.Type);
            writer.Write(this.flags);
            int dataSize = this.DataSize;
            writer.Write(this.RecordSize);
            writer.Write(dataSize);
        }

        protected short Flags =>
            this.flags;

        public int ObjectId =>
            this.flags & 0xff;

        public int RecordSize =>
            this.DataSize + 12;

        protected virtual EmfPlusRecordType Type =>
            EmfPlusRecordType.EmfPlusComment;

        protected virtual int DataSize =>
            0;
    }
}

