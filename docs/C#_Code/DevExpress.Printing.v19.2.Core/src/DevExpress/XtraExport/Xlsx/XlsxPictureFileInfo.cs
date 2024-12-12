namespace DevExpress.XtraExport.Xlsx
{
    using System;
    using System.Runtime.CompilerServices;

    internal class XlsxPictureFileInfo
    {
        public XlsxPictureFileInfo(string fileName, byte[] fileDigest)
        {
            this.FileName = fileName;
            this.FileDigest = fileDigest;
        }

        public bool EqualsDigest(byte[] digest)
        {
            if (digest == null)
            {
                return false;
            }
            if (this.FileDigest.Length != digest.Length)
            {
                return false;
            }
            for (int i = 0; i < this.FileDigest.Length; i++)
            {
                if (this.FileDigest[i] != digest[i])
                {
                    return false;
                }
            }
            return true;
        }

        public string FileName { get; private set; }

        public byte[] FileDigest { get; private set; }
    }
}

