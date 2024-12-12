namespace DevExpress.XtraExport.Xls
{
    using System;

    public class OlePropertyVersion : OlePropertyInt32
    {
        public OlePropertyVersion() : base(0x17)
        {
        }

        public override void Execute(IDocumentPropertiesContainer propertiesContainer, OlePropertySetBase propertySet)
        {
            ushort num = (ushort) (base.Value & 0xffff);
            ushort num2 = (ushort) ((base.Value & -65536) >> 0x10);
            propertiesContainer.SetVersion($"{num2}.{num:D4}");
        }
    }
}

