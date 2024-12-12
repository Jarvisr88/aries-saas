namespace DevExpress.Xpf.Core.WizardFramework
{
    using DevExpress.Data.WizardFramework;
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Markup;

    public class WizardView : UserControl, IWizardView, IComponentConnector
    {
        internal ContentPresenter pageContent;
        internal Button cancelButton;
        internal Button previousButton;
        internal Button nextButton;
        internal Button finishButton;
        private bool _contentLoaded;

        public event EventHandler Cancel;

        public event EventHandler Finish;

        public event EventHandler Next;

        public event EventHandler Previous;

        public WizardView()
        {
            this.InitializeComponent();
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            if (this.Cancel != null)
            {
                this.Cancel(this, EventArgs.Empty);
            }
        }

        public void EnableFinish(bool enable)
        {
            this.finishButton.IsEnabled = enable;
        }

        public void EnableNext(bool enable)
        {
            this.nextButton.IsEnabled = enable;
        }

        public void EnablePrevious(bool enable)
        {
            this.previousButton.IsEnabled = enable;
        }

        private void finish_Click(object sender, RoutedEventArgs e)
        {
            if (this.Finish != null)
            {
                this.Finish(this, EventArgs.Empty);
            }
        }

        [GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode]
        public void InitializeComponent()
        {
            if (!this._contentLoaded)
            {
                this._contentLoaded = true;
                Uri resourceLocator = new Uri("/DevExpress.Xpf.Core.v19.2;component/wizardframework/wizardview.xaml", UriKind.Relative);
                Application.LoadComponent(this, resourceLocator);
            }
        }

        private void next_Click(object sender, RoutedEventArgs e)
        {
            if (this.Next != null)
            {
                this.Next(this, EventArgs.Empty);
            }
        }

        private void previous_Click(object sender, RoutedEventArgs e)
        {
            if (this.Previous != null)
            {
                this.Previous(this, EventArgs.Empty);
            }
        }

        public void SetPageContent(object content)
        {
            this.pageContent.Content = content;
        }

        public void ShowError(string error)
        {
            MessageBox.Show(error);
        }

        [EditorBrowsable(EditorBrowsableState.Never), GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode]
        void IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:
                    this.pageContent = (ContentPresenter) target;
                    return;

                case 2:
                    this.cancelButton = (Button) target;
                    this.cancelButton.Click += new RoutedEventHandler(this.cancel_Click);
                    return;

                case 3:
                    this.previousButton = (Button) target;
                    this.previousButton.Click += new RoutedEventHandler(this.previous_Click);
                    return;

                case 4:
                    this.nextButton = (Button) target;
                    this.nextButton.Click += new RoutedEventHandler(this.next_Click);
                    return;

                case 5:
                    this.finishButton = (Button) target;
                    this.finishButton.Click += new RoutedEventHandler(this.finish_Click);
                    return;
            }
            this._contentLoaded = true;
        }
    }
}

