namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Windows.Markup;

    public class DXTabControlHeaderMenuTemplateSelector : DXTabControlHeaderMenuTemplateSelectorBase, IComponentConnector
    {
        private bool _contentLoaded;

        public DXTabControlHeaderMenuTemplateSelector();
        [DebuggerNonUserCode, GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent();
        [GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode, EditorBrowsable(EditorBrowsableState.Never)]
        void IComponentConnector.Connect(int connectionId, object target);
    }
}

