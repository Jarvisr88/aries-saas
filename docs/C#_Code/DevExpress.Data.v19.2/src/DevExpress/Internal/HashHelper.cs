namespace DevExpress.Internal
{
    using System;
    using System.IO;
    using System.Xml;

    internal static class HashHelper
    {
        private static uint[] Devide(uint[] a, uint[] b)
        {
            int length = a.Length;
            int num2 = b.Length;
            uint[] destinationArray = new uint[b.Length + 1];
            Array.Copy(b, destinationArray, b.Length);
            uint[] numArray2 = new uint[a.Length + 1];
            Array.Copy(a, numArray2, a.Length);
            int num3 = length - num2;
            while (num3 >= 0)
            {
                ulong num4 = (ulong) ((numArray2[num3 + num2] * 0x100000000L) + numArray2[(num3 + num2) - 1]);
                ulong num5 = num4 / ((ulong) destinationArray[num2 - 1]);
                ulong num6 = num4 - (num5 * destinationArray[num2 - 1]);
                while (true)
                {
                    if ((num5 >= 0x100000000L) || ((num5 * destinationArray[num2 - 2]) > ((num6 * ((ulong) 0x100000000L)) + numArray2[(num3 + num2) - 2])))
                    {
                        num5 -= (ulong) 1L;
                        num6 += destinationArray[num2 - 1];
                        if (num6 < 0x100000000L)
                        {
                            continue;
                        }
                    }
                    uint num9 = 0;
                    int index = 0;
                    while (true)
                    {
                        long num8;
                        if (index >= num2)
                        {
                            num8 = numArray2[num3 + num2] - num9;
                            numArray2[num3 + num2] = (uint) num8;
                            if (num8 < 0L)
                            {
                                num9 = 0;
                                int num11 = 0;
                                while (true)
                                {
                                    if (num11 >= num2)
                                    {
                                        numArray2[num3 + num2] += num9;
                                        break;
                                    }
                                    num8 = (numArray2[num11 + num3] + destinationArray[num11]) + num9;
                                    numArray2[num3 + num11] = (uint) num8;
                                    num9 = (uint) (num8 >> 0x20);
                                    num11++;
                                }
                            }
                            num3--;
                            break;
                        }
                        ulong num7 = num5 * destinationArray[index];
                        num8 = (long) ((numArray2[index + num3] - num9) - (num7 & 0xffffffffUL));
                        numArray2[num3 + index] = (uint) num8;
                        num9 = (uint) ((num7 >> 0x20) - ((ulong) (num8 >> 0x20)));
                        index++;
                    }
                    break;
                }
            }
            Normalize(ref numArray2);
            return numArray2;
        }

        private static byte[] GetBytes(uint[] data)
        {
            int num = data.Length * 4;
            byte[] buffer = new byte[num];
            int index = num - 1;
            for (int i = 0; index > -1; i++)
            {
                buffer[index] = (byte) (data[i / 4] >> ((8 * (i % 4)) & 0x1f));
                index--;
            }
            return buffer;
        }

        private static uint[] Multiply(uint[] a, uint[] b)
        {
            uint[] data = new uint[a.Length + b.Length];
            int index = 0;
            while (index < b.Length)
            {
                uint num2 = 0;
                int num4 = 0;
                while (true)
                {
                    if (num4 >= a.Length)
                    {
                        data[index + a.Length] = num2;
                        index++;
                        break;
                    }
                    ulong num = ((a[num4] * b[index]) + data[num4 + index]) + num2;
                    data[num4 + index] = (uint) num;
                    num2 = (uint) (num >> 0x20);
                    num4++;
                }
            }
            Normalize(ref data);
            return data;
        }

        private static void Normalize(ref uint[] data)
        {
            int index = data.Length - 1;
            while ((index > 0) && (data[index] <= 0))
            {
                index--;
            }
            Array.Resize<uint>(ref data, index + 1);
        }

        private static unsafe uint[] Parse(byte[] value)
        {
            uint[] numArray = new uint[value.Length / 4];
            int index = value.Length - 1;
            for (int i = 0; index >= 0; i++)
            {
                uint* numPtr1 = &(numArray[i / 4]);
                numPtr1[0] += (uint) (value[index] << ((8 * (i % 4)) & 0x1f));
                index--;
            }
            return numArray;
        }

        public static bool VerifyHash(string puk, byte[] rgbHash, byte[] rgbSignature)
        {
            byte[] buffer = null;
            byte[] buffer2 = null;
            XmlTextReader reader = new XmlTextReader(new StringReader(puk));
            while (reader.Read())
            {
                if (reader.NodeType != XmlNodeType.Element)
                {
                    continue;
                }
                if ((reader.Name == "Modulus") && (reader.Read() && (reader.NodeType == XmlNodeType.Text)))
                {
                    buffer = Convert.FromBase64String(reader.Value);
                }
                if ((reader.Name == "Exponent") && (reader.Read() && (reader.NodeType == XmlNodeType.Text)))
                {
                    buffer2 = Convert.FromBase64String(reader.Value);
                }
            }
            uint[] a = new uint[] { 1 };
            uint[] b = Parse(rgbSignature);
            uint[] numArray3 = Parse(buffer);
            if (buffer2.Length != 3)
            {
                throw new Exception();
            }
            int num = (buffer2[0] + (buffer2[1] * 0x100)) + (buffer2[2] * 0x10000);
            while (num > 0)
            {
                if ((num % 2) > 0)
                {
                    a = Devide(Multiply(a, b), numArray3);
                }
                num = num >> 1;
                b = Devide(Multiply(b, b), numArray3);
            }
            byte[] bytes = GetBytes(a);
            int num2 = bytes.Length - rgbHash.Length;
            for (int i = 0; i < rgbHash.Length; i++)
            {
                if (rgbHash[i] != bytes[num2 + i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}

