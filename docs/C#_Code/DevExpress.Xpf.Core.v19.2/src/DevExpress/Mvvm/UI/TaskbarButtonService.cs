namespace DevExpress.Mvvm.UI
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.UI.Interactivity;
    using DevExpress.Mvvm.UI.Native;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Markup;
    using System.Windows.Media;
    using System.Windows.Shell;

    [ContentProperty("ThumbButtonInfos"), TargetType(typeof(Window)), TargetType(typeof(UserControl))]
    public class TaskbarButtonService : WindowAwareServiceBase, ITaskbarButtonService
    {
        public static readonly DependencyProperty ProgressStateProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty ItemInfoProgressStateProperty;
        public static readonly DependencyProperty ProgressValueProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty ItemInfoProgressValueProperty;
        public static readonly DependencyProperty OverlayIconProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty ItemInfoOverlayIconProperty;
        public static readonly DependencyProperty DescriptionProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty ItemInfoDescriptionProperty;
        public static readonly DependencyProperty ThumbButtonInfosProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty ItemInfoThumbButtonInfosProperty;
        public static readonly DependencyProperty ThumbnailClipMarginProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty ItemInfoThumbnailClipMarginProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty WindowItemInfoProperty;
        public static readonly DependencyProperty ThumbnailClipMarginCallbackProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        internal static readonly DependencyProperty InternalItemsProperty;
        private TaskbarItemInfo itemInfo;
        private bool lockUpdateInternalItems;
        private bool processWindowItemInfoChanged;

        static TaskbarButtonService()
        {
            ProgressStateProperty = DependencyProperty.Register("ProgressState", typeof(TaskbarItemProgressState), typeof(TaskbarButtonService), new FrameworkPropertyMetadata(TaskbarItemProgressState.None, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (d, e) => ((TaskbarButtonService) d).OnProgressStateChanged(e)));
            ItemInfoProgressStateProperty = DependencyProperty.Register("ItemInfoProgressState", typeof(TaskbarItemProgressState), typeof(TaskbarButtonService), new PropertyMetadata(TaskbarItemProgressState.None, (d, e) => ((TaskbarButtonService) d).OnItemInfoProgressStateChanged(e)));
            ProgressValueProperty = DependencyProperty.Register("ProgressValue", typeof(double), typeof(TaskbarButtonService), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (d, e) => ((TaskbarButtonService) d).OnProgressValueChanged(e)));
            ItemInfoProgressValueProperty = DependencyProperty.Register("ItemInfoProgressValue", typeof(double), typeof(TaskbarButtonService), new PropertyMetadata(0.0, (d, e) => ((TaskbarButtonService) d).OnItemInfoProgressValueChanged(e)));
            OverlayIconProperty = DependencyProperty.Register("OverlayIcon", typeof(ImageSource), typeof(TaskbarButtonService), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (d, e) => ((TaskbarButtonService) d).OnOverlayIconChanged(e)));
            ItemInfoOverlayIconProperty = DependencyProperty.Register("ItemInfoOverlayIcon", typeof(ImageSource), typeof(TaskbarButtonService), new PropertyMetadata(null, (d, e) => ((TaskbarButtonService) d).OnItemInfoOverlayIconChanged(e)));
            DescriptionProperty = DependencyProperty.Register("Description", typeof(string), typeof(TaskbarButtonService), new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (d, e) => ((TaskbarButtonService) d).OnDescriptionChanged(e)));
            ItemInfoDescriptionProperty = DependencyProperty.Register("ItemInfoDescription", typeof(string), typeof(TaskbarButtonService), new PropertyMetadata("", (d, e) => ((TaskbarButtonService) d).OnItemInfoDescriptionChanged(e)));
            ThumbButtonInfosProperty = DependencyProperty.Register("ThumbButtonInfos", typeof(TaskbarThumbButtonInfoCollection), typeof(TaskbarButtonService), new PropertyMetadata(null, (d, e) => ((TaskbarButtonService) d).OnThumbButtonInfosChanged(e)));
            ItemInfoThumbButtonInfosProperty = DependencyProperty.Register("ItemInfoThumbButtonInfos", typeof(ThumbButtonInfoCollection), typeof(TaskbarButtonService), new PropertyMetadata(null, (d, e) => ((TaskbarButtonService) d).OnItemInfoThumbButtonInfosChanged(e)));
            Thickness defaultValue = new Thickness();
            ThumbnailClipMarginProperty = DependencyProperty.Register("ThumbnailClipMargin", typeof(Thickness), typeof(TaskbarButtonService), new FrameworkPropertyMetadata(defaultValue, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (d, e) => ((TaskbarButtonService) d).OnThumbnailClipMarginChanged(e)));
            defaultValue = new Thickness();
            ItemInfoThumbnailClipMarginProperty = DependencyProperty.Register("ItemInfoThumbnailClipMargin", typeof(Thickness), typeof(TaskbarButtonService), new PropertyMetadata(defaultValue, (d, e) => ((TaskbarButtonService) d).OnItemInfoThumbnailClipMarginChanged(e)));
            WindowItemInfoProperty = DependencyProperty.Register("WindowItemInfo", typeof(TaskbarItemInfo), typeof(TaskbarButtonService), new PropertyMetadata(null, (d, e) => ((TaskbarButtonService) d).OnWindowItemInfoChanged(e)));
            ThumbnailClipMarginCallbackProperty = DependencyProperty.Register("ThumbnailClipMarginCallback", typeof(Func<Size, Thickness>), typeof(TaskbarButtonService), new PropertyMetadata(null, (d, e) => ((TaskbarButtonService) d).OnThumbnailClipMarginCallbackChanged(e)));
            InternalItemsProperty = DependencyProperty.RegisterAttached("InternalItems", typeof(FreezableCollection<TaskbarThumbButtonInfo>), typeof(TaskbarButtonService), new PropertyMetadata(null));
        }

        public TaskbarButtonService()
        {
            SetInternalItems(this, new FreezableCollection<TaskbarThumbButtonInfo>());
            this.ItemInfo = new TaskbarItemInfo();
        }

        protected override Freezable CreateInstanceCore() => 
            this;

        internal static FreezableCollection<TaskbarThumbButtonInfo> GetInternalItems(TaskbarButtonService obj) => 
            (FreezableCollection<TaskbarThumbButtonInfo>) obj.GetValue(InternalItemsProperty);

        protected override void OnActualWindowChanged(Window oldWindow)
        {
            if (oldWindow != null)
            {
                oldWindow.SizeChanged -= new SizeChangedEventHandler(this.OnWindowSizeChanged);
                oldWindow.Loaded -= new RoutedEventHandler(this.OnWindowLoaded);
            }
            this.processWindowItemInfoChanged = false;
            Window actualWindow = base.ActualWindow;
            if (actualWindow == null)
            {
                BindingOperations.ClearBinding(this, WindowItemInfoProperty);
                this.ItemInfo = new TaskbarItemInfo();
            }
            else
            {
                if (actualWindow.TaskbarItemInfo == null)
                {
                    TaskbarInfoApplicator.SetTaskbarItemInfo(actualWindow, this.ItemInfo);
                }
                else
                {
                    actualWindow.TaskbarItemInfo.ProgressState = this.ItemInfo.ProgressState;
                    actualWindow.TaskbarItemInfo.ProgressValue = this.ItemInfo.ProgressValue;
                    actualWindow.TaskbarItemInfo.Description = this.ItemInfo.Description;
                    actualWindow.TaskbarItemInfo.Overlay = this.ItemInfo.Overlay;
                    actualWindow.TaskbarItemInfo.ThumbButtonInfos = this.ItemInfo.ThumbButtonInfos;
                    actualWindow.TaskbarItemInfo.ThumbnailClipMargin = this.ItemInfo.ThumbnailClipMargin;
                    this.ItemInfo = actualWindow.TaskbarItemInfo;
                }
                if (actualWindow.TaskbarItemInfo != null)
                {
                    Binding binding = new Binding("TaskbarItemInfo");
                    binding.Source = actualWindow;
                    binding.Mode = BindingMode.TwoWay;
                    binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                    BindingOperations.SetBinding(this, WindowItemInfoProperty, binding);
                    this.processWindowItemInfoChanged = true;
                }
                actualWindow.SizeChanged -= new SizeChangedEventHandler(this.OnWindowSizeChanged);
                actualWindow.SizeChanged += new SizeChangedEventHandler(this.OnWindowSizeChanged);
                actualWindow.Loaded -= new RoutedEventHandler(this.OnWindowLoaded);
                actualWindow.Loaded += new RoutedEventHandler(this.OnWindowLoaded);
                this.OnWindowSizeChanged(base.Window, null);
            }
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            this.UpdateInternalItems();
        }

        protected virtual void OnDescriptionChanged(DependencyPropertyChangedEventArgs e)
        {
            this.ItemInfo.Description = (string) e.NewValue;
        }

        protected override void OnDetaching()
        {
            GetInternalItems(this).Clear();
            base.OnDetaching();
        }

        private void OnItemInfoDescriptionChanged(DependencyPropertyChangedEventArgs e)
        {
            this.Description = (string) e.NewValue;
        }

        private void OnItemInfoOverlayIconChanged(DependencyPropertyChangedEventArgs e)
        {
            this.OverlayIcon = (ImageSource) e.NewValue;
        }

        private void OnItemInfoProgressStateChanged(DependencyPropertyChangedEventArgs e)
        {
            this.ProgressState = (TaskbarItemProgressState) e.NewValue;
        }

        private void OnItemInfoProgressValueChanged(DependencyPropertyChangedEventArgs e)
        {
            double newValue = (double) e.NewValue;
            if (Math.Abs((double) (this.ProgressValue - newValue)) > double.Epsilon)
            {
                this.ProgressValue = (double) e.NewValue;
            }
        }

        private void OnItemInfoThumbButtonInfosChanged(DependencyPropertyChangedEventArgs e)
        {
            this.ThumbButtonInfos = new TaskbarThumbButtonInfoCollection((ThumbButtonInfoCollection) e.NewValue);
        }

        private void OnItemInfoThumbnailClipMarginChanged(DependencyPropertyChangedEventArgs e)
        {
            this.ThumbnailClipMargin = (Thickness) e.NewValue;
        }

        protected virtual void OnOverlayIconChanged(DependencyPropertyChangedEventArgs e)
        {
            this.ItemInfo.Overlay = (ImageSource) e.NewValue;
        }

        protected virtual void OnProgressStateChanged(DependencyPropertyChangedEventArgs e)
        {
            this.ItemInfo.ProgressState = (TaskbarItemProgressState) e.NewValue;
        }

        protected virtual void OnProgressValueChanged(DependencyPropertyChangedEventArgs e)
        {
            double newValue = (double) e.NewValue;
            if (Math.Abs((double) (this.ItemInfo.ProgressValue - newValue)) > double.Epsilon)
            {
                this.ItemInfo.ProgressValue = newValue;
            }
        }

        protected virtual void OnThumbButtonInfosChanged(DependencyPropertyChangedEventArgs e)
        {
            TaskbarThumbButtonInfoCollection newValue = (TaskbarThumbButtonInfoCollection) e.NewValue;
            this.ItemInfo.ThumbButtonInfos = newValue.InternalCollection;
            this.UpdateInternalItems();
        }

        protected virtual void OnThumbnailClipMarginCallbackChanged(DependencyPropertyChangedEventArgs e)
        {
            this.UpdateThumbnailClipMargin();
        }

        protected virtual void OnThumbnailClipMarginChanged(DependencyPropertyChangedEventArgs e)
        {
            this.ItemInfo.ThumbnailClipMargin = (Thickness) e.NewValue;
        }

        private void OnWindowItemInfoChanged(DependencyPropertyChangedEventArgs e)
        {
            if (this.processWindowItemInfoChanged && (base.ActualWindow != null))
            {
                TaskbarItemInfo newValue = (TaskbarItemInfo) e.NewValue;
                if (newValue == null)
                {
                    newValue = new TaskbarItemInfo();
                    TaskbarInfoApplicator.SetTaskbarItemInfo(base.ActualWindow, newValue);
                }
                this.ItemInfo = newValue;
            }
        }

        private void OnWindowLoaded(object sender, EventArgs e)
        {
            this.OnWindowSizeChanged(sender, null);
        }

        protected virtual void OnWindowSizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.UpdateThumbnailClipMargin();
        }

        internal static void SetInternalItems(TaskbarButtonService obj, FreezableCollection<TaskbarThumbButtonInfo> value)
        {
            obj.SetValue(InternalItemsProperty, value);
        }

        private bool ShouldUpdateInternalItems()
        {
            if (base.IsAttached)
            {
                TaskbarThumbButtonInfoCollection thumbButtonInfos = this.ThumbButtonInfos;
                FreezableCollection<TaskbarThumbButtonInfo> internalItems = GetInternalItems(this);
                if (thumbButtonInfos.Count != internalItems.Count)
                {
                    return true;
                }
                for (int i = 0; i < thumbButtonInfos.Count; i++)
                {
                    TaskbarThumbButtonInfo objA = thumbButtonInfos[i];
                    TaskbarThumbButtonInfo objB = internalItems[i];
                    if (!ReferenceEquals(objA, objB))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private void UpdateInternalItems()
        {
            if (!this.lockUpdateInternalItems && this.ShouldUpdateInternalItems())
            {
                try
                {
                    this.lockUpdateInternalItems = true;
                    this.UpdateInternalItemsCore();
                }
                finally
                {
                    this.lockUpdateInternalItems = false;
                }
            }
        }

        internal virtual void UpdateInternalItemsCore()
        {
            GetInternalItems(this).Clear();
            foreach (TaskbarThumbButtonInfo info in this.ThumbButtonInfos)
            {
                GetInternalItems(this).Add(info);
            }
        }

        public void UpdateThumbnailClipMargin()
        {
            if ((base.ActualWindow != null) && (this.ThumbnailClipMarginCallback != null))
            {
                this.ThumbnailClipMargin = this.ThumbnailClipMarginCallback(new Size(base.ActualWindow.Width, base.ActualWindow.Height));
            }
        }

        public TaskbarItemProgressState ProgressState
        {
            get => 
                (TaskbarItemProgressState) base.GetValue(ProgressStateProperty);
            set => 
                base.SetValue(ProgressStateProperty, value);
        }

        public double ProgressValue
        {
            get => 
                (double) base.GetValue(ProgressValueProperty);
            set => 
                base.SetValue(ProgressValueProperty, value);
        }

        public ImageSource OverlayIcon
        {
            get => 
                (ImageSource) base.GetValue(OverlayIconProperty);
            set => 
                base.SetValue(OverlayIconProperty, value);
        }

        public string Description
        {
            get => 
                (string) base.GetValue(DescriptionProperty);
            set => 
                base.SetValue(DescriptionProperty, value);
        }

        public TaskbarThumbButtonInfoCollection ThumbButtonInfos
        {
            get => 
                (TaskbarThumbButtonInfoCollection) base.GetValue(ThumbButtonInfosProperty);
            set => 
                base.SetValue(ThumbButtonInfosProperty, value);
        }

        public Thickness ThumbnailClipMargin
        {
            get => 
                (Thickness) base.GetValue(ThumbnailClipMarginProperty);
            set => 
                base.SetValue(ThumbnailClipMarginProperty, value);
        }

        public Func<Size, Thickness> ThumbnailClipMarginCallback
        {
            get => 
                (Func<Size, Thickness>) base.GetValue(ThumbnailClipMarginCallbackProperty);
            set => 
                base.SetValue(ThumbnailClipMarginCallbackProperty, value);
        }

        private TaskbarItemInfo ItemInfo
        {
            get => 
                this.itemInfo;
            set
            {
                if (!ReferenceEquals(this.itemInfo, value))
                {
                    this.itemInfo = value;
                    Binding binding = new Binding("ProgressState");
                    binding.Source = this.itemInfo;
                    binding.Mode = BindingMode.TwoWay;
                    binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                    BindingOperations.SetBinding(this, ItemInfoProgressStateProperty, binding);
                    Binding binding2 = new Binding("ProgressValue");
                    binding2.Source = this.itemInfo;
                    binding2.Mode = BindingMode.TwoWay;
                    binding2.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                    BindingOperations.SetBinding(this, ItemInfoProgressValueProperty, binding2);
                    Binding binding3 = new Binding("Overlay");
                    binding3.Source = this.itemInfo;
                    binding3.Mode = BindingMode.TwoWay;
                    binding3.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                    BindingOperations.SetBinding(this, ItemInfoOverlayIconProperty, binding3);
                    Binding binding4 = new Binding("Description");
                    binding4.Source = this.itemInfo;
                    binding4.Mode = BindingMode.TwoWay;
                    binding4.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                    BindingOperations.SetBinding(this, ItemInfoDescriptionProperty, binding4);
                    Binding binding5 = new Binding("ThumbnailClipMargin");
                    binding5.Source = this.itemInfo;
                    binding5.Mode = BindingMode.TwoWay;
                    binding5.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                    BindingOperations.SetBinding(this, ItemInfoThumbnailClipMarginProperty, binding5);
                    Binding binding6 = new Binding("ThumbButtonInfos");
                    binding6.Source = this.itemInfo;
                    binding6.Mode = BindingMode.TwoWay;
                    binding6.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                    BindingOperations.SetBinding(this, ItemInfoThumbButtonInfosProperty, binding6);
                }
            }
        }

        IList<TaskbarThumbButtonInfo> ITaskbarButtonService.ThumbButtonInfos =>
            this.ThumbButtonInfos;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly TaskbarButtonService.<>c <>9 = new TaskbarButtonService.<>c();

            internal void <.cctor>b__71_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((TaskbarButtonService) d).OnProgressStateChanged(e);
            }

            internal void <.cctor>b__71_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((TaskbarButtonService) d).OnItemInfoProgressStateChanged(e);
            }

            internal void <.cctor>b__71_10(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((TaskbarButtonService) d).OnThumbnailClipMarginChanged(e);
            }

            internal void <.cctor>b__71_11(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((TaskbarButtonService) d).OnItemInfoThumbnailClipMarginChanged(e);
            }

            internal void <.cctor>b__71_12(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((TaskbarButtonService) d).OnWindowItemInfoChanged(e);
            }

            internal void <.cctor>b__71_13(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((TaskbarButtonService) d).OnThumbnailClipMarginCallbackChanged(e);
            }

            internal void <.cctor>b__71_2(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((TaskbarButtonService) d).OnProgressValueChanged(e);
            }

            internal void <.cctor>b__71_3(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((TaskbarButtonService) d).OnItemInfoProgressValueChanged(e);
            }

            internal void <.cctor>b__71_4(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((TaskbarButtonService) d).OnOverlayIconChanged(e);
            }

            internal void <.cctor>b__71_5(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((TaskbarButtonService) d).OnItemInfoOverlayIconChanged(e);
            }

            internal void <.cctor>b__71_6(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((TaskbarButtonService) d).OnDescriptionChanged(e);
            }

            internal void <.cctor>b__71_7(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((TaskbarButtonService) d).OnItemInfoDescriptionChanged(e);
            }

            internal void <.cctor>b__71_8(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((TaskbarButtonService) d).OnThumbButtonInfosChanged(e);
            }

            internal void <.cctor>b__71_9(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((TaskbarButtonService) d).OnItemInfoThumbButtonInfosChanged(e);
            }
        }
    }
}

