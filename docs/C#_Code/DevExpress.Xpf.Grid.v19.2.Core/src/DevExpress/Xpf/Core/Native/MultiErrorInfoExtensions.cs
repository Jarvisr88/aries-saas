namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.CompilerServices;

    internal static class MultiErrorInfoExtensions
    {
        public static bool HasErrors(this MultiErrorInfo multiErrorInfo) => 
            (multiErrorInfo != null) && (multiErrorInfo.Errors.Length != 0);
    }
}

