namespace DevExpress.XtraExport.Xls
{
    using System;

    public class OlePropertyApplication : OlePropertyString
    {
        public OlePropertyApplication(int propertyType) : base(0x12, propertyType)
        {
        }

        public override void Execute(IDocumentPropertiesContainer propertiesContainer, OlePropertySetBase propertySet)
        {
            propertiesContainer.SetApplication(base.Value);
        }
    }
}

