namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Core;
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Markup;

    public class ColorChooserControl : UserControl, IComponentConnector
    {
        private IColorEdit ownerEdit;
        internal ColorChooser colorChooser;
        internal Button okButton;
        internal Button Cancel;
        private bool _contentLoaded;

        public ColorChooserControl(IColorEdit owner)
        {
            this.InitializeComponent();
            this.ownerEdit = owner;
            this.colorChooser.Color = owner.Color;
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            bool? dialogResult = null;
            FloatingContainer.CloseDialog(this, dialogResult);
        }

        [DebuggerNonUserCode, GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent()
        {
            if (!this._contentLoaded)
            {
                this._contentLoaded = true;
                Uri resourceLocator = new Uri("/DevExpress.Xpf.Core.v19.2;component/editors/controls/coloredit/colorchoosercontrol.xaml", UriKind.Relative);
                Application.LoadComponent(this, resourceLocator);
            }
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            this.ownerEdit.AddCustomColor(this.colorChooser.Color);
            bool? dialogResult = null;
            FloatingContainer.CloseDialog(this, dialogResult);
        }

        [EditorBrowsable(EditorBrowsableState.Never), GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode]
        void IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:
                    this.colorChooser = (ColorChooser) target;
                    return;

                case 2:
                    this.okButton = (Button) target;
                    this.okButton.Click += new RoutedEventHandler(this.okButton_Click);
                    return;

                case 3:
                    this.Cancel = (Button) target;
                    this.Cancel.Click += new RoutedEventHandler(this.cancelButton_Click);
                    return;
            }
            this._contentLoaded = true;
        }
    }
}

