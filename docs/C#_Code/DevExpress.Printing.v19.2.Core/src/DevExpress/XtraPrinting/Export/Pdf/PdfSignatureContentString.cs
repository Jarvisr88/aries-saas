namespace DevExpress.XtraPrinting.Export.Pdf
{
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.IO;
    using System.Security.Cryptography;
    using System.Security.Cryptography.Pkcs;
    using System.Security.Cryptography.X509Certificates;

    public class PdfSignatureContentString : PdfHexadecimalString
    {
        private long offset;
        private long length;
        private int placeholderLength;
        private X509Certificate2 certificate;
        private const int bufferLength = 0x8000;

        public PdfSignatureContentString(X509Certificate2 certificate) : base(null)
        {
            this.certificate = certificate;
            if (certificate != null)
            {
                using (HashAlgorithm algorithm = SHA1.Create())
                {
                    byte[] buffer1 = new byte[] { 1 };
                    byte[] buffer = this.SignData(algorithm.ComputeHash(buffer1));
                    this.placeholderLength = buffer.Length;
                    base.SetValue(buffer);
                }
            }
        }

        private byte[] CalculateHash(Stream stream, HashAlgorithm hashAlgorithm, long offset, long length)
        {
            byte[] buffer = new byte[0x8000];
            long num = offset;
            long num2 = offset + length;
            while (true)
            {
                int inputCount = stream.Read(buffer, 0, buffer.Length);
                long num4 = stream.Position - inputCount;
                long position = stream.Position;
                if ((num4 >= num2) || (num >= position))
                {
                    hashAlgorithm.TransformBlock(buffer, 0, inputCount, null, 0);
                }
                else
                {
                    if (num4 < num)
                    {
                        hashAlgorithm.TransformBlock(buffer, 0, (int) (num - num4), null, 0);
                    }
                    if (num2 < position)
                    {
                        hashAlgorithm.TransformBlock(buffer, (int) (num2 - num4), inputCount - ((int) (num2 - num4)), null, 0);
                    }
                }
                if (position == stream.Length)
                {
                    hashAlgorithm.TransformFinalBlock(buffer, 0, 0);
                    return hashAlgorithm.Hash;
                }
            }
        }

        public void Patch(StreamWriter writer)
        {
            writer.Flush();
            writer.BaseStream.Position = 0L;
            using (HashAlgorithm algorithm = SHA1.Create())
            {
                byte[] buffer = this.SignData(this.CalculateHash(writer.BaseStream, algorithm, this.offset, this.length));
                if (buffer.Length != this.placeholderLength)
                {
                    throw new Exception("PDF signature length");
                }
                base.SetValue(buffer);
            }
            writer.BaseStream.Position = this.offset;
            base.WriteContent(writer);
            writer.Flush();
        }

        private byte[] SignData(byte[] hash)
        {
            SignedCms cms = new SignedCms(new ContentInfo(hash));
            CmsSigner signer = new CmsSigner(this.certificate);
            cms.ComputeSignature(signer, !Environment.UserInteractive || PSNativeMethods.AspIsRunning);
            return cms.Encode();
        }

        protected override void WriteContent(StreamWriter writer)
        {
            writer.Flush();
            this.offset = writer.BaseStream.Position;
            base.WriteContent(writer);
            writer.Flush();
            this.length = writer.BaseStream.Position - this.offset;
        }

        public long Offset =>
            this.offset;

        public long Length =>
            this.length;
    }
}

