namespace DevExpress.Xpf.Core.ConditionalFormatting
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Markup;

    public class CustomFormatEditorView : UserControl, IComponentConnector
    {
        private bool _contentLoaded;

        public CustomFormatEditorView()
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
                Uri resourceLocator = new Uri("/DevExpress.Xpf.Core.v19.2;component/core/conditionalformatting/dialogs/views/customformateditorview.xaml", UriKind.Relative);
                Application.LoadComponent(this, resourceLocator);
            }
        }

        [GeneratedCode("PresentationBuildTasks", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Never), DebuggerNonUserCode]
        void IComponentConnector.Connect(int connectionId, object target)
        {
            this._contentLoaded = true;
        }
    }
}

