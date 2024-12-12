namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Media;

    public abstract class RenderControlBaseContext : FrameworkRenderElementContext
    {
        private HorizontalAlignment hca;
        private VerticalAlignment vca;

        protected RenderControlBaseContext(RenderControlBase factory);
        protected bool CanSetValue(string propertyName);
        protected override void FontSettingsChanged(RenderFontSettings oldValue, RenderFontSettings newValue);
        protected override void ForegroundChanged(object oldValue, object newValue);
        protected override object GetValueOverride(string propertyName);
        protected virtual void HorizontalContentAlignmentChanged();
        protected virtual bool IsContextProperty(string propertyName);
        public override void Release();
        protected override void ResetValueOverride(string propertyName);
        protected override void SetValueOverride(string propertyName, object value);
        public override bool ShouldUseTransform();
        protected internal virtual void UpdateControlFontSettings();
        protected internal virtual void UpdateControlForeground();
        protected internal virtual void UpdateControlProperty(DependencyProperty dp, object value);
        protected virtual void UpdateGeneralTransform();
        protected internal override void UpdateOpacity();
        public override void UpdateRenderTransform();
        protected virtual void VerticalContentAlignmentChanged();

        public HorizontalAlignment HorizontalContentAlignment { get; set; }

        public VerticalAlignment VerticalContentAlignment { get; set; }

        public override bool AttachToRoot { get; }

        public virtual FrameworkElement Control { get; internal set; }

        public Transform GeneralTransform { get; protected set; }

        protected virtual bool UpdateControlPropertyWithCurrentValue { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly RenderControlBaseContext.<>c <>9;
            public static Func<FrameworkRenderElementContext, double> <>9__24_0;
            public static Func<double, double, double> <>9__24_1;

            static <>c();
            internal double <UpdateOpacity>b__24_0(FrameworkRenderElementContext x);
            internal double <UpdateOpacity>b__24_1(double a, double b);
        }
    }
}

