namespace DevExpress.Xpf.Core
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI;
    using DevExpress.Mvvm.UI.Interactivity;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Core.Serialization;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    [TargetType(typeof(Window)), TargetType(typeof(UserControl)), TargetType(typeof(DXTabControl))]
    public class TabbedWindowDocumentUIService : TabbedDocumentUIServiceBase, IGroupedDocumentManagerService, IDocumentManagerService, IDocumentOwner
    {
        public static readonly DependencyProperty WindowShownCommandProperty = DependencyProperty.Register("WindowShownCommand", typeof(ICommand), typeof(TabbedWindowDocumentUIService), new PropertyMetadata(null));
        public static readonly DependencyProperty WindowClosingCommandProperty = DependencyProperty.Register("WindowClosingCommand", typeof(ICommand), typeof(TabbedWindowDocumentUIService), new PropertyMetadata(null));
        public static readonly DependencyProperty SingleWindowClosingCommandProperty = DependencyProperty.Register("SingleWindowClosingCommand", typeof(ICommand), typeof(TabbedWindowDocumentUIService), new PropertyMetadata(null));
        private bool lockTabShowing;
        private bool lockTabHidding;
        private List<TabbedWindowDocument> documents = new List<TabbedWindowDocument>();

        IDocument IDocumentManagerService.CreateDocument(string documentType, object viewModel, object parameter, object parentViewModel)
        {
            object view = base.CreateAndInitializeView(documentType, viewModel, parameter, parentViewModel, this);
            DXSerializer.SetEnabled((DependencyObject) view, false);
            return new TabbedWindowDocument(this, view, documentType);
        }

        void IDocumentOwner.Close(IDocumentContent documentContent, bool force)
        {
            CloseDocument(this, documentContent, force);
        }

        private IEnumerable<IDocument> GetDocuments(DXTabControl tabControl)
        {
            List<IDocument> currentGroup = new List<IDocument>();
            if (tabControl != null)
            {
                tabControl.ForEachTabItem(delegate (DXTabItem x) {
                    IDocument item = GetDocument(x);
                    if (item != null)
                    {
                        currentGroup.Add(item);
                    }
                });
            }
            return currentGroup;
        }

        private IEnumerable<IDocumentGroup> GetGroups()
        {
            List<TabbedWindowDocumentGroup> list = new List<TabbedWindowDocumentGroup>();
            if (base.CurrentTarget != null)
            {
                if (base.CurrentTarget.View.StretchView == null)
                {
                    list.Add(new TabbedWindowDocumentGroup(this.Documents));
                    return (IEnumerable<IDocumentGroup>) list;
                }
                foreach (DXTabControl control in DragDropRegionManager.GetDragDropControls(base.CurrentTarget.View.StretchView.DragDropRegion, base.CurrentTarget.Dispatcher).OfType<DXTabControl>())
                {
                    IEnumerable<IDocument> documents = this.GetDocuments(control);
                    if (documents.Count<IDocument>() > 0)
                    {
                        list.Add(new TabbedWindowDocumentGroup(documents));
                    }
                }
            }
            return (IEnumerable<IDocumentGroup>) list;
        }

        protected override void OnTabbedWindowClosed(object sender, EventArgs e)
        {
            base.OnTabbedWindowClosed(sender, e);
            foreach (TabbedWindowDocument document in (from x in this.documents
                where ReferenceEquals(x.Window, sender)
                select x).ToList<TabbedWindowDocument>())
            {
                if (document.State == DocumentState.Visible)
                {
                    document.Close(true);
                }
            }
        }

        protected override void OnTabbedWindowClosing(object sender, CancelEventArgs e)
        {
            base.OnTabbedWindowClosing(sender, e);
            DocumentsClosingEventArgs args1 = new DocumentsClosingEventArgs((IEnumerable<IDocument>) (from x in this.documents
                where ReferenceEquals(x.Window, sender)
                select x).ToList<TabbedWindowDocument>());
            args1.Cancel = e.Cancel;
            DocumentsClosingEventArgs args = args1;
            this.WindowClosingCommand.If<ICommand>(x => x.CanExecute(args)).Do<ICommand>(x => x.Execute(args));
            if (this.Groups.Count<IDocumentGroup>() == 1)
            {
                this.SingleWindowClosingCommand.If<ICommand>(x => x.CanExecute(args)).Do<ICommand>(x => x.Execute(args));
            }
            e.Cancel = args.Cancel;
        }

        protected override void OnTabbedWindowShown(object sender, EventArgs e)
        {
            base.OnTabbedWindowShown(sender, e);
            this.WindowShownCommand.If<ICommand>(x => x.CanExecute(e)).Do<ICommand>(x => x.Execute(e));
        }

        protected override void OnTabControlTabHiding(object sender, TabControlTabHidingEventArgs e)
        {
            base.OnTabControlTabHiding(sender, e);
            if (!this.lockTabHidding)
            {
                DXTabControl control = (DXTabControl) sender;
                DXTabItem tabItem = control.GetTabItem(e.Item);
                TabbedWindowDocument document = (tabItem != null) ? ((TabbedWindowDocument) GetDocument(tabItem)) : null;
                if (document != null)
                {
                    e.Cancel = true;
                    if (control.View.RemoveTabItemsOnHiding)
                    {
                        document.Close(false);
                    }
                    else
                    {
                        document.Hide();
                    }
                }
            }
        }

        protected override void OnTabControlTabShowing(object sender, TabControlTabShowingEventArgs e)
        {
            base.OnTabControlTabShowing(sender, e);
            if (!this.lockTabShowing)
            {
                DXTabItem tabItem = ((DXTabControl) sender).GetTabItem(e.Item);
                TabbedWindowDocument document = (tabItem != null) ? ((TabbedWindowDocument) GetDocument(tabItem)) : null;
                if (document != null)
                {
                    e.Cancel = true;
                    document.Show();
                }
            }
        }

        public ICommand WindowShownCommand
        {
            get => 
                (ICommand) base.GetValue(WindowShownCommandProperty);
            set => 
                base.SetValue(WindowShownCommandProperty, value);
        }

        public ICommand WindowClosingCommand
        {
            get => 
                (ICommand) base.GetValue(WindowClosingCommandProperty);
            set => 
                base.SetValue(WindowClosingCommandProperty, value);
        }

        public ICommand SingleWindowClosingCommand
        {
            get => 
                (ICommand) base.GetValue(SingleWindowClosingCommandProperty);
            set => 
                base.SetValue(SingleWindowClosingCommandProperty, value);
        }

        public IEnumerable<IDocument> Documents =>
            (IEnumerable<IDocument>) this.documents;

        public IDocumentGroup ActiveGroup =>
            new TabbedWindowDocumentGroup(this.GetDocuments(base.CurrentTarget));

        public IEnumerable<IDocumentGroup> Groups =>
            this.GetGroups();

        public class TabbedWindowDocument : ViewModelBase, IDocument, IDocumentInfo, IDisposable
        {
            private bool lockShow;

            public TabbedWindowDocument(TabbedWindowDocumentUIService owner, object view, string documentType)
            {
                this.Owner = owner;
                this.View = view;
                this.DocumentType = documentType;
                this.Owner.documents.Add(this);
                this.State = DocumentState.Hidden;
            }

            public void Close(bool force = true)
            {
                this.CloseCore(force, this.DestroyOnClose);
            }

            internal void CloseCore(bool force, bool dispose)
            {
                if (this.State != DocumentState.Destroyed)
                {
                    CancelEventArgs e = new CancelEventArgs();
                    if (!force)
                    {
                        DocumentViewModelHelper.OnClose(this.Content, e);
                    }
                    if (!e.Cancel)
                    {
                        this.Owner.lockTabHidding = true;
                        this.Tab.Close();
                        this.Owner.lockTabHidding = false;
                        this.Tab.Owner.Do<DXTabControl>(x => x.RemoveTabItem(this.Tab));
                        this.State = DocumentState.Hidden;
                        if (dispose)
                        {
                            this.Dispose();
                        }
                    }
                }
            }

            private void CreateOrReplaceTab()
            {
                DXTabControl currentTarget = this.Owner.CurrentTarget;
                int index = -1;
                Func<DXTabItem, bool> evaluator = <>c.<>9__41_0;
                if (<>c.<>9__41_0 == null)
                {
                    Func<DXTabItem, bool> local1 = <>c.<>9__41_0;
                    evaluator = <>c.<>9__41_0 = x => x.IsNew;
                }
                if (currentTarget.SelectedContainer.Return<DXTabItem, bool>(evaluator, <>c.<>9__41_1 ??= () => false))
                {
                    index = currentTarget.SelectedIndex;
                    this.Owner.lockTabHidding = true;
                    currentTarget.RemoveTabItem(currentTarget.SelectedContainer);
                    this.Owner.lockTabHidding = false;
                }
                if (this.Tab != null)
                {
                    if (index != -1)
                    {
                        this.Tab.Remove();
                        this.Tab.Insert(currentTarget, index);
                    }
                }
                else
                {
                    this.Tab = this.Owner.CreateTabItem();
                    DocumentUIServiceBase.SetDocument(this.Tab, this);
                    this.Tab.Content = this.View;
                    this.Owner.InitializeDocumentContainer(this.Tab, ContentControl.ContentProperty, null);
                    if (index != -1)
                    {
                        currentTarget.Items.Insert(index, this.Tab);
                    }
                    else
                    {
                        currentTarget.Items.Add(this.Tab);
                    }
                }
            }

            public void Dispose()
            {
                Func<DXTabItem, DXTabControl> evaluator = <>c.<>9__46_0;
                if (<>c.<>9__46_0 == null)
                {
                    Func<DXTabItem, DXTabControl> local1 = <>c.<>9__46_0;
                    evaluator = <>c.<>9__46_0 = x => x.Owner;
                }
                this.Tab.With<DXTabItem, DXTabControl>(evaluator).Do<DXTabControl>(x => x.RemoveTabItem(this.Tab));
                DocumentViewModelHelper.OnDestroy(this.Content);
                this.Owner.documents.Remove(this);
                this.Owner = null;
                this.Tab = null;
                this.View = null;
                this.State = DocumentState.Destroyed;
            }

            public void Hide()
            {
                if (this.State == DocumentState.Visible)
                {
                    TabbedWindowDocumentUIService owner = this.Owner;
                    owner.lockTabHidding = true;
                    this.Tab.Close();
                    this.State = DocumentState.Hidden;
                    owner.lockTabHidding = false;
                }
            }

            private void OnTitleChanged()
            {
                this.Tab.Do<DXTabItem>(x => x.Header = this.Title);
            }

            private void PrepareTab()
            {
                DocumentUIServiceBase.SetDocument(this.Tab, this);
                this.Tab.SetCurrentValue(DXTabItem.IsNewProperty, false);
                if (this.Tab.Owner == null)
                {
                    this.Owner.CurrentTarget.Items.Add(this.Tab);
                }
                DocumentUIServiceBase.SetTitleBinding(this.View, HeaderedSelectorItemBase<DXTabControl, DXTabItem>.HeaderProperty, this.Tab, false);
                if (this.Title != null)
                {
                    this.OnTitleChanged();
                }
                if (string.IsNullOrEmpty(this.Tab.Name) && (this.Id != null))
                {
                    this.Tab.Name = this.Id.ToString();
                }
            }

            public void Show()
            {
                if ((this.Owner != null) && !this.lockShow)
                {
                    this.lockShow = true;
                    this.Owner.lockTabShowing = true;
                    if ((this.Tab == null) || (this.Tab.Owner == null))
                    {
                        this.CreateOrReplaceTab();
                        this.PrepareTab();
                    }
                    Action<System.Windows.Window> action = <>c.<>9__40_0;
                    if (<>c.<>9__40_0 == null)
                    {
                        Action<System.Windows.Window> local1 = <>c.<>9__40_0;
                        action = <>c.<>9__40_0 = x => x.Activate();
                    }
                    System.Windows.Window.GetWindow(this.Tab.Owner).Do<System.Windows.Window>(action);
                    this.Tab.Owner.ShowTabItem(this.Tab, true);
                    this.State = DocumentState.Visible;
                    this.Owner.lockTabShowing = false;
                    this.lockShow = false;
                }
            }

            public TabbedWindowDocumentUIService Owner { get; private set; }

            public object View { get; private set; }

            public System.Windows.Window Window =>
                this.TabControl.With<DXTabControl, System.Windows.Window>(new Func<DXTabControl, System.Windows.Window>(System.Windows.Window.GetWindow));

            public DXTabControl TabControl
            {
                get
                {
                    Func<DXTabItem, DXTabControl> evaluator = <>c.<>9__11_0;
                    if (<>c.<>9__11_0 == null)
                    {
                        Func<DXTabItem, DXTabControl> local1 = <>c.<>9__11_0;
                        evaluator = <>c.<>9__11_0 = x => x.Owner;
                    }
                    return this.Tab.With<DXTabItem, DXTabControl>(evaluator);
                }
            }

            public DXTabItem Tab { get; private set; }

            public object Id { get; set; }

            public object Title
            {
                get => 
                    base.GetProperty<object>(System.Linq.Expressions.Expression.Lambda<Func<object>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(TabbedWindowDocumentUIService.TabbedWindowDocument)), (MethodInfo) methodof(TabbedWindowDocumentUIService.TabbedWindowDocument.get_Title)), new ParameterExpression[0]));
                set => 
                    base.SetProperty<object>(System.Linq.Expressions.Expression.Lambda<Func<object>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(TabbedWindowDocumentUIService.TabbedWindowDocument)), (MethodInfo) methodof(TabbedWindowDocumentUIService.TabbedWindowDocument.get_Title)), new ParameterExpression[0]), value, new Action(this.OnTitleChanged));
            }

            public object Content =>
                ViewHelper.GetViewModelFromView(this.View);

            public bool DestroyOnClose { get; set; }

            public string DocumentType { get; private set; }

            public DocumentState State { get; private set; }

            [Serializable, CompilerGenerated]
            private sealed class <>c
            {
                public static readonly TabbedWindowDocumentUIService.TabbedWindowDocument.<>c <>9 = new TabbedWindowDocumentUIService.TabbedWindowDocument.<>c();
                public static Func<DXTabItem, DXTabControl> <>9__11_0;
                public static Action<Window> <>9__40_0;
                public static Func<DXTabItem, bool> <>9__41_0;
                public static Func<bool> <>9__41_1;
                public static Func<DXTabItem, DXTabControl> <>9__46_0;

                internal bool <CreateOrReplaceTab>b__41_0(DXTabItem x) => 
                    x.IsNew;

                internal bool <CreateOrReplaceTab>b__41_1() => 
                    false;

                internal DXTabControl <Dispose>b__46_0(DXTabItem x) => 
                    x.Owner;

                internal DXTabControl <get_TabControl>b__11_0(DXTabItem x) => 
                    x.Owner;

                internal void <Show>b__40_0(Window x)
                {
                    x.Activate();
                }
            }
        }

        public class TabbedWindowDocumentGroup : IDocumentGroup
        {
            public TabbedWindowDocumentGroup(IEnumerable<IDocument> documents)
            {
                this.Documents = documents;
            }

            public IEnumerable<IDocument> Documents { get; private set; }
        }
    }
}

