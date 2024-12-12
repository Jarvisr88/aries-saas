namespace DevExpress.Utils.Text
{
    using DevExpress.Utils.Helpers;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Reflection;
    using System.Reflection.Emit;

    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
    public static class StringFormatGuard
    {
        private static readonly Func<StringFormat, IntPtr> nativeFormatAccessor = EmitNativeFormatAccessor();

        private static Func<StringFormat, IntPtr> EmitNativeFormatAccessor()
        {
            Type[] parameterTypes = new Type[] { typeof(StringFormat) };
            return NativeField<StringFormat>.EmitAccessor(NativeField<StringFormat>.Ensure("nativeFormat"), new DynamicMethod("__get_nativeFormat", MethodAttributes.Static | MethodAttributes.Public, CallingConventions.Standard, typeof(IntPtr), parameterTypes, typeof(StringFormatGuard).Module, true), null);
        }

        public static bool IsDisposedOrInvalid(StringFormat format) => 
            Equals(IntPtr.Zero, nativeFormatAccessor(format));
    }
}

