namespace DevExpress.XtraReports.Design
{
    using DevExpress.Utils.Design;
    using DevExpress.XtraPrinting.BarCode;
    using System;
    using System.ComponentModel;

    public class Code128LeadZeroConverter : BooleanTypeConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            Code128Generator instance = context.Instance as Code128Generator;
            return ((instance != null) && ((instance.CharacterSet == Code128Charset.CharsetC) && base.GetStandardValuesSupported(context)));
        }
    }
}

