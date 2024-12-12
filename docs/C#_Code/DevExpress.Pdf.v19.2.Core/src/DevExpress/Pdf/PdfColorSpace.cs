namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public abstract class PdfColorSpace : PdfObject
    {
        internal const string GrayColorSpaceAbbreviation = "G";
        internal const string RgbColorSpaceAbbreviation = "RGB";
        internal const string CmykColorSpaceAbbreviation = "CMYK";
        private const string indexedColorSpaceAbbreviation = "I";

        protected PdfColorSpace()
        {
        }

        protected internal virtual PdfColor AlternateTransform(PdfColor color) => 
            this.Transform(color);

        internal static PdfColorSpace CreateColorSpace(string name)
        {
            uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
            if (num > 0xc9d3835c)
            {
                if (num > 0xd3881823)
                {
                    if (num == 0xe3439258)
                    {
                        if (name == "DeviceGray")
                        {
                            goto TR_0003;
                        }
                    }
                    else if ((num == 0xf49120c9) && (name == "Pattern"))
                    {
                        return new PdfPatternColorSpace();
                    }
                }
                else if (num == 0xce6d07f5)
                {
                    if (name == "CMYK")
                    {
                        goto TR_0008;
                    }
                }
                else if ((num == 0xd3881823) && (name == "DeviceCMYK"))
                {
                    goto TR_0008;
                }
                goto TR_0001;
            }
            else
            {
                if (num == 0x2180c09a)
                {
                    if (name == "DeviceRGB")
                    {
                        goto TR_0000;
                    }
                }
                else if (num == 0xc20bf3a6)
                {
                    if (name == "G")
                    {
                        goto TR_0003;
                    }
                }
                else if ((num == 0xc9d3835c) && (name == "RGB"))
                {
                    goto TR_0000;
                }
                goto TR_0001;
            }
            goto TR_0008;
        TR_0000:
            return new PdfDeviceColorSpace(PdfDeviceColorSpaceKind.RGB);
        TR_0001:
            return null;
        TR_0003:
            return new PdfDeviceColorSpace(PdfDeviceColorSpaceKind.Gray);
        TR_0008:
            return new PdfDeviceColorSpace(PdfDeviceColorSpaceKind.CMYK);
        }

        protected internal virtual PdfRange[] CreateDefaultDecodeArray(int bitsPerComponent)
        {
            int componentsCount = this.ComponentsCount;
            PdfRange[] rangeArray = new PdfRange[componentsCount];
            for (int i = 0; i < componentsCount; i++)
            {
                rangeArray[i] = new PdfRange(0.0, 1.0);
            }
            return rangeArray;
        }

        protected virtual IPdfImageScanlineSource GetDecodedImageScanlineSource(IPdfImageScanlineSource decoratingSource, PdfImage image, int width)
        {
            IList<PdfRange> decode = image.Decode;
            if (decode != null)
            {
                PdfRange[] rangeArray = this.CreateDefaultDecodeArray(image.BitsPerComponent);
                int length = rangeArray.Length;
                if (length == decode.Count)
                {
                    for (int i = 0; i < length; i++)
                    {
                        if (!rangeArray[i].IsSame(decode[i]))
                        {
                            return new PdfDecodeImageScanlineSource(decode, width, decoratingSource);
                        }
                    }
                }
            }
            return decoratingSource;
        }

        internal static PdfColorSpace Parse(PdfObjectCollection objects, object value)
        {
            PdfName name = value as PdfName;
            if (name != null)
            {
                PdfColorSpace space = CreateColorSpace(name.Name);
                if (space == null)
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                return space;
            }
            IList<object> array = value as IList<object>;
            if (array == null)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            name = array[0] as PdfName;
            if (name == null)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            string s = name.Name;
            uint num = <PrivateImplementationDetails>.ComputeStringHash(s);
            if (num > 0xc03e8651)
            {
                if (num > 0xcc0c0364)
                {
                    if (num > 0xd3881823)
                    {
                        if (num == 0xe3439258)
                        {
                            if (s == "DeviceGray")
                            {
                                goto TR_0023;
                            }
                        }
                        else if ((num == 0xf49120c9) && (s == "Pattern"))
                        {
                            int count = array.Count;
                            if (count == 1)
                            {
                                return new PdfPatternColorSpace();
                            }
                            if (count == 2)
                            {
                                return new PdfPatternColorSpace(objects.GetColorSpace(array[1]));
                            }
                            PdfDocumentStructureReader.ThrowIncorrectDataException();
                            return null;
                        }
                    }
                    else if (num == 0xce6d07f5)
                    {
                        if (s == "CMYK")
                        {
                            goto TR_002A;
                        }
                    }
                    else if ((num == 0xd3881823) && (s == "DeviceCMYK"))
                    {
                        goto TR_002A;
                    }
                }
                else if (num > 0xc4eb2a94)
                {
                    if (num == 0xc9d3835c)
                    {
                        if (s == "RGB")
                        {
                            goto TR_0012;
                        }
                    }
                    else if ((num == 0xcc0c0364) && (s == "I"))
                    {
                        goto TR_001C;
                    }
                }
                else if (num == 0xc20bf3a6)
                {
                    if (s == "G")
                    {
                        goto TR_0023;
                    }
                }
                else if ((num == 0xc4eb2a94) && (s == "Lab"))
                {
                    return PdfLabColorSpace.Create(objects, array);
                }
                goto TR_0001;
            }
            else
            {
                if (num > 0x67b8afcc)
                {
                    if (num > 0x89b202f5)
                    {
                        if (num == 0xad43cc0a)
                        {
                            if (s == "Indexed")
                            {
                                goto TR_001C;
                            }
                        }
                        else if ((num == 0xc03e8651) && (s == "ICCBased"))
                        {
                            return new PdfICCBasedColorSpace(objects, array);
                        }
                    }
                    else if (num != 0x6e5e4416)
                    {
                        if ((num == 0x89b202f5) && (s == "Separation"))
                        {
                            return new PdfSeparationColorSpace(objects, array);
                        }
                    }
                    else if (s == "CalRGB")
                    {
                        return PdfCalRGBColorSpace.Create(objects, array);
                    }
                }
                else if (num == 0x2180c09a)
                {
                    if (s == "DeviceRGB")
                    {
                        goto TR_0012;
                    }
                }
                else if (num != 0x24acee67)
                {
                    if ((num == 0x67b8afcc) && (s == "CalGray"))
                    {
                        return PdfCalGrayColorSpace.Create(objects, array);
                    }
                }
                else if (s == "DeviceN")
                {
                    if (array.Count != 5)
                    {
                        return new PdfDeviceNColorSpace(objects, array);
                    }
                    value = array[4];
                    PdfReaderDictionary dictionary = objects.TryResolve(value, null) as PdfReaderDictionary;
                    if (dictionary == null)
                    {
                        PdfDocumentStructureReader.ThrowIncorrectDataException();
                    }
                    string str2 = dictionary.GetName("Subtype");
                    if (str2 == null)
                    {
                        return new PdfDeviceNColorSpace(objects, array);
                    }
                    if (str2 == "DeviceN")
                    {
                        return new PdfDeviceNColorSpace(objects, array);
                    }
                    if (str2 == "NChannel")
                    {
                        return new PdfNChannelColorSpace(objects, array, dictionary);
                    }
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                    return null;
                }
                goto TR_0001;
            }
            goto TR_002A;
        TR_0001:
            PdfDocumentStructureReader.ThrowIncorrectDataException();
            return null;
        TR_0012:
            return new PdfDeviceColorSpace(PdfDeviceColorSpaceKind.RGB);
        TR_001C:
            return new PdfIndexedColorSpace(objects, array);
        TR_0023:
            return new PdfDeviceColorSpace(PdfDeviceColorSpaceKind.Gray);
        TR_002A:
            return new PdfDeviceColorSpace(PdfDeviceColorSpaceKind.CMYK);
        }

        protected internal virtual PdfColor Transform(PdfColor color) => 
            null;

        protected internal virtual PdfScanlineTransformationResult Transform(IPdfImageScanlineSource data, int width) => 
            null;

        protected internal virtual PdfScanlineTransformationResult Transform(PdfImage image, IPdfImageScanlineSource data, PdfImageParameters parameters)
        {
            if ((parameters.Width <= image.Width) || (parameters.Height <= image.Height))
            {
                return this.Transform(this.GetDecodedImageScanlineSource(image.GetInterpolatedScanlineSource(data, parameters), image, parameters.Width), parameters.Width);
            }
            PdfScanlineTransformationResult result = this.Transform(this.GetDecodedImageScanlineSource(data, image, image.Width), image.Width);
            return new PdfScanlineTransformationResult(image.GetInterpolatedScanlineSource(result.ScanlineSource, parameters), result.PixelFormat);
        }

        protected internal abstract object Write(PdfObjectCollection collection);

        public abstract int ComponentsCount { get; }
    }
}

