namespace DevExpress.Utils.Text
{
    using DevExpress.Utils.Helpers;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Reflection;
    using System.Reflection.Emit;

    public static class FontGuard
    {
        private static readonly Func<Font, IntPtr> nativeFontAccessor = EmitNativeFontAccessor();

        private static Func<Font, IntPtr> EmitNativeFontAccessor()
        {
            Type[] parameterTypes = new Type[] { typeof(Font) };
            return NativeField<Font>.EmitAccessor(NativeField<Font>.Ensure("nativeFont", "NativeFont"), new DynamicMethod("__get_nativeFont", MethodAttributes.Static | MethodAttributes.Public, CallingConventions.Standard, typeof(IntPtr), parameterTypes, typeof(FontGuard).Module, true), "NativeFont");
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public static IntPtr GetHandle(Font font) => 
            nativeFontAccessor(font);

        public static bool IsDisposedOrInvalid(Font font) => 
            Equals(IntPtr.Zero, nativeFontAccessor(font));
    }
}

