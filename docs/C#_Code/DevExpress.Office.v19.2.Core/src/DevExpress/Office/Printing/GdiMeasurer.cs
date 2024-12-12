namespace DevExpress.Office.Printing
{
    using DevExpress.Office.Drawing;
    using DevExpress.Office.Layout;
    using DevExpress.Office.PInvoke;
    using DevExpress.Utils;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Security;

    public class GdiMeasurer : Measurer
    {
        private readonly DocumentLayoutUnitConverter unitConverter;

        public GdiMeasurer(DocumentLayoutUnitConverter unitConverter)
        {
            Guard.ArgumentNotNull(unitConverter, "unitConverter");
            this.unitConverter = unitConverter;
        }

        public override void Dispose()
        {
        }

        public override RectangleF GetRegionBounds(Region rgn, GraphicsUnit pageUnit) => 
            rgn.GetBounds(this.Graphics);

        [SecuritySafeCritical]
        public override Region[] MeasureCharacterRanges(string text, Font font, RectangleF layoutRect, StringFormat stringFormat, GraphicsUnit pageUnit)
        {
            Region[] regionArray2;
            Rectangle rectangle = Rectangle.Round(layoutRect);
            System.Drawing.Graphics graphics = this.Graphics;
            lock (graphics)
            {
                IntPtr hdc = this.Graphics.GetHdc();
                try
                {
                    IntPtr hGdiObj = GdiPlusFontInfo.CreateGdiFont(font, this.unitConverter, true);
                    IntPtr ptr3 = DevExpress.Office.PInvoke.Win32.SelectObject(hdc, hGdiObj);
                    DevExpress.Office.PInvoke.Win32.GCP_RESULTS lpResults = new DevExpress.Office.PInvoke.Win32.GCP_RESULTS {
                        lStructSize = Marshal.SizeOf(typeof(DevExpress.Office.PInvoke.Win32.GCP_RESULTS)),
                        lpOutString = IntPtr.Zero,
                        lpOrder = IntPtr.Zero,
                        lpDx = IntPtr.Zero,
                        lpCaretPos = Marshal.AllocCoTaskMem(4 * text.Length),
                        lpClass = IntPtr.Zero,
                        lpGlyphs = IntPtr.Zero,
                        nGlyphs = text.Length
                    };
                    if ((DevExpress.Office.PInvoke.Win32.GetCharacterPlacement(hdc, text, text.Length, 0, ref lpResults, DevExpress.Office.PInvoke.Win32.GcpFlags.GCP_LIGATE | DevExpress.Office.PInvoke.Win32.GcpFlags.GCP_USEKERNING) == 0) && (text.Length > 0))
                    {
                        int num = this.MeasureCharactersWithGetCharacterPlacementSlow(hdc, text, ref lpResults);
                    }
                    int length = text.Length;
                    Rectangle[] rectangleArray = new Rectangle[length];
                    int num3 = Marshal.ReadInt32(lpResults.lpCaretPos, 0);
                    int index = 0;
                    while (true)
                    {
                        if (index >= (length - 1))
                        {
                            if (length > 0)
                            {
                                rectangleArray[length - 1] = new Rectangle(rectangle.X + num3, rectangle.Y, rectangle.Width - num3, rectangle.Height);
                            }
                            Marshal.FreeCoTaskMem(lpResults.lpCaretPos);
                            DevExpress.Office.PInvoke.Win32.SelectObject(hdc, ptr3);
                            DevExpress.Office.PInvoke.Win32.DeleteObject(hGdiObj);
                            Region[] regionArray = new Region[rectangleArray.Length];
                            int num6 = 0;
                            while (true)
                            {
                                if (num6 >= rectangleArray.Length)
                                {
                                    regionArray2 = regionArray;
                                    break;
                                }
                                regionArray[num6] = new Region(rectangleArray[num6]);
                                num6++;
                            }
                            break;
                        }
                        int num5 = Marshal.ReadInt32(lpResults.lpCaretPos, (index + 1) * 4);
                        rectangleArray[index] = new Rectangle(rectangle.X + num3, rectangle.Y, num5 - num3, rectangle.Height);
                        num3 = num5;
                        index++;
                    }
                }
                finally
                {
                    this.Graphics.ReleaseHdc(hdc);
                }
            }
            return regionArray2;
        }

        [SecuritySafeCritical]
        private int MeasureCharactersWithGetCharacterPlacementSlow(IntPtr hdc, string text, ref DevExpress.Office.PInvoke.Win32.GCP_RESULTS gcpResults)
        {
            int num = Math.Max(1, (int) Math.Ceiling((double) (((double) text.Length) / 2.0)));
            int num2 = num;
            int num3 = 0;
            while (num3 < 3)
            {
                gcpResults.lpCaretPos = Marshal.ReAllocCoTaskMem(gcpResults.lpCaretPos, 4 * (text.Length + num2));
                gcpResults.nGlyphs = text.Length + num2;
                int num4 = DevExpress.Office.PInvoke.Win32.GetCharacterPlacement(hdc, text, text.Length, 0, ref gcpResults, DevExpress.Office.PInvoke.Win32.GcpFlags.GCP_LIGATE | DevExpress.Office.PInvoke.Win32.GcpFlags.GCP_USEKERNING);
                if (num4 != 0)
                {
                    return num4;
                }
                num3++;
                num2 += num;
            }
            return 0;
        }

        [SecuritySafeCritical]
        public override SizeF MeasureString(string text, Font font, PointF location, StringFormat stringFormat, GraphicsUnit pageUnit)
        {
            SizeF ef;
            System.Drawing.Graphics graphics = this.Graphics;
            lock (graphics)
            {
                IntPtr hdc = this.Graphics.GetHdc();
                try
                {
                    IntPtr hGdiObj = GdiPlusFontInfo.CreateGdiFont(font, this.unitConverter, true);
                    IntPtr ptr3 = DevExpress.Office.PInvoke.Win32.SelectObject(hdc, hGdiObj);
                    DevExpress.Office.PInvoke.Win32.GCP_RESULTS lpResults = new DevExpress.Office.PInvoke.Win32.GCP_RESULTS {
                        lStructSize = Marshal.SizeOf(typeof(DevExpress.Office.PInvoke.Win32.GCP_RESULTS)),
                        lpOutString = IntPtr.Zero,
                        lpOrder = IntPtr.Zero,
                        lpDx = Marshal.AllocCoTaskMem(4 * text.Length),
                        lpCaretPos = Marshal.AllocCoTaskMem(4 * text.Length),
                        lpClass = IntPtr.Zero,
                        lpGlyphs = IntPtr.Zero,
                        nGlyphs = text.Length
                    };
                    int num = DevExpress.Office.PInvoke.Win32.GetCharacterPlacement(hdc, text, text.Length, 0, ref lpResults, DevExpress.Office.PInvoke.Win32.GcpFlags.GCP_LIGATE | DevExpress.Office.PInvoke.Win32.GcpFlags.GCP_USEKERNING);
                    if ((num == 0) && (text.Length > 0))
                    {
                        num = this.MeasureWithGetCharacterPlacementSlow(hdc, text, ref lpResults);
                    }
                    int num2 = num & 0xffff;
                    int num3 = (num & -65536) >> 0x10;
                    int num4 = Marshal.ReadInt32(lpResults.lpCaretPos, (text.Length - 1) * 4);
                    if (num4 > 0xffff)
                    {
                        num2 = num4 + Marshal.ReadInt32(lpResults.lpDx, (text.Length - 1) * 4);
                    }
                    Marshal.FreeCoTaskMem(lpResults.lpCaretPos);
                    Marshal.FreeCoTaskMem(lpResults.lpDx);
                    DevExpress.Office.PInvoke.Win32.SelectObject(hdc, ptr3);
                    DevExpress.Office.PInvoke.Win32.DeleteObject(hGdiObj);
                    ef = new SizeF((float) num2, (float) num3);
                }
                finally
                {
                    this.Graphics.ReleaseHdc(hdc);
                }
            }
            return ef;
        }

        public override SizeF MeasureString(string text, Font font, SizeF size, StringFormat stringFormat, GraphicsUnit pageUnit) => 
            this.MeasureString(text, font, (PointF) Point.Empty, stringFormat, pageUnit);

        [SecuritySafeCritical]
        private int MeasureWithGetCharacterPlacementSlow(IntPtr hdc, string text, ref DevExpress.Office.PInvoke.Win32.GCP_RESULTS gcpResults)
        {
            int num = Math.Max(1, (int) Math.Ceiling((double) (((double) text.Length) / 2.0)));
            int num2 = num;
            int num3 = 0;
            while (num3 < 3)
            {
                gcpResults.lpDx = Marshal.ReAllocCoTaskMem(gcpResults.lpDx, 4 * (text.Length + num2));
                gcpResults.lpGlyphs = Marshal.ReAllocCoTaskMem(gcpResults.lpGlyphs, 2 * (text.Length + num2));
                gcpResults.nGlyphs = text.Length + num2;
                if (text.Length > 0)
                {
                    Marshal.WriteInt16(gcpResults.lpGlyphs, (short) 0);
                }
                int num4 = DevExpress.Office.PInvoke.Win32.GetCharacterPlacement(hdc, text, text.Length, 0, ref gcpResults, DevExpress.Office.PInvoke.Win32.GcpFlags.GCP_LIGATE | DevExpress.Office.PInvoke.Win32.GcpFlags.GCP_USEKERNING);
                if (num4 != 0)
                {
                    return num4;
                }
                num3++;
                num2 += num;
            }
            return 0;
        }

        public System.Drawing.Graphics Graphics =>
            base.Graph;
    }
}

