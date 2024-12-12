namespace DevExpress.XtraExport.Xls
{
    using System;

    public class OlePropertyContentStatus : OlePropertyString
    {
        public OlePropertyContentStatus(int propertyType) : base(0x1b, propertyType)
        {
        }

        public override void Execute(IDocumentPropertiesContainer propertiesContainer, OlePropertySetBase propertySet)
        {
            propertiesContainer.SetContentStatus(base.Value);
        }
    }
}

