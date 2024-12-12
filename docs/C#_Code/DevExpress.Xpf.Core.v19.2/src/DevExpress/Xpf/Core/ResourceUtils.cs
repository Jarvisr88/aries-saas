namespace DevExpress.Xpf.Core
{
    using System;
    using System.Text;

    internal static class ResourceUtils
    {
        private static string assemblyShortName;

        public static Uri MakeUri(string relativeFile)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("pack://application:,,,");
            builder.Append($"/{AssemblyShortName};component/{relativeFile}");
            return new Uri(builder.ToString(), UriKind.RelativeOrAbsolute);
        }

        private static string AssemblyShortName
        {
            get
            {
                if (assemblyShortName == null)
                {
                    char[] separator = new char[] { ',' };
                    assemblyShortName = typeof(ResourceUtils).Assembly.ToString().Split(separator)[0];
                }
                return assemblyShortName;
            }
        }
    }
}

