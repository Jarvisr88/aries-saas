namespace DevExpress.Xpf.Core
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI;
    using DevExpress.Mvvm.UI.Interactivity;
    using DevExpress.Mvvm.UI.Native;
    using DevExpress.Xpf.Core.Serialization;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls;

    [TargetType(typeof(UserControl)), TargetType(typeof(Window))]
    public class WindowedDocumentUIService : DocumentUIServiceBase, IDocumentManagerService, IDocumentOwner
    {
        public static readonly DependencyProperty WindowStartupLocationProperty = DependencyProperty.Register("WindowStartupLocation", typeof(System.Windows.WindowStartupLocation), typeof(WindowedDocumentUIService), new PropertyMetadata(System.Windows.WindowStartupLocation.CenterScreen));
        public static readonly DependencyProperty WindowStyleProperty = DependencyProperty.Register("WindowStyle", typeof(Style), typeof(WindowedDocumentUIService), new PropertyMetadata(null));
        public static readonly DependencyProperty WindowStyleSelectorProperty = DependencyProperty.Register("WindowStyleSelector", typeof(StyleSelector), typeof(WindowedDocumentUIService), new PropertyMetadata(null));
        public static readonly DependencyProperty SetWindowOwnerProperty = DependencyProperty.Register("SetWindowOwner", typeof(bool), typeof(WindowedDocumentUIService), new PropertyMetadata(true));
        public static readonly DependencyProperty WindowTypeProperty = DependencyProperty.Register("WindowType", typeof(Type), typeof(WindowedDocumentUIService), new PropertyMetadata(null));
        public static readonly DependencyProperty DocumentShowModeProperty = DependencyProperty.Register("DocumentShowMode", typeof(WindowShowMode), typeof(WindowedDocumentUIService), new PropertyMetadata(WindowShowMode.Default));
        public static readonly DependencyProperty ActiveDocumentProperty;
        private static readonly DependencyPropertyKey ActiveViewPropertyKey;
        public static readonly DependencyProperty ActiveViewProperty;
        private IList<IWindowSurrogate> windows = new List<IWindowSurrogate>();

        public event ActiveDocumentChangedEventHandler ActiveDocumentChanged;

        static WindowedDocumentUIService()
        {
            ActiveDocumentProperty = DependencyProperty.Register("ActiveDocument", typeof(IDocument), typeof(WindowedDocumentUIService), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (d, e) => ((WindowedDocumentUIService) d).OnActiveDocumentChanged(e.OldValue as IDocument, e.NewValue as IDocument)));
            ActiveViewPropertyKey = DependencyProperty.RegisterReadOnly("ActiveView", typeof(object), typeof(WindowedDocumentUIService), new PropertyMetadata(null));
            ActiveViewProperty = ActiveViewPropertyKey.DependencyProperty;
        }

        protected virtual IWindowSurrogate CreateWindow(object view)
        {
            IWindowSurrogate windowSurrogate = WindowProxy.GetWindowSurrogate(Activator.CreateInstance(this.ActualWindowType));
            base.UpdateThemeName(windowSurrogate.RealWindow);
            windowSurrogate.RealWindow.Content = view;
            if (this.SetWindowOwner)
            {
                windowSurrogate.RealWindow.Owner = Window.GetWindow(base.AssociatedObject);
            }
            Style documentContainerStyle = base.GetDocumentContainerStyle(windowSurrogate.RealWindow, view, this.WindowStyle, this.WindowStyleSelector);
            base.InitializeDocumentContainer(windowSurrogate.RealWindow, ContentControl.ContentProperty, documentContainerStyle);
            windowSurrogate.RealWindow.WindowStartupLocation = this.WindowStartupLocation;
            return windowSurrogate;
        }

        IDocument IDocumentManagerService.CreateDocument(string documentType, object viewModel, object parameter, object parentViewModel)
        {
            object view = base.CreateAndInitializeView(documentType, viewModel, parameter, parentViewModel, this);
            IWindowSurrogate item = this.CreateWindow(view);
            this.windows.Add(item);
            this.SubscribeWindow(item);
            IDocument document = new WindowDocument(this, item, view, documentType);
            SetDocument(item.RealWindow, document);
            SetTitleBinding(view, Window.TitleProperty, item.RealWindow, true);
            return document;
        }

        void IDocumentOwner.Close(IDocumentContent documentContent, bool force)
        {
            CloseDocument(this, documentContent, force);
        }

        private void OnActiveDocumentChanged(IDocument oldValue, IDocument newValue)
        {
            WindowDocument input = (WindowDocument) newValue;
            if (input != null)
            {
                input.Window.Activate();
            }
            Func<WindowDocument, object> evaluator = <>c.<>9__50_0;
            if (<>c.<>9__50_0 == null)
            {
                Func<WindowDocument, object> local1 = <>c.<>9__50_0;
                evaluator = <>c.<>9__50_0 = x => x.documentContentView;
            }
            this.ActiveView = input.With<WindowDocument, object>(evaluator);
            if (this.ActiveDocumentChanged != null)
            {
                this.ActiveDocumentChanged(this, new ActiveDocumentChangedEventArgs(oldValue, newValue));
            }
        }

        private void OnWindowActivated(object sender, EventArgs e)
        {
            this.ActiveDocument = GetDocument((Window) sender);
        }

        private void OnWindowClosed(object sender, EventArgs e)
        {
            if (this.windows.Count == 1)
            {
                this.ActiveDocument = null;
            }
            this.UnsubscribeWindow(WindowProxy.GetWindowSurrogate(sender));
        }

        private void OnWindowDeactivated(object sender, EventArgs e)
        {
            if (ReferenceEquals(this.ActiveDocument, GetDocument((Window) sender)))
            {
                this.ActiveDocument = null;
            }
        }

        private void SubscribeWindow(IWindowSurrogate window)
        {
            window.Activated += new EventHandler(this.OnWindowActivated);
            window.Deactivated += new EventHandler(this.OnWindowDeactivated);
            window.Closed += new EventHandler(this.OnWindowClosed);
        }

        private void UnsubscribeWindow(IWindowSurrogate window)
        {
            window.Activated -= new EventHandler(this.OnWindowActivated);
            window.Deactivated -= new EventHandler(this.OnWindowDeactivated);
            window.Closed -= new EventHandler(this.OnWindowClosed);
        }

        public System.Windows.WindowStartupLocation WindowStartupLocation
        {
            get => 
                (System.Windows.WindowStartupLocation) base.GetValue(WindowStartupLocationProperty);
            set => 
                base.SetValue(WindowStartupLocationProperty, value);
        }

        public Style WindowStyle
        {
            get => 
                (Style) base.GetValue(WindowStyleProperty);
            set => 
                base.SetValue(WindowStyleProperty, value);
        }

        public IDocument ActiveDocument
        {
            get => 
                (IDocument) base.GetValue(ActiveDocumentProperty);
            set => 
                base.SetValue(ActiveDocumentProperty, value);
        }

        public object ActiveView
        {
            get => 
                base.GetValue(ActiveViewProperty);
            private set => 
                base.SetValue(ActiveViewPropertyKey, value);
        }

        public StyleSelector WindowStyleSelector
        {
            get => 
                (StyleSelector) base.GetValue(WindowStyleSelectorProperty);
            set => 
                base.SetValue(WindowStyleSelectorProperty, value);
        }

        public bool SetWindowOwner
        {
            get => 
                (bool) base.GetValue(SetWindowOwnerProperty);
            set => 
                base.SetValue(SetWindowOwnerProperty, value);
        }

        public Type WindowType
        {
            get => 
                (Type) base.GetValue(WindowTypeProperty);
            set => 
                base.SetValue(WindowTypeProperty, value);
        }

        public WindowShowMode DocumentShowMode
        {
            get => 
                (WindowShowMode) base.GetValue(DocumentShowModeProperty);
            set => 
                base.SetValue(DocumentShowModeProperty, value);
        }

        private Type ActualWindowType =>
            this.WindowType ?? WindowService.GetDefaultWindowType(this.WindowStyle);

        public IEnumerable<IDocument> Documents
        {
            get
            {
                Func<IWindowSurrogate, IDocument> selector = <>c.<>9__41_0;
                if (<>c.<>9__41_0 == null)
                {
                    Func<IWindowSurrogate, IDocument> local1 = <>c.<>9__41_0;
                    selector = <>c.<>9__41_0 = w => GetDocument(w.RealWindow);
                }
                return this.windows.Select<IWindowSurrogate, IDocument>(selector);
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly WindowedDocumentUIService.<>c <>9 = new WindowedDocumentUIService.<>c();
            public static Func<IWindowSurrogate, IDocument> <>9__41_0;
            public static Func<WindowedDocumentUIService.WindowDocument, object> <>9__50_0;

            internal void <.cctor>b__52_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((WindowedDocumentUIService) d).OnActiveDocumentChanged(e.OldValue as IDocument, e.NewValue as IDocument);
            }

            internal IDocument <get_Documents>b__41_0(IWindowSurrogate w) => 
                DocumentUIServiceBase.GetDocument(w.RealWindow);

            internal object <OnActiveDocumentChanged>b__50_0(WindowedDocumentUIService.WindowDocument x) => 
                x.documentContentView;
        }

        public class WindowDocument : IDocument, IDocumentInfo
        {
            public readonly object documentContentView;
            private bool destroyOnClose = true;
            private readonly WindowedDocumentUIService owner;
            private DocumentState state = DocumentState.Hidden;
            private string documentType;
            private bool onClosing;

            public WindowDocument(WindowedDocumentUIService owner, IWindowSurrogate window, object documentContentView, string documentType)
            {
                this.owner = owner;
                this.Window = window;
                this.documentContentView = documentContentView;
                this.documentType = documentType;
                this.Window.Closing += new CancelEventHandler(this.window_Closing);
                this.Window.Closed += new EventHandler(this.window_Closed);
                if (documentContentView is DependencyObject)
                {
                    DXSerializer.SetEnabled((DependencyObject) documentContentView, false);
                }
            }

            void IDocument.Close(bool force)
            {
                if (!force)
                {
                    if (!this.onClosing)
                    {
                        this.Window.Close();
                    }
                }
                else
                {
                    this.Window.Closing -= new CancelEventHandler(this.window_Closing);
                    if (!this.destroyOnClose)
                    {
                        if (!this.onClosing)
                        {
                            this.Window.Hide();
                        }
                        this.state = DocumentState.Hidden;
                    }
                    else
                    {
                        if (!this.onClosing)
                        {
                            this.Window.Close();
                        }
                        this.state = DocumentState.Destroyed;
                    }
                }
            }

            void IDocument.Hide()
            {
                this.Window.Hide();
                this.state = DocumentState.Hidden;
            }

            void IDocument.Show()
            {
                this.state = DocumentState.Visible;
                if (this.owner.DocumentShowMode == WindowShowMode.Dialog)
                {
                    this.Window.ShowDialog();
                }
                else
                {
                    this.Window.Show();
                }
            }

            private object GetContent() => 
                ViewHelper.GetViewModelFromView(this.documentContentView);

            private void RemoveFromWindowsList()
            {
                this.owner.windows.Remove(this.Window);
            }

            private void window_Closed(object sender, EventArgs e)
            {
                this.RemoveFromWindowsList();
                this.Window.Closing -= new CancelEventHandler(this.window_Closing);
                this.Window.Closed -= new EventHandler(this.window_Closed);
                DocumentViewModelHelper.OnDestroy(this.GetContent());
            }

            private void window_Closing(object sender, CancelEventArgs e)
            {
                this.onClosing = true;
                DocumentViewModelHelper.OnClose(this.GetContent(), e);
                if (!this.destroyOnClose && !e.Cancel)
                {
                    e.Cancel = true;
                    this.Window.Hide();
                }
                this.state = this.destroyOnClose ? DocumentState.Destroyed : DocumentState.Hidden;
                this.onClosing = false;
            }

            [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
            public IWindowSurrogate Window { get; private set; }

            public bool DestroyOnClose
            {
                get => 
                    this.destroyOnClose;
                set => 
                    this.destroyOnClose = value;
            }

            public object Id { get; set; }

            public object Title
            {
                get => 
                    this.Window.RealWindow.Title;
                set => 
                    this.Window.RealWindow.Title = Convert.ToString(value);
            }

            public object Content =>
                this.GetContent();

            DocumentState IDocumentInfo.State =>
                this.state;

            string IDocumentInfo.DocumentType =>
                this.documentType;
        }
    }
}

