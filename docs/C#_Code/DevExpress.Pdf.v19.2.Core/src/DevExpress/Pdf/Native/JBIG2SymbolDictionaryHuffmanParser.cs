namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class JBIG2SymbolDictionaryHuffmanParser : JBIG2SymbolDictionaryParser<JBIG2SymbolDictionaryHuffmanDecoder>
    {
        private readonly int bitmapUsed;
        private readonly int bitmapRetained;
        private List<int> sdnewsymwidths;

        public JBIG2SymbolDictionaryHuffmanParser(JBIG2StreamHelper streamHelper, IList<JBIG2SymbolDictionary> sdReferred, JBIG2SegmentHeader segmentHeader, int flags, IList<JBIG2HuffmanTableSegment> userDefinedTables) : base(streamHelper, sdReferred, segmentHeader, flags)
        {
            this.sdnewsymwidths = new List<int>();
            this.sdnewsymwidths = new List<int>(base.Sdnumnewsyms);
            IHuffmanTreeNode sdhuffdh = null;
            IHuffmanTreeNode sdhuffdw = null;
            IHuffmanTreeNode sdhuffbmsize = null;
            IHuffmanTreeNode sdhuffagginst = null;
            JBIG2UserHuffmanTablesEnumerator enumerator = new JBIG2UserHuffmanTablesEnumerator(userDefinedTables);
            switch (((flags >> 2) & 3))
            {
                case 0:
                    sdhuffdh = JBIG2HuffmanTableModel.StandardHuffmanTableD();
                    break;

                case 1:
                    sdhuffdh = JBIG2HuffmanTableModel.StandardHuffmanTableE();
                    break;

                case 3:
                    sdhuffdh = enumerator.GetNext();
                    break;

                default:
                    break;
            }
            switch (((flags >> 4) & 3))
            {
                case 0:
                    sdhuffdw = JBIG2HuffmanTableModel.StandardHuffmanTableB();
                    break;

                case 1:
                    sdhuffdw = JBIG2HuffmanTableModel.StandardHuffmanTableC();
                    break;

                case 3:
                    sdhuffdw = enumerator.GetNext();
                    break;

                default:
                    break;
            }
            sdhuffbmsize = (((flags >> 6) & 1) != 0) ? enumerator.GetNext() : JBIG2HuffmanTableModel.StandardHuffmanTableA();
            sdhuffagginst = (((flags >> 7) & 1) != 0) ? enumerator.GetNext() : JBIG2HuffmanTableModel.StandardHuffmanTableA();
            this.bitmapUsed = (flags >> 8) & 1;
            this.bitmapRetained = (flags >> 9) & 1;
            if (((flags & 2) == 0) && ((this.bitmapUsed != 0) || (this.bitmapRetained != 0)))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            this.<Decoder>k__BackingField = new JBIG2SymbolDictionaryHuffmanDecoder(base.StreamHelper, sdhuffdh, sdhuffdw, sdhuffbmsize, sdhuffagginst);
        }

        protected override void DecodeNonRefaggGLyph(int symWidth, int hcheight)
        {
            this.sdnewsymwidths.Add(symWidth);
        }

        protected override void DecodeRefaggNonRefaggninstGlyph(int symWidth, int hcheight, int refaggninst)
        {
            JBIG2Image image = new JBIG2Image(symWidth, hcheight);
            JBIG2TextRegionHuffmanTables tables = new JBIG2TextRegionHuffmanTables(JBIG2HuffmanTableModel.StandardHuffmanTableF(), JBIG2HuffmanTableModel.StandardHuffmanTableH(), JBIG2HuffmanTableModel.StandardHuffmanTableK(), JBIG2HuffmanTableModel.StandardHuffmanTableO(), JBIG2HuffmanTableModel.StandardHuffmanTableO(), JBIG2HuffmanTableModel.StandardHuffmanTableO(), JBIG2HuffmanTableModel.StandardHuffmanTableO(), JBIG2HuffmanTableModel.StandardHuffmanTableA());
            new JBIG2TextRegion(base.StreamHelper, tables, refaggninst, base.Sdrtemplate, base.Sdrat, base.Syms, image, base.Sdnuminsyms, base.Sdnumnewsyms).Process();
            base.Syms.Add(image);
        }

        protected override void DecodeRefaggRefaggninstGlyph(int symWidth, int hcheight)
        {
            JBIG2Image image = new JBIG2Image(symWidth, hcheight);
            JBIG2TextRegionHuffmanTables huffmanTables = new JBIG2TextRegionHuffmanTables(null, null, null, null, null, JBIG2HuffmanTableModel.StandardHuffmanTableO(), JBIG2HuffmanTableModel.StandardHuffmanTableO(), JBIG2HuffmanTableModel.StandardHuffmanTableA());
            int codeLen = (int) Math.Max(Math.Log((double) (base.Sdnuminsyms + base.Sdnumnewsyms)), 1.0);
            int num2 = base.Sdnuminsyms + base.Syms.Count;
            int?[] nullableArray = new int?[num2];
            for (int i = 0; i < num2; i++)
            {
                nullableArray[i] = new int?(i);
            }
            JBIG2HuffmanTreeBuilder builder = new JBIG2HuffmanTreeBuilder();
            for (int j = 0; j < nullableArray.Length; j++)
            {
                builder.AddRunCode(nullableArray[j], codeLen, j);
            }
            JBIG2TextRegionHuffmanDecoder decoder = new JBIG2TextRegionHuffmanDecoder(base.StreamHelper, huffmanTables, 0, builder.RootNode);
            int num3 = decoder.DecodeID();
            int dx = decoder.DecodeRDX();
            int dy = decoder.DecodeRDY();
            int num6 = decoder.DecodeRSize();
            if (num3 >= base.Syms.Count)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            new JBIG2RefinementRegion(base.Syms[num3], dx, dy, base.Sdrtemplate, base.Sdrat, JBIG2Decoder.Create(base.StreamHelper, base.Syms.Count), image).Process();
            decoder.IgnoreExtendedBits();
            base.Syms.Add(image);
        }

        protected override void ProcessStripBitmap(int hcheight)
        {
            int columns = ((IEnumerable<int>) this.sdnewsymwidths).Sum();
            int count = this.Decoder.DecodeBmsize();
            int num3 = (int) Math.Ceiling((double) (((double) columns) / 8.0));
            byte[] src = (count == 0) ? this.Decoder.ReadBytes(hcheight * num3) : PdfCCITTFaxDecoder.Decode(new PdfCCITTFaxDecoderParameters(true, false, 0, columns, hcheight, PdfCCITTFaxEncodingScheme.TwoDimensional), this.Decoder.ReadBytes(count));
            int num4 = 0;
            foreach (int num5 in this.sdnewsymwidths)
            {
                JBIG2Image item = new JBIG2Image(num5, hcheight);
                int num6 = 0;
                while (true)
                {
                    if (num6 >= hcheight)
                    {
                        base.Syms.Add(item);
                        num4 += num5;
                        break;
                    }
                    int srcOffset = (num3 * num6) + (num4 / 8);
                    int dstOffset = item.Stride * num6;
                    if ((num4 % 8) == 0)
                    {
                        Buffer.BlockCopy(src, srcOffset, item.Data, dstOffset, item.Stride);
                    }
                    else
                    {
                        int num9 = num4 % 8;
                        byte num10 = src[srcOffset];
                        int num11 = (1 << ((8 - num9) & 0x1f)) - 1;
                        int num12 = ~num11;
                        for (int i = 0; i < ((int) Math.Ceiling((double) (((double) num5) / 8.0))); i++)
                        {
                            int num14 = (num10 & num11) << (num9 & 0x1f);
                            int index = (srcOffset + i) + 1;
                            num10 = (index >= src.Length) ? 0xff : src[index];
                            item.Data[dstOffset + i] = (byte) (num14 | ((num10 & num12) >> ((8 - num9) & 0x1f)));
                        }
                    }
                    num6++;
                }
            }
            this.sdnewsymwidths.Clear();
        }

        protected override JBIG2SymbolDictionaryHuffmanDecoder Decoder { get; }
    }
}

