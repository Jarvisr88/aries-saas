namespace DevExpress.Xpf.Core.ConditionalFormatting
{
    using DevExpress.Xpf.Editors.Filtering;
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Markup;

    public class CustomConditionEditorView : UserControl, IComponentConnector
    {
        internal FilterControl filterControl;
        private bool _contentLoaded;

        public CustomConditionEditorView()
        {
            this.InitializeComponent();
            base.Loaded += new RoutedEventHandler(this.FrameworkElement_Loaded);
        }

        [DebuggerNonUserCode, GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        internal Delegate _CreateDelegate(Type delegateType, string handler) => 
            Delegate.CreateDelegate(delegateType, this, handler);

        private void FrameworkElement_Loaded(object sender, RoutedEventArgs e)
        {
            base.Loaded -= new RoutedEventHandler(this.FrameworkElement_Loaded);
            Window.GetWindow(this).Closing += new CancelEventHandler(this.Window_Closing);
        }

        [GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode]
        public void InitializeComponent()
        {
            if (!this._contentLoaded)
            {
                this._contentLoaded = true;
                Uri resourceLocator = new Uri("/DevExpress.Xpf.Core.v19.2;component/core/conditionalformatting/dialogs/views/customconditioneditorview.xaml", UriKind.Relative);
                Application.LoadComponent(this, resourceLocator);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never), GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode]
        void IComponentConnector.Connect(int connectionId, object target)
        {
            if (connectionId == 1)
            {
                this.filterControl = (FilterControl) target;
            }
            else
            {
                this._contentLoaded = true;
            }
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            ((Window) sender).Closing -= new CancelEventHandler(this.Window_Closing);
            this.filterControl.ApplyFilter();
        }
    }
}

