namespace DevExpress.XtraExport.Xls
{
    using System;

    public class OlePropertyCompany : OlePropertyString
    {
        public OlePropertyCompany(int propertyType) : base(15, propertyType)
        {
        }

        public override void Execute(IDocumentPropertiesContainer propertiesContainer, OlePropertySetBase propertySet)
        {
            propertiesContainer.SetCompany(base.Value);
        }
    }
}

