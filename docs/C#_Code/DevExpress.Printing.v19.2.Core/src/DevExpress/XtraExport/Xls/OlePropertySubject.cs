namespace DevExpress.XtraExport.Xls
{
    using System;

    public class OlePropertySubject : OlePropertyString
    {
        public OlePropertySubject(int propertyType) : base(3, propertyType)
        {
        }

        public override void Execute(IDocumentPropertiesContainer propertiesContainer, OlePropertySetBase propertySet)
        {
            propertiesContainer.SetSubject(base.Value);
        }
    }
}

