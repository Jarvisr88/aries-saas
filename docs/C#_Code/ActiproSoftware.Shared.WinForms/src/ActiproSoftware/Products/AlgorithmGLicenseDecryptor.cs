namespace ActiproSoftware.Products
{
    using #H;
    using #PAb;
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;

    internal class AlgorithmGLicenseDecryptor
    {
        private DateTime #cBb;
        private byte #eBb;
        private string #fBb;
        private string #gBb;
        private AssemblyLicenseType #hBb;
        private byte #iBb;
        private byte #jBb;
        private ushort #kBb;
        private AssemblyPlatform #lBb;
        private #OAb #EBb;
        private #NAb #FBb;
        private const byte #tBb = 90;
        private const byte #uBb = 5;
        private const int #vBb = 0x19;
        private const string #wBb = "G";
        private const byte #xBb = 1;
        private const byte #yBb = 6;
        private const uint #zBb = 1;
        private const uint #ABb = 0xfffff;
        private const byte #BBb = 1;
        private const byte #CBb = 4;
        private const string #DBb = "QHC36BW9KFGUL8PJ4X0T5NYEAZ1DR7V2";
        private static byte[] #GBb;
        private static byte[] #HBb;
        private static byte[] #IBb;
        private static byte[] #JBb;
        private static byte[] #KBb;
        private static byte[] #LBb;
        private static byte[] #MBb;
        private static byte[] #NBb;
        private static byte[] #OBb;
        private static byte[] #PBb;

        private static string #bVb(string #gBb)
        {
            if (#gBb == null)
            {
                #sVb(#MAb.#SBb);
            }
            string str = string.Empty;
            foreach (char ch in #gBb)
            {
                if ((ch != ' ') && (ch != '-'))
                {
                    str = str + ch.ToString();
                }
            }
            if (str.Length != 0x19)
            {
                #sVb(#MAb.#SBb);
            }
            if (!str.EndsWith(#G.#eg(0x19d6), StringComparison.Ordinal))
            {
                #sVb(#MAb.#TBb);
            }
            return str;
        }

        private static uint #kVb(uint #jwf, byte[] #vb, uint #mc)
        {
            uint num = #jwf & 0xffff;
            uint num2 = (#jwf >> 0x10) & 0xffff;
            uint index = 0;
            if (#vb == null)
            {
                return 1;
            }
            while (#mc > 0)
            {
                uint num4 = (#mc < 0x15b0) ? #mc : 0x15b0;
                #mc -= num4;
                while (true)
                {
                    if (num4 < 0x10)
                    {
                        if (num4 != 0)
                        {
                            do
                            {
                                num += #vb[index];
                                index++;
                                num2 += num;
                            }
                            while (--num4 != 0);
                        }
                        num = num % 0xfff1;
                        num2 = num2 % 0xfff1;
                        break;
                    }
                    int num5 = 0;
                    while (true)
                    {
                        if (num5 >= 0x10)
                        {
                            num4 -= (uint) 0x10;
                            break;
                        }
                        num += #vb[index];
                        num2 += num;
                        num5++;
                        index++;
                    }
                }
            }
            return ((num2 << 0x10) | num);
        }

        private void #lVb()
        {
            this.Reset();
            DateTime time = new DateTime(0x7d2, 1, 1);
            byte[] buffer = new byte[12];
            #qVb(this.#gBb, buffer);
            uint num = #mVb(buffer, #GBb);
            this.#EBb = (#OAb) num;
            num = #mVb(buffer, #OBb);
            #nVb(buffer, #OBb, 0);
            if (num != #pVb(this.LicenseKey, this.#EBb, buffer))
            {
                #sVb(#MAb.#1Bb);
            }
            num = #mVb(buffer, #HBb);
            this.#iBb = (byte) (num / 10);
            this.#jBb = (byte) (num % 10);
            num = #mVb(buffer, #IBb);
            this.#hBb = (AssemblyLicenseType) num;
            char ch = (char) #mVb(buffer, #JBb);
            num = #mVb(buffer, #KBb);
            this.#eBb = (byte) num;
            num = #mVb(buffer, #LBb);
            this.#lBb = (AssemblyPlatform) num;
            num = #mVb(buffer, #NBb);
            this.#kBb = (ushort) num;
            this.#cBb = time.AddDays((double) #mVb(buffer, #MBb));
            num = #mVb(buffer, #PBb);
            this.#FBb = (#NAb) num;
            if ((this.#EBb < #OAb.#aCb) || (this.#EBb > 0xfffff))
            {
                #sVb(#MAb.#UBb);
            }
            if ((this.#iBb < 0) || ((this.#iBb > 0x33) || ((this.#jBb < 0) || (this.#jBb > 9))))
            {
                #sVb(#MAb.#YBb);
            }
            if ((((byte) this.#hBb) < 1) || (((byte) this.#hBb) > 4))
            {
                #sVb(#MAb.#VBb);
            }
            if ((((byte) this.#lBb) < 1) || (((byte) this.#lBb) > 6))
            {
                #sVb(#MAb.#XBb);
            }
            string str = this.#iBb.ToString(#G.#eg(0x19db), CultureInfo.InvariantCulture) + this.#jBb.ToString(#G.#eg(0x19e0), CultureInfo.InvariantCulture);
            if (this.#gBb.Substring(3, 3) != str)
            {
                #sVb(#MAb.#0Bb);
            }
            if ((this.#hBb == AssemblyLicenseType.Full) && ((this.#fBb == null) || ((this.#fBb.Length == 0) || !this.#fBb.StartsWith(ch.ToString(), StringComparison.OrdinalIgnoreCase))))
            {
                #sVb(#MAb.#WBb);
            }
        }

        private static uint #mVb(byte[] #kwf, byte[] #lwf)
        {
            uint num = 0;
            for (int i = 0; i < #lwf.Length; i++)
            {
                byte num3 = #lwf[i];
                byte num4 = (byte) (1 << ((7 - (num3 % 8)) & 0x1f));
                if ((#kwf[num3 / 8] & num4) != 0)
                {
                    num |= (uint) (1 << (i & 0x1f));
                }
            }
            return num;
        }

        private static void #nVb(byte[] #kwf, byte[] #mwf, uint #nwf)
        {
            int length = #mwf.Length;
            if (#nwf >= (1 << (length & 0x1f)))
            {
                #sVb(#MAb.#RBb);
            }
            byte num2 = 0;
            for (int i = 0; i < length; i++)
            {
                if (num2 < #mwf[i])
                {
                    num2 = #mwf[i];
                }
            }
            if ((#kwf.Length * 8) <= num2)
            {
                #sVb(#MAb.#RBb);
            }
            for (int j = 0; j < length; j++)
            {
                byte num5 = #mwf[j];
                byte num6 = (byte) (1 << ((7 - (num5 % 8)) & 0x1f));
                #kwf[num5 / 8] = ((#nwf & (1 << (j & 0x1f))) == 0L) ? ((byte) (#kwf[num5 / 8] & ~num6)) : ((byte) (#kwf[num5 / 8] | num6));
            }
        }

        private static ushort #oVb(char #cvf)
        {
            for (ushort i = 0; i < #G.#eg(0x19e5).Length; i = (ushort) (i + 1))
            {
                if (#G.#eg(0x19e5)[i] == #cvf)
                {
                    return i;
                }
            }
            return 0xffff;
        }

        private static byte #pVb(string #gBb, #OAb #EBb, byte[] #kwf)
        {
            byte[] buffer = new byte[9];
            for (int i = 0; i < 5; i++)
            {
                buffer[i] = (byte) #gBb[i];
            }
            buffer[5] = (byte) (#EBb & (#OAb.#Jog | #OAb.#cRf | #OAb.#bRf | #OAb.#nCb));
            buffer[6] = (byte) (#EBb & (#OAb.#mCb | #OAb.#iCb | #OAb.#hCb | #OAb.#Mfb));
            buffer[7] = (byte) (#EBb & (#OAb.#cxj | #OAb.#fCb | #OAb.#eCb | #OAb.#dCb));
            buffer[8] = (byte) (#EBb & (#OAb.#cCb | #OAb.#9mb | #OAb.#bCb | #OAb.#aCb));
            uint num = #kVb(#kVb(0, buffer, 9), #kwf, (uint) #kwf.Length);
            ushort num2 = (ushort) (((num & -65536) >> 0x10) ^ (num & 0xffff));
            return (byte) (((num2 & 0xff00) >> 8) ^ (num2 & 0xff));
        }

        private static void #qVb(string #gBb, byte[] #kwf)
        {
            ushort num = 11;
            uint index = 0;
            uint num3 = 0;
            int num4 = 6;
            while ((index < #kwf.Length) && (num3 < 90))
            {
                ushort num5 = #oVb(#gBb[num4++]);
                if (num5 == 0xffff)
                {
                    #sVb(#MAb.#RBb);
                }
                num5 = (ushort) (num5 << (num & 0x1f));
                #kwf[index] = (byte) (#kwf[index] | ((byte) ((num5 & 0xff00) >> 8)));
                #kwf[((int) index) + 1] = (byte) (#kwf[((int) index) + 1] | ((byte) (num5 & 0xff)));
                if (num >= 8)
                {
                    num = (ushort) (num - 5);
                }
                else
                {
                    num = (ushort) (num + 3);
                    index++;
                }
                num3 += 5;
            }
        }

        private void #rVb(byte[] #kwf)
        {
            #nVb(#kwf, #OBb, 0);
            #nVb(#kwf, #OBb, #pVb(this.#gBb, this.#EBb, #kwf));
            string str = string.Empty;
            int num = 0;
            while (num < 0x12)
            {
                byte num2 = 0;
                byte num3 = 0;
                while (true)
                {
                    if (num3 >= 5)
                    {
                        str = str + #G.#eg(0x19e5)[num2].ToString();
                        num++;
                        break;
                    }
                    int num4 = (num * 5) + num3;
                    byte num5 = (byte) (1 << ((7 - (num4 % 8)) & 0x1f));
                    if ((#kwf[num4 / 8] & num5) != 0)
                    {
                        num2 = (byte) (num2 | ((byte) Math.Pow(2.0, (double) (4 - num3))));
                    }
                    num3 = (byte) (num3 + 1);
                }
            }
            this.#gBb = this.#gBb.Substring(0, 6) + str + this.#gBb.Substring(this.#gBb.Length - 1, 1);
        }

        private static void #sVb(#MAb #dBb)
        {
            throw new LicenseException((int) #dBb);
        }

        static AlgorithmGLicenseDecryptor()
        {
            // Unresolved stack state at '00000000'
        }

        internal AlgorithmGLicenseDecryptor(string licensee, string licenseKey)
        {
            this.#fBb = licensee;
            this.#gBb = #bVb(licenseKey);
            this.#lVb();
        }

        private void Reset()
        {
            this.#EBb = #OAb.#6Bb;
            this.#iBb = 0;
            this.#jBb = 0;
            this.#hBb = AssemblyLicenseType.Invalid;
            this.#eBb = 0;
            this.#lBb = AssemblyPlatform.Invalid;
            this.#kBb = 0;
            this.#cBb = DateTime.Now;
        }

        internal DateTime Date
        {
            get => 
                this.#cBb;
            set
            {
                this.#cBb = value;
                DateTime time = new DateTime(0x7d2, 1, 1);
                byte[] buffer = new byte[12];
                #qVb(this.#gBb, buffer);
                #nVb(buffer, #MBb, (uint) (this.#cBb - time).Days);
                this.#rVb(buffer);
            }
        }

        internal byte LicenseCount =>
            this.#eBb;

        internal string Licensee =>
            this.#fBb;

        internal string LicenseKey =>
            this.#gBb;

        internal AssemblyLicenseType LicenseType =>
            this.#hBb;

        internal byte MajorVersion =>
            this.#iBb;

        internal byte MinorVersion =>
            this.#jBb;

        internal ushort OrganizationID
        {
            get => 
                this.#kBb;
            set
            {
                this.#kBb = value;
                byte[] buffer = new byte[12];
                #qVb(this.#gBb, buffer);
                #nVb(buffer, #NBb, this.#kBb);
                this.#rVb(buffer);
            }
        }

        internal AssemblyPlatform Platform =>
            this.#lBb;

        internal #OAb ProductCodes =>
            this.#EBb;

        internal #NAb UsageAllowed
        {
            get => 
                this.#FBb;
            set
            {
                this.#FBb = value;
                byte[] buffer = new byte[12];
                #qVb(this.#gBb, buffer);
                #nVb(buffer, #PBb, (uint) this.#FBb);
                this.#rVb(buffer);
            }
        }

        internal enum #MAb
        {
            #QBb = 0,
            #RBb = 1,
            #SBb = 2,
            #TBb = 3,
            #UBb = 4,
            #VBb = 5,
            #WBb = 6,
            #XBb = 7,
            #YBb = 8,
            #ZBb = 20,
            #0Bb = 0x15,
            #1Bb = 30,
            #2Bb = 40,
            #3Bb = 0x29,
            #4Bb = 0x2a
        }

        internal enum #NAb
        {
            #5Bb,
            #6Bb,
            #7Bb,
            #8Bb
        }

        [Serializable, SuppressMessage("Microsoft.Design", "CA1032:ImplementStandardExceptionConstructors", Justification="Typical constructors are not used."), SuppressMessage("Microsoft.Design", "CA1064:ExceptionsShouldBePublic", Justification="Exception is caught and handled internally.")]
        internal class LicenseException : ApplicationException
        {
            private int exceptionType;

            internal LicenseException(int exceptionType)
            {
                this.exceptionType = exceptionType;
            }

            internal int ExceptionType =>
                this.exceptionType;
        }
    }
}

