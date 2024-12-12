namespace DMEWorks.Data.Adapters.InsuranceCompany
{
    using DMEWorks.Data.TypeHandlers;
    using System;
    using System.Collections.Generic;

    public class BasisTypeHandler : EnumTypeHandler<Basis>
    {
        private static readonly Basis[] values = ((Basis[]) Enum.GetValues(typeof(Basis)));

        protected override string GetText(Basis region)
        {
            if (region == Basis.Billable)
            {
                return "Bill";
            }
            if (region != Basis.Allowable)
            {
                throw new ArgumentOutOfRangeException();
            }
            return "Allowed";
        }

        protected override IEnumerable<Basis> GetValues() => 
            values;
    }
}

