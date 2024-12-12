namespace DevExpress.Xpf.Editors
{
    using DevExpress.Mvvm;
    using DevExpress.Xpf.Core;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows.Input;

    public class CameraSettingsProvider : ViewModelBase
    {
        private Locker actualDeviceLocker = new Locker();

        public CameraSettingsProvider(CameraControl owner)
        {
            this.Owner = owner;
            CollectionCameraSettings settings1 = new CollectionCameraSettings();
            settings1.Caption = EditorLocalizer.GetString(EditorStringId.CameraDeviceCaption);
            settings1.AvaliableValues = this.Owner.GetAvailableDevices();
            this.Device = settings1;
            CollectionCameraSettings settings2 = new CollectionCameraSettings();
            settings2.Caption = EditorLocalizer.GetString(EditorStringId.CameraResolutionCaption);
            this.Resolution = settings2;
            RangeCameraSettings settings3 = new RangeCameraSettings();
            settings3.Caption = EditorLocalizer.GetString(EditorStringId.CameraBrightnessCaption);
            this.Brightness = settings3;
            RangeCameraSettings settings4 = new RangeCameraSettings();
            settings4.Caption = EditorLocalizer.GetString(EditorStringId.CameraContrastCaption);
            this.Contrast = settings4;
            BaseCameraSettings settings5 = new BaseCameraSettings();
            settings5.Caption = EditorLocalizer.GetString(EditorStringId.CameraDesaturateCaption);
            this.Desaturate = settings5;
            BaseCameraSettings settings6 = new BaseCameraSettings();
            settings6.Caption = EditorLocalizer.GetString(EditorStringId.CameraResetButtonCaption);
            this.Reset = settings6;
            this.ActualDevice = this.Owner.DeviceInfo;
        }

        private IEnumerable<object> GetAvaliableResolutions(CameraDevice newDevice)
        {
            List<ResolutionItem> result = new List<ResolutionItem>();
            newDevice.GetAvailiableResolutions().ForEach(delegate (Size x) {
                result.Add(new ResolutionItem(x));
            });
            result.Sort(new ResolutionComparer());
            return result;
        }

        private void ResetToDefault()
        {
            if (this.Owner.DeviceSettings != null)
            {
                this.Owner.DeviceSettings.Brightness.ResetToDefault();
                this.Owner.DeviceSettings.Contrast.ResetToDefault();
                this.Owner.DeviceSettings.Saturation.ResetToDefault();
            }
            this.SyncSettings();
        }

        private void SyncSettings()
        {
            CameraDevice newDevice = this.Owner.Device;
            this.EnableSettings = newDevice != null;
            if (this.EnableSettings)
            {
                this.UpdateResolution(newDevice);
                this.UpdateBrightness();
                this.UpdateContrast();
                this.CanDesaturate = this.Owner.DeviceSettings.Saturation.Min == this.Owner.DeviceSettings.Saturation.Value;
                this.ResetToDefaultCommand = new DelegateCommand(new Action(this.ResetToDefault));
            }
        }

        private void UpdateBrightness()
        {
            this.Brightness.MinValue = this.Owner.DeviceSettings.Brightness.Min;
            this.Brightness.MaxValue = this.Owner.DeviceSettings.Brightness.Max;
            this.ActualBrightness = this.Owner.DeviceSettings.Brightness.Value;
        }

        private void UpdateContrast()
        {
            this.Contrast.MinValue = this.Owner.DeviceSettings.Contrast.Min;
            this.Contrast.MaxValue = this.Owner.DeviceSettings.Contrast.Max;
            this.ActualContrast = this.Owner.DeviceSettings.Contrast.Value;
        }

        private void UpdateResolution(CameraDevice newDevice)
        {
            this.Resolution.AvaliableValues = this.GetAvaliableResolutions(newDevice);
            this.ActualResolution = newDevice.Resolution;
        }

        public DeviceInfo ActualDevice
        {
            get => 
                base.GetProperty<DeviceInfo>(Expression.Lambda<Func<DeviceInfo>>(Expression.Property(Expression.Constant(this, typeof(CameraSettingsProvider)), (MethodInfo) methodof(CameraSettingsProvider.get_ActualDevice)), new ParameterExpression[0]));
            set => 
                this.actualDeviceLocker.DoLockedActionIfNotLocked(delegate {
                    this.SetProperty<DeviceInfo>(Expression.Lambda<Func<DeviceInfo>>(Expression.Property(Expression.Constant(this, typeof(CameraSettingsProvider)), (MethodInfo) methodof(CameraSettingsProvider.get_ActualDevice)), new ParameterExpression[0]), value);
                    this.Owner.SyncDeviceFromSettings(value);
                    this.SyncSettings();
                });
        }

        public Size ActualResolution
        {
            get => 
                base.GetProperty<Size>(Expression.Lambda<Func<Size>>(Expression.Property(Expression.Constant(this, typeof(CameraSettingsProvider)), (MethodInfo) methodof(CameraSettingsProvider.get_ActualResolution)), new ParameterExpression[0]));
            set
            {
                base.SetProperty<Size>(Expression.Lambda<Func<Size>>(Expression.Property(Expression.Constant(this, typeof(CameraSettingsProvider)), (MethodInfo) methodof(CameraSettingsProvider.get_ActualResolution)), new ParameterExpression[0]), value);
                if (value != this.Owner.Device.Resolution)
                {
                    this.Owner.Device.Resolution = value;
                }
            }
        }

        public double ActualBrightness
        {
            get => 
                base.GetProperty<double>(Expression.Lambda<Func<double>>(Expression.Property(Expression.Constant(this, typeof(CameraSettingsProvider)), (MethodInfo) methodof(CameraSettingsProvider.get_ActualBrightness)), new ParameterExpression[0]));
            set
            {
                base.SetProperty<double>(Expression.Lambda<Func<double>>(Expression.Property(Expression.Constant(this, typeof(CameraSettingsProvider)), (MethodInfo) methodof(CameraSettingsProvider.get_ActualBrightness)), new ParameterExpression[0]), value);
                if (((int) value) != this.Owner.DeviceSettings.Brightness.Value)
                {
                    this.Owner.DeviceSettings.Brightness.Value = (int) value;
                }
            }
        }

        public double ActualContrast
        {
            get => 
                base.GetProperty<double>(Expression.Lambda<Func<double>>(Expression.Property(Expression.Constant(this, typeof(CameraSettingsProvider)), (MethodInfo) methodof(CameraSettingsProvider.get_ActualContrast)), new ParameterExpression[0]));
            set
            {
                base.SetProperty<double>(Expression.Lambda<Func<double>>(Expression.Property(Expression.Constant(this, typeof(CameraSettingsProvider)), (MethodInfo) methodof(CameraSettingsProvider.get_ActualContrast)), new ParameterExpression[0]), value);
                if (((int) value) != this.Owner.DeviceSettings.Contrast.Value)
                {
                    this.Owner.DeviceSettings.Contrast.Value = (int) value;
                }
            }
        }

        public bool CanDesaturate
        {
            get => 
                base.GetProperty<bool>(Expression.Lambda<Func<bool>>(Expression.Property(Expression.Constant(this, typeof(CameraSettingsProvider)), (MethodInfo) methodof(CameraSettingsProvider.get_CanDesaturate)), new ParameterExpression[0]));
            set
            {
                base.SetProperty<bool>(Expression.Lambda<Func<bool>>(Expression.Property(Expression.Constant(this, typeof(CameraSettingsProvider)), (MethodInfo) methodof(CameraSettingsProvider.get_CanDesaturate)), new ParameterExpression[0]), value);
                int num = value ? this.Owner.DeviceSettings.Saturation.Min : this.Owner.DeviceSettings.Saturation.Default;
                if (num != this.Owner.DeviceSettings.Saturation.Value)
                {
                    this.Owner.DeviceSettings.Saturation.Value = num;
                }
            }
        }

        public bool EnableSettings
        {
            get => 
                base.GetProperty<bool>(Expression.Lambda<Func<bool>>(Expression.Property(Expression.Constant(this, typeof(CameraSettingsProvider)), (MethodInfo) methodof(CameraSettingsProvider.get_EnableSettings)), new ParameterExpression[0]));
            set => 
                base.SetProperty<bool>(Expression.Lambda<Func<bool>>(Expression.Property(Expression.Constant(this, typeof(CameraSettingsProvider)), (MethodInfo) methodof(CameraSettingsProvider.get_EnableSettings)), new ParameterExpression[0]), value);
        }

        public string SettingsCaption =>
            EditorLocalizer.GetString(EditorStringId.CameraSettingsCaption);

        public ICommand ResetToDefaultCommand { get; private set; }

        public CollectionCameraSettings Device { get; private set; }

        public CollectionCameraSettings Resolution { get; private set; }

        public RangeCameraSettings Brightness { get; private set; }

        public RangeCameraSettings Contrast { get; private set; }

        public BaseCameraSettings Reset { get; private set; }

        public BaseCameraSettings Desaturate { get; private set; }

        private CameraControl Owner { get; set; }
    }
}

