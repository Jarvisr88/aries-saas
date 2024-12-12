namespace DevExpress.XtraPrinting.XamlExport
{
    using DevExpress.Utils;
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public static class FontResolver
    {
        public static event EventHandler<FamilyNameResolvedEventArgs> FamilyNameResolved;

        private static void RaiseFamilyNameResolved(ref string familyName)
        {
            if (FamilyNameResolved != null)
            {
                FamilyNameResolvedEventArgs e = new FamilyNameResolvedEventArgs(familyName);
                FamilyNameResolved(null, e);
                familyName = e.FamilyName;
            }
        }

        internal static string ResolveEnglishFamilyName(Font font)
        {
            string englishFamilyName = FontHelper.GetEnglishFamilyName(font);
            RaiseFamilyNameResolved(ref englishFamilyName);
            return englishFamilyName;
        }

        internal static string ResolveFamilyName(Font font)
        {
            string familyName = FontHelper.GetFamilyName(font);
            RaiseFamilyNameResolved(ref familyName);
            return familyName;
        }

        public class FamilyNameResolvedEventArgs : EventArgs
        {
            public FamilyNameResolvedEventArgs(string familyName)
            {
                this.FamilyName = familyName;
            }

            public string FamilyName { get; set; }
        }
    }
}

