namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;
    using System.IO;
    using System.Security.Cryptography;

    public class PdfEncryptHelper
    {
        private const string passwordPaddingString = "(\x00bfN^Nu\x008aAd\0NV\x00ff\x00fa\x0001\b..\0\x00b6\x00d0h>\x0080/\f\x00a9\x00fedSiz";

        private static string AlignPassword(string password)
        {
            password = string.IsNullOrEmpty(password) ? string.Empty : password;
            return ((password.Length <= 0x20) ? ((password.Length >= 0x20) ? password : (password + "(\x00bfN^Nu\x008aAd\0NV\x00ff\x00fa\x0001\b..\0\x00b6\x00d0h>\x0080/\f\x00a9\x00fedSiz".Substring(0, 0x20 - password.Length))) : password.Substring(0, 0x20));
        }

        private static unsafe void ARCFOUR(byte[] bytes, byte[] key)
        {
            byte num;
            int num2;
            byte[] buffer = new byte[0x100];
            byte[] buffer2 = new byte[0x100];
            for (num2 = 0; num2 < 0x100; num2++)
            {
                buffer[num2] = (byte) num2;
                buffer2[num2] = key[num2 % key.GetLength(0)];
            }
            int index = 0;
            for (num2 = 0; num2 < 0x100; num2++)
            {
                index = ((index + buffer[num2]) + buffer2[num2]) % 0x100;
                num = buffer[num2];
                buffer[num2] = buffer[index];
                buffer[index] = num;
            }
            num2 = index = 0;
            for (int i = 0; i < bytes.GetLength(0); i++)
            {
                num2 = (num2 + 1) % 0x100;
                index = (index + buffer[num2]) % 0x100;
                num = buffer[num2];
                buffer[num2] = buffer[index];
                buffer[index] = num;
                int num5 = (buffer[num2] + buffer[index]) % 0x100;
                byte* numPtr1 = &(bytes[i]);
                numPtr1[0] = (byte) (numPtr1[0] ^ buffer[num5]);
            }
        }

        private static int CalcKeyLength(int revision, int length) => 
            (revision == 2) ? 5 : (length / 8);

        public static byte[] ComputeEncryptionKey(string openPassword, string ownerPassword, int permissions, int revision, int length, bool encryptMetada, byte[] id)
        {
            openPassword = AlignPassword(openPassword);
            byte[] isoBytes = PdfStringUtils.GetIsoBytes(openPassword);
            MD5 md = new MD5CryptoServiceProvider();
            md.TransformBlock(isoBytes, 0, isoBytes.Length, isoBytes, 0);
            byte[] inputBuffer = PdfStringUtils.GetIsoBytes(ownerPassword);
            md.TransformBlock(inputBuffer, 0, inputBuffer.Length, inputBuffer, 0);
            byte[] bytes = BitConverter.GetBytes((uint) permissions);
            md.TransformBlock(bytes, 0, bytes.Length, bytes, 0);
            if ((id != null) && (id.Length != 0))
            {
                byte[] buffer5 = (byte[]) id.Clone();
                md.TransformBlock(buffer5, 0, buffer5.Length, buffer5, 0);
            }
            if ((revision >= 4) && !encryptMetada)
            {
                byte[] buffer6 = BitConverter.GetBytes(uint.MaxValue);
                md.TransformBlock(buffer6, 0, buffer6.Length, buffer6, 0);
            }
            md.TransformFinalBlock(new byte[0], 0, 0);
            int num = CalcKeyLength(revision, length);
            byte[] destinationArray = new byte[num];
            Array.Copy(md.Hash, destinationArray, num);
            if (revision >= 3)
            {
                md.Initialize();
                for (int i = 0; i < 50; i++)
                {
                    Array.Copy(md.ComputeHash(destinationArray), destinationArray, destinationArray.Length);
                    md.Initialize();
                }
            }
            return destinationArray;
        }

        public static string ComputeOwnerPassword(string openPassword, string permissionsPassword, int revision, int length)
        {
            permissionsPassword = string.IsNullOrEmpty(permissionsPassword) ? openPassword : permissionsPassword;
            permissionsPassword = AlignPassword(permissionsPassword);
            MD5 md = new MD5CryptoServiceProvider();
            byte[] buffer = md.ComputeHash(PdfStringUtils.GetIsoBytes(permissionsPassword));
            if (revision >= 3)
            {
                for (int i = 0; i < 50; i++)
                {
                    buffer = md.ComputeHash(buffer);
                }
            }
            byte[] destinationArray = new byte[CalcKeyLength(revision, length)];
            Array.Copy(buffer, destinationArray, destinationArray.Length);
            openPassword = AlignPassword(openPassword);
            byte[] isoBytes = PdfStringUtils.GetIsoBytes(openPassword);
            ARCFOUR(isoBytes, destinationArray);
            if (revision >= 3)
            {
                int num3 = 1;
                while (num3 <= 0x13)
                {
                    byte[] key = new byte[destinationArray.Length];
                    int index = 0;
                    while (true)
                    {
                        if (index >= destinationArray.Length)
                        {
                            ARCFOUR(isoBytes, key);
                            num3++;
                            break;
                        }
                        key[index] = (byte) (destinationArray[index] ^ num3);
                        index++;
                    }
                }
            }
            return PdfStringUtils.GetIsoString(isoBytes);
        }

        public static string ComputeUserPassword(byte[] key, int revision, byte[] id) => 
            (revision != 2) ? ComputeUserPasswordR3(key, id) : ComputeUserPasswordR2(key);

        private static string ComputeUserPasswordR2(byte[] key)
        {
            byte[] isoBytes = PdfStringUtils.GetIsoBytes("(\x00bfN^Nu\x008aAd\0NV\x00ff\x00fa\x0001\b..\0\x00b6\x00d0h>\x0080/\f\x00a9\x00fedSiz");
            ARCFOUR(isoBytes, key);
            return PdfStringUtils.GetIsoString(isoBytes);
        }

        private static string ComputeUserPasswordR3(byte[] key, byte[] id)
        {
            MD5 md = new MD5CryptoServiceProvider();
            byte[] isoBytes = PdfStringUtils.GetIsoBytes("(\x00bfN^Nu\x008aAd\0NV\x00ff\x00fa\x0001\b..\0\x00b6\x00d0h>\x0080/\f\x00a9\x00fedSiz");
            md.TransformBlock(isoBytes, 0, isoBytes.Length, isoBytes, 0);
            md.TransformFinalBlock(id, 0, id.Length);
            byte[] hash = md.Hash;
            ARCFOUR(hash, key);
            int num = 1;
            while (num <= 0x13)
            {
                byte[] buffer4 = new byte[key.Length];
                int index = 0;
                while (true)
                {
                    if (index >= key.Length)
                    {
                        ARCFOUR(hash, buffer4);
                        num++;
                        break;
                    }
                    buffer4[index] = (byte) (key[index] ^ num);
                    index++;
                }
            }
            byte[] destinationArray = new byte[0x20];
            Array.Copy(hash, destinationArray, hash.Length);
            return PdfStringUtils.GetIsoString(destinationArray);
        }

        public static byte[] EncryptData(byte[] data, byte[] key, int objectNumber, int generationNumber)
        {
            byte[] buffer;
            SymmetricAlgorithm algorithm = GetAesAlgorithm(key, objectNumber, generationNumber);
            using (MemoryStream stream = new MemoryStream())
            {
                using (CryptoStream stream2 = new CryptoStream(stream, algorithm.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    stream2.Write(algorithm.IV, 0, algorithm.IV.Length);
                    stream2.Write(data, 0, data.Length);
                    stream2.FlushFinalBlock();
                    buffer = stream.ToArray();
                }
            }
            return buffer;
        }

        public static MemoryStream EncryptStream(MemoryStream stream, byte[] key, int objectNumber, int generationNumber) => 
            new MemoryStream(EncryptData(stream.ToArray(), key, objectNumber, generationNumber));

        public static string EncryptString(string text, byte[] key, int objectNumber, int generationNumber) => 
            PdfStringUtils.GetIsoString(EncryptData(PdfStringUtils.GetIsoBytes(text), key, objectNumber, generationNumber));

        private static SymmetricAlgorithm GetAesAlgorithm(byte[] key, int objectNumber, int generationNumber)
        {
            byte[] array = new byte[key.Length + 9];
            key.CopyTo(array, 0);
            Array.Copy(BitConverter.GetBytes(objectNumber), 0, array, key.Length, 3);
            Array.Copy(BitConverter.GetBytes(generationNumber), 0, array, key.Length + 3, 2);
            Array.Copy(PdfStringUtils.GetIsoBytes("sAlT"), 0, array, key.Length + 5, 4);
            MD5 md = new MD5CryptoServiceProvider();
            md.ComputeHash(array);
            RijndaelManaged managed = new RijndaelManaged {
                BlockSize = 0x80,
                KeySize = 0x80,
                Padding = PaddingMode.PKCS7,
                Mode = CipherMode.CBC,
                Key = md.Hash
            };
            managed.GenerateIV();
            return managed;
        }

        public static byte[] GetISOBytes(string text)
        {
            if (text == null)
            {
                return null;
            }
            int length = text.Length;
            byte[] buffer = new byte[length];
            for (int i = 0; i < length; i++)
            {
                buffer[i] = (byte) text[i];
            }
            return buffer;
        }
    }
}

