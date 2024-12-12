namespace DevExpress.Office.Crypto
{
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Security.Cryptography;
    using System.Xml;

    public static class Utils
    {
        internal static void CheckNode(this XmlReader reader, string namespaceUri, string localName)
        {
            if (reader.LocalName != localName)
            {
                throw new InvalidDataException();
            }
            if (reader.NamespaceURI != namespaceUri)
            {
                throw new InvalidDataException();
            }
        }

        public static byte[] CloneToFit(this byte[] input, int size) => 
            input.CloneToFit(size, 0x36);

        public static byte[] CloneToFit(this byte[] input, int size, byte pad)
        {
            byte[] dst = new byte[size];
            Buffer.BlockCopy(input, 0, dst, 0, Math.Min(input.Length, dst.Length));
            for (int i = input.Length; i < dst.Length; i++)
            {
                dst[i] = pad;
            }
            return dst;
        }

        public static bool EqualBytes(this byte[] arr1, byte[] arr2)
        {
            uint num = (uint) (arr1.Length ^ arr2.Length);
            for (int i = 0; (i < arr1.Length) && (i < arr2.Length); i++)
            {
                num |= (uint) (arr1[i] ^ arr2[i]);
            }
            return (num == 0);
        }

        public static ICryptoTransform GetCryptoTransform(this ICipherProvider cipher, int key1, int key2, bool isEncryption)
        {
            if (key2 == 0)
            {
                return cipher.GetCryptoTransform(BitConverter.GetBytes(key1), isEncryption);
            }
            byte[] dst = new byte[8];
            Buffer.BlockCopy(BitConverter.GetBytes(key2), 0, dst, 0, 4);
            Buffer.BlockCopy(BitConverter.GetBytes(key1), 0, dst, 4, 4);
            return cipher.GetCryptoTransform(dst, isEncryption);
        }

        public static ICryptoTransform GetDecryptor(this ICipherProvider cipher, byte[] blockKey) => 
            cipher.GetCryptoTransform(blockKey, false);

        public static ICryptoTransform GetDecryptor(this ICipherProvider cipher, int key1, int key2) => 
            cipher.GetCryptoTransform(key1, key2, false);

        public static ICryptoTransform GetEncryptor(this ICipherProvider cipher, byte[] blockKey) => 
            cipher.GetCryptoTransform(blockKey, true);

        public static ICryptoTransform GetEncryptor(this ICipherProvider cipher, int key1, int key2) => 
            cipher.GetCryptoTransform(key1, key2, true);

        public static byte[] GetRandomBytes(int length)
        {
            byte[] data = new byte[length];
            using (RandomNumberGenerator generator = RandomNumberGenerator.Create())
            {
                generator.GetBytes(data);
            }
            return data;
        }

        internal static bool IsXmlnsAttribute(this XmlReader reader) => 
            (reader.NodeType == XmlNodeType.Attribute) ? ((reader.Prefix != "xmlns") ? ((reader.Prefix.Length == 0) && (reader.LocalName == "xmlns")) : true) : false;

        internal static void MoveToEndElement(this XmlReader reader)
        {
            if (!reader.IsEmptyElement)
            {
                reader.MoveToNextContent();
                if (reader.NodeType != XmlNodeType.EndElement)
                {
                    throw new InvalidDataException();
                }
            }
        }

        internal static void MoveToNextContent(this XmlReader reader)
        {
            if (!reader.Read())
            {
                throw new InvalidDataException();
            }
            reader.MoveToContent();
        }

        internal static void MoveToStartElement(this XmlReader reader)
        {
            reader.MoveToNextContent();
            if (reader.NodeType != XmlNodeType.Element)
            {
                throw new InvalidDataException();
            }
        }

        internal static void MoveToStartElement(this XmlReader reader, string namespaceUri, string localName)
        {
            reader.MoveToStartElement();
            reader.CheckNode(namespaceUri, localName);
        }

        internal static void ParseAttributes(this XmlReader reader, int attributeMax, Func<NameToken, string, bool> action)
        {
            int num = 0;
            if (reader.MoveToFirstAttribute())
            {
                do
                {
                    if (!reader.IsXmlnsAttribute())
                    {
                        NameToken token;
                        if (!Enum.TryParse<NameToken>(reader.LocalName, out token))
                        {
                            throw new InvalidDataException();
                        }
                        if (reader.NamespaceURI.Length != 0)
                        {
                            throw new InvalidDataException();
                        }
                        if (!action(token, reader.Value))
                        {
                            throw new InvalidDataException();
                        }
                        num++;
                    }
                }
                while (reader.MoveToNextAttribute());
            }
            if (num != attributeMax)
            {
                throw new InvalidDataException();
            }
            reader.MoveToElement();
        }

        public static int RoundUp(int value, int round) => 
            (round != 0) ? ((((value + round) - 1) / round) * round) : round;

        public static void TransformInPlace(this ICryptoTransform transform, byte[] buffer, int offset, int count)
        {
            if (count != 0)
            {
                int num = transform.TransformBlock(buffer, offset, count, buffer, offset);
                if (count != num)
                {
                    throw new InvalidDataException();
                }
            }
        }

        public static byte[] TransformWithChecks(this ICryptoTransform transform, byte[] inputBuffer)
        {
            byte[] buffer = transform.TransformFinalBlock(inputBuffer, 0, inputBuffer.Length);
            if (inputBuffer.Length != buffer.Length)
            {
                throw new InvalidDataException();
            }
            return buffer;
        }
    }
}

