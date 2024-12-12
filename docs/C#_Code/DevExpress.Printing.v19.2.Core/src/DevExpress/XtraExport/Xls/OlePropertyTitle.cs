namespace DevExpress.XtraExport.Xls
{
    using System;

    public class OlePropertyTitle : OlePropertyString
    {
        public OlePropertyTitle(int propertyType) : base(2, propertyType)
        {
        }

        public override void Execute(IDocumentPropertiesContainer propertiesContainer, OlePropertySetBase propertySet)
        {
            propertiesContainer.SetTitle(base.Value);
        }
    }
}

