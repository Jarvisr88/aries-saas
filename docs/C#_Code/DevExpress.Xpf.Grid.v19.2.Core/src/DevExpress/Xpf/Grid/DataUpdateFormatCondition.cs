namespace DevExpress.Xpf.Grid
{
    using DevExpress.Utils.Serializing;
    using DevExpress.Xpf.Core.ConditionalFormatting;
    using DevExpress.Xpf.Core.ConditionalFormatting.Native;
    using DevExpress.Xpf.Core.ConditionalFormatting.Printing;
    using DevExpress.Xpf.Core.ConditionalFormattingManager;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class DataUpdateFormatCondition : FormatConditionBase
    {
        public static readonly DependencyProperty FormatProperty = DependencyProperty.Register("Format", typeof(DevExpress.Xpf.Core.ConditionalFormatting.Format), typeof(DataUpdateFormatCondition), new PropertyMetadata(null, new PropertyChangedCallback(FormatConditionBase.OnFormatChanged), new CoerceValueCallback(FormatConditionBase.OnCoerceFreezable)));
        public static readonly DependencyProperty AnimationSettingsProperty;
        public static readonly DependencyProperty RuleProperty;

        static DataUpdateFormatCondition()
        {
            AnimationSettingsProperty = DependencyProperty.Register("AnimationSettings", typeof(DataUpdateAnimationSettings), typeof(DataUpdateFormatCondition), new PropertyMetadata(null, (d, e) => ((DataUpdateFormatCondition) d).OnInfoPropertyChanged(e)));
            RuleProperty = DependencyProperty.Register("Rule", typeof(DataUpdateRule), typeof(DataUpdateFormatCondition), new PropertyMetadata(DataUpdateRule.Never, (d, e) => ((DataUpdateFormatCondition) d).OnInfoPropertyChanged(e)));
        }

        protected override BaseEditUnit CreateEmptyEditUnit() => 
            new AnimationEditUnit();

        internal override FormatConditionRuleBase CreateExportWrapper() => 
            new DataUpdateFormatConditionExportWrapper();

        protected override FormatConditionBaseInfo CreateInfo() => 
            new DataUpdateConditionInfo();

        internal override bool HasVisualSettings() => 
            (this.AnimationSettings == null) ? base.HasVisualSettings() : true;

        protected override void SyncProperty(DependencyProperty property)
        {
            base.SyncProperty(property);
            base.SyncIfNeeded(property, AnimationSettingsProperty, () => this.ValueInfo.AnimationSettings = this.AnimationSettings);
            base.SyncIfNeeded(property, RuleProperty, () => this.ValueInfo.Rule = this.Rule);
        }

        protected override void UpdateEditUnit(BaseEditUnit unit)
        {
            base.UpdateEditUnit(unit);
            AnimationEditUnit unit2 = unit as AnimationEditUnit;
            if (unit2 != null)
            {
                unit2.Format = this.Format;
                unit2.Rule = this.Rule;
                unit2.AnimationSettings = this.AnimationSettings;
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

        [XtraSerializableProperty(XtraSerializationVisibility.Content)]
        public DataUpdateAnimationSettings AnimationSettings
        {
            get => 
                (DataUpdateAnimationSettings) base.GetValue(AnimationSettingsProperty);
            set => 
                base.SetValue(AnimationSettingsProperty, value);
        }

        [XtraSerializableProperty]
        public DataUpdateRule Rule
        {
            get => 
                (DataUpdateRule) base.GetValue(RuleProperty);
            set => 
                base.SetValue(RuleProperty, value);
        }

        private DataUpdateConditionInfo ValueInfo =>
            base.Info as DataUpdateConditionInfo;

        public override DependencyProperty FormatPropertyForBinding =>
            FormatProperty;

        internal override DependencyProperty ActualAnimationSettingsProperty =>
            AnimationSettingsProperty;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DataUpdateFormatCondition.<>c <>9 = new DataUpdateFormatCondition.<>c();

            internal void <.cctor>b__25_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DataUpdateFormatCondition) d).OnInfoPropertyChanged(e);
            }

            internal void <.cctor>b__25_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DataUpdateFormatCondition) d).OnInfoPropertyChanged(e);
            }
        }
    }
}

