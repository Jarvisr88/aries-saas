namespace DevExpress.Utils.Helpers
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Reflection;
    using System.Reflection.Emit;

    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
    public static class GraphicsGuard
    {
        private static readonly Func<Graphics, IntPtr> nativeGraphicsAccessor = EmitNativeGraphicsAccessor();

        private static Func<Graphics, IntPtr> EmitNativeGraphicsAccessor()
        {
            Type[] parameterTypes = new Type[] { typeof(Graphics) };
            return NativeField<Graphics>.EmitAccessor(NativeField<Graphics>.Ensure("nativeGraphics", "NativeGraphics"), new DynamicMethod("__get_nativeGraphics", MethodAttributes.Static | MethodAttributes.Public, CallingConventions.Standard, typeof(IntPtr), parameterTypes, typeof(GraphicsGuard).Module, true), "NativeGraphics");
        }

        public static bool IsDisposedOrInvalid(Graphics g) => 
            Equals(IntPtr.Zero, nativeGraphicsAccessor(g));
    }
}

