namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class PdfImage : PdfXObject
    {
        internal const string Type = "Image";
        internal const string WidthDictionaryKey = "Width";
        internal const string HeightDictionaryKey = "Height";
        internal const string ColorSpaceDictionaryKey = "ColorSpace";
        internal const string BitsPerComponentDictionaryKey = "BitsPerComponent";
        internal const string WidthDictionaryAbbreviation = "W";
        internal const string HeightDictionaryAbbreviation = "H";
        internal const string ColorSpaceDictionaryAbbreviation = "CS";
        internal const string BitsPerComponentDictionaryAbbreviation = "BPC";
        internal const string DecodeDictionaryKey = "Decode";
        internal const string DecodeDictionaryAbbreviation = "D";
        internal const string IntentDictionaryKey = "Intent";
        internal const string ImageMaskDictionaryKey = "ImageMask";
        internal const string ImageMaskDictionaryAbbreviation = "IM";
        internal const string InterpolateDictionaryKey = "Interpolate";
        internal const string InterpolateDictionaryAbbreviation = "I";
        private const string maskDictionaryKey = "Mask";
        private const string sMaskDictionaryKey = "SMask";
        private const string matteDictionaryKey = "Matte";
        private const string sMaskInDataDictionaryKey = "SMaskInData";
        private readonly int width;
        private readonly int height;
        private readonly PdfColorSpace colorSpace;
        private readonly int bitsPerComponent;
        private readonly PdfRenderingIntent? intent;
        private readonly bool isMask;
        private readonly PdfImage mask;
        private readonly IList<PdfRange> decode;
        private readonly bool interpolate;
        private readonly PdfImage sMask;
        private readonly IList<double> matte;
        private readonly PdfArrayCompressedData compressedData;
        private IList<PdfRange> colorKeyMask;

        internal PdfImage(PdfReaderStream stream) : base(stream.Dictionary)
        {
            object obj2;
            int componentsCount;
            PdfDeviceColorSpace colorSpace;
            object obj3;
            this.compressedData = new PdfArrayCompressedData(stream);
            PdfReaderDictionary dictionary = stream.Dictionary;
            this.width = GetDimension(dictionary, "Width");
            this.height = GetDimension(dictionary, "Height");
            PdfObjectCollection objects = dictionary.Objects;
            this.colorSpace = dictionary.GetColorSpace("ColorSpace");
            int? integer = dictionary.GetInteger("BitsPerComponent");
            this.intent = this.CreateIntent(dictionary);
            bool? boolean = dictionary.GetBoolean("ImageMask");
            this.isMask = (boolean != null) ? boolean.GetValueOrDefault() : false;
            obj2 = dictionary.TryGetValue("Mask", out obj2) ? objects.TryResolve(obj2, null) : null;
            if (this.isMask)
            {
                this.bitsPerComponent = (integer != null) ? integer.Value : 1;
                if (this.colorSpace == null)
                {
                    this.colorSpace = new PdfDeviceColorSpace(PdfDeviceColorSpaceKind.Gray);
                }
                else
                {
                    colorSpace = this.colorSpace as PdfDeviceColorSpace;
                    if ((colorSpace == null) || (colorSpace.Kind != PdfDeviceColorSpaceKind.Gray))
                    {
                        PdfDocumentStructureReader.ThrowIncorrectDataException();
                    }
                }
                componentsCount = 1;
            }
            else
            {
                bool flag = this.colorSpace != null;
                bool flag2 = integer != null;
                if (flag & flag2)
                {
                    componentsCount = this.colorSpace.ComponentsCount;
                    this.bitsPerComponent = integer.Value;
                    if ((this.bitsPerComponent != 1) && ((this.bitsPerComponent != 2) && ((this.bitsPerComponent != 4) && ((this.bitsPerComponent != 8) && (this.bitsPerComponent != 0x10)))))
                    {
                        PdfDocumentStructureReader.ThrowIncorrectDataException();
                    }
                }
                else
                {
                    IList<PdfFilter> filters = this.Filters;
                    int num2 = filters.Count - 1;
                    if (num2 < 0)
                    {
                        PdfDocumentStructureReader.ThrowIncorrectDataException();
                    }
                    PdfFilter filter = filters[num2];
                    if (filter is PdfJPXDecodeFilter)
                    {
                        componentsCount = 0;
                    }
                    else
                    {
                        PdfFlateDecodeFilter filter2 = filter as PdfFlateDecodeFilter;
                        if (filter2 == null)
                        {
                            PdfDocumentStructureReader.ThrowIncorrectDataException();
                        }
                        if (flag)
                        {
                            componentsCount = this.colorSpace.ComponentsCount;
                        }
                        else
                        {
                            switch (filter2.Colors)
                            {
                                case 1:
                                    this.colorSpace = new PdfDeviceColorSpace(PdfDeviceColorSpaceKind.Gray);
                                    break;

                                case 3:
                                    this.colorSpace = new PdfDeviceColorSpace(PdfDeviceColorSpaceKind.RGB);
                                    break;

                                case 4:
                                    this.colorSpace = new PdfDeviceColorSpace(PdfDeviceColorSpaceKind.CMYK);
                                    break;

                                default:
                                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                                    break;
                            }
                        }
                        this.bitsPerComponent = flag2 ? integer.Value : filter2.BitsPerComponent;
                    }
                }
                if (obj2 != null)
                {
                    IList<object> list2 = obj2 as IList<object>;
                    if (list2 == null)
                    {
                        this.mask = objects.GetXObject(obj2, null, "Image") as PdfImage;
                    }
                    else
                    {
                        if ((this.bitsPerComponent == 0) || ((componentsCount == 0) || (list2.Count != (componentsCount * 2))))
                        {
                            PdfDocumentStructureReader.ThrowIncorrectDataException();
                        }
                        int num3 = (1 << (this.bitsPerComponent & 0x1f)) - 1;
                        this.colorKeyMask = new List<PdfRange>(componentsCount);
                        int num4 = 0;
                        int num5 = 0;
                        while (num4 < componentsCount)
                        {
                            object obj4 = list2[num5++];
                            object obj5 = list2[num5++];
                            if (!(obj4 is int) || !(obj5 is int))
                            {
                                PdfDocumentStructureReader.ThrowIncorrectDataException();
                            }
                            int num6 = Math.Max(0, Math.Min((int) obj4, num3));
                            int num7 = Math.Max(0, Math.Min((int) obj5, num3));
                            if (num7 < num6)
                            {
                                num7 = num6;
                                num6 = num7;
                            }
                            this.colorKeyMask.Add(new PdfRange((double) num6, (double) num7));
                            num4++;
                        }
                    }
                }
            }
            this.decode = this.CreateDecode(dictionary.GetArray("Decode"));
            boolean = dictionary.GetBoolean("Interpolate");
            this.interpolate = (boolean != null) ? boolean.GetValueOrDefault() : false;
            if (dictionary.TryGetValue("SMask", out obj3))
            {
                PdfObjectReference reference = obj3 as PdfObjectReference;
                if ((reference != null) && ((reference.Generation == base.ObjectGeneration) && (reference.Number == base.ObjectNumber)))
                {
                    this.sMask = this;
                }
                else
                {
                    PdfXObject obj6 = objects.GetXObject(obj3, null, "Image");
                    if (obj6 != null)
                    {
                        this.sMask = obj6 as PdfImage;
                        if ((this.sMask == null) || (componentsCount == 0))
                        {
                            PdfDocumentStructureReader.ThrowIncorrectDataException();
                        }
                        colorSpace = this.sMask.ColorSpace as PdfDeviceColorSpace;
                        IList<double> matte = this.sMask.Matte;
                        if ((colorSpace == null) || ((colorSpace.Kind != PdfDeviceColorSpaceKind.Gray) || ((matte != null) && (matte.Count != componentsCount))))
                        {
                            PdfDocumentStructureReader.ThrowIncorrectDataException();
                        }
                    }
                }
            }
            this.matte = dictionary.GetDoubleArray("Matte");
            this.<SMaskInData>k__BackingField = dictionary.GetInteger("SMaskInData").GetValueOrDefault(0);
        }

        private PdfImage(int width, int height, PdfColorSpace colorSpace, int bitsPerComponent, PdfArrayCompressedData compressedData)
        {
            this.width = width;
            this.height = height;
            this.colorSpace = colorSpace;
            this.bitsPerComponent = bitsPerComponent;
            this.compressedData = compressedData;
        }

        internal PdfImage(int width, int height, PdfColorSpace colorSpace, int bitsPerComponent, IList<PdfRange> decode, PdfArrayCompressedData data, PdfImage sMask) : this(width, height, colorSpace, bitsPerComponent, data)
        {
            this.sMask = sMask;
            this.decode = decode;
        }

        internal PdfImage(int width, int height, PdfColorSpace colorSpace, int bitsPerComponent, bool isMask, PdfArrayCompressedData data, PdfReaderDictionary dictionary, PdfResources resources) : this(width, height, colorSpace, bitsPerComponent, data)
        {
            bool valueOrDefault;
            this.isMask = isMask;
            this.intent = this.CreateIntent(dictionary);
            if (!isMask)
            {
                if (this.colorSpace == null)
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
            }
            else if (this.colorSpace == null)
            {
                this.colorSpace = new PdfDeviceColorSpace(PdfDeviceColorSpaceKind.Gray);
            }
            else
            {
                PdfDeviceColorSpace space = this.colorSpace as PdfDeviceColorSpace;
                if ((space == null) || (space.Kind != PdfDeviceColorSpaceKind.Gray))
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
            }
            IList<object> array = dictionary.GetArray("D");
            IList<object> decodeArray = array;
            if (array == null)
            {
                IList<object> local1 = array;
                decodeArray = dictionary.GetArray("Decode");
            }
            this.decode = this.CreateDecode(decodeArray);
            bool? boolean = dictionary.GetBoolean("I");
            if (boolean != null)
            {
                valueOrDefault = boolean.GetValueOrDefault();
            }
            else
            {
                bool? nullable2 = dictionary.GetBoolean("Interpolate");
                valueOrDefault = (nullable2 != null) ? nullable2.GetValueOrDefault() : false;
            }
            this.interpolate = valueOrDefault;
        }

        private PdfImageData ApplyMask(IPdfImageScanlineSource maskScanlineSource, PdfImageParameters parameters, IList<double> matte)
        {
            PdfScanlineTransformationResult transformedData = this.GetTransformedData(parameters);
            int width = parameters.Width;
            IPdfImageScanlineSource scanlineSource = transformedData.ScanlineSource;
            if (transformedData.PixelFormat == PdfPixelFormat.Gray8bit)
            {
                scanlineSource = new PdfGrayToRGBImageScanlineSource(scanlineSource, width);
            }
            if ((this.colorSpace != null) && (matte != null))
            {
                matte = this.colorSpace.AlternateTransform(new PdfColor(matte)).Components;
            }
            return new PdfImageData(((matte == null) || (matte.Count != scanlineSource.ComponentsCount)) ? ((PdfImageDataSource) new PdfTransparentImageDataSource(scanlineSource, maskScanlineSource, width)) : ((PdfImageDataSource) new PdfTransparentMatteImageDataSource(scanlineSource, maskScanlineSource, width, matte)), width, parameters.Height, width * 4, PdfPixelFormat.Argb32bpp, null);
        }

        private IList<PdfRange> CreateDecode(IList<object> decodeArray)
        {
            if (decodeArray == null)
            {
                if (this.colorSpace != null)
                {
                    return this.colorSpace.CreateDefaultDecodeArray(this.bitsPerComponent);
                }
                return new PdfRange[] { new PdfRange(0.0, 1.0) };
            }
            int num = (this.colorSpace == null) ? 1 : this.colorSpace.ComponentsCount;
            if (decodeArray.Count < (num * 2))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            PdfRange[] rangeArray = new PdfRange[num];
            int index = 0;
            int num3 = 0;
            while (index < num)
            {
                double min = PdfDocumentReader.ConvertToDouble(decodeArray[num3++]);
                double max = PdfDocumentReader.ConvertToDouble(decodeArray[num3++]);
                rangeArray[index] = new PdfRange(min, max);
                index++;
            }
            return rangeArray;
        }

        protected override PdfWriterDictionary CreateDictionary(PdfObjectCollection objects)
        {
            PdfWriterDictionary dictionary = base.CreateDictionary(objects);
            dictionary.AddName("Subtype", "Image");
            dictionary.Add("Width", this.width);
            dictionary.Add("Height", this.height);
            if (this.isMask)
            {
                dictionary.Add("ImageMask", this.isMask);
            }
            else
            {
                if (this.colorSpace != null)
                {
                    dictionary.Add("ColorSpace", this.colorSpace.Write(objects));
                }
                dictionary.Add("BitsPerComponent", this.bitsPerComponent, 0);
                if (this.mask != null)
                {
                    dictionary.Add("Mask", this.mask);
                }
                else if (this.colorKeyMask != null)
                {
                    dictionary.Add("Mask", PdfRange.ToArray(this.colorKeyMask));
                }
            }
            if (this.intent != null)
            {
                dictionary.AddName("Intent", PdfEnumToStringConverter.Convert<PdfRenderingIntent>(this.intent.Value, false));
            }
            dictionary.Add("Decode", PdfRange.ToArray(this.decode));
            dictionary.Add("Interpolate", this.interpolate);
            dictionary.Add("SMask", this.sMask);
            dictionary.Add("Matte", this.matte);
            dictionary.Add("SMaskInData", (int) this.SMaskInData, 0);
            return dictionary;
        }

        private PdfRenderingIntent? CreateIntent(PdfReaderDictionary dictionary)
        {
            string name = dictionary.GetName("Intent");
            if (!string.IsNullOrEmpty(name))
            {
                return new PdfRenderingIntent?(PdfEnumToStringConverter.Parse<PdfRenderingIntent>(name, true));
            }
            return null;
        }

        protected override PdfStream CreateStream(PdfObjectCollection objects) => 
            this.compressedData.CreateWriterStream(this.CreateDictionary(objects));

        internal PdfImageData GetActualData(PdfImageParameters parameters, bool invertRGB)
        {
            PdfImageColor[] colorArray;
            if (this.sMask != null)
            {
                return this.ApplyMask(this.sMask.GetTransformedData(parameters).ScanlineSource, parameters, this.sMask.Matte);
            }
            if (this.HasValidStencilMask)
            {
                return this.ApplyMask(new PdfInvertedImageScanlineSource(this.mask.GetTransformedData(parameters).ScanlineSource), parameters, null);
            }
            PdfScanlineTransformationResult transformedData = this.GetTransformedData(parameters);
            PdfPixelFormat pixelFormat = transformedData.PixelFormat;
            int width = parameters.Width;
            int height = parameters.Height;
            if (transformedData.ScanlineSource.HasAlpha)
            {
                IPdfImageScanlineSource scanlineSource = transformedData.ScanlineSource;
                if (pixelFormat == PdfPixelFormat.Gray8bit)
                {
                    scanlineSource = new PdfGrayToRGBImageScanlineSource(scanlineSource, width);
                }
                return new PdfImageData(new PdfColorKeyMaskedImageDataSource(scanlineSource, width), width, height, width * 4, PdfPixelFormat.Argb32bpp, null);
            }
            if (pixelFormat != PdfPixelFormat.Gray8bit)
            {
                colorArray = null;
            }
            else
            {
                colorArray = new PdfImageColor[0x100];
                for (int i = 0; i < 0x100; i++)
                {
                    colorArray[i] = new PdfImageColor((byte) i, (byte) i, (byte) i);
                }
            }
            int num3 = width * transformedData.ScanlineSource.ComponentsCount;
            int num4 = num3 % 4;
            int stride = (num4 > 0) ? ((num3 + 4) - num4) : num3;
            return new PdfImageData(!invertRGB ? ((PdfImageDataSource) new PdfRGBImageDataSource(transformedData.ScanlineSource, width, stride)) : ((PdfImageDataSource) new PdfBGRImageDataSource(transformedData.ScanlineSource, width, stride)), width, height, stride, pixelFormat, colorArray);
        }

        internal PdfImageParameters GetActualSize(PdfImageParameters parameters)
        {
            int width = Math.Min(this.width, parameters.Width);
            int height = Math.Min(this.height, parameters.Height);
            if ((width == this.width) && (height == this.height))
            {
                int num3 = 0;
                int num4 = 0;
                if (this.HasValidStencilMask)
                {
                    num3 = this.mask.width;
                    num4 = this.mask.height;
                }
                else if (this.sMask != null)
                {
                    num3 = this.sMask.width;
                    num4 = this.sMask.height;
                }
                if ((num3 > this.width) || (num4 > this.height))
                {
                    return new PdfImageParameters(num3, num4, parameters.ShouldInterpolate);
                }
            }
            return (((this.width * this.height) > 0x15f90) ? new PdfImageParameters(width, height, parameters.ShouldInterpolate) : new PdfImageParameters(this.width, this.height, parameters.ShouldInterpolate));
        }

        private static int GetDimension(PdfReaderDictionary dictionary, string key)
        {
            int? integer = dictionary.GetInteger(key);
            if (integer == null)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            return Math.Max(0, integer.Value);
        }

        internal IPdfImageScanlineSource GetInterpolatedScanlineSource(IPdfImageScanlineSource data, PdfImageParameters parameters)
        {
            int width = parameters.Width;
            int height = parameters.Height;
            return (((width != this.width) || (height != this.height)) ? PdfImageScanlineSourceFactory.CreateInterpolator(data, width, height, this.width, this.height, parameters.ShouldInterpolate) : data);
        }

        private PdfScanlineTransformationResult GetTransformedData(PdfImageParameters parameters)
        {
            IPdfImageScanlineSource source;
            IList<PdfFilter> filters = this.Filters;
            int num = filters.Count - 1;
            if (num < 0)
            {
                source = PdfCommonImageScanlineSource.CreateImageScanlineSource(this.Data, this, this.colorSpace.ComponentsCount);
            }
            else
            {
                byte[] data = this.Data;
                int num2 = 0;
                while (true)
                {
                    if (num2 >= num)
                    {
                        PdfScanlineTransformationResult result = filters[num].CreateScanlineSource(this, (this.colorSpace != null) ? this.colorSpace.ComponentsCount : 1, data);
                        if (this.colorSpace != null)
                        {
                            source = result.ScanlineSource;
                            break;
                        }
                        IPdfImageScanlineSource scanlineSource = result.ScanlineSource;
                        return new PdfScanlineTransformationResult(this.GetInterpolatedScanlineSource(scanlineSource, parameters), result.PixelFormat);
                    }
                    data = filters[num2].Decode(data);
                    num2++;
                }
            }
            return this.colorSpace.Transform(this, source, parameters);
        }

        public int Width =>
            this.width;

        public int Height =>
            this.height;

        public PdfColorSpace ColorSpace =>
            this.colorSpace;

        public int BitsPerComponent =>
            this.bitsPerComponent;

        public PdfRenderingIntent? Intent =>
            this.intent;

        public bool IsMask =>
            this.isMask;

        public PdfImage Mask =>
            this.mask;

        public IList<PdfRange> Decode =>
            this.decode;

        public bool Interpolate =>
            this.interpolate;

        public PdfImage SMask =>
            this.sMask;

        public IList<double> Matte =>
            this.matte;

        public IList<PdfFilter> Filters =>
            this.compressedData.Filters;

        public byte[] Data =>
            this.compressedData.Data;

        internal PdfCompressedData CompressedData =>
            this.compressedData;

        internal PdfImageSMaskInDataType SMaskInData { get; }

        public IList<PdfRange> ColorKeyMask
        {
            get => 
                this.colorKeyMask;
            internal set => 
                this.colorKeyMask = value;
        }

        private bool HasValidStencilMask =>
            (this.mask != null) && ((this.mask.BitsPerComponent == 1) && ((this.mask.ColorSpace == null) || (this.mask.ColorSpace.ComponentsCount == 1)));
    }
}

