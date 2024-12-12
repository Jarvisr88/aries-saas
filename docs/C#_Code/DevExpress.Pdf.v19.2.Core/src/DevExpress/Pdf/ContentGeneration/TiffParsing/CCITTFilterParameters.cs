namespace DevExpress.Pdf.ContentGeneration.TiffParsing
{
    using DevExpress.Pdf;
    using System;
    using System.Runtime.CompilerServices;

    public class CCITTFilterParameters
    {
        private const int twoDimensionalFlag = 1;
        private const int uncompressedModeFlag = 2;
        private const int byteAllignFlag = 4;
        private const int centimeterMark = 3;

        private CCITTFilterParameters()
        {
        }

        public static CCITTFilterParameters Create(TiffParser parser)
        {
            long? nullable5;
            long num2;
            long? nullable6;
            CCITTFilterParameters parameters = new CCITTFilterParameters();
            long? nullable = GetValue(parser, TiffTag.Compression);
            long? nullable3 = nullable;
            long num = 4L;
            if ((nullable3.GetValueOrDefault() == num) ? (nullable3 != null) : false)
            {
                long? nullable1;
                nullable5 = GetValue(parser, TiffTag.T6Options);
                num2 = 2L;
                if (nullable5 != null)
                {
                    nullable1 = new long?(nullable5.GetValueOrDefault() & num2);
                }
                else
                {
                    nullable6 = null;
                    nullable1 = nullable6;
                }
                nullable3 = nullable1;
                num = 2L;
                if ((nullable3.GetValueOrDefault() == num) ? (nullable3 != null) : false)
                {
                    return null;
                }
                parameters.EncodingScheme = PdfCCITTFaxEncodingScheme.TwoDimensional;
                parameters.EndOfLine = false;
                parameters.EncodedByteAlign = false;
            }
            else
            {
                nullable3 = nullable;
                num = 3L;
                if (!((nullable3.GetValueOrDefault() == num) ? (nullable3 != null) : false))
                {
                    return null;
                }
                long? nullable7 = GetValue(parser, TiffTag.T4Options);
                if (nullable7 == null)
                {
                    parameters.EncodingScheme = PdfCCITTFaxEncodingScheme.OneDimensional;
                    parameters.EncodedByteAlign = false;
                }
                else
                {
                    long? nullable8;
                    long? nullable9;
                    long? nullable10;
                    nullable5 = nullable7;
                    num2 = 2L;
                    if (nullable5 != null)
                    {
                        nullable8 = new long?(nullable5.GetValueOrDefault() & num2);
                    }
                    else
                    {
                        nullable6 = null;
                        nullable8 = nullable6;
                    }
                    nullable3 = nullable8;
                    num = 2L;
                    if ((nullable3.GetValueOrDefault() == num) ? (nullable3 != null) : false)
                    {
                        return null;
                    }
                    nullable5 = nullable7;
                    num2 = 1L;
                    if (nullable5 != null)
                    {
                        nullable9 = new long?(nullable5.GetValueOrDefault() & num2);
                    }
                    else
                    {
                        nullable6 = null;
                        nullable9 = nullable6;
                    }
                    nullable3 = nullable9;
                    num = 1L;
                    if (!((nullable3.GetValueOrDefault() == num) ? (nullable3 != null) : false))
                    {
                        parameters.EncodingScheme = PdfCCITTFaxEncodingScheme.OneDimensional;
                    }
                    else
                    {
                        parameters.EncodingScheme = PdfCCITTFaxEncodingScheme.Mixed;
                        ITiffValue[] directoryEntryValue = parser.GetDirectoryEntryValue(TiffTag.YResolution);
                        if (directoryEntryValue == null)
                        {
                            return null;
                        }
                        double num3 = directoryEntryValue[0].AsDouble();
                        nullable3 = GetValue(parser, TiffTag.ResolutionUnit);
                        num = 3L;
                        if ((nullable3.GetValueOrDefault() == num) ? (nullable3 != null) : false)
                        {
                            num3 *= 2.54;
                        }
                        parameters.TwoDimensionalLineCount = (num3 > 150.0) ? 3 : 1;
                    }
                    nullable5 = nullable7;
                    num2 = 4L;
                    if (nullable5 != null)
                    {
                        nullable10 = new long?(nullable5.GetValueOrDefault() & num2);
                    }
                    else
                    {
                        nullable6 = null;
                        nullable10 = nullable6;
                    }
                    nullable3 = nullable10;
                    num = 4L;
                    parameters.EncodedByteAlign = (nullable3.GetValueOrDefault() == num) ? (nullable3 != null) : false;
                }
                parameters.EndOfLine = true;
            }
            long? nullable2 = GetValue(parser, TiffTag.ImageWidth);
            if (nullable2 == null)
            {
                return null;
            }
            parameters.Columns = (int) nullable2.Value;
            CCITTFilterParameters parameters1 = parameters;
            if (<>c.<>9__6_0 == null)
            {
                CCITTFilterParameters local1 = parameters;
                parameters1 = (CCITTFilterParameters) (<>c.<>9__6_0 = v => (int) v);
            }
            <>c.<>9__6_0.Rows = GetValueOrDefault<int>(0x101, (TiffTag) parser, (Func<long, int>) parameters1, 0);
            Func<long, bool> getValue = <>c.<>9__6_1;
            if (<>c.<>9__6_1 == null)
            {
                Func<long, bool> local2 = <>c.<>9__6_1;
                getValue = <>c.<>9__6_1 = v => v == 0L;
            }
            parameters.BlackIs1 = !GetValueOrDefault<bool>(parser, TiffTag.PhotometricInterpretation, getValue, false);
            return parameters;
        }

        private static long? GetValue(TiffParser parser, TiffTag tag)
        {
            ITiffValue[] directoryEntryValue = parser.GetDirectoryEntryValue(tag);
            if (directoryEntryValue != null)
            {
                return new long?(directoryEntryValue[0].AsUint());
            }
            return null;
        }

        private static T GetValueOrDefault<T>(TiffParser parser, TiffTag tag, Func<long, T> getValue, T defaultValue)
        {
            long? nullable = GetValue(parser, tag);
            return ((nullable == null) ? defaultValue : getValue(nullable.Value));
        }

        public PdfCCITTFaxEncodingScheme EncodingScheme { get; private set; }

        public int TwoDimensionalLineCount { get; private set; }

        public bool EndOfLine { get; private set; }

        public bool EncodedByteAlign { get; private set; }

        public int Columns { get; private set; }

        public int Rows { get; private set; }

        public bool EndOfBlock =>
            true;

        public bool BlackIs1 { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly CCITTFilterParameters.<>c <>9 = new CCITTFilterParameters.<>c();
            public static Func<long, int> <>9__6_0;
            public static Func<long, bool> <>9__6_1;

            internal int <Create>b__6_0(long v) => 
                (int) v;

            internal bool <Create>b__6_1(long v) => 
                v == 0L;
        }
    }
}

