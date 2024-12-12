namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.Data.Utils;
    using DevExpress.Utils;
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;

    public class PSConvert
    {
        private static DevExpress.Data.Utils.ImageTool imageTool;

        public static Image ImageFromArray(byte[] buffer) => 
            ImageTool.FromArray(buffer);

        public static byte[] ImageToArray(Image img) => 
            ImageTool.ToArray(img);

        public static byte[] ImageToArray(Image img, ImageFormat format) => 
            ImageTool.ToArray(img, format);

        public static void SaveImage(Image img, Stream stream, ImageFormat format)
        {
            ImageTool.SaveImage(img, stream, format);
        }

        public static HorzAlignment ToHorzAlignment(StringAlignment alignment) => 
            (alignment == StringAlignment.Center) ? HorzAlignment.Center : ((alignment == StringAlignment.Far) ? HorzAlignment.Far : HorzAlignment.Near);

        public static string ToRomanString(int val)
        {
            string[] textArray1 = new string[13];
            textArray1[0] = "M";
            textArray1[1] = "CM";
            textArray1[2] = "D";
            textArray1[3] = "CD";
            textArray1[4] = "C";
            textArray1[5] = "XC";
            textArray1[6] = "L";
            textArray1[7] = "XL";
            textArray1[8] = "X";
            textArray1[9] = "IX";
            textArray1[10] = "V";
            textArray1[11] = "IV";
            textArray1[12] = "I";
            string[] strArray = textArray1;
            int[] numArray = new int[] { 0x3e8, 900, 500, 400, 100, 90, 50, 40, 10, 9, 5, 4, 1 };
            string str = string.Empty;
            int index = 0;
            while (index < 13)
            {
                int num2 = val / numArray[index];
                int num3 = 0;
                while (true)
                {
                    if (num3 >= num2)
                    {
                        val = val % numArray[index];
                        index++;
                        break;
                    }
                    str = str + strArray[index];
                    num3++;
                }
            }
            return str;
        }

        public static StringAlignment ToStringAlignment(HorzAlignment hAlignment) => 
            (hAlignment == HorzAlignment.Center) ? StringAlignment.Center : ((hAlignment == HorzAlignment.Far) ? StringAlignment.Far : StringAlignment.Near);

        public static StringAlignment ToStringAlignment(VertAlignment vAlignment) => 
            (vAlignment == VertAlignment.Center) ? StringAlignment.Center : ((vAlignment == VertAlignment.Bottom) ? StringAlignment.Far : StringAlignment.Near);

        public static VertAlignment ToVertAlignment(StringAlignment alignment) => 
            (alignment == StringAlignment.Center) ? VertAlignment.Center : ((alignment == StringAlignment.Far) ? VertAlignment.Bottom : VertAlignment.Top);

        public static DevExpress.Data.Utils.ImageTool ImageTool
        {
            get
            {
                imageTool ??= new DevExpress.Data.Utils.ImageTool();
                return imageTool;
            }
            set => 
                imageTool = value;
        }
    }
}

