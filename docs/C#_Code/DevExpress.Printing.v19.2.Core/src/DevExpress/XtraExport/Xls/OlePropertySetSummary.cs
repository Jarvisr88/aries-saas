namespace DevExpress.XtraExport.Xls
{
    using System;
    using System.IO;

    public class OlePropertySetSummary : OlePropertySetBase
    {
        protected override OlePropertyBase CreateProperty(BinaryReader reader, int propertyId, int offset)
        {
            reader.BaseStream.Position = offset;
            int propertyType = reader.ReadInt32() & 0xffff;
            switch (propertyId)
            {
                case 1:
                    return new OlePropertyCodePage();

                case 2:
                    return new OlePropertyTitle(propertyType);

                case 3:
                    return new OlePropertySubject(propertyType);

                case 4:
                    return new OlePropertyAuthor(propertyType);

                case 5:
                    return new OlePropertyKeywords(propertyType);

                case 6:
                    return new OlePropertyComments(propertyType);

                case 8:
                    return new OlePropertyLastAuthor(propertyType);

                case 9:
                    return (base.IsStringPropertyType(propertyType) ? new OlePropertyDocumentRevision(propertyType) : null);

                case 11:
                    return new OlePropertyLastPrinted();

                case 12:
                    return new OlePropertyCreated();

                case 13:
                    return new OlePropertyModified();

                case 0x12:
                    return new OlePropertyApplication(propertyType);

                case 0x13:
                    return new OlePropertyDocSecurity();
            }
            return null;
        }

        public override Guid FormatId =>
            OlePropDefs.FMTID_SummaryInformation;
    }
}

