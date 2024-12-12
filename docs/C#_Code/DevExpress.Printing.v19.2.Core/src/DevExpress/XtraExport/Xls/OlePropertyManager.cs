namespace DevExpress.XtraExport.Xls
{
    using System;

    public class OlePropertyManager : OlePropertyString
    {
        public OlePropertyManager(int propertyType) : base(14, propertyType)
        {
        }

        public override void Execute(IDocumentPropertiesContainer propertiesContainer, OlePropertySetBase propertySet)
        {
            propertiesContainer.SetManager(base.Value);
        }
    }
}

