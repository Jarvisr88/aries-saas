namespace DevExpress.XtraPrinting.Native
{
    using System;

    public class SmartConvertHelper : ConvertHelper
    {
        private ConvertHelper.SimpleConverter converter;

        protected override ConvertHelper.SimpleConverter Converter { get; set; }
    }
}

