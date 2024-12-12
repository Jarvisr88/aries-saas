namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.IO;
    using System.Reflection;

    internal static class SystemWebAssemblyLoader
    {
        private static readonly Assembly systemWeb = Load("System.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");

        private static Assembly Load(string name)
        {
            try
            {
                return Assembly.Load(name);
            }
            catch (FileNotFoundException)
            {
                return null;
            }
            catch (FileLoadException)
            {
                return null;
            }
        }

        public static Assembly SystemWeb =>
            systemWeb;
    }
}

