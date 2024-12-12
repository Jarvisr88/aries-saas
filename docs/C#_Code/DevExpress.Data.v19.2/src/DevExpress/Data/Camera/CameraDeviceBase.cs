namespace DevExpress.Data.Camera
{
    using DevExpress.Data.Camera.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Security;
    using System.Windows.Forms;

    [SecuritySafeCritical]
    public class CameraDeviceBase : IDisposable
    {
        private IGraphBuilder graph;
        private ISampleGrabber grabber;
        private ICaptureGraphBuilder2 captureGraphBuilder;
        private IBaseFilter sourceFilter;
        private IBaseFilter grabberFilter;
        private IMediaControl mediaControl;
        private IMediaEventEx mediaEventEx;
        private CaptureGrabber capGrabber;
        private AMMediaType mediaType;
        private IntPtr framePtr;
        private IntPtr capturedFramePtr;
        private FrameRateCounter rateCounter;
        private string deviceMoniker;
        private string name;
        private ICameraDeviceClient client;
        private bool isRunning;

        public CameraDeviceBase(CameraDeviceInfo deviceInfo);
        private void cg_NewFrame(object sender, EventArgs e);
        private void cg_SizeChanged(object sender, EventArgs e);
        private void ConfigureStream(IAMStreamConfig streamCfg);
        protected virtual void CreateFrameCore(IntPtr section, int width, int height, IntPtr stride);
        private IBaseFilter CreateSourceFilter();
        public void Dispose();
        public override bool Equals(object obj);
        protected virtual void FreeFrame();
        public List<Size> GetAvailableResolutions();
        [EditorBrowsable(EditorBrowsableState.Never)]
        public List<Size> GetAvailiableResolutions();
        private void GetAvailiableResolutions(IBaseFilter source, List<Size> result);
        private Size GetCurrentResolution();
        private IAMStreamConfig GetFilterStreamConfig(IBaseFilter source);
        public override int GetHashCode();
        private IAMStreamConfig GetStreamCfg(ICaptureGraphBuilder2 graphBuilder, IBaseFilter baseFilter, Guid pinCategory);
        private int GetVideoProcessingProperty(VideoProcAmpProperty videoProperty, bool getValue, string subPropName);
        internal int GetVideoProcessingPropertyByName(string propName, bool getValue, string subPropName);
        internal void HandleGraphEvent();
        private void OnDeviceLost();
        private void OnGrabberSizeChanged();
        protected virtual void OnNewFrame();
        private void RunDevice();
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void SetClient(ICameraDeviceClient client);
        private void SetResolution(Size resolution);
        internal void SetVideoProcessingProperty(VideoProcAmpProperty videoProperty, int value);
        internal void SetVideoProcessingPropertyByName(string propName, int val);
        public void Start();
        public void Stop();
        private void Stop(bool forced);
        private void SubscribeOnCaptureGrabber(CaptureGrabber cg);
        private void SubscribeOnMediaEventNotifying(IGraphBuilder iGraphBuilder);
        public Bitmap TakeSnapshot();
        private bool TryGetMediaTypeByResolution(IBaseFilter source, Size resolution, out AMMediaType result);
        private void UnsubscribeFromMediaEventNotifying();
        private void UnSubscribeOnCaptureGrabber(CaptureGrabber cg);
        public virtual void WndProc(ref Message m);

        private float FPS { get; }

        [Description("Gets the string representation of the moniker for the current device."), Browsable(false)]
        public string DeviceMoniker { get; }

        [Description("Gets the UI display name of the video capture device."), Browsable(false)]
        public string Name { get; }

        [Description("Gets whether the video capture device is already in use in another application."), Browsable(false)]
        public bool IsBusy { get; private set; }

        [Description("Gets whether the device is currently capturing video."), Browsable(false)]
        public bool IsRunning { get; private set; }

        protected internal int BitsPerPixel { get; }

        protected PixelFormat CurrentPixelFormat { get; }

        private Size PreferredResolution { get; set; }

        [Description("Gets or sets the resolution of a video stream captured by the current device.")]
        public Size Resolution { get; set; }
    }
}

