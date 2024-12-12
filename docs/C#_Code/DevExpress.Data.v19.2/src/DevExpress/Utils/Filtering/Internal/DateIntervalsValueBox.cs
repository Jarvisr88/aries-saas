namespace DevExpress.Utils.Filtering.Internal
{
    using DevExpress.Data.Filtering;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class DateIntervalsValueBox : RangeValueBox<DateTime>, IDatesTreeViewModel, IRangeValueViewModel<DateTime>, IRangeValueViewModel, IValueViewModel
    {
        private readonly EventHandlerList Events;
        private readonly EventHandler HashTreeCheckedChanged;
        private readonly EventHandler HashTreeRangeChanged;
        private IDateIntervalsHashTree hashTreeIndices;

        public DateIntervalsValueBox();
        protected sealed override bool AfterParseCore(IEndUserFilteringMetric metric, bool result);
        protected sealed override void BeforeParseCore(IEndUserFilteringMetric metric, CriteriaOperator criteria);
        private void InitializeHashTree();
        private void OnHashTreeCheckedChanged(object sender, EventArgs e);
        private void OnHashTreeRangeChanged(object sender, EventArgs e);
        protected override void OnInitialized();
        protected override void OnReleasing();
        protected override void OnUniqueValuesChanged();
        protected override void ResetCore();
        protected override void SetIsNull();

        [Browsable(false)]
        protected bool IsRangeSelector { get; }

        public IDateIntervalsHashTree HashTree { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DateIntervalsValueBox.<>c <>9;
            public static Action<IDateIntervalsHashTree> <>9__9_0;
            public static Action<IDateIntervalsHashTree> <>9__17_0;

            static <>c();
            internal void <BeforeParseCore>b__17_0(IDateIntervalsHashTree x);
            internal void <ResetCore>b__9_0(IDateIntervalsHashTree x);
        }
    }
}

