namespace DevExpress.Xpf.Core.Utils
{
    using DevExpress.Data.Utils;
    using DevExpress.Xpf.Editors.Helpers;
    using System;
    using System.Runtime.CompilerServices;

    public static class NetVersionDetector
    {
        public static bool IsNet452() => 
            Net45Detector.IsNet45();

        public static bool IsNet452(this object This) => 
            IsNet452();

        public static bool IsNet462() => 
            Net462Detector.IsNet462();

        public static bool IsNet462(this object This) => 
            IsNet462();

        public static bool IsNetCore3() => 
            FrameworkVersions.IsNetCore3AndAbove();

        public static bool IsNetCore3(this object This) => 
            IsNetCore3();
    }
}

