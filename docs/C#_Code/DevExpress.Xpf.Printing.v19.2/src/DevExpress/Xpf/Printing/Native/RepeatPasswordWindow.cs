namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Editors;
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Markup;

    public class RepeatPasswordWindow : DXDialog, IComponentConnector
    {
        internal TextBlock note;
        internal Label passwordName;
        internal PasswordBoxEdit password;
        private bool _contentLoaded;

        public RepeatPasswordWindow(string caption, string note, string passwordName)
        {
            this.InitializeComponent();
            base.Title = caption;
            this.note.Text = note;
            this.passwordName.Content = passwordName;
            RoutedEventHandler handler1 = <>c.<>9__0_0;
            if (<>c.<>9__0_0 == null)
            {
                RoutedEventHandler local1 = <>c.<>9__0_0;
                handler1 = <>c.<>9__0_0 = (editor, args) => ((PasswordBoxEdit) editor).Focus();
            }
            this.password.Loaded += handler1;
        }

        [DebuggerNonUserCode, GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent()
        {
            if (!this._contentLoaded)
            {
                this._contentLoaded = true;
                Uri resourceLocator = new Uri("/DevExpress.Xpf.Printing.v19.2;component/native/repeatpasswordwindow.xaml", UriKind.Relative);
                Application.LoadComponent(this, resourceLocator);
            }
        }

        [DebuggerNonUserCode, GeneratedCode("PresentationBuildTasks", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Never)]
        void IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:
                    this.note = (TextBlock) target;
                    return;

                case 2:
                    this.passwordName = (Label) target;
                    return;

                case 3:
                    this.password = (PasswordBoxEdit) target;
                    return;
            }
            this._contentLoaded = true;
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly RepeatPasswordWindow.<>c <>9 = new RepeatPasswordWindow.<>c();
            public static RoutedEventHandler <>9__0_0;

            internal void <.ctor>b__0_0(object editor, RoutedEventArgs args)
            {
                ((PasswordBoxEdit) editor).Focus();
            }
        }
    }
}

