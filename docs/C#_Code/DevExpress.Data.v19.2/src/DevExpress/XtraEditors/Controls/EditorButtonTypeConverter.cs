namespace DevExpress.XtraEditors.Controls
{
    using DevExpress.Utils.Design;
    using System;
    using System.Reflection;

    public class EditorButtonTypeConverter : UniversalTypeConverter
    {
        private static bool ConstructorAllowed(ConstructorInfo cInfo);
        protected override ConstructorInfo[] FilterConstructors(ConstructorInfo[] ctors);

        protected override bool AllowBinaryType { get; }
    }
}

