namespace DMEWorks.Install
{
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Configuration.Install;
    using System.IO;
    using System.Security.AccessControl;

    [RunInstaller(true)]
    public class Installer : System.Configuration.Install.Installer
    {
        private const string sid = @"BUILTIN\\USERS";

        protected override void OnAfterInstall(IDictionary savedState)
        {
            base.OnAfterInstall(savedState);
            try
            {
                string path = Path.Combine(Path.GetDirectoryName(base.Context.Parameters["assemblypath"]), "Custom");
                if (!Directory.Exists(path))
                {
                    base.Context.LogMessage($"Directory '{path}' does not exist");
                }
                else
                {
                    DirectorySecurity accessControl = Directory.GetAccessControl(path);
                    accessControl.ResetAccessRule(new FileSystemAccessRule(@"BUILTIN\\USERS", FileSystemRights.Modify, InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, PropagationFlags.None, AccessControlType.Allow));
                    Directory.SetAccessControl(path, accessControl);
                }
            }
            catch (Exception exception1)
            {
                Exception ex = exception1;
                ProjectData.SetProjectError(ex);
                Exception exception = ex;
                base.Context.LogMessage(exception.ToString());
                ProjectData.ClearProjectError();
            }
        }
    }
}

