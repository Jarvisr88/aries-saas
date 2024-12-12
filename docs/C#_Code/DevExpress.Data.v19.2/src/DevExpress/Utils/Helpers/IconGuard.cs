namespace DevExpress.Utils.Helpers
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Reflection;
    using System.Reflection.Emit;

    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
    public static class IconGuard
    {
        private static readonly Func<Icon, IntPtr> iconHandleAccessor = EmitIconHandleAccessor();

        private static Func<Icon, IntPtr> EmitIconHandleAccessor()
        {
            Type[] parameterTypes = new Type[] { typeof(Icon) };
            return NativeField<Icon>.EmitAccessor(NativeField<Icon>.Ensure("handle"), new DynamicMethod("__get_handle", MethodAttributes.Static | MethodAttributes.Public, CallingConventions.Standard, typeof(IntPtr), parameterTypes, typeof(IconGuard).Module, true), null);
        }

        public static bool IsDisposedOrInvalid(Icon icon) => 
            Equals(IntPtr.Zero, iconHandleAccessor(icon));
    }
}

