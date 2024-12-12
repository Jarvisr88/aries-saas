namespace DevExpress.XtraExport.Xls
{
    using System;

    public class OlePropertyCreated : OlePropertyFileTime
    {
        public OlePropertyCreated() : base(12)
        {
        }

        public override void Execute(IDocumentPropertiesContainer propertiesContainer, OlePropertySetBase propertySet)
        {
            propertiesContainer.SetCreated(base.Value);
        }
    }
}

