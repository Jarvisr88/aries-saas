namespace ActiproSoftware.Products
{
    using #H;
    using #PAb;
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Drawing;
    using System.Globalization;
    using System.IO;
    using System.Reflection;
    using System.Windows.Forms;

    public abstract class AssemblyInfo
    {
        protected AssemblyInfo()
        {
        }

        public System.Version GetAssemblyVersion()
        {
            AssemblyInformationalVersionAttribute customAttribute = Attribute.GetCustomAttribute(this.Assembly, System.Type.GetTypeFromHandle(typeof(AssemblyInformationalVersionAttribute).TypeHandle)) as AssemblyInformationalVersionAttribute;
            return ((customAttribute != null) ? GetAssemblyVersion(customAttribute.InformationalVersion) : new System.Version(0, 0, 0, 0));
        }

        [SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults", MessageId="System.Int32.TryParse(System.String,System.Int32@)")]
        internal static System.Version GetAssemblyVersion(string #7gb)
        {
            int num2;
            int result = 1;
            if (0 != 0)
            {
            }
            else
            {
                num2 = 0;
            }
            int num3 = 0;
            int num4 = 0;
            if (#7gb != null)
            {
                char[] separator = new char[] { '.' };
                string[] strArray = #7gb.Split(separator);
                if ((strArray.Length >= 1) && (int.TryParse(strArray[0], out result) && ((strArray.Length >= 2) && (int.TryParse(strArray[1], out num2) && ((strArray.Length >= 3) && (int.TryParse(strArray[2], out num3) && (strArray.Length >= 4)))))))
                {
                    int.TryParse(strArray[3], out num4);
                }
            }
            return new System.Version(result, num2, num3, num4);
        }

        public Cursor GetCursor(string name)
        {
            string str = this.GetType().Namespace + #G.#eg(0x1a12) + name;
            Stream manifestResourceStream = base.GetType().Assembly.GetManifestResourceStream(str);
            return ((manifestResourceStream != null) ? new Cursor(manifestResourceStream) : null);
        }

        public Icon GetIcon(string name)
        {
            string str = this.GetType().Namespace + #G.#eg(0x1a1f) + name;
            Stream manifestResourceStream = base.GetType().Assembly.GetManifestResourceStream(str);
            return ((manifestResourceStream != null) ? new Icon(manifestResourceStream) : null);
        }

        public Image GetImage(string name)
        {
            string str = this.GetType().Namespace + #G.#eg(0x1a1f) + name;
            Stream manifestResourceStream = base.GetType().Assembly.GetManifestResourceStream(str);
            return ((manifestResourceStream != null) ? Image.FromStream(manifestResourceStream) : null);
        }

        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        public void ShowLicenseWindow(ActiproLicense license)
        {
            #Rqe rqe1 = new #Rqe(license);
            #Rqe rqe2 = new #Rqe(license);
            DialogResult local1 = rqe2.ShowDialog();
            rqe2.Dispose();
        }

        public System.Reflection.Assembly Assembly =>
            this.GetType().Assembly;

        public string Copyright
        {
            get
            {
                AssemblyCopyrightAttribute customAttribute = Attribute.GetCustomAttribute(this.Assembly, System.Type.GetTypeFromHandle(typeof(AssemblyCopyrightAttribute).TypeHandle)) as AssemblyCopyrightAttribute;
                return customAttribute?.Copyright;
            }
        }

        public string Description
        {
            get
            {
                AssemblyDescriptionAttribute customAttribute = Attribute.GetCustomAttribute(this.Assembly, System.Type.GetTypeFromHandle(typeof(AssemblyDescriptionAttribute).TypeHandle)) as AssemblyDescriptionAttribute;
                return customAttribute?.Description;
            }
        }

        public abstract AssemblyLicenseType LicenseType { get; }

        public abstract AssemblyPlatform Platform { get; }

        public string Product
        {
            get
            {
                AssemblyProductAttribute customAttribute = Attribute.GetCustomAttribute(this.Assembly, System.Type.GetTypeFromHandle(typeof(AssemblyProductAttribute).TypeHandle)) as AssemblyProductAttribute;
                return customAttribute?.Product;
            }
        }

        public abstract string ProductCode { get; }

        public abstract int ProductId { get; }

        public string Title
        {
            get
            {
                AssemblyTitleAttribute customAttribute = Attribute.GetCustomAttribute(this.Assembly, System.Type.GetTypeFromHandle(typeof(AssemblyTitleAttribute).TypeHandle)) as AssemblyTitleAttribute;
                return customAttribute?.Title;
            }
        }

        public string Version
        {
            get
            {
                System.Version assemblyVersion = this.GetAssemblyVersion();
                object[] args = new object[] { assemblyVersion.Major, assemblyVersion.Minor, assemblyVersion.Build };
                return string.Format(CultureInfo.InvariantCulture, #G.#eg(0x1a2c), args);
            }
        }
    }
}

