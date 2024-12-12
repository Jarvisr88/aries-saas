namespace DevExpress.Xpf.Core.ConditionalFormatting.Native
{
    using DevExpress.Xpf.Core.ConditionalFormatting;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public class TopBottomRuleFormatConditionInfo : ExpressionConditionBaseInfo
    {
        public override bool CalcCondition(FormatValueProvider provider)
        {
            if (provider.Value == null)
            {
                return false;
            }
            object totalSummaryValue = provider.GetTotalSummaryValue(this.SummaryType);
            if (totalSummaryValue == null)
            {
                return false;
            }
            if ((this.Rule == TopBottomRule.AboveAverage) || (this.Rule == TopBottomRule.BelowAverage))
            {
                decimal? decimalValue = IndicatorFormatBase.GetDecimalValue(provider.Value);
                decimal? y = IndicatorFormatBase.GetDecimalValue(totalSummaryValue);
                return ((decimalValue != null) && ((y != null) && ((provider.ValueComparer != null) && ((provider.ValueComparer.Compare(decimalValue, y) * this.GetComparisonSign()) > 0))));
            }
            SortedIndices indices = totalSummaryValue as SortedIndices;
            if (indices == null)
            {
                return false;
            }
            int num = GetCount(this.Rule, this.Threshold, indices.Count);
            return indices.IsTopBottomItem(provider, Math.Max(0, num), this.GetUseTopItems());
        }

        private int GetComparisonSign()
        {
            TopBottomRule rule = this.Rule;
            if (rule == TopBottomRule.AboveAverage)
            {
                return 1;
            }
            if (rule != TopBottomRule.BelowAverage)
            {
                throw new InvalidOperationException();
            }
            return -1;
        }

        public static int GetCount(TopBottomRule rule, double threshold, int totalCount) => 
            GetUseItemCount(rule) ? ((int) Math.Min(2147483647.0, threshold)) : ((int) Math.Max(1.0, Math.Floor((double) (totalCount * (threshold / 100.0)))));

        [IteratorStateMachine(typeof(<GetSummaries>d__10))]
        public override IEnumerable<ConditionalFormatSummaryType> GetSummaries()
        {
            <GetSummaries>d__10 d__1 = new <GetSummaries>d__10(-2);
            d__1.<>4__this = this;
            return d__1;
        }

        private static bool GetUseItemCount(TopBottomRule rule)
        {
            switch (rule)
            {
                case TopBottomRule.TopItems:
                case TopBottomRule.BottomItems:
                    return true;

                case TopBottomRule.TopPercent:
                case TopBottomRule.BottomPercent:
                    return false;
            }
            throw new InvalidOperationException();
        }

        private bool GetUseTopItems()
        {
            switch (this.Rule)
            {
                case TopBottomRule.TopItems:
                case TopBottomRule.TopPercent:
                    return true;

                case TopBottomRule.BottomItems:
                case TopBottomRule.BottomPercent:
                    return false;
            }
            throw new InvalidOperationException();
        }

        public static TopBottomRule ToTopBottomRule(ConditionFilterType type)
        {
            switch (type)
            {
                case ConditionFilterType.TopItems:
                    return TopBottomRule.TopItems;

                case ConditionFilterType.BottomItems:
                    return TopBottomRule.BottomItems;

                case ConditionFilterType.TopPercent:
                    return TopBottomRule.TopPercent;

                case ConditionFilterType.BottomPercent:
                    return TopBottomRule.BottomPercent;
            }
            throw new InvalidOperationException();
        }

        public TopBottomRule Rule { get; set; }

        public double Threshold { get; set; }

        private ConditionalFormatSummaryType SummaryType
        {
            get
            {
                switch (this.Rule)
                {
                    case TopBottomRule.TopItems:
                    case TopBottomRule.TopPercent:
                    case TopBottomRule.BottomItems:
                    case TopBottomRule.BottomPercent:
                        return ConditionalFormatSummaryType.SortedList;

                    case TopBottomRule.AboveAverage:
                    case TopBottomRule.BelowAverage:
                        return ConditionalFormatSummaryType.Average;
                }
                throw new NotImplementedException();
            }
        }

        [CompilerGenerated]
        private sealed class <GetSummaries>d__10 : IEnumerable<ConditionalFormatSummaryType>, IEnumerable, IEnumerator<ConditionalFormatSummaryType>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private ConditionalFormatSummaryType <>2__current;
            private int <>l__initialThreadId;
            public TopBottomRuleFormatConditionInfo <>4__this;

            [DebuggerHidden]
            public <GetSummaries>d__10(int <>1__state)
            {
                this.<>1__state = <>1__state;
                this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
            }

            private bool MoveNext()
            {
                int num = this.<>1__state;
                if (num == 0)
                {
                    this.<>1__state = -1;
                    this.<>2__current = this.<>4__this.SummaryType;
                    this.<>1__state = 1;
                    return true;
                }
                if (num == 1)
                {
                    this.<>1__state = -1;
                }
                return false;
            }

            [DebuggerHidden]
            IEnumerator<ConditionalFormatSummaryType> IEnumerable<ConditionalFormatSummaryType>.GetEnumerator()
            {
                TopBottomRuleFormatConditionInfo.<GetSummaries>d__10 d__;
                if ((this.<>1__state == -2) && (this.<>l__initialThreadId == Environment.CurrentManagedThreadId))
                {
                    this.<>1__state = 0;
                    d__ = this;
                }
                else
                {
                    d__ = new TopBottomRuleFormatConditionInfo.<GetSummaries>d__10(0) {
                        <>4__this = this.<>4__this
                    };
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

