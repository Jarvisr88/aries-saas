namespace DevExpress.XtraExport.Xls
{
    using System;

    public class OlePropertyLastAuthor : OlePropertyString
    {
        public OlePropertyLastAuthor(int propertyType) : base(8, propertyType)
        {
        }

        public override void Execute(IDocumentPropertiesContainer propertiesContainer, OlePropertySetBase propertySet)
        {
            propertiesContainer.SetLastModifiedBy(base.Value);
        }
    }
}

