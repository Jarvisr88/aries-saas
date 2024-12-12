namespace DevExpress.Mvvm.Native
{
    using DevExpress.Mvvm;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Resources;
    using System.Security;

    [SecuritySafeCritical]
    public class DataAnnotationsResourcesResolver
    {
        private static ResourceManager annotationsResourceManager;

        private static string GetResourceString(string resourceName) => 
            AnnotationsResourceManager.GetString(resourceName) ?? DataAnnotationsResources.ResourceManager.GetString(resourceName);

        internal static ResourceManager AnnotationsResourceManager
        {
            get
            {
                annotationsResourceManager ??= (!typeof(ValidationAttribute).Assembly.FullName.StartsWith("System.ComponentModel.DataAnnotations,") ? new ResourceManager("FxResources.System.ComponentModel.Annotations.SR", typeof(ValidationAttribute).Assembly) : new ResourceManager("System.ComponentModel.DataAnnotations.Resources.DataAnnotationsResources", typeof(ValidationAttribute).Assembly));
                return annotationsResourceManager;
            }
        }

        public static string MinLengthAttribute_ValidationError =>
            GetResourceString("MinLengthAttribute_ValidationError");

        public static string MinLengthAttribute_InvalidMinLength =>
            GetResourceString("MinLengthAttribute_InvalidMinLength");

        public static string MaxLengthAttribute_InvalidMaxLength =>
            GetResourceString("MaxLengthAttribute_InvalidMaxLength");

        public static string MaxLengthAttribute_ValidationError =>
            GetResourceString("MaxLengthAttribute_ValidationError");

        public static string PhoneAttribute_Invalid =>
            GetResourceString("PhoneAttribute_Invalid");

        public static string CreditCardAttribute_Invalid =>
            GetResourceString("CreditCardAttribute_Invalid");

        public static string EmailAddressAttribute_Invalid =>
            GetResourceString("EmailAddressAttribute_Invalid");

        public static string UrlAttribute_Invalid =>
            GetResourceString("UrlAttribute_Invalid");

        public static string RangeAttribute_ValidationError =>
            GetResourceString("RangeAttribute_ValidationError");

        public static string RegexAttribute_ValidationError =>
            GetResourceString("RegexAttribute_ValidationError");

        public static string CustomValidationAttribute_ValidationError =>
            GetResourceString("CustomValidationAttribute_ValidationError");

        public static string RequiredAttribute_ValidationError =>
            GetResourceString("RequiredAttribute_ValidationError");
    }
}

