namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Mvvm.POCO;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.LayoutControl;
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Linq.Expressions;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Markup;

    public class PageSetupWindow : DXDialog, IComponentConnector
    {
        internal DevExpress.Xpf.LayoutControl.LayoutControl LayoutRoot;
        private bool _contentLoaded;

        public PageSetupWindow()
        {
            this.InitializeComponent();
            this.Model = ViewModelSource.Create<LegacyPageSetupViewModel>(System.Linq.Expressions.Expression.Lambda<Func<LegacyPageSetupViewModel>>(System.Linq.Expressions.Expression.New(typeof(LegacyPageSetupViewModel)), new ParameterExpression[0]));
        }

        [DebuggerNonUserCode, GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent()
        {
            if (!this._contentLoaded)
            {
                this._contentLoaded = true;
                Uri resourceLocator = new Uri("/DevExpress.Xpf.Printing.v19.2;component/native/pagesetupwindow.xaml", UriKind.Relative);
                Application.LoadComponent(this, resourceLocator);
            }
        }

        [DebuggerNonUserCode, GeneratedCode("PresentationBuildTasks", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Never)]
        void IComponentConnector.Connect(int connectionId, object target)
        {
            if (connectionId == 1)
            {
                this.LayoutRoot = (DevExpress.Xpf.LayoutControl.LayoutControl) target;
            }
            else
            {
                this._contentLoaded = true;
            }
        }

        public LegacyPageSetupViewModel Model { get; private set; }
    }
}

