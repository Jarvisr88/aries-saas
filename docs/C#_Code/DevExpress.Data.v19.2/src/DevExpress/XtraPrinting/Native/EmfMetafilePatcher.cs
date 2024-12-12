namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.Data.Utils;
    using System;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Security;

    public class EmfMetafilePatcher
    {
        [SecuritySafeCritical]
        public static Metafile PatchResolution(Metafile metafile, int newResolution)
        {
            byte[] buffer = new ImageTool().ToArray(metafile);
            EmfHeaderWrapper wrapper = new EmfHeaderWrapper(buffer) {
                logicalDpiX = newResolution,
                logicalDpiY = newResolution
            };
            double num = 96.0;
            double num2 = 96.0;
            int num3 = Round((25.4 / num) * wrapper.szlDevice_cx);
            int num4 = Round((25.4 / num2) * wrapper.szlDevice_cy);
            if ((wrapper.logicalDpiX == newResolution) && ((num3 == wrapper.szlMillimeters_cx) && (num4 == wrapper.szlMillimeters_cy)))
            {
                return metafile;
            }
            double num5 = ((double) num3) / ((double) wrapper.szlMillimeters_cx);
            double num6 = ((double) num4) / ((double) wrapper.szlMillimeters_cy);
            wrapper.rclFrame_left = Round(wrapper.rclFrame_left * num5);
            wrapper.rclFrame_top = Round(wrapper.rclFrame_top * num6);
            wrapper.rclFrame_right = Round(wrapper.rclFrame_right * num5);
            wrapper.rclFrame_bottom = Round(wrapper.rclFrame_bottom * num6);
            wrapper.szlMillimeters_cx = num3;
            wrapper.szlMillimeters_cy = num4;
            if (wrapper.nSize >= 0x6c)
            {
                wrapper.szlMicrometers_cx = Round(((25.4 / num) * 1000.0) * wrapper.szlDevice_cx);
                wrapper.szlMicrometers_cy = Round(((25.4 / num2) * 1000.0) * wrapper.szlDevice_cy);
            }
            return new Metafile(new MemoryStream(buffer));
        }

        private static int Round(double value) => 
            (int) Math.Round(value, MidpointRounding.AwayFromZero);

        private class EmfHeaderWrapper
        {
            private byte[] buffer;
            private const int nSize_Offset = 4;
            private const int szlDevice_cx_Offset = 0x48;
            private const int szlDevice_cy_Offset = 0x4c;
            private const int szlMillimeters_cx_Offset = 80;
            private const int szlMillimeters_cy_Offset = 0x54;
            private const int rclFrame_left_Offset = 0x18;
            private const int rclFrame_top_Offset = 0x1c;
            private const int rclFrame_right_Offset = 0x20;
            private const int rclFrame_bottom_Offset = 0x24;
            private const int szlMicrometers_cx_Offset = 100;
            private const int szlMicrometers_cy_Offset = 0x68;

            public EmfHeaderWrapper(byte[] buffer)
            {
                this.buffer = buffer;
            }

            private int GetInt(int offset) => 
                BitConverter.ToInt32(this.buffer, offset);

            private void SetInt(int offset, int value)
            {
                Array.Copy(BitConverter.GetBytes(value), 0, this.buffer, offset, 4);
            }

            private int logicalDpiX_Offset =>
                this.nSize + 0x24;

            private int logicalDpiY_Offset =>
                this.nSize + 40;

            public int nSize
            {
                get => 
                    this.GetInt(4);
                set => 
                    this.SetInt(4, value);
            }

            public int logicalDpiX
            {
                get => 
                    this.GetInt(this.logicalDpiX_Offset);
                set => 
                    this.SetInt(this.logicalDpiX_Offset, value);
            }

            public int logicalDpiY
            {
                get => 
                    this.GetInt(this.logicalDpiY_Offset);
                set => 
                    this.SetInt(this.logicalDpiY_Offset, value);
            }

            public int szlDevice_cx
            {
                get => 
                    this.GetInt(0x48);
                set => 
                    this.SetInt(0x48, value);
            }

            public int szlDevice_cy
            {
                get => 
                    this.GetInt(0x4c);
                set => 
                    this.SetInt(0x4c, value);
            }

            public int szlMillimeters_cx
            {
                get => 
                    this.GetInt(80);
                set => 
                    this.SetInt(80, value);
            }

            public int szlMillimeters_cy
            {
                get => 
                    this.GetInt(0x54);
                set => 
                    this.SetInt(0x54, value);
            }

            public int rclFrame_left
            {
                get => 
                    this.GetInt(0x18);
                set => 
                    this.SetInt(0x18, value);
            }

            public int rclFrame_top
            {
                get => 
                    this.GetInt(0x1c);
                set => 
                    this.SetInt(0x1c, value);
            }

            public int rclFrame_right
            {
                get => 
                    this.GetInt(0x20);
                set => 
                    this.SetInt(0x20, value);
            }

            public int rclFrame_bottom
            {
                get => 
                    this.GetInt(0x24);
                set => 
                    this.SetInt(0x24, value);
            }

            public int szlMicrometers_cx
            {
                get => 
                    this.GetInt(100);
                set => 
                    this.SetInt(100, value);
            }

            public int szlMicrometers_cy
            {
                get => 
                    this.GetInt(0x68);
                set => 
                    this.SetInt(0x68, value);
            }
        }
    }
}

