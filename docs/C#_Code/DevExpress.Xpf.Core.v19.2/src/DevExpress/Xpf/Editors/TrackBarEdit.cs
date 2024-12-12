namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Editors.Automation;
    using DevExpress.Xpf.Printing;
    using DevExpress.Xpf.Utils;
    using DevExpress.Xpf.Utils.About;
    using System;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Automation.Peers;
    using System.Windows.Controls.Primitives;
    using System.Windows.Data;
    using System.Windows.Input;
    using System.Windows.Media;

    [DXToolboxBrowsable(DXToolboxItemKind.Free), LicenseProvider(typeof(DX_WPFEditors_LicenseProvider))]
    public class TrackBarEdit : RangeBaseEdit, ITrackBarExportSettings, ITextExportSettings, IExportSettings
    {
        private static DoubleCollection empty;
        public static readonly DependencyProperty IsSnapToTickEnabledProperty;
        public static readonly DependencyProperty IsZoomProperty;
        private static readonly DependencyPropertyKey IsZoomPropertyKey;
        public static readonly DependencyProperty IsRangeProperty;
        private static readonly DependencyPropertyKey IsRangePropertyKey;
        public static readonly DependencyProperty TickPlacementProperty;
        public static readonly DependencyProperty TickFrequencyProperty;
        public static readonly DependencyProperty SelectionStartProperty;
        public static readonly DependencyProperty SelectionEndProperty;
        public static readonly DependencyProperty IsMoveToPointEnabledProperty;
        public static readonly DependencyProperty TicksProperty;
        public static readonly DependencyProperty StepsProperty;

        static TrackBarEdit()
        {
            Type ownerType = typeof(TrackBarEdit);
            TickPlacementProperty = DependencyPropertyManager.Register("TickPlacement", typeof(System.Windows.Controls.Primitives.TickPlacement), ownerType, new FrameworkPropertyMetadata(System.Windows.Controls.Primitives.TickPlacement.BottomRight));
            TickFrequencyProperty = DependencyPropertyManager.Register("TickFrequency", typeof(double), ownerType, new FrameworkPropertyMetadata(5.0));
            TicksProperty = DependencyProperty.Register("Ticks", typeof(DoubleCollection), ownerType, new FrameworkPropertyMetadata(Empty));
            StepsProperty = DependencyProperty.Register("Steps", typeof(DoubleCollection), ownerType, new FrameworkPropertyMetadata(Empty));
            BaseEdit.EditValueProperty.OverrideMetadata(ownerType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, null, null, true, UpdateSourceTrigger.PropertyChanged));
            IsMoveToPointEnabledProperty = DependencyPropertyManager.Register("IsMoveToPointEnabled", typeof(bool?), ownerType);
            IsSnapToTickEnabledProperty = DependencyPropertyManager.Register("IsSnapToTickEnabled", typeof(bool), ownerType, new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.None, new PropertyChangedCallback(TrackBarEdit.IsSnapToTickEnabledPropertyChanged)));
            SelectionStartProperty = DependencyPropertyManager.Register("SelectionStart", typeof(double), ownerType, new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.None, new PropertyChangedCallback(TrackBarEdit.SelectionStartPropertyChanged), new CoerceValueCallback(TrackBarEdit.CoerceSelectionStart)));
            SelectionEndProperty = DependencyPropertyManager.Register("SelectionEnd", typeof(double), ownerType, new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.None, new PropertyChangedCallback(TrackBarEdit.SelectionEndPropertyChanged), new CoerceValueCallback(TrackBarEdit.CoerceSelectionEnd)));
            IsRangePropertyKey = DependencyPropertyManager.RegisterReadOnly("IsRange", typeof(bool), ownerType, new FrameworkPropertyMetadata(false));
            IsRangeProperty = IsRangePropertyKey.DependencyProperty;
            IsZoomPropertyKey = DependencyPropertyManager.RegisterReadOnly("IsZoom", typeof(bool), ownerType, new FrameworkPropertyMetadata(false));
            IsZoomProperty = IsZoomPropertyKey.DependencyProperty;
        }

        public TrackBarEdit()
        {
            this.SetDefaultStyleKey(typeof(TrackBarEdit));
        }

        private static object CoerceSelectionEnd(DependencyObject d, object value) => 
            ((TrackBarEdit) d).EditStrategy.CoerceSelectionEnd((double) value);

        private static object CoerceSelectionStart(DependencyObject d, object value) => 
            ((TrackBarEdit) d).EditStrategy.CoerceSelectionStart((double) value);

        protected override ActualPropertyProvider CreateActualPropertyProvider() => 
            new TrackBarEditPropertyProvider(this);

        protected override EditStrategyBase CreateEditStrategy() => 
            new TrackBarEditStrategy(this);

        public void Decrement(double value, TrackBarIncrementTargetEnum target)
        {
            this.EditStrategy.IncrementDecrement(value, false, false, target);
        }

        public void DecrementLarge(TrackBarIncrementTargetEnum target)
        {
            this.EditStrategy.DecrementLarge(target);
        }

        public void DecrementSmall(TrackBarIncrementTargetEnum target)
        {
            this.EditStrategy.DecrementSmall(target);
        }

        protected override string GetExportText() => 
            $"{this.GetTextValueInternal()}";

        public void Increment(double value, TrackBarIncrementTargetEnum target)
        {
            this.EditStrategy.IncrementDecrement(value, true, false, target);
        }

        public void IncrementLarge(TrackBarIncrementTargetEnum target)
        {
            this.EditStrategy.IncrementLarge(target);
        }

        public void IncrementSmall(TrackBarIncrementTargetEnum target)
        {
            this.EditStrategy.IncrementSmall(target);
        }

        protected virtual void IsSnapToTickEnabledChanged(bool value)
        {
            this.EditStrategy.IsSnapToTickEnabledChanged(value);
        }

        private static void IsSnapToTickEnabledPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((TrackBarEdit) d).IsSnapToTickEnabledChanged((bool) e.NewValue);
        }

        public void Maximize(TrackBarIncrementTargetEnum target)
        {
            this.EditStrategy.Maximize(target);
        }

        public void Minimize(TrackBarIncrementTargetEnum target)
        {
            this.EditStrategy.Minimize(target);
        }

        protected override AutomationPeer OnCreateAutomationPeer() => 
            new TrackBarEditAutomationPeer(this);

        protected override void OnPreviewMouseDown(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseDown(e);
            this.EditStrategy.PreviewMouseDown(e);
        }

        protected override void OnPreviewMouseUp(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseUp(e);
            this.EditStrategy.PreviewMouseUp(e);
        }

        protected virtual void SelectionEndChanged(double oldValue, double value)
        {
            this.EditStrategy.SelectionEndChanged(oldValue, value);
            TrackBarEditAutomationPeer peer = (TrackBarEditAutomationPeer) UIElementAutomationPeer.FromElement(this);
            if (peer != null)
            {
                peer.RaiseSelectionEndPropertyChangedEvent(oldValue, value);
            }
        }

        private static void SelectionEndPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((TrackBarEdit) d).SelectionEndChanged((double) e.OldValue, (double) e.NewValue);
        }

        protected virtual void SelectionStartChanged(double oldValue, double value)
        {
            this.EditStrategy.SelectionStartChanged(oldValue, value);
            TrackBarEditAutomationPeer peer = (TrackBarEditAutomationPeer) UIElementAutomationPeer.FromElement(this);
            if (peer != null)
            {
                peer.RaiseSelectionStartPropertyChangedEvent(oldValue, value);
            }
        }

        private static void SelectionStartPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((TrackBarEdit) d).SelectionStartChanged((double) e.OldValue, (double) e.NewValue);
        }

        internal static DoubleCollection Empty
        {
            get
            {
                if (empty == null)
                {
                    DoubleCollection doubles = new DoubleCollection();
                    doubles.Freeze();
                    empty = doubles;
                }
                return empty;
            }
        }

        private TrackBarEditStrategy EditStrategy =>
            base.EditStrategy as TrackBarEditStrategy;

        public DoubleCollection Steps
        {
            get => 
                (DoubleCollection) base.GetValue(StepsProperty);
            set => 
                base.SetValue(StepsProperty, value);
        }

        public DoubleCollection Ticks
        {
            get => 
                (DoubleCollection) base.GetValue(TicksProperty);
            set => 
                base.SetValue(TicksProperty, value);
        }

        [Description("Gets or sets the location of ticks.")]
        public System.Windows.Controls.Primitives.TickPlacement TickPlacement
        {
            get => 
                (System.Windows.Controls.Primitives.TickPlacement) base.GetValue(TickPlacementProperty);
            set => 
                base.SetValue(TickPlacementProperty, value);
        }

        [Description("Gets or sets a value that specifies the delta between ticks drawn on a track bar.")]
        public double TickFrequency
        {
            get => 
                (double) base.GetValue(TickFrequencyProperty);
            set => 
                base.SetValue(TickFrequencyProperty, value);
        }

        public bool? IsMoveToPointEnabled
        {
            get => 
                (bool?) base.GetValue(IsMoveToPointEnabledProperty);
            set => 
                base.SetValue(IsMoveToPointEnabledProperty, value);
        }

        [Description("Gets or sets whether snapping to ticks is enabled.")]
        public bool IsSnapToTickEnabled
        {
            get => 
                (bool) base.GetValue(IsSnapToTickEnabledProperty);
            set => 
                base.SetValue(IsSnapToTickEnabledProperty, value);
        }

        [Description("Gets or sets the start of the range.")]
        public double SelectionStart
        {
            get => 
                (double) base.GetValue(SelectionStartProperty);
            set => 
                base.SetValue(SelectionStartProperty, value);
        }

        [Description("Gets or sets the end of the range.")]
        public double SelectionEnd
        {
            get => 
                (double) base.GetValue(SelectionEndProperty);
            set => 
                base.SetValue(SelectionEndProperty, value);
        }

        [Description("Gets whether a track bar allows a range of values to be specified.")]
        public bool IsRange
        {
            get => 
                (bool) base.GetValue(IsRangeProperty);
            internal set => 
                base.SetValue(IsRangePropertyKey, value);
        }

        [Description("Gets whether a track bar displays zoom buttons.")]
        public bool IsZoom
        {
            get => 
                (bool) base.GetValue(IsZoomProperty);
            internal set => 
                base.SetValue(IsZoomPropertyKey, value);
        }

        [Browsable(true), Category("Behavior")]
        public BaseEditStyleSettings StyleSettings
        {
            get => 
                base.StyleSettings;
            set => 
                base.StyleSettings = value;
        }

        protected internal override Type StyleSettingsType =>
            typeof(TrackBarStyleSettings);

        int ITrackBarExportSettings.Position =>
            base.ToInt(this.EditStrategy.GetRealValue(base.GetPrintPosition()));

        int ITrackBarExportSettings.Minimum =>
            base.ToInt(base.Minimum);

        int ITrackBarExportSettings.Maximum =>
            base.ToInt(base.Maximum);

        Color IExportSettings.Foreground =>
            Colors.DimGray;
    }
}

