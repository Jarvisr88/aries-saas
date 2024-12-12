namespace DevExpress.Xpf.Grid
{
    using DevExpress.Utils.Serializing;
    using DevExpress.Xpf.Core.ConditionalFormatting;
    using DevExpress.Xpf.Core.ConditionalFormatting.Native;
    using DevExpress.Xpf.Core.ConditionalFormattingManager;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public abstract class IndicatorFormatConditionBase : FormatConditionBase
    {
        public static readonly DependencyProperty MinValueProperty;
        public static readonly DependencyProperty MaxValueProperty;
        public static readonly DependencyProperty SelectiveExpressionProperty;
        public static readonly DependencyProperty AnimateTransitionProperty;
        public static readonly DependencyProperty AnimationSettingsProperty;

        static IndicatorFormatConditionBase()
        {
            MinValueProperty = DependencyProperty.Register("MinValue", typeof(object), typeof(IndicatorFormatConditionBase), new PropertyMetadata(null, (d, e) => ((IndicatorFormatConditionBase) d).OnMinMaxChanged(e)));
            MaxValueProperty = DependencyProperty.Register("MaxValue", typeof(object), typeof(IndicatorFormatConditionBase), new PropertyMetadata(null, (d, e) => ((IndicatorFormatConditionBase) d).OnMinMaxChanged(e)));
            SelectiveExpressionProperty = DependencyProperty.Register("SelectiveExpression", typeof(string), typeof(IndicatorFormatConditionBase), new PropertyMetadata(null, (d, e) => ((IndicatorFormatConditionBase) d).OnSelectiveExpressionChanged(e)));
            AnimateTransitionProperty = DependencyProperty.Register("AnimateTransition", typeof(bool?), typeof(IndicatorFormatConditionBase), new PropertyMetadata(null, (d, e) => ((IndicatorFormatConditionBase) d).OnInfoPropertyChanged(e)));
            AnimationSettingsProperty = DependencyProperty.Register("AnimationSettings", typeof(ConditionalFormattingAnimationSettings), typeof(IndicatorFormatConditionBase), new PropertyMetadata(null, (d, e) => ((IndicatorFormatConditionBase) d).OnInfoPropertyChanged(e)));
        }

        protected IndicatorFormatConditionBase()
        {
        }

        private void OnMinMaxChanged(DependencyPropertyChangedEventArgs e)
        {
            this.IndicatorInfo.OnMinMaxChanged(e.NewValue, ReferenceEquals(e.Property, MaxValueProperty));
            base.OnChanged(e, FormatConditionBaseInfo.GetChangeType(e));
        }

        private void OnSelectiveExpressionChanged(DependencyPropertyChangedEventArgs e)
        {
            this.IndicatorInfo.SelectiveExpression = this.SelectiveExpression;
            base.OnChanged(e, FormatConditionChangeType.All);
        }

        protected override void SyncProperty(DependencyProperty property)
        {
            base.SyncProperty(property);
            base.SyncIfNeeded(property, SelectiveExpressionProperty, () => this.IndicatorInfo.SelectiveExpression = this.SelectiveExpression);
            base.SyncIfNeeded(property, AnimateTransitionProperty, () => this.IndicatorInfo.AllowConditionalAnimation = this.AnimateTransition);
            base.SyncIfNeeded(property, AnimationSettingsProperty, () => this.IndicatorInfo.AnimationSettings = this.AnimationSettings);
        }

        protected override void UpdateEditUnit(BaseEditUnit unit)
        {
            base.UpdateEditUnit(unit);
            IndicatorEditUnit unit2 = unit as IndicatorEditUnit;
            if (unit2 != null)
            {
                unit2.MinValue = this.MinValue;
                unit2.MaxValue = this.MaxValue;
            }
        }

        [XtraSerializableProperty]
        public object MinValue
        {
            get => 
                base.GetValue(MinValueProperty);
            set => 
                base.SetValue(MinValueProperty, value);
        }

        [XtraSerializableProperty]
        public object MaxValue
        {
            get => 
                base.GetValue(MaxValueProperty);
            set => 
                base.SetValue(MaxValueProperty, value);
        }

        [XtraSerializableProperty]
        public string SelectiveExpression
        {
            get => 
                (string) base.GetValue(SelectiveExpressionProperty);
            set => 
                base.SetValue(SelectiveExpressionProperty, value);
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

        private IndicatorFormatConditionInfo IndicatorInfo =>
            base.Info as IndicatorFormatConditionInfo;

        private IndicatorFormatBase FormatCore =>
            (IndicatorFormatBase) base.GetValue(this.FormatPropertyForBinding);

        protected override bool CanAttach =>
            !string.IsNullOrEmpty(base.FieldName);

        internal override DependencyProperty ActualAnimationSettingsProperty =>
            AnimationSettingsProperty;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly IndicatorFormatConditionBase.<>c <>9 = new IndicatorFormatConditionBase.<>c();

            internal void <.cctor>b__33_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((IndicatorFormatConditionBase) d).OnMinMaxChanged(e);
            }

            internal void <.cctor>b__33_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((IndicatorFormatConditionBase) d).OnMinMaxChanged(e);
            }

            internal void <.cctor>b__33_2(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((IndicatorFormatConditionBase) d).OnSelectiveExpressionChanged(e);
            }

            internal void <.cctor>b__33_3(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((IndicatorFormatConditionBase) d).OnInfoPropertyChanged(e);
            }

            internal void <.cctor>b__33_4(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((IndicatorFormatConditionBase) d).OnInfoPropertyChanged(e);
            }
        }
    }
}

