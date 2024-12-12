namespace DevExpress.Office.Crypto
{
    using DevExpress.Office.Utils;
    using DevExpress.XtraExport.Xls;
    using System;
    using System.IO;

    public class StandardEncryptionHeader : XlsRC4CryptoAPIEncryptionHeader
    {
        public void Read(BinaryReader reader)
        {
            XlReader reader2 = new XlReader(reader);
            reader2.Seek((long) (-4), SeekOrigin.Current);
            base.Read(reader2);
        }
    }
}

