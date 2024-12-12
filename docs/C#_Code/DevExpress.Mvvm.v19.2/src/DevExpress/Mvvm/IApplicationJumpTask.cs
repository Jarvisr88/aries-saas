namespace DevExpress.Mvvm
{
    using System;
    using System.Windows.Media;

    public interface IApplicationJumpTask : IApplicationJumpItem
    {
        string Title { get; set; }

        ImageSource Icon { get; set; }

        string IconResourcePath { get; set; }

        int IconResourceIndex { get; set; }

        string Description { get; set; }

        string ApplicationPath { get; set; }

        string Arguments { get; set; }

        string WorkingDirectory { get; set; }

        string CommandId { get; set; }

        System.Action Action { get; set; }
    }
}

