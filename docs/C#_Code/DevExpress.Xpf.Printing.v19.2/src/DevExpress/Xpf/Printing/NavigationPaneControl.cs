namespace DevExpress.Xpf.Printing
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.DocumentViewer;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Printing.PreviewControl.Native;
    using DevExpress.Xpf.Printing.PreviewControl.Native.Models;
    using DevExpress.XtraPrinting;
    using System;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Input;
    using System.Windows.Threading;

    [TemplatePart(Name="PART_DocumentMapControl", Type=typeof(DocumentMap))]
    public class NavigationPaneControl : Control, INavigationPaneUI
    {
        private const string PART_DocumentMapControl = "PART_DocumentMapControl";
        public static readonly DependencyProperty NavigationPaneSettingsProperty;
        public static readonly DependencyProperty NavigationPaneSearchModelProperty;
        private static readonly DependencyProperty ActualDocumentProperty;
        private static readonly DependencyPropertyKey NavigationPaneSearchModelPropertyKey;
        private readonly Locker settingsLocker = new Locker();
        private DocumentPreviewControl viewer;
        private ButtonEdit searchBox;

        static NavigationPaneControl()
        {
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(NavigationPaneControl), "d");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            FrameworkPropertyMetadataOptions? frameworkOptions = null;
            DependencyPropertyRegistrator<NavigationPaneControl> registrator1 = DependencyPropertyRegistrator<NavigationPaneControl>.New().RegisterReadOnly<DevExpress.Xpf.Printing.NavigationPaneSearchModel>(System.Linq.Expressions.Expression.Lambda<Func<NavigationPaneControl, DevExpress.Xpf.Printing.NavigationPaneSearchModel>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(NavigationPaneControl.get_NavigationPaneSearchModel)), parameters), out NavigationPaneSearchModelPropertyKey, out NavigationPaneSearchModelProperty, null, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(NavigationPaneControl), "d");
            ParameterExpression[] expressionArray2 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<NavigationPaneControl> registrator2 = registrator1.Register<DevExpress.Xpf.Printing.NavigationPaneSettings>(System.Linq.Expressions.Expression.Lambda<Func<NavigationPaneControl, DevExpress.Xpf.Printing.NavigationPaneSettings>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(NavigationPaneControl.get_NavigationPaneSettings)), expressionArray2), out NavigationPaneSettingsProperty, null, (d, o, n) => d.OnSettingsChanged(o, n), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(NavigationPaneControl), "d");
            ParameterExpression[] expressionArray3 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            registrator2.Register<DocumentViewModel>(System.Linq.Expressions.Expression.Lambda<Func<NavigationPaneControl, DocumentViewModel>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(NavigationPaneControl.get_ActualDocument)), expressionArray3), out ActualDocumentProperty, null, (d, o, n) => d.OnDocumentChanged(o, n), frameworkOptions).OverrideMetadata<DocumentPreviewControl>(DocumentViewerControl.ActualViewerProperty, null, (d, o, n) => d.viewer = n, FrameworkPropertyMetadataOptions.None).OverrideDefaultStyleKey();
        }

        public NavigationPaneControl()
        {
            this.SearchCommand = new DelegateCommand(() => this.NavigationPaneSearchModel.Search());
            base.SetCurrentValue(NavigationPaneSettingsProperty, new DevExpress.Xpf.Printing.NavigationPaneSettings());
            Binding binding = new Binding();
            object[] pathParameters = new object[] { DocumentViewerControl.ActualViewerProperty };
            binding.Path = new PropertyPath("(0).Document", pathParameters);
            binding.Source = this;
            binding.Mode = BindingMode.OneWay;
            binding.FallbackValue = null;
            base.SetBinding(ActualDocumentProperty, binding);
        }

        void INavigationPaneUI.SyncSearchParametersWithModel()
        {
            this.settingsLocker.DoLockedActionIfNotLocked(delegate {
                if ((this.NavigationPaneSearchModel != null) && (this.NavigationPaneSettings != null))
                {
                    this.NavigationPaneSearchModel.UseCaseSensitiveSearch = this.NavigationPaneSettings.UseCaseSensitiveSearch;
                    this.NavigationPaneSearchModel.SearchWholeWords = this.NavigationPaneSettings.SearchWholeWords;
                }
            });
        }

        void INavigationPaneUI.SyncSearchParametersWithSettings()
        {
            this.settingsLocker.DoLockedActionIfNotLocked(delegate {
                if ((this.NavigationPaneSearchModel != null) && (this.NavigationPaneSettings != null))
                {
                    this.NavigationPaneSettings.UseCaseSensitiveSearch = this.NavigationPaneSearchModel.UseCaseSensitiveSearch;
                    this.NavigationPaneSettings.SearchWholeWords = this.NavigationPaneSearchModel.SearchWholeWords;
                }
            });
        }

        public void FocusSearchBox()
        {
            this.searchBox.Focus();
            Keyboard.Focus(this.searchBox);
            DispatcherHelper.DoEvents(2);
            this.searchBox.EditValue = null;
            this.searchBox.Text = null;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.searchBox = (ButtonEdit) base.GetTemplateChild("PART_SearchBox");
        }

        private void OnDocumentChanged(DocumentViewModel oldValue, DocumentViewModel newValue)
        {
            if (oldValue != null)
            {
                oldValue.StartDocumentCreation -= new EventHandler(this.OnStartDocumentCreation);
                oldValue.DocumentCreated -= new EventHandler(this.OnDocumentCreated);
            }
            if (newValue == null)
            {
                this.NavigationPaneSearchModel = null;
            }
            else
            {
                this.NavigationPaneSearchModel = DevExpress.Xpf.Printing.NavigationPaneSearchModel.Create(this);
                ((INavigationPaneUI) this).SyncSearchParametersWithModel();
                newValue.StartDocumentCreation += new EventHandler(this.OnStartDocumentCreation);
                newValue.DocumentCreated += new EventHandler(this.OnDocumentCreated);
            }
        }

        private void OnDocumentCreated(object sender, EventArgs e)
        {
            this.NavigationPaneSearchModel.Search();
        }

        private void OnSettingsChanged(DevExpress.Xpf.Printing.NavigationPaneSettings o, DevExpress.Xpf.Printing.NavigationPaneSettings n)
        {
            Action<DevExpress.Xpf.Printing.NavigationPaneSettings> action = <>c.<>9__39_0;
            if (<>c.<>9__39_0 == null)
            {
                Action<DevExpress.Xpf.Printing.NavigationPaneSettings> local1 = <>c.<>9__39_0;
                action = <>c.<>9__39_0 = x => x.AssignNavigationPaneUI(null);
            }
            o.Do<DevExpress.Xpf.Printing.NavigationPaneSettings>(action);
            n.Do<DevExpress.Xpf.Printing.NavigationPaneSettings>(x => x.AssignNavigationPaneUI(this));
        }

        private void OnStartDocumentCreation(object sender, EventArgs e)
        {
            if (this.NavigationPaneSearchModel.CanStopSearch())
            {
                this.NavigationPaneSearchModel.StopSearch();
            }
        }

        public DevExpress.Xpf.Printing.NavigationPaneSearchModel NavigationPaneSearchModel
        {
            get => 
                (DevExpress.Xpf.Printing.NavigationPaneSearchModel) base.GetValue(NavigationPaneSearchModelProperty);
            private set => 
                base.SetValue(NavigationPaneSearchModelPropertyKey, value);
        }

        public DocumentViewModel ActualDocument
        {
            get => 
                (DocumentViewModel) base.GetValue(ActualDocumentProperty);
            set => 
                base.SetValue(ActualDocumentProperty, value);
        }

        public DevExpress.Xpf.Printing.NavigationPaneSettings NavigationPaneSettings
        {
            get => 
                (DevExpress.Xpf.Printing.NavigationPaneSettings) base.GetValue(NavigationPaneSettingsProperty);
            set => 
                base.SetValue(NavigationPaneSettingsProperty, value);
        }

        object INavigationPaneUI.SelectedBookmarkNode
        {
            get
            {
                Func<DevExpress.Xpf.Printing.PreviewControl.Native.DocumentMapSettings, object> evaluator = <>c.<>9__18_0;
                if (<>c.<>9__18_0 == null)
                {
                    Func<DevExpress.Xpf.Printing.PreviewControl.Native.DocumentMapSettings, object> local1 = <>c.<>9__18_0;
                    evaluator = <>c.<>9__18_0 = x => x.SelectedNode;
                }
                return (this.viewer.ActualDocumentMapSettings as DevExpress.Xpf.Printing.PreviewControl.Native.DocumentMapSettings).With<DevExpress.Xpf.Printing.PreviewControl.Native.DocumentMapSettings, object>(evaluator);
            }
            set => 
                (this.viewer.ActualDocumentMapSettings as DevExpress.Xpf.Printing.PreviewControl.Native.DocumentMapSettings).Do<DevExpress.Xpf.Printing.PreviewControl.Native.DocumentMapSettings>(x => x.SelectedNode = value);
        }

        PrintingSystemBase INavigationPaneUI.PrintingSystem =>
            this.viewer.Document.PrintingSystem;

        Dispatcher INavigationPaneUI.Dispatcher =>
            this.viewer.Dispatcher;

        DevExpress.Xpf.Printing.DocumentPresenterControl INavigationPaneUI.DocumentPresenter =>
            this.viewer.DocumentPresenter;

        int INavigationPaneUI.CurrentPageNumber =>
            this.viewer.CurrentPageNumber;

        NavigationPaneTabType INavigationPaneUI.ActiveTab =>
            this.NavigationPaneSettings.ActiveTab;

        ObservableRangeCollection<object> INavigationPaneUI.BookmarkNodes =>
            (ObservableRangeCollection<object>) this.viewer.ActualDocumentMapSettings.Source;

        public ICommand SearchCommand { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly NavigationPaneControl.<>c <>9 = new NavigationPaneControl.<>c();
            public static Func<DevExpress.Xpf.Printing.PreviewControl.Native.DocumentMapSettings, object> <>9__18_0;
            public static Action<NavigationPaneSettings> <>9__39_0;

            internal void <.cctor>b__36_0(NavigationPaneControl d, NavigationPaneSettings o, NavigationPaneSettings n)
            {
                d.OnSettingsChanged(o, n);
            }

            internal void <.cctor>b__36_1(NavigationPaneControl d, DocumentViewModel o, DocumentViewModel n)
            {
                d.OnDocumentChanged(o, n);
            }

            internal void <.cctor>b__36_2(NavigationPaneControl d, DocumentPreviewControl o, DocumentPreviewControl n)
            {
                d.viewer = n;
            }

            internal object <DevExpress.Xpf.Printing.PreviewControl.Native.INavigationPaneUI.get_SelectedBookmarkNode>b__18_0(DevExpress.Xpf.Printing.PreviewControl.Native.DocumentMapSettings x) => 
                x.SelectedNode;

            internal void <OnSettingsChanged>b__39_0(NavigationPaneSettings x)
            {
                x.AssignNavigationPaneUI(null);
            }
        }
    }
}

