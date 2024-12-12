namespace DevExpress.XtraExport.Xls
{
    using System;

    public class OlePropertyLastPrinted : OlePropertyFileTime
    {
        public OlePropertyLastPrinted() : base(11)
        {
        }

        public override void Execute(IDocumentPropertiesContainer propertiesContainer, OlePropertySetBase propertySet)
        {
            propertiesContainer.SetLastPrinted(base.Value);
        }
    }
}

