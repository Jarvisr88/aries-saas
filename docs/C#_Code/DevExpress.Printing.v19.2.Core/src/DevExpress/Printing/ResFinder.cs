namespace DevExpress.Printing
{
    using System;
    using System.IO;
    using System.Reflection;

    public class ResFinder
    {
        private static string GetFullName(string name) => 
            Namespace + "." + name;

        public static Stream GetManifestResourceStream(string name) => 
            Assembly.GetManifestResourceStream(GetFullName(name));

        private static string Namespace =>
            typeof(ResFinder).Namespace;

        public static System.Reflection.Assembly Assembly =>
            typeof(ResFinder).Assembly;
    }
}

