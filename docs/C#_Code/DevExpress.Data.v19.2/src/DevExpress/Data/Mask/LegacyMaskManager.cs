namespace DevExpress.Data.Mask
{
    using System;

    public class LegacyMaskManager : MaskManagerSelectAllEnhancer<LegacyMaskManagerCore>
    {
        public LegacyMaskManager(LegacyMaskInfo info, char blank, bool saveLiteral, bool ignoreMaskBlank);
    }
}

