namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;

    public class PdfPngPredictor : PdfFlateLZWDecodeFilterPredictor
    {
        public PdfPngPredictor(PdfFlateLZWDecodeFilter filter) : base(filter)
        {
        }

        protected override byte[] Decode(byte[] data)
        {
            int bytesPerPixel = base.BytesPerPixel;
            int rowLength = base.RowLength;
            int length = data.Length;
            int num4 = base.CalcRowCount(length);
            byte[] buffer = new byte[rowLength * num4];
            byte[] buffer2 = new byte[rowLength];
            byte[] array = new byte[bytesPerPixel];
            byte[] buffer4 = new byte[bytesPerPixel];
            int num5 = 0;
            int num6 = 0;
            int num7 = 0;
            while (num5 < num4)
            {
                Array.Clear(array, 0, bytesPerPixel);
                Array.Clear(buffer4, 0, bytesPerPixel);
                PngPrediction prediction = (PngPrediction) data[num6++];
                int index = 0;
                int num9 = 0;
                while (true)
                {
                    byte num11;
                    if ((index >= rowLength) || (num6 >= length))
                    {
                        num5++;
                        break;
                    }
                    byte num10 = data[num6++];
                    switch (prediction)
                    {
                        case PngPrediction.None:
                            num11 = num10;
                            break;

                        case PngPrediction.Sub:
                            num11 = (byte) (array[num9] + num10);
                            break;

                        case PngPrediction.Up:
                            num11 = (byte) (buffer2[index] + num10);
                            break;

                        case PngPrediction.Average:
                            num11 = (byte) (((array[num9] + buffer2[index]) / 2) + num10);
                            break;

                        case PngPrediction.Paeth:
                        {
                            byte num12 = array[num9];
                            byte num13 = buffer2[index];
                            byte num14 = buffer4[num9];
                            int num15 = (num12 + num13) - num14;
                            int num16 = Math.Abs((int) (num15 - num12));
                            int num17 = Math.Abs((int) (num15 - num13));
                            int num18 = Math.Abs((int) (num15 - num14));
                            byte num19 = (num16 > num17) ? ((num17 <= num18) ? num13 : num14) : ((num16 <= num18) ? num12 : num14);
                            num11 = (byte) (num19 + num10);
                            break;
                        }
                        default:
                            PdfDocumentStructureReader.ThrowIncorrectDataException();
                            num11 = num10;
                            break;
                    }
                    buffer[num7++] = num11;
                    buffer4[num9] = buffer2[index];
                    buffer2[index] = num11;
                    array[num9] = num11;
                    if (++num9 == bytesPerPixel)
                    {
                        num9 = 0;
                    }
                    index++;
                }
            }
            return buffer;
        }

        protected override int ActualRowLength =>
            base.RowLength + 1;
    }
}

