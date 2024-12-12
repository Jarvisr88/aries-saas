namespace DevExpress.Xpf.Grid
{
    using DevExpress.Xpf.Core.ConditionalFormatting;
    using DevExpress.Xpf.Core.ConditionalFormatting.Native;
    using DevExpress.Xpf.GridData;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class UniqueDuplicateRuleFormatConditionInfo : ExpressionConditionBaseInfo
    {
        private readonly IColumnInfo selectiveColumnInfo;

        public UniqueDuplicateRuleFormatConditionInfo()
        {
            this.selectiveColumnInfo = new ConditionalFormattingColumnInfo(() => this.SelectiveExpression);
        }

        public override bool CalcCondition(FormatValueProvider provider)
        {
            if ((this.selectiveColumnInfo.UnboundExpression != null) && !provider.GetSelectiveValue(this.selectiveColumnInfo.FieldName))
            {
                return false;
            }
            ICollection<object> totalSummaryValue = provider.GetTotalSummaryValue(this.GetSummaryType()) as ICollection<object>;
            return ((totalSummaryValue != null) ? totalSummaryValue.Contains(provider.Value) : false);
        }

        [IteratorStateMachine(typeof(<GetSummaries>d__11))]
        public override IEnumerable<ConditionalFormatSummaryType> GetSummaries()
        {
            <GetSummaries>d__11 d__1 = new <GetSummaries>d__11(-2);
            d__1.<>4__this = this;
            return d__1;
        }

        private ConditionalFormatSummaryType GetSummaryType()
        {
            UniqueDuplicateRule rule = this.Rule;
            if (rule == UniqueDuplicateRule.Unique)
            {
                return ConditionalFormatSummaryType.Unique;
            }
            if (rule != UniqueDuplicateRule.Duplicate)
            {
                throw new InvalidOperationException();
            }
            return ConditionalFormatSummaryType.Duplicate;
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

        public UniqueDuplicateRule Rule { get; set; }

        public string SelectiveExpression { get; set; }

        [CompilerGenerated]
        private sealed class <GetSummaries>d__11 : IEnumerable<ConditionalFormatSummaryType>, IEnumerable, IEnumerator<ConditionalFormatSummaryType>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private ConditionalFormatSummaryType <>2__current;
            private int <>l__initialThreadId;
            public UniqueDuplicateRuleFormatConditionInfo <>4__this;

            [DebuggerHidden]
            public <GetSummaries>d__11(int <>1__state)
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
                    this.<>2__current = this.<>4__this.GetSummaryType();
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
                UniqueDuplicateRuleFormatConditionInfo.<GetSummaries>d__11 d__;
                if ((this.<>1__state == -2) && (this.<>l__initialThreadId == Environment.CurrentManagedThreadId))
                {
                    this.<>1__state = 0;
                    d__ = this;
                }
                else
                {
                    d__ = new UniqueDuplicateRuleFormatConditionInfo.<GetSummaries>d__11(0) {
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

