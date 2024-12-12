namespace DevExpress.XtraExport.Xls
{
    using System;
    using System.IO;

    public class OlePropertySetDocSummary : OlePropertySetBase
    {
        protected override OlePropertyBase CreateProperty(BinaryReader reader, int propertyId, int offset)
        {
            reader.BaseStream.Position = offset;
            int propertyType = reader.ReadInt32() & 0xffff;
            if (propertyId <= 14)
            {
                if (propertyId == 1)
                {
                    return new OlePropertyCodePage();
                }
                if (propertyId == 2)
                {
                    return new OlePropertyCategory(propertyType);
                }
                if (propertyId == 14)
                {
                    return new OlePropertyManager(propertyType);
                }
            }
            else if (propertyId <= 0x17)
            {
                if (propertyId == 15)
                {
                    return new OlePropertyCompany(propertyType);
                }
                if (propertyId == 0x17)
                {
                    return new OlePropertyVersion();
                }
            }
            else
            {
                if (propertyId == 0x1b)
                {
                    return new OlePropertyContentStatus(propertyType);
                }
                if (propertyId == 0x1d)
                {
                    return (base.IsStringPropertyType(propertyType) ? new OlePropertyDocumentVersion(propertyType) : null);
                }
            }
            return null;
        }

        public override Guid FormatId =>
            OlePropDefs.FMTID_DocSummaryInformation;
    }
}

