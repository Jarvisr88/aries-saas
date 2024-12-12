namespace DevExpress.Xpf.Editors
{
    using DevExpress.Data.Camera;
    using DevExpress.Data.Camera.Interfaces;
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.Native;
    using DevExpress.Utils;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Interop;
    using System.Windows.Media;
    using System.Windows.Threading;

    [ToolboxTabName("DX.19.2: Common Controls"), DXToolboxBrowsable(true)]
    public class CameraControl : Control, ICameraDeviceClient
    {
        public static readonly DependencyProperty AllowAutoStartProperty;
        public static readonly DependencyProperty ShowSettingsButtonProperty;
        public static readonly DependencyProperty StretchProperty;
        public static readonly DependencyProperty StretchDirectionProperty;
        public static readonly DependencyProperty DeviceProperty;
        private static readonly DependencyPropertyKey DevicePropertyKey;
        public static readonly DependencyProperty DeviceSettingsProperty;
        private static readonly DependencyPropertyKey DeviceSettingsPropertyKey;
        public static readonly DependencyProperty DeviceInfoProperty;
        public static readonly DependencyProperty BorderTemplateProperty;
        private static readonly DependencyPropertyKey PropertyProviderPropertyKey;
        public static readonly DependencyProperty PropertyProviderProperty;
        public static readonly DependencyProperty ShowBorderProperty;
        private static readonly DependencyPropertyKey NativeImageSourcePropertyKey;
        public static readonly DependencyProperty NativeImageSourceProperty;
        private CameraDevice device;
        private Locker syncDeviceLocker = new Locker();

        static CameraControl()
        {
            Type forType = typeof(CameraControl);
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(forType, new FrameworkPropertyMetadata(forType));
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(CameraControl), "owner");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            AllowAutoStartProperty = DependencyPropertyRegistrator.Register<CameraControl, bool>(System.Linq.Expressions.Expression.Lambda<Func<CameraControl, bool>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(CameraControl.get_AllowAutoStart)), parameters), true);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(CameraControl), "owner");
            ParameterExpression[] expressionArray2 = new ParameterExpression[] { expression };
            ShowSettingsButtonProperty = DependencyPropertyRegistrator.Register<CameraControl, bool>(System.Linq.Expressions.Expression.Lambda<Func<CameraControl, bool>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(CameraControl.get_ShowSettingsButton)), expressionArray2), true, (control, value, newValue) => control.ShowSettingsButtonChanged());
            expression = System.Linq.Expressions.Expression.Parameter(typeof(CameraControl), "owner");
            ParameterExpression[] expressionArray3 = new ParameterExpression[] { expression };
            StretchProperty = DependencyPropertyRegistrator.Register<CameraControl, System.Windows.Media.Stretch>(System.Linq.Expressions.Expression.Lambda<Func<CameraControl, System.Windows.Media.Stretch>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(CameraControl.get_Stretch)), expressionArray3), System.Windows.Media.Stretch.Uniform, (control, value, newValue) => control.StretchChanged(value, newValue));
            expression = System.Linq.Expressions.Expression.Parameter(typeof(CameraControl), "owner");
            ParameterExpression[] expressionArray4 = new ParameterExpression[] { expression };
            StretchDirectionProperty = DependencyPropertyRegistrator.Register<CameraControl, System.Windows.Controls.StretchDirection>(System.Linq.Expressions.Expression.Lambda<Func<CameraControl, System.Windows.Controls.StretchDirection>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(CameraControl.get_StretchDirection)), expressionArray4), System.Windows.Controls.StretchDirection.Both, (control, value, newValue) => control.StretchDirectionChanged(value, newValue));
            expression = System.Linq.Expressions.Expression.Parameter(typeof(CameraControl), "owner");
            ParameterExpression[] expressionArray5 = new ParameterExpression[] { expression };
            DevicePropertyKey = DependencyPropertyRegistrator.RegisterReadOnly<CameraControl, CameraDevice>(System.Linq.Expressions.Expression.Lambda<Func<CameraControl, CameraDevice>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(CameraControl.get_Device)), expressionArray5), null, (control, value, newValue) => control.DeviceChanged(value, newValue));
            DeviceProperty = DevicePropertyKey.DependencyProperty;
            expression = System.Linq.Expressions.Expression.Parameter(typeof(CameraControl), "owner");
            ParameterExpression[] expressionArray6 = new ParameterExpression[] { expression };
            DeviceSettingsPropertyKey = DependencyPropertyRegistrator.RegisterReadOnly<CameraControl, DeviceVideoSettings>(System.Linq.Expressions.Expression.Lambda<Func<CameraControl, DeviceVideoSettings>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(CameraControl.get_DeviceSettings)), expressionArray6), null, null);
            DeviceSettingsProperty = DeviceSettingsPropertyKey.DependencyProperty;
            expression = System.Linq.Expressions.Expression.Parameter(typeof(CameraControl), "owner");
            ParameterExpression[] expressionArray7 = new ParameterExpression[] { expression };
            DeviceInfoProperty = DependencyPropertyRegistrator.Register<CameraControl, DevExpress.Xpf.Editors.DeviceInfo>(System.Linq.Expressions.Expression.Lambda<Func<CameraControl, DevExpress.Xpf.Editors.DeviceInfo>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(CameraControl.get_DeviceInfo)), expressionArray7), null, (control, value, newValue) => control.DeviceInfoChanged(value, newValue));
            BorderTemplateProperty = DependencyProperty.Register("BorderTemplate", typeof(ControlTemplate), forType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure));
            expression = System.Linq.Expressions.Expression.Parameter(typeof(CameraControl), "owner");
            ParameterExpression[] expressionArray8 = new ParameterExpression[] { expression };
            PropertyProviderPropertyKey = DependencyPropertyRegistrator.RegisterReadOnly<CameraControl, CameraPropertyProvider>(System.Linq.Expressions.Expression.Lambda<Func<CameraControl, CameraPropertyProvider>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(CameraControl.get_PropertyProvider)), expressionArray8), null, null);
            PropertyProviderProperty = PropertyProviderPropertyKey.DependencyProperty;
            expression = System.Linq.Expressions.Expression.Parameter(typeof(CameraControl), "owner");
            ParameterExpression[] expressionArray9 = new ParameterExpression[] { expression };
            ShowBorderProperty = DependencyPropertyRegistrator.Register<CameraControl, bool>(System.Linq.Expressions.Expression.Lambda<Func<CameraControl, bool>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(CameraControl.get_ShowBorder)), expressionArray9), true);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(CameraControl), "owner");
            ParameterExpression[] expressionArray10 = new ParameterExpression[] { expression };
            NativeImageSourcePropertyKey = DependencyPropertyRegistrator.RegisterReadOnly<CameraControl, ImageSource>(System.Linq.Expressions.Expression.Lambda<Func<CameraControl, ImageSource>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(CameraControl.get_NativeImageSource)), expressionArray10), null, null);
            NativeImageSourceProperty = NativeImageSourcePropertyKey.DependencyProperty;
        }

        public CameraControl()
        {
            base.Loaded += new RoutedEventHandler(this.CameraControlLoaded);
            base.Unloaded += new RoutedEventHandler(this.CameraControlUnloaded);
            this.StartCommand = new DelegateCommand(() => this.Start());
            this.StopCommand = new DelegateCommand(() => this.Stop());
            CameraPropertyProvider provider1 = new CameraPropertyProvider();
            provider1.ShowSettingsButton = true;
            provider1.RefreshCommand = new DelegateCommand(new Action(this.Refresh));
            this.PropertyProvider = provider1;
        }

        private void CameraControlLoaded(object sender, RoutedEventArgs e)
        {
            if (!this.IsInDesignTool())
            {
                this.CreateDevice();
                this.UpdateHasDevices();
            }
        }

        private void CameraControlUnloaded(object sender, RoutedEventArgs e)
        {
            this.Stop();
            this.Device = null;
        }

        private void CreateDevice()
        {
            if (this.DeviceInfo != null)
            {
                if (this.Device == null)
                {
                    this.UpdateDevice(this.DeviceInfo);
                }
            }
            else
            {
                CameraDeviceInfo defaultDevice = this.GetDefaultDevice();
                if (defaultDevice != null)
                {
                    DevExpress.Xpf.Editors.DeviceInfo info1 = new DevExpress.Xpf.Editors.DeviceInfo();
                    info1.Moniker = defaultDevice.MonikerString;
                    info1.Name = defaultDevice.Name;
                    base.SetCurrentValue(DeviceInfoProperty, info1);
                }
            }
        }

        protected virtual CameraDevice CreateDevice(DevExpress.Xpf.Editors.DeviceInfo deviceInfo)
        {
            if (deviceInfo == null)
            {
                return null;
            }
            CameraDevice device = new CameraDevice(new CameraDeviceInfo(deviceInfo.Moniker, deviceInfo.Name));
            device.SetClient(this);
            return device;
        }

        private DeviceVideoSettings CreateDeviceSettings(CameraDevice device) => 
            (device != null) ? new DeviceVideoSettings(this) : null;

        void ICameraDeviceClient.OnDeviceLost(CameraDeviceBase lostDevice)
        {
            this.UpdateIsBusy();
            this.Device = null;
        }

        void ICameraDeviceClient.OnNewFrame()
        {
            if (base.Dispatcher != null)
            {
                base.Dispatcher.BeginInvoke(DispatcherPriority.Render, delegate {
                    if ((this.Device != null) && this.Device.IsRunning)
                    {
                        if (!ReferenceEquals(this.NativeImageSource, this.Device.BitmapSource))
                        {
                            this.NativeImageSource = this.Device.BitmapSource;
                        }
                        if (this.Device.BitmapSource != null)
                        {
                            this.Device.BitmapSource.Invalidate();
                        }
                    }
                });
            }
        }

        void ICameraDeviceClient.OnResolutionChanged()
        {
        }

        protected virtual void DeviceChanged(CameraDevice oldValue, CameraDevice newValue)
        {
            Action<CameraDevice> action = <>c.<>9__70_0;
            if (<>c.<>9__70_0 == null)
            {
                Action<CameraDevice> local1 = <>c.<>9__70_0;
                action = <>c.<>9__70_0 = x => x.Dispose();
            }
            oldValue.Do<CameraDevice>(action);
            this.device = newValue;
            if (newValue != null)
            {
                this.NativeImageSource = newValue.BitmapSource;
            }
        }

        private void DeviceInfoChanged(DevExpress.Xpf.Editors.DeviceInfo oldValue, DevExpress.Xpf.Editors.DeviceInfo newValue)
        {
            this.UpdateDevice(newValue);
        }

        private void FlyoutOpened(object sender, EventArgs e)
        {
            this.UpdateSettings();
        }

        public IEnumerable<DevExpress.Xpf.Editors.DeviceInfo> GetAvailableDevices()
        {
            List<DevExpress.Xpf.Editors.DeviceInfo> result = new List<DevExpress.Xpf.Editors.DeviceInfo>();
            DeviceHelper.GetDevices().ForEach(delegate (CameraDeviceInfo x) {
                if ((this.DeviceInfo != null) && ((this.DeviceInfo.Moniker == x.MonikerString) && (x.Name == this.DeviceInfo.Name)))
                {
                    result.Add(this.DeviceInfo);
                }
                else
                {
                    DevExpress.Xpf.Editors.DeviceInfo item = new DevExpress.Xpf.Editors.DeviceInfo();
                    item.Moniker = x.MonikerString;
                    item.Name = x.Name;
                    result.Add(item);
                }
            });
            return result;
        }

        private CameraDeviceInfo GetDefaultDevice() => 
            DeviceHelper.GetDevices().FirstOrDefault<CameraDeviceInfo>();

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            LayoutHelper.FindElementByType<FlyoutControl>(LayoutHelper.FindElementByType<ContentControl>(this).Content as FrameworkElement).Opened += new EventHandler(this.FlyoutOpened);
        }

        private void Refresh()
        {
            this.CreateDevice();
            this.Start();
        }

        private void ShowSettingsButtonChanged()
        {
            this.PropertyProvider.ShowSettingsButton = this.ShowSettingsButton;
        }

        public virtual void Start()
        {
            Action<CameraDevice> action = <>c.<>9__76_0;
            if (<>c.<>9__76_0 == null)
            {
                Action<CameraDevice> local1 = <>c.<>9__76_0;
                action = <>c.<>9__76_0 = x => x.Start();
            }
            this.Device.Do<CameraDevice>(action);
            this.UpdateIsBusy();
            this.UpdateHasDevices();
        }

        public virtual void Stop()
        {
            Action<CameraDevice> action = <>c.<>9__78_0;
            if (<>c.<>9__78_0 == null)
            {
                Action<CameraDevice> local1 = <>c.<>9__78_0;
                action = <>c.<>9__78_0 = x => x.Stop();
            }
            this.Device.Do<CameraDevice>(action);
        }

        protected virtual void StretchChanged(System.Windows.Media.Stretch oldValue, System.Windows.Media.Stretch newValue)
        {
        }

        protected virtual void StretchDirectionChanged(System.Windows.Controls.StretchDirection oldValue, System.Windows.Controls.StretchDirection newValue)
        {
        }

        internal void SyncDeviceFromSettings(DevExpress.Xpf.Editors.DeviceInfo device)
        {
            this.syncDeviceLocker.DoLockedAction<DevExpress.Xpf.Editors.DeviceInfo>(delegate {
                DevExpress.Xpf.Editors.DeviceInfo info;
                this.DeviceInfo = info = device;
                return info;
            });
        }

        public ImageSource TakeSnapshot() => 
            ((this.Device == null) || !this.Device.IsRunning) ? null : ImageHelper.CreateImageSource(this.Device.TakeSnapshot());

        private void UpdateDevice(DevExpress.Xpf.Editors.DeviceInfo info)
        {
            this.Device = this.CreateDevice(info);
            this.DeviceSettings = this.CreateDeviceSettings(this.Device);
            if (this.AllowAutoStart)
            {
                this.Start();
            }
            if (!this.syncDeviceLocker)
            {
                this.UpdateSettings();
            }
        }

        private void UpdateHasDevices()
        {
            Func<IList, int> evaluator = <>c.<>9__77_0;
            if (<>c.<>9__77_0 == null)
            {
                Func<IList, int> local1 = <>c.<>9__77_0;
                evaluator = <>c.<>9__77_0 = x => x.Count;
            }
            this.PropertyProvider.HasDevices = (this.GetAvailableDevices() as IList).Return<IList, int>(evaluator, (<>c.<>9__77_1 ??= () => 0)) > 0;
        }

        private void UpdateIsBusy()
        {
            if (this.Device != null)
            {
                this.PropertyProvider.IsBusy = this.Device.IsBusy;
            }
        }

        private void UpdateSettings()
        {
            this.PropertyProvider.Settings = new CameraSettingsProvider(this);
        }

        public bool ShowBorder
        {
            get => 
                (bool) base.GetValue(ShowBorderProperty);
            set => 
                base.SetValue(ShowBorderProperty, value);
        }

        public ControlTemplate BorderTemplate
        {
            get => 
                (ControlTemplate) base.GetValue(BorderTemplateProperty);
            set => 
                base.SetValue(BorderTemplateProperty, value);
        }

        public DevExpress.Xpf.Editors.DeviceInfo DeviceInfo
        {
            get => 
                (DevExpress.Xpf.Editors.DeviceInfo) base.GetValue(DeviceInfoProperty);
            set => 
                base.SetValue(DeviceInfoProperty, value);
        }

        public DeviceVideoSettings DeviceSettings
        {
            get => 
                (DeviceVideoSettings) base.GetValue(DeviceSettingsProperty);
            private set => 
                base.SetValue(DeviceSettingsPropertyKey, value);
        }

        CameraDeviceBase ICameraDeviceClient.Device =>
            this.Device;

        public CameraDevice Device
        {
            get => 
                (CameraDevice) base.GetValue(DeviceProperty);
            private set => 
                base.SetValue(DevicePropertyKey, value);
        }

        public bool AllowAutoStart
        {
            get => 
                (bool) base.GetValue(AllowAutoStartProperty);
            set => 
                base.SetValue(AllowAutoStartProperty, value);
        }

        public bool ShowSettingsButton
        {
            get => 
                (bool) base.GetValue(ShowSettingsButtonProperty);
            set => 
                base.SetValue(ShowSettingsButtonProperty, value);
        }

        public System.Windows.Media.Stretch Stretch
        {
            get => 
                (System.Windows.Media.Stretch) base.GetValue(StretchProperty);
            set => 
                base.SetValue(StretchProperty, value);
        }

        public System.Windows.Controls.StretchDirection StretchDirection
        {
            get => 
                (System.Windows.Controls.StretchDirection) base.GetValue(StretchDirectionProperty);
            set => 
                base.SetValue(StretchDirectionProperty, value);
        }

        public CameraPropertyProvider PropertyProvider
        {
            get => 
                (CameraPropertyProvider) base.GetValue(PropertyProviderProperty);
            private set => 
                base.SetValue(PropertyProviderPropertyKey, value);
        }

        public ImageSource NativeImageSource
        {
            get => 
                (ImageSource) base.GetValue(NativeImageSourceProperty);
            private set => 
                base.SetValue(NativeImageSourcePropertyKey, value);
        }

        public ICommand StartCommand { get; private set; }

        public ICommand StopCommand { get; private set; }

        IntPtr ICameraDeviceClient.Handle
        {
            get
            {
                Func<HwndSource, IntPtr> evaluator = <>c.<>9__85_0;
                if (<>c.<>9__85_0 == null)
                {
                    Func<HwndSource, IntPtr> local1 = <>c.<>9__85_0;
                    evaluator = <>c.<>9__85_0 = x => x.Handle;
                }
                return ((HwndSource) PresentationSource.FromDependencyObject(this)).Return<HwndSource, IntPtr>(evaluator, (<>c.<>9__85_1 ??= () => IntPtr.Zero));
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly CameraControl.<>c <>9 = new CameraControl.<>c();
            public static Action<CameraDevice> <>9__70_0;
            public static Action<CameraDevice> <>9__76_0;
            public static Func<IList, int> <>9__77_0;
            public static Func<int> <>9__77_1;
            public static Action<CameraDevice> <>9__78_0;
            public static Func<HwndSource, IntPtr> <>9__85_0;
            public static Func<IntPtr> <>9__85_1;

            internal void <.cctor>b__15_0(CameraControl control, bool value, bool newValue)
            {
                control.ShowSettingsButtonChanged();
            }

            internal void <.cctor>b__15_1(CameraControl control, Stretch value, Stretch newValue)
            {
                control.StretchChanged(value, newValue);
            }

            internal void <.cctor>b__15_2(CameraControl control, StretchDirection value, StretchDirection newValue)
            {
                control.StretchDirectionChanged(value, newValue);
            }

            internal void <.cctor>b__15_3(CameraControl control, CameraDevice value, CameraDevice newValue)
            {
                control.DeviceChanged(value, newValue);
            }

            internal void <.cctor>b__15_4(CameraControl control, DeviceInfo value, DeviceInfo newValue)
            {
                control.DeviceInfoChanged(value, newValue);
            }

            internal IntPtr <DevExpress.Data.Camera.Interfaces.ICameraDeviceClient.get_Handle>b__85_0(HwndSource x) => 
                x.Handle;

            internal IntPtr <DevExpress.Data.Camera.Interfaces.ICameraDeviceClient.get_Handle>b__85_1() => 
                IntPtr.Zero;

            internal void <DeviceChanged>b__70_0(CameraDevice x)
            {
                x.Dispose();
            }

            internal void <Start>b__76_0(CameraDevice x)
            {
                x.Start();
            }

            internal void <Stop>b__78_0(CameraDevice x)
            {
                x.Stop();
            }

            internal int <UpdateHasDevices>b__77_0(IList x) => 
                x.Count;

            internal int <UpdateHasDevices>b__77_1() => 
                0;
        }
    }
}

