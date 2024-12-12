namespace DevExpress.Data.Linq
{
    using System;
    using System.Collections.Generic;

    internal static class AttachedProperty
    {
        private static Dictionary<WeakReference, Dictionary<string, object>> Values;

        static AttachedProperty();
        public static object GetAttachedProperty(object o, string name);
        private static WeakReference GetKey(object o);
        public static void SetAttachedProperty(object o, string name, object value);
    }
}

