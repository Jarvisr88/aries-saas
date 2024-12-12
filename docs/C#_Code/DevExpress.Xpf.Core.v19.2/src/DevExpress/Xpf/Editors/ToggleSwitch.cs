namespace DevExpress.Xpf.Editors
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Utils;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Editors.Internal;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;

    [DXToolboxBrowsable(DXToolboxItemKind.Free), ToolboxTabName("DX.19.2: Common Controls")]
    public class ToggleSwitch : ToggleStateButton
    {
        public static readonly DependencyProperty CheckedStateContentProperty;
        public static readonly DependencyProperty IsReadOnlyProperty;
        public static readonly DependencyProperty UncheckedStateContentProperty;
        public static readonly DependencyProperty EnableAnimationProperty;
        public static readonly DependencyProperty ContentPlacementProperty;
        public static readonly DependencyProperty ToggleSwitchWidthProperty;
        public static readonly DependencyProperty ToggleSwitchHeightProperty;
        public static readonly DependencyProperty CheckedStateContentTemplateProperty;
        public static readonly DependencyProperty UncheckedStateContentTemplateProperty;
        private static readonly DependencyPropertyKey HasCheckedStateContentPropertyKey;
        public static readonly DependencyProperty HasCheckedStateContentProperty;
        private static readonly DependencyPropertyKey HasUncheckedStateContentPropertyKey;
        public static readonly DependencyProperty HasUncheckedStateContentProperty;
        private static readonly DependencyPropertyKey ActualUncheckedStateContentPropertyKey;
        public static readonly DependencyProperty ActualUncheckedStateContentProperty;
        private static readonly DependencyPropertyKey ActualCheckedStateContentPropertyKey;
        public static readonly DependencyProperty ActualCheckedStateContentProperty;
        private static readonly DependencyPropertyKey ActualUncheckedStateContentTemplatePropertyKey;
        public static readonly DependencyProperty ActualUncheckedStateContentTemplateProperty;
        private static readonly DependencyPropertyKey ActualCheckedStateContentTemplatePropertyKey;
        public static readonly DependencyProperty ActualCheckedStateContentTemplateProperty;
        public static readonly DependencyProperty HasForegroundProperty;
        private static readonly DependencyPropertyKey HasForegroundPropertyKey;
        public static readonly DependencyProperty AnimationModeProperty;
        public static readonly DependencyProperty SwitchThumbTemplateProperty;
        public static readonly DependencyProperty SwitchBorderTemplateProperty;
        private Locker animationLocker = new Locker();

        static ToggleSwitch()
        {
            Type forType = typeof(ToggleSwitch);
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(forType, new FrameworkPropertyMetadata(forType));
            CheckedStateContentProperty = DependencyProperty.Register("CheckedStateContent", typeof(object), forType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure, (d, e) => ((ToggleSwitch) d).OnToggleSwitchContentChanged()));
            UncheckedStateContentProperty = DependencyProperty.Register("UncheckedStateContent", typeof(object), forType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure, (d, e) => ((ToggleSwitch) d).OnToggleSwitchContentChanged()));
            EnableAnimationProperty = DependencyProperty.Register("EnableAnimation", typeof(bool), forType, new FrameworkPropertyMetadata(true));
            ToggleButton.IsCheckedProperty.AddOwner(forType, new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsMeasure, (d, e) => ((ToggleSwitch) d).OnIsCheckedChanged()));
            ToggleButton.IsThreeStateProperty.AddOwner(forType, new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsMeasure, (d, e) => ((ToggleSwitch) d).OnIsThreeStateChanged()));
            ContentPlacementProperty = DependencyProperty.Register("ContentPlacement", typeof(ToggleSwitchContentPlacement), forType, new FrameworkPropertyMetadata(ToggleSwitchContentPlacement.Near, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure, (d, e) => ((ToggleSwitch) d).OnContentPlacementChanged()));
            ToggleSwitchWidthProperty = DependencyProperty.Register("ToggleSwitchWidth", typeof(double), forType, new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsMeasure, null, (CoerceValueCallback) ((d, e) => ((ToggleSwitch) d).CoerceSwitchSize((double) e))));
            ToggleSwitchHeightProperty = DependencyProperty.Register("ToggleSwitchHeight", typeof(double), forType, new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsMeasure, null, (CoerceValueCallback) ((d, e) => ((ToggleSwitch) d).CoerceSwitchSize((double) e))));
            CheckedStateContentTemplateProperty = DependencyProperty.Register("CheckedStateContentTemplate", typeof(DataTemplate), forType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure, (d, e) => ((ToggleSwitch) d).OnToggleSwitchContentTemplateChanged()));
            UncheckedStateContentTemplateProperty = DependencyProperty.Register("UncheckedStateContentTemplate", typeof(DataTemplate), forType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure, (d, e) => ((ToggleSwitch) d).OnToggleSwitchContentTemplateChanged()));
            HasUncheckedStateContentPropertyKey = DependencyProperty.RegisterReadOnly("HasUncheckedStateContent", typeof(bool), forType, new FrameworkPropertyMetadata(false));
            HasUncheckedStateContentProperty = HasUncheckedStateContentPropertyKey.DependencyProperty;
            HasCheckedStateContentPropertyKey = DependencyProperty.RegisterReadOnly("HasCheckedStateContent", typeof(bool), forType, new FrameworkPropertyMetadata(false));
            HasCheckedStateContentProperty = HasCheckedStateContentPropertyKey.DependencyProperty;
            ActualCheckedStateContentPropertyKey = DependencyProperty.RegisterReadOnly("ActualCheckedStateContent", typeof(object), forType, new FrameworkPropertyMetadata(string.Empty, (d, e) => ((ToggleSwitch) d).OnActualContentChanged()));
            ActualCheckedStateContentProperty = ActualCheckedStateContentPropertyKey.DependencyProperty;
            ActualUncheckedStateContentPropertyKey = DependencyProperty.RegisterReadOnly("ActualUncheckedStateContent", typeof(object), forType, new FrameworkPropertyMetadata(string.Empty, (d, e) => ((ToggleSwitch) d).OnActualContentChanged()));
            ActualUncheckedStateContentProperty = ActualUncheckedStateContentPropertyKey.DependencyProperty;
            ActualCheckedStateContentTemplatePropertyKey = DependencyProperty.RegisterReadOnly("ActualCheckedStateContentTemplate", typeof(DataTemplate), forType, new FrameworkPropertyMetadata(null, (d, e) => ((ToggleSwitch) d).OnActualContentTemplateChanged()));
            ActualCheckedStateContentTemplateProperty = ActualCheckedStateContentTemplatePropertyKey.DependencyProperty;
            ActualUncheckedStateContentTemplatePropertyKey = DependencyProperty.RegisterReadOnly("ActualUncheckedStateContentTemplate", typeof(DataTemplate), forType, new FrameworkPropertyMetadata(null, (d, e) => ((ToggleSwitch) d).OnActualContentTemplateChanged()));
            ActualUncheckedStateContentTemplateProperty = ActualUncheckedStateContentTemplatePropertyKey.DependencyProperty;
            IsReadOnlyProperty = DependencyProperty.Register("IsReadOnly", typeof(bool), forType, new FrameworkPropertyMetadata(false));
            HasForegroundPropertyKey = DependencyProperty.RegisterReadOnly("HasForeground", typeof(bool), forType, new FrameworkPropertyMetadata(false));
            HasForegroundProperty = HasForegroundPropertyKey.DependencyProperty;
            AnimationModeProperty = DependencyProperty.Register("AnimationMode", typeof(ToggleSwitchAnimationMode), forType, new FrameworkPropertyMetadata(ToggleSwitchAnimationMode.Always));
            SwitchThumbTemplateProperty = DependencyProperty.Register("SwitchThumbTemplate", typeof(DataTemplate), forType);
            SwitchBorderTemplateProperty = DependencyProperty.Register("SwitchBorderTemplate", typeof(DataTemplate), forType);
        }

        public ToggleSwitch()
        {
            this.MoveSwitchThumbLocker = new Locker();
            base.VerticalContentAlignment = VerticalAlignment.Stretch;
            base.HorizontalContentAlignment = HorizontalAlignment.Stretch;
        }

        private void ChromeLoaded(object sender, RoutedEventArgs e)
        {
            this.UpdateLayoutProvider();
            this.MoveSwitchThumb(false);
        }

        private double CoerceSwitchSize(double value) => 
            Math.Max(0.0, value);

        private bool IsForegroundSet()
        {
            BaseValueSource baseValueSource = DependencyPropertyHelper.GetValueSource(this, Control.ForegroundProperty).BaseValueSource;
            return ((baseValueSource != BaseValueSource.Default) && (baseValueSource != BaseValueSource.Inherited));
        }

        protected void MoveSwitchThumb(bool shouldAnimate)
        {
            shouldAnimate = !this.animationLocker.IsLocked & shouldAnimate;
            if ((this.Chrome != null) && !this.MoveSwitchThumbLocker.IsLocked)
            {
                this.Chrome.MoveSwitchThumb(base.IsChecked, shouldAnimate);
            }
        }

        private void OnActualContentChanged()
        {
            this.UpdateHasContent();
        }

        private void OnActualContentTemplateChanged()
        {
            this.UpdateHasContent();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.Chrome = (ToggleSwitchButtonChrome) base.GetTemplateChild("PART_Owner");
            this.Chrome.Owner = this;
            this.Chrome.Loaded += new RoutedEventHandler(this.ChromeLoaded);
        }

        protected override void OnContentChanged(object oldContent, object newContent)
        {
            base.OnContentChanged(oldContent, newContent);
            this.UpdateActualContent();
        }

        private void OnContentPlacementChanged()
        {
            this.UpdateLayoutProvider();
        }

        protected override void OnContentTemplateChanged(DataTemplate oldContentTemplate, DataTemplate newContentTemplate)
        {
            base.OnContentTemplateChanged(oldContentTemplate, newContentTemplate);
            this.UpdateActualContentTemplate();
        }

        protected override void OnContentTemplateSelectorChanged(DataTemplateSelector oldContentTemplateSelector, DataTemplateSelector newContentTemplateSelector)
        {
            base.OnContentTemplateSelectorChanged(oldContentTemplateSelector, newContentTemplateSelector);
            this.UpdateActualContentTemplate();
        }

        protected virtual void OnIsCheckedChanged()
        {
            this.MoveSwitchThumb(this.EnableAnimation && (this.AnimationMode == ToggleSwitchAnimationMode.Always));
        }

        private void OnIsThreeStateChanged()
        {
            Action<ToggleSwitchButtonChrome> action = <>c.<>9__117_0;
            if (<>c.<>9__117_0 == null)
            {
                Action<ToggleSwitchButtonChrome> local1 = <>c.<>9__117_0;
                action = <>c.<>9__117_0 = x => x.UpdateLayoutProvider();
            }
            this.Chrome.Do<ToggleSwitchButtonChrome>(action);
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (ReferenceEquals(e.Property, Control.ForegroundProperty))
            {
                this.HasForeground = (e.NewValue != null) && this.IsForegroundSet();
            }
        }

        protected override void OnToggle()
        {
            base.OnToggle();
            if (!this.IsReadOnly)
            {
                bool? isChecked = null;
                bool? nullable = base.IsChecked;
                bool flag = true;
                if (!((nullable.GetValueOrDefault() == flag) ? (nullable != null) : false))
                {
                    isChecked = new bool?(base.IsChecked != null);
                }
                else
                {
                    bool? nullable1;
                    if (!base.IsThreeState)
                    {
                        nullable1 = false;
                    }
                    else
                    {
                        nullable = null;
                        nullable1 = nullable;
                    }
                    isChecked = nullable1;
                }
                this.MoveSwitchThumbLocker.DoLockedAction(() => this.SetCurrentValue(ToggleButton.IsCheckedProperty, isChecked));
                this.MoveSwitchThumb(this.EnableAnimation);
            }
        }

        private void OnToggleSwitchContentChanged()
        {
            this.UpdateActualContent();
        }

        private void OnToggleSwitchContentTemplateChanged()
        {
            this.UpdateActualContentTemplate();
        }

        internal void SetValueInternal(bool? value)
        {
            this.animationLocker.DoLockedAction<bool?>(delegate {
                bool? nullable;
                this.IsChecked = nullable = value;
                return nullable;
            });
        }

        private void UpdateActualCheckedStateContentTemplate()
        {
            if (this.CheckedStateContentTemplate != null)
            {
                this.ActualCheckedStateContentTemplate = this.CheckedStateContentTemplate;
            }
            else
            {
                this.ActualCheckedStateContentTemplate = (base.ContentTemplateSelector != null) ? base.ContentTemplateSelector.SelectTemplate(base.DataContext, this) : base.ContentTemplate;
            }
        }

        private void UpdateActualContent()
        {
            object checkedStateContent = this.CheckedStateContent;
            object content = checkedStateContent;
            if (checkedStateContent == null)
            {
                object local1 = checkedStateContent;
                content = base.Content;
            }
            this.ActualCheckedStateContent = content;
            object uncheckedStateContent = this.UncheckedStateContent;
            object content = uncheckedStateContent;
            if (uncheckedStateContent == null)
            {
                object local2 = uncheckedStateContent;
                content = base.Content;
            }
            this.ActualUncheckedStateContent = content;
        }

        private void UpdateActualContentTemplate()
        {
            this.UpdateActualCheckedStateContentTemplate();
            this.UpdateActualUncheckedStateContentTemplate();
        }

        private void UpdateActualUncheckedStateContentTemplate()
        {
            if (this.UncheckedStateContentTemplate != null)
            {
                this.ActualUncheckedStateContentTemplate = this.UncheckedStateContentTemplate;
            }
            else
            {
                this.ActualUncheckedStateContentTemplate = (base.ContentTemplateSelector != null) ? base.ContentTemplateSelector.SelectTemplate(base.DataContext, this) : base.ContentTemplate;
            }
        }

        private void UpdateHasContent()
        {
            this.HasCheckedStateContent = (this.CheckedStateContent != null) || (this.CheckedStateContentTemplate != null);
            this.HasUncheckedStateContent = (this.UncheckedStateContent != null) || (this.UncheckedStateContentTemplate != null);
        }

        private void UpdateLayoutProvider()
        {
            Action<ToggleSwitchButtonChrome> action = <>c.<>9__116_0;
            if (<>c.<>9__116_0 == null)
            {
                Action<ToggleSwitchButtonChrome> local1 = <>c.<>9__116_0;
                action = <>c.<>9__116_0 = x => x.UpdateLayoutProvider();
            }
            this.Chrome.Do<ToggleSwitchButtonChrome>(action);
        }

        public DataTemplate SwitchThumbTemplate
        {
            get => 
                (DataTemplate) base.GetValue(SwitchThumbTemplateProperty);
            set => 
                base.SetValue(SwitchThumbTemplateProperty, value);
        }

        public DataTemplate SwitchBorderTemplate
        {
            get => 
                (DataTemplate) base.GetValue(SwitchBorderTemplateProperty);
            set => 
                base.SetValue(SwitchBorderTemplateProperty, value);
        }

        public ToggleSwitchAnimationMode AnimationMode
        {
            get => 
                (ToggleSwitchAnimationMode) base.GetValue(AnimationModeProperty);
            set => 
                base.SetValue(AnimationModeProperty, value);
        }

        public bool HasForeground
        {
            get => 
                (bool) base.GetValue(HasForegroundProperty);
            private set => 
                base.SetValue(HasForegroundPropertyKey, value);
        }

        public bool IsReadOnly
        {
            get => 
                (bool) base.GetValue(IsReadOnlyProperty);
            set => 
                base.SetValue(IsReadOnlyProperty, value);
        }

        public object ActualCheckedStateContent
        {
            get => 
                base.GetValue(ActualCheckedStateContentProperty);
            private set => 
                base.SetValue(ActualCheckedStateContentPropertyKey, value);
        }

        public object ActualUncheckedStateContent
        {
            get => 
                base.GetValue(ActualUncheckedStateContentProperty);
            private set => 
                base.SetValue(ActualUncheckedStateContentPropertyKey, value);
        }

        public DataTemplate ActualCheckedStateContentTemplate
        {
            get => 
                (DataTemplate) base.GetValue(ActualCheckedStateContentTemplateProperty);
            private set => 
                base.SetValue(ActualCheckedStateContentTemplatePropertyKey, value);
        }

        public DataTemplate ActualUncheckedStateContentTemplate
        {
            get => 
                (DataTemplate) base.GetValue(ActualUncheckedStateContentTemplateProperty);
            private set => 
                base.SetValue(ActualUncheckedStateContentTemplatePropertyKey, value);
        }

        public bool HasCheckedStateContent
        {
            get => 
                (bool) base.GetValue(HasCheckedStateContentProperty);
            private set => 
                base.SetValue(HasCheckedStateContentPropertyKey, value);
        }

        public bool HasUncheckedStateContent
        {
            get => 
                (bool) base.GetValue(HasUncheckedStateContentProperty);
            private set => 
                base.SetValue(HasUncheckedStateContentPropertyKey, value);
        }

        public DataTemplate CheckedStateContentTemplate
        {
            get => 
                (DataTemplate) base.GetValue(CheckedStateContentTemplateProperty);
            set => 
                base.SetValue(CheckedStateContentTemplateProperty, value);
        }

        public DataTemplate UncheckedStateContentTemplate
        {
            get => 
                (DataTemplate) base.GetValue(UncheckedStateContentTemplateProperty);
            set => 
                base.SetValue(UncheckedStateContentTemplateProperty, value);
        }

        public double ToggleSwitchWidth
        {
            get => 
                (double) base.GetValue(ToggleSwitchWidthProperty);
            set => 
                base.SetValue(ToggleSwitchWidthProperty, value);
        }

        public double ToggleSwitchHeight
        {
            get => 
                (double) base.GetValue(ToggleSwitchHeightProperty);
            set => 
                base.SetValue(ToggleSwitchHeightProperty, value);
        }

        public ToggleSwitchContentPlacement ContentPlacement
        {
            get => 
                (ToggleSwitchContentPlacement) base.GetValue(ContentPlacementProperty);
            set => 
                base.SetValue(ContentPlacementProperty, value);
        }

        public object CheckedStateContent
        {
            get => 
                base.GetValue(CheckedStateContentProperty);
            set => 
                base.SetValue(CheckedStateContentProperty, value);
        }

        public object UncheckedStateContent
        {
            get => 
                base.GetValue(UncheckedStateContentProperty);
            set => 
                base.SetValue(UncheckedStateContentProperty, value);
        }

        public bool EnableAnimation
        {
            get => 
                (bool) base.GetValue(EnableAnimationProperty);
            set => 
                base.SetValue(EnableAnimationProperty, value);
        }

        private ToggleSwitchButtonChrome Chrome { get; set; }

        private Locker MoveSwitchThumbLocker { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ToggleSwitch.<>c <>9 = new ToggleSwitch.<>c();
            public static Action<ToggleSwitchButtonChrome> <>9__116_0;
            public static Action<ToggleSwitchButtonChrome> <>9__117_0;

            internal void <.cctor>b__26_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ToggleSwitch) d).OnToggleSwitchContentChanged();
            }

            internal void <.cctor>b__26_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ToggleSwitch) d).OnToggleSwitchContentChanged();
            }

            internal void <.cctor>b__26_10(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ToggleSwitch) d).OnActualContentChanged();
            }

            internal void <.cctor>b__26_11(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ToggleSwitch) d).OnActualContentTemplateChanged();
            }

            internal void <.cctor>b__26_12(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ToggleSwitch) d).OnActualContentTemplateChanged();
            }

            internal void <.cctor>b__26_2(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ToggleSwitch) d).OnIsCheckedChanged();
            }

            internal void <.cctor>b__26_3(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ToggleSwitch) d).OnIsThreeStateChanged();
            }

            internal void <.cctor>b__26_4(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ToggleSwitch) d).OnContentPlacementChanged();
            }

            internal object <.cctor>b__26_5(DependencyObject d, object e) => 
                ((ToggleSwitch) d).CoerceSwitchSize((double) e);

            internal object <.cctor>b__26_6(DependencyObject d, object e) => 
                ((ToggleSwitch) d).CoerceSwitchSize((double) e);

            internal void <.cctor>b__26_7(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ToggleSwitch) d).OnToggleSwitchContentTemplateChanged();
            }

            internal void <.cctor>b__26_8(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ToggleSwitch) d).OnToggleSwitchContentTemplateChanged();
            }

            internal void <.cctor>b__26_9(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ToggleSwitch) d).OnActualContentChanged();
            }

            internal void <OnIsThreeStateChanged>b__117_0(ToggleSwitchButtonChrome x)
            {
                x.UpdateLayoutProvider();
            }

            internal void <UpdateLayoutProvider>b__116_0(ToggleSwitchButtonChrome x)
            {
                x.UpdateLayoutProvider();
            }
        }
    }
}

