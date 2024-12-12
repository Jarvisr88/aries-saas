namespace DevExpress.XtraExport.Xls
{
    using System;
    using System.IO;

    public class OlePropertySetUserDefined : OlePropertySetBase
    {
        protected override OlePropertyBase CreateProperty(BinaryReader reader, int propertyId, int offset)
        {
            reader.BaseStream.Position = offset;
            if ((propertyId < 2) || (propertyId > 0x7fffffff))
            {
                if (propertyId == 0)
                {
                    return new OlePropertyDictionary();
                }
                if (propertyId == 1)
                {
                    reader.ReadInt32();
                    return new OlePropertyCodePage();
                }
                if (propertyId == -2147483648)
                {
                    reader.ReadInt32();
                    return new OlePropertyLocale();
                }
            }
            else
            {
                int propertyType = reader.ReadInt32() & 0xffff;
                if (propertyType > 0x13)
                {
                    if ((propertyType == 30) || (propertyType == 0x1f))
                    {
                        return new OlePropertyString(propertyId, propertyType);
                    }
                    if (propertyType == 0x40)
                    {
                        return new OlePropertyFileTime(propertyId);
                    }
                }
                else
                {
                    switch (propertyType)
                    {
                        case 2:
                            return new OlePropertyInt16(propertyId);

                        case 3:
                            return new OlePropertyInt32(propertyId);

                        case 4:
                            return new OlePropertyFloat(propertyId);

                        case 5:
                            return new OlePropertyDouble(propertyId);

                        default:
                            if (propertyType == 11)
                            {
                                return new OlePropertyBool(propertyId);
                            }
                            switch (propertyType)
                            {
                                case 0x11:
                                    return new OlePropertyByte(propertyId);

                                case 0x12:
                                    return new OlePropertyUInt16(propertyId);

                                case 0x13:
                                    return new OlePropertyUInt32(propertyId);

                                default:
                                    break;
                            }
                            break;
                    }
                }
            }
            return null;
        }

        public override string GetPropertyName(int propertyId)
        {
            OlePropertyDictionary dictionary = base.FindById(0) as OlePropertyDictionary;
            return (((dictionary == null) || !dictionary.Entries.ContainsKey(propertyId)) ? string.Empty : dictionary.Entries[propertyId]);
        }

        public override Guid FormatId =>
            OlePropDefs.FMTID_UserDefinedProperties;
    }
}

