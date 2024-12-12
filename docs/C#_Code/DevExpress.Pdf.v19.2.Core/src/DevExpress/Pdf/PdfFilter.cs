namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public abstract class PdfFilter
    {
        protected PdfFilter()
        {
        }

        internal static PdfFilter Create(string name, PdfReaderDictionary parameters)
        {
            uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
            if (num > 0x5cc248eb)
            {
                if (num > 0x810503fb)
                {
                    if (num > 0xa2aa4b86)
                    {
                        if (num == 0xc24c20dc)
                        {
                            if (name == "LZW")
                            {
                                goto TR_0008;
                            }
                        }
                        else if (num == 0xe926fcbf)
                        {
                            if (name == "Crypt")
                            {
                                return new PdfCryptFilter();
                            }
                        }
                        else if ((num == 0xf1b0efe8) && (name == "RunLengthDecode"))
                        {
                            goto TR_001B;
                        }
                    }
                    else if (num == 0x896ade5c)
                    {
                        if (name == "AHx")
                        {
                            goto TR_000E;
                        }
                    }
                    else if ((num == 0xa2aa4b86) && (name == "DCTDecode"))
                    {
                        goto TR_0013;
                    }
                }
                else if (num > 0x6b2fbed1)
                {
                    if (num == 0x6b4a2b7f)
                    {
                        if (name == "CCF")
                        {
                            goto TR_0011;
                        }
                    }
                    else if ((num == 0x810503fb) && (name == "RL"))
                    {
                        goto TR_001B;
                    }
                }
                else if (num == 0x60d334cf)
                {
                    if (name == "Fl")
                    {
                        goto TR_0006;
                    }
                }
                else if ((num == 0x6b2fbed1) && (name == "ASCII85Decode"))
                {
                    goto TR_000C;
                }
                goto TR_0001;
            }
            else
            {
                if (num > 0x18481007)
                {
                    if (num > 0x4f80a68b)
                    {
                        if (num == 0x58000b2c)
                        {
                            if (name == "DCT")
                            {
                                goto TR_0013;
                            }
                        }
                        else if ((num == 0x5cc248eb) && (name == "CCITTFaxDecode"))
                        {
                            goto TR_0011;
                        }
                    }
                    else if (num == 0x2c7c4e77)
                    {
                        if (name == "ASCIIHexDecode")
                        {
                            goto TR_000E;
                        }
                    }
                    else if ((num == 0x4f80a68b) && (name == "A85"))
                    {
                        goto TR_000C;
                    }
                }
                else if (num > 0x1543b53b)
                {
                    if (num == 0x180cd636)
                    {
                        if (name == "LZWDecode")
                        {
                            goto TR_0008;
                        }
                    }
                    else if ((num == 0x18481007) && (name == "FlateDecode"))
                    {
                        goto TR_0006;
                    }
                }
                else if (num != 0x134f6ef7)
                {
                    if ((num == 0x1543b53b) && (name == "JBIG2Decode"))
                    {
                        return new PdfJBIG2DecodeFilter(parameters);
                    }
                }
                else if (name == "JPXDecode")
                {
                    return new PdfJPXDecodeFilter();
                }
                goto TR_0001;
            }
            goto TR_001B;
        TR_0001:
            PdfDocumentStructureReader.ThrowIncorrectDataException();
            return null;
        TR_0006:
            return new PdfFlateDecodeFilter(parameters);
        TR_0008:
            return new PdfLZWDecodeFilter(parameters);
        TR_000C:
            return new PdfASCII85DecodeFilter();
        TR_000E:
            return new PdfASCIIHexDecodeFilter();
        TR_0011:
            return new PdfCCITTFaxDecodeFilter(parameters);
        TR_0013:
            return new PdfDCTDecodeFilter(parameters);
        TR_001B:
            return new PdfRunLengthDecodeFilter();
        }

        protected internal virtual PdfScanlineTransformationResult CreateScanlineSource(PdfImage image, int componentsCount, byte[] data) => 
            new PdfScanlineTransformationResult(PdfCommonImageScanlineSource.CreateImageScanlineSource(this.Decode(data), image, componentsCount));

        protected internal abstract byte[] Decode(byte[] data);
        protected internal virtual PdfWriterDictionary Write(PdfObjectCollection objects) => 
            null;

        protected internal abstract string FilterName { get; }

        internal virtual byte[] EodToken =>
            null;
    }
}

