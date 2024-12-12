namespace DevExpress.XtraPrinting.Native.DrillDown
{
    using System;
    using System.Runtime;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct DrillDownKey
    {
        public static readonly DrillDownKey Empty;
        private int hashCode;
        private string[] subkeys;
        static DrillDownKey();
        public static bool IsNullOrEmpty(object key);
        public static void EnsureStaticConstructor();
        public static bool operator ==(DrillDownKey left, DrillDownKey right);
        [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        public static bool operator !=(DrillDownKey left, DrillDownKey right);
        [Obsolete("Use the DrillDownKey(params string[] items) constructor instead.")]
        public DrillDownKey(string name, int level, int[] indices);
        public DrillDownKey(params string[] subkeys);
        public override string ToString();
        public static DrillDownKey Parse(string s);
        public override int GetHashCode();
        public override bool Equals(object obj);
        private bool EqualsCore(DrillDownKey other);
    }
}

