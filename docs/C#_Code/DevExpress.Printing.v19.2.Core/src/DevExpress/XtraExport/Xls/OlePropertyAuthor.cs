namespace DevExpress.XtraExport.Xls
{
    using System;

    public class OlePropertyAuthor : OlePropertyString
    {
        public OlePropertyAuthor(int propertyType) : base(4, propertyType)
        {
        }

        public override void Execute(IDocumentPropertiesContainer propertiesContainer, OlePropertySetBase propertySet)
        {
            propertiesContainer.SetAuthor(base.Value);
        }
    }
}

