namespace DevExpress.Data.Camera
{
    using DevExpress.Data.Camera.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class DeviceVideoSettings : INotifyPropertyChanged
    {
        private ICameraDeviceClient _deviceClient;
        private List<DeviceVideoProperty> propsList;

        public event PropertyChangedEventHandler PropertyChanged;

        public DeviceVideoSettings(ICameraDeviceClient client);
        internal int GetPropDefault(string name);
        internal int GetPropMax(string name);
        internal int GetPropMin(string name);
        internal int GetPropSteppingDelta(string name);
        internal int GetPropValue(string name);
        private void InitProps();
        private void RaisePropertyChanged(string property);
        public void ResetToDefaults();
        internal void SetProp(string propName, int val);

        public DeviceVideoProperty Brightness { get; private set; }

        public DeviceVideoProperty Contrast { get; private set; }

        public DeviceVideoProperty Hue { get; private set; }

        public DeviceVideoProperty Saturation { get; private set; }

        public DeviceVideoProperty Sharpness { get; private set; }

        public DeviceVideoProperty Gamma { get; private set; }

        public DeviceVideoProperty ColorEnable { get; private set; }

        public DeviceVideoProperty WhiteBalance { get; private set; }

        public DeviceVideoProperty BacklightCompensation { get; private set; }

        public DeviceVideoProperty Gain { get; private set; }

        private CameraDeviceBase Device { get; }

        private bool DeviceIsAvailable { get; }
    }
}

