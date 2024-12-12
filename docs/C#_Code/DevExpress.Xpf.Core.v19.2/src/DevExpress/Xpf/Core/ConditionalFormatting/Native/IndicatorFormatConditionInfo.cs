namespace DevExpress.Xpf.Core.ConditionalFormatting.Native
{
    using DevExpress.Xpf.Core.ConditionalFormatting;
    using DevExpress.Xpf.GridData;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows.Media;

    public abstract class IndicatorFormatConditionInfo : FormatConditionBaseInfo
    {
        private readonly IColumnInfo selectiveColumnInfo;

        public IndicatorFormatConditionInfo()
        {
            this.selectiveColumnInfo = new ConditionalFormattingColumnInfo(() => this.SelectiveExpression);
        }

        protected abstract bool AllowTransition(object oldTransitionState, object newTransitionState);
        protected virtual object CalcFormatTransitionState(FormatValueProvider provider) => 
            this.CoerceDataBarFormatInfo(null, provider);

        private T Coerce<T>(T value, FormatValueProvider provider, Func<T, FormatValueProvider, decimal?, decimal?, T> coerceAction) => 
            ((this.selectiveColumnInfo.UnboundExpression == null) || provider.GetSelectiveValue(this.selectiveColumnInfo.FieldName)) ? coerceAction(value, provider, this.ActualMin, this.ActualMax) : value;

        public override Brush CoerceBackground(Brush value, FormatValueProvider provider)
        {
            IndicatorFormatBase indicatorFormat = this.IndicatorFormat;
            return this.Coerce<Brush>(value, provider, new Func<Brush, FormatValueProvider, decimal?, decimal?, Brush>(indicatorFormat.CoerceBackground));
        }

        public override DataBarFormatInfo CoerceDataBarFormatInfo(DataBarFormatInfo value, FormatValueProvider provider)
        {
            IndicatorFormatBase indicatorFormat = this.IndicatorFormat;
            return this.Coerce<DataBarFormatInfo>(value, provider, new Func<DataBarFormatInfo, FormatValueProvider, decimal?, decimal?, DataBarFormatInfo>(indicatorFormat.CoerceDataBarFormatInfo));
        }

        public static decimal? GetParsedDecimalValue(object value)
        {
            decimal num;
            decimal? decimalValue = IndicatorFormatBase.GetDecimalValue(value);
            if ((decimalValue == null) && ((value != null) && decimal.TryParse(value.ToString(), NumberStyles.Number, CultureInfo.InvariantCulture, out num)))
            {
                decimalValue = new decimal?(num);
            }
            return decimalValue;
        }

        [IteratorStateMachine(typeof(<GetSummaries>d__15))]
        public override IEnumerable<ConditionalFormatSummaryType> GetSummaries()
        {
            if (this.IndicatorFormat == null)
            {
                goto TR_0003;
            }
            else if (this.ActualMin == null)
            {
                yield return ConditionalFormatSummaryType.Min;
            }
            if (this.ActualMax == null)
            {
                yield return ConditionalFormatSummaryType.Max;
            }
        TR_0003:;
        }

        public override IEnumerable<IColumnInfo> GetUnboundColumnInfo()
        {
            IEnumerable<IColumnInfo> unboundColumnInfo = base.GetUnboundColumnInfo();
            if (this.selectiveColumnInfo.UnboundExpression != null)
            {
                IColumnInfo[] second = new IColumnInfo[] { this.selectiveColumnInfo };
                unboundColumnInfo = unboundColumnInfo.Concat<IColumnInfo>(second);
            }
            return unboundColumnInfo;
        }

        protected override bool NeedFormatChangeOverride(AnimationTriggerContext context)
        {
            if (base.FormatCore == null)
            {
                return false;
            }
            DataUpdate dataUpdate = context.DataUpdate;
            object oldTransitionState = this.CalcFormatTransitionState(dataUpdate.GetOldValue(base.ActualFieldName));
            if (oldTransitionState == null)
            {
                return false;
            }
            object newTransitionState = this.CalcFormatTransitionState(dataUpdate.GetNewValue(base.ActualFieldName));
            return ((newTransitionState != null) ? this.AllowTransition(oldTransitionState, newTransitionState) : false);
        }

        public void OnMinMaxChanged(object newValue, bool isMax)
        {
            decimal? parsedDecimalValue = GetParsedDecimalValue(newValue);
            if (isMax)
            {
                this.ActualMax = parsedDecimalValue;
            }
            else
            {
                this.ActualMin = parsedDecimalValue;
            }
        }

        public string SelectiveExpression { get; set; }

        protected IndicatorFormatBase IndicatorFormat =>
            base.FormatCore as IndicatorFormatBase;

        protected decimal? ActualMin { get; private set; }

        protected decimal? ActualMax { get; private set; }

    }
}

