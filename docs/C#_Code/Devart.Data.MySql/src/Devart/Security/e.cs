namespace Devart.Security
{
    using Devart.Common;
    using Devart.Cryptography;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Security.Cryptography;
    using System.Text;

    internal class e : ICloneable
    {
        private IntPtr a;
        private Devart.Security.af b;
        internal CertificateInfo c;
        internal Devart.Cryptography.w d;
        private Devart.Security.aj e;

        public e(Devart.Security.e A_0)
        {
            if (A_0 == null)
            {
                throw new ArgumentNullException();
            }
            this.a(A_0.x(), true, null);
        }

        public e(IntPtr A_0) : this(A_0, false)
        {
        }

        internal e(IntPtr A_0, Devart.Security.af A_1)
        {
            this.a(A_0, false, A_1);
        }

        public e(IntPtr A_0, bool A_1)
        {
            this.a(A_0, A_1, null);
        }

        private byte[] a()
        {
            byte[] destination = new byte[this.d.c];
            Marshal.Copy(this.d.b, destination, 0, this.d.c);
            return destination;
        }

        internal void a(Devart.Security.af A_0)
        {
            if (!ReferenceEquals(this.b, A_0))
            {
                this.e = null;
            }
            this.b = A_0;
        }

        public virtual bool a(Devart.Security.e A_0) => 
            (A_0 != null) ? (Devart.Cryptography.z.CertCompareCertificate(0x10001, this.d.d, A_0.d.d) != 0) : false;

        public byte[] a(Devart.Security.j A_0)
        {
            byte[] buffer;
            IntPtr ptr = Devart.Common.ar.a(0x100);
            try
            {
                int num = 0x100;
                if ((Devart.Cryptography.z.CertGetCertificateContextProperty(this.x(), (int) A_0, ptr, ref num) == 0) || ((num <= 0) || (num > 0x100)))
                {
                    throw new Devart.Security.r("An error occurs while retrieving the hash of the certificate.");
                }
                buffer = new byte[num];
                Marshal.Copy(ptr, buffer, 0, num);
            }
            catch (Exception exception1)
            {
                throw exception1;
            }
            finally
            {
                Devart.Common.ar.c(ptr);
            }
            return buffer;
        }

        public string a(bool A_0)
        {
            if (!A_0)
            {
                return this.ToString();
            }
            string[] textArray1 = new string[12];
            textArray1[0] = "CERTIFICATE:\r\n        Format:  X509\r\n        Name:  ";
            textArray1[1] = this.ai();
            textArray1[2] = "\r\n        Issuing CA:  ";
            textArray1[3] = this.z();
            textArray1[4] = "\r\n        Key Algorithm:  ";
            textArray1[5] = this.m();
            textArray1[6] = "\r\n        Serial Number:  ";
            textArray1[7] = this.y();
            textArray1[8] = "\r\n        Key Alogrithm Parameters:  ";
            textArray1[9] = this.e();
            textArray1[10] = "\r\n        Public Key:  ";
            textArray1[11] = this.p();
            return string.Concat(textArray1);
        }

        public override bool a(object A_0)
        {
            try
            {
                return this.a((Devart.Security.e) A_0);
            }
            catch
            {
                return false;
            }
        }

        public static Devart.Security.e a(string A_0)
        {
            if (A_0 == null)
            {
                throw new ArgumentNullException("rawString");
            }
            return e(Convert.FromBase64String(A_0));
        }

        private void a(byte[] A_0)
        {
            int num = 0;
            IntPtr zero = IntPtr.Zero;
            if (Devart.Cryptography.z.CryptImportKey(Devart.Cryptography.m.b(), A_0, A_0.Length, IntPtr.Zero, num, ref zero) == 0)
            {
                throw new Devart.Security.r("Could not import the private key from the PVK file.");
            }
            Devart.Cryptography.ap ap = new Devart.Cryptography.ap();
            ap.b(Devart.Cryptography.m.e());
            ap.a(null);
            ap.c = 1;
            ap.d = 0;
            ap.e = 0;
            ap.f = IntPtr.Zero;
            ap.g = 2;
            if (Devart.Cryptography.z.CertSetCertificateContextProperty(this.x(), 2, 0, ref ap) == 0)
            {
                throw new Devart.Security.r("Could not associate the private key with the certificate.");
            }
            Devart.Cryptography.z.CryptDestroyKey(zero);
            Array.Clear(A_0, 0, A_0.Length);
        }

        private byte[] a(RSAParameters A_0)
        {
            Devart.Cryptography.aj aj = new Devart.Cryptography.aj(A_0.Modulus);
            Devart.Security.g g = Devart.Security.i.b((uint) aj.a());
            byte[] buffer = new byte[] { 7, 2, 0, 0 };
            int num = 0 + 4;
            ax.b(0x2400, buffer, num);
            num += 4;
            Buffer.BlockCopy(Encoding.ASCII.GetBytes("RSA2"), 0, buffer, num, 4);
            num += 4;
            ax.b((uint) aj.a(), buffer, num);
            num += 4;
            if (A_0.Exponent.Length > 4)
            {
                throw new Exception("Exponent length is too big.");
            }
            num += Devart.Cryptography.aj.a(new Devart.Cryptography.aj(A_0.Exponent), buffer, num, (int) g.o());
            num += Devart.Cryptography.aj.a(aj, buffer, num, (int) g.c());
            num += Devart.Cryptography.aj.a(new Devart.Cryptography.aj(A_0.P), buffer, num, (int) g.f());
            num += Devart.Cryptography.aj.a(new Devart.Cryptography.aj(A_0.Q), buffer, num, (int) g.i());
            num += Devart.Cryptography.aj.a(new Devart.Cryptography.aj(A_0.DP), buffer, num, (int) g.p());
            num += Devart.Cryptography.aj.a(new Devart.Cryptography.aj(A_0.DQ), buffer, num, (int) g.m());
            num += Devart.Cryptography.aj.a(new Devart.Cryptography.aj(A_0.InverseQ), buffer, num, (int) g.g());
            return ax.a(buffer, (int) (num + Devart.Cryptography.aj.a(new Devart.Cryptography.aj(A_0.D), buffer, num, (int) g.a())));
        }

        private static string a(string A_0, string A_1)
        {
            int index = A_0.IndexOf("-----BEGIN " + A_1 + "-----");
            if (index < 0)
            {
                return null;
            }
            int num2 = A_0.IndexOf("-----END " + A_1 + "-----", index);
            if (num2 < 0)
            {
                return null;
            }
            int num3 = A_1.Length + 0x10;
            int length = num2 - (index + num3);
            return A_0.Substring(index + num3, length);
        }

        private void a(byte[] A_0, string A_1)
        {
            if (A_1 == null)
            {
                throw new ArgumentNullException();
            }
            try
            {
                FileStream stream1 = File.Open(A_1, FileMode.CreateNew, FileAccess.Write, FileShare.None);
                stream1.Write(A_0, 0, A_0.Length);
                stream1.Close();
            }
            catch (Exception exception)
            {
                throw new IOException("Could not write data to file.", exception);
            }
        }

        private void a(IntPtr A_0, bool A_1, Devart.Security.af A_2)
        {
            if (A_0 == IntPtr.Zero)
            {
                throw new ArgumentException("Invalid certificate handle!");
            }
            this.a = !A_1 ? A_0 : Devart.Cryptography.z.CertDuplicateCertificateContext(A_0);
            this.d = (Devart.Cryptography.w) Marshal.PtrToStructure(A_0, typeof(Devart.Cryptography.w));
            this.c = (CertificateInfo) Marshal.PtrToStructure(this.d.d, typeof(CertificateInfo));
            if (A_2 == null)
            {
                this.b = null;
            }
            else
            {
                this.b = A_2;
            }
        }

        public static Devart.Security.e a(byte[] A_0, int A_1, int A_2)
        {
            if (A_0 == null)
            {
                throw new ArgumentNullException();
            }
            if ((A_1 < 0) || ((A_1 + A_2) > A_0.Length))
            {
                throw new ArgumentOutOfRangeException();
            }
            IntPtr destination = Devart.Common.ar.a(A_2);
            Marshal.Copy(A_0, A_1, destination, A_2);
            IntPtr ptr2 = Devart.Cryptography.z.CertCreateCertificateContext(0x10001, destination, A_2);
            Devart.Common.ar.c(destination);
            if (ptr2 == IntPtr.Zero)
            {
                throw new Devart.Security.r("Unable to load the specified certificate.");
            }
            return new Devart.Security.e(ptr2);
        }

        private static string a(string A_0, string A_1, ref int A_2)
        {
            int index = A_0.IndexOf("-----BEGIN " + A_1 + "-----", A_2);
            if (index < 0)
            {
                return null;
            }
            int num2 = A_0.IndexOf("-----END " + A_1 + "-----", index);
            if (num2 < 0)
            {
                return null;
            }
            int num3 = A_1.Length + 0x10;
            int length = num2 - (index + num3);
            A_2 = (num2 + A_1.Length) + 14;
            return A_0.Substring(index + num3, length);
        }

        private byte[] a(byte[] A_0, int A_1, int A_2, byte[] A_3, int A_4)
        {
            byte[] destinationArray = new byte[0x10];
            Array.Copy(SHA1.Create().ComputeHash(A_3, 0, A_3.Length), 0, destinationArray, 0, A_4);
            byte[] buffer2 = Devart.Cryptography.h.a().CreateDecryptor(destinationArray, null).TransformFinalBlock(A_0, A_1, A_2);
            return (((buffer2[0] != 0x52) || ((buffer2[1] != 0x53) || ((buffer2[2] != 0x41) || (buffer2[3] != 50)))) ? null : buffer2);
        }

        public override int aa()
        {
            byte[] sourceArray = this.l();
            byte[] destinationArray = new byte[4];
            if (sourceArray.Length < destinationArray.Length)
            {
                Array.Copy(sourceArray, 0, destinationArray, 0, sourceArray.Length);
            }
            else
            {
                Array.Copy(sourceArray, 0, destinationArray, 0, destinationArray.Length);
            }
            return Devart.Common.a0.g(destinationArray, 0);
        }

        public Devart.Security.a[] ab()
        {
            Devart.Security.a[] aArray = new Devart.Security.a[this.c.cExtension];
            int num = 8 + (IntPtr.Size * 2);
            IntPtr rgExtension = this.c.rgExtension;
            Type structureType = typeof(Devart.Security.l);
            for (int i = 0; i < this.c.cExtension; i++)
            {
                Devart.Security.l l = (Devart.Security.l) Marshal.PtrToStructure(rgExtension, structureType);
                aArray[i] = new Devart.Security.a(Devart.Common.ar.b(l.a), l.b != 0, new byte[l.c]);
                Marshal.Copy(l.d, aArray[i].c, 0, l.c);
                rgExtension = new IntPtr(rgExtension.ToInt64() + num);
            }
            return aArray;
        }

        public RSA ac()
        {
            int num = 0;
            int num2 = 0;
            IntPtr zero = IntPtr.Zero;
            int num3 = 0x40;
            if (Devart.Cryptography.z.CryptAcquireCertificatePrivateKey(this.x(), num3, IntPtr.Zero, ref zero, ref num, ref num2) == 0)
            {
                throw new Devart.Security.r("Could not acquire private key.");
            }
            if (num2 != 0)
            {
                Devart.Cryptography.z.CryptReleaseContext(zero, 0);
            }
            num3 = 0x40;
            int num4 = 0;
            if ((Devart.Cryptography.z.CryptFindCertificateKeyProvInfo(this.x(), num3, IntPtr.Zero) == 0) || (Devart.Cryptography.z.CertGetCertificateContextProperty(this.x(), 2, (byte[]) null, ref num4) == 0))
            {
                throw new Devart.Security.r("Could not query the associated private key.");
            }
            IntPtr ptr2 = Devart.Common.ar.a(num4);
            RSA rsa = null;
            try
            {
                Devart.Cryptography.z.CertGetCertificateContextProperty(this.x(), 2, ptr2, ref num4);
                Devart.Cryptography.ap ap = (Devart.Cryptography.ap) Marshal.PtrToStructure(ptr2, typeof(Devart.Cryptography.ap));
                CspParameters parameters = new CspParameters {
                    KeyContainerName = ap.a(),
                    ProviderName = null,
                    ProviderType = ap.c,
                    KeyNumber = ap.g
                };
                if ((ap.d & 0x20) != 0)
                {
                    parameters.Flags = CspProviderFlags.UseMachineKeyStore;
                }
                rsa = new RSACryptoServiceProvider(parameters);
            }
            catch (Devart.Security.r r1)
            {
                throw r1;
            }
            catch (Exception exception)
            {
                throw new Devart.Security.r("An error occurs while accessing the certificate's private key.", exception);
            }
            finally
            {
                Devart.Common.ar.c(ptr2);
            }
            return rsa;
        }

        public Devart.Security.ag ad() => 
            new Devart.Security.ag(this.c.SubjectpbData, this.c.SubjectcbData);

        public Devart.Security.aj ae()
        {
            this.e ??= new Devart.Security.aj(this, this.s());
            return this.e;
        }

        public string af()
        {
            byte[] inArray = this.k();
            return Convert.ToBase64String(inArray, 0, inArray.Length);
        }

        public string ag() => 
            this.b(Devart.Security.j.a);

        public int ah()
        {
            IntPtr ptr = Devart.Common.ar.a(4);
            Devart.Cryptography.z.CertGetIntendedKeyUsage(0x10001, this.d.d, ptr, 4);
            byte[] destination = new byte[4];
            Marshal.Copy(ptr, destination, 0, 4);
            Devart.Common.ar.c(ptr);
            return Devart.Common.a0.g(destination, 0);
        }

        public string ai()
        {
            int num = 0;
            Devart.Cryptography.z.CryptDecodeObjectEx(0x10001, new IntPtr(20), this.c.SubjectpbData, this.c.SubjectcbData, 0, IntPtr.Zero, ref num);
            if (num <= 0)
            {
                throw new Devart.Security.r("Unable to decode the name of the certificate.");
            }
            IntPtr zero = IntPtr.Zero;
            string str = null;
            try
            {
                zero = Devart.Common.ar.a(num);
                if (Devart.Cryptography.z.CryptDecodeObjectEx(0x10001, new IntPtr(20), this.c.SubjectpbData, this.c.SubjectcbData, 0, zero, ref num) == 0)
                {
                    throw new Devart.Security.r("Unable to decode the name of the certificate.");
                }
                IntPtr ptr = Devart.Cryptography.z.CertFindRDNAttr("2.5.4.3", zero);
                if (ptr == IntPtr.Zero)
                {
                    ptr = Devart.Cryptography.z.CertFindRDNAttr("1.2.840.113549.1.9.2", zero);
                }
                if (ptr == IntPtr.Zero)
                {
                    ptr = Devart.Cryptography.z.CertFindRDNAttr("2.5.4.10", zero);
                }
                if (ptr != IntPtr.Zero)
                {
                    Devart.Security.ak ak = (Devart.Security.ak) Marshal.PtrToStructure(ptr, typeof(Devart.Security.ak));
                    str = Marshal.PtrToStringUni(ak.d, ak.c / 2);
                }
            }
            catch (Devart.Security.r r1)
            {
                throw r1;
            }
            catch (Exception exception)
            {
                throw new Devart.Security.r("Could not get certificate attributes.", exception);
            }
            finally
            {
                if (zero != IntPtr.Zero)
                {
                    Devart.Common.ar.c(zero);
                }
            }
            if (str == null)
            {
                throw new Devart.Security.r("Certificate does not have a name attribute.");
            }
            return str;
        }

        public object aj() => 
            new Devart.Security.e(Devart.Cryptography.z.CertDuplicateCertificateContext(this.x()));

        public byte[] ak()
        {
            int num = 0;
            Devart.Cryptography.z.CertGetCertificateContextProperty(this.x(), 20, (byte[]) null, ref num);
            byte[] buffer = new byte[num];
            Devart.Cryptography.z.CertGetCertificateContextProperty(this.x(), 20, buffer, ref num);
            return buffer;
        }

        protected override void al()
        {
            try
            {
                if (this.x() != IntPtr.Zero)
                {
                    Devart.Cryptography.z.CertFreeCertificateContext(this.x());
                    this.a = IntPtr.Zero;
                }
            }
            finally
            {
                base.Finalize();
            }
        }

        public byte[] am()
        {
            byte[] destination = new byte[this.c.SubjectPublicKeyInfoPublicKeycbData];
            Marshal.Copy(this.c.SubjectPublicKeyInfoPublicKeypbData, destination, 0, destination.Length);
            return destination;
        }

        public byte[] b()
        {
            byte[] destination = new byte[this.c.SerialNumbercbData];
            if (destination.Length != 0)
            {
                Marshal.Copy(this.c.SerialNumberpbData, destination, 0, destination.Length);
                Array.Reverse(destination, 0, destination.Length);
            }
            return destination;
        }

        public string b(Devart.Security.j A_0) => 
            this.b(this.a(A_0));

        public static Devart.Security.e[] b(string A_0) => 
            c(Devart.Security.af.b(A_0));

        private string b(byte[] A_0)
        {
            string str = "";
            for (int i = 0; i < A_0.Length; i++)
            {
                str = str + A_0[i].ToString("X2");
            }
            return str;
        }

        public void b(string A_0, string A_1)
        {
            if (!File.Exists(A_0))
            {
                throw new FileNotFoundException("The PVK file could not be found.");
            }
            byte[] buffer = new byte[0x18];
            FileStream stream = File.Open(A_0, FileMode.Open, FileAccess.Read, FileShare.Read);
            stream.Read(buffer, 0, buffer.Length);
            if (Devart.Common.a0.b(buffer, 0) != 0xb0b5f11e)
            {
                throw new Devart.Security.r("The specified file is not a valid PVK file.");
            }
            Devart.Common.a0.g(buffer, 8);
            int num = Devart.Common.a0.g(buffer, 0x10);
            byte[] buffer2 = new byte[num];
            byte[] buffer3 = new byte[Devart.Common.a0.g(buffer, 20)];
            stream.Read(buffer2, 0, buffer2.Length);
            stream.Read(buffer3, 0, buffer3.Length);
            if (Devart.Common.a0.g(buffer, 12) != 0)
            {
                if (A_1 == null)
                {
                    throw new ArgumentNullException();
                }
                byte[] bytes = Encoding.ASCII.GetBytes(A_1);
                byte[] destinationArray = new byte[buffer2.Length + A_1.Length];
                Array.Copy(buffer2, 0, destinationArray, 0, buffer2.Length);
                Array.Copy(bytes, 0, destinationArray, buffer2.Length, bytes.Length);
                byte[] sourceArray = this.a(buffer3, 8, buffer3.Length - 8, destinationArray, 0x10);
                if (sourceArray == null)
                {
                    sourceArray = this.a(buffer3, 8, buffer3.Length - 8, destinationArray, 5);
                    if (sourceArray == null)
                    {
                        throw new Devart.Security.r("The PVK file could not be decrypted. [wrong password?]");
                    }
                }
                Array.Copy(sourceArray, 0, buffer3, 8, sourceArray.Length);
                Array.Clear(sourceArray, 0, sourceArray.Length);
                Array.Clear(bytes, 0, bytes.Length);
                Array.Clear(destinationArray, 0, destinationArray.Length);
            }
            this.a(buffer3);
        }

        public string c() => 
            this.b(this.q());

        public static Devart.Security.e c(string A_0) => 
            d(Devart.Security.af.b(A_0));

        public static Devart.Security.e[] c(byte[] A_0)
        {
            if (A_0 == null)
            {
                throw new ArgumentNullException();
            }
            List<Devart.Security.e> list = new List<Devart.Security.e>();
            int num = 0;
            while (true)
            {
                string str = Encoding.ASCII.GetString(A_0, 0, A_0.Length);
                string s = a(str, "CERTIFICATE", ref num);
                if (s == null)
                {
                    s = a(str, "X509 CERTIFICATE", ref num);
                    if (s == null)
                    {
                        if (list.Count == 0)
                        {
                            throw new Devart.Security.r("The specified PEM file does not contain a certificate.");
                        }
                        return list.ToArray();
                    }
                }
                Devart.Security.e item = e(Convert.FromBase64String(s));
                list.Add(item);
            }
        }

        public DateTime d() => 
            DateTime.FromFileTime(this.c.NotBefore);

        public static Devart.Security.e d(string A_0) => 
            Devart.Security.af.a(A_0).a();

        public static Devart.Security.e d(byte[] A_0)
        {
            if (A_0 == null)
            {
                throw new ArgumentNullException();
            }
            string str = Encoding.ASCII.GetString(A_0, 0, A_0.Length);
            string s = a(str, "CERTIFICATE");
            if (s == null)
            {
                s = a(str, "X509 CERTIFICATE");
                if (s == null)
                {
                    throw new Devart.Security.r("The specified PEM file does not contain a certificate.");
                }
            }
            return e(Convert.FromBase64String(s));
        }

        public string e() => 
            this.b(this.j());

        public static Devart.Security.e e(string A_0)
        {
            int length = A_0.LastIndexOf(',');
            string assemblyString = (length == -1) ? null : A_0.Substring(0, length);
            Assembly assembly = null;
            assembly = (assemblyString != null) ? Assembly.Load(assemblyString) : Assembly.GetEntryAssembly();
            A_0 = A_0.Substring(length + 1).Trim();
            if (assembly != null)
            {
                Stream manifestResourceStream = assembly.GetManifestResourceStream(A_0);
                if (manifestResourceStream != null)
                {
                    byte[] buffer = new byte[manifestResourceStream.Length];
                    manifestResourceStream.Read(buffer, 0, buffer.Length);
                    return d(buffer);
                }
            }
            throw new ArgumentException("Resource ID " + A_0 + ": resource not found.");
        }

        public static Devart.Security.e e(byte[] A_0)
        {
            if (A_0 == null)
            {
                throw new ArgumentNullException();
            }
            return a(A_0, 0, A_0.Length);
        }

        public bool f()
        {
            IntPtr zero = IntPtr.Zero;
            int num = 0;
            int num2 = 0;
            bool flag = false;
            if (Devart.Cryptography.z.CryptAcquireCertificatePrivateKey(this.x(), 0x44, IntPtr.Zero, ref zero, ref num, ref num2) != 0)
            {
                flag = true;
            }
            if (num2 != 0)
            {
                Devart.Cryptography.z.CryptReleaseContext(zero, 0);
            }
            return flag;
        }

        public Devart.Security.a f(string A_0)
        {
            if (A_0 == null)
            {
                throw new ArgumentNullException();
            }
            IntPtr ptr = Devart.Cryptography.z.CertFindExtension(A_0, this.c.cExtension, this.c.rgExtension);
            if (ptr == IntPtr.Zero)
            {
                return null;
            }
            Devart.Security.l l = (Devart.Security.l) Marshal.PtrToStructure(ptr, typeof(Devart.Security.l));
            Devart.Security.a a = new Devart.Security.a(Devart.Common.ar.b(l.a), l.b != 0, new byte[l.c]);
            Marshal.Copy(l.d, a.c, 0, l.c);
            return a;
        }

        public void f(byte[] A_0)
        {
            RSAParameters parameters = Devart.Security.i.a(A_0);
            this.a(this.a(parameters));
        }

        public RSA g()
        {
            IntPtr zero = IntPtr.Zero;
            IntPtr ptr2 = IntPtr.Zero;
            IntPtr ptr3 = Devart.Cryptography.m.b();
            RSA rsa = null;
            try
            {
                Devart.Cryptography.a3 a = new Devart.Cryptography.a3(this.c);
                int num = 0;
                if (Devart.Cryptography.z.CryptImportPublicKeyInfoEx(ptr3, 0x10001, ref a, 0, 0, IntPtr.Zero, ref ptr2) == 0)
                {
                    throw new Devart.Security.r("Could not obtain the handle of the public key.");
                }
                if (Devart.Cryptography.z.CryptExportKey(ptr2, IntPtr.Zero, 6, 0, IntPtr.Zero, ref num) == 0)
                {
                    throw new Devart.Security.r("Could not get the size of the key.");
                }
                zero = Devart.Common.ar.a(num);
                if (Devart.Cryptography.z.CryptExportKey(ptr2, IntPtr.Zero, 6, 0, zero, ref num) == 0)
                {
                    throw new Devart.Security.r("Could not export the key.");
                }
                Devart.Security.q q = (Devart.Security.q) Marshal.PtrToStructure(zero, typeof(Devart.Security.q));
                if (q.e != 0x31415352)
                {
                    throw new Devart.Security.r("This is not an RSA certificate.");
                }
                RSAParameters parameters = new RSAParameters();
                byte[] buffer = new byte[4];
                ax.a((uint) q.g, buffer, 0);
                parameters.Exponent = buffer;
                parameters.Modulus = new byte[q.f / 8];
                Marshal.Copy(new IntPtr(zero.ToInt64() + Marshal.SizeOf(typeof(Devart.Security.q))), parameters.Modulus, 0, parameters.Modulus.Length);
                Array.Reverse(parameters.Modulus, 0, parameters.Modulus.Length);
                CspParameters parameters1 = new CspParameters();
                parameters1.Flags = CspProviderFlags.UseMachineKeyStore;
                rsa = new RSACryptoServiceProvider(parameters1);
                rsa.ImportParameters(parameters);
            }
            finally
            {
                if (ptr2 != IntPtr.Zero)
                {
                    Devart.Cryptography.z.CryptDestroyKey(ptr2);
                }
                if (zero != IntPtr.Zero)
                {
                    Devart.Common.ar.c(zero);
                }
            }
            return rsa;
        }

        public void g(string A_0)
        {
            int length = A_0.LastIndexOf(',');
            string assemblyString = (length == -1) ? null : A_0.Substring(0, length);
            Assembly assembly = null;
            assembly = (assemblyString != null) ? Assembly.Load(assemblyString) : Assembly.GetEntryAssembly();
            A_0 = A_0.Substring(length + 1).Trim();
            if (assembly != null)
            {
                Stream manifestResourceStream = assembly.GetManifestResourceStream(A_0);
                if (manifestResourceStream != null)
                {
                    byte[] buffer = new byte[manifestResourceStream.Length];
                    manifestResourceStream.Read(buffer, 0, buffer.Length);
                    this.f(buffer);
                    return;
                }
            }
            throw new ArgumentException("Resource ID " + A_0 + ": resource not found.");
        }

        internal CertificateInfo h() => 
            this.c;

        public void h(string A_0)
        {
            this.a(this.a(), A_0);
        }

        public int i() => 
            Devart.Cryptography.z.CertGetPublicKeyLength(0x10001, new IntPtr(this.d.d.ToInt64() + Marshal.OffsetOf(typeof(CertificateInfo), "SubjectPublicKeyInfoAlgorithmpszObjId").ToInt64()));

        public void i(string A_0)
        {
            if (!File.Exists(A_0))
            {
                throw new FileNotFoundException("The PEM file could not be found.");
            }
            RSAParameters parameters = Devart.Security.i.d(A_0);
            this.a(this.a(parameters));
        }

        public byte[] j()
        {
            byte[] destination = new byte[this.c.SignatureAlgorithmParameterscbData];
            if (destination.Length != 0)
            {
                Marshal.Copy(this.c.SignatureAlgorithmParameterspbData, destination, 0, destination.Length);
            }
            return destination;
        }

        public byte[] k() => 
            this.a();

        public byte[] l() => 
            this.a(Devart.Security.j.a);

        public string m() => 
            Devart.Common.ar.b(this.c.SignatureAlgorithmpszObjId);

        public string n() => 
            "X509";

        public DateTime o() => 
            DateTime.FromFileTime(this.c.NotAfter);

        public string p() => 
            this.b(this.am());

        public byte[] q()
        {
            byte[] destination = new byte[this.d.c];
            Marshal.Copy(this.d.b, destination, 0, destination.Length);
            return destination;
        }

        public byte[] r() => 
            Encoding.ASCII.GetBytes("-----BEGIN CERTIFICATE-----\r\n" + this.af() + "\r\n-----END CERTIFICATE-----\r\n");

        internal Devart.Security.af s() => 
            this.b;

        public bool t() => 
            (this.ah() == 0) || ((this.ah() & 0x80) != 0);

        public override string u() => 
            base.GetType().FullName;

        public bool v() => 
            Devart.Cryptography.z.CertVerifyTimeValidity(IntPtr.Zero, this.d.d) == 0;

        public bool w() => 
            (this.ah() == 0) || ((this.ah() & 0x10) != 0);

        public IntPtr x() => 
            this.a;

        public string y() => 
            this.b(this.b());

        public string z()
        {
            int num = Devart.Cryptography.z.CertGetNameString(this.x(), 4, 0x10001, IntPtr.Zero, IntPtr.Zero, 0);
            if (num <= 0)
            {
                throw new Devart.Security.r("An error occurs while requesting the issuer name.");
            }
            IntPtr ptr = Devart.Common.ar.a(num);
            Devart.Cryptography.z.CertGetNameString(this.x(), 4, 0x10001, IntPtr.Zero, ptr, num);
            Devart.Common.ar.c(ptr);
            return Devart.Common.ar.b(ptr);
        }
    }
}

