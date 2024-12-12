namespace DevExpress.XtraExport.Xls
{
    using System;

    public class OlePropertyComments : OlePropertyString
    {
        public OlePropertyComments(int propertyType) : base(6, propertyType)
        {
        }

        public override void Execute(IDocumentPropertiesContainer propertiesContainer, OlePropertySetBase propertySet)
        {
            propertiesContainer.SetDescription(base.Value);
        }
    }
}

