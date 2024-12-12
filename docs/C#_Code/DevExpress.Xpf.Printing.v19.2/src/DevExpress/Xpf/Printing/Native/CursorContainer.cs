namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Xpf.Printing;
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Markup;
    using System.Windows.Media;

    public class CursorContainer : UserControl, IComponentConnector
    {
        private Point cursorPosition;
        internal ContentPresenter cursorPresenter;
        private bool _contentLoaded;

        public CursorContainer()
        {
            this.InitializeComponent();
        }

        [DebuggerNonUserCode, GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent()
        {
            if (!this._contentLoaded)
            {
                this._contentLoaded = true;
                Uri resourceLocator = new Uri("/DevExpress.Xpf.Printing.v19.2;component/native/cursorcontainer.xaml", UriKind.Relative);
                Application.LoadComponent(this, resourceLocator);
            }
        }

        [DebuggerNonUserCode, GeneratedCode("PresentationBuildTasks", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Never)]
        void IComponentConnector.Connect(int connectionId, object target)
        {
            if (connectionId == 1)
            {
                this.cursorPresenter = (ContentPresenter) target;
            }
            else
            {
                this._contentLoaded = true;
            }
        }

        public DevExpress.Xpf.Printing.CustomCursor CustomCursor
        {
            get => 
                this.cursorPresenter.Content as DevExpress.Xpf.Printing.CustomCursor;
            set => 
                this.cursorPresenter.Content = value;
        }

        public Point CursorPosition
        {
            get => 
                this.cursorPosition;
            set
            {
                if ((this.cursorPosition != value) && (this.CustomCursor != null))
                {
                    this.cursorPosition = value;
                    this.CursorPositionTransform.X = this.cursorPosition.X - this.CustomCursor.HotSpot.X;
                    this.CursorPositionTransform.Y = this.cursorPosition.Y - this.CustomCursor.HotSpot.Y;
                }
            }
        }

        private TranslateTransform CursorPositionTransform =>
            (TranslateTransform) this.cursorPresenter.RenderTransform;
    }
}

