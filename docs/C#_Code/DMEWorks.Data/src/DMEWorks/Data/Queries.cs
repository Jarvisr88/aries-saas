namespace DMEWorks.Data
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Globalization;
    using System.Resources;
    using System.Runtime.CompilerServices;

    [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0"), DebuggerNonUserCode, CompilerGenerated]
    internal class Queries
    {
        private static System.Resources.ResourceManager resourceMan;
        private static CultureInfo resourceCulture;

        internal Queries()
        {
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static System.Resources.ResourceManager ResourceManager
        {
            get
            {
                resourceMan ??= new System.Resources.ResourceManager("DMEWorks.Data.Queries", typeof(Queries).Assembly);
                return resourceMan;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static CultureInfo Culture
        {
            get => 
                resourceCulture;
            set => 
                resourceCulture = value;
        }

        internal static string InsuranceCompanyDelete =>
            ResourceManager.GetString("InsuranceCompanyDelete", resourceCulture);

        internal static string InsuranceCompanyInsert =>
            ResourceManager.GetString("InsuranceCompanyInsert", resourceCulture);

        internal static string InsuranceCompanySelect =>
            ResourceManager.GetString("InsuranceCompanySelect", resourceCulture);

        internal static string InsuranceCompanyUpdate =>
            ResourceManager.GetString("InsuranceCompanyUpdate", resourceCulture);
    }
}

