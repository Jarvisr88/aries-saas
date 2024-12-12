namespace DevExpress.XtraReports.Design.Commands
{
    using System;
    using System.ComponentModel.Design;

    public class MenuCommandEventArgs : EventArgs
    {
        private System.ComponentModel.Design.MenuCommand menuCommand;

        public MenuCommandEventArgs(System.ComponentModel.Design.MenuCommand menuCommand);

        public System.ComponentModel.Design.MenuCommand MenuCommand { get; }
    }
}

