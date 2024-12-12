namespace DevExpress.XtraExport.Xls
{
    using System;

    public class OlePropertyDocSecurity : OlePropertyInt32
    {
        public OlePropertyDocSecurity() : base(0x13)
        {
        }

        public override void Execute(IDocumentPropertiesContainer propertiesContainer, OlePropertySetBase propertySet)
        {
            propertiesContainer.SetSecurity(base.Value);
        }
    }
}

