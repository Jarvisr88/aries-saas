namespace DevExpress.Data.Camera
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class DeviceVideoProperty : INotifyPropertyChanged
    {
        private readonly DeviceVideoSettings settings;

        public event PropertyChangedEventHandler PropertyChanged;

        internal DeviceVideoProperty(DeviceVideoSettings settings, string name);
        private void RaisePropertyChanged(string property);
        public void ResetToDefault();

        [Browsable(false)]
        public string Name { get; private set; }

        [Browsable(false)]
        public bool IsActive { get; }

        public int Min { get; }

        public int Max { get; }

        public int SteppingDelta { get; }

        public int Default { get; }

        public int Value { get; set; }
    }
}

