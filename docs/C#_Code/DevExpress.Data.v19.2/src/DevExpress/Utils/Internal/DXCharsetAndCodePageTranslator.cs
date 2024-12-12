namespace DevExpress.Utils.Internal
{
    using DevExpress.Data.Helpers;
    using System;
    using System.Security.Permissions;

    public abstract class DXCharsetAndCodePageTranslator
    {
        private static DXCharsetAndCodePageTranslator instance;

        protected DXCharsetAndCodePageTranslator()
        {
        }

        public abstract int CharsetFromCodePage(int codePage);
        public static void ClearInstance()
        {
            instance = null;
        }

        public abstract int CodePageFromCharset(int charset);
        private static DXCharsetAndCodePageTranslator CreateInstance() => 
            !SecurityHelper.IsPermissionGranted(new SecurityPermission(SecurityPermissionFlag.UnmanagedCode)) ? ((DXCharsetAndCodePageTranslator) new PartialTrustCharsetAndCodePageTranslator()) : ((DXCharsetAndCodePageTranslator) new FullTrustCharsetAndCodePageTranslator());

        public static DXCharsetAndCodePageTranslator Instance
        {
            get
            {
                instance ??= CreateInstance();
                return instance;
            }
        }
    }
}

