namespace Devart.Common
{
    using Devart.Cryptography;
    using Microsoft.Win32;
    using System;
    using System.Collections;
    using System.Globalization;
    using System.IO;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Security;
    using System.Text;

    internal class ab
    {
        private static byte[] a;
        private static byte b;
        private bool c = true;
        protected static Devart.Common.ab d;
        private static Devart.Cryptography.ad e;

        private Devart.Cryptography.ad a()
        {
            Devart.Cryptography.ad e;
            lock (this)
            {
                if (Devart.Common.ab.e != null)
                {
                    e = Devart.Common.ab.e;
                }
                else
                {
                    Devart.Common.ab.e = new Devart.Cryptography.ad();
                    Devart.Common.ab.e.a(this.b());
                    e = Devart.Common.ab.e;
                }
            }
            return e;
        }

        internal static string a(Assembly A_0)
        {
            string fullName = A_0.FullName;
            string path = null;
            try
            {
                path = A_0.Location;
            }
            catch
            {
            }
            if ((path == null) || (path == ""))
            {
                path = A_0.ManifestModule.ScopeName;
            }
            return (fullName.Substring(0, fullName.IndexOf(',')) + Path.GetExtension(path));
        }

        internal string[] a(string A_0)
        {
            if ((A_0 == null) || (A_0 == ""))
            {
                return null;
            }
            string path = Path.Combine(Path.GetDirectoryName(A_0), this.l());
            if (!File.Exists(path))
            {
                return null;
            }
            StreamReader reader = null;
            ArrayList list = new ArrayList();
            try
            {
                string str2;
                reader = new StreamReader(path, Encoding.ASCII, true, 0x200);
                while ((str2 = reader.ReadLine()) != null)
                {
                    str2 = Path.GetFileName(str2).Trim();
                    if (str2 != "")
                    {
                        list.Add(str2);
                    }
                }
            }
            finally
            {
                reader.Close();
            }
            return (string[]) list.ToArray(typeof(string));
        }

        private bool a(byte[] A_0)
        {
            int index = 0;
            Devart.Cryptography.ad ad = this.a();
            byte[] buffer = this.e();
            if ((A_0 == null) || (A_0.Length <= buffer.Length))
            {
                return false;
            }
            byte[] buffer2 = new byte[buffer.Length];
            byte[] buffer3 = new byte[A_0.Length];
            ad.a(buffer, 0, buffer2.Length, buffer2, 0, new byte[8]);
            ad.a(A_0, 0, buffer3.Length, buffer3, 0, new byte[8]);
            int num2 = (buffer2[1] << 8) | buffer2[0];
            int num3 = (buffer3[1] << 8) | buffer3[0];
            if ((num2 > (buffer2.Length - 2)) || ((num3 > (buffer3.Length - 2)) || (num2 > num3)))
            {
                return false;
            }
            index = 2;
            int num4 = (((buffer3[index + 3] << 0x18) | (buffer3[index + 2] << 0x10)) | (buffer3[index + 1] << 8)) | buffer3[index];
            index += 4;
            for (int i = index; i < buffer3.Length; i++)
            {
                buffer3[i] = (byte) (buffer3[i] - num4);
            }
            for (int j = 0; j < num2; j++)
            {
                if (buffer2[j + 2] != buffer3[index + j])
                {
                    return false;
                }
            }
            return true;
        }

        private static double a(byte[] A_0, int A_1)
        {
            if (!BitConverter.IsLittleEndian)
            {
                byte[] buffer = new byte[8];
                int num = A_1 + 7;
                int index = 0;
                while (true)
                {
                    if (index >= 8)
                    {
                        A_0 = buffer;
                        A_1 = 0;
                        break;
                    }
                    buffer[index] = A_0[num--];
                    index++;
                }
            }
            return BitConverter.ToDouble(A_0, A_1);
        }

        public int a(out string A_0, out string A_1)
        {
            A_1 = null;
            A_0 = null;
            byte[] inArray = null;
            byte[] buffer2 = this.d();
            RegistryKey key = null;
            try
            {
                key = Registry.ClassesRoot.OpenSubKey(Devart.Common.n.b(buffer2));
            }
            catch (SecurityException exception)
            {
                A_1 = exception.Message;
                return 11;
            }
            if (key == null)
            {
                return 1;
            }
            try
            {
                inArray = key.GetValue(null) as byte[];
            }
            finally
            {
                key.Close();
            }
            if (inArray == null)
            {
                return 1;
            }
            A_0 = Convert.ToBase64String(inArray, 0, inArray.Length);
            return this.a(A_0, null, false, out A_1);
        }

        internal virtual string a(int A_0, string A_1) => 
            "";

        protected int a(string A_0, out byte[] A_1)
        {
            A_1 = null;
            if ((A_0 == null) || (A_0 == ""))
            {
                return 0;
            }
            string path = Path.Combine(Path.GetDirectoryName(A_0), this.i());
            if (!File.Exists(path))
            {
                return 0;
            }
            StreamReader reader = null;
            string s = null;
            try
            {
                reader = new StreamReader(path, Encoding.ASCII, true, 0x200);
                while ((s = reader.ReadLine()) != null)
                {
                    byte[] buffer = Convert.FromBase64String(s);
                    if (this.a(buffer))
                    {
                        A_1 = buffer;
                        break;
                    }
                }
            }
            finally
            {
                reader.Close();
            }
            return ((A_1 != null) ? 0 : 2);
        }

        private string a(byte[] A_0, int A_1, int A_2)
        {
            int count = 0;
            int num2 = 0;
            while (true)
            {
                if (num2 < A_2)
                {
                    if (A_0[A_1 + num2] != 0)
                    {
                        num2++;
                        continue;
                    }
                    count = num2;
                }
                return h().GetString(A_0, A_1, count);
            }
        }

        public int a(string A_0, string A_1, out string A_2, out string A_3)
        {
            int num2;
            A_3 = null;
            MemoryStream stream = new MemoryStream();
            A_2 = null;
            byte[] buffer = null;
            int num = this.a(out A_2, out A_3);
            try
            {
                buffer = Convert.FromBase64String(A_2);
            }
            catch
            {
            }
            if ((buffer == null) && (num == 0))
            {
                A_3 = "10";
                num = 2;
            }
            if (buffer == null)
            {
                num2 = 0;
            }
            else
            {
                if (num != 0)
                {
                    byte[] buffer6 = new byte[buffer.Length];
                    this.a().a(buffer, 0, buffer.Length, buffer6, 0, new byte[8]);
                    byte[] buffer7 = this.g();
                    if (buffer7 == null)
                    {
                        buffer6[0x1b] = 0xff;
                    }
                    else
                    {
                        int index = 0;
                        while (true)
                        {
                            if (index >= 8)
                            {
                                buffer6[0x1b] = b;
                                break;
                            }
                            buffer6[0x13 + index] = buffer7[index];
                            index++;
                        }
                    }
                    buffer = new byte[buffer6.Length];
                    this.a().b(buffer6, 0, buffer6.Length, buffer, 0, new byte[8]);
                }
                num2 = buffer.Length;
            }
            int num3 = (5 + num2) + 0x100;
            stream.WriteByte(0);
            stream.WriteByte(0);
            stream.WriteByte(2);
            stream.WriteByte((byte) num2);
            stream.WriteByte((byte) (num2 >> 8));
            if (buffer != null)
            {
                stream.Write(buffer, 0, num2);
            }
            byte[] bytes = Encoding.Default.GetBytes(A_0);
            int length = bytes.Length;
            if (length > 0xff)
            {
                length = 0xff;
            }
            stream.Write(bytes, 0, length);
            for (int i = 0; i < (0x100 - length); i++)
            {
                stream.WriteByte(0);
            }
            byte[] buffer3 = null;
            if (this.k())
            {
                int num9 = this.a(A_1, out buffer3);
                this.c = (buffer3 != null) || (num9 != 0);
                if ((num9 != 0) && (num == 0))
                {
                    num = num9;
                    A_3 = "13";
                }
            }
            byte num5 = 6;
            if (this.j())
            {
                num5 = (byte) (num5 | 3);
            }
            if ((num != 0) && (num != 3))
            {
                num5 = (byte) (num5 | 8);
            }
            if (!this.m() || this.k())
            {
                num5 = (byte) (num5 | 0x10);
            }
            if (this.k())
            {
                num5 = (byte) (num5 | 0x20);
            }
            stream.WriteByte(num5);
            if (this.k() && (buffer3 != null))
            {
                stream.WriteByte((byte) buffer3.Length);
                stream.WriteByte((byte) (buffer3.Length >> 8));
                stream.Write(buffer3, 0, buffer3.Length);
            }
            if (this.m() && !this.k())
            {
                string[] strArray = this.a(A_1);
                int num10 = (strArray != null) ? strArray.Length : 0;
                stream.WriteByte((byte) num10);
                for (int j = 0; j < num10; j++)
                {
                    bytes = Encoding.Default.GetBytes(strArray[j]);
                    stream.Write(bytes, 0, bytes.Length);
                    stream.WriteByte(0);
                }
            }
            int num6 = ((((((int) stream.Length) + 2) - 1) / 8) + 1) * 8;
            while ((stream.Length + 2L) < num6)
            {
                stream.WriteByte(0);
            }
            stream.WriteByte(0x53);
            stream.WriteByte(0xa9);
            byte[] buffer4 = new byte[] { (byte) num6, (byte) (num6 >> 8) };
            stream.Seek(0L, SeekOrigin.Begin);
            stream.Read(buffer4, 0, buffer4.Length);
            byte[] buffer5 = new byte[buffer4.Length];
            this.a().b(buffer4, 0, buffer4.Length, buffer5, 0, new byte[8]);
            A_2 = Convert.ToBase64String(buffer5, 0, buffer5.Length);
            return num;
        }

        public int a(string A_0, string A_1, bool A_2, out string A_3)
        {
            byte[] buffer2;
            bool flag;
            double num;
            int num6;
            int num7;
            int num8;
            Assembly entryAssembly;
            bool flag4;
            byte[] buffer1;
            A_3 = null;
            if ((A_0 == null) || (A_0 == ""))
            {
                return 1;
            }
            byte[] buffer = null;
            try
            {
                buffer = Convert.FromBase64String(A_0);
            }
            catch
            {
            }
            if (buffer == null)
            {
                A_3 = "1";
                return 2;
            }
            Devart.Cryptography.ad ad = this.a();
            byte[] buffer3 = null;
            if (!A_2)
            {
                buffer2 = new byte[buffer.Length];
                if (buffer.Length < 0x30)
                {
                    A_3 = "2";
                    return 2;
                }
                ad.a(buffer, 0, buffer.Length, buffer2, 0, new byte[8]);
            }
            else
            {
                buffer3 = new byte[buffer.Length];
                ad.a(buffer, 0, buffer.Length, buffer3, 0, new byte[8]);
                int num2 = (buffer3[1] << 8) | buffer3[0];
                if (num2 > buffer3.Length)
                {
                    A_3 = "3";
                    return 2;
                }
                if (((buffer3[num2 - 1] << 8) | buffer3[num2 - 2]) != 0xa953)
                {
                    A_3 = "4";
                    return 2;
                }
                int num3 = (buffer3[4] << 8) | buffer3[3];
                if (num3 < 0x30)
                {
                    A_3 = "4";
                    return 9;
                }
                buffer2 = new byte[num3];
                ad.a(buffer3, 5, buffer2.Length, buffer2, 0, new byte[8]);
            }
            byte[] buffer4 = this.c();
            int index = 0;
            while (true)
            {
                if (index < buffer4.Length)
                {
                    if (buffer4[index] != buffer2[index])
                    {
                        A_3 = "5";
                        return 2;
                    }
                    index++;
                    continue;
                }
                flag = false;
                if (A_2)
                {
                    num6 = 5 + buffer2.Length;
                    num7 = num6 + 0x100;
                    flag = (buffer3[num7] & 2) != 0;
                    if (buffer3[2] == 0)
                    {
                        return 2;
                    }
                    if ((buffer3[num7] & 0x10) != 0)
                    {
                        goto TR_0027;
                    }
                    else
                    {
                        num8 = 0;
                        entryAssembly = Assembly.GetEntryAssembly();
                        if (buffer3[2] < 2)
                        {
                            goto TR_0029;
                        }
                        else
                        {
                            int num9 = num7 + 1;
                            num8 = buffer3[num9++];
                            flag4 = false;
                            if ((num8 <= 0) || (entryAssembly == null))
                            {
                                goto TR_0029;
                            }
                            else
                            {
                                for (int i = 0; i < num8; i++)
                                {
                                    string strA = this.a(buffer3, num9, buffer3.Length - num9);
                                    num9++;
                                    if ((strA != null) && (strA != ""))
                                    {
                                        num9 += strA.Length;
                                        if (string.Compare(strA, a(entryAssembly), true, CultureInfo.InvariantCulture) == 0)
                                        {
                                            flag4 = true;
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    if ((buffer2[8] != 1) || (buffer2[9] != 8))
                    {
                        A_3 = "6";
                        return 2;
                    }
                    byte[] buffer5 = this.g();
                    if ((buffer5 == null) || (buffer5.Length != 8))
                    {
                        return 1;
                    }
                    int num5 = 0;
                    while (true)
                    {
                        if (num5 < 8)
                        {
                            if (buffer2[num5 + 10] == buffer5[num5])
                            {
                                num5++;
                                continue;
                            }
                            if ((buffer2[0x12] == b) && (b != 3))
                            {
                                A_3 = "7";
                                return 2;
                            }
                        }
                        flag = true;
                        break;
                    }
                    goto TR_000A;
                }
                break;
            }
            if (!flag4)
            {
                A_3 = a(entryAssembly);
                return 6;
            }
            goto TR_0029;
        TR_000A:
            num = a(buffer2, 40);
            DateTime today = DateTime.Today;
            if ((num > 364999.0) && (num < 365001.0))
            {
                return (!flag ? 4 : 0);
            }
            bool flag2 = true;
            if ((num < -657435.0) || (num > 2958466.0))
            {
                flag2 = false;
            }
            else
            {
                today = today.AddDays((double) (-((int) num) + 2));
                if ((today.Year != 0x76c) || ((today.Month > 1) || (today.Day > 30)))
                {
                    flag2 = false;
                }
            }
            return (flag2 ? 0 : 3);
        TR_0027:
            if ((buffer3[num7] & 0x20) != 0)
            {
                int num11 = num7 + 1;
                int num12 = (buffer3[num11 + 1] << 8) | buffer3[num11];
                if (num12 > ((buffer3.Length + num11) + 2))
                {
                    A_3 = "11";
                    return 2;
                }
                byte[] dst = new byte[num12];
                Buffer.BlockCopy(buffer3, num11 + 2, dst, 0, dst.Length);
                if (!this.a(dst))
                {
                    A_3 = "12";
                    return 2;
                }
            }
            if ((buffer3[num7] & 2) == 0)
            {
                A_3 = "Standard";
                return 8;
            }
            if ((buffer3[num7] & 8) != 0)
            {
                return 9;
            }
            goto TR_000A;
        TR_0029:
            buffer1 = new byte[] { 0x4f, 0x89, 0x24, 0x59, 0x55, 0x3b, 0x2a, 13, 0x2a, 0x5f, 8, 0xca, 6, 0xb6, 0x2b, 0x80 };
            byte[] buffer6 = buffer1;
            bool flag3 = (buffer3[num7] & 4) != 0;
            if ((entryAssembly != null) && ((num8 == 0) && (string.Compare(entryAssembly.GetName().Name, A_1, true, CultureInfo.InvariantCulture) != 0)))
            {
                A_3 = entryAssembly.GetName().Name;
                return 6;
            }
            if ((string.Compare(this.a(buffer3, num6, 0xff), A_1, StringComparison.CurrentCultureIgnoreCase) != 0) && (!flag3 || (string.Compare(A_1, Devart.Common.n.b(buffer6), StringComparison.CurrentCultureIgnoreCase) != 0)))
            {
                A_3 = A_1;
                return 7;
            }
            goto TR_0027;
        }

        public int a(string A_0, Assembly A_1, bool A_2, out string A_3, out string A_4)
        {
            A_3 = A_1?.FullName.Substring(0, A_1?.FullName.IndexOf(','));
            return this.a(A_0, A_3, A_2, out A_4);
        }

        protected virtual byte[] b() => 
            new byte[] { 
                0x8e, 0xbd, 12, 0xe8, 0x4b, 0x10, 0xf8, 240, 0x34, 0x52, 11, 0x52, 80, 0x18, 0xb0, 0xcd,
                0x80, 0xfe, 0xb1, 4, 0x3b, 0x87, 0xc1, 0xb6, 0x62, 0x69, 0x33, 0x44, 0x52, 0xaf, 0xb8, 240,
                0x1b
            };

        protected virtual byte[] c() => 
            new byte[] { 0xe8, 0x2b, 0x55, 0xf4, 200, 0x2b, 0x4d, 240 };

        protected virtual byte[] d() => 
            new byte[] { 
                0x42, 0xe0, 0xea, 0x11, 11, 0x87, 0x72, 0x54, 0x7f, 0x6c, 100, 60, 7, 0x86, 0xcc, 0x76,
                0xb1, 0x67, 0x97, 0x4c, 0x4f, 0xce, 0xc0, 220, 0x12, 0xe2, 70, 0x27, 0x1c, 0xf6, 0xd1, 20,
                0x3e, 0x26, 0x54, 0xd5, 9, 0x27, 0x53, 0xef
            };

        protected virtual byte[] e() => 
            new byte[] { 0x7d, 0x44, 0xff, 0x39, 0xd5, 0x39, 0xc4, 0xce, 0x2d, 0x56, 0xd3, 0x5c, 160, 0xbd, 0xc6, 0xb7 };

        public static Devart.Common.ab f()
        {
            lock (typeof(Devart.Common.ab))
            {
                d ??= new Devart.Common.ab();
                return d;
            }
        }

        private byte[] g()
        {
            byte[] a;
            lock (this)
            {
                byte[] bytes;
                RegistryKey key;
                string str;
                if (Devart.Common.ab.a == null)
                {
                    bytes = null;
                    ArrayList list = new ArrayList();
                    if (IntPtr.Size == 8)
                    {
                        byte[] buffer1 = new byte[] { 
                            0x89, 0x94, 0x8b, 180, 0x8e, 0xce, 60, 0x10, 70, 210, 0xa3, 0xf7, 0x9e, 0x76, 0xa3, 0xac,
                            0x47, 0xb6, 0xae, 0xee, 0x67, 0xf4, 0xad, 140, 0xc6, 6, 0x2e, 0x87, 240, 0x98, 0x5e, 1,
                            0xc1, 0x93, 0x66, 0x27, 0x19, 0xbd, 30, 0x48, 0xa4, 0x5b, 0xf9, 0x76, 0x4b, 0x4e, 0x91, 0x9a,
                            0xa5, 0x95, 0x7c, 0x7a, 0x16, 100, 0x49, 0x1d
                        };
                        list.Add(buffer1);
                        byte[] buffer6 = new byte[] { 
                            0x3a, 0xa7, 0x89, 0x7f, 0xbd, 20, 60, 240, 0x9a, 0x7c, 50, 0xbd, 0x8b, 0x80, 0xa8, 0x7d,
                            0x19, 0xf7, 7, 110, 0x8a, 0x79, 0xa1, 0xd5, 0xb8, 0xf4, 0x26, 0xb0, 0x63, 0xf2, 0x95, 0x9c,
                            100, 0x51, 0xa4, 0x90, 0xf4, 180, 0x43, 0xeb, 0xfc, 5, 0xdb, 0xb1, 0x24, 0xc5, 0x4e, 0xa5,
                            5, 8, 0x17, 30, 0xe1, 5, 0xbd, 0x95, 0xbb, 0xce, 0x31, 0xe2, 0x20, 0x74, 0x38, 0xbf
                        };
                        list.Add(buffer6);
                    }
                    else
                    {
                        byte[] buffer7 = new byte[] { 
                            0x67, 0x1a, 0xd1, 7, 170, 0xd3, 50, 0x19, 0xc0, 11, 80, 0x16, 0x21, 0x62, 6, 0xf1,
                            0x5f, 0xbc, 0x34, 0xba, 0x20, 0xa7, 0x4e, 0xf6, 0x47, 0x51, 0x1f, 0x89, 0xc3, 0xa3, 240, 0xca,
                            0x85, 0x42, 0x39, 0x6d, 110, 0xe0, 3, 90, 0x7f, 0x34, 0x3e, 0xfd, 0xc1, 0xc0, 0x25, 0xac
                        };
                        list.Add(buffer7);
                        byte[] buffer8 = new byte[] { 
                            0x30, 0x24, 0x4f, 0x2f, 0x43, 0xa2, 0x87, 100, 0x87, 0xaf, 0x42, 0xab, 0x94, 0x13, 0xe2, 0xe4,
                            0xa8, 0x68, 0x63, 0x7e, 0xa7, 0xca, 0xdd, 0x29, 170, 0x2b, 0xf5, 0x77, 0xe3, 0xf5, 5, 0xde,
                            6, 0xf9, 0xd1, 0x1c, 0x19, 0x2a, 0xaf, 0xc6, 0xa5, 0xef, 0x59, 0xe1, 0x17, 0xe5, 0xa8, 0xb8
                        };
                        list.Add(buffer8);
                    }
                    str = null;
                    b = (IntPtr.Size == 8) ? ((byte) 4) : ((byte) 0);
                    for (int i = 0; i < list.Count; i++)
                    {
                        key = Registry.LocalMachine.OpenSubKey(Devart.Common.n.b((byte[]) list[i]));
                        if (key != null)
                        {
                            try
                            {
                                byte[] buffer9 = new byte[] { 0x92, 0xd9, 0x21, 0x93, 0x3f, 0x31, 0x56, 0xa5, 0x89, 0xf4, 0x1d, 0x9e, 0xfe, 0x92, 160, 0x68 };
                                str = key.GetValue(Devart.Common.n.b(buffer9)) as string;
                            }
                            finally
                            {
                                key.Close();
                            }
                            if (str != null)
                            {
                                b = (byte) (b | (i + 1));
                                break;
                            }
                        }
                    }
                }
                else
                {
                    return Devart.Common.ab.a;
                }
                if (str == null)
                {
                    byte[] buffer5;
                    b = (byte) (b | 3);
                    if (IntPtr.Size == 8)
                    {
                        buffer5 = new byte[] { 
                            0x3a, 0xa7, 0x89, 0x7f, 0xbd, 20, 60, 240, 0x9a, 0x7c, 50, 0xbd, 0x8b, 0x80, 0xa8, 0x7d,
                            0x19, 0xf7, 7, 110, 0x8a, 0x79, 0xa1, 0xd5, 0xb8, 0xf4, 0x26, 0xb0, 0x63, 0xf2, 0x95, 0x9c,
                            100, 0x51, 0xa4, 0x90, 0xf4, 180, 0x43, 0xeb, 0xfc, 5, 0xdb, 0xb1, 0x24, 0xc5, 0x4e, 0xa5,
                            5, 8, 0x17, 30, 0xe1, 5, 0xbd, 0x95, 0xbb, 0xce, 0x31, 0xe2, 0x20, 0x74, 0x38, 0xbf
                        };
                    }
                    else
                    {
                        buffer5 = new byte[] { 
                            0x30, 0x24, 0x4f, 0x2f, 0x43, 0xa2, 0x87, 100, 0x87, 0xaf, 0x42, 0xab, 0x94, 0x13, 0xe2, 0xe4,
                            0xa8, 0x68, 0x63, 0x7e, 0xa7, 0xca, 0xdd, 0x29, 170, 0x2b, 0xf5, 0x77, 0xe3, 0xf5, 5, 0xde,
                            6, 0xf9, 0xd1, 0x1c, 0x19, 0x2a, 0xaf, 0xc6, 0xa5, 0xef, 0x59, 0xe1, 0x17, 0xe5, 0xa8, 0xb8
                        };
                    }
                    key = Registry.LocalMachine.OpenSubKey(Devart.Common.n.b(buffer5));
                    if (key != null)
                    {
                        try
                        {
                            byte[] buffer12 = new byte[] { 
                                130, 0x54, 160, 0xa7, 0x30, 0x19, 0xdf, 0xdb, 0xe5, 0xb3, 0x58, 0x49, 0xe8, 0x26, 0xf8, 0x25,
                                0x44, 0x4a, 0xd9, 0x39, 0xda, 140, 0x39, 0x45
                            };
                            byte[][] bufferArray1 = new byte[3][];
                            bufferArray1[0] = buffer12;
                            bufferArray1[1] = new byte[] { 0x2f, 0xc1, 13, 9, 0x62, 0x8b, 30, 0x31, 110, 0x51, 0x34, 0x51, 0x22, 0xe0, 0xde, 0xa5 };
                            bufferArray1[2] = new byte[] { 
                                0xc5, 0xce, 0x1d, 0xab, 0x16, 0xaf, 0x65, 0x9d, 0xd9, 0x16, 0x1c, 0x6c, 0x37, 0x29, 0xf2, 0x9b,
                                0x18, 0x2c, 0x34, 0x4e, 0xc9, 0x3e, 0x19, 2
                            };
                            byte[][] bufferArray = bufferArray1;
                            str = "";
                            for (int i = 0; i < bufferArray.Length; i++)
                            {
                                object obj2 = key.GetValue(Devart.Common.n.b(bufferArray[i]));
                                if (obj2 != null)
                                {
                                    str = str + obj2.ToString();
                                }
                            }
                        }
                        finally
                        {
                            key.Close();
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
                if (str.Length > 30)
                {
                    str = str.Substring(0, 30);
                }
                bytes = Encoding.Default.GetBytes(str);
                if (bytes == null)
                {
                    a = null;
                }
                else
                {
                    Devart.Cryptography.ad ad = new Devart.Cryptography.ad();
                    ad.a(bytes);
                    Devart.Common.ab.a = new byte[8];
                    byte[] buffer2 = new byte[] { 160, 0x9c, 0x7b, 0x8b, 0x2a, 0x38, 0x2b, 0x57 };
                    ad.b(buffer2, 0, buffer2.Length, Devart.Common.ab.a, 0, new byte[8]);
                    a = Devart.Common.ab.a;
                }
            }
            return a;
        }

        protected static Encoding h() => 
            Encoding.Default;

        protected virtual string i()
        {
            byte[] buffer1 = new byte[] { 0xfe, 230, 0xb1, 0xce, 0xbb, 0x21, 180, 0x85, 11, 0x85, 0x60, 0x73, 0x21, 0xa5, 13, 0x94 };
            return Devart.Common.n.b(buffer1);
        }

        protected virtual bool j() => 
            false;

        protected virtual bool k() => 
            this.c;

        protected virtual string l()
        {
            byte[] buffer1 = new byte[] { 
                0x17, 0x5c, 0xef, 0x95, 0xeb, 0x42, 0xbb, 0x41, 0x6c, 0xe2, 0x54, 0xcd, 190, 0x8a, 0x44, 0x8b,
                0, 11, 0xe7, 0x74, 0xed, 0xcf, 0xc5, 230
            };
            return Devart.Common.n.b(buffer1);
        }

        protected virtual bool m() => 
            true;
    }
}

