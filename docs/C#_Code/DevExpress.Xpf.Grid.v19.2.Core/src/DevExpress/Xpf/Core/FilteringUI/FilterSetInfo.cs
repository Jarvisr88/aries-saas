namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public sealed class FilterSetInfo
    {
        private readonly Action updateCommandsStatus;
        internal readonly Dictionary<string, Lazy<CriteriaOperator>> Filters = new Dictionary<string, Lazy<CriteriaOperator>>();
        internal readonly HashSet<FilterModelBase> Models = new HashSet<FilterModelBase>();

        internal FilterSetInfo(Action updateCommandsStatus)
        {
            this.updateCommandsStatus = updateCommandsStatus;
        }

        internal void ApplyFilter(string propertyName, Lazy<CriteriaOperator> value)
        {
            this.Filters[propertyName] = value;
            this.IsFilterCleared = false;
            this.updateCommandsStatus();
        }

        internal void ClearFilter()
        {
            foreach (FilterModelBase base2 in this.Models)
            {
                base2.Update(null);
                Func<CriteriaOperator> valueFactory = <>c.<>9__12_0;
                if (<>c.<>9__12_0 == null)
                {
                    Func<CriteriaOperator> local1 = <>c.<>9__12_0;
                    valueFactory = <>c.<>9__12_0 = (Func<CriteriaOperator>) (() => null);
                }
                this.ApplyFilter(base2.PropertyName, new Lazy<CriteriaOperator>(valueFactory));
            }
            this.IsFilterCleared = true;
            this.updateCommandsStatus();
        }

        internal void FilterUpdated()
        {
            this.IsFilterCleared = false;
            this.Reset();
        }

        internal void RegisterModel(FilterModelBase model)
        {
            this.Models.Add(model);
            this.updateCommandsStatus();
        }

        internal void Reset()
        {
            this.Filters.Clear();
            this.updateCommandsStatus();
        }

        internal bool IsFilterCleared { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly FilterSetInfo.<>c <>9 = new FilterSetInfo.<>c();
            public static Func<CriteriaOperator> <>9__12_0;

            internal CriteriaOperator <ClearFilter>b__12_0() => 
                null;
        }
    }
}

