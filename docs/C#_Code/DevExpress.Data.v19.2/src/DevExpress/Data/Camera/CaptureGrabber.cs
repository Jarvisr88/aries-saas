namespace DevExpress.Data.Camera
{
    using DevExpress.Data.Camera.Interfaces;
    using System;
    using System.Runtime.CompilerServices;
    using System.Security;

    [SecuritySafeCritical]
    internal class CaptureGrabber : ISampleGrabberCB
    {
        private CameraDeviceBase device;
        private int imageHeight;
        private int imageWidth;
        private int stride;

        public event EventHandler NewFrame;

        public event EventHandler SizeChanged;

        public CaptureGrabber(CameraDeviceBase device);
        [SecuritySafeCritical]
        public int BufferCB(double sampleTime, IntPtr buffer, int bufferLen);
        private int CalcStride();
        private void OnNewFrameArrived();
        private void OnPropertyChanged();
        public int SampleCB(double sampleTime, IntPtr sample);

        public CameraDeviceBase Device { get; }

        public IntPtr FramePtr { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }
    }
}

