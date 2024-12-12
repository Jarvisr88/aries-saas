namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Media;

    public class FrameworkRenderElementContext : IFrameworkRenderElementContext
    {
        public static readonly int DataContextPropertyKey;
        public static readonly int ForegroundPropertyKey;
        public static readonly int IsEnabledPropertyKey;
        public static readonly int IsTouchEnabledPropertyKey;
        public static readonly int UseLayoutRoundingPropertyKey;
        public static readonly int FlowDirectionPropertyKey;
        public static readonly int FontSettingsPropertyKey;
        public static readonly int SvgPalettePropertyKey;
        public static readonly int SvgStatePropertyKey;
        protected internal FREInheritedProperties InheritedProperties;
        private System.Windows.Visibility? visibility;
        private double? opacity;
        private bool? allowMouseCapturing;
        private Size renderSize;
        private Size aggregateSize;
        private System.Windows.HorizontalAlignment? ha;
        private System.Windows.Media.Transform transform;
        private Point? transformOrigin;
        private System.Windows.VerticalAlignment? va;
        private Thickness? margin;
        private bool isMouseOverCore;
        private bool? showBounds;
        private bool neverMeasured;
        private bool neverArranged;
        private double? minHeight;
        private double? height;
        private double? maxHeight;
        private double? minWidth;
        private double? width;
        private double? maxWidth;
        private readonly Locker isInSupportInitialize;
        private Size desiredSize;
        private bool isMeasureValid;
        private bool isArrangeValid;
        private bool isCollapsed;
        private System.Windows.Controls.Dock? dock;
        private bool useMirrorTransform;
        private readonly Locker resetValueLocker;

        static FrameworkRenderElementContext();
        public FrameworkRenderElementContext(FrameworkRenderElement factory);
        public virtual void AddChild(FrameworkRenderElementContext child);
        public void Arrange(Rect finalRect);
        protected internal virtual void AttachToVisualTree(FrameworkElement root);
        protected bool CaptureMouse();
        public void CoerceValue(int propertyKey);
        protected virtual void DataContextChanged(object oldValue, object newValue);
        private void DataContextChangedImpl(object oldValue, object newValue);
        protected internal virtual void DetachFromVisualTree(FrameworkElement root);
        bool IFrameworkRenderElementContext.CaptureMouse();
        FrameworkRenderElementContext IFrameworkRenderElementContext.GetRenderChild(int index);
        void IFrameworkRenderElementContext.OnGotMouseCapture();
        void IFrameworkRenderElementContext.OnLostMouseCapture();
        void IFrameworkRenderElementContext.OnMouseDown(MouseRenderEventArgs args);
        void IFrameworkRenderElementContext.OnMouseEnter(MouseRenderEventArgs args);
        void IFrameworkRenderElementContext.OnMouseLeave(MouseRenderEventArgs args);
        void IFrameworkRenderElementContext.OnMouseMove(MouseRenderEventArgs args);
        void IFrameworkRenderElementContext.OnMouseUp(MouseRenderEventArgs args);
        void IFrameworkRenderElementContext.OnPreviewMouseDown(MouseRenderEventArgs args);
        void IFrameworkRenderElementContext.OnPreviewMouseUp(MouseRenderEventArgs args);
        void IFrameworkRenderElementContext.ReleaseMouseCapture();
        protected virtual void FontSettingsChanged(RenderFontSettings oldValue, RenderFontSettings newValue);
        protected virtual RenderFontSettings FontSettingsChanging(RenderFontSettings value, FREInheritedPropertyValueSource valueSource);
        protected virtual void ForegroundChanged(object oldValue, object newValue);
        private object ForegroundChanging(Brush value, FREInheritedPropertyValueSource valueSource);
        private System.Windows.FlowDirection GetChromeFlowDirectionIfRoot();
        protected virtual FrameworkRenderElementContext GetRenderChild(int index);
        public FrameworkRenderElementContext GetUIParentNo3DTraversal();
        public FrameworkRenderElementContext GetUIParentWithinLayoutIsland();
        public object GetValue(string propertyName);
        protected virtual object GetValueOverride(string propertyName);
        public virtual bool HitTest(Point point);
        public void InvalidateArrange();
        public void InvalidateArrangeInternal();
        public void InvalidateMeasure();
        public void InvalidateMeasureInternal();
        public void InvalidateVisual();
        protected virtual void IsTouchEnabledChanged(bool oldValue, bool newValue);
        public void Measure(Size availableSize);
        public void OnChildDesiredSizeChanged(FrameworkRenderElementContext context);
        protected virtual void OnFlowDirectionChanged(System.Windows.FlowDirection oldValue, System.Windows.FlowDirection newValue);
        protected virtual System.Windows.FlowDirection OnFlowDirectionChanging(System.Windows.FlowDirection value);
        private System.Windows.FlowDirection OnFlowDirectionChangingCore(System.Windows.FlowDirection value, FREInheritedPropertyValueSource valueSource);
        protected virtual void OnGotMouseCapture();
        protected virtual void OnIsEnabledChanged(bool oldValue, bool newValue);
        protected virtual bool OnIsEnabledChanging(bool value);
        private bool OnIsEnabledChangingCore(bool value);
        protected virtual void OnIsMouseOverCoreChanged();
        protected virtual bool OnIsTouchEnabledChanging(bool value);
        private object OnIsTouchEnabledChangingCore(bool value);
        protected virtual void OnLostMouseCapture();
        protected virtual void OnMouseDown(MouseRenderEventArgs args);
        protected virtual void OnMouseEnter(MouseRenderEventArgs args);
        protected virtual void OnMouseLeave(MouseRenderEventArgs args);
        protected virtual void OnMouseMove(MouseRenderEventArgs args);
        protected virtual void OnMouseUp(MouseRenderEventArgs args);
        protected virtual void OnPreviewMouseDown(MouseRenderEventArgs args);
        protected virtual void OnPreviewMouseUp(MouseRenderEventArgs args);
        private object OnUseLayoutRoundingChangingCore(bool value, FREInheritedPropertyValueSource valueSource);
        public virtual void PreApplyTemplate();
        public virtual void Release();
        protected void ReleaseMouseCapture();
        public virtual void RemoveChild(FrameworkRenderElementContext child);
        public void Render(DrawingContext dc);
        protected virtual void RenderSizeChanged();
        private void ResetRenderCaches();
        protected virtual void ResetRenderCachesInternal();
        protected internal virtual void ResetSizeSpecificCaches();
        private void ResetTemplatesAndVisuals();
        protected virtual void ResetTemplatesAndVisualsInternal();
        private void ResetTriggers(RenderValueSource valueSource);
        public void ResetValue(string propertyName);
        protected virtual void ResetValueCore(string propertyName);
        protected virtual void ResetValueOverride(string propertyName);
        public void SetIsArrangeInvalid();
        public void SetIsArrangeValid();
        public void SetIsMeasureInvalid();
        public void SetIsMeasureValid();
        protected bool SetProperty<TProperty>(ref TProperty container, TProperty value, FREInvalidateOptions invalidateOptions = 0x1a);
        public void SetValue(string propertyName, object value);
        protected virtual void SetValueOverride(string propertyName, object value);
        public virtual bool ShouldUseMirrorTransform();
        public virtual bool ShouldUseTransform();
        protected virtual void SvgPaletteChanged(WpfSvgPalette oldValue, WpfSvgPalette newValue);
        private object SvgPaletteChanging(WpfSvgPalette value, FREInheritedPropertyValueSource valueSource);
        protected virtual void SvgStateChanged(string oldValue, string newValue);
        private object SvgStateChanging(string value, FREInheritedPropertyValueSource valueSource);
        public void UpdateLayout(FREInvalidateOptions invalidateOptions = 0x1a);
        protected internal virtual void UpdateOpacity();
        public virtual void UpdateRenderTransform();
        protected void UpdateTouchState(INamescope scope, bool newValue);
        private void VisibilityChanged();

        public WpfSvgPalette SvgPalette { get; set; }

        public string SvgState { get; set; }

        public System.Windows.FlowDirection FlowDirection { get; set; }

        public bool IsMouseOverCore { get; set; }

        public bool IsMouseCaptured { get; private set; }

        public System.Windows.Media.Transform Transform { get; set; }

        public Point? TransformOrigin { get; set; }

        public System.Windows.Visibility? Visibility { get; set; }

        public bool AllowMouseCapturing { get; set; }

        public double? Opacity { get; set; }

        protected internal double ActualOpacity { get; }

        public object DataContext { get; set; }

        public RenderFontSettings FontSettings { get; set; }

        public Brush Foreground { get; set; }

        public bool IsEnabled { get; set; }

        public bool IsTouchEnabled { get; protected internal set; }

        public System.Windows.Controls.Dock? Dock { get; set; }

        public System.Windows.HorizontalAlignment? HorizontalAlignment { get; set; }

        public System.Windows.VerticalAlignment? VerticalAlignment { get; set; }

        public Thickness? Margin { get; set; }

        public bool? ShowBounds { get; set; }

        public bool UseLayoutRounding { get; set; }

        public double? MinHeight { get; set; }

        public double? Height { get; set; }

        public double? MaxHeight { get; set; }

        public double? MinWidth { get; set; }

        public double? Width { get; set; }

        public double? MaxWidth { get; set; }

        public RenderRequest MeasureRequest { get; internal set; }

        public RenderRequest ArrangeRequest { get; internal set; }

        public bool IsArrangeValid { get; }

        public bool IsMeasureValid { get; }

        public bool MeasureInProgress { get; internal set; }

        public bool ArrangeInProgress { get; internal set; }

        public bool IsLayoutIslandRoot { get; internal set; }

        public bool HasTemplateGeneratedSubTree { get; internal set; }

        public int TreeLevel { get; internal set; }

        public bool IsCollapsed { get; internal set; }

        public bool NeverMeasured { get; internal set; }

        public bool NeverArranged { get; internal set; }

        public bool IsLayoutSuspended { get; internal set; }

        public bool IsInSupportInitialize { get; }

        public virtual bool AttachToRoot { get; }

        public bool IsAttachedToRoot { get; internal set; }

        public bool MeasureDuringArrange { get; internal set; }

        public System.Windows.Media.Transform RenderTransform { get; protected set; }

        public Geometry LayoutClip { get; internal set; }

        public string Name { get; }

        public Size DesiredSize { get; internal set; }

        public Size AggregateSize { get; set; }

        public Size? UnclippedDesiredSize { get; internal set; }

        public Size RenderSize { get; set; }

        public Vector VisualOffset { get; internal set; }

        public bool NeedsClipBounds { get; internal set; }

        public Rect RenderRect { get; }

        public Rect FinalRect { get; internal set; }

        public Size PreviousAvailableSize { get; internal set; }

        public Rect PreviousArrangeRect { get; }

        public FrameworkRenderElement Factory { get; private set; }

        public string ThemeInfo { get; }

        int IFrameworkRenderElementContext.RenderChildrenCount { get; }

        protected virtual int RenderChildrenCount { get; }

        public DevExpress.Xpf.Core.Native.Namescope NamescopeHolder { get; internal set; }

        public INamescope Namescope { get; }

        public IElementHost ElementHost { get; }

        public FrameworkRenderElementContext Parent { get; private set; }

        public double DpiScale { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly FrameworkRenderElementContext.<>c <>9;

            static <>c();
            internal object <.cctor>b__36_0(FrameworkRenderElementContext frec, object v, FREInheritedPropertyValueSource vs);
            internal void <.cctor>b__36_1(FrameworkRenderElementContext frec, object ov, object nv);
            internal void <.cctor>b__36_10(FrameworkRenderElementContext frec, object ov, object nv);
            internal object <.cctor>b__36_11(FrameworkRenderElementContext frec, object v, FREInheritedPropertyValueSource vs);
            internal void <.cctor>b__36_12(FrameworkRenderElementContext frec, object ov, object nv);
            internal object <.cctor>b__36_13(FrameworkRenderElementContext frec, object v, FREInheritedPropertyValueSource vs);
            internal void <.cctor>b__36_14(FrameworkRenderElementContext frec, object ov, object nv);
            internal object <.cctor>b__36_15(FrameworkRenderElementContext frec, object v, FREInheritedPropertyValueSource vs);
            internal object <.cctor>b__36_2(FrameworkRenderElementContext frec, object v, FREInheritedPropertyValueSource vs);
            internal void <.cctor>b__36_3(FrameworkRenderElementContext frec, object ov, object nv);
            internal object <.cctor>b__36_4(FrameworkRenderElementContext frec, object v, FREInheritedPropertyValueSource vs);
            internal void <.cctor>b__36_5(FrameworkRenderElementContext frec, object ov, object nv);
            internal object <.cctor>b__36_6(FrameworkRenderElementContext frec, object v, FREInheritedPropertyValueSource vs);
            internal void <.cctor>b__36_7(FrameworkRenderElementContext frec, object ov, object nv);
            internal void <.cctor>b__36_8(FrameworkRenderElementContext frec, object ov, object nv);
            internal object <.cctor>b__36_9(FrameworkRenderElementContext frec, object v, FREInheritedPropertyValueSource vs);
        }
    }
}

