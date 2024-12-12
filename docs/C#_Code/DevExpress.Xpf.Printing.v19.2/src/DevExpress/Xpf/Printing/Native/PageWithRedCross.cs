namespace DevExpress.Xpf.Printing.Native
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Markup;

    public class PageWithRedCross : UserControl, IComponentConnector
    {
        internal Grid Error;
        private bool _contentLoaded;

        public PageWithRedCross()
        {
            this.InitializeComponent();
        }

        [DebuggerNonUserCode, GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent()
        {
            if (!this._contentLoaded)
            {
                this._contentLoaded = true;
                Uri resourceLocator = new Uri("/DevExpress.Xpf.Printing.v19.2;component/native/pagewithredcross.xaml", UriKind.Relative);
                Application.LoadComponent(this, resourceLocator);
            }
        }

        [DebuggerNonUserCode, GeneratedCode("PresentationBuildTasks", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Never)]
        void IComponentConnector.Connect(int connectionId, object target)
        {
            if (connectionId == 1)
            {
                this.Error = (Grid) target;
            }
            else
            {
                this._contentLoaded = true;
            }
        }
    }
}

