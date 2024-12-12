namespace DevExpress.Office.Crypto
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Security.Cryptography;

    internal static class Hmac
    {
        private static readonly byte[] hmacSaltBlockKey = new byte[] { 0x5f, 0xb2, 0xad, 1, 12, 0xb9, 0xe1, 0xf6 };
        private static readonly byte[] hmacValueBlockKey = new byte[] { 160, 0x67, 0x7f, 2, 0xb2, 0x2c, 0x84, 0x33 };

        public static bool CheckStream(ICipherProvider cipher, HashInfo hashInfo, HmacData hmacInfo, Stream stream)
        {
            byte[] buffer;
            byte[] buffer2;
            using (ICryptoTransform transform = cipher.GetDecryptor(hmacSaltBlockKey))
            {
                buffer = transform.TransformWithChecks(hmacInfo.EncryptedKey);
            }
            using (ICryptoTransform transform2 = cipher.GetDecryptor(hmacValueBlockKey))
            {
                buffer2 = transform2.TransformWithChecks(hmacInfo.EncryptedValue);
            }
            Func<byte, bool> predicate = <>c.<>9__3_0;
            if (<>c.<>9__3_0 == null)
            {
                Func<byte, bool> local1 = <>c.<>9__3_0;
                predicate = <>c.<>9__3_0 = b => b == 0;
            }
            if (buffer.All<byte>(predicate))
            {
                Func<byte, bool> func2 = <>c.<>9__3_1;
                if (<>c.<>9__3_1 == null)
                {
                    Func<byte, bool> local2 = <>c.<>9__3_1;
                    func2 = <>c.<>9__3_1 = b => b == 0;
                }
                if (buffer2.All<byte>(func2))
                {
                    return true;
                }
            }
            byte[] dst = new byte[hashInfo.HashBits / 8];
            Buffer.BlockCopy(buffer, 0, dst, 0, dst.Length);
            for (int i = dst.Length; i < buffer.Length; i++)
            {
                if (buffer[i] != 0)
                {
                    return false;
                }
            }
            HMAC hmac = CreateHMAC(hashInfo.Name);
            hmac.HashName = hashInfo.Name;
            hmac.Key = dst;
            hmac.Initialize();
            byte[] buffer4 = new byte[hashInfo.HashBits / 8];
            Buffer.BlockCopy(buffer2, 0, buffer4, 0, buffer4.Length);
            for (int j = buffer2.Length; j < buffer2.Length; j++)
            {
                if (buffer2[j] != 0)
                {
                    return false;
                }
            }
            return hmac.ComputeHash(stream).EqualBytes(buffer4);
        }

        private static HMAC CreateHMAC(string name) => 
            (name == "SHA256") ? ((HMAC) new HMACSHA256()) : ((name == "SHA384") ? ((HMAC) new HMACSHA384()) : ((name == "SHA512") ? ((HMAC) new HMACSHA512()) : ((name == "SHA1") ? ((HMAC) new HMACSHA1()) : HMAC.Create(name))));

        public static HmacData GetHmac(ICipherProvider cipher, HashInfo hashInfo, Stream stream)
        {
            byte[] randomBytes = DevExpress.Office.Crypto.Utils.GetRandomBytes(hashInfo.HashBits / 8);
            randomBytes = randomBytes.CloneToFit(DevExpress.Office.Crypto.Utils.RoundUp(randomBytes.Length, cipher.BlockBytes), 0);
            HMAC hmac = CreateHMAC(hashInfo.Name);
            hmac.HashName = hashInfo.Name;
            hmac.Key = randomBytes;
            hmac.Initialize();
            stream.Position = 0L;
            byte[] input = hmac.ComputeHash(stream);
            input = input.CloneToFit(DevExpress.Office.Crypto.Utils.RoundUp(input.Length, cipher.BlockBytes), 0);
            using (ICryptoTransform transform = cipher.GetEncryptor(hmacSaltBlockKey))
            {
                transform.TransformInPlace(randomBytes, 0, randomBytes.Length);
            }
            using (ICryptoTransform transform2 = cipher.GetEncryptor(hmacValueBlockKey))
            {
                transform2.TransformInPlace(input, 0, input.Length);
            }
            return new HmacData { 
                EncryptedKey = randomBytes,
                EncryptedValue = input
            };
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly Hmac.<>c <>9 = new Hmac.<>c();
            public static Func<byte, bool> <>9__3_0;
            public static Func<byte, bool> <>9__3_1;

            internal bool <CheckStream>b__3_0(byte b) => 
                b == 0;

            internal bool <CheckStream>b__3_1(byte b) => 
                b == 0;
        }
    }
}

