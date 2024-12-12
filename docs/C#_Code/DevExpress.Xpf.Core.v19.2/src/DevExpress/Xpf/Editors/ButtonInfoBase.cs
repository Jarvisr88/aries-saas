namespace DevExpress.Xpf.Editors
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Bars;
    using DevExpress.Xpf.Bars.Internal;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Automation.Peers;
    using System.Windows.Controls;
    using System.Windows.Media;

    public abstract class ButtonInfoBase : FrameworkContentElement, ICloneable, ILogicalOwner, IInputElement, ILogicalChildrenContainerProvider
    {
        public static readonly DependencyProperty IsDefaultButtonProperty;
        public static readonly DependencyProperty IsLeftProperty;
        public static readonly DependencyProperty TemplateProperty;
        public static readonly DependencyProperty ClickModeProperty;
        public static readonly DependencyProperty ForegroundProperty;
        public static readonly DependencyProperty MarginProperty;
        private static readonly DependencyPropertyKey ActualMarginPropertyKey;
        public static readonly DependencyProperty ActualMarginProperty;
        public static readonly DependencyProperty MarginCorrectionProperty;
        public static readonly DependencyProperty VisibilityProperty;
        public static readonly DependencyProperty IsMouseOverProperty;
        public static readonly DependencyProperty IsPressedProperty;
        public static readonly DependencyProperty IndexProperty;
        public static readonly DependencyProperty RenderTemplateProperty;
        public static readonly DependencyProperty RaiseClickEventInInplaceInactiveModeProperty;
        public static readonly DependencyProperty IsFirstProperty;
        private static readonly DependencyPropertyKey IsFirstPropertyKey;
        public static readonly DependencyProperty IsLastProperty;
        private static readonly DependencyPropertyKey IsLastPropertyKey;
        internal EventHandlerList events = new EventHandlerList();
        private IEnumerable<DependencyProperty> cloneProperties;

        event RoutedEventHandler ILogicalOwner.Loaded
        {
            add
            {
                base.Loaded += value;
            }
            remove
            {
                base.Loaded -= value;
            }
        }

        static ButtonInfoBase()
        {
            Type ownerType = typeof(ButtonInfoBase);
            TemplateProperty = DependencyPropertyManager.Register("Template", typeof(DataTemplate), ownerType, new FrameworkPropertyMetadata(null, new PropertyChangedCallback(ButtonInfoBase.OnTemplateChanged)));
            IsDefaultButtonProperty = DependencyPropertyManager.Register("IsDefaultButton", typeof(bool), ownerType, new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.None, (o, args) => ((ButtonInfoBase) o).IsDefaultButtonChanged((bool) args.NewValue)));
            IsLeftProperty = DependencyPropertyManager.Register("IsLeft", typeof(bool), ownerType, new FrameworkPropertyMetadata(false, new PropertyChangedCallback(ButtonInfoBase.OnIsLeftChanged)));
            ClickModeProperty = DependencyPropertyManager.Register("ClickMode", typeof(System.Windows.Controls.ClickMode), ownerType, new FrameworkPropertyMetadata(System.Windows.Controls.ClickMode.Press, (o, args) => ((ButtonInfoBase) o).ClickModeChanged((System.Windows.Controls.ClickMode) args.NewValue)));
            ForegroundProperty = DependencyPropertyManager.Register("Foreground", typeof(Brush), ownerType, new PropertyMetadata(null, (o, args) => ((ButtonInfoBase) o).ForegroundChanged((Brush) args.NewValue)));
            MarginCorrectionProperty = DependencyPropertyManager.Register("MarginCorrection", typeof(Thickness), ownerType, new FrameworkPropertyMetadata(new Thickness(-1.0), (o, args) => ((ButtonInfoBase) o).MarginCorrectionChanged((Thickness) args.NewValue)));
            MarginProperty = DependencyPropertyManager.Register("Margin", typeof(Thickness), ownerType, new FrameworkPropertyMetadata(new Thickness(0.0), (o, args) => ((ButtonInfoBase) o).MarginChanged((Thickness) args.NewValue)));
            ActualMarginPropertyKey = DependencyPropertyManager.RegisterReadOnly("ActualMargin", typeof(Thickness), ownerType, new PropertyMetadata(new Thickness(0.0), (o, args) => ((ButtonInfoBase) o).ActualMarginChanged((Thickness) args.NewValue)));
            ActualMarginProperty = ActualMarginPropertyKey.DependencyProperty;
            ContentElement.IsEnabledProperty.OverrideMetadata(ownerType, new FrameworkPropertyMetadata((d, args) => ((ButtonInfoBase) d).OnIsEnabledChanged((bool) args.NewValue)));
            RaiseClickEventInInplaceInactiveModeProperty = DependencyPropertyManager.Register("RaiseClickEventInInplaceInactiveMode", typeof(bool), ownerType, new FrameworkPropertyMetadata(true));
            RenderTemplateProperty = DependencyPropertyManager.Register("RenderTemplate", typeof(DevExpress.Xpf.Core.Native.RenderTemplate), ownerType, new FrameworkPropertyMetadata(null));
            VisibilityProperty = DependencyPropertyManager.Register("Visibility", typeof(System.Windows.Visibility), ownerType, new FrameworkPropertyMetadata(System.Windows.Visibility.Visible, (o, args) => ((ButtonInfoBase) o).VisibilityChanged((System.Windows.Visibility) args.NewValue)));
            IsMouseOverProperty = DependencyPropertyManager.Register("IsMouseOver", typeof(bool), ownerType, new PropertyMetadata(false));
            IsPressedProperty = DependencyPropertyManager.Register("IsPressed", typeof(bool), ownerType, new PropertyMetadata(false));
            IndexProperty = DependencyPropertyManager.Register("Index", typeof(int), ownerType, new FrameworkPropertyMetadata(0, (d, e) => ((ButtonInfoBase) d).OnIndexChanged()));
            IsFirstPropertyKey = DependencyPropertyManager.RegisterReadOnly("IsFirst", typeof(bool), ownerType, new FrameworkPropertyMetadata(false));
            IsFirstProperty = IsFirstPropertyKey.DependencyProperty;
            IsLastPropertyKey = DependencyPropertyManager.RegisterReadOnly("IsLast", typeof(bool), ownerType, new FrameworkPropertyMetadata(false));
            IsLastProperty = IsLastPropertyKey.DependencyProperty;
        }

        protected ButtonInfoBase()
        {
            this.LCContainer = new LogicalChildrenContainer(this);
        }

        protected virtual void ActualMarginChanged(Thickness value)
        {
        }

        protected void AssignToClone(ButtonInfoBase clone)
        {
            this.AssignToCloneInternal(clone);
            foreach (DependencyProperty property in this.CloneProperties)
            {
                this.AssignValueToClone(clone, property);
            }
        }

        protected virtual void AssignToCloneInternal(ButtonInfoBase clone)
        {
            clone.events.AddHandlers(this.events);
        }

        protected void AssignValueToClone(ButtonInfoBase clone, DependencyProperty property)
        {
            if (this.ShouldAssignToClone(property))
            {
                clone.SetValue(property, base.GetValue(property));
            }
        }

        private Thickness CalcActualMargin(Thickness thickness, Thickness marginCorrection, bool isLeft, bool isFirst, bool isLast)
        {
            Thickness thickness2 = thickness;
            Func<ButtonEdit, bool> func1 = <>c.<>9__99_0;
            if (<>c.<>9__99_0 == null)
            {
                Func<ButtonEdit, bool> local1 = <>c.<>9__99_0;
                func1 = <>c.<>9__99_0 = x => (x.EditMode != EditMode.Standalone) && (!x.ShowBorder || !x.ShowBorderInInplaceMode);
            }
            return (!((ButtonEdit) func1).Return<ButtonEdit, bool>(((Func<ButtonEdit, bool>) (<>c.<>9__99_1 ??= () => false)), (<>c.<>9__99_1 ??= () => false)) ? thickness2 : this.CalcActualMarginInternal(thickness, marginCorrection, isLeft, isFirst, isLast));
        }

        private Thickness CalcActualMarginInternal(Thickness thickness, Thickness marginCorrection, bool isLeft, bool isFirst, bool isLast)
        {
            Thickness thickness2 = thickness;
            if ((isLeft & isFirst) || (!isLeft & isLast))
            {
                ThicknessHelper.Inc(ref thickness2, marginCorrection);
            }
            return thickness2;
        }

        internal Thickness CalcRenderActualMargin() => 
            this.CalcActualMarginInternal(this.Margin, this.MarginCorrection, this.IsLeft, this.IsFirst, this.IsLast);

        protected virtual void ClickModeChanged(System.Windows.Controls.ClickMode value)
        {
        }

        protected abstract ButtonInfoBase CreateClone();
        protected virtual List<DependencyProperty> CreateCloneProperties() => 
            new List<DependencyProperty> { 
                IsDefaultButtonProperty,
                ClickModeProperty,
                IsLeftProperty,
                RenderTemplateProperty,
                RaiseClickEventInInplaceInactiveModeProperty,
                IndexProperty,
                FrameworkContentElement.CursorProperty
            };

        void ILogicalOwner.AddChild(object child)
        {
            base.AddLogicalChild(child);
        }

        void ILogicalOwner.RemoveChild(object child)
        {
            base.RemoveLogicalChild(child);
        }

        protected internal virtual void FindContent(RenderContentControlContext templatedParent)
        {
        }

        protected internal virtual void FindContent(FrameworkElement templatedParent)
        {
        }

        protected virtual void ForegroundChanged(Brush value)
        {
        }

        protected internal abstract AutomationPeer GetRenderAutomationPeer();
        protected internal bool IsClone(ButtonInfoBase clone) => 
            !(base.GetType() != clone.GetType()) ? (this.IsCloneInternal(clone) ? this.CloneProperties.TrueForEach<DependencyProperty>(p => ((this.IsPropertyAssigned(p) || clone.IsPropertyAssigned(p)) ? Equals(this.GetValue(p), clone.GetValue(p)) : true)) : false) : false;

        protected virtual bool IsCloneInternal(ButtonInfoBase clone) => 
            true;

        protected virtual void IsDefaultButtonChanged(bool value)
        {
        }

        protected virtual void MarginChanged(Thickness newValue)
        {
            this.UpdateActualMargin();
        }

        protected virtual void MarginCorrectionChanged(Thickness newValue)
        {
            this.UpdateActualMargin();
        }

        protected virtual void OnIndexChanged()
        {
            if (this.Owner != null)
            {
                this.Owner.UpdateButtonInfoCollections();
            }
        }

        protected virtual void OnIsEnabledChanged(bool value)
        {
        }

        protected virtual void OnIsLeftChanged()
        {
            if (this.Owner != null)
            {
                this.Owner.UpdateButtonInfoCollections();
            }
            this.UpdateActualMargin();
        }

        private static void OnIsLeftChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((ButtonInfoBase) obj).OnIsLeftChanged();
        }

        protected virtual void OnTemplateChanged()
        {
        }

        private static void OnTemplateChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((ButtonInfoBase) obj).OnTemplateChanged();
        }

        protected bool ShouldAssignToClone(DependencyProperty property) => 
            this.IsPropertySet(property);

        protected internal virtual void Subscribe(FrameworkElement element)
        {
        }

        protected internal virtual void SubscribeAsDefault(FrameworkElement element)
        {
        }

        object ICloneable.Clone()
        {
            ButtonInfoBase clone = this.CreateClone();
            this.AssignToClone(clone);
            return clone;
        }

        internal void UpdateActualMargin()
        {
            this.ActualMargin = this.CalcActualMargin(this.Margin, this.MarginCorrection, this.IsLeft, this.IsFirst, this.IsLast);
        }

        protected internal virtual void UpdateOnEditModeChanged()
        {
            this.UpdateActualMargin();
        }

        protected virtual void VisibilityChanged(System.Windows.Visibility value)
        {
        }

        protected RenderContentControlContext RenderButton { get; private set; }

        protected internal LogicalChildrenContainer LCContainer { get; private set; }

        protected override IEnumerator LogicalChildren
        {
            get
            {
                IEnumerator[] args = new IEnumerator[] { base.LogicalChildren, this.LCContainer.GetEnumerator() };
                return new MergedEnumerator(args);
            }
        }

        public bool IsFirst
        {
            get => 
                (bool) base.GetValue(IsFirstProperty);
            internal set => 
                base.SetValue(IsFirstPropertyKey, value);
        }

        public bool IsLast
        {
            get => 
                (bool) base.GetValue(IsLastProperty);
            internal set => 
                base.SetValue(IsLastPropertyKey, value);
        }

        public int Index
        {
            get => 
                (int) base.GetValue(IndexProperty);
            set => 
                base.SetValue(IndexProperty, value);
        }

        public Thickness ActualMargin
        {
            get => 
                (Thickness) base.GetValue(ActualMarginProperty);
            private set => 
                base.SetValue(ActualMarginPropertyKey, value);
        }

        public Thickness Margin
        {
            get => 
                (Thickness) base.GetValue(MarginProperty);
            set => 
                base.SetValue(MarginProperty, value);
        }

        public Thickness MarginCorrection
        {
            get => 
                (Thickness) base.GetValue(MarginCorrectionProperty);
            set => 
                base.SetValue(MarginCorrectionProperty, value);
        }

        protected ButtonEdit Owner =>
            LogicalTreeHelper.GetParent(this) as ButtonEdit;

        public bool IsMouseOver
        {
            get => 
                (bool) base.GetValue(IsMouseOverProperty);
            set => 
                base.SetValue(IsMouseOverProperty, value);
        }

        public bool IsPressed
        {
            get => 
                (bool) base.GetValue(IsPressedProperty);
            set => 
                base.SetValue(IsPressedProperty, value);
        }

        [Category("Behavior"), Description("Gets or sets the visibility of a button. This is a dependency property.")]
        public System.Windows.Visibility Visibility
        {
            get => 
                (System.Windows.Visibility) base.GetValue(VisibilityProperty);
            set => 
                base.SetValue(VisibilityProperty, value);
        }

        [Description("Gets or sets whether the button is the default button. This is a dependency property."), Category("Behavior")]
        public bool IsDefaultButton
        {
            get => 
                (bool) base.GetValue(IsDefaultButtonProperty);
            set => 
                base.SetValue(IsDefaultButtonProperty, value);
        }

        [Category("Layout"), Description("Gets or sets the button's alignment within a ButtonEdit control. This is a dependency property.")]
        public bool IsLeft
        {
            get => 
                (bool) base.GetValue(IsLeftProperty);
            set => 
                base.SetValue(IsLeftProperty, value);
        }

        [Browsable(false), Description("Gets or sets a button's template. This is a dependency property.")]
        public DataTemplate Template
        {
            get => 
                (DataTemplate) base.GetValue(TemplateProperty);
            set => 
                base.SetValue(TemplateProperty, value);
        }

        [Description("Gets or sets the template that is used to render the button contents in optimized mode. This is a dependency property."), Browsable(false)]
        public DevExpress.Xpf.Core.Native.RenderTemplate RenderTemplate
        {
            get => 
                (DevExpress.Xpf.Core.Native.RenderTemplate) base.GetValue(RenderTemplateProperty);
            set => 
                base.SetValue(RenderTemplateProperty, value);
        }

        [Browsable(false), Description("Specifies whether the ButtonInfo.Click event is raised when the edit mode is set to InplaceInactive. This is a dependency property.")]
        public bool RaiseClickEventInInplaceInactiveMode
        {
            get => 
                (bool) base.GetValue(RaiseClickEventInInplaceInactiveModeProperty);
            set => 
                base.SetValue(RaiseClickEventInInplaceInactiveModeProperty, value);
        }

        [Description("Gets or sets when the ButtonInfo.Click event occurs. This is a dependency property."), Category("Behavior")]
        public System.Windows.Controls.ClickMode ClickMode
        {
            get => 
                (System.Windows.Controls.ClickMode) base.GetValue(ClickModeProperty);
            set => 
                base.SetValue(ClickModeProperty, value);
        }

        public Brush Foreground
        {
            get => 
                (Brush) base.GetValue(ForegroundProperty);
            set => 
                base.SetValue(ForegroundProperty, value);
        }

        internal IEnumerable<DependencyProperty> CloneProperties
        {
            get
            {
                this.cloneProperties ??= this.CreateCloneProperties();
                return this.cloneProperties;
            }
        }

        double ILogicalOwner.ActualWidth =>
            0.0;

        double ILogicalOwner.ActualHeight =>
            0.0;

        ILogicalChildrenContainer2 ILogicalChildrenContainerProvider.LogicalChildrenContainer =>
            this.LCContainer;

        bool ILogicalOwner.IsLoaded =>
            base.IsLoaded;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ButtonInfoBase.<>c <>9 = new ButtonInfoBase.<>c();
            public static Func<ButtonEdit, bool> <>9__99_0;
            public static Func<bool> <>9__99_1;

            internal void <.cctor>b__19_0(DependencyObject o, DependencyPropertyChangedEventArgs args)
            {
                ((ButtonInfoBase) o).IsDefaultButtonChanged((bool) args.NewValue);
            }

            internal void <.cctor>b__19_1(DependencyObject o, DependencyPropertyChangedEventArgs args)
            {
                ((ButtonInfoBase) o).ClickModeChanged((ClickMode) args.NewValue);
            }

            internal void <.cctor>b__19_2(DependencyObject o, DependencyPropertyChangedEventArgs args)
            {
                ((ButtonInfoBase) o).ForegroundChanged((Brush) args.NewValue);
            }

            internal void <.cctor>b__19_3(DependencyObject o, DependencyPropertyChangedEventArgs args)
            {
                ((ButtonInfoBase) o).MarginCorrectionChanged((Thickness) args.NewValue);
            }

            internal void <.cctor>b__19_4(DependencyObject o, DependencyPropertyChangedEventArgs args)
            {
                ((ButtonInfoBase) o).MarginChanged((Thickness) args.NewValue);
            }

            internal void <.cctor>b__19_5(DependencyObject o, DependencyPropertyChangedEventArgs args)
            {
                ((ButtonInfoBase) o).ActualMarginChanged((Thickness) args.NewValue);
            }

            internal void <.cctor>b__19_6(DependencyObject d, DependencyPropertyChangedEventArgs args)
            {
                ((ButtonInfoBase) d).OnIsEnabledChanged((bool) args.NewValue);
            }

            internal void <.cctor>b__19_7(DependencyObject o, DependencyPropertyChangedEventArgs args)
            {
                ((ButtonInfoBase) o).VisibilityChanged((Visibility) args.NewValue);
            }

            internal void <.cctor>b__19_8(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ButtonInfoBase) d).OnIndexChanged();
            }

            internal bool <CalcActualMargin>b__99_0(ButtonEdit x) => 
                (x.EditMode != EditMode.Standalone) && (!x.ShowBorder || !x.ShowBorderInInplaceMode);

            internal bool <CalcActualMargin>b__99_1() => 
                false;
        }
    }
}

