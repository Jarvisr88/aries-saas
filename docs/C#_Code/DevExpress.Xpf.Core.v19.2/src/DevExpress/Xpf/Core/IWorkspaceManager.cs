namespace DevExpress.Xpf.Core
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public interface IWorkspaceManager
    {
        event EventHandler AfterApplyWorkspace;

        event EventHandler BeforeApplyWorkspace;

        void ApplyWorkspace(string name);
        void CaptureWorkspace(string name);
        bool LoadWorkspace(string name, object path);
        void RemoveWorkspace(string name);
        void RenameWorkspace(string oldName, string newName);
        bool SaveWorkspace(string name, object path);

        FrameworkElement TargetControl { get; }

        List<IWorkspace> Workspaces { get; }

        DevExpress.Xpf.Core.TransitionEffect TransitionEffect { get; set; }

        bool? CloseStreamOnWorkspaceSaving { get; set; }

        bool? CloseStreamOnWorkspaceLoading { get; set; }
    }
}

