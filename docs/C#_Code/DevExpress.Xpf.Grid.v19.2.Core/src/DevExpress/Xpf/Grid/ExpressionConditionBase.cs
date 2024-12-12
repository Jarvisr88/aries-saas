namespace DevExpress.Xpf.Grid
{
    using DevExpress.Utils.Serializing;
    using DevExpress.Xpf.Core.ConditionalFormatting;
    using DevExpress.Xpf.Core.ConditionalFormattingManager;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public abstract class ExpressionConditionBase : FormatConditionBase
    {
        public static readonly DependencyProperty FormatProperty = DependencyProperty.Register("Format", typeof(DevExpress.Xpf.Core.ConditionalFormatting.Format), typeof(ExpressionConditionBase), new PropertyMetadata(null, new PropertyChangedCallback(FormatConditionBase.OnFormatChanged), new CoerceValueCallback(FormatConditionBase.OnCoerceFreezable)));
        public static readonly DependencyProperty AnimateTransitionProperty;
        public static readonly DependencyProperty AnimationSettingsProperty;

        static ExpressionConditionBase()
        {
            AnimateTransitionProperty = DependencyProperty.Register("AnimateTransition", typeof(bool?), typeof(ExpressionConditionBase), new PropertyMetadata(null, (d, e) => ((ExpressionConditionBase) d).OnInfoPropertyChanged(e)));
            AnimationSettingsProperty = DependencyProperty.Register("AnimationSettings", typeof(ConditionalFormattingAnimationSettings), typeof(ExpressionConditionBase), new PropertyMetadata(null, (d, e) => ((ExpressionConditionBase) d).OnInfoPropertyChanged(e)));
        }

        protected ExpressionConditionBase()
        {
        }

        protected override void SyncProperty(DependencyProperty property)
        {
            base.SyncProperty(property);
            base.SyncIfNeeded(property, AnimateTransitionProperty, () => base.Info.AllowConditionalAnimation = this.AnimateTransition);
            base.SyncIfNeeded(property, AnimationSettingsProperty, () => base.Info.AnimationSettings = this.AnimationSettings);
        }

        protected override void UpdateEditUnit(BaseEditUnit unit)
        {
            base.UpdateEditUnit(unit);
            ExpressionEditUnit unit2 = unit as ExpressionEditUnit;
            if (unit2 != null)
            {
                unit2.Format = this.Format;
            }
        }

        [XtraSerializableProperty(XtraSerializationVisibility.Content)]
        public DevExpress.Xpf.Core.ConditionalFormatting.Format Format
        {
            get => 
                (DevExpress.Xpf.Core.ConditionalFormatting.Format) base.GetValue(FormatProperty);
            set => 
                base.SetValue(FormatProperty, value);
        }

        [XtraSerializableProperty]
        public bool? AnimateTransition
        {
            get => 
                (bool?) base.GetValue(AnimateTransitionProperty);
            set => 
                base.SetValue(AnimateTransitionProperty, value);
        }

        [XtraSerializableProperty(XtraSerializationVisibility.Content)]
        public ConditionalFormattingAnimationSettings AnimationSettings
        {
            get => 
                (ConditionalFormattingAnimationSettings) base.GetValue(AnimationSettingsProperty);
            set => 
                base.SetValue(AnimationSettingsProperty, value);
        }

        public override DependencyProperty FormatPropertyForBinding =>
            FormatProperty;

        internal override DependencyProperty ActualAnimationSettingsProperty =>
            AnimationSettingsProperty;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ExpressionConditionBase.<>c <>9 = new ExpressionConditionBase.<>c();

            internal void <.cctor>b__19_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ExpressionConditionBase) d).OnInfoPropertyChanged(e);
            }

            internal void <.cctor>b__19_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ExpressionConditionBase) d).OnInfoPropertyChanged(e);
            }
        }
    }
}

