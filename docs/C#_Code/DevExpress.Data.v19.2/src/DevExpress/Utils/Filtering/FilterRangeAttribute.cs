namespace DevExpress.Utils.Filtering
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public sealed class FilterRangeAttribute : BaseFilterRangeAttribute
    {
        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public static readonly FilterRangeAttribute Implicit = new FilterRangeAttribute(0, 0);

        public FilterRangeAttribute() : this(null, null, null)
        {
        }

        public FilterRangeAttribute(object minOrMinMember, object maxOrMaxMember) : this(minOrMinMember, maxOrMaxMember, null)
        {
        }

        public FilterRangeAttribute(object minOrMinMember, object maxOrMaxMember, object avgOrAvgMember) : base(minOrMinMember, maxOrMaxMember)
        {
            if ((avgOrAvgMember is string) && !this.TryParse((string) avgOrAvgMember, out avgOrAvgMember))
            {
                this.AverageMember = (string) avgOrAvgMember;
            }
            else
            {
                this.Average = avgOrAvgMember;
            }
        }

        protected internal override string[] GetMembers() => 
            new string[] { base.MinimumMember, base.MaximumMember, this.AverageMember };

        public bool IsImplicit =>
            ReferenceEquals(this, Implicit);

        public RangeUIEditorType EditorType { get; set; }

        public object Average { get; private set; }

        public string AverageMember { get; set; }
    }
}

