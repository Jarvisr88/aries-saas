namespace DevExpress.Mvvm
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Globalization;
    using System.Resources;
    using System.Runtime.CompilerServices;

    [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0"), DebuggerNonUserCode, CompilerGenerated]
    internal class DataAnnotationsResources
    {
        private static System.Resources.ResourceManager resourceMan;
        private static CultureInfo resourceCulture;

        internal DataAnnotationsResources()
        {
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static System.Resources.ResourceManager ResourceManager
        {
            get
            {
                resourceMan ??= new System.Resources.ResourceManager("DevExpress.Mvvm.DataAnnotationsResources", typeof(DataAnnotationsResources).Assembly);
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

        internal static string CreditCardAttribute_Invalid =>
            ResourceManager.GetString("CreditCardAttribute_Invalid", resourceCulture);

        internal static string EmailAddressAttribute_Invalid =>
            ResourceManager.GetString("EmailAddressAttribute_Invalid", resourceCulture);

        internal static string MaxLengthAttribute_InvalidMaxLength =>
            ResourceManager.GetString("MaxLengthAttribute_InvalidMaxLength", resourceCulture);

        internal static string MaxLengthAttribute_ValidationError =>
            ResourceManager.GetString("MaxLengthAttribute_ValidationError", resourceCulture);

        internal static string MinLengthAttribute_InvalidMinLength =>
            ResourceManager.GetString("MinLengthAttribute_InvalidMinLength", resourceCulture);

        internal static string MinLengthAttribute_ValidationError =>
            ResourceManager.GetString("MinLengthAttribute_ValidationError", resourceCulture);

        internal static string PhoneAttribute_Invalid =>
            ResourceManager.GetString("PhoneAttribute_Invalid", resourceCulture);

        internal static string RangeAttribute_ValidationError =>
            ResourceManager.GetString("RangeAttribute_ValidationError", resourceCulture);

        internal static string RegexAttribute_ValidationError =>
            ResourceManager.GetString("RegexAttribute_ValidationError", resourceCulture);

        internal static string UrlAttribute_Invalid =>
            ResourceManager.GetString("UrlAttribute_Invalid", resourceCulture);
    }
}

