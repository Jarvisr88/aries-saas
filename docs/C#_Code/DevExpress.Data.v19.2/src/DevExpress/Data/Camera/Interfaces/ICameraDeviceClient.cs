namespace DevExpress.Data.Camera.Interfaces
{
    using DevExpress.Data.Camera;
    using System;

    public interface ICameraDeviceClient
    {
        void OnDeviceLost(CameraDeviceBase lostDevice);
        void OnNewFrame();
        void OnResolutionChanged();

        IntPtr Handle { get; }

        CameraDeviceBase Device { get; }
    }
}

