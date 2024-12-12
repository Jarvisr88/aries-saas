namespace DevExpress.Xpf.Core.ConditionalFormatting.Native
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core.ConditionalFormatting;
    using DevExpress.Xpf.GridData;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Media;

    public abstract class FormatConditionBaseInfo
    {
        private readonly IColumnInfo unboundColumnInfo;

        public FormatConditionBaseInfo()
        {
            this.unboundColumnInfo = new ConditionalFormattingColumnInfo(() => this.ActualExpression);
        }

        public abstract Brush CoerceBackground(Brush value, FormatValueProvider provider);
        public abstract DataBarFormatInfo CoerceDataBarFormatInfo(DataBarFormatInfo value, FormatValueProvider provider);
        public virtual FontFamily CoerceFontFamily(FontFamily value, FormatValueProvider provider) => 
            value;

        public virtual double CoerceFontSize(double value, FormatValueProvider provider) => 
            value;

        public virtual FontStretch CoerceFontStretch(FontStretch value, FormatValueProvider provider) => 
            value;

        public virtual FontStyle CoerceFontStyle(FontStyle value, FormatValueProvider provider) => 
            value;

        public virtual FontWeight CoerceFontWeight(FontWeight value, FormatValueProvider provider) => 
            value;

        public virtual Brush CoerceForeground(Brush value, FormatValueProvider provider) => 
            value;

        public virtual TextDecorationCollection CoerceTextDecorations(TextDecorationCollection value, FormatValueProvider provider) => 
            value;

        public virtual IConditionalAnimationFactory CreateAnimationFactory() => 
            new EmptyConditionalAnimationFactory();

        internal virtual AnimationSettingsBase CreateDefaultAnimationSettings(DefaultAnimationSettings? settings)
        {
            ConditionalFormattingAnimationSettings settings2 = new ConditionalFormattingAnimationSettings();
            if (settings != null)
            {
                settings2.Duration = settings.Value.ConditionalFormattingAnimationDuration;
            }
            return settings2;
        }

        public IEnumerable<ConditionalFormatSummaryInfo> CreateSummaryItems() => 
            from x in this.GetSummaries() select new ConditionalFormatSummaryInfo(x, this.ActualFieldName);

        public static FormatConditionChangeType GetChangeType(DependencyPropertyChangedEventArgs e) => 
            ((e.OldValue == null) || (e.NewValue == null)) ? FormatConditionChangeType.All : FormatConditionChangeType.AppearanceOnly;

        public abstract IEnumerable<ConditionalFormatSummaryType> GetSummaries();
        public virtual IEnumerable<IColumnInfo> GetUnboundColumnInfo()
        {
            if (this.unboundColumnInfo.UnboundExpression == null)
            {
                return Enumerable.Empty<IColumnInfo>();
            }
            return new IColumnInfo[] { this.unboundColumnInfo };
        }

        public virtual bool HasNonEmptyTextDecorations() => 
            false;

        public bool IsAnimationEnabled(bool isEnabledByDefault) => 
            (this.AnimationSettings == null) ? ((this.AllowConditionalAnimation == null) ? isEnabledByDefault : this.AllowConditionalAnimation.Value) : true;

        public static bool IsFit(object value) => 
            (value as bool) ? ((bool) value) : false;

        public bool NeedFormatChange(AnimationTriggerContext context)
        {
            if (context == null)
            {
                return false;
            }
            Func<AnimationTriggerContext, DataUpdate> evaluator = <>c.<>9__43_0;
            if (<>c.<>9__43_0 == null)
            {
                Func<AnimationTriggerContext, DataUpdate> local1 = <>c.<>9__43_0;
                evaluator = <>c.<>9__43_0 = x => x.DataUpdate;
            }
            Func<DataUpdate, IValidationService> func2 = <>c.<>9__43_1;
            if (<>c.<>9__43_1 == null)
            {
                Func<DataUpdate, IValidationService> local2 = <>c.<>9__43_1;
                func2 = <>c.<>9__43_1 = y => y.ValidationService;
            }
            IValidationService local3 = context.With<AnimationTriggerContext, DataUpdate>(evaluator).With<DataUpdate, IValidationService>(func2);
            IValidationService local5 = local3;
            if (local3 == null)
            {
                IValidationService local4 = local3;
                local5 = new DefaultValidationService();
            }
            return local5.Execute(() => this.NeedFormatChangeOverride(context));
        }

        protected virtual bool NeedFormatChangeOverride(AnimationTriggerContext context) => 
            false;

        public static object OnCoerceFreezable(object baseValue)
        {
            Func<Freezable, Freezable> evaluator = <>c.<>9__17_0;
            if (<>c.<>9__17_0 == null)
            {
                Func<Freezable, Freezable> local1 = <>c.<>9__17_0;
                evaluator = <>c.<>9__17_0 = x => x.GetAsFrozen();
            }
            return (baseValue as Freezable).With<Freezable, Freezable>(evaluator);
        }

        public void OnFormatNameChanged(DependencyObject condition, string predefinedFormatName, string predefinedFormatsOwnerPath, DependencyProperty formatPropertyForBinding)
        {
            if (string.IsNullOrEmpty(predefinedFormatName))
            {
                BindingOperations.ClearBinding(condition, formatPropertyForBinding);
            }
            else
            {
                string[] textArray1 = new string[] { predefinedFormatsOwnerPath, this.OwnerPredefinedFormatsPropertyName, "[", predefinedFormatName, "].Format" };
                Binding binding = new Binding(string.Concat(textArray1));
                binding.RelativeSource = RelativeSource.Self;
                BindingOperations.SetBinding(condition, formatPropertyForBinding, binding);
            }
        }

        public Freezable FormatCore { get; set; }

        public string FieldName { get; set; }

        public string Expression { get; set; }

        public abstract string OwnerPredefinedFormatsPropertyName { get; }

        public string ActualFieldName =>
            (this.unboundColumnInfo.UnboundExpression != null) ? this.unboundColumnInfo.FieldName : this.FieldName;

        protected virtual string ActualExpression =>
            this.Expression;

        public abstract ConditionalFormatMask FormatMask { get; }

        public bool? AllowConditionalAnimation { get; set; }

        public AnimationSettingsBase AnimationSettings { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly FormatConditionBaseInfo.<>c <>9 = new FormatConditionBaseInfo.<>c();
            public static Func<Freezable, Freezable> <>9__17_0;
            public static Func<AnimationTriggerContext, DataUpdate> <>9__43_0;
            public static Func<DataUpdate, IValidationService> <>9__43_1;

            internal DataUpdate <NeedFormatChange>b__43_0(AnimationTriggerContext x) => 
                x.DataUpdate;

            internal IValidationService <NeedFormatChange>b__43_1(DataUpdate y) => 
                y.ValidationService;

            internal Freezable <OnCoerceFreezable>b__17_0(Freezable x) => 
                x.GetAsFrozen();
        }
    }
}

