namespace DevExpress.Xpf.Core.ConditionalFormatting.Native
{
    using DevExpress.Xpf.Core.ConditionalFormatting;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Media;

    public abstract class ExpressionConditionBaseInfo : FormatConditionBaseInfo
    {
        protected ExpressionConditionBaseInfo()
        {
        }

        public abstract bool CalcCondition(FormatValueProvider provider);
        public override Brush CoerceBackground(Brush value, FormatValueProvider provider) => 
            this.CoerceValue<Brush>(value, DevExpress.Xpf.Core.ConditionalFormatting.Format.BackgroundProperty, provider, null);

        public override DataBarFormatInfo CoerceDataBarFormatInfo(DataBarFormatInfo value, FormatValueProvider provider) => 
            this.CoerceValue<DataBarFormatInfo>(value, DevExpress.Xpf.Core.ConditionalFormatting.Format.IconProperty, provider, (val, icon) => DataBarFormatInfo.AddIcon(val, (ImageSource) icon, this.Format.IconVerticalAlignment));

        public override FontFamily CoerceFontFamily(FontFamily value, FormatValueProvider provider) => 
            this.CoerceValue<FontFamily>(value, DevExpress.Xpf.Core.ConditionalFormatting.Format.FontFamilyProperty, provider, null);

        public override double CoerceFontSize(double value, FormatValueProvider provider) => 
            this.CoerceValue<double>(value, DevExpress.Xpf.Core.ConditionalFormatting.Format.FontSizeProperty, provider, null);

        public override FontStretch CoerceFontStretch(FontStretch value, FormatValueProvider provider) => 
            this.CoerceValue<FontStretch>(value, DevExpress.Xpf.Core.ConditionalFormatting.Format.FontStretchProperty, provider, null);

        public override FontStyle CoerceFontStyle(FontStyle value, FormatValueProvider provider) => 
            this.CoerceValue<FontStyle>(value, DevExpress.Xpf.Core.ConditionalFormatting.Format.FontStyleProperty, provider, null);

        public override FontWeight CoerceFontWeight(FontWeight value, FormatValueProvider provider) => 
            this.CoerceValue<FontWeight>(value, DevExpress.Xpf.Core.ConditionalFormatting.Format.FontWeightProperty, provider, null);

        public override Brush CoerceForeground(Brush value, FormatValueProvider provider) => 
            this.CoerceValue<Brush>(value, DevExpress.Xpf.Core.ConditionalFormatting.Format.ForegroundProperty, provider, null);

        public override TextDecorationCollection CoerceTextDecorations(TextDecorationCollection value, FormatValueProvider provider) => 
            this.CoerceValue<TextDecorationCollection>(value, DevExpress.Xpf.Core.ConditionalFormatting.Format.TextDecorationsProperty, provider, null);

        private TValue CoerceValue<TValue>(TValue value, DependencyProperty property, FormatValueProvider provider, Func<TValue, object, TValue> valueCombiner = null)
        {
            if (this.Format.IsPropertyAssigned(property) && this.CalcCondition(provider))
            {
                object obj2 = this.Format.GetValue(property);
                value = (valueCombiner != null) ? valueCombiner(value, obj2) : ((TValue) obj2);
            }
            return value;
        }

        public override IConditionalAnimationFactory CreateAnimationFactory()
        {
            ConditionalAnimationFactory factory1 = new ConditionalAnimationFactory();
            factory1.Condition = this;
            return factory1;
        }

        [IteratorStateMachine(typeof(<GetSummaries>d__17))]
        public override IEnumerable<ConditionalFormatSummaryType> GetSummaries() => 
            new <GetSummaries>d__17(-2);

        public override bool HasNonEmptyTextDecorations() => 
            (this.Format != null) && ((this.Format.TextDecorations != null) && (this.Format.TextDecorations.Count > 0));

        protected override bool NeedFormatChangeOverride(AnimationTriggerContext context)
        {
            DataUpdate dataUpdate = context.DataUpdate;
            return (!this.CalcCondition(dataUpdate.GetOldValue(base.ActualFieldName)) && this.CalcCondition(dataUpdate.GetNewValue(base.ActualFieldName)));
        }

        public override string OwnerPredefinedFormatsPropertyName =>
            "PredefinedFormats";

        protected DevExpress.Xpf.Core.ConditionalFormatting.Format Format =>
            base.FormatCore as DevExpress.Xpf.Core.ConditionalFormatting.Format;

        public override ConditionalFormatMask FormatMask =>
            this.Format.FormatMask;

        [CompilerGenerated]
        private sealed class <GetSummaries>d__17 : IEnumerable<ConditionalFormatSummaryType>, IEnumerable, IEnumerator<ConditionalFormatSummaryType>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private ConditionalFormatSummaryType <>2__current;
            private int <>l__initialThreadId;

            [DebuggerHidden]
            public <GetSummaries>d__17(int <>1__state)
            {
                this.<>1__state = <>1__state;
                this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
            }

            private bool MoveNext()
            {
                if (this.<>1__state == 0)
                {
                    this.<>1__state = -1;
                }
                return false;
            }

            [DebuggerHidden]
            IEnumerator<ConditionalFormatSummaryType> IEnumerable<ConditionalFormatSummaryType>.GetEnumerator()
            {
                ExpressionConditionBaseInfo.<GetSummaries>d__17 d__;
                if ((this.<>1__state != -2) || (this.<>l__initialThreadId != Environment.CurrentManagedThreadId))
                {
                    d__ = new ExpressionConditionBaseInfo.<GetSummaries>d__17(0);
                }
                else
                {
                    this.<>1__state = 0;
                    d__ = this;
                }
                return d__;
            }

            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator() => 
                this.System.Collections.Generic.IEnumerable<DevExpress.Xpf.Core.ConditionalFormatting.Native.ConditionalFormatSummaryType>.GetEnumerator();

            [DebuggerHidden]
            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            [DebuggerHidden]
            void IDisposable.Dispose()
            {
            }

            ConditionalFormatSummaryType IEnumerator<ConditionalFormatSummaryType>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }
    }
}

