namespace DevExpress.Xpf.Docking.Native
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI;
    using DevExpress.Xpf.Core.Serialization;
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Docking.Base;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Globalization;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Windows;
    using System.Windows.Data;

    public abstract class DockingDocumentUIServiceBase<TPanel, TGroup> : DocumentUIServiceBase, IDocumentManagerService, IDocumentOwner where TPanel: LayoutPanel where TGroup: LayoutGroup
    {
        public static readonly DependencyProperty ActiveDocumentProperty;
        private static readonly DependencyPropertyKey ActiveViewPropertyKey;
        public static readonly DependencyProperty ActiveViewProperty;
        private static readonly DependencyPropertyKey DocumentsPropertyKey;
        public static readonly DependencyProperty DocumentsProperty;
        private Dictionary<TPanel, FloatGroup> removingPanelsCollection;
        private List<TPanel> floatGroupPanelsCollection;
        [CompilerGenerated]
        private ActiveDocumentChangedEventHandler ActiveDocumentChanged;
        private bool activeDocumentChangeEnabled;

        public event ActiveDocumentChangedEventHandler ActiveDocumentChanged
        {
            [CompilerGenerated] add
            {
                ActiveDocumentChangedEventHandler activeDocumentChanged = this.ActiveDocumentChanged;
                while (true)
                {
                    ActiveDocumentChangedEventHandler comparand = activeDocumentChanged;
                    ActiveDocumentChangedEventHandler handler3 = comparand + value;
                    activeDocumentChanged = Interlocked.CompareExchange<ActiveDocumentChangedEventHandler>(ref this.ActiveDocumentChanged, handler3, comparand);
                    if (ReferenceEquals(activeDocumentChanged, comparand))
                    {
                        return;
                    }
                }
            }
            [CompilerGenerated] remove
            {
                ActiveDocumentChangedEventHandler activeDocumentChanged = this.ActiveDocumentChanged;
                while (true)
                {
                    ActiveDocumentChangedEventHandler comparand = activeDocumentChanged;
                    ActiveDocumentChangedEventHandler handler3 = comparand - value;
                    activeDocumentChanged = Interlocked.CompareExchange<ActiveDocumentChangedEventHandler>(ref this.ActiveDocumentChanged, handler3, comparand);
                    if (ReferenceEquals(activeDocumentChanged, comparand))
                    {
                        return;
                    }
                }
            }
        }

        static DockingDocumentUIServiceBase()
        {
            DockingDocumentUIServiceBase<TPanel, TGroup>.ActiveDocumentProperty = DependencyProperty.Register("ActiveDocument", typeof(IDocument), typeof(DockingDocumentUIServiceBase<TPanel, TGroup>), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (d, e) => ((DockingDocumentUIServiceBase<TPanel, TGroup>) d).OnActiveDocumentChanged(e.OldValue as IDocument, e.NewValue as IDocument)));
            DockingDocumentUIServiceBase<TPanel, TGroup>.ActiveViewPropertyKey = DependencyProperty.RegisterReadOnly("ActiveView", typeof(object), typeof(DockingDocumentUIServiceBase<TPanel, TGroup>), new PropertyMetadata(null));
            DockingDocumentUIServiceBase<TPanel, TGroup>.ActiveViewProperty = DockingDocumentUIServiceBase<TPanel, TGroup>.ActiveViewPropertyKey.DependencyProperty;
            DockingDocumentUIServiceBase<TPanel, TGroup>.DocumentsPropertyKey = DependencyProperty.RegisterReadOnly("Documents", typeof(IEnumerable<IDocument>), typeof(DockingDocumentUIServiceBase<TPanel, TGroup>), new PropertyMetadata(null));
            DockingDocumentUIServiceBase<TPanel, TGroup>.DocumentsProperty = DockingDocumentUIServiceBase<TPanel, TGroup>.DocumentsPropertyKey.DependencyProperty;
        }

        public DockingDocumentUIServiceBase()
        {
            this.activeDocumentChangeEnabled = true;
            this.Documents = new ObservableCollection<IDocument>();
            this.removingPanelsCollection = new Dictionary<TPanel, FloatGroup>();
            this.floatGroupPanelsCollection = new List<TPanel>();
        }

        private void ActivateAfterRemoveOrHidePanelGroup(Document<TPanel, TGroup> document)
        {
            this.ActivateAfterRemovePanel(document);
            if (ReferenceEquals(document, this.ActiveDocument))
            {
                if ((this.GetCountOfOpenDocuments(document) == 1) && (this.DocumentsCollection.Count > 1))
                {
                    Func<Document<TPanel, TGroup>, TPanel> selector = <>c<TPanel, TGroup>.<>9__39_0;
                    if (<>c<TPanel, TGroup>.<>9__39_0 == null)
                    {
                        Func<Document<TPanel, TGroup>, TPanel> local1 = <>c<TPanel, TGroup>.<>9__39_0;
                        selector = <>c<TPanel, TGroup>.<>9__39_0 = x => x.DocumentPanel;
                    }
                    using (IEnumerator<TPanel> enumerator = this.DocumentsCollection.OfType<Document<TPanel, TGroup>>().Select<Document<TPanel, TGroup>, TPanel>(selector).GetEnumerator())
                    {
                        while (true)
                        {
                            if (!enumerator.MoveNext())
                            {
                                break;
                            }
                            TPanel current = enumerator.Current;
                            if (((current != document.DocumentPanel) && (!current.IsTabPage || current.IsSelectedItem)) && !current.Closed)
                            {
                                this.activeDocumentChangeEnabled = false;
                                base.SetCurrentValue(DockingDocumentUIServiceBase<TPanel, TGroup>.ActiveDocumentProperty, GetDocument(current));
                                return;
                            }
                        }
                    }
                }
                Func<IDocument, bool> predicate = <>c<TPanel, TGroup>.<>9__39_1;
                if (<>c<TPanel, TGroup>.<>9__39_1 == null)
                {
                    Func<IDocument, bool> local2 = <>c<TPanel, TGroup>.<>9__39_1;
                    predicate = <>c<TPanel, TGroup>.<>9__39_1 = x => !((Document<TPanel, TGroup>) x).DocumentPanel.Closed;
                }
                if (this.DocumentsCollection.Count<IDocument>(predicate) == 1)
                {
                    base.SetCurrentValue(DockingDocumentUIServiceBase<TPanel, TGroup>.ActiveDocumentProperty, null);
                }
            }
        }

        private void ActivateAfterRemovePanel(Document<TPanel, TGroup> document)
        {
            if (ReferenceEquals(document, this.ActiveDocument) && (this.GetCountOfOpenDocuments(document) > 1))
            {
                Func<BaseLayoutItem, bool> predicate = <>c<TPanel, TGroup>.<>9__38_0;
                if (<>c<TPanel, TGroup>.<>9__38_0 == null)
                {
                    Func<BaseLayoutItem, bool> local1 = <>c<TPanel, TGroup>.<>9__38_0;
                    predicate = <>c<TPanel, TGroup>.<>9__38_0 = x => (x is TPanel) && !x.Closed;
                }
                IEnumerable<TPanel> source = document.DocumentPanel.Parent.Items.Where<BaseLayoutItem>(predicate).Cast<TPanel>();
                int num = 0;
                foreach (TPanel local in source)
                {
                    if (ReferenceEquals(this.ActiveDocument, GetDocument(local)) && (source.Count<TPanel>() > 1))
                    {
                        TPanel local2 = (num != 0) ? source.ElementAt<TPanel>((num - 1)) : source.ElementAt<TPanel>((num + 1));
                        IDocument document2 = GetDocument(local2);
                        this.SetCurrentValue(DockingDocumentUIServiceBase<TPanel, TGroup>.ActiveDocumentProperty, ((document2 == null) || !this.Documents.Contains<IDocument>(document2)) ? null : document2);
                        break;
                    }
                    num++;
                }
            }
        }

        private bool ActiveDocumentChangeEnabled()
        {
            if (this.activeDocumentChangeEnabled)
            {
                return true;
            }
            this.activeDocumentChangeEnabled = true;
            return false;
        }

        protected abstract TPanel CreateDocumentPanel();
        IDocument IDocumentManagerService.CreateDocument(string documentType, object viewModel, object parameter, object parentViewModel)
        {
            TGroup actualDocumentGroup = this.GetActualDocumentGroup();
            if (actualDocumentGroup == null)
            {
                return null;
            }
            if (actualDocumentGroup.Manager != null)
            {
                actualDocumentGroup.Manager.DockItemActivated -= new DockItemActivatedEventHandler(this.DocumentItemActivated);
                actualDocumentGroup.Manager.DockItemActivated += new DockItemActivatedEventHandler(this.DocumentItemActivated);
                actualDocumentGroup.Manager.DockItemClosing -= new DockItemCancelEventHandler(this.DockItemClosing);
                actualDocumentGroup.Manager.DockItemClosing += new DockItemCancelEventHandler(this.DockItemClosing);
            }
            object documentContentView = base.CreateAndInitializeView(documentType, viewModel, parameter, parentViewModel, this);
            TPanel documentContainer = this.CreateDocumentPanel();
            base.InitializeDocumentContainer(documentContainer, ContentItem.ContentProperty, this.GetDocumentPanelStyle(documentContainer, documentContentView));
            Document<TPanel, TGroup> item = new Document<TPanel, TGroup>((DockingDocumentUIServiceBase<TPanel, TGroup>) this, documentContainer, documentType);
            Binding binding = new Binding("BindableId");
            binding.Source = item;
            binding.Converter = new IgnoreIncorrectNameValuesConverter<TPanel, TGroup>();
            BindingOperations.SetBinding(documentContainer, BaseLayoutItem.BindableNameProperty, binding);
            if (documentContentView is DependencyObject)
            {
                DXSerializer.SetEnabled((DependencyObject) documentContentView, false);
            }
            SetTitleBinding(documentContentView, BaseLayoutItem.CaptionProperty, documentContainer, false);
            documentContainer.PreventCaptionSerialization = true;
            documentContainer.Content = documentContentView;
            actualDocumentGroup.Items.Add(documentContainer);
            item.RemoveDocumentPanel += new EventHandler(this.RemoveDocumentPanel);
            item.HideDocumentPanel += new EventHandler(this.HideDocumentPanel);
            this.DocumentsCollection.Add(item);
            return item;
        }

        void IDocumentOwner.Close(IDocumentContent documentContent, bool force)
        {
            CloseDocument(this, documentContent, force);
        }

        private void DockItemClosing(object sender, ItemCancelEventArgs e)
        {
            FloatGroup item = e.Item as FloatGroup;
            if (item != null)
            {
                e.Cancel = !this.DocumentsClose(item);
                if (!e.Cancel)
                {
                    this.floatGroupPanelsCollection.Clear();
                }
            }
        }

        private void DocumentItemActivated(object sender, DockItemActivatedEventArgs e)
        {
            TPanel item = e.Item as TPanel;
            TPanel oldItem = e.OldItem as TPanel;
            bool flag = this.ActiveDocumentChangeEnabled();
            if ((item != null) && (item.IsActive && flag))
            {
                IDocument document = GetDocument(item);
                if (((document != null) && this.Documents.Contains<IDocument>(document)) && !ReferenceEquals(this.ActiveDocument, document))
                {
                    this.activeDocumentChangeEnabled = false;
                    base.SetCurrentValue(DockingDocumentUIServiceBase<TPanel, TGroup>.ActiveDocumentProperty, document);
                }
            }
        }

        private bool DocumentsClose(FloatGroup floatGroup)
        {
            bool flag = true;
            foreach (TPanel local in this.GetItemsOfFloatGroup(floatGroup))
            {
                local.ExecuteCloseCommand();
                if (((Document<TPanel, TGroup>) GetDocument(local)).DocumentPanel != null)
                {
                    flag = false;
                    if (!this.removingPanelsCollection.ContainsKey(local))
                    {
                        this.removingPanelsCollection.Add(local, floatGroup);
                        local.Unloaded += new RoutedEventHandler(this.PanelUnloaded);
                    }
                }
            }
            return flag;
        }

        protected abstract TGroup GetActualDocumentGroup();
        private int GetCountOfOpenDocuments(Document<TPanel, TGroup> document)
        {
            LayoutGroup parent = document.DocumentPanel.Parent;
            if (parent == null)
            {
                return 0;
            }
            Func<BaseLayoutItem, bool> predicate = <>c<TPanel, TGroup>.<>9__40_0;
            if (<>c<TPanel, TGroup>.<>9__40_0 == null)
            {
                Func<BaseLayoutItem, bool> local1 = <>c<TPanel, TGroup>.<>9__40_0;
                predicate = <>c<TPanel, TGroup>.<>9__40_0 = x => !x.Closed;
            }
            return parent.Items.Count<BaseLayoutItem>(predicate);
        }

        private void GetDocumentPanels(LayoutGroup group)
        {
            foreach (BaseLayoutItem item in group.Items)
            {
                LayoutGroup group2 = item as LayoutGroup;
                if (group2 != null)
                {
                    this.GetDocumentPanels(group2);
                }
                TGroup local = item as TGroup;
                if (local != null)
                {
                    foreach (BaseLayoutItem item2 in local.Items)
                    {
                        this.floatGroupPanelsCollection.Add((TPanel) item2);
                    }
                }
            }
        }

        protected abstract Style GetDocumentPanelStyle(TPanel documentPanel, object documentContentView);
        private IEnumerable<TPanel> GetItemsOfFloatGroup(FloatGroup floatGroup)
        {
            this.floatGroupPanelsCollection.Clear();
            LayoutGroup group = floatGroup.Items.FirstOrDefault<BaseLayoutItem>() as LayoutGroup;
            if (group != null)
            {
                this.GetDocumentPanels(group);
            }
            Func<TPanel, bool> keySelector = <>c<TPanel, TGroup>.<>9__28_0;
            if (<>c<TPanel, TGroup>.<>9__28_0 == null)
            {
                Func<TPanel, bool> local1 = <>c<TPanel, TGroup>.<>9__28_0;
                keySelector = <>c<TPanel, TGroup>.<>9__28_0 = x => x.IsActive;
            }
            return this.floatGroupPanelsCollection.OrderBy<TPanel, bool>(keySelector);
        }

        private void HideDocumentPanel(object sender, EventArgs e)
        {
            Document<TPanel, TGroup> document = (Document<TPanel, TGroup>) sender;
            this.ActivateAfterRemoveOrHidePanelGroup(document);
        }

        private void OnActiveDocumentChanged(IDocument oldValue, IDocument newValue)
        {
            Document<TPanel, TGroup> input = (Document<TPanel, TGroup>) newValue;
            bool flag = this.ActiveDocumentChangeEnabled();
            if ((input != null) & flag)
            {
                this.activeDocumentChangeEnabled = false;
                input.DocumentPanel.Closed = false;
                input.DocumentPanel.IsActive = true;
            }
            Func<Document<TPanel, TGroup>, object> evaluator = <>c<TPanel, TGroup>.<>9__42_0;
            if (<>c<TPanel, TGroup>.<>9__42_0 == null)
            {
                Func<Document<TPanel, TGroup>, object> local1 = <>c<TPanel, TGroup>.<>9__42_0;
                evaluator = <>c<TPanel, TGroup>.<>9__42_0 = x => x.DocumentPanel.Content;
            }
            this.ActiveView = input.With<Document<TPanel, TGroup>, object>(evaluator);
            if (this.ActiveDocumentChanged != null)
            {
                this.ActiveDocumentChanged(this, new ActiveDocumentChangedEventArgs(oldValue, newValue));
            }
        }

        private void PanelUnloaded(object sender, RoutedEventArgs e)
        {
            TPanel key = (TPanel) sender;
            key.Unloaded -= new RoutedEventHandler(this.PanelUnloaded);
            FloatGroup floatGroup = this.removingPanelsCollection[key];
            if (floatGroup != null)
            {
                this.removingPanelsCollection.Remove(key);
                if (this.GetItemsOfFloatGroup(floatGroup).Count<TPanel>() == 0)
                {
                    floatGroup.Manager.DockController.Close(floatGroup);
                }
            }
        }

        private void RemoveDocumentPanel(object sender, EventArgs e)
        {
            Document<TPanel, TGroup> document = (Document<TPanel, TGroup>) sender;
            document.RemoveDocumentPanel -= new EventHandler(this.RemoveDocumentPanel);
            document.HideDocumentPanel -= new EventHandler(this.HideDocumentPanel);
            this.ActivateAfterRemoveOrHidePanelGroup(document);
        }

        public IDocument ActiveDocument
        {
            get => 
                (IDocument) base.GetValue(DockingDocumentUIServiceBase<TPanel, TGroup>.ActiveDocumentProperty);
            set => 
                base.SetValue(DockingDocumentUIServiceBase<TPanel, TGroup>.ActiveDocumentProperty, value);
        }

        public object ActiveView
        {
            get => 
                base.GetValue(DockingDocumentUIServiceBase<TPanel, TGroup>.ActiveViewProperty);
            private set => 
                base.SetValue(DockingDocumentUIServiceBase<TPanel, TGroup>.ActiveViewPropertyKey, value);
        }

        public IEnumerable<IDocument> Documents
        {
            get => 
                (IEnumerable<IDocument>) base.GetValue(DockingDocumentUIServiceBase<TPanel, TGroup>.DocumentsProperty);
            private set => 
                base.SetValue(DockingDocumentUIServiceBase<TPanel, TGroup>.DocumentsPropertyKey, value);
        }

        private ObservableCollection<IDocument> DocumentsCollection =>
            (ObservableCollection<IDocument>) this.Documents;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DockingDocumentUIServiceBase<TPanel, TGroup>.<>c <>9;
            public static Func<TPanel, bool> <>9__28_0;
            public static Func<BaseLayoutItem, bool> <>9__38_0;
            public static Func<DockingDocumentUIServiceBase<TPanel, TGroup>.Document, TPanel> <>9__39_0;
            public static Func<IDocument, bool> <>9__39_1;
            public static Func<BaseLayoutItem, bool> <>9__40_0;
            public static Func<DockingDocumentUIServiceBase<TPanel, TGroup>.Document, object> <>9__42_0;

            static <>c()
            {
                DockingDocumentUIServiceBase<TPanel, TGroup>.<>c.<>9 = new DockingDocumentUIServiceBase<TPanel, TGroup>.<>c();
            }

            internal void <.cctor>b__43_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DockingDocumentUIServiceBase<TPanel, TGroup>) d).OnActiveDocumentChanged(e.OldValue as IDocument, e.NewValue as IDocument);
            }

            internal TPanel <ActivateAfterRemoveOrHidePanelGroup>b__39_0(DockingDocumentUIServiceBase<TPanel, TGroup>.Document x) => 
                x.DocumentPanel;

            internal bool <ActivateAfterRemoveOrHidePanelGroup>b__39_1(IDocument x) => 
                !((DockingDocumentUIServiceBase<TPanel, TGroup>.Document) x).DocumentPanel.Closed;

            internal bool <ActivateAfterRemovePanel>b__38_0(BaseLayoutItem x) => 
                (x is TPanel) && !x.Closed;

            internal bool <GetCountOfOpenDocuments>b__40_0(BaseLayoutItem x) => 
                !x.Closed;

            internal bool <GetItemsOfFloatGroup>b__28_0(TPanel x) => 
                x.IsActive;

            internal object <OnActiveDocumentChanged>b__42_0(DockingDocumentUIServiceBase<TPanel, TGroup>.Document x) => 
                x.DocumentPanel.Content;
        }

        public class Document : BindableBase, IDocument, IDocumentInfo
        {
            [CompilerGenerated]
            private EventHandler RemoveDocumentPanel;
            [CompilerGenerated]
            private EventHandler HideDocumentPanel;
            private DocumentState state;
            private bool destroyOnClose;

            public event EventHandler HideDocumentPanel
            {
                [CompilerGenerated] add
                {
                    EventHandler hideDocumentPanel = this.HideDocumentPanel;
                    while (true)
                    {
                        EventHandler comparand = hideDocumentPanel;
                        EventHandler handler3 = comparand + value;
                        hideDocumentPanel = Interlocked.CompareExchange<EventHandler>(ref this.HideDocumentPanel, handler3, comparand);
                        if (ReferenceEquals(hideDocumentPanel, comparand))
                        {
                            return;
                        }
                    }
                }
                [CompilerGenerated] remove
                {
                    EventHandler hideDocumentPanel = this.HideDocumentPanel;
                    while (true)
                    {
                        EventHandler comparand = hideDocumentPanel;
                        EventHandler handler3 = comparand - value;
                        hideDocumentPanel = Interlocked.CompareExchange<EventHandler>(ref this.HideDocumentPanel, handler3, comparand);
                        if (ReferenceEquals(hideDocumentPanel, comparand))
                        {
                            return;
                        }
                    }
                }
            }

            public event EventHandler RemoveDocumentPanel
            {
                [CompilerGenerated] add
                {
                    EventHandler removeDocumentPanel = this.RemoveDocumentPanel;
                    while (true)
                    {
                        EventHandler comparand = removeDocumentPanel;
                        EventHandler handler3 = comparand + value;
                        removeDocumentPanel = Interlocked.CompareExchange<EventHandler>(ref this.RemoveDocumentPanel, handler3, comparand);
                        if (ReferenceEquals(removeDocumentPanel, comparand))
                        {
                            return;
                        }
                    }
                }
                [CompilerGenerated] remove
                {
                    EventHandler removeDocumentPanel = this.RemoveDocumentPanel;
                    while (true)
                    {
                        EventHandler comparand = removeDocumentPanel;
                        EventHandler handler3 = comparand - value;
                        removeDocumentPanel = Interlocked.CompareExchange<EventHandler>(ref this.RemoveDocumentPanel, handler3, comparand);
                        if (ReferenceEquals(removeDocumentPanel, comparand))
                        {
                            return;
                        }
                    }
                }
            }

            public Document(DockingDocumentUIServiceBase<TPanel, TGroup> owner, TPanel documentPanel, string type)
            {
                this.state = DocumentState.Hidden;
                this.destroyOnClose = true;
                this.Owner = owner;
                this.DocumentPanel = documentPanel;
                this.DocumentType = type;
                DocumentUIServiceBase.SetDocument(documentPanel, this);
                this.UpdateCloseCommand();
            }

            private bool CanClose() => 
                this.DocumentPanel.AllowClose;

            private void CloseCore(bool force)
            {
                using (this.DocumentPanel.LockCloseCommand())
                {
                    if (!force)
                    {
                        CancelEventArgs e = new CancelEventArgs();
                        DocumentViewModelHelper.OnClose(this.GetContent(), e);
                        if (e.Cancel)
                        {
                            return;
                        }
                    }
                    if (this.destroyOnClose)
                    {
                        this.RemoveFromItems();
                        this.state = DocumentState.Destroyed;
                    }
                    else
                    {
                        if (this.HideDocumentPanel != null)
                        {
                            this.HideDocumentPanel(this, EventArgs.Empty);
                        }
                        this.HidePanel();
                    }
                }
            }

            void IDocument.Close(bool force)
            {
                this.CloseCore(force);
            }

            void IDocument.Hide()
            {
                this.HidePanel();
                this.state = DocumentState.Hidden;
            }

            void IDocument.Show()
            {
                this.DocumentPanel.Closed = false;
                this.DocumentPanel.IsActive = true;
                this.state = DocumentState.Visible;
            }

            private object GetContent() => 
                ViewHelper.GetViewModelFromView(this.DocumentPanel.Content);

            private void HidePanel()
            {
                this.DocumentPanel.Closed = true;
                this.DocumentPanel.IsActive = false;
                this.state = DocumentState.Hidden;
            }

            private void RemoveFromItems()
            {
                DocumentViewModelHelper.OnDestroy(this.GetContent());
                if (this.RemoveDocumentPanel != null)
                {
                    this.RemoveDocumentPanel(this, EventArgs.Empty);
                }
                DockLayoutManager dockLayoutManager = this.DocumentPanel.GetDockLayoutManager();
                if ((dockLayoutManager != null) && (dockLayoutManager.DockController != null))
                {
                    dockLayoutManager.DockController.RemovePanel(this.DocumentPanel);
                }
                else
                {
                    this.DocumentPanel.Parent.Items.Remove(this.DocumentPanel);
                }
                this.Owner.DocumentsCollection.Remove(this);
                Action<IDocumentContent> action = <>c<TPanel, TGroup>.<>9__17_0;
                if (<>c<TPanel, TGroup>.<>9__17_0 == null)
                {
                    Action<IDocumentContent> local1 = <>c<TPanel, TGroup>.<>9__17_0;
                    action = <>c<TPanel, TGroup>.<>9__17_0 = x => x.DocumentOwner = null;
                }
                (this.GetContent() as IDocumentContent).Do<IDocumentContent>(action);
                this.DocumentPanel.Content = null;
                TPanel local = default(TPanel);
                this.DocumentPanel = local;
                this.Owner = null;
            }

            private void UpdateCloseCommand()
            {
                this.DocumentPanel.CloseCommand = DelegateCommandFactory.Create(() => base.CloseCore(false), new Func<bool>(this.CanClose), false);
            }

            public DockingDocumentUIServiceBase<TPanel, TGroup> Owner { get; private set; }

            public TPanel DocumentPanel { get; private set; }

            public bool DestroyOnClose
            {
                get => 
                    this.destroyOnClose;
                set => 
                    this.destroyOnClose = value;
            }

            public object Title
            {
                get => 
                    this.DocumentPanel.Caption;
                set => 
                    this.DocumentPanel.Caption = value;
            }

            public object Content =>
                this.GetContent();

            public object Id
            {
                get => 
                    this.BindableId;
                set => 
                    this.BindableId = value;
            }

            DocumentState IDocumentInfo.State =>
                this.state;

            public string DocumentType { get; private set; }

            public object BindableId
            {
                get => 
                    base.GetProperty<object>(System.Linq.Expressions.Expression.Lambda<Func<object>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(DockingDocumentUIServiceBase<TPanel, TGroup>.Document)), (MethodInfo) methodof(DockingDocumentUIServiceBase<TPanel, TGroup>.Document.get_BindableId, DockingDocumentUIServiceBase<TPanel, TGroup>.Document)), new ParameterExpression[0]));
                set => 
                    base.SetProperty<object>(System.Linq.Expressions.Expression.Lambda<Func<object>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(DockingDocumentUIServiceBase<TPanel, TGroup>.Document)), (MethodInfo) methodof(DockingDocumentUIServiceBase<TPanel, TGroup>.Document.get_BindableId, DockingDocumentUIServiceBase<TPanel, TGroup>.Document)), new ParameterExpression[0]), value);
            }

            [Serializable, CompilerGenerated]
            private sealed class <>c
            {
                public static readonly DockingDocumentUIServiceBase<TPanel, TGroup>.Document.<>c <>9;
                public static Action<IDocumentContent> <>9__17_0;

                static <>c()
                {
                    DockingDocumentUIServiceBase<TPanel, TGroup>.Document.<>c.<>9 = new DockingDocumentUIServiceBase<TPanel, TGroup>.Document.<>c();
                }

                internal void <RemoveFromItems>b__17_0(IDocumentContent x)
                {
                    x.DocumentOwner = null;
                }
            }
        }

        private class IgnoreIncorrectNameValuesConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                string input = value as string;
                return (((input == null) || !Regex.IsMatch(input, "^[_a-zA-Z][_a-zA-Z0-9]*$")) ? null : value);
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }
    }
}

