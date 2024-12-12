namespace DevExpress.XtraExport.Xls
{
    using System;

    public class OlePropertyDocumentRevision : OlePropertyString
    {
        public OlePropertyDocumentRevision(int propertyType) : base(9, propertyType)
        {
        }

        public override void Execute(IDocumentPropertiesContainer propertiesContainer, OlePropertySetBase propertySet)
        {
            propertiesContainer.SetDocumentRevision(base.Value);
        }
    }
}

