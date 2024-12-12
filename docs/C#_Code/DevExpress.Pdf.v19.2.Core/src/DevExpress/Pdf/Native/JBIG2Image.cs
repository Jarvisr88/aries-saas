namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.CompilerServices;

    public class JBIG2Image
    {
        private int width;
        private int height;
        private int stride;
        private byte[] data;
        private readonly Dictionary<int, JBIG2SegmentHeader> segments;
        private readonly Dictionary<int, JBIG2SegmentHeader> globalSegments;

        public JBIG2Image()
        {
            this.segments = new Dictionary<int, JBIG2SegmentHeader>();
            this.globalSegments = new Dictionary<int, JBIG2SegmentHeader>();
        }

        public JBIG2Image(int width, int height)
        {
            this.segments = new Dictionary<int, JBIG2SegmentHeader>();
            this.globalSegments = new Dictionary<int, JBIG2SegmentHeader>();
            this.width = width;
            this.height = height;
            this.stride = ((width - 1) >> 3) + 1;
            this.data = new byte[this.stride * height];
        }

        internal void Clear(bool color)
        {
            for (int i = 0; i < this.data.Length; i++)
            {
                this.data[i] = color ? ((byte) 0xff) : ((byte) 0);
            }
        }

        internal void Composite(JBIG2Image image, int x, int y, int composeOperator)
        {
            if ((x == 0) && ((y == 0) && ((image.width == this.width) && (image.height == this.height))))
            {
                this.CompositeFast(image, composeOperator);
            }
            else if (composeOperator != 0)
            {
                this.CompositeGeneral(image, x, y, composeOperator);
            }
            else
            {
                this.CompositeOrFast(image, x, y);
            }
        }

        private void CompositeFast(JBIG2Image image, int composeOperator)
        {
            Func<byte, byte, byte> func = CreateComposeOperatorByte(composeOperator);
            for (int i = 0; i < this.data.Length; i++)
            {
                this.data[i] = func(this.data[i], image.data[i]);
            }
        }

        private void CompositeGeneral(JBIG2Image image, int x, int y, int composeOperator)
        {
            int width = image.width;
            int height = image.height;
            int num3 = 0;
            int num4 = 0;
            if (x < 0)
            {
                num3 += -x;
                width -= -x;
                x = 0;
            }
            if (y < 0)
            {
                num4 += -y;
                height -= -y;
                y = 0;
            }
            if ((x + width) >= this.width)
            {
                width = this.width - x;
            }
            if ((y + height) >= this.height)
            {
                height = this.height - y;
            }
            Func<int, int, int> func = CreateComposeOperator(composeOperator);
            int num5 = 0;
            while (num5 < height)
            {
                int num6 = 0;
                while (true)
                {
                    if (num6 >= width)
                    {
                        num5++;
                        break;
                    }
                    this.SetPixel(num6 + x, num5 + y, func(image.GetPixel(num6 + num3, num5 + num4), this.GetPixel(num6 + x, num5 + y)));
                    num6++;
                }
            }
        }

        private unsafe void CompositeOrFast(JBIG2Image image, int x, int y)
        {
            int width = image.width;
            int height = image.height;
            int num5 = 0;
            int index = num5;
            if (x < 0)
            {
                width += x;
                x = 0;
            }
            if (y < 0)
            {
                height += y;
                y = 0;
            }
            width = ((x + width) < this.width) ? width : (this.width - x);
            height = ((y + height) < this.height) ? height : (this.height - y);
            if ((width > 0) && (height > 0))
            {
                int num;
                int num7 = x >> 3;
                int num8 = ((x + width) - 1) >> 3;
                int num9 = x & 7;
                int num10 = (y * this.stride) + num7;
                int num11 = num10;
                if ((num11 < 0) || ((num7 > this.stride) || (((height * this.stride) < 0) || (((num11 - num7) + (height * this.stride)) > (this.height * this.stride)))))
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                if (num7 == num8)
                {
                    num = 0x100 - (0x100 >> (width & 0x1f));
                    for (int i = 0; i < height; i++)
                    {
                        byte* numPtr1 = &(this.data[num11]);
                        numPtr1[0] = (byte) (numPtr1[0] | ((byte) ((image.data[index] & num) >> (num9 & 0x1f))));
                        num11 += this.stride;
                        index += image.stride;
                    }
                }
                else
                {
                    int num2;
                    if (num9 == 0)
                    {
                        num2 = ((width & 7) != 0) ? (0x100 - (1 << ((8 - (width & 7)) & 0x1f))) : 0xff;
                        int num13 = 0;
                        while (num13 < height)
                        {
                            int num14 = num7;
                            while (true)
                            {
                                if (num14 >= num8)
                                {
                                    byte* numPtr3 = &(this.data[num11]);
                                    numPtr3[0] = (byte) (numPtr3[0] | ((byte) (image.data[index] & num2)));
                                    num11 = num10 + this.stride;
                                    index = num5 + image.stride;
                                    num13++;
                                    break;
                                }
                                byte* numPtr2 = &(this.data[num11++]);
                                numPtr2[0] = (byte) (numPtr2[0] | image.data[index++]);
                                num14++;
                            }
                        }
                    }
                    else
                    {
                        bool flag = ((width + 7) >> 3) < ((((x + width) + 7) >> 3) - (x >> 3));
                        num = 0x100 - (1 << (num9 & 0x1f));
                        num2 = !flag ? (0x100 - (0x100 >> ((width & 7) & 0x1f))) : ((0x100 - (0x100 >> (((x + width) & 7) & 0x1f))) >> ((8 - num9) & 0x1f));
                        int num15 = 0;
                        while (num15 < height)
                        {
                            byte* numPtr4 = &(this.data[num11++]);
                            numPtr4[0] = (byte) (numPtr4[0] | ((byte) ((image.data[index] & num) >> (num9 & 0x1f))));
                            int num16 = num7;
                            while (true)
                            {
                                if (num16 >= (num8 - 1))
                                {
                                    if (flag)
                                    {
                                        byte* numPtr7 = &(this.data[num11]);
                                        numPtr7[0] = (byte) (numPtr7[0] | ((byte) ((image.data[index] & num2) << ((8 - num9) & 0x1f))));
                                    }
                                    else
                                    {
                                        byte* numPtr8 = &(this.data[num11]);
                                        numPtr8[0] = (byte) (numPtr8[0] | ((byte) (((image.data[index] & ~num) << ((8 - num9) & 0x1f)) | ((image.data[index + 1] & num2) >> (num9 & 0x1f)))));
                                    }
                                    num11 = num10 + this.stride;
                                    index = num5 + image.stride;
                                    num15++;
                                    break;
                                }
                                byte* numPtr5 = &(this.data[num11]);
                                numPtr5[0] = (byte) (numPtr5[0] | ((byte) ((image.data[index++] & ~num) << ((8 - num9) & 0x1f))));
                                byte* numPtr6 = &(this.data[num11++]);
                                numPtr6[0] = (byte) (numPtr6[0] | ((byte) ((image.data[index] & num) >> (num9 & 0x1f))));
                                num16++;
                            }
                        }
                    }
                }
            }
        }

        private static Func<int, int, int> CreateComposeOperator(int value)
        {
            switch (value)
            {
                case 0:
                    return (<>c.<>9__2_0 ??= (a, b) => (a | b));

                case 1:
                    return (<>c.<>9__2_1 ??= (a, b) => (a & b));

                case 2:
                    return (<>c.<>9__2_2 ??= (a, b) => (a ^ b));

                case 3:
                    return (<>c.<>9__2_3 ??= (a, b) => (~(a ^ b) & 1));

                case 4:
                    return (<>c.<>9__2_4 ??= (a, b) => a);
            }
            PdfDocumentStructureReader.ThrowIncorrectDataException();
            return null;
        }

        private static Func<byte, byte, byte> CreateComposeOperatorByte(int value)
        {
            switch (value)
            {
                case 0:
                    return (<>c.<>9__1_0 ??= (a, b) => ((byte) (a | b)));

                case 1:
                    return (<>c.<>9__1_1 ??= (a, b) => ((byte) (a & b)));

                case 2:
                    return (<>c.<>9__1_2 ??= (a, b) => ((byte) (a ^ b)));

                case 3:
                    return (<>c.<>9__1_3 ??= (a, b) => ((byte) ~(a ^ b)));

                case 4:
                    return (<>c.<>9__1_4 ??= (a, b) => a);
            }
            PdfDocumentStructureReader.ThrowIncorrectDataException();
            return null;
        }

        public static byte[] Decode(byte[] data, Dictionary<int, JBIG2SegmentHeader> globalSegments)
        {
            JBIG2Image image = new JBIG2Image();
            if ((globalSegments != null) && (globalSegments.Count != 0))
            {
                foreach (KeyValuePair<int, JBIG2SegmentHeader> pair in globalSegments)
                {
                    image.GlobalSegments.Add(pair.Key, pair.Value);
                }
            }
            MemoryStream stream = new MemoryStream(data);
            while (true)
            {
                try
                {
                    while (true)
                    {
                        JBIG2SegmentHeader header = new JBIG2SegmentHeader(stream, image);
                        image.Segments.Add(header.Number, header);
                        if (!header.EndOfFile && (stream.Position <= (stream.Length - 1L)))
                        {
                            break;
                        }
                        foreach (JBIG2SegmentHeader header2 in image.Segments.Values)
                        {
                            header2.Process();
                        }
                        for (int i = 0; i < image.Data.Length; i++)
                        {
                            image.Data[i] = (byte) (0xff - image.Data[i]);
                        }
                        return image.Data;
                    }
                }
                finally
                {
                    if (stream != null)
                    {
                        stream.Dispose();
                    }
                }
            }
        }

        public int GetPixel(int x, int y)
        {
            int index = (x >> 3) + (y * this.stride);
            int num2 = 7 - (x & 7);
            return (((x < 0) || ((x >= this.width) || ((y < 0) || (y >= this.height)))) ? 0 : ((this.data[index] >> (num2 & 0x1f)) & 1));
        }

        public void SetDimensions(int width, int height, bool initialPixelValue)
        {
            this.width = width;
            this.height = height;
            this.stride = ((width - 1) >> 3) + 1;
            int num = this.stride * height;
            this.data = new byte[num];
            if (initialPixelValue)
            {
                for (int i = 0; i < num; i++)
                {
                    this.data[i] = 0xff;
                }
            }
        }

        public void SetPixel(int x, int y, int value)
        {
            if (((x >= 0) && (x < this.width)) && ((y >= 0) && (y < this.height)))
            {
                int index = (x >> 3) + (y * this.stride);
                int num2 = 7 - (x & 7);
                this.data[index] = (byte) ((this.data[index] & ((1 << (num2 & 0x1f)) ^ 0xff)) | (value << (num2 & 0x1f)));
            }
        }

        public int Width =>
            this.width;

        public int Height =>
            this.height;

        public int Stride =>
            this.stride;

        public Dictionary<int, JBIG2SegmentHeader> Segments =>
            this.segments;

        public Dictionary<int, JBIG2SegmentHeader> GlobalSegments =>
            this.globalSegments;

        public byte[] Data =>
            this.data;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly JBIG2Image.<>c <>9 = new JBIG2Image.<>c();
            public static Func<byte, byte, byte> <>9__1_0;
            public static Func<byte, byte, byte> <>9__1_1;
            public static Func<byte, byte, byte> <>9__1_2;
            public static Func<byte, byte, byte> <>9__1_3;
            public static Func<byte, byte, byte> <>9__1_4;
            public static Func<int, int, int> <>9__2_0;
            public static Func<int, int, int> <>9__2_1;
            public static Func<int, int, int> <>9__2_2;
            public static Func<int, int, int> <>9__2_3;
            public static Func<int, int, int> <>9__2_4;

            internal int <CreateComposeOperator>b__2_0(int a, int b) => 
                a | b;

            internal int <CreateComposeOperator>b__2_1(int a, int b) => 
                a & b;

            internal int <CreateComposeOperator>b__2_2(int a, int b) => 
                a ^ b;

            internal int <CreateComposeOperator>b__2_3(int a, int b) => 
                ~(a ^ b) & 1;

            internal int <CreateComposeOperator>b__2_4(int a, int b) => 
                a;

            internal byte <CreateComposeOperatorByte>b__1_0(byte a, byte b) => 
                (byte) (a | b);

            internal byte <CreateComposeOperatorByte>b__1_1(byte a, byte b) => 
                (byte) (a & b);

            internal byte <CreateComposeOperatorByte>b__1_2(byte a, byte b) => 
                (byte) (a ^ b);

            internal byte <CreateComposeOperatorByte>b__1_3(byte a, byte b) => 
                (byte) ~(a ^ b);

            internal byte <CreateComposeOperatorByte>b__1_4(byte a, byte b) => 
                a;
        }
    }
}

