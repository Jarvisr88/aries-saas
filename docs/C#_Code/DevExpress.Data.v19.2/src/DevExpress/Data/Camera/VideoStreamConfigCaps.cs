﻿namespace DevExpress.Data.Camera
{
    using System;
    using System.Drawing;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential), ComVisible(false)]
    internal class VideoStreamConfigCaps
    {
        public System.Guid Guid;
        public AnalogVideoStandard VideoStandard;
        public Size InputSize;
        public Size MinCroppingSize;
        public Size MaxCroppingSize;
        public int CropGranularityX;
        public int CropGranularityY;
        public int CropAlignX;
        public int CropAlignY;
        public Size MinOutputSize;
        public Size MaxOutputSize;
        public int OutputGranularityX;
        public int OutputGranularityY;
        public int StretchTapsX;
        public int StretchTapsY;
        public int ShrinkTapsX;
        public int ShrinkTapsY;
        public long MinFrameInterval;
        public long MaxFrameInterval;
        public int MinBitsPerSecond;
        public int MaxBitsPerSecond;
    }
}

