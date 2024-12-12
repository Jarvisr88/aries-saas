namespace ActiproSoftware.Products
{
    using #H;
    using System;
    using System.ComponentModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;

    [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId="Actipro")]
    public class ActiproLicense : License
    {
        private ActiproSoftware.Products.AssemblyInfo #bBb;
        private DateTime #cBb;
        private int #dBb;
        private byte #eBb;
        private string #fBb;
        private string #gBb;
        private AssemblyLicenseType #hBb;
        private byte #iBb;
        private byte #jBb;
        private ushort #kBb;
        private AssemblyPlatform #lBb;
        private uint #mBb;
        private ActiproLicenseSourceLocation #Ymb;

        private static string #bVb(string #gBb) => 
            #gBb?.Replace(#G.#eg(0xc0e), string.Empty).Replace(#G.#eg(0x5c), string.Empty);

        private static string #dVb(string #gBb)
        {
            if ((#gBb == null) || (#gBb.Length < 0x15))
            {
                return #gBb;
            }
            string[] textArray1 = new string[9];
            textArray1[0] = #gBb.Substring(0, 6);
            textArray1[1] = #G.#eg(0x5c);
            textArray1[2] = #gBb.Substring(6, 5);
            textArray1[3] = #G.#eg(0x5c);
            textArray1[4] = #gBb.Substring(11, 5);
            textArray1[5] = #G.#eg(0x5c);
            textArray1[6] = #gBb.Substring(0x10, 5);
            textArray1[7] = #G.#eg(0x5c);
            textArray1[8] = #gBb.Substring(0x15);
            return string.Concat(textArray1);
        }

        private void #gVb()
        {
            if (!this.#gBb.StartsWith(#G.#eg(0xfab), StringComparison.OrdinalIgnoreCase))
            {
                this.#dBb = 100;
            }
            else
            {
                Version assemblyVersion = this.#bBb.GetAssemblyVersion();
                if ((this.#iBb != assemblyVersion.Major) || (this.#jBb != assemblyVersion.Minor))
                {
                    this.#dBb = 0x65;
                }
                else if ((this.#hBb != this.#bBb.LicenseType) && ((this.#hBb != AssemblyLicenseType.Evaluation) || (this.#bBb.LicenseType != AssemblyLicenseType.Full)))
                {
                    this.#dBb = 0x66;
                }
                else if ((this.#lBb != this.#bBb.Platform) && (this.#bBb.Platform != AssemblyPlatform.Independent))
                {
                    this.#dBb = 0x67;
                }
                else if ((this.#hBb <= AssemblyLicenseType.Prerelease) && ((DateTime.Now > this.#cBb) || (this.#kBb == 0)))
                {
                    this.#dBb = 50;
                }
            }
        }

        internal ActiproLicense(ActiproSoftware.Products.AssemblyInfo assemblyInfo, string licensee, string licenseKey, ActiproLicenseSourceLocation sourceLocation, int exceptionType)
        {
            this.#bBb = assemblyInfo;
            this.#fBb = licensee;
            this.#gBb = #bVb(licenseKey);
            this.#Ymb = sourceLocation;
            this.#dBb = exceptionType;
        }

        internal ActiproLicense(ActiproSoftware.Products.AssemblyInfo assemblyInfo, string licensee, string licenseKey, ActiproLicenseSourceLocation sourceLocation, int exceptionType, uint productIDs, byte majorVersion, byte minorVersion, AssemblyLicenseType licenseType, byte licenseCount, AssemblyPlatform platform, ushort organizationID, DateTime date) : this(assemblyInfo, licensee, licenseKey, sourceLocation, exceptionType)
        {
            this.#mBb = productIDs;
            this.#iBb = majorVersion;
            this.#jBb = minorVersion;
            this.#hBb = licenseType;
            this.#eBb = licenseCount;
            this.#lBb = platform;
            this.#kBb = organizationID;
            this.#cBb = date;
            if (exceptionType == 0)
            {
                this.#gVb();
            }
        }

        public sealed override void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.#fBb = null;
                this.#gBb = null;
            }
        }

        ~ActiproLicense()
        {
            this.Dispose(false);
        }

        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        public string GetDetails()
        {
            string text1;
            string[] textArray3 = new string[20];
            string[] textArray4 = new string[20];
            textArray4[0] = #G.#eg(0xfb0);
            string[] textArray5 = textArray3;
            if (this.#bBb == null)
            {
                text1 = null;
            }
            else
            {
                string[] textArray1 = new string[6];
                string[] textArray2 = new string[6];
                textArray2[0] = this.#bBb.Product;
                string[] local1 = textArray2;
                local1[1] = #G.#eg(0xc0e);
                local1[2] = this.#bBb.Version;
                local1[3] = #G.#eg(0xfbd);
                local1[4] = this.#bBb.LicenseType.ToString();
                local1[5] = #G.#eg(0xfc2);
                text1 = string.Concat(local1);
            }
            textArray5[1] = text1;
            string[] local2 = textArray5;
            local2[2] = Environment.NewLine;
            local2[3] = #G.#eg(0xfc7);
            local2[4] = this.#hBb.ToString();
            local2[5] = Environment.NewLine;
            local2[6] = #G.#eg(0xfdc);
            local2[7] = this.#fBb;
            local2[8] = Environment.NewLine;
            local2[9] = #G.#eg(0xfed);
            local2[10] = ((this.#gBb == null) || (this.#gBb.Length <= 20)) ? this.#gBb : (this.ExpandedLicenseKey.Substring(0, 0x12) + #G.#eg(0x1002));
            string[] local3 = local2;
            local3[11] = Environment.NewLine;
            local3[12] = #G.#eg(0x1027);
            local3[13] = this.#lBb.ToString();
            local3[14] = Environment.NewLine;
            local3[15] = #G.#eg(0x1038);
            local3[0x10] = this.#kBb.ToString();
            local3[0x11] = Environment.NewLine;
            local3[0x12] = #G.#eg(0x1041);
            local3[0x13] = this.#dBb.ToString();
            return string.Concat(local3);
        }

        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        public string GetQuickInfo()
        {
            int exceptionType = this.ExceptionType;
            if (exceptionType <= 1)
            {
                if (exceptionType == 0)
                {
                    if (this.#hBb != AssemblyLicenseType.Full)
                    {
                        return #G.#eg(0x10d4);
                    }
                    object[] args = new object[] { this.#fBb, this.IsSiteLicense ? #G.#eg(0x10c7) : this.#eBb.ToString(CultureInfo.InvariantCulture) };
                    return string.Format(CultureInfo.CurrentCulture, #G.#eg(0x1052), args);
                }
                if (exceptionType == 1)
                {
                    return string.Format(CultureInfo.CurrentCulture, #G.#eg(0x151c), new object[0]);
                }
            }
            else
            {
                switch (exceptionType)
                {
                    case 40:
                        return string.Format(CultureInfo.CurrentCulture, #G.#eg(0x13cc), new object[0]);

                    case 0x29:
                        return string.Format(CultureInfo.CurrentCulture, #G.#eg(0x118a), new object[0]);

                    case 0x2a:
                        return string.Format(CultureInfo.CurrentCulture, #G.#eg(0x146e), new object[0]);

                    default:
                    {
                        if (exceptionType != 50)
                        {
                            break;
                        }
                        if ((this.#bBb.LicenseType - 1) <= AssemblyLicenseType.Beta)
                        {
                            object[] objArray2 = new object[] { this.ExpirationDate };
                            return string.Format(CultureInfo.CurrentCulture, #G.#eg(0x1220), objArray2);
                        }
                        object[] args = new object[] { this.ExpirationDate };
                        return string.Format(CultureInfo.CurrentCulture, #G.#eg(0x12ae), args);
                    }
                }
            }
            return (((this.#gBb == null) || (this.#gBb.Length <= 0)) ? string.Format(CultureInfo.CurrentCulture, #G.#eg(0x1644), new object[0]) : string.Format(CultureInfo.CurrentCulture, #G.#eg(0x15aa), new object[0]));
        }

        public void SetExceptionType(int value)
        {
            if (value > 0)
            {
                this.#dBb = value;
            }
        }

        internal ActiproSoftware.Products.AssemblyInfo AssemblyInfo =>
            this.#bBb;

        internal int ExceptionType =>
            this.#dBb;

        internal ushort OrganizationID =>
            this.#kBb;

        internal AssemblyPlatform Platform =>
            this.#lBb;

        internal uint ProductIDs =>
            this.#mBb;

        public DateTime ExpirationDate =>
            (this.#hBb != AssemblyLicenseType.Full) ? this.#cBb : DateTime.MaxValue;

        public string ExpandedLicenseKey
        {
            get
            {
                string text1 = this.#gBb;
                if (this.#gBb == null)
                {
                    string local1 = this.#gBb;
                    text1 = string.Empty;
                }
                return #dVb(text1);
            }
        }

        public bool IsUnlicensedProduct =>
            this.#dBb == 0x2a;

        public bool IsValid =>
            this.#dBb == 0;

        public bool IsSiteLicense =>
            this.#eBb == 0xff;

        public byte LicenseCount =>
            this.#eBb;

        public string Licensee =>
            this.#fBb;

        public override string LicenseKey =>
            this.#gBb;

        public AssemblyLicenseType LicenseType =>
            this.#hBb;

        public byte MajorVersion =>
            this.#iBb;

        public byte MinorVersion =>
            this.#jBb;

        public ActiproLicenseSourceLocation SourceLocation =>
            this.#Ymb;
    }
}

