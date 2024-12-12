namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class JBIG2TextRegionHuffmanDecoderParser : JBIG2TextRegionParser<JBIG2TextRegionHuffmanDecoder>
    {
        private readonly JBIG2TextRegionHuffmanTables tables;
        private readonly int?[] sbsymcodes;

        public JBIG2TextRegionHuffmanDecoderParser(JBIG2StreamHelper streamHelper, JBIG2RegionSegmentInfo regionInfo, int flags, IList<JBIG2SymbolDictionary> sdReferred, IList<JBIG2HuffmanTableSegment> userDefinedTables) : base(streamHelper, regionInfo, flags, sdReferred)
        {
            this.tables = new JBIG2TextRegionHuffmanTables(base.HuffmanFlags, userDefinedTables);
            int count = base.Glyphs.Count;
            IList<int> preflen = ReadSBSYMCODES(count, streamHelper);
            this.sbsymcodes = JBIG2HuffmanTableBuilder.AssignPrefixCodes(preflen, count, 5);
            JBIG2HuffmanTreeBuilder builder = new JBIG2HuffmanTreeBuilder();
            for (int i = 0; i < this.sbsymcodes.Length; i++)
            {
                builder.AddRunCode(this.sbsymcodes[i], preflen[i], i);
            }
            this.<Decoder>k__BackingField = new JBIG2TextRegionHuffmanDecoder(streamHelper, this.tables, base.Sbstrips, builder.RootNode);
        }

        public JBIG2TextRegionHuffmanDecoderParser(JBIG2StreamHelper streamHelper, JBIG2TextRegionHuffmanTables tables, int refaggninst, int sdrtemplate, int[] at, List<JBIG2Image> glyphs, JBIG2RegionSegmentInfo regionInfo, int sdnuminsyms, int sdnumnewsyms) : base(refaggninst, sdrtemplate, at, glyphs, regionInfo)
        {
            int codeLen = (int) Math.Max(Math.Log((double) (sdnuminsyms + sdnumnewsyms)), 1.0);
            for (int i = 0; i < base.Glyphs.Count; i++)
            {
                this.sbsymcodes[i] = new int?(i);
            }
            JBIG2HuffmanTreeBuilder builder = new JBIG2HuffmanTreeBuilder();
            for (int j = 0; j < this.sbsymcodes.Length; j++)
            {
                builder.AddRunCode(this.sbsymcodes[j], codeLen, j);
            }
            this.<Decoder>k__BackingField = new JBIG2TextRegionHuffmanDecoder(streamHelper, tables, base.Sbstrips, builder.RootNode);
        }

        private static void AddRun(int length, int value, IList<int> list)
        {
            for (int i = 0; i < length; i++)
            {
                list.Add(value);
            }
        }

        private static IList<int> ReadSBSYMCODES(int sbnumsyms, JBIG2StreamHelper streamHelper)
        {
            List<int> preflen = new List<int>();
            PdfBitReader reader = streamHelper.CreateBitReader();
            for (int i = 0; i < 0x23; i++)
            {
                preflen.Add(reader.GetInteger(4));
            }
            int?[] nullableArray = JBIG2HuffmanTableBuilder.AssignPrefixCodes(preflen, 0x23, 4);
            JBIG2HuffmanTreeBuilder builder = new JBIG2HuffmanTreeBuilder();
            for (int j = 0; j < nullableArray.Length; j++)
            {
                builder.AddRunCode(nullableArray[j], preflen[j], j);
            }
            IHuffmanTreeNode rootNode = builder.RootNode;
            List<int> list = new List<int>();
            while (sbnumsyms != list.Count)
            {
                int? nullable = rootNode.DecodeValue(reader);
                if (nullable != null)
                {
                    switch (nullable.GetValueOrDefault())
                    {
                        case 0x20:
                        {
                            AddRun(reader.GetInteger(2) + 3, list.Last<int>(), list);
                            continue;
                        }
                        case 0x21:
                        {
                            AddRun(reader.GetInteger(3) + 3, 0, list);
                            continue;
                        }
                        case 0x22:
                        {
                            AddRun(reader.GetInteger(7) + 11, 0, list);
                            continue;
                        }
                        default:
                            break;
                    }
                }
                int? nullable2 = nullable;
                int num3 = 0x20;
                if ((nullable2.GetValueOrDefault() > num3) ? (nullable2 != null) : false)
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                list.Add(nullable.Value);
            }
            return list;
        }

        protected override void RefinementDecode(JBIG2Image ib, int dx, int dy, JBIG2Image refImage)
        {
            int num = this.Decoder.DecodeRSize();
            new JBIG2RefinementRegion(ib, dx, dy, base.Sbrtemplate, base.Sbrat, JBIG2Decoder.Create(base.StreamHelper, base.Glyphs.Count), refImage).Process();
            this.Decoder.IgnoreExtendedBits();
        }

        protected override JBIG2TextRegionHuffmanDecoder Decoder { get; }
    }
}

