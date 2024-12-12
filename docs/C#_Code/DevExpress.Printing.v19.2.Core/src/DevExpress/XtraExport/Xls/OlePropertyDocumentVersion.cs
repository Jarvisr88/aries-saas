namespace DevExpress.XtraExport.Xls
{
    using System;

    public class OlePropertyDocumentVersion : OlePropertyString
    {
        public OlePropertyDocumentVersion(int propertyType) : base(0x1d, propertyType)
        {
        }

        public override void Execute(IDocumentPropertiesContainer propertiesContainer, OlePropertySetBase propertySet)
        {
            propertiesContainer.SetDocumentVersion(base.Value);
        }
    }
}

