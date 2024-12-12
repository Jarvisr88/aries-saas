namespace DevExpress.Data.Mask
{
    using System;
    using System.Globalization;

    public class RegExpMaskManager : MaskManagerSelectAllEnhancer<RegExpMaskManagerCore>
    {
        public RegExpMaskManager(string regExp, bool reverseDfa, bool isAutoComplete, bool isOptimistic, bool showPlaceHolders, char anySymbolPlaceHolder, CultureInfo managerCultureInfo);
    }
}

