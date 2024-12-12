namespace DMEWorks.Data.Adapters.InsuranceCompany
{
    using DMEWorks.Data.TypeHandlers;
    using System;
    using System.Collections.Generic;

    public class RegionTypeHandler : EnumTypeHandler<Region>
    {
        private static readonly Region[] values = ((Region[]) Enum.GetValues(typeof(Region)));

        protected override string GetText(Region region)
        {
            switch (region)
            {
                case Region.AbilityRegionA:
                    return "Region A";

                case Region.AbilityRegionB:
                    return "Region B";

                case Region.AbilityRegionC:
                    return "Region C";

                case Region.AbilityRegionD:
                    return "Region D";

                case Region.AbilityPrivate:
                    return "Ability";

                case Region.Availity:
                    return "Availity";

                case Region.ClaimMD:
                    return "Claim.MD";

                case Region.MediCal:
                    return "Medi-Cal";

                case Region.OffiiceAlly:
                    return "Office Ally";

                case Region.Zirmed:
                    return "Zirmed";
            }
            throw new ArgumentOutOfRangeException();
        }

        protected override IEnumerable<Region> GetValues() => 
            values;
    }
}

