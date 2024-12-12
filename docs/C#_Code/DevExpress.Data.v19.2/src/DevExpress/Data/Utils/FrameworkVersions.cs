namespace DevExpress.Data.Utils
{
    using System;
    using System.Reflection;
    using System.Security;

    public static class FrameworkVersions
    {
        private static readonly bool Matrix_nativeMatrix_FieldExists;
        private static readonly bool NativeWindow_createWindowSyncObject_FieldRenamed;

        static FrameworkVersions();
        [SecuritySafeCritical]
        private static FieldInfo GetCreateWindowSyncObject();
        [SecuritySafeCritical]
        private static FieldInfo GetNativeMatrix();
        public static bool IsFullFramework();
        public static bool IsNetCore3AndAbove();
        public static bool IsNetCore5AndAbove();
    }
}

