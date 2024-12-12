namespace DevExpress.Xpf.Bars
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Windows;

    public class CloneNameHelper
    {
        public static readonly DependencyProperty SourceNameProperty;
        private static Dictionary<string, int> clonedNames;
        private static Dictionary<CloneNameHelper.CloneNameData, string> generatedNames;

        static CloneNameHelper();
        public static string GetCloneName(IFrameworkInputElement source, IFrameworkInputElement target);
        private static string GetNameSubstring(string name, ref int index);
        public static string GetSourceName(DependencyObject obj);
        public static void Register(string nameString);
        public static void SetSourceName(DependencyObject obj, string value);

        [StructLayout(LayoutKind.Sequential)]
        private struct CloneNameData
        {
            private readonly WeakReference sourceWR;
            private readonly WeakReference targetWR;
            private readonly int hashCode;
            public CloneNameData(object source, object target);
            private int GetHashCodeImpl();
            public override int GetHashCode();
            public override bool Equals(object obj);
            public bool IsAlive();
        }
    }
}

