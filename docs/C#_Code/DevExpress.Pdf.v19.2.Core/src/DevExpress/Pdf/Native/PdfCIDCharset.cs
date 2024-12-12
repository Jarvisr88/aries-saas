namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Threading;

    public class PdfCIDCharset
    {
        private const string charsetResourceName = "DocumentModel.Fonts.CompositeFonts.charsets.bin";
        private static readonly ThreadLocal<WeakReferenceCache<string, PdfCIDCharset>> cache = new ThreadLocal<WeakReferenceCache<string, PdfCIDCharset>>(() => new WeakReferenceCache<string, PdfCIDCharset>(new Func<string, PdfCIDCharset>(PdfCIDCharset.Parse)));
        private const string AdobeGB1 = "Adobe-GB1";
        private const string AdobeCNS1 = "Adobe-CNS1";
        private const string AdobeJapan1 = "Adobe-Japan1";
        private const string AdobeKorea1 = "Adobe-Korea1";
        private const short notDef = 0;
        private readonly IDictionary<short, short> mapping;
        private readonly PdfFontCharset charset;

        private PdfCIDCharset(IDictionary<short, short> mapping, string registryOrdering)
        {
            this.mapping = mapping;
            if (registryOrdering == "Adobe-GB1")
            {
                this.charset = PdfFontCharset.GB1;
            }
            else if (registryOrdering == "Adobe-CNS1")
            {
                this.charset = PdfFontCharset.CNS1;
            }
            else if (registryOrdering == "Adobe-Japan1")
            {
                this.charset = PdfFontCharset.Japan1;
            }
            else if (registryOrdering != "Adobe-Korea1")
            {
                this.charset = PdfFontCharset.Basic;
            }
            else
            {
                this.charset = PdfFontCharset.Korea1;
            }
        }

        public static PdfCIDCharset GetCharsetByPredefinedEncodingName(string encodingName)
        {
            uint num = <PrivateImplementationDetails>.ComputeStringHash(encodingName);
            if (num > 0x97c156de)
            {
                if (num > 0xdfd234fa)
                {
                    if (num > 0xe96f7710)
                    {
                        if (num > 0xfbdbf01d)
                        {
                            if (num > 0xfddbf343)
                            {
                                if (num == 0xfec3ea10)
                                {
                                    if (encodingName == "GBKp-EUC-H")
                                    {
                                        goto TR_0003;
                                    }
                                }
                                else if ((num == 0xff074b07) && (encodingName == "EUC-V"))
                                {
                                    goto TR_000D;
                                }
                            }
                            else if (num == 0xfcc3e6ea)
                            {
                                if (encodingName == "GBKp-EUC-V")
                                {
                                    goto TR_0003;
                                }
                            }
                            else if ((num == 0xfddbf343) && (encodingName == "UniKS-UTF16-V"))
                            {
                                goto TR_0000;
                            }
                        }
                        else if (num > 0xf5170e5a)
                        {
                            if (num == 0xf85b70f4)
                            {
                                if (encodingName == "UniJIS-UCS2-HW-H")
                                {
                                    goto TR_000D;
                                }
                            }
                            else if ((num == 0xfbdbf01d) && (encodingName == "UniKS-UTF16-H"))
                            {
                                goto TR_0000;
                            }
                        }
                        else if (num == 0xf28ec93c)
                        {
                            if (encodingName == "Ext-RKSJ-H")
                            {
                                goto TR_000D;
                            }
                        }
                        else if ((num == 0xf5170e5a) && (encodingName == "90ms-RKSJ-V"))
                        {
                            goto TR_000D;
                        }
                    }
                    else if (num > 0xe5072219)
                    {
                        if (num > 0xe76f73ea)
                        {
                            if (num == 0xe88eb97e)
                            {
                                if (encodingName == "Ext-RKSJ-V")
                                {
                                    goto TR_000D;
                                }
                            }
                            else if ((num == 0xe96f7710) && (encodingName == "CNS-EUC-V"))
                            {
                                goto TR_0008;
                            }
                        }
                        else if (num == 0xe5867b1a)
                        {
                            if (encodingName == "83pv-RKSJ-H")
                            {
                                goto TR_000D;
                            }
                        }
                        else if ((num == 0xe76f73ea) && (encodingName == "CNS-EUC-H"))
                        {
                            goto TR_0008;
                        }
                    }
                    else if (num == 0xe1967156)
                    {
                        if (encodingName == "KSCpc-EUC-H")
                        {
                            goto TR_0000;
                        }
                    }
                    else if (num == 0xe4bcc06f)
                    {
                        if (encodingName == "UniJIS-UTF16-H")
                        {
                            goto TR_000D;
                        }
                    }
                    else if ((num == 0xe5072219) && (encodingName == "EUC-H"))
                    {
                        goto TR_000D;
                    }
                }
                else if (num > 0xc1d205c0)
                {
                    if (num > 0xd30c0e69)
                    {
                        if (num > 0xdabcb0b1)
                        {
                            if (num == 0xde5b4806)
                            {
                                if (encodingName == "UniJIS-UCS2-HW-V")
                                {
                                    goto TR_000D;
                                }
                            }
                            else if ((num == 0xdfd234fa) && (encodingName == "GBpc-EUC-H"))
                            {
                                goto TR_0003;
                            }
                        }
                        else if (num == 0xd716df20)
                        {
                            if (encodingName == "90ms-RKSJ-H")
                            {
                                goto TR_000D;
                            }
                        }
                        else if ((num == 0xdabcb0b1) && (encodingName == "UniJIS-UTF16-V"))
                        {
                            goto TR_000D;
                        }
                    }
                    else if (num > 0xc503062a)
                    {
                        if (num == 0xcd0c04f7)
                        {
                            if (encodingName == "H")
                            {
                                goto TR_000D;
                            }
                        }
                        else if ((num == 0xd30c0e69) && (encodingName == "V"))
                        {
                            goto TR_000D;
                        }
                    }
                    else if (num == 0xc47fe81a)
                    {
                        if (encodingName == "UniKS-UCS2-H")
                        {
                            goto TR_0000;
                        }
                    }
                    else if ((num == 0xc503062a) && (encodingName == "90pv-RKSJ-H"))
                    {
                        goto TR_000D;
                    }
                }
                else if (num > 0xa8dcd55e)
                {
                    if (num > 0xb69521fe)
                    {
                        if (num == 0xc09531bc)
                        {
                            if (encodingName == "ETen-B5-V")
                            {
                                goto TR_0008;
                            }
                        }
                        else if ((num == 0xc1d205c0) && (encodingName == "GBpc-EUC-V"))
                        {
                            goto TR_0003;
                        }
                    }
                    else if (num == 0xb2dce51c)
                    {
                        if (encodingName == "ETenms-B5-V")
                        {
                            goto TR_0008;
                        }
                    }
                    else if ((num == 0xb69521fe) && (encodingName == "ETen-B5-H"))
                    {
                        goto TR_0008;
                    }
                }
                else if (num == 0xa1c1669c)
                {
                    if (encodingName == "B5pc-H")
                    {
                        goto TR_0008;
                    }
                }
                else if (num == 0xa67fb8e0)
                {
                    if (encodingName == "UniKS-UCS2-V")
                    {
                        goto TR_0000;
                    }
                }
                else if ((num == 0xa8dcd55e) && (encodingName == "ETenms-B5-H"))
                {
                    goto TR_0008;
                }
                goto TR_0001;
            }
            else if (num > 0x4e26052a)
            {
                if (num > 0x660df7de)
                {
                    if (num > 0x73f11358)
                    {
                        if (num > 0x7c0c1e02)
                        {
                            if (num == 0x81f12962)
                            {
                                if (encodingName == "UniGB-UTF16-H")
                                {
                                    goto TR_0003;
                                }
                            }
                            else if ((num == 0x97c156de) && (encodingName == "B5pc-V"))
                            {
                                goto TR_0008;
                            }
                        }
                        else if (num == 0x78eca36f)
                        {
                            if (encodingName == "UniCNS-UTF16-H")
                            {
                                goto TR_0008;
                            }
                        }
                        else if ((num == 0x7c0c1e02) && (encodingName == "HKscs-B5-H"))
                        {
                            goto TR_0008;
                        }
                    }
                    else if (num > 0x6eec93b1)
                    {
                        if (num == 0x700e079c)
                        {
                            if (encodingName == "UniJIS-UCS2-H")
                            {
                                goto TR_000D;
                            }
                        }
                        else if ((num == 0x73f11358) && (encodingName == "UniGB-UTF16-V"))
                        {
                            goto TR_0003;
                        }
                    }
                    else if (num == 0x6e0c07f8)
                    {
                        if (encodingName == "HKscs-B5-V")
                        {
                            goto TR_0008;
                        }
                    }
                    else if ((num == 0x6eec93b1) && (encodingName == "UniCNS-UTF16-V"))
                    {
                        goto TR_0008;
                    }
                    goto TR_0001;
                }
                else if (num > 0x551672ea)
                {
                    if (num > 0x567a63b0)
                    {
                        if (num == 0x57167610)
                        {
                            if (encodingName == "GBK-EUC-V")
                            {
                                goto TR_0003;
                            }
                        }
                        else if ((num == 0x660df7de) && (encodingName == "UniJIS-UCS2-V"))
                        {
                            goto TR_000D;
                        }
                    }
                    else if (num == 0x55db3467)
                    {
                        if (encodingName == "GBK2K-H")
                        {
                            goto TR_0003;
                        }
                    }
                    else if ((num == 0x567a63b0) && (encodingName == "KSCms-UHC-V"))
                    {
                        goto TR_0000;
                    }
                    goto TR_0001;
                }
                else if (num == 0x50260850)
                {
                    if (encodingName != "90msp-RKSJ-H")
                    {
                        goto TR_0001;
                    }
                }
                else
                {
                    if (num == 0x547a608a)
                    {
                        if (encodingName == "KSCms-UHC-H")
                        {
                            goto TR_0000;
                        }
                    }
                    else if ((num == 0x551672ea) && (encodingName == "GBK-EUC-H"))
                    {
                        goto TR_0003;
                    }
                    goto TR_0001;
                }
            }
            else if (num > 0x3237c95b)
            {
                if (num > 0x3784c027)
                {
                    if (num > 0x3bdb0b79)
                    {
                        if (num == 0x47da9882)
                        {
                            if (encodingName == "KSCms-UHC-HW-H")
                            {
                                goto TR_0000;
                            }
                            goto TR_0001;
                        }
                        else if ((num != 0x4e26052a) || (encodingName != "90msp-RKSJ-V"))
                        {
                            goto TR_0001;
                        }
                    }
                    else
                    {
                        if (num == 0x39da8278)
                        {
                            if (encodingName == "KSCms-UHC-HW-V")
                            {
                                goto TR_0000;
                            }
                        }
                        else if ((num == 0x3bdb0b79) && (encodingName == "GBK2K-V"))
                        {
                            goto TR_0003;
                        }
                        goto TR_0001;
                    }
                }
                else if (num == 0x3558789c)
                {
                    if (encodingName == "UniCNS-UCS2-H")
                    {
                        goto TR_0008;
                    }
                    goto TR_0001;
                }
                else if (num == 0x36423d3c)
                {
                    if (encodingName != "Add-RKSJ-H")
                    {
                        goto TR_0001;
                    }
                }
                else
                {
                    if ((num == 0x3784c027) && (encodingName == "UniGB-UCS2-H"))
                    {
                        goto TR_0003;
                    }
                    goto TR_0001;
                }
            }
            else if (num > 0x2037ad05)
            {
                if (num > 0x2b5868de)
                {
                    if (num == 0x2c422d7e)
                    {
                        if (encodingName != "Add-RKSJ-V")
                        {
                            goto TR_0001;
                        }
                    }
                    else
                    {
                        if ((num == 0x3237c95b) && (encodingName == "KSC-EUC-H"))
                        {
                            goto TR_0000;
                        }
                        goto TR_0001;
                    }
                }
                else
                {
                    if (num == 0x298163cf)
                    {
                        if (encodingName == "GB-EUC-H")
                        {
                            goto TR_0003;
                        }
                    }
                    else if ((num == 0x2b5868de) && (encodingName == "UniCNS-UCS2-V"))
                    {
                        goto TR_0008;
                    }
                    goto TR_0001;
                }
            }
            else
            {
                if (num == 0x1d849739)
                {
                    if (encodingName == "UniGB-UCS2-V")
                    {
                        goto TR_0003;
                    }
                }
                else if (num == 0x1f815411)
                {
                    if (encodingName == "GB-EUC-V")
                    {
                        goto TR_0003;
                    }
                }
                else if ((num == 0x2037ad05) && (encodingName == "KSC-EUC-V"))
                {
                    goto TR_0000;
                }
                goto TR_0001;
            }
            goto TR_000D;
        TR_0000:
            return GetPredefinedCharset("Adobe-Korea1");
        TR_0001:
            return null;
        TR_0003:
            return GetPredefinedCharset("Adobe-GB1");
        TR_0008:
            return GetPredefinedCharset("Adobe-CNS1");
        TR_000D:
            return GetPredefinedCharset("Adobe-Japan1");
        }

        public static PdfCIDCharset GetPredefinedCharset(PdfType0Font font)
        {
            PdfCIDSystemInfo systemInfo = font.SystemInfo;
            PdfCIDCharset charset = (systemInfo == null) ? null : GetPredefinedCharset(systemInfo.Registry + "-" + systemInfo.Ordering);
            if (charset == null)
            {
                PdfPredefinedCompositeFontEncoding encoding = font.Encoding as PdfPredefinedCompositeFontEncoding;
                charset = (encoding == null) ? null : GetCharsetByPredefinedEncodingName(encoding.Name);
            }
            return charset;
        }

        public static PdfCIDCharset GetPredefinedCharset(string registryOrdering) => 
            cache.Value.GetValue(registryOrdering);

        public short GetUnicode(short cid)
        {
            short num;
            return (!this.mapping.TryGetValue(cid, out num) ? 0 : num);
        }

        private static PdfCIDCharset Parse(string registryOrdering)
        {
            PdfCIDCharset charset;
            if ((registryOrdering != "Adobe-GB1") && ((registryOrdering != "Adobe-CNS1") && ((registryOrdering != "Adobe-Japan1") && (registryOrdering != "Adobe-Korea1"))))
            {
                return null;
            }
            using (Stream stream = PdfEmbeddedResourceProvider.GetDecompressedEmbeddedResourceStream("DocumentModel.Fonts.CompositeFonts.charsets.bin"))
            {
                using (BinaryReader reader = new BinaryReader(stream))
                {
                    while (true)
                    {
                        if (stream.Position != stream.Length)
                        {
                            int num = reader.ReadInt32();
                            int count = reader.ReadInt32();
                            string str = Encoding.Unicode.GetString(reader.ReadBytes(count));
                            int num3 = (num - count) - 4;
                            if (str.Substring(0, str.Length - 2) != registryOrdering)
                            {
                                stream.Position += num3;
                                continue;
                            }
                            IDictionary<short, short> mapping = new Dictionary<short, short>();
                            long num4 = stream.Position + num3;
                            while (true)
                            {
                                if (stream.Position >= num4)
                                {
                                    charset = new PdfCIDCharset(mapping, registryOrdering);
                                    break;
                                }
                                int num5 = reader.ReadInt32();
                                short num6 = reader.ReadInt16();
                                short key = reader.ReadInt16();
                                mapping.Add(key, num6);
                                for (int i = 0; i < num5; i++)
                                {
                                    key = (short) (key + 1);
                                    mapping.Add(key, reader.ReadInt16());
                                }
                            }
                        }
                        else
                        {
                            return null;
                        }
                        break;
                    }
                }
            }
            return charset;
        }

        public PdfFontCharset Charset =>
            this.charset;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PdfCIDCharset.<>c <>9 = new PdfCIDCharset.<>c();

            internal WeakReferenceCache<string, PdfCIDCharset> <.cctor>b__17_0() => 
                new WeakReferenceCache<string, PdfCIDCharset>(new Func<string, PdfCIDCharset>(PdfCIDCharset.Parse));
        }
    }
}

