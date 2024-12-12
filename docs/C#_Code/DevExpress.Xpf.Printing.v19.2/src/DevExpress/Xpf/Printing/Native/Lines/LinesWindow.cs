namespace DevExpress.Xpf.Printing.Native.Lines
{
    using DevExpress.Xpf.Core;
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Markup;

    public class LinesWindow : DXDialog, IComponentConnector
    {
        internal LinesContainer linesContainer;
        private bool _contentLoaded;

        public LinesWindow()
        {
            this.InitializeComponent();
        }

        [DebuggerNonUserCode, GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        internal Delegate _CreateDelegate(Type delegateType, string handler) => 
            Delegate.CreateDelegate(delegateType, this, handler);

        [DebuggerNonUserCode, GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent()
        {
            if (!this._contentLoaded)
            {
                this._contentLoaded = true;
                Uri resourceLocator = new Uri("/DevExpress.Xpf.Printing.v19.2;component/native/lines/lineswindow.xaml", UriKind.Relative);
                Application.LoadComponent(this, resourceLocator);
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            base.OkButton.IsDefault = true;
            base.CancelButton.IsCancel = true;
        }

        public void SetLines(LineBase[] lines)
        {
            this.linesContainer.Lines = lines;
        }

        [DebuggerNonUserCode, GeneratedCode("PresentationBuildTasks", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Never)]
        void IComponentConnector.Connect(int connectionId, object target)
        {
            if (connectionId == 1)
            {
                this.linesContainer = (LinesContainer) target;
            }
            else
            {
                this._contentLoaded = true;
            }
        }
    }
}

