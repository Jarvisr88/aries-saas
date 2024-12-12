namespace DevExpress.Xpf.Printing
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.POCO;
    using DevExpress.Mvvm.UI;
    using DevExpress.Mvvm.UI.Native;
    using DevExpress.Xpf.Bars;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.DocumentViewer;
    using DevExpress.Xpf.Printing.Native;
    using DevExpress.Xpf.Printing.Parameters.Models;
    using DevExpress.Xpf.Printing.PreviewControl;
    using DevExpress.Xpf.Printing.PreviewControl.Native;
    using DevExpress.Xpf.Printing.PreviewControl.Native.Models;
    using DevExpress.Xpf.Printing.PreviewControl.Native.Rendering;
    using DevExpress.Xpf.Printing.Themes;
    using DevExpress.Xpf.Utils.About;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Localization;
    using DevExpress.XtraPrinting.Native;
    using DevExpress.XtraPrinting.Native.Navigation;
    using DevExpress.XtraReports;
    using DevExpress.XtraReports.Parameters;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Threading;

    [DXToolboxBrowsable(true), LicenseProvider(typeof(DX_WPF_ControlRequiredForReports_LicenseProvider))]
    public class DocumentPreviewControl : DocumentViewerControl
    {
        private static readonly System.Type ownerType = typeof(DocumentPreviewControl);
        public static readonly DependencyProperty CursorModeProperty;
        public static readonly DependencyProperty HighlightSelectionColorProperty;
        internal static readonly DependencyPropertyKey HasSelectionPropertyKey;
        public static readonly DependencyProperty HasSelectionProperty;
        internal static readonly DependencyPropertyKey ParametersModelPropertyKey;
        public static readonly DependencyProperty ParametersModelProperty;
        public static readonly DependencyProperty AutoShowParametersPanelProperty;
        public static readonly DependencyProperty AutoShowDocumentMapProperty;
        public static readonly DependencyProperty ShowThumbnailsProperty;
        public static readonly DependencyProperty ShowNavigationPaneProperty;
        public static readonly DependencyProperty DialogServiceTemplateProperty;
        public static readonly DependencyProperty MessageBoxServiceTemplateProperty;
        public static readonly DependencyProperty SaveFileDialogTemplateProperty;
        public static readonly DependencyProperty PageDisplayModeProperty;
        public static readonly DependencyProperty ColumnsCountProperty;
        public static readonly DependencyProperty HiddenExportFormatsProperty;
        public static readonly DependencyProperty RequestDocumentCreationProperty;
        public static readonly DependencyPropertyKey ThumbnailsSettingsPropertyKey;
        public static readonly DependencyProperty ThumbnailsSettingsProperty;
        public static readonly DependencyProperty HighlightEditingFieldsProperty;
        public static readonly DependencyProperty EditingFieldTemplateSelectorProperty;
        public static readonly DependencyProperty ShowCoverPageProperty;
        public static readonly DependencyProperty EnableContinuousScrollingProperty;
        public static readonly DependencyProperty ShowPageMarginsProperty;
        public static readonly DependencyProperty HighlightCurrentPageProperty;
        public static readonly DependencyProperty AllowCachePagesProperty;
        public static readonly DependencyProperty AllowDocumentEditingProperty;
        public static readonly DependencyProperty DocumentMapSettingsProperty;
        public static readonly DependencyProperty UseOfficeInspiredNavigationPaneProperty;
        public static readonly DependencyProperty NavigationPaneSettingsProperty;
        public static readonly DependencyPropertyKey ActualNavigationPaneSettingsPropertyKey;
        public static readonly DependencyProperty ActualNavigationPaneSettingsProperty;
        public static RoutedEvent CursorModeChangedEvent;
        public static RoutedEvent DocumentPreviewMouseClickEvent;
        public static RoutedEvent DocumentPreviewMouseDoubleClickEvent;
        public static RoutedEvent DocumentPreviewMouseMoveEvent;
        public static RoutedEvent SelectionStartedEvent;
        public static RoutedEvent SelectionContinuedEvent;
        public static RoutedEvent SelectionEndedEvent;
        public static RoutedEvent DocumentLoadedEvent;

        public event RoutedEventHandler CursorModeChanged
        {
            add
            {
                base.AddHandler(CursorModeChangedEvent, value);
            }
            remove
            {
                base.RemoveHandler(CursorModeChangedEvent, value);
            }
        }

        public event RoutedEventHandler DocumentLoaded
        {
            add
            {
                base.AddHandler(DocumentLoadedEvent, value);
            }
            remove
            {
                base.RemoveHandler(DocumentLoadedEvent, value);
            }
        }

        public event DocumentPreviewMouseEventHandler DocumentPreviewMouseClick
        {
            add
            {
                base.AddHandler(DocumentPreviewMouseClickEvent, value);
            }
            remove
            {
                base.RemoveHandler(DocumentPreviewMouseClickEvent, value);
            }
        }

        public event DocumentPreviewMouseEventHandler DocumentPreviewMouseDoubleClick
        {
            add
            {
                base.AddHandler(DocumentPreviewMouseDoubleClickEvent, value);
            }
            remove
            {
                base.RemoveHandler(DocumentPreviewMouseDoubleClickEvent, value);
            }
        }

        public event DocumentPreviewMouseEventHandler DocumentPreviewMouseMove
        {
            add
            {
                base.AddHandler(DocumentPreviewMouseMoveEvent, value);
            }
            remove
            {
                base.RemoveHandler(DocumentPreviewMouseMoveEvent, value);
            }
        }

        public event RoutedEventHandler SelectionContinued
        {
            add
            {
                base.AddHandler(SelectionContinuedEvent, value);
            }
            remove
            {
                base.RemoveHandler(SelectionContinuedEvent, value);
            }
        }

        public event RoutedEventHandler SelectionEnded
        {
            add
            {
                base.AddHandler(SelectionEndedEvent, value);
            }
            remove
            {
                base.RemoveHandler(SelectionEndedEvent, value);
            }
        }

        public event RoutedEventHandler SelectionStarted
        {
            add
            {
                base.AddHandler(SelectionStartedEvent, value);
            }
            remove
            {
                base.RemoveHandler(SelectionStartedEvent, value);
            }
        }

        static DocumentPreviewControl()
        {
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(DocumentPreviewControl), "owner");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            FrameworkPropertyMetadataOptions? frameworkOptions = null;
            DependencyPropertyRegistrator<DocumentPreviewControl> registrator1 = DependencyPropertyRegistrator<DocumentPreviewControl>.New().Register<CursorModeType>(System.Linq.Expressions.Expression.Lambda<Func<DocumentPreviewControl, CursorModeType>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DocumentPreviewControl.get_CursorMode)), parameters), out CursorModeProperty, CursorModeType.SelectTool, (owner, oldValue, newValue) => owner.OnCursorModeChanged(newValue), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(DocumentPreviewControl), "owner");
            ParameterExpression[] expressionArray2 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<DocumentPreviewControl> registrator2 = registrator1.Register<Color>(System.Linq.Expressions.Expression.Lambda<Func<DocumentPreviewControl, Color>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DocumentPreviewControl.get_HighlightSelectionColor)), expressionArray2), out HighlightSelectionColorProperty, Color.FromArgb(0x59, 0x60, 0x98, 0xc0), (owner, oldValue, newValue) => owner.OnSelectionColorChanged(newValue), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(DocumentPreviewControl), "owner");
            ParameterExpression[] expressionArray3 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<DocumentPreviewControl> registrator3 = registrator2.RegisterReadOnly<bool>(System.Linq.Expressions.Expression.Lambda<Func<DocumentPreviewControl, bool>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DocumentPreviewControl.get_HasSelection)), expressionArray3), out HasSelectionPropertyKey, out HasSelectionProperty, false, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(DocumentPreviewControl), "owner");
            ParameterExpression[] expressionArray4 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<DocumentPreviewControl> registrator4 = registrator3.RegisterReadOnly<DevExpress.Xpf.Printing.Parameters.Models.ParametersModel>(System.Linq.Expressions.Expression.Lambda<Func<DocumentPreviewControl, DevExpress.Xpf.Printing.Parameters.Models.ParametersModel>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DocumentPreviewControl.get_ParametersModel)), expressionArray4), out ParametersModelPropertyKey, out ParametersModelProperty, null, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(DocumentPreviewControl), "owner");
            ParameterExpression[] expressionArray5 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<DocumentPreviewControl> registrator5 = registrator4.Register<bool>(System.Linq.Expressions.Expression.Lambda<Func<DocumentPreviewControl, bool>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DocumentPreviewControl.get_AutoShowParametersPanel)), expressionArray5), out AutoShowParametersPanelProperty, true, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(DocumentPreviewControl), "owner");
            ParameterExpression[] expressionArray6 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<DocumentPreviewControl> registrator6 = registrator5.Register<bool>(System.Linq.Expressions.Expression.Lambda<Func<DocumentPreviewControl, bool>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DocumentPreviewControl.get_AutoShowDocumentMap)), expressionArray6), out AutoShowDocumentMapProperty, true, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(DocumentPreviewControl), "owner");
            ParameterExpression[] expressionArray7 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<DocumentPreviewControl> registrator7 = registrator6.Register<bool>(System.Linq.Expressions.Expression.Lambda<Func<DocumentPreviewControl, bool>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DocumentPreviewControl.get_ShowThumbnails)), expressionArray7), out ShowThumbnailsProperty, false, delegate (DocumentPreviewControl d) {
                Action<DocumentCommandProvider> action = <>c.<>9__161_3;
                if (<>c.<>9__161_3 == null)
                {
                    Action<DocumentCommandProvider> local1 = <>c.<>9__161_3;
                    action = <>c.<>9__161_3 = x => x.UpdateCommands();
                }
                d.ActualCommandProvider.Do<DocumentCommandProvider>(action);
            }, (d, newValue) => !d.Document.IsRemoteReportDocumentSource() && newValue, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(DocumentPreviewControl), "owner");
            ParameterExpression[] expressionArray8 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<DocumentPreviewControl> registrator8 = registrator7.Register<DataTemplate>(System.Linq.Expressions.Expression.Lambda<Func<DocumentPreviewControl, DataTemplate>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DocumentPreviewControl.get_DialogServiceTemplate)), expressionArray8), out DialogServiceTemplateProperty, null, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(DocumentPreviewControl), "owner");
            ParameterExpression[] expressionArray9 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<DocumentPreviewControl> registrator9 = registrator8.Register<DataTemplate>(System.Linq.Expressions.Expression.Lambda<Func<DocumentPreviewControl, DataTemplate>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DocumentPreviewControl.get_MessageBoxServiceTemplate)), expressionArray9), out MessageBoxServiceTemplateProperty, null, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(DocumentPreviewControl), "owner");
            ParameterExpression[] expressionArray10 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<DocumentPreviewControl> registrator10 = registrator9.Register<DataTemplate>(System.Linq.Expressions.Expression.Lambda<Func<DocumentPreviewControl, DataTemplate>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DocumentPreviewControl.get_SaveFileDialogTemplate)), expressionArray10), out SaveFileDialogTemplateProperty, null, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(DocumentPreviewControl), "owner");
            ParameterExpression[] expressionArray11 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<DocumentPreviewControl> registrator11 = registrator10.Register<DevExpress.Xpf.DocumentViewer.PageDisplayMode>(System.Linq.Expressions.Expression.Lambda<Func<DocumentPreviewControl, DevExpress.Xpf.DocumentViewer.PageDisplayMode>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DocumentPreviewControl.get_PageDisplayMode)), expressionArray11), out PageDisplayModeProperty, DevExpress.Xpf.DocumentViewer.PageDisplayMode.Single, owner => owner.OnPageLayoutChanged(), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(DocumentPreviewControl), "owner");
            ParameterExpression[] expressionArray12 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<DocumentPreviewControl> registrator12 = registrator11.Register<int>(System.Linq.Expressions.Expression.Lambda<Func<DocumentPreviewControl, int>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DocumentPreviewControl.get_ColumnsCount)), expressionArray12), out ColumnsCountProperty, 1, x => x.OnPageLayoutChanged(), (owner, newValue) => (newValue > 1) ? newValue : 1, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(DocumentPreviewControl), "owner");
            ParameterExpression[] expressionArray13 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<DocumentPreviewControl> registrator13 = registrator12.Register<ObservableCollection<ExportFormat>>(System.Linq.Expressions.Expression.Lambda<Func<DocumentPreviewControl, ObservableCollection<ExportFormat>>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DocumentPreviewControl.get_HiddenExportFormats)), expressionArray13), out HiddenExportFormatsProperty, null, (owner, oldValue, newValue) => owner.OnHiddenFormatsChanged(oldValue, newValue), (owner, newValue) => owner.CoerceHiddenFormats(newValue), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(DocumentPreviewControl), "owner");
            ParameterExpression[] expressionArray14 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<DocumentPreviewControl> registrator14 = registrator13.Register<bool>(System.Linq.Expressions.Expression.Lambda<Func<DocumentPreviewControl, bool>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DocumentPreviewControl.get_RequestDocumentCreation)), expressionArray14), out RequestDocumentCreationProperty, false, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(DocumentPreviewControl), "owner");
            ParameterExpression[] expressionArray15 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<DocumentPreviewControl> registrator15 = registrator14.RegisterReadOnly<ThumbnailsViewerSettings>(System.Linq.Expressions.Expression.Lambda<Func<DocumentPreviewControl, ThumbnailsViewerSettings>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DocumentPreviewControl.get_ThumbnailsSettings)), expressionArray15), out ThumbnailsSettingsPropertyKey, out ThumbnailsSettingsProperty, null, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(DocumentPreviewControl), "owner");
            ParameterExpression[] expressionArray16 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<DocumentPreviewControl> registrator16 = registrator15.Register<bool>(System.Linq.Expressions.Expression.Lambda<Func<DocumentPreviewControl, bool>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DocumentPreviewControl.get_HighlightEditingFields)), expressionArray16), out HighlightEditingFieldsProperty, false, delegate (DocumentPreviewControl d) {
                Action<DevExpress.Xpf.Printing.DocumentPresenterControl> action = <>c.<>9__161_11;
                if (<>c.<>9__161_11 == null)
                {
                    Action<DevExpress.Xpf.Printing.DocumentPresenterControl> local1 = <>c.<>9__161_11;
                    action = <>c.<>9__161_11 = x => x.Update();
                }
                d.DocumentPresenter.Do<DevExpress.Xpf.Printing.DocumentPresenterControl>(action);
            }, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(DocumentPreviewControl), "owner");
            ParameterExpression[] expressionArray17 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<DocumentPreviewControl> registrator17 = registrator16.Register<DevExpress.Xpf.Printing.EditingFieldTemplateSelector>(System.Linq.Expressions.Expression.Lambda<Func<DocumentPreviewControl, DevExpress.Xpf.Printing.EditingFieldTemplateSelector>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DocumentPreviewControl.get_EditingFieldTemplateSelector)), expressionArray17), out EditingFieldTemplateSelectorProperty, new DevExpress.Xpf.Printing.EditingFieldTemplateSelector(), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(DocumentPreviewControl), "owner");
            ParameterExpression[] expressionArray18 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<DocumentPreviewControl> registrator18 = registrator17.Register<bool>(System.Linq.Expressions.Expression.Lambda<Func<DocumentPreviewControl, bool>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DocumentPreviewControl.get_ShowCoverPage)), expressionArray18), out ShowCoverPageProperty, false, d => d.AssignDocumentPresenterProperties(), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(DocumentPreviewControl), "owner");
            ParameterExpression[] expressionArray19 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<DocumentPreviewControl> registrator19 = registrator18.Register<bool>(System.Linq.Expressions.Expression.Lambda<Func<DocumentPreviewControl, bool>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DocumentPreviewControl.get_EnableContinuousScrolling)), expressionArray19), out EnableContinuousScrollingProperty, true, d => d.OnPageLayoutChanged(), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(DocumentPreviewControl), "owner");
            ParameterExpression[] expressionArray20 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<DocumentPreviewControl> registrator20 = registrator19.Register<bool>(System.Linq.Expressions.Expression.Lambda<Func<DocumentPreviewControl, bool>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DocumentPreviewControl.get_ShowPageMargins)), expressionArray20), out ShowPageMarginsProperty, true, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(DocumentPreviewControl), "owner");
            ParameterExpression[] expressionArray21 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<DocumentPreviewControl> registrator21 = registrator20.Register<bool>(System.Linq.Expressions.Expression.Lambda<Func<DocumentPreviewControl, bool>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DocumentPreviewControl.get_HighlightCurrentPage)), expressionArray21), out HighlightCurrentPageProperty, true, d => d.OnHighlightCurrentPageChanged(), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(DocumentPreviewControl), "owner");
            ParameterExpression[] expressionArray22 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<DocumentPreviewControl> registrator22 = registrator21.Register<bool>(System.Linq.Expressions.Expression.Lambda<Func<DocumentPreviewControl, bool>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DocumentPreviewControl.get_AllowCachePages)), expressionArray22), out AllowCachePagesProperty, false, d => d.OnAllowCachePagesChanges(), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(DocumentPreviewControl), "owner");
            ParameterExpression[] expressionArray23 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<DocumentPreviewControl> registrator23 = registrator22.Register<bool>(System.Linq.Expressions.Expression.Lambda<Func<DocumentPreviewControl, bool>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DocumentPreviewControl.get_AllowDocumentEditing)), expressionArray23), out AllowDocumentEditingProperty, true, d => d.OnAllowDocumentEditingChanged(), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(DocumentPreviewControl), "owner");
            ParameterExpression[] expressionArray24 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<DocumentPreviewControl> registrator24 = registrator23.Register<DevExpress.Xpf.Printing.PreviewControl.Native.DocumentMapSettings>(System.Linq.Expressions.Expression.Lambda<Func<DocumentPreviewControl, DevExpress.Xpf.Printing.PreviewControl.Native.DocumentMapSettings>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DocumentPreviewControl.get_DocumentMapSettings)), expressionArray24), out DocumentMapSettingsProperty, null, d => d.OnDocumentMapSettingsChanged(), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(DocumentPreviewControl), "owner");
            ParameterExpression[] expressionArray25 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<DocumentPreviewControl> registrator25 = registrator24.Register<DevExpress.Xpf.Printing.NavigationPaneSettings>(System.Linq.Expressions.Expression.Lambda<Func<DocumentPreviewControl, DevExpress.Xpf.Printing.NavigationPaneSettings>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DocumentPreviewControl.get_NavigationPaneSettings)), expressionArray25), out NavigationPaneSettingsProperty, null, d => d.OnNavigationPaneSettingsChanged(), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(DocumentPreviewControl), "owner");
            ParameterExpression[] expressionArray26 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<DocumentPreviewControl> registrator26 = registrator25.RegisterReadOnly<DevExpress.Xpf.Printing.NavigationPaneSettings>(System.Linq.Expressions.Expression.Lambda<Func<DocumentPreviewControl, DevExpress.Xpf.Printing.NavigationPaneSettings>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DocumentPreviewControl.get_ActualNavigationPaneSettings)), expressionArray26), out ActualNavigationPaneSettingsPropertyKey, out ActualNavigationPaneSettingsProperty, null, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(DocumentPreviewControl), "d");
            ParameterExpression[] expressionArray27 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<DocumentPreviewControl> registrator27 = registrator26.Register<bool>(System.Linq.Expressions.Expression.Lambda<Func<DocumentPreviewControl, bool>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DocumentPreviewControl.get_UseOfficeInspiredNavigationPane)), expressionArray27), out UseOfficeInspiredNavigationPaneProperty, true, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(DocumentPreviewControl), "d");
            ParameterExpression[] expressionArray28 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            registrator27.Register<bool>(System.Linq.Expressions.Expression.Lambda<Func<DocumentPreviewControl, bool>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DocumentPreviewControl.get_ShowNavigationPane)), expressionArray28), out ShowNavigationPaneProperty, false, delegate (DocumentPreviewControl d) {
                Action<DocumentCommandProvider> action = <>c.<>9__161_20;
                if (<>c.<>9__161_20 == null)
                {
                    Action<DocumentCommandProvider> local1 = <>c.<>9__161_20;
                    action = <>c.<>9__161_20 = x => x.UpdateCommands();
                }
                d.ActualCommandProvider.Do<DocumentCommandProvider>(action);
            }, frameworkOptions).OverrideMetadata(BarNameScope.IsScopeOwnerProperty, true, null, FrameworkPropertyMetadataOptions.None);
            CursorModeChangedEvent = EventManager.RegisterRoutedEvent("CursorModeChanged", RoutingStrategy.Direct, typeof(RoutedEventHandler), ownerType);
            DocumentPreviewMouseClickEvent = EventManager.RegisterRoutedEvent("DocumentPreviewMouseClick", RoutingStrategy.Direct, typeof(DocumentPreviewMouseEventHandler), ownerType);
            DocumentPreviewMouseDoubleClickEvent = EventManager.RegisterRoutedEvent("DocumentPreviewMouseDoubleClick", RoutingStrategy.Direct, typeof(DocumentPreviewMouseEventHandler), ownerType);
            DocumentPreviewMouseMoveEvent = EventManager.RegisterRoutedEvent("DocumentPreviewMouseMove", RoutingStrategy.Direct, typeof(DocumentPreviewMouseEventHandler), ownerType);
            SelectionStartedEvent = EventManager.RegisterRoutedEvent("SelectionStarted", RoutingStrategy.Direct, typeof(RoutedEventHandler), ownerType);
            SelectionContinuedEvent = EventManager.RegisterRoutedEvent("SelectionContinued", RoutingStrategy.Direct, typeof(RoutedEventHandler), ownerType);
            SelectionEndedEvent = EventManager.RegisterRoutedEvent("SelectionEnded", RoutingStrategy.Direct, typeof(RoutedEventHandler), ownerType);
            DocumentLoadedEvent = EventManager.RegisterRoutedEvent("DocumentLoaded", RoutingStrategy.Direct, typeof(RoutedEventHandler), ownerType);
            BrickResolver.EnsureStaticConstructor();
        }

        public DocumentPreviewControl()
        {
            this.Cache = new TextureCache();
            this.HiddenExportFormats = new ObservableCollection<ExportFormat>();
            base.DefaultStyleKey = typeof(DocumentPreviewControl);
            this.CreateParametersModel();
            this.ThumbnailsSettings = new ThumbnailsViewerSettings();
            this.ThumbnailsSettings.Initialize(this);
            this.ActualNavigationPaneSettings = this.CreateDefaultNavigationPaneSettings();
        }

        [CompilerGenerated, DebuggerHidden]
        private bool <>n__0(bool? show) => 
            base.CanShowFindText(show);

        [CompilerGenerated, DebuggerHidden]
        private bool <>n__1(double zoomFactor) => 
            base.CanSetZoomFactor(zoomFactor);

        protected override void AssignDocumentPresenterProperties()
        {
            base.AssignDocumentPresenterProperties();
            this.AssignPageLayoutSettings();
        }

        private void AssignPageLayoutSettings()
        {
            Action<DocumentCommandProvider> action = <>c.<>9__353_0;
            if (<>c.<>9__353_0 == null)
            {
                Action<DocumentCommandProvider> local1 = <>c.<>9__353_0;
                action = <>c.<>9__353_0 = x => x.UpdatePageLayoutCommands();
            }
            this.ActualCommandProvider.Do<DocumentCommandProvider>(action);
            this.DocumentPresenter.Do<DevExpress.Xpf.Printing.DocumentPresenterControl>(delegate (DevExpress.Xpf.Printing.DocumentPresenterControl x) {
                x.PageDisplayMode = this.PageDisplayMode;
                x.ColumnsCount = (this.PageDisplayMode == DevExpress.Xpf.DocumentViewer.PageDisplayMode.Columns) ? this.ColumnsCount : x.ColumnsCount;
                x.ShowCoverPage = ((this.PageDisplayMode == DevExpress.Xpf.DocumentViewer.PageDisplayMode.Columns) && (x.ColumnsCount == 2)) && this.ShowCoverPage;
                x.ShowSingleItem = ((this.PageDisplayMode == DevExpress.Xpf.DocumentViewer.PageDisplayMode.Single) || (this.PageDisplayMode == DevExpress.Xpf.DocumentViewer.PageDisplayMode.Columns)) ? !this.EnableContinuousScrolling : false;
                x.HighlightSelectionColor = this.HighlightSelectionColor;
            });
        }

        protected override bool CanClockwiseRotate() => 
            false;

        protected virtual bool CanCopy() => 
            this.HasSelection;

        public virtual bool CanExport(ExportFormat? format)
        {
            if ((this.Document == null) || (!this.Document.IsCreated || this.IsInProgress))
            {
                return false;
            }
            IEnumerable<ExportFormat> first = ExportOptionsViewModelBase.AllExportFormats();
            if (format != null)
            {
                ExportFormat? nullable = format;
                ExportFormat csv = ExportFormat.Csv;
                if (!((((ExportFormat) nullable.GetValueOrDefault()) == csv) ? (nullable != null) : false))
                {
                    nullable = format;
                    csv = ExportFormat.Txt;
                    if (!((((ExportFormat) nullable.GetValueOrDefault()) == csv) ? (nullable != null) : false))
                    {
                        goto TR_0001;
                    }
                }
                if (!this.Document.PrintingSystem.Document.CanPerformContinuousExport)
                {
                    return false;
                }
            }
        TR_0001:
            return first.Except<ExportFormat>(this.HiddenExportFormats).Any<ExportFormat>();
        }

        protected virtual bool CanGoToFirstPage()
        {
            Func<bool> fallback = <>c.<>9__279_1;
            if (<>c.<>9__279_1 == null)
            {
                Func<bool> local1 = <>c.<>9__279_1;
                fallback = <>c.<>9__279_1 = () => false;
            }
            return this.Document.Return<DevExpress.Xpf.Printing.PreviewControl.IDocumentViewModel, bool>(x => ((base.PageCount > 0) && (base.CurrentPageNumber > 1)), fallback);
        }

        protected virtual bool CanGoToLastPage()
        {
            Func<bool> fallback = <>c.<>9__280_1;
            if (<>c.<>9__280_1 == null)
            {
                Func<bool> local1 = <>c.<>9__280_1;
                fallback = <>c.<>9__280_1 = () => false;
            }
            return this.Document.Return<DevExpress.Xpf.Printing.PreviewControl.IDocumentViewModel, bool>(x => (x.IsCreated && ((base.PageCount != 0) && (base.CurrentPageNumber < base.PageCount))), fallback);
        }

        protected virtual bool CanPageSetup()
        {
            if (!(base.DocumentSource is ILink))
            {
                return false;
            }
            Func<bool> fallback = <>c.<>9__271_1;
            if (<>c.<>9__271_1 == null)
            {
                Func<bool> local1 = <>c.<>9__271_1;
                fallback = <>c.<>9__271_1 = () => false;
            }
            return this.Document.Return<DevExpress.Xpf.Printing.PreviewControl.IDocumentViewModel, bool>(x => (x.IsCreated && (!this.IsInProgress && x.CanChangePageSettings)), fallback);
        }

        public virtual bool CanPrint()
        {
            Func<DevExpress.Xpf.Printing.PreviewControl.IDocumentViewModel, bool> evaluator = <>c.<>9__266_0;
            if (<>c.<>9__266_0 == null)
            {
                Func<DevExpress.Xpf.Printing.PreviewControl.IDocumentViewModel, bool> local1 = <>c.<>9__266_0;
                evaluator = <>c.<>9__266_0 = x => x.IsCreated && !((IProgressSettings) x).InProgress;
            }
            return this.Document.Return<DevExpress.Xpf.Printing.PreviewControl.IDocumentViewModel, bool>(evaluator, (<>c.<>9__266_1 ??= () => false));
        }

        public virtual bool CanPrintDirect()
        {
            Func<bool> fallback = <>c.<>9__268_1;
            if (<>c.<>9__268_1 == null)
            {
                Func<bool> local1 = <>c.<>9__268_1;
                fallback = <>c.<>9__268_1 = () => false;
            }
            return this.Document.Return<DevExpress.Xpf.Printing.PreviewControl.IDocumentViewModel, bool>(x => (x.IsCreated && !this.IsInProgress), fallback);
        }

        public virtual bool CanSave()
        {
            Func<bool> fallback = <>c.<>9__262_1;
            if (<>c.<>9__262_1 == null)
            {
                Func<bool> local1 = <>c.<>9__262_1;
                fallback = <>c.<>9__262_1 = () => false;
            }
            return this.Document.Return<DevExpress.Xpf.Printing.PreviewControl.IDocumentViewModel, bool>(x => (x.IsCreated && !this.IsInProgress), fallback);
        }

        protected virtual bool CanScale()
        {
            if (!(base.DocumentSource is ILink))
            {
                return false;
            }
            Func<bool> fallback = <>c.<>9__277_1;
            if (<>c.<>9__277_1 == null)
            {
                Func<bool> local1 = <>c.<>9__277_1;
                fallback = <>c.<>9__277_1 = () => false;
            }
            return this.Document.Return<DevExpress.Xpf.Printing.PreviewControl.IDocumentViewModel, bool>(x => (x.IsCreated && (!this.IsInProgress && (x.CanChangePageSettings && (!x.IsLegacyLinkDocumentSource() && !x.IsRemoteReportDocumentSource())))), fallback);
        }

        public virtual bool CanSend(ExportFormat? format)
        {
            if ((this.Document == null) || (!this.Document.IsCreated || this.IsInProgress))
            {
                return false;
            }
            IEnumerable<ExportFormat> first = ExportOptionsViewModelBase.AllExportFormats().Except<ExportFormat>(ExportFormat.Htm.Yield<ExportFormat>());
            if (format != null)
            {
                ExportFormat? nullable = format;
                ExportFormat csv = ExportFormat.Csv;
                if (!((((ExportFormat) nullable.GetValueOrDefault()) == csv) ? (nullable != null) : false))
                {
                    nullable = format;
                    csv = ExportFormat.Txt;
                    if (!((((ExportFormat) nullable.GetValueOrDefault()) == csv) ? (nullable != null) : false))
                    {
                        goto TR_0001;
                    }
                }
                if (!this.Document.PrintingSystem.Document.CanPerformContinuousExport)
                {
                    return false;
                }
            }
        TR_0001:
            return first.Except<ExportFormat>(this.HiddenExportFormats).Any<ExportFormat>();
        }

        protected virtual bool CanSetCursorMode(CursorModeType cursorMode)
        {
            Func<DevExpress.Xpf.Printing.PreviewControl.IDocumentViewModel, bool> evaluator = <>c.<>9__299_0;
            if (<>c.<>9__299_0 == null)
            {
                Func<DevExpress.Xpf.Printing.PreviewControl.IDocumentViewModel, bool> local1 = <>c.<>9__299_0;
                evaluator = <>c.<>9__299_0 = x => x.IsCreated;
            }
            return this.Document.Return<DevExpress.Xpf.Printing.PreviewControl.IDocumentViewModel, bool>(evaluator, (<>c.<>9__299_1 ??= () => false));
        }

        protected virtual bool CanSetPageLayout(PageLayoutSettings settings) => 
            true;

        protected virtual bool CanSetWatermark()
        {
            Func<bool> fallback = <>c.<>9__274_1;
            if (<>c.<>9__274_1 == null)
            {
                Func<bool> local1 = <>c.<>9__274_1;
                fallback = <>c.<>9__274_1 = () => false;
            }
            return this.Document.Return<DevExpress.Xpf.Printing.PreviewControl.IDocumentViewModel, bool>(x => (!x.IsRemoteReportDocumentSource() && (x.IsCreated && !this.IsInProgress)), fallback);
        }

        protected override bool CanSetZoomFactor(double zoomFactor)
        {
            Func<bool> fallback = <>c.<>9__312_1;
            if (<>c.<>9__312_1 == null)
            {
                Func<bool> local1 = <>c.<>9__312_1;
                fallback = <>c.<>9__312_1 = () => false;
            }
            return this.Document.Return<DevExpress.Xpf.Printing.PreviewControl.IDocumentViewModel, bool>(x => (x.IsLoaded && this.<>n__1(zoomFactor)), fallback);
        }

        protected override bool CanSetZoomMode(ZoomMode zoomMode) => 
            base.CanSetZoomMode(zoomMode);

        protected override bool CanShowFindText(bool? show)
        {
            Func<bool> fallback = <>c.<>9__309_1;
            if (<>c.<>9__309_1 == null)
            {
                Func<bool> local1 = <>c.<>9__309_1;
                fallback = <>c.<>9__309_1 = () => false;
            }
            return this.Document.Return<DevExpress.Xpf.Printing.PreviewControl.IDocumentViewModel, bool>(x => (!x.IsRemoteReportDocumentSource() && (x.IsCreated && this.<>n__0(show))), fallback);
        }

        public virtual bool CanStopBuilding()
        {
            Func<DevExpress.Xpf.Printing.PreviewControl.IDocumentViewModel, bool> evaluator = <>c.<>9__316_0;
            if (<>c.<>9__316_0 == null)
            {
                Func<DevExpress.Xpf.Printing.PreviewControl.IDocumentViewModel, bool> local1 = <>c.<>9__316_0;
                evaluator = <>c.<>9__316_0 = x => x.CanStopPageBuilding;
            }
            return this.Document.Return<DevExpress.Xpf.Printing.PreviewControl.IDocumentViewModel, bool>(evaluator, (<>c.<>9__316_1 ??= () => false));
        }

        protected virtual bool CanToggleDocumentMap()
        {
            Func<DevExpress.Xpf.Printing.PreviewControl.IDocumentViewModel, bool> evaluator = <>c.<>9__292_0;
            if (<>c.<>9__292_0 == null)
            {
                Func<DevExpress.Xpf.Printing.PreviewControl.IDocumentViewModel, bool> local1 = <>c.<>9__292_0;
                evaluator = <>c.<>9__292_0 = x => x.HasBookmarks;
            }
            return this.Document.Return<DevExpress.Xpf.Printing.PreviewControl.IDocumentViewModel, bool>(evaluator, (<>c.<>9__292_1 ??= () => false));
        }

        protected virtual bool CanToggleEditingFields()
        {
            Func<DevExpress.Xpf.Printing.PreviewControl.IDocumentViewModel, bool> evaluator = <>c.<>9__298_0;
            if (<>c.<>9__298_0 == null)
            {
                Func<DevExpress.Xpf.Printing.PreviewControl.IDocumentViewModel, bool> local1 = <>c.<>9__298_0;
                evaluator = <>c.<>9__298_0 = x => x.EditingFields.Any<EditingField>();
            }
            return this.Document.Return<DevExpress.Xpf.Printing.PreviewControl.IDocumentViewModel, bool>(evaluator, (<>c.<>9__298_1 ??= () => false));
        }

        protected virtual bool CanToggleEnableContinuousScrolling() => 
            (this.PageDisplayMode == DevExpress.Xpf.DocumentViewer.PageDisplayMode.Single) || (this.PageDisplayMode == DevExpress.Xpf.DocumentViewer.PageDisplayMode.Columns);

        protected internal virtual bool CanToggleNavigationPane() => 
            (this.Document != null) ? (!this.Document.IsRemoteReportDocumentSource() || this.Document.HasBookmarks) : false;

        protected virtual bool CanToggleParametersPanel() => 
            this.ParametersModel.HasVisibleParameters;

        protected virtual bool CanToggleShowCoverPage() => 
            (this.PageDisplayMode == DevExpress.Xpf.DocumentViewer.PageDisplayMode.Columns) && (this.ColumnsCount == 2);

        protected virtual bool CanToggleThumbnails()
        {
            Func<DevExpress.Xpf.Printing.PreviewControl.IDocumentViewModel, bool> evaluator = <>c.<>9__296_0;
            if (<>c.<>9__296_0 == null)
            {
                Func<DevExpress.Xpf.Printing.PreviewControl.IDocumentViewModel, bool> local1 = <>c.<>9__296_0;
                evaluator = <>c.<>9__296_0 = x => !x.IsRemoteReportDocumentSource() && x.Pages.Any<PageViewModel>();
            }
            return this.Document.Return<DevExpress.Xpf.Printing.PreviewControl.IDocumentViewModel, bool>(evaluator, (<>c.<>9__296_1 ??= () => false));
        }

        protected override bool CanZoomIn()
        {
            Func<bool> fallback = <>c.<>9__310_1;
            if (<>c.<>9__310_1 == null)
            {
                Func<bool> local1 = <>c.<>9__310_1;
                fallback = <>c.<>9__310_1 = () => false;
            }
            return this.Document.Return<DevExpress.Xpf.Printing.PreviewControl.IDocumentViewModel, bool>(x => (x.IsLoaded && base.CanZoomIn()), fallback);
        }

        protected override bool CanZoomOut()
        {
            Func<bool> fallback = <>c.<>9__311_1;
            if (<>c.<>9__311_1 == null)
            {
                Func<bool> local1 = <>c.<>9__311_1;
                fallback = <>c.<>9__311_1 = () => false;
            }
            return this.Document.Return<DevExpress.Xpf.Printing.PreviewControl.IDocumentViewModel, bool>(x => (x.IsLoaded && base.CanZoomOut()), fallback);
        }

        protected override void ClockwiseRotate()
        {
            throw new NotSupportedException();
        }

        private void CloseActiveEditor()
        {
            if ((this.DocumentPresenter != null) && this.DocumentPresenter.IsInEditing)
            {
                this.DocumentPresenter.InputController.ForceHideEditor();
            }
        }

        private ObservableCollection<ExportFormat> CoerceHiddenFormats(ObservableCollection<ExportFormat> hiddenFormats) => 
            (hiddenFormats != null) ? new ObservableCollection<ExportFormat>(hiddenFormats.Distinct<ExportFormat>()) : new ObservableCollection<ExportFormat>();

        protected virtual void Copy()
        {
            try
            {
                Action<DevExpress.Xpf.Printing.DocumentPresenterControl> action = <>c.<>9__302_0;
                if (<>c.<>9__302_0 == null)
                {
                    Action<DevExpress.Xpf.Printing.DocumentPresenterControl> local1 = <>c.<>9__302_0;
                    action = <>c.<>9__302_0 = x => x.SelectionService.CopyToClipboard();
                }
                this.DocumentPresenter.Do<DevExpress.Xpf.Printing.DocumentPresenterControl>(action);
            }
            catch (Exception exception)
            {
                this.OnDocumentException(this, new DevExpress.XtraPrinting.ExceptionEventArgs(exception));
            }
        }

        protected override BehaviorProvider CreateBehaviorProvider() => 
            new PreviewBehaviorProvider();

        protected override CommandProvider CreateCommandProvider() => 
            new DocumentCommandProvider();

        protected virtual DevExpress.Xpf.Core.DialogService CreateDefaultDialogService()
        {
            NewDocumentViewerThemeKeyExtension resourceKey = new NewDocumentViewerThemeKeyExtension();
            resourceKey.ResourceKey = NewDocumentViewerThemeKeys.ExportOptionsDialogStyle;
            DevExpress.Xpf.Core.DialogService service1 = new DevExpress.Xpf.Core.DialogService();
            service1.DialogStyle = (Style) base.FindResource(resourceKey);
            service1.DialogWindowStartupLocation = WindowStartupLocation.CenterOwner;
            return service1;
        }

        protected override DevExpress.Xpf.DocumentViewer.DocumentMapSettings CreateDefaultDocumentMapSettings() => 
            new DevExpress.Xpf.Printing.PreviewControl.Native.DocumentMapSettings();

        protected virtual DXMessageBoxService CreateDefaultMessageBoxService() => 
            new DXMessageBoxService();

        protected virtual DevExpress.Xpf.Printing.NavigationPaneSettings CreateDefaultNavigationPaneSettings() => 
            new DevExpress.Xpf.Printing.NavigationPaneSettings();

        protected virtual SaveFileDialogService CreateDefaultSaveFileDialogService()
        {
            SaveFileDialogService service1 = new SaveFileDialogService();
            service1.DefaultExt = ".prnx";
            service1.Filter = this.GetOpenFileFilter();
            return service1;
        }

        protected override DevExpress.Xpf.DocumentViewer.IDocument CreateDocument(object source)
        {
            DocumentViewModel documentViewModel = (source is IReport) ? new ReportDocumentViewModel() : new DocumentViewModel();
            this.Subscribe(documentViewModel);
            return documentViewModel;
        }

        private void CreateDocumentCore()
        {
            if ((this.Document != null) && ((this.Document.PrintingSystem != null) && !this.Document.PrintingSystem.IsDisposed))
            {
                this.Document.CreateDocument();
            }
        }

        internal virtual void CreateDocumentIfNeeded()
        {
            if (((!this.Document.IsCachedReportSource() || !this.Document.IsCreating) && !this.Document.IsCreated) && (!DesignerProperties.GetIsInDesignMode(this) && this.RequestDocumentCreation))
            {
                base.Dispatcher.BeginInvoke(() => this.CreateDocumentCore(), DispatcherPriority.Loaded, new object[0]);
            }
        }

        private void CreateParametersModel()
        {
            this.ParametersModel = DevExpress.Xpf.Printing.Parameters.Models.ParametersModel.CreateParametersModel();
            this.ParametersModel.Submit += new EventHandler(this.OnSubmitParameters);
        }

        protected override DevExpress.Xpf.DocumentViewer.PropertyProvider CreatePropertyProvider() => 
            new DocumentPreviewPropertyProvider(delegate {
                Action<DocumentCommandProvider> action = <>c.<>9__325_1;
                if (<>c.<>9__325_1 == null)
                {
                    Action<DocumentCommandProvider> local1 = <>c.<>9__325_1;
                    action = <>c.<>9__325_1 = x => x.UpdateCommands();
                }
                this.ActualCommandProvider.Do<DocumentCommandProvider>(action);
            });

        protected override void ExecuteNavigate(object parameter)
        {
            object page;
            base.ShowFindTextCommand.Execute(false);
            BookmarkNode node1 = parameter as BookmarkNode;
            BookmarkNode node2 = node1;
            if (node1 == null)
            {
                BookmarkNode local1 = node1;
                BookmarkNodeItem item1 = parameter as BookmarkNodeItem;
                if (item1 != null)
                {
                    node2 = item1.BookmarkNode;
                }
                else
                {
                    BookmarkNodeItem local2 = item1;
                    node2 = null;
                }
            }
            BookmarkNode bookmarkNode = node2;
            if (bookmarkNode == null)
            {
                BookmarkNode local3 = bookmarkNode;
                page = null;
            }
            else
            {
                BrickPagePair pair = bookmarkNode.Pair;
                if (pair != null)
                {
                    page = pair.GetPage(this.Document.PrintingSystem.Pages);
                }
                else
                {
                    BrickPagePair local4 = pair;
                    page = null;
                }
            }
            if (page != null)
            {
                this.DocumentPresenter.Do<DevExpress.Xpf.Printing.DocumentPresenterControl>(delegate (DevExpress.Xpf.Printing.DocumentPresenterControl presenter) {
                    presenter.SelectionService.ResetAll();
                    ScrollIntoViewMode? scrollIntoView = null;
                    this.DocumentPresenter.NavigationStrategy.ShowBrick(bookmarkNode.Pair, scrollIntoView);
                });
            }
            BrickPagePair pair2 = parameter as BrickPagePair;
            BrickPagePair brickPagePair = pair2;
            if (pair2 == null)
            {
                BrickPagePair local5 = pair2;
                DevExpress.XtraPrinting.Native.Navigation.SearchData data1 = parameter as DevExpress.XtraPrinting.Native.Navigation.SearchData;
                if (data1 != null)
                {
                    brickPagePair = data1.BrickPagePair;
                }
                else
                {
                    DevExpress.XtraPrinting.Native.Navigation.SearchData local6 = data1;
                    brickPagePair = null;
                }
            }
            BrickPagePair result = brickPagePair;
            if (result != null)
            {
                this.SelectBrick(result, 0);
            }
        }

        protected virtual void Export(ExportOptionsViewModel exportOptionsModel)
        {
            this.Document.Export(exportOptionsModel);
        }

        public virtual void Export(ExportFormat? format)
        {
            if ((this.Document == null) || this.IsInProgress)
            {
                throw new InvalidOperationException();
            }
            ExportFormat format2 = (format != null) ? format.Value : this.Document.DefaultExportFormat;
            ExportOptionsViewModel exportOptionsModel = ExportOptionsViewModel.Create(this.Document.PrintingSystem, format2, this.HiddenExportFormats);
            this.CloseActiveEditor();
            if (exportOptionsModel.ShowOptionsBeforeExport ? this.ShowExportOptionsDialog(exportOptionsModel) : ((exportOptionsModel.Options.PrintPreview.SaveMode == SaveMode.UsingDefaultPath) || exportOptionsModel.SelectFile()))
            {
                this.Export(exportOptionsModel);
            }
        }

        protected override void FindNextText(TextSearchParameter parameter)
        {
            this.DocumentPresenter.SelectionService.ResetSelectedBricks();
            BrickPagePair result = this.Document.PerformSearch(parameter);
            if (result != null)
            {
                ScrollIntoViewMode? scrollIntoView = null;
                this.SelectBrick(result, scrollIntoView);
            }
            else
            {
                Action<DXMessageBoxService> action = <>c.<>9__307_0;
                if (<>c.<>9__307_0 == null)
                {
                    Action<DXMessageBoxService> local1 = <>c.<>9__307_0;
                    action = <>c.<>9__307_0 = service => service.ShowMessage(PrintingLocalizer.GetString(PrintingStringId.SearchFinished_NoMatchesFound), PreviewStringId.Msg_Search_Caption.GetString(), MessageButton.OK, MessageIcon.Information);
                }
                AssignableServiceHelper2<DocumentPreviewControl, DXMessageBoxService>.DoServiceAction(this, this.MessageBoxServiceTemplate.Return<DataTemplate, DXMessageBoxService>(new Func<DataTemplate, DXMessageBoxService>(TemplateHelper.LoadFromTemplate<DXMessageBoxService>), new Func<DXMessageBoxService>(this.CreateDefaultMessageBoxService)), action);
            }
        }

        private static string GetExceptionMessage(Exception e)
        {
            if ((e is TargetInvocationException) && (e.InnerException != null))
            {
                e = e.InnerException;
            }
            else if (e is AggregateException)
            {
                e = e.GetBaseException();
            }
            return e.Message;
        }

        protected override string GetOpenFileFilter() => 
            string.Format("{0} (*{1})|*{1}", PreviewLocalizer.GetString(PreviewStringId.SaveDlg_FilterNativeFormat), ".prnx");

        protected virtual void GoToFirstPage()
        {
            base.CurrentPageNumber = 1;
        }

        protected virtual void GoToLastPage()
        {
            base.CurrentPageNumber = base.PageCount;
        }

        protected override void InitializeCommands()
        {
            base.InitializeCommands();
            this.SetCursorModeCommand = DelegateCommandFactory.Create<CursorModeType>(new Action<CursorModeType>(this.SetCursorMode), new Func<CursorModeType, bool>(this.CanSetCursorMode));
            this.CopyCommand = DelegateCommandFactory.Create(new Action(this.Copy), new Func<bool>(this.CanCopy));
            this.ToggleParametersPanelCommand = DelegateCommandFactory.Create(new Action(this.ToggleParametersPanel), new Func<bool>(this.CanToggleParametersPanel));
            this.ToggleDocumentMapCommand = DelegateCommandFactory.Create(new Action(this.ToggleDocumentMap), new Func<bool>(this.CanToggleDocumentMap));
            this.ToggleThumbnailsCommand = DelegateCommandFactory.Create(new Action(this.ToggleThumbnails), new Func<bool>(this.CanToggleThumbnails));
            this.ToggleNavigationPaneCommand = DelegateCommandFactory.Create(new Action(this.ToggleNavigationPane), new Func<bool>(this.CanToggleNavigationPane));
            this.ToggleEditingFieldsCommand = DelegateCommandFactory.Create(new Action(this.ToggleEditingFields), new Func<bool>(this.CanToggleEditingFields));
            this.SaveCommand = DelegateCommandFactory.Create(new Action(this.Save), new Func<bool>(this.CanSave));
            this.PrintCommand = DelegateCommandFactory.Create(new Action(this.Print), new Func<bool>(this.CanPrint));
            this.PrintDirectCommand = DelegateCommandFactory.Create<string>(new Action<string>(this.PrintDirect), x => this.CanPrintDirect());
            this.PageSetupCommand = DelegateCommandFactory.Create(new Action(this.PageSetup), new Func<bool>(this.CanPageSetup));
            this.ScaleCommand = DelegateCommandFactory.Create(new Action(this.Scale), new Func<bool>(this.CanScale));
            this.FirstPageCommand = DelegateCommandFactory.Create(new Action(this.GoToFirstPage), new Func<bool>(this.CanGoToFirstPage));
            this.LastPageCommand = DelegateCommandFactory.Create(new Action(this.GoToLastPage), new Func<bool>(this.CanGoToLastPage));
            this.ExportCommand = DelegateCommandFactory.Create<ExportFormat?>(new Action<ExportFormat?>(this.Export), new Func<ExportFormat?, bool>(this.CanExport));
            this.SendCommand = DelegateCommandFactory.Create<ExportFormat?>(new Action<ExportFormat?>(this.Send), new Func<ExportFormat?, bool>(this.CanSend));
            this.SetWatermarkCommand = DelegateCommandFactory.Create(new Action(this.SetWatermark), new Func<bool>(this.CanSetWatermark));
            this.StopPageBuildingCommand = DelegateCommandFactory.Create(new Action(this.StopBuilding), new Func<bool>(this.CanStopBuilding));
            this.SetPageLayoutCommand = DelegateCommandFactory.Create<PageLayoutSettings>(new Action<PageLayoutSettings>(this.SetPageLayout), new Func<PageLayoutSettings, bool>(this.CanSetPageLayout));
            this.ToggleShowCoverPageCommand = DelegateCommandFactory.Create(new Action(this.ToggleShowCoverPage), new Func<bool>(this.CanToggleShowCoverPage));
            this.ToggleEnableContinuousScrollingCommand = DelegateCommandFactory.Create(new Action(this.ToggleEnableContinuousScrolling), new Func<bool>(this.CanToggleEnableContinuousScrolling));
        }

        protected sealed override void LoadDocument(object source)
        {
            base.LoadDocument(source);
            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                ((DocumentViewModel) this.Document).Load(source);
                this.RaiseDocumentLoaded();
                base.ActualDocumentMapSettings.UpdateProperties();
                Action<DocumentCommandProvider> action = <>c.<>9__327_0;
                if (<>c.<>9__327_0 == null)
                {
                    Action<DocumentCommandProvider> local1 = <>c.<>9__327_0;
                    action = <>c.<>9__327_0 = x => x.UpdateCommands();
                }
                this.ActualCommandProvider.Do<DocumentCommandProvider>(action);
                if (this.UseOfficeInspiredNavigationPane && this.AutoShowDocumentMap)
                {
                    Func<DevExpress.Xpf.Printing.PreviewControl.IDocumentViewModel, bool> evaluator = <>c.<>9__327_1;
                    if (<>c.<>9__327_1 == null)
                    {
                        Func<DevExpress.Xpf.Printing.PreviewControl.IDocumentViewModel, bool> local2 = <>c.<>9__327_1;
                        evaluator = <>c.<>9__327_1 = x => x.HasBookmarks;
                    }
                    if (this.Document.Return<DevExpress.Xpf.Printing.PreviewControl.IDocumentViewModel, bool>(evaluator, <>c.<>9__327_2 ??= () => false))
                    {
                        this.ActualNavigationPaneSettings.ActiveTab = NavigationPaneTabType.DocumentMap;
                        this.ShowNavigationPane = true;
                    }
                }
                this.CreateDocumentIfNeeded();
            }
        }

        private void OnAllowCachePagesChanges()
        {
            Action<DevExpress.Xpf.Printing.DocumentPresenterControl> action = <>c.<>9__354_0;
            if (<>c.<>9__354_0 == null)
            {
                Action<DevExpress.Xpf.Printing.DocumentPresenterControl> local1 = <>c.<>9__354_0;
                action = <>c.<>9__354_0 = x => x.Renderer.UpdateInnerRenderer();
            }
            this.DocumentPresenter.Do<DevExpress.Xpf.Printing.DocumentPresenterControl>(action);
        }

        private void OnAllowDocumentEditingChanged()
        {
            if (!this.AllowDocumentEditing)
            {
                Action<DevExpress.Xpf.Printing.DocumentPresenterControl> action = <>c.<>9__355_0;
                if (<>c.<>9__355_0 == null)
                {
                    Action<DevExpress.Xpf.Printing.DocumentPresenterControl> local1 = <>c.<>9__355_0;
                    action = <>c.<>9__355_0 = x => x.EditingStrategy.EndEditing();
                }
                this.DocumentPresenter.Do<DevExpress.Xpf.Printing.DocumentPresenterControl>(action);
            }
        }

        protected override void OnCommandProviderChanged(CommandProvider oldValue, CommandProvider newValue)
        {
            if (!(newValue is DocumentCommandProvider))
            {
                base.CommandBarStyle = CommandBarStyle.None;
            }
        }

        protected override void OnCurrentPageNumberChanged(int oldValue, int newValue)
        {
            base.OnCurrentPageNumberChanged(oldValue, newValue);
            if (this.HighlightCurrentPage)
            {
                this.Document.SetCurrentPage(newValue - 1);
            }
        }

        protected void OnCursorModeChanged(CursorModeType cursorModeType)
        {
            Action<DocumentCommandProvider> action = <>c.<>9__345_0;
            if (<>c.<>9__345_0 == null)
            {
                Action<DocumentCommandProvider> local1 = <>c.<>9__345_0;
                action = <>c.<>9__345_0 = x => x.UpdateCommands();
            }
            this.ActualCommandProvider.Do<DocumentCommandProvider>(action);
            if (cursorModeType != CursorModeType.SelectTool)
            {
                Action<DevExpress.Xpf.Printing.DocumentPresenterControl> action2 = <>c.<>9__345_1;
                if (<>c.<>9__345_1 == null)
                {
                    Action<DevExpress.Xpf.Printing.DocumentPresenterControl> local2 = <>c.<>9__345_1;
                    action2 = <>c.<>9__345_1 = x => x.SelectionService.ResetSelectedBricks();
                }
                this.DocumentPresenter.Do<DevExpress.Xpf.Printing.DocumentPresenterControl>(action2);
            }
            this.UpdateSelectionState();
            base.RaiseEvent(new RoutedEventArgs(CursorModeChangedEvent));
        }

        protected override void OnDocumentChanged(DevExpress.Xpf.DocumentViewer.IDocument oldValue, DevExpress.Xpf.DocumentViewer.IDocument newValue)
        {
            this.ParametersModel.AssignParameters(null);
            this.ParametersModel.LookUpValuesProvider = null;
            if (!this.UseOfficeInspiredNavigationPane)
            {
                this.PropertyProvider.ShowDocumentMap = this.AutoShowDocumentMap;
            }
            this.PropertyProvider.ShowParametersPanel = this.AutoShowParametersPanel;
            Action<DocumentCommandProvider> action = <>c.<>9__331_0;
            if (<>c.<>9__331_0 == null)
            {
                Action<DocumentCommandProvider> local1 = <>c.<>9__331_0;
                action = <>c.<>9__331_0 = x => x.UpdateCommands();
            }
            this.ActualCommandProvider.Do<DocumentCommandProvider>(action);
            this.ThumbnailsSettings.RaiseInvalidate();
            this.ThumbnailsSettings.UpdateThumbnailsDocument();
            this.Cache.Reset();
            Action<DocumentViewModel> action2 = <>c.<>9__331_1;
            if (<>c.<>9__331_1 == null)
            {
                Action<DocumentViewModel> local2 = <>c.<>9__331_1;
                action2 = <>c.<>9__331_1 = document => document.NavigationState = null;
            }
            (this.Document as DocumentViewModel).Do<DocumentViewModel>(action2);
        }

        internal override void OnDocumentChangedInternal(DevExpress.Xpf.DocumentViewer.IDocument oldValue, DevExpress.Xpf.DocumentViewer.IDocument newValue)
        {
            DocumentViewModel model1 = oldValue as DocumentViewModel;
            if (model1 == null)
            {
                DocumentViewModel local1 = model1;
            }
            else
            {
                model1.Dispose();
            }
            base.OnDocumentChangedInternal(oldValue, newValue);
        }

        private void OnDocumentCreated(object sender, EventArgs e)
        {
            base.ActualDocumentMapSettings.UpdateProperties();
            ((DelegateCommand) this.ToggleEditingFieldsCommand).RaiseCanExecuteChanged();
            Action<DocumentCommandProvider> action = <>c.<>9__342_0;
            if (<>c.<>9__342_0 == null)
            {
                Action<DocumentCommandProvider> local1 = <>c.<>9__342_0;
                action = <>c.<>9__342_0 = x => x.UpdateCommands();
            }
            this.ActualCommandProvider.Do<DocumentCommandProvider>(action);
            if (this.UseOfficeInspiredNavigationPane && (this.PropertyProvider.ShowDocumentMap && (this.AutoShowDocumentMap && this.Document.HasBookmarks)))
            {
                this.ActualNavigationPaneSettings.ActiveTab = NavigationPaneTabType.DocumentMap;
                this.ShowNavigationPane = true;
            }
            Action<DevExpress.Xpf.Printing.DocumentPresenterControl> action2 = <>c.<>9__342_1;
            if (<>c.<>9__342_1 == null)
            {
                Action<DevExpress.Xpf.Printing.DocumentPresenterControl> local2 = <>c.<>9__342_1;
                action2 = <>c.<>9__342_1 = delegate (DevExpress.Xpf.Printing.DocumentPresenterControl x) {
                    Action<DocumentViewerPanel> action1 = <>c.<>9__342_2;
                    if (<>c.<>9__342_2 == null)
                    {
                        Action<DocumentViewerPanel> local1 = <>c.<>9__342_2;
                        action1 = <>c.<>9__342_2 = panel => panel.UpdateLayout();
                    }
                    x.ItemsPanel.Do<DocumentViewerPanel>(action1);
                    x.Update();
                };
            }
            this.DocumentPresenter.Do<DevExpress.Xpf.Printing.DocumentPresenterControl>(action2);
            this.Document.Do<DevExpress.Xpf.Printing.PreviewControl.IDocumentViewModel>(x => x.SetCurrentPage(base.CurrentPageNumber - 1));
        }

        protected virtual void OnDocumentException(object sender, DevExpress.XtraPrinting.ExceptionEventArgs e)
        {
            AssignableServiceHelper2<DocumentPreviewControl, IMessageBoxService>.DoServiceAction(this, this.MessageBoxServiceTemplate.Return<DataTemplate, IMessageBoxService>(new Func<DataTemplate, IMessageBoxService>(TemplateHelper.LoadFromTemplate<IMessageBoxService>), new Func<IMessageBoxService>(this.CreateDefaultMessageBoxService)), delegate (IMessageBoxService service) {
                Action<IMessageBoxService> <>9__1;
                Action<IMessageBoxService> action = <>9__1;
                if (<>9__1 == null)
                {
                    Action<IMessageBoxService> local1 = <>9__1;
                    action = <>9__1 = x => x.ShowMessage(GetExceptionMessage(e.Exception), PrintingLocalizer.GetString(PrintingStringId.Error), MessageButton.OK, MessageIcon.Error);
                }
                service.Do<IMessageBoxService>(action);
            });
        }

        private void OnDocumentMapSettingsChanged()
        {
            DevExpress.Xpf.Printing.PreviewControl.Native.DocumentMapSettings documentMapSettings = this.DocumentMapSettings;
            DevExpress.Xpf.Printing.PreviewControl.Native.DocumentMapSettings settings2 = documentMapSettings;
            if (documentMapSettings == null)
            {
                DevExpress.Xpf.Printing.PreviewControl.Native.DocumentMapSettings local1 = documentMapSettings;
                settings2 = (DevExpress.Xpf.Printing.PreviewControl.Native.DocumentMapSettings) this.CreateDefaultDocumentMapSettings();
            }
            this.ActualDocumentMapSettings = settings2;
        }

        protected override void OnGotFocus(RoutedEventArgs e)
        {
            base.OnGotFocus(e);
            Action<DevExpress.Xpf.Printing.DocumentPresenterControl> action = <>c.<>9__170_0;
            if (<>c.<>9__170_0 == null)
            {
                Action<DevExpress.Xpf.Printing.DocumentPresenterControl> local1 = <>c.<>9__170_0;
                action = <>c.<>9__170_0 = x => x.Focus();
            }
            this.DocumentPresenter.Do<DevExpress.Xpf.Printing.DocumentPresenterControl>(action);
        }

        private void OnHiddenFormatsChanged(ObservableCollection<ExportFormat> oldValue, ObservableCollection<ExportFormat> newValue)
        {
            oldValue.Do<ObservableCollection<ExportFormat>>(delegate (ObservableCollection<ExportFormat> x) {
                x.CollectionChanged -= new NotifyCollectionChangedEventHandler(this.OnHiddenFormatsCollectionChanged);
            });
            newValue.Do<ObservableCollection<ExportFormat>>(delegate (ObservableCollection<ExportFormat> x) {
                x.CollectionChanged += new NotifyCollectionChangedEventHandler(this.OnHiddenFormatsCollectionChanged);
            });
            Action<DocumentCommandProvider> action = <>c.<>9__348_2;
            if (<>c.<>9__348_2 == null)
            {
                Action<DocumentCommandProvider> local1 = <>c.<>9__348_2;
                action = <>c.<>9__348_2 = x => x.UpdateExportCommands();
            }
            this.ActualCommandProvider.Do<DocumentCommandProvider>(action);
        }

        private void OnHiddenFormatsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Action<DocumentCommandProvider> action = <>c.<>9__349_0;
            if (<>c.<>9__349_0 == null)
            {
                Action<DocumentCommandProvider> local1 = <>c.<>9__349_0;
                action = <>c.<>9__349_0 = x => x.UpdateExportCommands();
            }
            this.ActualCommandProvider.Do<DocumentCommandProvider>(action);
        }

        private void OnHighlightCurrentPageChanged()
        {
            this.Document.SetCurrentPage(this.HighlightCurrentPage ? (base.CurrentPageNumber - 1) : -1);
        }

        protected override void OnLoaded(object sender, RoutedEventArgs e)
        {
            base.OnLoaded(sender, e);
            DocumentViewModel document = this.Document as DocumentViewModel;
            this.Subscribe(document);
            Action<ThumbnailsDocumentViewModel> action = <>c.<>9__169_0;
            if (<>c.<>9__169_0 == null)
            {
                Action<ThumbnailsDocumentViewModel> local1 = <>c.<>9__169_0;
                action = <>c.<>9__169_0 = x => x.AllowSynchronization(true);
            }
            this.ThumbnailsSettings.ThumbnailsDocument.Do<ThumbnailsDocumentViewModel>(action);
        }

        private void OnNavigationPaneSettingsChanged()
        {
            DevExpress.Xpf.Printing.NavigationPaneSettings navigationPaneSettings = this.NavigationPaneSettings;
            DevExpress.Xpf.Printing.NavigationPaneSettings settings2 = navigationPaneSettings;
            if (navigationPaneSettings == null)
            {
                DevExpress.Xpf.Printing.NavigationPaneSettings local1 = navigationPaneSettings;
                settings2 = this.CreateDefaultNavigationPaneSettings();
            }
            this.ActualNavigationPaneSettings = settings2;
        }

        protected virtual void OnPageLayoutChanged()
        {
            ZoomMode zoomMode = base.ActualBehaviorProvider.ZoomMode;
            double zoomFactor = base.ActualBehaviorProvider.ZoomFactor;
            this.AssignDocumentPresenterProperties();
            if (this.PageDisplayMode == DevExpress.Xpf.DocumentViewer.PageDisplayMode.Columns)
            {
                this.SetZoomMode(this.EnableContinuousScrolling ? ZoomMode.FitToWidth : ZoomMode.PageLevel);
            }
            else if (zoomMode != ZoomMode.Custom)
            {
                this.SetZoomMode(zoomMode);
            }
            else
            {
                this.SetZoomFactor(zoomFactor);
            }
        }

        private void OnPagesCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            base.PageCount = this.Document.Pages.Count<PageViewModel>();
            if ((base.CurrentPageNumber == 0) && (base.PageCount > 0))
            {
                base.SetCurrentPageNumber(1);
            }
            if (this.HighlightCurrentPage)
            {
                this.Document.Do<DevExpress.Xpf.Printing.PreviewControl.IDocumentViewModel>(x => x.SetCurrentPage(base.CurrentPageNumber - 1));
            }
        }

        private void OnReportParametersRecieved(object sender, ReportParametersReceivedEventArgs e)
        {
            this.ParametersModel.AssignParameters(e.Parameters);
            this.ParametersModel.LookUpValuesProvider = e.LookUpValuesProvider;
            Action<DocumentCommandProvider> action = <>c.<>9__340_0;
            if (<>c.<>9__340_0 == null)
            {
                Action<DocumentCommandProvider> local1 = <>c.<>9__340_0;
                action = <>c.<>9__340_0 = x => x.UpdateCommands();
            }
            this.ActualCommandProvider.Do<DocumentCommandProvider>(action);
        }

        protected void OnSelectionColorChanged(Color color)
        {
            this.DocumentPresenter.Do<DevExpress.Xpf.Printing.DocumentPresenterControl>(x => x.HighlightSelectionColor = color);
        }

        private void OnStartDocumentCreation(object sender, EventArgs e)
        {
            base.ActualDocumentMapSettings.UpdateProperties();
            Action<DevExpress.Xpf.Printing.DocumentPresenterControl> action = <>c.<>9__341_0;
            if (<>c.<>9__341_0 == null)
            {
                Action<DevExpress.Xpf.Printing.DocumentPresenterControl> local1 = <>c.<>9__341_0;
                action = <>c.<>9__341_0 = delegate (DevExpress.Xpf.Printing.DocumentPresenterControl x) {
                    x.NavigationStrategy.ScrollToStartUp();
                    x.EditingStrategy.EndEditing();
                };
            }
            this.DocumentPresenter.Do<DevExpress.Xpf.Printing.DocumentPresenterControl>(action);
            this.Cache.Reset();
            this.ThumbnailsSettings.RaiseInvalidateTextureCache();
        }

        private void OnSubmitParameters(object sender, EventArgs e)
        {
            (this.Document as ReportDocumentViewModel).Do<ReportDocumentViewModel>(delegate (ReportDocumentViewModel x) {
                Func<ParameterModel, Parameter> selector = <>c.<>9__357_1;
                if (<>c.<>9__357_1 == null)
                {
                    Func<ParameterModel, Parameter> local1 = <>c.<>9__357_1;
                    selector = <>c.<>9__357_1 = p => p.Parameter;
                }
                x.Submit(this.ParametersModel.Parameters.Select<ParameterModel, Parameter>(selector).ToList<Parameter>());
            });
        }

        protected override void OnUnloaded(object sender, RoutedEventArgs e)
        {
            DocumentViewModel document = this.Document as DocumentViewModel;
            this.Unsubscribe(document);
            DocumentViewModel model2 = this.Document as DocumentViewModel;
            if (model2 != null)
            {
                model2.NavigationState = this.DocumentPresenter.NavigationStrategy.GenerateNavigationState();
                model2.ClearPages();
                model2.RaiseDocumentChanged();
            }
            Action<ThumbnailsDocumentViewModel> action = <>c.<>9__168_0;
            if (<>c.<>9__168_0 == null)
            {
                Action<ThumbnailsDocumentViewModel> local1 = <>c.<>9__168_0;
                action = <>c.<>9__168_0 = x => x.AllowSynchronization(false);
            }
            this.ThumbnailsSettings.ThumbnailsDocument.Do<ThumbnailsDocumentViewModel>(action);
            base.OnUnloaded(sender, e);
        }

        protected override void OnZoomFactorChanged(double oldValue, double newValue)
        {
            base.OnZoomFactorChanged(oldValue, newValue);
            Action<DocumentViewModel> action = <>c.<>9__332_0;
            if (<>c.<>9__332_0 == null)
            {
                Action<DocumentViewModel> local1 = <>c.<>9__332_0;
                action = <>c.<>9__332_0 = document => document.NavigationState = null;
            }
            (this.Document as DocumentViewModel).Do<DocumentViewModel>(action);
            Action<DevExpress.Xpf.Printing.DocumentPresenterControl> action2 = <>c.<>9__332_1;
            if (<>c.<>9__332_1 == null)
            {
                Action<DevExpress.Xpf.Printing.DocumentPresenterControl> local2 = <>c.<>9__332_1;
                action2 = <>c.<>9__332_1 = x => x.Update();
            }
            this.DocumentPresenter.Do<DevExpress.Xpf.Printing.DocumentPresenterControl>(action2);
        }

        protected override void OnZoomModeChanged(ZoomMode oldValue, ZoomMode newValue)
        {
            base.OnZoomModeChanged(oldValue, newValue);
            Action<DocumentViewModel> action = <>c.<>9__333_0;
            if (<>c.<>9__333_0 == null)
            {
                Action<DocumentViewModel> local1 = <>c.<>9__333_0;
                action = <>c.<>9__333_0 = document => document.NavigationState = null;
            }
            (this.Document as DocumentViewModel).Do<DocumentViewModel>(action);
        }

        public override void OpenDocument(string filePath = null)
        {
            string str;
            if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
            {
                str = filePath;
            }
            else
            {
                this.CloseActiveEditor();
                OpenFileDialogService service = base.OpenFileDialogTemplate.Return<DataTemplate, OpenFileDialogService>(new Func<DataTemplate, OpenFileDialogService>(TemplateHelper.LoadFromTemplate<OpenFileDialogService>), new Func<OpenFileDialogService>(this.CreateDefaultOpenFileDialogService));
                IFileInfo fileInfo = null;
                AssignableServiceHelper2<DocumentViewerControl, OpenFileDialogService>.DoServiceAction(this, service, delegate (OpenFileDialogService service) {
                    IOpenFileDialogService service2 = service;
                    fileInfo = service2.ShowDialog() ? service2.Files.FirstOrDefault<IFileInfo>() : null;
                });
                if (fileInfo == null)
                {
                    return;
                }
                str = Path.Combine(fileInfo.DirectoryName, fileInfo.Name);
            }
            base.DocumentSource = str;
        }

        protected virtual void PageSetup()
        {
            if (this.Document != null)
            {
                <>c__DisplayClass269_0 class_;
                Func<PageViewModel, bool> predicate = <>c.<>9__269_0;
                if (<>c.<>9__269_0 == null)
                {
                    Func<PageViewModel, bool> local1 = <>c.<>9__269_0;
                    predicate = <>c.<>9__269_0 = page => page.IsSelected;
                }
                PageViewModel selectedPage = this.Document.Pages.Where<PageViewModel>(predicate).FirstOrDefault<PageViewModel>();
                System.Linq.Expressions.Expression[] expressionArray1 = new System.Linq.Expressions.Expression[] { System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Field(System.Linq.Expressions.Expression.Constant(class_, typeof(<>c__DisplayClass269_0)), fieldof(<>c__DisplayClass269_0.selectedPage)), (MethodInfo) methodof(PageViewModelBase.get_Page)), (MethodInfo) methodof(Page.get_PageData)) };
                PageSetupViewModel model = ViewModelSource.Create<PageSetupViewModel>(System.Linq.Expressions.Expression.Lambda<Func<PageSetupViewModel>>(System.Linq.Expressions.Expression.New((ConstructorInfo) methodof(PageSetupViewModel..ctor), (IEnumerable<System.Linq.Expressions.Expression>) expressionArray1), new ParameterExpression[0]));
                this.CloseActiveEditor();
                if (this.ShowPageSetupDialog(model))
                {
                    PageSettingsViewModelHelper.AssignPageDataFromModel(model, selectedPage.Page, this.Document.PrintingSystem);
                }
            }
        }

        public virtual void Print()
        {
            if (this.CanPrint() && !this.IsInProgress)
            {
                PrintOptionsViewModel printModel = PrintOptionsViewModel.Create(this.Document.PrintingSystem);
                printModel.PagePreviewIndex = base.CurrentPageNumber - 1;
                this.CloseActiveEditor();
                this.ShowPrintDialog(printModel);
            }
        }

        protected virtual void Print(PrintOptionsViewModel printModel)
        {
            this.Document.Do<DevExpress.Xpf.Printing.PreviewControl.IDocumentViewModel>(x => x.Print(printModel));
        }

        public virtual void PrintDirect(string printerName = null)
        {
            this.Document.Do<DevExpress.Xpf.Printing.PreviewControl.IDocumentViewModel>(x => x.PrintDirect(printerName));
        }

        protected internal void RaiseDocumentLoaded()
        {
            base.RaiseEvent(new RoutedEventArgs(DocumentLoadedEvent));
        }

        protected internal void RaiseDocumentPreiewMouseClick(DocumentPreviewMouseEventArgs e)
        {
            e.RoutedEvent = DocumentPreviewMouseClickEvent;
            base.RaiseEvent(e);
        }

        protected internal void RaiseDocumentPreiewMouseDoubleClick(DocumentPreviewMouseEventArgs e)
        {
            e.RoutedEvent = DocumentPreviewMouseDoubleClickEvent;
            base.RaiseEvent(e);
        }

        protected internal void RaiseDocumentPreiewMouseMove(DocumentPreviewMouseEventArgs e)
        {
            e.RoutedEvent = DocumentPreviewMouseMoveEvent;
            base.RaiseEvent(e);
        }

        protected internal void RaiseEditingStarted()
        {
        }

        protected internal void RaiseSelectionContinued()
        {
            base.RaiseEvent(new RoutedEventArgs(SelectionContinuedEvent));
        }

        protected internal void RaiseSelectionEnded()
        {
            base.RaiseEvent(new RoutedEventArgs(SelectionEndedEvent));
        }

        protected internal void RaiseSelectionStarted()
        {
            base.RaiseEvent(new RoutedEventArgs(SelectionStartedEvent));
        }

        protected override void ReleaseDocument(DevExpress.Xpf.DocumentViewer.IDocument document)
        {
            base.ReleaseDocument(document);
            (document as DocumentViewModel).Do<DocumentViewModel>(delegate (DocumentViewModel x) {
                Action<ThumbnailsDocumentViewModel> action = <>c.<>9__330_1;
                if (<>c.<>9__330_1 == null)
                {
                    Action<ThumbnailsDocumentViewModel> local1 = <>c.<>9__330_1;
                    action = <>c.<>9__330_1 = d => d.AllowSynchronization(false);
                }
                this.ThumbnailsSettings.ThumbnailsDocument.Do<ThumbnailsDocumentViewModel>(action);
                this.Unsubscribe(x);
            });
        }

        public virtual void Save()
        {
            if (this.CanSave())
            {
                string str;
                this.CloseActiveEditor();
                if (this.ShowSaveDialog(out str) || !string.IsNullOrEmpty(str))
                {
                    this.Document.Save(str);
                }
            }
        }

        protected virtual void Scale()
        {
            if (this.CanScale())
            {
                ScaleOptionsViewModel scaleModel = ScaleOptionsViewModel.Create(this.Document.PrintingSystem);
                this.CloseActiveEditor();
                if (this.ShowScaleDialog(scaleModel))
                {
                    this.Document.Scale(scaleModel);
                }
            }
        }

        private void SelectBrick(BrickPagePair result, ScrollIntoViewMode? scrollIntoView = new ScrollIntoViewMode?())
        {
            this.DocumentPresenter.Do<DevExpress.Xpf.Printing.DocumentPresenterControl>(delegate (DevExpress.Xpf.Printing.DocumentPresenterControl x) {
                x.SelectionService.SelectBrick(result.GetPage(this.Document.PrintingSystem.Pages), result.GetBrick(this.Document.PrintingSystem.Pages));
                x.NavigationStrategy.ShowBrick(result, scrollIntoView);
                x.Update();
            });
        }

        protected virtual void Send(SendOptionsViewModel optionsModel)
        {
            this.Document.Send(optionsModel);
        }

        public virtual void Send(ExportFormat? format)
        {
            if ((this.Document == null) || this.IsInProgress)
            {
                throw new InvalidOperationException();
            }
            ExportFormat format2 = (format != null) ? format.Value : this.Document.DefaultSendFormat;
            SendOptionsViewModel exportOptionsModel = SendOptionsViewModel.Create(this.Document.PrintingSystem, format2, this.HiddenExportFormats);
            this.CloseActiveEditor();
            if (exportOptionsModel.ShowOptionsBeforeExport ? this.ShowExportOptionsDialog(exportOptionsModel) : ((exportOptionsModel.Options.PrintPreview.SaveMode == SaveMode.UsingDefaultPath) || exportOptionsModel.SelectFile()))
            {
                this.Send(exportOptionsModel);
            }
        }

        protected virtual void SetCursorMode(CursorModeType cursorMode)
        {
            this.CursorMode = cursorMode;
            Action<DocumentCommandProvider> action = <>c.<>9__300_0;
            if (<>c.<>9__300_0 == null)
            {
                Action<DocumentCommandProvider> local1 = <>c.<>9__300_0;
                action = <>c.<>9__300_0 = x => x.UpdateCommands();
            }
            this.ActualCommandProvider.Do<DocumentCommandProvider>(action);
        }

        protected virtual void SetPageLayout(PageLayoutSettings settings)
        {
            if (settings != null)
            {
                this.CloseActiveEditor();
                if (settings.PageDisplayMode == DevExpress.Xpf.DocumentViewer.PageDisplayMode.Columns)
                {
                    this.ColumnsCount = (settings.ColumnCount != 0) ? settings.ColumnCount : this.ColumnsCount;
                    this.PageDisplayMode = settings.PageDisplayMode;
                    this.SetZoomMode(ZoomMode.FitToWidth);
                }
                else
                {
                    double zoomFactor = base.ActualBehaviorProvider.ZoomFactor;
                    this.PageDisplayMode = settings.PageDisplayMode;
                    this.SetZoomFactor(zoomFactor);
                }
                this.ActualCommandProvider.UpdateCommands();
            }
        }

        protected virtual void SetWatermark()
        {
            if (this.Document != null)
            {
                System.Linq.Expressions.Expression[] arguments = new System.Linq.Expressions.Expression[] { System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(DocumentPreviewControl)), (MethodInfo) methodof(DocumentPreviewControl.get_Document)), (MethodInfo) methodof(DevExpress.Xpf.Printing.PreviewControl.IDocumentViewModel.get_Watermark)) };
                System.Linq.Expressions.Expression[] expressionArray2 = new System.Linq.Expressions.Expression[2];
                expressionArray2[0] = System.Linq.Expressions.Expression.Call(null, (MethodInfo) methodof(WatermarkPageHelper.CopyWatermark), arguments);
                System.Linq.Expressions.Expression[] expressionArray3 = new System.Linq.Expressions.Expression[] { System.Linq.Expressions.Expression.Subtract(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(DocumentPreviewControl)), (MethodInfo) methodof(DocumentViewerControl.get_CurrentPageNumber)), System.Linq.Expressions.Expression.Constant(1, typeof(int))) };
                System.Linq.Expressions.Expression[] expressionArray4 = new System.Linq.Expressions.Expression[] { System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Call(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(DocumentPreviewControl)), (MethodInfo) methodof(DocumentPreviewControl.get_Document)), (MethodInfo) methodof(DevExpress.Xpf.Printing.PreviewControl.IDocumentViewModel.get_Pages)), (MethodInfo) methodof(Collection<PageViewModel>.get_Item, Collection<PageViewModel>), expressionArray3), (MethodInfo) methodof(PageViewModelBase.get_Page)) };
                expressionArray2[1] = System.Linq.Expressions.Expression.Call(null, (MethodInfo) methodof(WatermarkPageHelper.CopyPage), expressionArray4);
                WatermarkEditorViewModel model = ViewModelSource.Create<WatermarkEditorViewModel>(System.Linq.Expressions.Expression.Lambda<Func<WatermarkEditorViewModel>>(System.Linq.Expressions.Expression.New((ConstructorInfo) methodof(WatermarkEditorViewModel..ctor), (IEnumerable<System.Linq.Expressions.Expression>) expressionArray2), new ParameterExpression[0]));
                this.CloseActiveEditor();
                if (this.ShowWatermarkDialog(model))
                {
                    this.Document.Do<DevExpress.Xpf.Printing.PreviewControl.IDocumentViewModel>(x => x.SetWatermark(model.Watermark));
                    this.DocumentPresenter.Update();
                }
            }
        }

        protected bool ShowExportOptionsDialog(ExportOptionsViewModelBase exportOptionsModel)
        {
            ICommand result = null;
            AssignableServiceHelper2<DocumentPreviewControl, DevExpress.Xpf.Core.DialogService>.DoServiceAction(this, this.DialogServiceTemplate.Return<DataTemplate, DevExpress.Xpf.Core.DialogService>(new Func<DataTemplate, DevExpress.Xpf.Core.DialogService>(TemplateHelper.LoadFromTemplate<DevExpress.Xpf.Core.DialogService>), new Func<DevExpress.Xpf.Core.DialogService>(this.CreateDefaultDialogService)), delegate (DevExpress.Xpf.Core.DialogService service) {
                DataTemplate template1;
                if (exportOptionsModel.SettingsType != SettingsType.Send)
                {
                    NewDocumentViewerThemeKeyExtension resourceKey = new NewDocumentViewerThemeKeyExtension();
                    resourceKey.ResourceKey = NewDocumentViewerThemeKeys.ExportOptionsDialogTemplate;
                    template1 = (DataTemplate) this.FindResource(resourceKey);
                }
                else
                {
                    NewDocumentViewerThemeKeyExtension resourceKey = new NewDocumentViewerThemeKeyExtension();
                    resourceKey.ResourceKey = NewDocumentViewerThemeKeys.SendOptionsDialogTemplate;
                    template1 = (DataTemplate) this.FindResource(resourceKey);
                }
                service.ViewTemplate = template1;
                string title = (exportOptionsModel.SettingsType == SettingsType.Send) ? PreviewLocalizer.GetString(PreviewStringId.TB_TTip_Send) : PreviewLocalizer.GetString(PreviewStringId.TB_TTip_Export);
                result = service.ShowDialog(exportOptionsModel.SubmitCommand, null, title, exportOptionsModel);
            });
            return ReferenceEquals(result, exportOptionsModel.SubmitCommand);
        }

        protected override void ShowFindText(bool? show)
        {
            if (this.UseOfficeInspiredNavigationPane && show.Value)
            {
                this.ActualNavigationPaneSettings.ForceFocusSearchBox();
            }
            else
            {
                base.ShowFindText(show);
            }
            if ((show != null) && show.Value)
            {
                this.DocumentPresenter.SelectionService.ResetSelectedBricks();
            }
        }

        private bool ShowPageSetupDialog(PageSetupViewModel model)
        {
            bool result = false;
            AssignableServiceHelper2<DocumentPreviewControl, DevExpress.Xpf.Core.DialogService>.DoServiceAction(this, this.DialogServiceTemplate.Return<DataTemplate, DevExpress.Xpf.Core.DialogService>(new Func<DataTemplate, DevExpress.Xpf.Core.DialogService>(TemplateHelper.LoadFromTemplate<DevExpress.Xpf.Core.DialogService>), new Func<DevExpress.Xpf.Core.DialogService>(this.CreateDefaultDialogService)), delegate (DevExpress.Xpf.Core.DialogService service) {
                NewDocumentViewerThemeKeyExtension resourceKey = new NewDocumentViewerThemeKeyExtension();
                resourceKey.ResourceKey = NewDocumentViewerThemeKeys.PageSetupDialogTemplate;
                service.ViewTemplate = (DataTemplate) this.FindResource(resourceKey);
                result = service.ShowDialog(MessageButton.OKCancel, PrintingLocalizer.GetString(PrintingStringId.PageSetup), model) == MessageResult.OK;
            });
            return result;
        }

        protected void ShowPrintDialog(PrintOptionsViewModel printModel)
        {
            DelegateCommand executeCommand = DelegateCommandFactory.Create(delegate {
                printModel.SavePrinterSettings();
                this.Print(printModel);
            }, () => printModel.IsValid);
            AssignableServiceHelper2<DocumentPreviewControl, DevExpress.Xpf.Core.DialogService>.DoServiceAction(this, this.DialogServiceTemplate.Return<DataTemplate, DevExpress.Xpf.Core.DialogService>(new Func<DataTemplate, DevExpress.Xpf.Core.DialogService>(TemplateHelper.LoadFromTemplate<DevExpress.Xpf.Core.DialogService>), new Func<DevExpress.Xpf.Core.DialogService>(this.CreateDefaultDialogService)), delegate (DevExpress.Xpf.Core.DialogService service) {
                NewDocumentViewerThemeKeyExtension resourceKey = new NewDocumentViewerThemeKeyExtension();
                resourceKey.ResourceKey = NewDocumentViewerThemeKeys.PrintDialogTemplate;
                service.ViewTemplate = (DataTemplate) this.FindResource(resourceKey);
                service.ShowDialog(executeCommand, null, PrintingLocalizer.GetString(PrintingStringId.Print), printModel);
            });
        }

        protected bool ShowSaveDialog(out string filePath)
        {
            string path = string.Empty;
            AssignableServiceHelper2<DevExpress.Xpf.Printing.DocumentPresenterControl, SaveFileDialogService>.DoServiceAction(this, this.SaveFileDialogTemplate.Return<DataTemplate, SaveFileDialogService>(new Func<DataTemplate, SaveFileDialogService>(TemplateHelper.LoadFromTemplate<SaveFileDialogService>), new Func<SaveFileDialogService>(this.CreateDefaultSaveFileDialogService)), delegate (SaveFileDialogService service) {
                service.InitialDirectory = this.Document.InitialDirectory;
                service.DefaultFileName = this.Document.DefaultFileName;
                ISaveFileDialogService service2 = service;
                if (service2.ShowDialog(null, null))
                {
                    path = Path.Combine(service2.File.DirectoryName, service2.File.Name);
                }
            });
            filePath = path;
            return false;
        }

        protected bool ShowScaleDialog(ScaleOptionsViewModel scaleModel)
        {
            bool result = false;
            AssignableServiceHelper2<DocumentPreviewControl, DevExpress.Xpf.Core.DialogService>.DoServiceAction(this, this.DialogServiceTemplate.Return<DataTemplate, DevExpress.Xpf.Core.DialogService>(new Func<DataTemplate, DevExpress.Xpf.Core.DialogService>(TemplateHelper.LoadFromTemplate<DevExpress.Xpf.Core.DialogService>), new Func<DevExpress.Xpf.Core.DialogService>(this.CreateDefaultDialogService)), delegate (DevExpress.Xpf.Core.DialogService service) {
                NewDocumentViewerThemeKeyExtension resourceKey = new NewDocumentViewerThemeKeyExtension();
                resourceKey.ResourceKey = NewDocumentViewerThemeKeys.ScaleDialogTemplate;
                service.ViewTemplate = (DataTemplate) this.FindResource(resourceKey);
                result = service.ShowDialog(MessageButton.OKCancel, PrintingLocalizer.GetString(PrintingStringId.Scaling), scaleModel) == MessageResult.OK;
            });
            return result;
        }

        private bool ShowWatermarkDialog(WatermarkEditorViewModel model)
        {
            bool result = false;
            AssignableServiceHelper2<DocumentPreviewControl, DevExpress.Xpf.Core.DialogService>.DoServiceAction(this, this.DialogServiceTemplate.Return<DataTemplate, DevExpress.Xpf.Core.DialogService>(new Func<DataTemplate, DevExpress.Xpf.Core.DialogService>(TemplateHelper.LoadFromTemplate<DevExpress.Xpf.Core.DialogService>), new Func<DevExpress.Xpf.Core.DialogService>(this.CreateDefaultDialogService)), delegate (DevExpress.Xpf.Core.DialogService service) {
                NewDocumentViewerThemeKeyExtension resourceKey = new NewDocumentViewerThemeKeyExtension();
                resourceKey.ResourceKey = NewDocumentViewerThemeKeys.WatermarkDialogTemplate;
                service.ViewTemplate = (DataTemplate) this.FindResource(resourceKey);
                result = service.ShowDialog(MessageButton.OKCancel, PrintingLocalizer.GetString(PrintingStringId.Watermark), model) == MessageResult.OK;
            });
            return result;
        }

        public virtual void StopBuilding()
        {
            if (this.CanStopBuilding())
            {
                this.Document.StopPageBuilding();
            }
        }

        private void Subscribe(DocumentViewModel documentViewModel)
        {
            this.Unsubscribe(documentViewModel);
            documentViewModel.Do<DocumentViewModel>(delegate (DocumentViewModel x) {
                x.Pages.CollectionChanged += new NotifyCollectionChangedEventHandler(this.OnPagesCollectionChanged);
                x.StartDocumentCreation += new EventHandler(this.OnStartDocumentCreation);
                x.DocumentCreated += new EventHandler(this.OnDocumentCreated);
                x.DocumentException += new ExceptionEventHandler(this.OnDocumentException);
                (x as ReportDocumentViewModel).Do<ReportDocumentViewModel>(delegate (ReportDocumentViewModel report) {
                    report.ReportParametersReceived += new ReportParametersReceivedEventHandler(this.OnReportParametersRecieved);
                });
                x.Subscribe();
            });
        }

        protected virtual void ToggleDocumentMap()
        {
            if (!this.UseOfficeInspiredNavigationPane)
            {
                this.PropertyProvider.ShowDocumentMap = !this.PropertyProvider.ShowDocumentMap;
            }
            else
            {
                this.ActualNavigationPaneSettings.ActiveTab = NavigationPaneTabType.DocumentMap;
                this.ShowNavigationPane = true;
            }
        }

        protected virtual void ToggleEditingFields()
        {
            ((DelegateCommand) this.ToggleEditingFieldsCommand).RaiseCanExecuteChanged();
            this.HighlightEditingFields = !this.HighlightEditingFields;
            Action<DocumentCommandProvider> action = <>c.<>9__297_0;
            if (<>c.<>9__297_0 == null)
            {
                Action<DocumentCommandProvider> local1 = <>c.<>9__297_0;
                action = <>c.<>9__297_0 = x => x.UpdateCommands();
            }
            this.ActualCommandProvider.Do<DocumentCommandProvider>(action);
        }

        protected virtual void ToggleEnableContinuousScrolling()
        {
            this.EnableContinuousScrolling = !this.EnableContinuousScrolling;
        }

        protected virtual void ToggleNavigationPane()
        {
            this.ShowNavigationPane = !this.ShowNavigationPane;
        }

        protected virtual void ToggleParametersPanel()
        {
            this.PropertyProvider.ShowParametersPanel = !this.PropertyProvider.ShowParametersPanel;
        }

        protected virtual void ToggleShowCoverPage()
        {
            this.ShowCoverPage = !this.ShowCoverPage;
        }

        protected virtual void ToggleThumbnails()
        {
            if (!this.UseOfficeInspiredNavigationPane)
            {
                this.ShowThumbnails = !this.ShowThumbnails;
            }
            else
            {
                this.ActualNavigationPaneSettings.ActiveTab = NavigationPaneTabType.Pages;
                this.ShowNavigationPane = true;
            }
        }

        private void Unsubscribe(DocumentViewModel documentViewModel)
        {
            documentViewModel.Do<DocumentViewModel>(delegate (DocumentViewModel x) {
                x.Unsubscribe();
                x.Pages.CollectionChanged -= new NotifyCollectionChangedEventHandler(this.OnPagesCollectionChanged);
                x.StartDocumentCreation -= new EventHandler(this.OnStartDocumentCreation);
                x.DocumentCreated -= new EventHandler(this.OnDocumentCreated);
                x.DocumentException -= new ExceptionEventHandler(this.OnDocumentException);
                (x as ReportDocumentViewModel).Do<ReportDocumentViewModel>(delegate (ReportDocumentViewModel report) {
                    report.ReportParametersReceived -= new ReportParametersReceivedEventHandler(this.OnReportParametersRecieved);
                });
            });
        }

        internal void UpdateSelectionState()
        {
            Func<DevExpress.Xpf.Printing.DocumentPresenterControl, bool> evaluator = <>c.<>9__368_0;
            if (<>c.<>9__368_0 == null)
            {
                Func<DevExpress.Xpf.Printing.DocumentPresenterControl, bool> local1 = <>c.<>9__368_0;
                evaluator = <>c.<>9__368_0 = x => x.SelectionService.HasSelection;
            }
            this.HasSelection = this.DocumentPresenter.Return<DevExpress.Xpf.Printing.DocumentPresenterControl, bool>(evaluator, <>c.<>9__368_1 ??= () => false);
        }

        public CursorModeType CursorMode
        {
            get => 
                (CursorModeType) base.GetValue(CursorModeProperty);
            set => 
                base.SetValue(CursorModeProperty, value);
        }

        public Color HighlightSelectionColor
        {
            get => 
                (Color) base.GetValue(HighlightSelectionColorProperty);
            set => 
                base.SetValue(HighlightSelectionColorProperty, value);
        }

        public bool HasSelection
        {
            get => 
                (bool) base.GetValue(HasSelectionProperty);
            private set => 
                base.SetValue(HasSelectionPropertyKey, value);
        }

        public DevExpress.Xpf.Printing.Parameters.Models.ParametersModel ParametersModel
        {
            get => 
                (DevExpress.Xpf.Printing.Parameters.Models.ParametersModel) base.GetValue(ParametersModelProperty);
            private set => 
                base.SetValue(ParametersModelPropertyKey, value);
        }

        public bool AutoShowParametersPanel
        {
            get => 
                (bool) base.GetValue(AutoShowParametersPanelProperty);
            set => 
                base.SetValue(AutoShowParametersPanelProperty, value);
        }

        public bool AutoShowDocumentMap
        {
            get => 
                (bool) base.GetValue(AutoShowDocumentMapProperty);
            set => 
                base.SetValue(AutoShowDocumentMapProperty, value);
        }

        public bool ShowThumbnails
        {
            get => 
                (bool) base.GetValue(ShowThumbnailsProperty);
            set => 
                base.SetValue(ShowThumbnailsProperty, value);
        }

        public bool ShowNavigationPane
        {
            get => 
                (bool) base.GetValue(ShowNavigationPaneProperty);
            set => 
                base.SetValue(ShowNavigationPaneProperty, value);
        }

        public DataTemplate DialogServiceTemplate
        {
            get => 
                (DataTemplate) base.GetValue(DialogServiceTemplateProperty);
            set => 
                base.SetValue(DialogServiceTemplateProperty, value);
        }

        public DataTemplate MessageBoxServiceTemplate
        {
            get => 
                (DataTemplate) base.GetValue(MessageBoxServiceTemplateProperty);
            set => 
                base.SetValue(MessageBoxServiceTemplateProperty, value);
        }

        public DataTemplate SaveFileDialogTemplate
        {
            get => 
                (DataTemplate) base.GetValue(SaveFileDialogTemplateProperty);
            set => 
                base.SetValue(SaveFileDialogTemplateProperty, value);
        }

        public DevExpress.Xpf.DocumentViewer.PageDisplayMode PageDisplayMode
        {
            get => 
                (DevExpress.Xpf.DocumentViewer.PageDisplayMode) base.GetValue(PageDisplayModeProperty);
            set => 
                base.SetValue(PageDisplayModeProperty, value);
        }

        public int ColumnsCount
        {
            get => 
                (int) base.GetValue(ColumnsCountProperty);
            set => 
                base.SetValue(ColumnsCountProperty, value);
        }

        public ObservableCollection<ExportFormat> HiddenExportFormats
        {
            get => 
                (ObservableCollection<ExportFormat>) base.GetValue(HiddenExportFormatsProperty);
            set => 
                base.SetValue(HiddenExportFormatsProperty, value);
        }

        public bool RequestDocumentCreation
        {
            get => 
                (bool) base.GetValue(RequestDocumentCreationProperty);
            set => 
                base.SetValue(RequestDocumentCreationProperty, value);
        }

        public ThumbnailsViewerSettings ThumbnailsSettings
        {
            get => 
                (ThumbnailsViewerSettings) base.GetValue(ThumbnailsSettingsProperty);
            private set => 
                base.SetValue(ThumbnailsSettingsPropertyKey, value);
        }

        public bool HighlightEditingFields
        {
            get => 
                (bool) base.GetValue(HighlightEditingFieldsProperty);
            set => 
                base.SetValue(HighlightEditingFieldsProperty, value);
        }

        public DevExpress.Xpf.Printing.EditingFieldTemplateSelector EditingFieldTemplateSelector
        {
            get => 
                (DevExpress.Xpf.Printing.EditingFieldTemplateSelector) base.GetValue(EditingFieldTemplateSelectorProperty);
            set => 
                base.SetValue(EditingFieldTemplateSelectorProperty, value);
        }

        public bool ShowCoverPage
        {
            get => 
                (bool) base.GetValue(ShowCoverPageProperty);
            set => 
                base.SetValue(ShowCoverPageProperty, value);
        }

        public bool EnableContinuousScrolling
        {
            get => 
                (bool) base.GetValue(EnableContinuousScrollingProperty);
            set => 
                base.SetValue(EnableContinuousScrollingProperty, value);
        }

        public bool ShowPageMargins
        {
            get => 
                (bool) base.GetValue(ShowPageMarginsProperty);
            set => 
                base.SetValue(ShowPageMarginsProperty, value);
        }

        public bool HighlightCurrentPage
        {
            get => 
                (bool) base.GetValue(HighlightCurrentPageProperty);
            set => 
                base.SetValue(HighlightCurrentPageProperty, value);
        }

        public bool AllowCachePages
        {
            get => 
                (bool) base.GetValue(AllowCachePagesProperty);
            set => 
                base.SetValue(AllowCachePagesProperty, value);
        }

        public bool AllowDocumentEditing
        {
            get => 
                (bool) base.GetValue(AllowDocumentEditingProperty);
            set => 
                base.SetValue(AllowDocumentEditingProperty, value);
        }

        public DevExpress.Xpf.Printing.PreviewControl.Native.DocumentMapSettings DocumentMapSettings
        {
            get => 
                (DevExpress.Xpf.Printing.PreviewControl.Native.DocumentMapSettings) base.GetValue(DocumentMapSettingsProperty);
            set => 
                base.SetValue(DocumentMapSettingsProperty, value);
        }

        public DevExpress.Xpf.Printing.NavigationPaneSettings NavigationPaneSettings
        {
            get => 
                (DevExpress.Xpf.Printing.NavigationPaneSettings) base.GetValue(NavigationPaneSettingsProperty);
            set => 
                base.SetValue(NavigationPaneSettingsProperty, value);
        }

        public DevExpress.Xpf.Printing.NavigationPaneSettings ActualNavigationPaneSettings
        {
            get => 
                (DevExpress.Xpf.Printing.NavigationPaneSettings) base.GetValue(ActualNavigationPaneSettingsProperty);
            private set => 
                base.SetValue(ActualNavigationPaneSettingsPropertyKey, value);
        }

        public bool UseOfficeInspiredNavigationPane
        {
            get => 
                (bool) base.GetValue(UseOfficeInspiredNavigationPaneProperty);
            set => 
                base.SetValue(UseOfficeInspiredNavigationPaneProperty, value);
        }

        protected internal DevExpress.Xpf.Printing.DocumentPresenterControl DocumentPresenter =>
            base.DocumentPresenter as DevExpress.Xpf.Printing.DocumentPresenterControl;

        public DevExpress.Xpf.Printing.PreviewControl.IDocumentViewModel Document =>
            (DevExpress.Xpf.Printing.PreviewControl.IDocumentViewModel) base.Document;

        public DocumentCommandProvider ActualCommandProvider =>
            base.ActualCommandProvider as DocumentCommandProvider;

        public DocumentPreviewPropertyProvider PropertyProvider =>
            base.PropertyProvider as DocumentPreviewPropertyProvider;

        protected internal TextureCache Cache { get; private set; }

        public ICommand SetCursorModeCommand { get; private set; }

        public ICommand CopyCommand { get; private set; }

        public ICommand ToggleParametersPanelCommand { get; private set; }

        public ICommand ToggleDocumentMapCommand { get; private set; }

        public ICommand ToggleThumbnailsCommand { get; private set; }

        public ICommand ToggleEditingFieldsCommand { get; private set; }

        public ICommand SaveCommand { get; private set; }

        public ICommand PrintCommand { get; private set; }

        public ICommand PrintDirectCommand { get; private set; }

        public ICommand PageSetupCommand { get; private set; }

        public ICommand ScaleCommand { get; private set; }

        public ICommand FirstPageCommand { get; private set; }

        public ICommand LastPageCommand { get; private set; }

        public ICommand ExportCommand { get; private set; }

        public ICommand SendCommand { get; private set; }

        public ICommand SetWatermarkCommand { get; private set; }

        public ICommand StopPageBuildingCommand { get; private set; }

        public ICommand SetPageLayoutCommand { get; private set; }

        public ICommand ToggleShowCoverPageCommand { get; private set; }

        public ICommand ToggleEnableContinuousScrollingCommand { get; private set; }

        public ICommand ToggleNavigationPaneCommand { get; private set; }

        private bool IsInProgress
        {
            get
            {
                Func<DocumentViewModel, bool> evaluator = <>c.<>9__257_0;
                if (<>c.<>9__257_0 == null)
                {
                    Func<DocumentViewModel, bool> local1 = <>c.<>9__257_0;
                    evaluator = <>c.<>9__257_0 = x => x.InProgress;
                }
                return (this.Document as DocumentViewModel).Return<DocumentViewModel, bool>(evaluator, (<>c.<>9__257_1 ??= () => false));
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DocumentPreviewControl.<>c <>9 = new DocumentPreviewControl.<>c();
            public static Action<DocumentCommandProvider> <>9__161_3;
            public static Action<DevExpress.Xpf.Printing.DocumentPresenterControl> <>9__161_11;
            public static Action<DocumentCommandProvider> <>9__161_20;
            public static Action<ThumbnailsDocumentViewModel> <>9__168_0;
            public static Action<ThumbnailsDocumentViewModel> <>9__169_0;
            public static Action<DevExpress.Xpf.Printing.DocumentPresenterControl> <>9__170_0;
            public static Func<DocumentViewModel, bool> <>9__257_0;
            public static Func<bool> <>9__257_1;
            public static Func<bool> <>9__262_1;
            public static Func<DevExpress.Xpf.Printing.PreviewControl.IDocumentViewModel, bool> <>9__266_0;
            public static Func<bool> <>9__266_1;
            public static Func<bool> <>9__268_1;
            public static Func<PageViewModel, bool> <>9__269_0;
            public static Func<bool> <>9__271_1;
            public static Func<bool> <>9__274_1;
            public static Func<bool> <>9__277_1;
            public static Func<bool> <>9__279_1;
            public static Func<bool> <>9__280_1;
            public static Func<DevExpress.Xpf.Printing.PreviewControl.IDocumentViewModel, bool> <>9__292_0;
            public static Func<bool> <>9__292_1;
            public static Func<DevExpress.Xpf.Printing.PreviewControl.IDocumentViewModel, bool> <>9__296_0;
            public static Func<bool> <>9__296_1;
            public static Action<DocumentCommandProvider> <>9__297_0;
            public static Func<DevExpress.Xpf.Printing.PreviewControl.IDocumentViewModel, bool> <>9__298_0;
            public static Func<bool> <>9__298_1;
            public static Func<DevExpress.Xpf.Printing.PreviewControl.IDocumentViewModel, bool> <>9__299_0;
            public static Func<bool> <>9__299_1;
            public static Action<DocumentCommandProvider> <>9__300_0;
            public static Action<DevExpress.Xpf.Printing.DocumentPresenterControl> <>9__302_0;
            public static Action<DXMessageBoxService> <>9__307_0;
            public static Func<bool> <>9__309_1;
            public static Func<bool> <>9__310_1;
            public static Func<bool> <>9__311_1;
            public static Func<bool> <>9__312_1;
            public static Func<DevExpress.Xpf.Printing.PreviewControl.IDocumentViewModel, bool> <>9__316_0;
            public static Func<bool> <>9__316_1;
            public static Action<DocumentCommandProvider> <>9__325_1;
            public static Action<DocumentCommandProvider> <>9__327_0;
            public static Func<DevExpress.Xpf.Printing.PreviewControl.IDocumentViewModel, bool> <>9__327_1;
            public static Func<bool> <>9__327_2;
            public static Action<ThumbnailsDocumentViewModel> <>9__330_1;
            public static Action<DocumentCommandProvider> <>9__331_0;
            public static Action<DocumentViewModel> <>9__331_1;
            public static Action<DocumentViewModel> <>9__332_0;
            public static Action<DevExpress.Xpf.Printing.DocumentPresenterControl> <>9__332_1;
            public static Action<DocumentViewModel> <>9__333_0;
            public static Action<DocumentCommandProvider> <>9__340_0;
            public static Action<DevExpress.Xpf.Printing.DocumentPresenterControl> <>9__341_0;
            public static Action<DocumentCommandProvider> <>9__342_0;
            public static Action<DocumentViewerPanel> <>9__342_2;
            public static Action<DevExpress.Xpf.Printing.DocumentPresenterControl> <>9__342_1;
            public static Action<DocumentCommandProvider> <>9__345_0;
            public static Action<DevExpress.Xpf.Printing.DocumentPresenterControl> <>9__345_1;
            public static Action<DocumentCommandProvider> <>9__348_2;
            public static Action<DocumentCommandProvider> <>9__349_0;
            public static Action<DocumentCommandProvider> <>9__353_0;
            public static Action<DevExpress.Xpf.Printing.DocumentPresenterControl> <>9__354_0;
            public static Action<DevExpress.Xpf.Printing.DocumentPresenterControl> <>9__355_0;
            public static Func<ParameterModel, Parameter> <>9__357_1;
            public static Func<DevExpress.Xpf.Printing.DocumentPresenterControl, bool> <>9__368_0;
            public static Func<bool> <>9__368_1;

            internal void <.cctor>b__161_0(DocumentPreviewControl owner, CursorModeType oldValue, CursorModeType newValue)
            {
                owner.OnCursorModeChanged(newValue);
            }

            internal void <.cctor>b__161_1(DocumentPreviewControl owner, Color oldValue, Color newValue)
            {
                owner.OnSelectionColorChanged(newValue);
            }

            internal void <.cctor>b__161_10(DocumentPreviewControl d)
            {
                Action<DevExpress.Xpf.Printing.DocumentPresenterControl> action = <>9__161_11;
                if (<>9__161_11 == null)
                {
                    Action<DevExpress.Xpf.Printing.DocumentPresenterControl> local1 = <>9__161_11;
                    action = <>9__161_11 = x => x.Update();
                }
                d.DocumentPresenter.Do<DevExpress.Xpf.Printing.DocumentPresenterControl>(action);
            }

            internal void <.cctor>b__161_11(DevExpress.Xpf.Printing.DocumentPresenterControl x)
            {
                x.Update();
            }

            internal void <.cctor>b__161_12(DocumentPreviewControl d)
            {
                d.AssignDocumentPresenterProperties();
            }

            internal void <.cctor>b__161_13(DocumentPreviewControl d)
            {
                d.OnPageLayoutChanged();
            }

            internal void <.cctor>b__161_14(DocumentPreviewControl d)
            {
                d.OnHighlightCurrentPageChanged();
            }

            internal void <.cctor>b__161_15(DocumentPreviewControl d)
            {
                d.OnAllowCachePagesChanges();
            }

            internal void <.cctor>b__161_16(DocumentPreviewControl d)
            {
                d.OnAllowDocumentEditingChanged();
            }

            internal void <.cctor>b__161_17(DocumentPreviewControl d)
            {
                d.OnDocumentMapSettingsChanged();
            }

            internal void <.cctor>b__161_18(DocumentPreviewControl d)
            {
                d.OnNavigationPaneSettingsChanged();
            }

            internal void <.cctor>b__161_19(DocumentPreviewControl d)
            {
                Action<DocumentCommandProvider> action = <>9__161_20;
                if (<>9__161_20 == null)
                {
                    Action<DocumentCommandProvider> local1 = <>9__161_20;
                    action = <>9__161_20 = x => x.UpdateCommands();
                }
                d.ActualCommandProvider.Do<DocumentCommandProvider>(action);
            }

            internal void <.cctor>b__161_2(DocumentPreviewControl d)
            {
                Action<DocumentCommandProvider> action = <>9__161_3;
                if (<>9__161_3 == null)
                {
                    Action<DocumentCommandProvider> local1 = <>9__161_3;
                    action = <>9__161_3 = x => x.UpdateCommands();
                }
                d.ActualCommandProvider.Do<DocumentCommandProvider>(action);
            }

            internal void <.cctor>b__161_20(DocumentCommandProvider x)
            {
                x.UpdateCommands();
            }

            internal void <.cctor>b__161_3(DocumentCommandProvider x)
            {
                x.UpdateCommands();
            }

            internal bool <.cctor>b__161_4(DocumentPreviewControl d, bool newValue) => 
                !d.Document.IsRemoteReportDocumentSource() && newValue;

            internal void <.cctor>b__161_5(DocumentPreviewControl owner)
            {
                owner.OnPageLayoutChanged();
            }

            internal void <.cctor>b__161_6(DocumentPreviewControl x)
            {
                x.OnPageLayoutChanged();
            }

            internal int <.cctor>b__161_7(DocumentPreviewControl owner, int newValue) => 
                (newValue > 1) ? newValue : 1;

            internal void <.cctor>b__161_8(DocumentPreviewControl owner, ObservableCollection<ExportFormat> oldValue, ObservableCollection<ExportFormat> newValue)
            {
                owner.OnHiddenFormatsChanged(oldValue, newValue);
            }

            internal ObservableCollection<ExportFormat> <.cctor>b__161_9(DocumentPreviewControl owner, ObservableCollection<ExportFormat> newValue) => 
                owner.CoerceHiddenFormats(newValue);

            internal void <AssignPageLayoutSettings>b__353_0(DocumentCommandProvider x)
            {
                x.UpdatePageLayoutCommands();
            }

            internal bool <CanGoToFirstPage>b__279_1() => 
                false;

            internal bool <CanGoToLastPage>b__280_1() => 
                false;

            internal bool <CanPageSetup>b__271_1() => 
                false;

            internal bool <CanPrint>b__266_0(DevExpress.Xpf.Printing.PreviewControl.IDocumentViewModel x) => 
                x.IsCreated && !((IProgressSettings) x).InProgress;

            internal bool <CanPrint>b__266_1() => 
                false;

            internal bool <CanPrintDirect>b__268_1() => 
                false;

            internal bool <CanSave>b__262_1() => 
                false;

            internal bool <CanScale>b__277_1() => 
                false;

            internal bool <CanSetCursorMode>b__299_0(DevExpress.Xpf.Printing.PreviewControl.IDocumentViewModel x) => 
                x.IsCreated;

            internal bool <CanSetCursorMode>b__299_1() => 
                false;

            internal bool <CanSetWatermark>b__274_1() => 
                false;

            internal bool <CanSetZoomFactor>b__312_1() => 
                false;

            internal bool <CanShowFindText>b__309_1() => 
                false;

            internal bool <CanStopBuilding>b__316_0(DevExpress.Xpf.Printing.PreviewControl.IDocumentViewModel x) => 
                x.CanStopPageBuilding;

            internal bool <CanStopBuilding>b__316_1() => 
                false;

            internal bool <CanToggleDocumentMap>b__292_0(DevExpress.Xpf.Printing.PreviewControl.IDocumentViewModel x) => 
                x.HasBookmarks;

            internal bool <CanToggleDocumentMap>b__292_1() => 
                false;

            internal bool <CanToggleEditingFields>b__298_0(DevExpress.Xpf.Printing.PreviewControl.IDocumentViewModel x) => 
                x.EditingFields.Any<EditingField>();

            internal bool <CanToggleEditingFields>b__298_1() => 
                false;

            internal bool <CanToggleThumbnails>b__296_0(DevExpress.Xpf.Printing.PreviewControl.IDocumentViewModel x) => 
                !x.IsRemoteReportDocumentSource() && x.Pages.Any<PageViewModel>();

            internal bool <CanToggleThumbnails>b__296_1() => 
                false;

            internal bool <CanZoomIn>b__310_1() => 
                false;

            internal bool <CanZoomOut>b__311_1() => 
                false;

            internal void <Copy>b__302_0(DevExpress.Xpf.Printing.DocumentPresenterControl x)
            {
                x.SelectionService.CopyToClipboard();
            }

            internal void <CreatePropertyProvider>b__325_1(DocumentCommandProvider x)
            {
                x.UpdateCommands();
            }

            internal void <FindNextText>b__307_0(DXMessageBoxService service)
            {
                service.ShowMessage(PrintingLocalizer.GetString(PrintingStringId.SearchFinished_NoMatchesFound), PreviewStringId.Msg_Search_Caption.GetString(), MessageButton.OK, MessageIcon.Information);
            }

            internal bool <get_IsInProgress>b__257_0(DocumentViewModel x) => 
                x.InProgress;

            internal bool <get_IsInProgress>b__257_1() => 
                false;

            internal void <LoadDocument>b__327_0(DocumentCommandProvider x)
            {
                x.UpdateCommands();
            }

            internal bool <LoadDocument>b__327_1(DevExpress.Xpf.Printing.PreviewControl.IDocumentViewModel x) => 
                x.HasBookmarks;

            internal bool <LoadDocument>b__327_2() => 
                false;

            internal void <OnAllowCachePagesChanges>b__354_0(DevExpress.Xpf.Printing.DocumentPresenterControl x)
            {
                x.Renderer.UpdateInnerRenderer();
            }

            internal void <OnAllowDocumentEditingChanged>b__355_0(DevExpress.Xpf.Printing.DocumentPresenterControl x)
            {
                x.EditingStrategy.EndEditing();
            }

            internal void <OnCursorModeChanged>b__345_0(DocumentCommandProvider x)
            {
                x.UpdateCommands();
            }

            internal void <OnCursorModeChanged>b__345_1(DevExpress.Xpf.Printing.DocumentPresenterControl x)
            {
                x.SelectionService.ResetSelectedBricks();
            }

            internal void <OnDocumentChanged>b__331_0(DocumentCommandProvider x)
            {
                x.UpdateCommands();
            }

            internal void <OnDocumentChanged>b__331_1(DocumentViewModel document)
            {
                document.NavigationState = null;
            }

            internal void <OnDocumentCreated>b__342_0(DocumentCommandProvider x)
            {
                x.UpdateCommands();
            }

            internal void <OnDocumentCreated>b__342_1(DevExpress.Xpf.Printing.DocumentPresenterControl x)
            {
                Action<DocumentViewerPanel> action = <>9__342_2;
                if (<>9__342_2 == null)
                {
                    Action<DocumentViewerPanel> local1 = <>9__342_2;
                    action = <>9__342_2 = panel => panel.UpdateLayout();
                }
                x.ItemsPanel.Do<DocumentViewerPanel>(action);
                x.Update();
            }

            internal void <OnDocumentCreated>b__342_2(DocumentViewerPanel panel)
            {
                panel.UpdateLayout();
            }

            internal void <OnGotFocus>b__170_0(DevExpress.Xpf.Printing.DocumentPresenterControl x)
            {
                x.Focus();
            }

            internal void <OnHiddenFormatsChanged>b__348_2(DocumentCommandProvider x)
            {
                x.UpdateExportCommands();
            }

            internal void <OnHiddenFormatsCollectionChanged>b__349_0(DocumentCommandProvider x)
            {
                x.UpdateExportCommands();
            }

            internal void <OnLoaded>b__169_0(ThumbnailsDocumentViewModel x)
            {
                x.AllowSynchronization(true);
            }

            internal void <OnReportParametersRecieved>b__340_0(DocumentCommandProvider x)
            {
                x.UpdateCommands();
            }

            internal void <OnStartDocumentCreation>b__341_0(DevExpress.Xpf.Printing.DocumentPresenterControl x)
            {
                x.NavigationStrategy.ScrollToStartUp();
                x.EditingStrategy.EndEditing();
            }

            internal Parameter <OnSubmitParameters>b__357_1(ParameterModel p) => 
                p.Parameter;

            internal void <OnUnloaded>b__168_0(ThumbnailsDocumentViewModel x)
            {
                x.AllowSynchronization(false);
            }

            internal void <OnZoomFactorChanged>b__332_0(DocumentViewModel document)
            {
                document.NavigationState = null;
            }

            internal void <OnZoomFactorChanged>b__332_1(DevExpress.Xpf.Printing.DocumentPresenterControl x)
            {
                x.Update();
            }

            internal void <OnZoomModeChanged>b__333_0(DocumentViewModel document)
            {
                document.NavigationState = null;
            }

            internal bool <PageSetup>b__269_0(PageViewModel page) => 
                page.IsSelected;

            internal void <ReleaseDocument>b__330_1(ThumbnailsDocumentViewModel d)
            {
                d.AllowSynchronization(false);
            }

            internal void <SetCursorMode>b__300_0(DocumentCommandProvider x)
            {
                x.UpdateCommands();
            }

            internal void <ToggleEditingFields>b__297_0(DocumentCommandProvider x)
            {
                x.UpdateCommands();
            }

            internal bool <UpdateSelectionState>b__368_0(DevExpress.Xpf.Printing.DocumentPresenterControl x) => 
                x.SelectionService.HasSelection;

            internal bool <UpdateSelectionState>b__368_1() => 
                false;
        }
    }
}

