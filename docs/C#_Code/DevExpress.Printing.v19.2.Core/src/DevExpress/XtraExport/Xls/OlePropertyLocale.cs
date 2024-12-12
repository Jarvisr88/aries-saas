namespace DevExpress.XtraExport.Xls
{
    using System;

    [CLSCompliant(false)]
    public class OlePropertyLocale : OlePropertyUInt32
    {
        public OlePropertyLocale() : base(-2147483648)
        {
        }

        public override void Execute(IDocumentPropertiesContainer propertiesContainer, OlePropertySetBase propertySet)
        {
        }
    }
}

