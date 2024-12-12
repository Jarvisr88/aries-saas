namespace DevExpress.Mvvm.UI.Native
{
    using System;

    public interface IJumpActionsManager
    {
        void BeginUpdate();
        void EndUpdate();
        void RegisterAction(IJumpAction jumpAction, string commandLineArgumentPrefix, Func<string> launcherPath);
    }
}

