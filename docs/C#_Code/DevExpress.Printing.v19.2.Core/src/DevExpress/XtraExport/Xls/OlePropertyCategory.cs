namespace DevExpress.XtraExport.Xls
{
    using System;

    public class OlePropertyCategory : OlePropertyString
    {
        public OlePropertyCategory(int propertyType) : base(2, propertyType)
        {
        }

        public override void Execute(IDocumentPropertiesContainer propertiesContainer, OlePropertySetBase propertySet)
        {
            propertiesContainer.SetCategory(base.Value);
        }
    }
}

