namespace DevExpress.Office.Utils
{
    using DevExpress.Office.PInvoke;
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Runtime.InteropServices;
    using System.Security;

    public class MetafilePhysicalDimensionCalculator
    {
        private Win32.MapMode mapMode;
        private Size windowExtent;

        public Size Calculate(Metafile metafile, byte[] bytes)
        {
            Size size;
            this.windowExtent = Size.Empty;
            this.mapMode = Win32.MapMode.HighMetric;
            IntPtr hmf = Win32.SetMetaFileBitsEx(bytes.Length, bytes);
            if (hmf != IntPtr.Zero)
            {
                size = this.CalculateWmfPhysicalDimension(hmf, metafile);
                Win32.DeleteMetaFile(hmf);
            }
            else
            {
                Win32.METAFILEPICT mfp = new Win32.METAFILEPICT(Win32.MapMode.Anisotropic, -1, -1);
                IntPtr hEmf = Win32.SetWinMetaFileBits(bytes.Length, bytes, IntPtr.Zero, ref mfp);
                if (hEmf == IntPtr.Zero)
                {
                    hEmf = Win32.SetEnhMetaFileBits(bytes.Length, bytes);
                }
                if (!(hEmf != IntPtr.Zero))
                {
                    size = Size.Round(metafile.PhysicalDimension);
                }
                else
                {
                    size = this.CalculateEmfPhysicalDimension(hEmf, metafile);
                    MetafileHelper.DeleteMetafileHandle(hEmf);
                }
            }
            return size;
        }

        private Size CalculateEmfPhysicalDimension(IntPtr hEmf, Metafile metafile)
        {
            Win32.RECT lpRect = new Win32.RECT();
            Win32.EnumEnhMetaFile(IntPtr.Zero, hEmf, new Win32.EnumMetaFileDelegate(this.EnumEnhMetafileCallback), IntPtr.Zero, ref lpRect);
            Size windowExtent = this.windowExtent;
            if (windowExtent == Size.Empty)
            {
                windowExtent = metafile.Size;
            }
            return this.ConvertLogicalUnitsToHundredthsOfMillimeter(windowExtent);
        }

        private Size CalculateWmfPhysicalDimension(IntPtr hmf, Metafile metafile)
        {
            Win32.EnumMetaFile(IntPtr.Zero, hmf, new Win32.EnumMetaFileDelegate(this.EnumMetafileCallback), IntPtr.Zero);
            Size windowExtent = this.windowExtent;
            if (windowExtent == Size.Empty)
            {
                windowExtent = metafile.Size;
            }
            return ((this.mapMode != Win32.MapMode.HighMetric) ? this.ConvertLogicalUnitsToHundredthsOfMillimeter(windowExtent) : windowExtent);
        }

        private Size ConvertLogicalUnitsToHundredthsOfMillimeter(Size size)
        {
            size.Width = (int) Math.Round((double) ((2540f * Math.Abs(size.Width)) / 96f));
            size.Height = (int) Math.Round((double) ((2540f * Math.Abs(size.Height)) / 96f));
            return size;
        }

        [SecuritySafeCritical]
        private int EnumEnhMetafileCallback(IntPtr hdc, IntPtr handleTable, IntPtr metafileRecord, int objectCount, IntPtr clientData)
        {
            EmfPlusRecordType type = (EmfPlusRecordType) Marshal.ReadInt32(metafileRecord);
            if (type == EmfPlusRecordType.EmfSetWindowExtEx)
            {
                int width = Marshal.ReadInt32(metafileRecord, 8);
                this.windowExtent = new Size(width, Marshal.ReadInt32(metafileRecord, 12));
            }
            else if (type == EmfPlusRecordType.WmfSetMapMode)
            {
                this.mapMode = (Win32.MapMode) Marshal.ReadInt32(metafileRecord, 8);
            }
            return 1;
        }

        [SecuritySafeCritical]
        private int EnumMetafileCallback(IntPtr hdc, IntPtr handleTable, IntPtr metafileRecord, int objectCount, IntPtr clientData)
        {
            EmfPlusRecordType type = (EmfPlusRecordType) (0x10000 + Marshal.ReadInt16(metafileRecord, 4));
            if (type == EmfPlusRecordType.WmfSetWindowExt)
            {
                int width = Marshal.ReadInt16(metafileRecord, 8);
                this.windowExtent = new Size(width, Marshal.ReadInt16(metafileRecord, 6));
            }
            else if (type == EmfPlusRecordType.WmfSetMapMode)
            {
                this.mapMode = (Win32.MapMode) Marshal.ReadInt16(metafileRecord, 6);
            }
            return 1;
        }
    }
}

