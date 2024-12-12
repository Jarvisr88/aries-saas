namespace DevExpress.Export.Binary
{
    using DevExpress.Office.Utils;
    using System;

    public static class BinaryHyperlinkMonikerFactory
    {
        public static readonly Guid CLSID_URLMoniker = new Guid("{0x79EAC9E0, 0xBAF9, 0x11CE, {0x8C, 0x82, 0x00, 0xAA, 0x00, 0x4B, 0xA9, 0x0B}}");
        public static readonly Guid CLSID_FileMoniker = new Guid("{0x00000303, 0x0000, 0x0000, {0xC0, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x46}}");
        public static readonly Guid CLSID_CompositeMoniker = new Guid("{0x00000309, 0x0000, 0x0000, {0xC0, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x46}}");
        public static readonly Guid CLSID_AntiMoniker = new Guid("{0x00000305, 0x0000, 0x0000, {0xC0, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x46}}");
        public static readonly Guid CLSID_ItemMoniker = new Guid("{0x00000304, 0x0000, 0x0000, {0xC0, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x46}}");

        public static BinaryHyperlinkMonikerBase Create(XlReader reader)
        {
            Guid guid = new Guid(reader.ReadBytes(0x10));
            if (guid == CLSID_URLMoniker)
            {
                return BinaryHyperlinkURLMoniker.FromStream(reader);
            }
            if (guid == CLSID_FileMoniker)
            {
                return BinaryHyperlinkFileMoniker.FromStream(reader);
            }
            if (guid == CLSID_CompositeMoniker)
            {
                return BinaryHyperlinkCompositeMoniker.FromStream(reader);
            }
            if (guid == CLSID_AntiMoniker)
            {
                return BinaryHyperlinkAntiMoniker.FromStream(reader);
            }
            if (guid != CLSID_ItemMoniker)
            {
                throw new Exception("Invalid binary file: Unknown hyperlink moniker CLSID");
            }
            return BinaryHyperlinkItemMoniker.FromStream(reader);
        }
    }
}

