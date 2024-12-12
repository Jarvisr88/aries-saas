namespace DevExpress.Security.Resources
{
    using DevExpress.Utils;
    using System;
    using System.IO;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public sealed class DirectoryAccessRule : UriAccessRule
    {
        private string[] directories;

        public DirectoryAccessRule(AccessPermission permission) : base(permission)
        {
        }

        public DirectoryAccessRule(AccessPermission permission, params string[] directories) : base(permission)
        {
            Guard.ArgumentNotNull(directories, "directories");
            Func<string, bool> predicate = <>c.<>9__3_0;
            if (<>c.<>9__3_0 == null)
            {
                Func<string, bool> local1 = <>c.<>9__3_0;
                predicate = <>c.<>9__3_0 = x => !Path.IsPathRooted(x);
            }
            string str = directories.FirstOrDefault<string>(predicate);
            if (str != null)
            {
                throw new InvalidOperationException("Directory '" + str + "' is not rooted.");
            }
            this.directories = directories;
        }

        public static DirectoryAccessRule Allow(params string[] directories) => 
            (directories.Length != 0) ? new DirectoryAccessRule(AccessPermission.Allow, directories) : new DirectoryAccessRule(AccessPermission.Allow);

        protected override bool CheckUriCore(Uri uri)
        {
            string str;
            return (TryGetScheme(uri, out str) && ((str == Uri.UriSchemeFile) && ((this.directories == null) || this.DirectoriesContainPath(GetLocalPath(uri)))));
        }

        public static DirectoryAccessRule Deny(params string[] directories) => 
            (directories.Length != 0) ? new DirectoryAccessRule(AccessPermission.Deny, directories) : new DirectoryAccessRule(AccessPermission.Deny);

        private bool DirectoriesContainPath(string path) => 
            this.directories.Any<string>(x => path.StartsWith(x.TrimEnd(new char[] { Path.DirectorySeparatorChar }) + Path.DirectorySeparatorChar.ToString(), StringComparison.OrdinalIgnoreCase));

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DirectoryAccessRule.<>c <>9 = new DirectoryAccessRule.<>c();
            public static Func<string, bool> <>9__3_0;

            internal bool <.ctor>b__3_0(string x) => 
                !Path.IsPathRooted(x);
        }
    }
}

