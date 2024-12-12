namespace DevExpress.Pdf.ContentGeneration.TiffParsing
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.CompilerServices;

    public class TiffParser
    {
        private static byte[] bitReverseTable = new byte[] { 
            0, 0x80, 0x40, 0xc0, 0x20, 160, 0x60, 0xe0, 0x10, 0x90, 80, 0xd0, 0x30, 0xb0, 0x70, 240,
            8, 0x88, 0x48, 200, 40, 0xa8, 0x68, 0xe8, 0x18, 0x98, 0x58, 0xd8, 0x38, 0xb8, 120, 0xf8,
            4, 0x84, 0x44, 0xc4, 0x24, 0xa4, 100, 0xe4, 20, 0x94, 0x54, 0xd4, 0x34, 180, 0x74, 0xf4,
            12, 140, 0x4c, 0xcc, 0x2c, 0xac, 0x6c, 0xec, 0x1c, 0x9c, 0x5c, 220, 60, 0xbc, 0x7c, 0xfc,
            2, 130, 0x42, 0xc2, 0x22, 0xa2, 0x62, 0xe2, 0x12, 0x92, 0x52, 210, 50, 0xb2, 0x72, 0xf2,
            10, 0x8a, 0x4a, 0xca, 0x2a, 170, 0x6a, 0xea, 0x1a, 0x9a, 90, 0xda, 0x3a, 0xba, 0x7a, 250,
            6, 0x86, 70, 0xc6, 0x26, 0xa6, 0x66, 230, 0x16, 150, 0x56, 0xd6, 0x36, 0xb6, 0x76, 0xf6,
            14, 0x8e, 0x4e, 0xce, 0x2e, 0xae, 110, 0xee, 30, 0x9e, 0x5e, 0xde, 0x3e, 190, 0x7e, 0xfe,
            1, 0x81, 0x41, 0xc1, 0x21, 0xa1, 0x61, 0xe1, 0x11, 0x91, 0x51, 0xd1, 0x31, 0xb1, 0x71, 0xf1,
            9, 0x89, 0x49, 0xc9, 0x29, 0xa9, 0x69, 0xe9, 0x19, 0x99, 0x59, 0xd9, 0x39, 0xb9, 0x79, 0xf9,
            5, 0x85, 0x45, 0xc5, 0x25, 0xa5, 0x65, 0xe5, 0x15, 0x95, 0x55, 0xd5, 0x35, 0xb5, 0x75, 0xf5,
            13, 0x8d, 0x4d, 0xcd, 0x2d, 0xad, 0x6d, 0xed, 0x1d, 0x9d, 0x5d, 0xdd, 0x3d, 0xbd, 0x7d, 0xfd,
            3, 0x83, 0x43, 0xc3, 0x23, 0xa3, 0x63, 0xe3, 0x13, 0x93, 0x53, 0xd3, 0x33, 0xb3, 0x73, 0xf3,
            11, 0x8b, 0x4b, 0xcb, 0x2b, 0xab, 0x6b, 0xeb, 0x1b, 0x9b, 0x5b, 0xdb, 0x3b, 0xbb, 0x7b, 0xfb,
            7, 0x87, 0x47, 0xc7, 0x27, 0xa7, 0x67, 0xe7, 0x17, 0x97, 0x57, 0xd7, 0x37, 0xb7, 0x77, 0xf7,
            15, 0x8f, 0x4f, 0xcf, 0x2f, 0xaf, 0x6f, 0xef, 0x1f, 0x9f, 0x5f, 0xdf, 0x3f, 0xbf, 0x7f, 0xff
        };
        private const int byteOrderMark = 0x4949;
        private const int fillOrderMark = 2;

        public TiffParser(Stream stream)
        {
            stream.Position = 0L;
            this.Reader = (new BinaryReader(stream).ReadInt16() != 0x4949) ? ((ITiffReader) new TiffBigEndianReader(stream)) : ((ITiffReader) new TiffLittleEndianReader(stream));
            this.Reader.ReadInt16();
            this.Reader.Position = this.Reader.ReadInt32();
            short num2 = this.Reader.ReadInt16();
            for (int i = 0; i < num2; i++)
            {
                this.ParseDirectoryEntry();
            }
        }

        public ITiffValue[] GetDirectoryEntryValue(TiffTag key)
        {
            ITiffValue[] valueArray;
            return (!this.DirectoryEntryDictionary.TryGetValue(key, out valueArray) ? null : valueArray);
        }

        public ITiffValue GetFirstValue(TiffTag key)
        {
            ITiffValue[] directoryEntryValue = this.GetDirectoryEntryValue(key);
            return (((directoryEntryValue == null) || (directoryEntryValue.Length == 0)) ? null : directoryEntryValue[0]);
        }

        public byte[] GetImageData()
        {
            ITiffValue[] directoryEntryValue = this.GetDirectoryEntryValue(TiffTag.StripOffsets);
            if (directoryEntryValue == null)
            {
                return null;
            }
            ITiffValue[] valueArray2 = this.GetDirectoryEntryValue(TiffTag.StripByteCounts);
            if (valueArray2 == null)
            {
                return null;
            }
            List<byte> list = new List<byte>();
            for (int i = 0; i < directoryEntryValue.Length; i++)
            {
                this.Reader.Position = directoryEntryValue[i].AsUint();
                list.AddRange(this.Reader.ReadBytes((int) valueArray2[i].AsUint()));
            }
            byte[] buffer = list.ToArray();
            ITiffValue[] valueArray3 = this.GetDirectoryEntryValue(TiffTag.FillOrder);
            if ((valueArray3 != null) && (valueArray3[0].AsInt() == 2))
            {
                for (int j = 0; j < buffer.Length; j++)
                {
                    buffer[j] = bitReverseTable[buffer[j]];
                }
            }
            return buffer;
        }

        public int GetStripCount()
        {
            ITiffValue[] directoryEntryValue = this.GetDirectoryEntryValue(TiffTag.StripOffsets);
            return ((directoryEntryValue == null) ? -1 : directoryEntryValue.Length);
        }

        private static int? GetTypeLength(TiffType type)
        {
            switch (type)
            {
                case TiffType.Byte:
                    return 1;

                case TiffType.Ascii:
                    return null;

                case TiffType.Short:
                    return 2;

                case TiffType.Long:
                    return 4;

                case TiffType.Rational:
                    return 8;

                case TiffType.SByte:
                    return 1;

                case TiffType.Undefined:
                    return null;

                case TiffType.SShort:
                    return 2;

                case TiffType.SLong:
                    return 4;

                case TiffType.SRational:
                    return null;

                case TiffType.Float:
                    return 4;

                case TiffType.Double:
                    return 8;
            }
            return null;
        }

        private void ParseDirectoryEntry()
        {
            TiffTag key = (TiffTag) this.Reader.ReadInt16();
            TiffType type = (TiffType) this.Reader.ReadInt16();
            int? typeLength = GetTypeLength(type);
            long position = this.Reader.Position;
            if (typeLength != null)
            {
                int count = this.Reader.ReadInt32();
                if ((typeLength.Value * count) <= 4)
                {
                    this.DirectoryEntryDictionary.Add(key, this.ReadValues(count, type));
                }
                else
                {
                    int num4 = this.Reader.ReadInt32();
                    this.Reader.Position = num4;
                    this.DirectoryEntryDictionary.Add(key, this.ReadValues(count, type));
                }
            }
            this.Reader.Position = position + 8L;
        }

        private ITiffValue ReadValue(TiffType type)
        {
            switch (type)
            {
                case TiffType.Byte:
                    return new TiffSignedInt(this.Reader.ReadByte());

                case TiffType.Ascii:
                    return null;

                case TiffType.Short:
                    return new TiffUnsignedInt((long) ((ushort) this.Reader.ReadInt16()));

                case TiffType.Long:
                    return new TiffUnsignedInt((long) ((ulong) this.Reader.ReadInt32()));

                case TiffType.Rational:
                    return new TiffDouble(((double) this.Reader.ReadInt32()) / ((double) this.Reader.ReadInt32()));

                case TiffType.SByte:
                    return new TiffSignedInt((sbyte) this.Reader.ReadByte());

                case TiffType.Undefined:
                    return null;

                case TiffType.SShort:
                    return new TiffSignedInt(this.Reader.ReadInt16());

                case TiffType.SLong:
                    return new TiffSignedInt(this.Reader.ReadInt32());

                case TiffType.SRational:
                    return null;

                case TiffType.Float:
                    return new TiffDouble((double) BitConverter.ToSingle(this.Reader.ReadBytes(4), 0));

                case TiffType.Double:
                    return new TiffDouble(BitConverter.ToDouble(this.Reader.ReadBytes(8), 0));
            }
            return null;
        }

        private ITiffValue[] ReadValues(int count, TiffType type)
        {
            ITiffValue[] valueArray = new ITiffValue[count];
            for (int i = 0; i < count; i++)
            {
                valueArray[i] = this.ReadValue(type);
            }
            return valueArray;
        }

        private ITiffReader Reader { get; set; }

        public IDictionary<TiffTag, ITiffValue[]> DirectoryEntryDictionary { get; } = new Dictionary<TiffTag, ITiffValue[]>()
    }
}

