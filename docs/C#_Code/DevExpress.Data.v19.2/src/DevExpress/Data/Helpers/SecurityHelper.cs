namespace DevExpress.Data.Helpers
{
    using System;
    using System.Security;

    public static class SecurityHelper
    {
        private static bool forcePartialTrustMode;
        [ThreadStatic]
        private static PermissionCheckerSet permissionCheckerSet;

        public static void ForceRecheckPermissions();
        public static bool IsPermissionGranted(IPermission permission);

        public static bool IsPartialTrust { get; }

        public static bool ForcePartialTrustMode { get; set; }

        public static bool IsWeakRefAvailable { get; }

        public static bool IsUnmanagedCodeGrantedAndHasZeroHwnd { get; }

        public static bool IsUnmanagedCodeGrantedAndCanUseGetHdc { get; }

        public static bool IsUnmanagedCodeGranted { get; }
    }
}

