namespace DevExpress.Utils
{
    using System;
    using System.ComponentModel;

    public class UrlAccessSecurityLevelSetting
    {
        [EditorBrowsable(EditorBrowsableState.Never), Obsolete("Use the DevExpress.Utils.Url.UriCreator.CreateUri method instead.")]
        public static Uri CreateUriFromCustomRegistration(string path)
        {
            throw new NotSupportedException();
        }

        [EditorBrowsable(EditorBrowsableState.Never), Obsolete("Use the DevExpress.Security.Resources.AccessSettings.StaticResources.TrySetRules method instead.")]
        public static bool RegisterCustomBaseDirectories(params string[] baseDirectories)
        {
            throw new NotSupportedException();
        }

        [EditorBrowsable(EditorBrowsableState.Never), Obsolete("Use the DevExpress.Security.Resources.AccessSettings.StaticResources.TrySetRules method instead.")]
        public static bool RegisterCustomCallback(Func<string, Uri> customCallback)
        {
            throw new NotSupportedException();
        }

        [EditorBrowsable(EditorBrowsableState.Never), Obsolete("Use the DevExpress.Security.Resources.AccessSettings.StaticResources.TrySetRules method instead.")]
        public static bool SafeSetSecurityLevel(UrlAccessSecurityLevel value)
        {
            throw new NotSupportedException();
        }

        [EditorBrowsable(EditorBrowsableState.Never), Obsolete("Use the DevExpress.Security.Resources.AccessSettings.StaticResources.TrySetRules method instead.")]
        public static UrlAccessSecurityLevel SecurityLevel
        {
            get => 
                UrlAccessSecurityLevel.Unrestricted;
            set
            {
                throw new NotSupportedException();
            }
        }
    }
}

