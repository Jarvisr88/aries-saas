namespace DevExpress.Utils.Helpers
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Reflection;
    using System.Reflection.Emit;

    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
    public static class BrushGuard
    {
        private static readonly Func<Brush, IntPtr> nativeBrushAccessor = EmitNativeBrushAccessor();

        private static Func<Brush, IntPtr> EmitNativeBrushAccessor()
        {
            Type[] parameterTypes = new Type[] { typeof(Brush) };
            return NativeField<Brush>.EmitAccessor(NativeField<Brush>.Ensure("nativeBrush"), new DynamicMethod("__get_nativeBrush", MethodAttributes.Static | MethodAttributes.Public, CallingConventions.Standard, typeof(IntPtr), parameterTypes, typeof(BrushGuard).Module, true), null);
        }

        public static bool IsDisposedOrInvalid(Brush brush) => 
            Equals(IntPtr.Zero, nativeBrushAccessor(brush));
    }
}

