namespace DevExpress.Utils.Helpers
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Reflection;
    using System.Reflection.Emit;

    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
    public static class ImageGuard
    {
        private static readonly Func<Image, IntPtr> nativeImageAccessor = EmitNativeImageAccessor();

        private static Func<Image, IntPtr> EmitNativeImageAccessor()
        {
            Type[] parameterTypes = new Type[] { typeof(Image) };
            return NativeField<Image>.EmitAccessor(NativeField<Image>.Ensure("nativeImage"), new DynamicMethod("__get_nativeImage", MethodAttributes.Static | MethodAttributes.Public, CallingConventions.Standard, typeof(IntPtr), parameterTypes, typeof(ImageGuard).Module, true), null);
        }

        public static bool IsDisposedOrInvalid(Image image) => 
            Equals(IntPtr.Zero, nativeImageAccessor(image));
    }
}

