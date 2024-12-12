namespace DevExpress.Utils.Design
{
    using DevExpress.Data.Utils;
    using System;
    using System.Reflection;
    using System.Security;

    public class DXAssemblyResolver
    {
        private static bool Checked;
        private static int locked;

        [SecuritySafeCritical]
        public static void Init()
        {
            if (!Checked)
            {
                Checked = true;
                try
                {
                    if ((Assembly.GetEntryAssembly() != null) && Assembly.GetEntryAssembly().FullName.StartsWith("WinRes"))
                    {
                        AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(DXAssemblyResolver.OnAssemblyResolve);
                    }
                }
                catch
                {
                }
            }
        }

        private static Assembly OnAssemblyResolve(object sender, ResolveEventArgs e)
        {
            if (locked == 0)
            {
                if (e.Name.Contains(".Design"))
                {
                    return null;
                }
                if (e.Name.StartsWith("DevExpress"))
                {
                    locked++;
                    try
                    {
                        return Helpers.LoadWithPartialName(e.Name);
                    }
                    catch
                    {
                    }
                    finally
                    {
                        locked--;
                    }
                }
            }
            return null;
        }
    }
}

