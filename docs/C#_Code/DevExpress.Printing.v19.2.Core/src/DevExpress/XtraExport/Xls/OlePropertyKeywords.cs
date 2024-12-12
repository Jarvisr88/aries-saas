namespace DevExpress.XtraExport.Xls
{
    using System;

    public class OlePropertyKeywords : OlePropertyString
    {
        public OlePropertyKeywords(int propertyType) : base(5, propertyType)
        {
        }

        public override void Execute(IDocumentPropertiesContainer propertiesContainer, OlePropertySetBase propertySet)
        {
            propertiesContainer.SetKeywords(base.Value);
        }
    }
}

