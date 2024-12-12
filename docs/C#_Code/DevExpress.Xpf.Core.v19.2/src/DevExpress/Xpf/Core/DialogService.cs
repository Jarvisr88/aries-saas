namespace DevExpress.Xpf.Core
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI;
    using DevExpress.Mvvm.UI.Interactivity;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    [TargetType(typeof(UserControl)), TargetType(typeof(Window))]
    public class DialogService : ViewServiceBase, IDialogService, IMessageBoxButtonLocalizer, IMessageButtonLocalizer, IDocumentOwner
    {
        internal const string ShowDialogException = "Cannot use dialogButtons and dialogCommands parameters simultaneously.";
        public static readonly DependencyProperty TitleProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty TitleFromViewModelProperty;
        public static readonly DependencyProperty DialogStyleProperty;
        public static readonly DependencyProperty DialogWindowStartupLocationProperty;
        public static readonly DependencyProperty SetWindowOwnerProperty;
        private List<WeakReference> windows = new List<WeakReference>();
        private string initTitle;

        static DialogService()
        {
            TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(DialogService), new PropertyMetadata(null, (d, e) => ((DialogService) d).OnTitleChanged()));
            TitleFromViewModelProperty = DependencyProperty.Register("TitleFromViewModel", typeof(string), typeof(DialogService), new PropertyMetadata(null, (d, e) => ((DialogService) d).OnTitleFromViewModelChanged()));
            DialogStyleProperty = DependencyProperty.Register("DialogStyle", typeof(Style), typeof(DialogService), new PropertyMetadata(null));
            DialogWindowStartupLocationProperty = DependencyProperty.Register("DialogWindowStartupLocation", typeof(WindowStartupLocation), typeof(DialogService), new PropertyMetadata(WindowStartupLocation.CenterScreen));
            SetWindowOwnerProperty = DependencyProperty.Register("SetWindowOwner", typeof(bool), typeof(DialogService), new PropertyMetadata(true));
        }

        protected virtual Window CreateDialogWindow(object view)
        {
            Window w = this.CreateDialogWindowCore();
            w.Content = view;
            w.WindowStartupLocation = this.DialogWindowStartupLocation;
            if (this.SetWindowOwner)
            {
                UpdateWindowOwner(w, base.AssociatedObject);
            }
            base.InitializeDocumentContainer(w, ContentControl.ContentProperty, this.DialogStyle);
            base.UpdateThemeName(w);
            return w;
        }

        protected virtual Window CreateDialogWindowCore() => 
            (Window) Activator.CreateInstance(WindowService.GetDefaultDialogWindowType(this.DialogStyle));

        UICommand IDialogService.ShowDialog(IEnumerable<UICommand> dialogCommands, string title, string documentType, object viewModel, object parameter, object parentViewModel)
        {
            object view = base.CreateAndInitializeView(documentType, viewModel, parameter, parentViewModel, this);
            return this.ShowDialog(dialogCommands, title, view);
        }

        void IDocumentOwner.Close(IDocumentContent documentContent, bool force)
        {
            Window window = (from w in this.GetWindows()
                where ViewHelper.GetViewModelFromView(w.Content) == documentContent
                select w).FirstOrDefault<Window>();
            if (window != null)
            {
                if (force)
                {
                    window.Closing -= new CancelEventHandler(this.OnDialogWindowClosing);
                }
                window.Close();
            }
        }

        string IMessageBoxButtonLocalizer.Localize(MessageBoxResult button) => 
            this.Localize(button);

        string IMessageButtonLocalizer.Localize(MessageResult button) => 
            this.Localize(button.ToMessageBoxResult());

        private object GetViewModel(Window window) => 
            ViewHelper.GetViewModelFromView(window.Content);

        [IteratorStateMachine(typeof(<GetWindows>d__25))]
        private IEnumerable<Window> GetWindows()
        {
            <GetWindows>d__25 d__1 = new <GetWindows>d__25(-2);
            d__1.<>4__this = this;
            return d__1;
        }

        private string Localize(MessageBoxResult button) => 
            new DXDialogWindowMessageBoxButtonLocalizer().Localize(button);

        protected virtual void OnDialogWindowClosed(object sender, EventArgs e)
        {
            Window window = (Window) sender;
            this.UnsubscribeWindow(window);
            DocumentViewModelHelper.OnDestroy(this.GetViewModel(window));
            DocumentUIServiceBase.ClearTitleBinding(TitleFromViewModelProperty, this);
            this.DialogWindow = null;
        }

        protected virtual void OnDialogWindowClosing(object sender, CancelEventArgs e)
        {
            Window window = (Window) sender;
            DocumentViewModelHelper.OnClose(this.GetViewModel(window), e);
        }

        private void OnTitleChanged()
        {
            this.UpdateTitle();
        }

        private void OnTitleFromViewModelChanged()
        {
            this.UpdateTitle();
        }

        protected UICommand ShowDialog(IEnumerable<UICommand> dialogCommands, string title, object view)
        {
            this.DialogWindow = this.CreateDialogWindow(view);
            this.windows.Add(new WeakReference(this.DialogWindow));
            this.initTitle = title;
            DocumentUIServiceBase.SetTitleBinding(view, TitleFromViewModelProperty, this, true);
            this.UpdateTitle();
            this.SubscribeWindow(this.DialogWindow);
            if (this.DialogWindow is DXDialogWindow)
            {
                return this.ShowDXDialogWindow((DXDialogWindow) this.DialogWindow, dialogCommands);
            }
            if (this.DialogWindow is ThemedWindow)
            {
                return this.ShowThemedWindow((ThemedWindow) this.DialogWindow, dialogCommands);
            }
            this.DialogWindow.ShowDialog();
            return null;
        }

        private UICommand ShowDXDialogWindow(DXDialogWindow w, IEnumerable<UICommand> dialogCommands)
        {
            if (dialogCommands != null)
            {
                w.CommandsSource = dialogCommands;
            }
            return w.ShowDialogWindow();
        }

        private UICommand ShowThemedWindow(ThemedWindow w, IEnumerable<UICommand> dialogCommands)
        {
            if (dialogCommands != null)
            {
                return w.ShowDialog(dialogCommands);
            }
            w.ShowDialog();
            return null;
        }

        private void SubscribeWindow(Window window)
        {
            window.Closing += new CancelEventHandler(this.OnDialogWindowClosing);
            window.Closed += new EventHandler(this.OnDialogWindowClosed);
        }

        private void UnsubscribeWindow(Window window)
        {
            window.Closing -= new CancelEventHandler(this.OnDialogWindowClosing);
            window.Closed -= new EventHandler(this.OnDialogWindowClosed);
        }

        private void UpdateTitle()
        {
            if (this.DialogWindow != null)
            {
                if (!string.IsNullOrEmpty(this.Title))
                {
                    this.DialogWindow.Title = this.Title;
                }
                else if (!string.IsNullOrEmpty(this.TitleFromViewModel))
                {
                    this.DialogWindow.Title = this.TitleFromViewModel;
                }
                else if (!string.IsNullOrEmpty(this.initTitle))
                {
                    this.DialogWindow.Title = this.initTitle;
                }
            }
        }

        public string Title
        {
            get => 
                (string) base.GetValue(TitleProperty);
            set => 
                base.SetValue(TitleProperty, value);
        }

        private string TitleFromViewModel
        {
            get => 
                (string) base.GetValue(TitleFromViewModelProperty);
            set => 
                base.SetValue(TitleFromViewModelProperty, value);
        }

        public Style DialogStyle
        {
            get => 
                (Style) base.GetValue(DialogStyleProperty);
            set => 
                base.SetValue(DialogStyleProperty, value);
        }

        public WindowStartupLocation DialogWindowStartupLocation
        {
            get => 
                (WindowStartupLocation) base.GetValue(DialogWindowStartupLocationProperty);
            set => 
                base.SetValue(DialogWindowStartupLocationProperty, value);
        }

        public bool SetWindowOwner
        {
            get => 
                (bool) base.GetValue(SetWindowOwnerProperty);
            set => 
                base.SetValue(SetWindowOwnerProperty, value);
        }

        protected Window DialogWindow { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DialogService.<>c <>9 = new DialogService.<>c();

            internal void <.cctor>b__47_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DialogService) d).OnTitleChanged();
            }

            internal void <.cctor>b__47_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DialogService) d).OnTitleFromViewModelChanged();
            }
        }

        [CompilerGenerated]
        private sealed class <GetWindows>d__25 : IEnumerable<Window>, IEnumerable, IEnumerator<Window>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private Window <>2__current;
            private int <>l__initialThreadId;
            public DialogService <>4__this;
            private int <windowIndex>5__1;

            [DebuggerHidden]
            public <GetWindows>d__25(int <>1__state)
            {
                this.<>1__state = <>1__state;
                this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
            }

            private bool MoveNext()
            {
                int num = this.<>1__state;
                if (num == 0)
                {
                    this.<>1__state = -1;
                    this.<windowIndex>5__1 = this.<>4__this.windows.Count;
                }
                else
                {
                    if (num != 1)
                    {
                        return false;
                    }
                    this.<>1__state = -1;
                }
                while (true)
                {
                    int num2 = this.<windowIndex>5__1 - 1;
                    this.<windowIndex>5__1 = num2;
                    if (num2 < 0)
                    {
                        return false;
                    }
                    Window target = (Window) this.<>4__this.windows[this.<windowIndex>5__1].Target;
                    if (target != null)
                    {
                        this.<>2__current = target;
                        this.<>1__state = 1;
                        return true;
                    }
                    this.<>4__this.windows.RemoveAt(this.<windowIndex>5__1);
                }
            }

            [DebuggerHidden]
            IEnumerator<Window> IEnumerable<Window>.GetEnumerator()
            {
                DialogService.<GetWindows>d__25 d__;
                if ((this.<>1__state == -2) && (this.<>l__initialThreadId == Environment.CurrentManagedThreadId))
                {
                    this.<>1__state = 0;
                    d__ = this;
                }
                else
                {
                    d__ = new DialogService.<GetWindows>d__25(0) {
                        <>4__this = this.<>4__this
                    };
                }
                return d__;
            }

            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator() => 
                this.System.Collections.Generic.IEnumerable<System.Windows.Window>.GetEnumerator();

            [DebuggerHidden]
            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            [DebuggerHidden]
            void IDisposable.Dispose()
            {
            }

            Window IEnumerator<Window>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }
    }
}

