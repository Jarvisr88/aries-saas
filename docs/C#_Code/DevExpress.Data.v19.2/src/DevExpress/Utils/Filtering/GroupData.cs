namespace DevExpress.Utils.Filtering
{
    using DevExpress.Data.Filtering;
    using DevExpress.Utils.Filtering.Internal;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class GroupData : MetricAttributesData
    {
        internal GroupData(IDictionary<string, object> memberValues) : base(memberValues)
        {
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public virtual object GetDataItemsLookup() => 
            base.GetValue<IDictionary<int, object>>("DataItemsLookup")[FNV1a.Create(this.GetParentValues())];

        internal CriteriaOperator GetParentCriteria() => 
            base.GetValue<CriteriaOperator>("#GroupParentCriteria");

        internal object[] GetParentValues() => 
            base.GetValue<object[]>("#GroupParentValues");

        internal string GetPath() => 
            base.GetValue<string>("#GroupPath");

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public override void SetDataItemsLookup(object lookup)
        {
            base.GetValue<IDictionary<int, object>>("DataItemsLookup")[FNV1a.Create(this.GetParentValues())] = lookup;
        }

        public void SetGroupData(object[] values, string[] displayValues)
        {
            int num = FNV1a.Create(this.GetParentValues());
            base.GetValue<IDictionary<int, object[]>>("GroupValues")[num] = values;
            base.GetValue<IDictionary<int, string[]>>("GroupTexts")[num] = displayValues;
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public void SetGroupTexts(string[] texts)
        {
            base.GetValue<IDictionary<int, string[]>>("GroupTexts")[FNV1a.Create(this.GetParentValues())] = texts;
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public void SetGroupValues(object[] values)
        {
            base.GetValue<IDictionary<int, object[]>>("GroupValues")[FNV1a.Create(this.GetParentValues())] = values;
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public void SetupGroupFilters(FilteringViewModelPropertyValuesProvider provider, string path, object[] values, string[] displayTexts)
        {
            provider.SetupDisplayRadio(this, path);
            base.SetValue<object[]>("UniqueValues", values);
            base.SetValue<string[]>("DisplayTexts", displayTexts);
        }

        internal bool HasParentValues
        {
            get
            {
                Predicate<object[]> nonDefault = <>c.<>9__5_0;
                if (<>c.<>9__5_0 == null)
                {
                    Predicate<object[]> local1 = <>c.<>9__5_0;
                    nonDefault = <>c.<>9__5_0 = x => x.Length != 0;
                }
                return this.HasValue<object[]>("#GroupParentValues", nonDefault);
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly GroupData.<>c <>9 = new GroupData.<>c();
            public static Predicate<object[]> <>9__5_0;

            internal bool <get_HasParentValues>b__5_0(object[] x) => 
                x.Length != 0;
        }
    }
}

