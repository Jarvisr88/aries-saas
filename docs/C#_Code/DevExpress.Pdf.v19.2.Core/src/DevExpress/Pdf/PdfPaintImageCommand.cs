namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class PdfPaintImageCommand : PdfCommand
    {
        private readonly PdfImage image;

        internal PdfPaintImageCommand(PdfImage image)
        {
            this.image = image;
        }

        private object ConvertFilter(object obj)
        {
            PdfName name = obj as PdfName;
            if (name != null)
            {
                string s = name.Name;
                uint num = <PrivateImplementationDetails>.ComputeStringHash(s);
                if (num <= 0x2c7c4e77)
                {
                    if (num == 0x180cd636)
                    {
                        if (s == "LZWDecode")
                        {
                            return new PdfName("LZW");
                        }
                    }
                    else if (num != 0x18481007)
                    {
                        if ((num == 0x2c7c4e77) && (s == "ASCIIHexDecode"))
                        {
                            return new PdfName("AHx");
                        }
                    }
                    else if (s == "FlateDecode")
                    {
                        return new PdfName("Fl");
                    }
                }
                else if (num <= 0x6b2fbed1)
                {
                    if (num != 0x5cc248eb)
                    {
                        if ((num == 0x6b2fbed1) && (s == "ASCII85Decode"))
                        {
                            return new PdfName("A85");
                        }
                    }
                    else if (s == "CCITTFaxDecode")
                    {
                        return new PdfName("CCF");
                    }
                }
                else if (num != 0xa2aa4b86)
                {
                    if ((num == 0xf1b0efe8) && (s == "RunLengthDecode"))
                    {
                        return new PdfName("RL");
                    }
                }
                else if (s == "DCTDecode")
                {
                    return new PdfName("DCT");
                }
            }
            return obj;
        }

        protected internal override void Execute(PdfCommandInterpreter interpreter)
        {
            interpreter.DrawImage(this.image);
        }

        protected internal override void Write(PdfResources resources, PdfDocumentStream writer)
        {
            object obj2;
            object obj3;
            writer.WriteSpace();
            writer.WriteString("BI");
            writer.WriteSpace();
            PdfColorSpace colorSpace = this.image.ColorSpace;
            if (colorSpace != null)
            {
                writer.WriteName(new PdfName("CS"));
                writer.WriteSpace();
                object obj4 = resources.FindColorSpaceName(colorSpace);
                if (obj4 == null)
                {
                    obj4 = colorSpace.ToWritableObject(null);
                    PdfName name = obj4 as PdfName;
                    if (name != null)
                    {
                        string str = name.Name;
                        if (str == "DeviceGray")
                        {
                            obj4 = new PdfName("G");
                        }
                        else if (str == "DeviceRGB")
                        {
                            obj4 = new PdfName("RGB");
                        }
                        else if (str == "DeviceCMYK")
                        {
                            obj4 = new PdfName("CMYK");
                        }
                    }
                }
                writer.WriteObject(obj4, -1);
                writer.WriteSpace();
            }
            writer.WriteName(new PdfName("W"));
            writer.WriteSpace();
            writer.WriteInt(this.image.Width);
            writer.WriteSpace();
            writer.WriteName(new PdfName("H"));
            writer.WriteSpace();
            writer.WriteInt(this.image.Height);
            writer.WriteSpace();
            writer.WriteName(new PdfName("BPC"));
            writer.WriteSpace();
            writer.WriteInt(this.image.BitsPerComponent);
            writer.WriteSpace();
            writer.WriteName(new PdfName("D"));
            writer.WriteSpace();
            writer.WriteObject(PdfRange.ToArray(this.image.Decode), -1);
            writer.WriteSpace();
            writer.WriteName(new PdfName("IM"));
            writer.WriteSpace();
            writer.WriteObject(this.image.IsMask, -1);
            writer.WriteSpace();
            if (this.image.Intent != null)
            {
                writer.WriteName(new PdfName("Intent"));
                writer.WriteSpace();
                writer.WriteName(new PdfName(PdfEnumToStringConverter.Convert<PdfRenderingIntent>(this.image.Intent.Value, false)));
                writer.WriteSpace();
            }
            writer.WriteName(new PdfName("I"));
            writer.WriteSpace();
            writer.WriteObject(this.image.Interpolate, -1);
            writer.WriteSpace();
            PdfWriterDictionary dictionary = new PdfWriterDictionary(null);
            this.image.CompressedData.AddFilters(dictionary);
            if (dictionary.TryGetValue("Filter", out obj2))
            {
                writer.WriteName(new PdfName("F"));
                writer.WriteSpace();
                IEnumerable enumerable = obj2 as IEnumerable;
                if (enumerable == null)
                {
                    obj2 = this.ConvertFilter(obj2);
                }
                else
                {
                    IList<object> list = new List<object>();
                    foreach (object obj5 in enumerable)
                    {
                        list.Add(this.ConvertFilter(obj5));
                    }
                    obj2 = new PdfWritableArray(list);
                }
                writer.WriteObject(obj2, -1);
                writer.WriteSpace();
            }
            if (dictionary.TryGetValue("DecodeParms", out obj3))
            {
                IEnumerable enumerable2 = obj3 as IEnumerable;
                if (enumerable2 != null)
                {
                    foreach (object obj6 in enumerable2)
                    {
                        if (obj6 != null)
                        {
                            writer.WriteName(new PdfName("DP"));
                            writer.WriteSpace();
                            writer.WriteObject(obj3, -1);
                            writer.WriteSpace();
                            break;
                        }
                    }
                }
            }
            writer.WriteString("ID");
            writer.WriteSpace();
            writer.WriteBytes(this.image.Data);
            writer.WriteSpace();
            writer.WriteString("EI");
        }

        public PdfImage Image =>
            this.image;
    }
}

