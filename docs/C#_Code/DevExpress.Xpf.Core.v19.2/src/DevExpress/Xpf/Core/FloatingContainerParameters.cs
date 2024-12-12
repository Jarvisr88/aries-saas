namespace DevExpress.Xpf.Core
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Media;

    public class FloatingContainerParameters
    {
        public FloatingContainerParameters()
        {
            this.ClosedDelegate = null;
            this.Title = string.Empty;
            this.AllowSizing = false;
            this.ShowApplyButton = false;
            this.CloseOnEscape = false;
            this.ContainerFocusable = true;
            this.ShowModal = true;
        }

        public DialogClosedDelegate ClosedDelegate { get; set; }

        public string Title { get; set; }

        public bool AllowSizing { get; set; }

        public bool ShowApplyButton { get; set; }

        public bool CloseOnEscape { get; set; }

        public ImageSource Icon { get; set; }

        public bool ContainerFocusable { get; set; }

        public bool ShowModal { get; set; }
    }
}

