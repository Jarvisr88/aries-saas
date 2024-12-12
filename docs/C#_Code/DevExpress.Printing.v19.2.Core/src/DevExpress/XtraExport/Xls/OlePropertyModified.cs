namespace DevExpress.XtraExport.Xls
{
    using System;

    public class OlePropertyModified : OlePropertyFileTime
    {
        public OlePropertyModified() : base(13)
        {
        }

        public override void Execute(IDocumentPropertiesContainer propertiesContainer, OlePropertySetBase propertySet)
        {
            propertiesContainer.SetModified(base.Value);
        }
    }
}

