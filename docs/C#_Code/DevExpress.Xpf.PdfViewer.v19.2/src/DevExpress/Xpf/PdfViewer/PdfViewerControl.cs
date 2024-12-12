namespace DevExpress.Xpf.PdfViewer
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI;
    using DevExpress.Mvvm.UI.Native;
    using DevExpress.Pdf;
    using DevExpress.Pdf.Drawing;
    using DevExpress.Pdf.Localization;
    using DevExpress.Pdf.Native;
    using DevExpress.Printing;
    using DevExpress.Utils;
    using DevExpress.Xpf;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.DocumentViewer;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.PdfViewer.Extensions;
    using DevExpress.Xpf.PdfViewer.Helpers;
    using DevExpress.Xpf.PdfViewer.Internal;
    using DevExpress.Xpf.PdfViewer.Themes;
    using DevExpress.Xpf.PdfViewer.UI;
    using DevExpress.Xpf.Utils;
    using DevExpress.Xpf.Utils.About;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Printing;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Security;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    [LicenseProvider(typeof(DX_WPF_LicenseProvider)), DXToolboxBrowsable(true), ToolboxTabName("DX.19.2: Data & Analytics")]
    public class PdfViewerControl : DocumentViewerControl, IPdfViewer
    {
        public static readonly DependencyProperty AcceptsTabProperty;
        public static readonly DependencyProperty AsyncDocumentLoadProperty;
        public static readonly DependencyProperty AllowCachePagesProperty;
        public static readonly DependencyProperty CacheSizeProperty;
        public static readonly DependencyProperty HighlightSelectionColorProperty;
        public static readonly DependencyProperty CaretColorProperty;
        public static readonly DependencyProperty PagePaddingProperty;
        public static readonly DependencyProperty AllowCurrentPageHighlightingProperty;
        public static readonly DependencyProperty CursorModeProperty;
        public static readonly DependencyProperty ShowStartScreenProperty;
        public static readonly DependencyProperty RecentFilesProperty;
        public static readonly DependencyProperty NumberOfRecentFilesProperty;
        public static readonly DependencyProperty ShowOpenFileOnStartScreenProperty;
        internal static readonly DependencyPropertyKey HasSelectionPropertyKey;
        public static readonly DependencyProperty HasSelectionProperty;
        public static readonly DependencyProperty MaxPrintingDpiProperty;
        public static readonly DependencyProperty DocumentCreatorProperty;
        public static readonly DependencyProperty DocumentProducerProperty;
        public static readonly DependencyProperty DetachStreamOnLoadCompleteProperty;
        public static readonly DependencyProperty IsReadOnlyProperty;
        public static readonly DependencyProperty PrintPreviewDialogTemplateProperty;
        public static readonly DependencyProperty OutlinesViewerSettingsProperty;
        public static readonly DependencyProperty SaveFileDialogTemplateProperty;
        public static readonly DependencyProperty PageLayoutProperty;
        public static readonly DependencyProperty NavigationPanelsLayoutProperty;
        private static readonly DependencyPropertyKey HasOutlinesPropertyKey;
        public static readonly DependencyProperty HasOutlinesProperty;
        private static readonly DependencyPropertyKey ActualAttachmentsViewerSettingsPropertyKey;
        public static readonly DependencyProperty ActualAttachmentsViewerSettingsProperty;
        public static readonly DependencyProperty AttachmentsViewerSettingsProperty;
        private static readonly DependencyPropertyKey HasAttachmentsPropertyKey;
        public static readonly DependencyProperty HasAttachmentsProperty;
        private static readonly DependencyPropertyKey ActualThumbnailsViewerSettingsPropertyKey;
        public static readonly DependencyProperty ActualThumbnailsViewerSettingsProperty;
        public static readonly DependencyProperty ThumbnailsViewerSettingsProperty;
        private static readonly DependencyPropertyKey HasThumbnailsPropertyKey;
        public static readonly DependencyProperty HasThumbnailsProperty;
        public static readonly DependencyProperty HighlightFormFieldsProperty;
        public static readonly DependencyProperty MarkupToolsSettingsProperty;
        public static readonly DependencyProperty ShowPrintStatusDialogProperty;
        public static readonly DependencyProperty HighlightedFormFieldColorProperty;
        public static readonly DependencyProperty ContinueSearchFromProperty;
        public static RoutedEvent CursorModeChangedEvent;
        public static RoutedEvent UriOpeningEvent;
        public static RoutedEvent GetDocumentPasswordEvent;
        public static RoutedEvent SelectionStartedEvent;
        public static RoutedEvent SelectionEndedEvent;
        public static RoutedEvent SelectionContinuedEvent;
        public static RoutedEvent DocumentLoadedEvent;
        public static RoutedEvent ReferencedDocumentOpeningEvent;
        public static RoutedEvent DocumentClosingEvent;
        public static RoutedEvent PageLayoutChangedEvent;
        public static RoutedEvent AttachmentOpeningEvent;
        public static RoutedEvent PrintPageEvent;
        public static RoutedEvent QueryPageSettingsEvent;
        public static RoutedEvent FormFieldValueChangingEvent;
        public static RoutedEvent FormFieldValueChangedEvent;
        public static RoutedEvent PageSetupDialogShowingEvent;
        public static RoutedEvent TextMarkupAnnotationCreatingEvent;
        public static RoutedEvent TextMarkupAnnotationCreatedEvent;
        public static RoutedEvent ShownEditorEvent;
        public static RoutedEvent ShowingEditorEvent;
        public static RoutedEvent HiddenEditorEvent;
        public static RoutedEvent AnnotationDeletingEvent;
        public static RoutedEvent PopupMenuShowingEvent;
        public static RoutedEvent TextMarkupAnnotationChangedEvent;
        public static RoutedEvent MarkupAnnotationGotFocusEvent;
        public static RoutedEvent MarkupAnnotationLostFocusEvent;
        public static RoutedEvent ExceptionMessageEvent;
        private readonly Locker documentClosingLocker = new Locker();
        private readonly Locker updateDocumentSourceLocker = new Locker();
        private readonly Locker ignoreSaveChangesLocker = new Locker();
        private readonly PdfViewerBackend viewerBackend;
        private CursorModeType lastCommonCursorMode = CursorModeType.SelectTool;

        public event AnnotationDeletingEventHandler AnnotationDeleting
        {
            add
            {
                base.AddHandler(AnnotationDeletingEvent, value);
            }
            remove
            {
                base.RemoveHandler(AnnotationDeletingEvent, value);
            }
        }

        public event AttachmentOpeningEventHandler AttachmentOpening
        {
            add
            {
                base.AddHandler(AttachmentOpeningEvent, value);
            }
            remove
            {
                base.RemoveHandler(AttachmentOpeningEvent, value);
            }
        }

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

        public event DocumentClosingEventHandler DocumentClosing
        {
            add
            {
                base.AddHandler(DocumentClosingEvent, value);
            }
            remove
            {
                base.RemoveHandler(DocumentClosingEvent, value);
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

        public event ExceptionMessageEventHandler ExceptionMessage
        {
            add
            {
                base.AddHandler(ExceptionMessageEvent, value);
            }
            remove
            {
                base.RemoveHandler(ExceptionMessageEvent, value);
            }
        }

        public event FormFieldValueChangedEventHandler FormFieldValueChanged
        {
            add
            {
                base.AddHandler(FormFieldValueChangedEvent, value);
            }
            remove
            {
                base.RemoveHandler(FormFieldValueChangedEvent, value);
            }
        }

        public event FormFieldValueChangingEventHandler FormFieldValueChanging
        {
            add
            {
                base.AddHandler(FormFieldValueChangingEvent, value);
            }
            remove
            {
                base.RemoveHandler(FormFieldValueChangingEvent, value);
            }
        }

        public event GetDocumentPasswordEventHandler GetDocumentPassword
        {
            add
            {
                base.AddHandler(GetDocumentPasswordEvent, value);
            }
            remove
            {
                base.RemoveHandler(GetDocumentPasswordEvent, value);
            }
        }

        public event HiddenEditorEventHandler HiddenEditor
        {
            add
            {
                base.AddHandler(HiddenEditorEvent, value);
            }
            remove
            {
                base.RemoveHandler(HiddenEditorEvent, value);
            }
        }

        public event MarkupAnnotationGotFocusEventHandler MarkupAnnotationGotFocus
        {
            add
            {
                base.AddHandler(MarkupAnnotationGotFocusEvent, value);
            }
            remove
            {
                base.RemoveHandler(MarkupAnnotationGotFocusEvent, value);
            }
        }

        public event MarkupAnnotationLostFocusEventHandler MarkupAnnotationLostFocus
        {
            add
            {
                base.AddHandler(MarkupAnnotationLostFocusEvent, value);
            }
            remove
            {
                base.RemoveHandler(MarkupAnnotationLostFocusEvent, value);
            }
        }

        public event RoutedEventHandler PageLayoutChanged
        {
            add
            {
                base.AddHandler(PageLayoutChangedEvent, value);
            }
            remove
            {
                base.RemoveHandler(PageLayoutChangedEvent, value);
            }
        }

        public event PageSetupDialogShowingEventHandler PageSetupDialogShowing
        {
            add
            {
                base.AddHandler(PageSetupDialogShowingEvent, value);
            }
            remove
            {
                base.RemoveHandler(PageSetupDialogShowingEvent, value);
            }
        }

        public event PopupMenuShowingEventHandler PopupMenuShowing
        {
            add
            {
                base.AddHandler(PopupMenuShowingEvent, value);
            }
            remove
            {
                base.RemoveHandler(PopupMenuShowingEvent, value);
            }
        }

        public event DevExpress.Xpf.PdfViewer.PdfPrintPageEventHandler PrintPage
        {
            add
            {
                base.AddHandler(PrintPageEvent, value);
            }
            remove
            {
                base.RemoveHandler(PrintPageEvent, value);
            }
        }

        public event DevExpress.Xpf.PdfViewer.PdfQueryPageSettingsEventHandler QueryPageSettings
        {
            add
            {
                base.AddHandler(QueryPageSettingsEvent, value);
            }
            remove
            {
                base.RemoveHandler(QueryPageSettingsEvent, value);
            }
        }

        public event ReferencedDocumentOpeningEventHandler ReferencedDocumentOpening
        {
            add
            {
                base.AddHandler(ReferencedDocumentOpeningEvent, value);
            }
            remove
            {
                base.RemoveHandler(ReferencedDocumentOpeningEvent, value);
            }
        }

        public event SelectionEventHandler SelectionContinued
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

        public event SelectionEventHandler SelectionEnded
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

        public event SelectionEventHandler SelectionStarted
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

        public event ShowingEditorEventHandler ShowingEditor
        {
            add
            {
                base.AddHandler(ShowingEditorEvent, value);
            }
            remove
            {
                base.RemoveHandler(ShowingEditorEvent, value);
            }
        }

        public event ShownEditorEventHandler ShownEditor
        {
            add
            {
                base.AddHandler(ShownEditorEvent, value);
            }
            remove
            {
                base.RemoveHandler(ShownEditorEvent, value);
            }
        }

        public event PdfTextMarkupAnnotationChangedEventHandler TextMarkupAnnotationChanged
        {
            add
            {
                base.AddHandler(TextMarkupAnnotationChangedEvent, value);
            }
            remove
            {
                base.RemoveHandler(TextMarkupAnnotationChangedEvent, value);
            }
        }

        public event PdfTextMarkupAnnotationCreatedEventHandler TextMarkupAnnotationCreated
        {
            add
            {
                base.AddHandler(TextMarkupAnnotationCreatedEvent, value);
            }
            remove
            {
                base.RemoveHandler(TextMarkupAnnotationCreatedEvent, value);
            }
        }

        public event PdfTextMarkupAnnotationCreatingEventHandler TextMarkupAnnotationCreating
        {
            add
            {
                base.AddHandler(TextMarkupAnnotationCreatingEvent, value);
            }
            remove
            {
                base.RemoveHandler(TextMarkupAnnotationCreatingEvent, value);
            }
        }

        public event UriOpeningEventHandler UriOpening
        {
            add
            {
                base.AddHandler(UriOpeningEvent, value);
            }
            remove
            {
                base.RemoveHandler(UriOpeningEvent, value);
            }
        }

        static PdfViewerControl()
        {
            Type forType = typeof(PdfViewerControl);
            Control.HorizontalContentAlignmentProperty.OverrideMetadata(forType, new FrameworkPropertyMetadata(HorizontalAlignment.Center));
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(PdfViewerControl), "owner");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            DetachStreamOnLoadCompleteProperty = DependencyPropertyRegistrator.Register<PdfViewerControl, bool>(System.Linq.Expressions.Expression.Lambda<Func<PdfViewerControl, bool>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(PdfViewerControl.get_DetachStreamOnLoadComplete)), parameters), false);
            AcceptsTabProperty = DependencyProperty.Register("AcceptsTab", typeof(bool), forType, new FrameworkPropertyMetadata(true));
            AsyncDocumentLoadProperty = DependencyProperty.Register("AsyncDocumentLoad", typeof(bool), forType, new FrameworkPropertyMetadata(true));
            HighlightSelectionColorProperty = DependencyPropertyManager.Register("HighlightSelectionColor", typeof(System.Windows.Media.Color), forType, new FrameworkPropertyMetadata(System.Windows.Media.Color.FromArgb(0x59, 0x60, 0x98, 0xc0)));
            CaretColorProperty = DependencyPropertyManager.Register("CaretColor", typeof(System.Windows.Media.Color), forType, new FrameworkPropertyMetadata(Colors.Black));
            PagePaddingProperty = DependencyPropertyManager.Register("PagePadding", typeof(Thickness), forType, new FrameworkPropertyMetadata(new Thickness(5.0)));
            AllowCurrentPageHighlightingProperty = DependencyPropertyManager.Register("AllowCurrentPageHighlighting", typeof(bool), forType, new FrameworkPropertyMetadata(false));
            CursorModeProperty = DependencyPropertyManager.Register("CursorMode", typeof(CursorModeType), forType, new FrameworkPropertyMetadata(CursorModeType.SelectTool, FrameworkPropertyMetadataOptions.None, (d, args) => ((PdfViewerControl) d).OnCursorModeChanged((CursorModeType) args.NewValue)));
            RecentFilesProperty = DependencyPropertyManager.Register("RecentFiles", typeof(ObservableCollection<RecentFileViewModel>), forType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None));
            NumberOfRecentFilesProperty = DependencyPropertyManager.Register("NumberOfRecentFiles", typeof(int), forType, new FrameworkPropertyMetadata(5));
            ShowOpenFileOnStartScreenProperty = DependencyPropertyManager.Register("ShowOpenFileOnStartScreen", typeof(bool), forType, new FrameworkPropertyMetadata(true));
            HasSelectionPropertyKey = DependencyPropertyManager.RegisterReadOnly("HasSelection", typeof(bool), forType, new FrameworkPropertyMetadata(false));
            HasSelectionProperty = HasSelectionPropertyKey.DependencyProperty;
            ShowStartScreenProperty = DependencyPropertyManager.Register("ShowStartScreen", typeof(bool?), forType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None, (obj, args) => ((PdfViewerControl) obj).OnShowStartScreenChanged((bool?) args.NewValue)));
            MaxPrintingDpiProperty = DependencyPropertyManager.Register("MaxPrintingDpi", typeof(int), forType, new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.None, null, (o, value) => (((int) value) < 0) ? 0 : value));
            DocumentCreatorProperty = DependencyPropertyManager.Register("DocumentCreator", typeof(string), forType);
            DocumentProducerProperty = DependencyPropertyManager.Register("DocumentProducer", typeof(string), forType);
            AllowCachePagesProperty = DependencyPropertyManager.Register("AllowCachePages", typeof(bool), forType, new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.None, (d, e) => ((PdfViewerControl) d).AllowCachePagesChanged((bool) e.NewValue)));
            CacheSizeProperty = DependencyPropertyManager.Register("CacheSize", typeof(int), forType, new FrameworkPropertyMetadata(0x11e1a300, FrameworkPropertyMetadataOptions.None, (obj, args) => ((PdfViewerControl) obj).CacheSizeChanged((int) args.NewValue)));
            expression = System.Linq.Expressions.Expression.Parameter(typeof(PdfViewerControl), "owner");
            ParameterExpression[] expressionArray2 = new ParameterExpression[] { expression };
            IsReadOnlyProperty = DependencyPropertyRegistrator.Register<PdfViewerControl, bool>(System.Linq.Expressions.Expression.Lambda<Func<PdfViewerControl, bool>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(PdfViewerControl.get_IsReadOnly)), expressionArray2), false);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(PdfViewerControl), "owner");
            ParameterExpression[] expressionArray3 = new ParameterExpression[] { expression };
            PrintPreviewDialogTemplateProperty = DependencyPropertyRegistrator.Register<PdfViewerControl, DataTemplate>(System.Linq.Expressions.Expression.Lambda<Func<PdfViewerControl, DataTemplate>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(PdfViewerControl.get_PrintPreviewDialogTemplate)), expressionArray3), null);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(PdfViewerControl), "owner");
            ParameterExpression[] expressionArray4 = new ParameterExpression[] { expression };
            SaveFileDialogTemplateProperty = DependencyPropertyRegistrator.Register<PdfViewerControl, DataTemplate>(System.Linq.Expressions.Expression.Lambda<Func<PdfViewerControl, DataTemplate>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(PdfViewerControl.get_SaveFileDialogTemplate)), expressionArray4), null);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(PdfViewerControl), "control");
            ParameterExpression[] expressionArray5 = new ParameterExpression[] { expression };
            OutlinesViewerSettingsProperty = DependencyPropertyRegistrator.Register<PdfViewerControl, PdfOutlinesViewerSettings>(System.Linq.Expressions.Expression.Lambda<Func<PdfViewerControl, PdfOutlinesViewerSettings>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(PdfViewerControl.get_OutlinesViewerSettings)), expressionArray5), null, (control, oldValue, newValue) => control.OutlinesViewerSettingsChanged(oldValue, newValue));
            expression = System.Linq.Expressions.Expression.Parameter(typeof(PdfViewerControl), "owner");
            ParameterExpression[] expressionArray6 = new ParameterExpression[] { expression };
            PageLayoutProperty = DependencyPropertyRegistrator.Register<PdfViewerControl, PdfPageLayout>(System.Linq.Expressions.Expression.Lambda<Func<PdfViewerControl, PdfPageLayout>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(PdfViewerControl.get_PageLayout)), expressionArray6), PdfPageLayout.OneColumn, (d, oldValue, newValue) => d.OnPageLayoutChanged(newValue));
            expression = System.Linq.Expressions.Expression.Parameter(typeof(PdfViewerControl), "owner");
            ParameterExpression[] expressionArray7 = new ParameterExpression[] { expression };
            NavigationPanelsLayoutProperty = DependencyPropertyRegistrator.Register<PdfViewerControl, DevExpress.Xpf.PdfViewer.NavigationPanelsLayout>(System.Linq.Expressions.Expression.Lambda<Func<PdfViewerControl, DevExpress.Xpf.PdfViewer.NavigationPanelsLayout>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(PdfViewerControl.get_NavigationPanelsLayout)), expressionArray7), DevExpress.Xpf.PdfViewer.NavigationPanelsLayout.Tab, (d, oldValue, newValue) => d.NavigationPanelsLayoutChanged(newValue));
            CursorModeChangedEvent = EventManager.RegisterRoutedEvent("CursorModeChanged", RoutingStrategy.Direct, typeof(RoutedEventHandler), forType);
            GetDocumentPasswordEvent = EventManager.RegisterRoutedEvent("GetDocumentPassword", RoutingStrategy.Direct, typeof(GetDocumentPasswordEventHandler), forType);
            UriOpeningEvent = EventManager.RegisterRoutedEvent("UriOpening", RoutingStrategy.Direct, typeof(UriOpeningEventHandler), forType);
            ReferencedDocumentOpeningEvent = EventManager.RegisterRoutedEvent("ReferencedDocumentOpening", RoutingStrategy.Direct, typeof(ReferencedDocumentOpeningEventHandler), forType);
            DocumentLoadedEvent = EventManager.RegisterRoutedEvent("DocumentLoaded", RoutingStrategy.Direct, typeof(RoutedEventHandler), forType);
            DocumentClosingEvent = EventManager.RegisterRoutedEvent("DocumentClosing", RoutingStrategy.Direct, typeof(DocumentClosingEventHandler), forType);
            PageLayoutChangedEvent = EventManager.RegisterRoutedEvent("PageLayoutChanged", RoutingStrategy.Direct, typeof(RoutedEventHandler), forType);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(PdfViewerControl), "owner");
            ParameterExpression[] expressionArray8 = new ParameterExpression[] { expression };
            HasOutlinesPropertyKey = DependencyPropertyRegistrator.RegisterReadOnly<PdfViewerControl, bool>(System.Linq.Expressions.Expression.Lambda<Func<PdfViewerControl, bool>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(PdfViewerControl.get_HasOutlines)), expressionArray8), false, null);
            HasOutlinesProperty = HasOutlinesPropertyKey.DependencyProperty;
            expression = System.Linq.Expressions.Expression.Parameter(typeof(PdfViewerControl), "owner");
            ParameterExpression[] expressionArray9 = new ParameterExpression[] { expression };
            ActualAttachmentsViewerSettingsPropertyKey = DependencyPropertyRegistrator.RegisterReadOnly<PdfViewerControl, PdfAttachmentsViewerSettings>(System.Linq.Expressions.Expression.Lambda<Func<PdfViewerControl, PdfAttachmentsViewerSettings>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(PdfViewerControl.get_ActualAttachmentsViewerSettings)), expressionArray9), null, (owner, oldValue, newValue) => owner.ActualAttachmentsViewerSettingsChanged(oldValue, newValue));
            ActualAttachmentsViewerSettingsProperty = ActualAttachmentsViewerSettingsPropertyKey.DependencyProperty;
            expression = System.Linq.Expressions.Expression.Parameter(typeof(PdfViewerControl), "owner");
            ParameterExpression[] expressionArray10 = new ParameterExpression[] { expression };
            AttachmentsViewerSettingsProperty = DependencyPropertyRegistrator.Register<PdfViewerControl, PdfAttachmentsViewerSettings>(System.Linq.Expressions.Expression.Lambda<Func<PdfViewerControl, PdfAttachmentsViewerSettings>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(PdfViewerControl.get_AttachmentsViewerSettings)), expressionArray10), null, (control, oldValue, newValue) => control.AttachmentsViewerSettingsChanged(oldValue, newValue));
            expression = System.Linq.Expressions.Expression.Parameter(typeof(PdfViewerControl), "owner");
            ParameterExpression[] expressionArray11 = new ParameterExpression[] { expression };
            HasAttachmentsPropertyKey = DependencyPropertyRegistrator.RegisterReadOnly<PdfViewerControl, bool>(System.Linq.Expressions.Expression.Lambda<Func<PdfViewerControl, bool>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(PdfViewerControl.get_HasAttachments)), expressionArray11), false, null);
            HasAttachmentsProperty = HasAttachmentsPropertyKey.DependencyProperty;
            expression = System.Linq.Expressions.Expression.Parameter(typeof(PdfViewerControl), "owner");
            ParameterExpression[] expressionArray12 = new ParameterExpression[] { expression };
            ActualThumbnailsViewerSettingsPropertyKey = DependencyPropertyRegistrator.RegisterReadOnly<PdfViewerControl, PdfThumbnailsViewerSettings>(System.Linq.Expressions.Expression.Lambda<Func<PdfViewerControl, PdfThumbnailsViewerSettings>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(PdfViewerControl.get_ActualThumbnailsViewerSettings)), expressionArray12), null, (owner, oldValue, newValue) => owner.ActualThumbnailsViewerSettingsChanged(oldValue, newValue));
            ActualThumbnailsViewerSettingsProperty = ActualThumbnailsViewerSettingsPropertyKey.DependencyProperty;
            expression = System.Linq.Expressions.Expression.Parameter(typeof(PdfViewerControl), "owner");
            ParameterExpression[] expressionArray13 = new ParameterExpression[] { expression };
            ThumbnailsViewerSettingsProperty = DependencyPropertyRegistrator.Register<PdfViewerControl, PdfThumbnailsViewerSettings>(System.Linq.Expressions.Expression.Lambda<Func<PdfViewerControl, PdfThumbnailsViewerSettings>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(PdfViewerControl.get_ThumbnailsViewerSettings)), expressionArray13), null, (control, oldValue, newValue) => control.ThumbnailsViewerSettingsChanged(oldValue, newValue));
            expression = System.Linq.Expressions.Expression.Parameter(typeof(PdfViewerControl), "owner");
            ParameterExpression[] expressionArray14 = new ParameterExpression[] { expression };
            HasThumbnailsPropertyKey = DependencyPropertyRegistrator.RegisterReadOnly<PdfViewerControl, bool>(System.Linq.Expressions.Expression.Lambda<Func<PdfViewerControl, bool>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(PdfViewerControl.get_HasThumbnails)), expressionArray14), false, null);
            HasThumbnailsProperty = HasThumbnailsPropertyKey.DependencyProperty;
            expression = System.Linq.Expressions.Expression.Parameter(typeof(PdfViewerControl), "owner");
            ParameterExpression[] expressionArray15 = new ParameterExpression[] { expression };
            HighlightFormFieldsProperty = DependencyPropertyRegistrator.Register<PdfViewerControl, bool>(System.Linq.Expressions.Expression.Lambda<Func<PdfViewerControl, bool>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(PdfViewerControl.get_HighlightFormFields)), expressionArray15), false, (owner, oldValue, newValue) => owner.HighlightFormFieldsChanged(newValue));
            expression = System.Linq.Expressions.Expression.Parameter(typeof(PdfViewerControl), "owner");
            ParameterExpression[] expressionArray16 = new ParameterExpression[] { expression };
            MarkupToolsSettingsProperty = DependencyPropertyRegistrator.Register<PdfViewerControl, PdfMarkupToolsSettings>(System.Linq.Expressions.Expression.Lambda<Func<PdfViewerControl, PdfMarkupToolsSettings>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(PdfViewerControl.get_MarkupToolsSettings)), expressionArray16), new PdfMarkupToolsSettings());
            expression = System.Linq.Expressions.Expression.Parameter(typeof(PdfViewerControl), "owner");
            ParameterExpression[] expressionArray17 = new ParameterExpression[] { expression };
            ShowPrintStatusDialogProperty = DependencyPropertyRegistrator.Register<PdfViewerControl, bool>(System.Linq.Expressions.Expression.Lambda<Func<PdfViewerControl, bool>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(PdfViewerControl.get_ShowPrintStatusDialog)), expressionArray17), true);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(PdfViewerControl), "owner");
            ParameterExpression[] expressionArray18 = new ParameterExpression[] { expression };
            HighlightedFormFieldColorProperty = DependencyPropertyRegistrator.Register<PdfViewerControl, System.Windows.Media.Color>(System.Linq.Expressions.Expression.Lambda<Func<PdfViewerControl, System.Windows.Media.Color>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(PdfViewerControl.get_HighlightedFormFieldColor)), expressionArray18), System.Windows.Media.Color.FromArgb(0xff, 0xcd, 0xd7, 0xff), (owner, oldValue, newValue) => owner.HighlightedFormFieldColorChanged(newValue));
            expression = System.Linq.Expressions.Expression.Parameter(typeof(PdfViewerControl), "owner");
            ParameterExpression[] expressionArray19 = new ParameterExpression[] { expression };
            ContinueSearchFromProperty = DependencyPropertyRegistrator.Register<PdfViewerControl, PdfContinueSearchFrom>(System.Linq.Expressions.Expression.Lambda<Func<PdfViewerControl, PdfContinueSearchFrom>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(PdfViewerControl.get_ContinueSearchFrom)), expressionArray19), PdfContinueSearchFrom.CurrentPage);
            SelectionStartedEvent = EventManager.RegisterRoutedEvent("SelectionStarted", RoutingStrategy.Direct, typeof(SelectionEventHandler), forType);
            SelectionEndedEvent = EventManager.RegisterRoutedEvent("SelectionEnded", RoutingStrategy.Direct, typeof(SelectionEventHandler), forType);
            SelectionContinuedEvent = EventManager.RegisterRoutedEvent("SelectionContinued", RoutingStrategy.Direct, typeof(SelectionEventHandler), forType);
            AttachmentOpeningEvent = EventManager.RegisterRoutedEvent("AttachmentOpening", RoutingStrategy.Direct, typeof(AttachmentOpeningEventHandler), forType);
            PrintPageEvent = EventManager.RegisterRoutedEvent("PrintPage", RoutingStrategy.Direct, typeof(DevExpress.Xpf.PdfViewer.PdfPrintPageEventHandler), forType);
            QueryPageSettingsEvent = EventManager.RegisterRoutedEvent("QueryPageSettings", RoutingStrategy.Direct, typeof(DevExpress.Xpf.PdfViewer.PdfQueryPageSettingsEventHandler), forType);
            FormFieldValueChangingEvent = EventManager.RegisterRoutedEvent("FormFieldValueChanging", RoutingStrategy.Direct, typeof(FormFieldValueChangingEventHandler), forType);
            FormFieldValueChangedEvent = EventManager.RegisterRoutedEvent("FormFieldValueChanged", RoutingStrategy.Direct, typeof(FormFieldValueChangedEventHandler), forType);
            PageSetupDialogShowingEvent = EventManager.RegisterRoutedEvent("PageSetupDialogShowing", RoutingStrategy.Direct, typeof(PageSetupDialogShowingEventHandler), forType);
            TextMarkupAnnotationCreatingEvent = EventManager.RegisterRoutedEvent("TextMarkupAnnotationCreating", RoutingStrategy.Direct, typeof(PdfTextMarkupAnnotationCreatingEventHandler), forType);
            TextMarkupAnnotationCreatedEvent = EventManager.RegisterRoutedEvent("TextMarkupAnnotationCreated", RoutingStrategy.Direct, typeof(PdfTextMarkupAnnotationCreatedEventHandler), forType);
            AnnotationDeletingEvent = EventManager.RegisterRoutedEvent("AnnotationDeleting", RoutingStrategy.Direct, typeof(AnnotationDeletingEventHandler), forType);
            PopupMenuShowingEvent = EventManager.RegisterRoutedEvent("PopupMenuShowing", RoutingStrategy.Direct, typeof(PopupMenuShowingEventHandler), forType);
            TextMarkupAnnotationChangedEvent = EventManager.RegisterRoutedEvent("TextMarkupAnnotationChanged", RoutingStrategy.Direct, typeof(PdfTextMarkupAnnotationChangedEventHandler), forType);
            MarkupAnnotationGotFocusEvent = EventManager.RegisterRoutedEvent("MarkupAnnotationGotFocus", RoutingStrategy.Direct, typeof(MarkupAnnotationGotFocusEventHandler), forType);
            MarkupAnnotationLostFocusEvent = EventManager.RegisterRoutedEvent("MarkupAnnotationLostFocus", RoutingStrategy.Direct, typeof(MarkupAnnotationLostFocusEventHandler), forType);
            ShownEditorEvent = EventManager.RegisterRoutedEvent("ShownEditor", RoutingStrategy.Direct, typeof(ShownEditorEventHandler), forType);
            ShowingEditorEvent = EventManager.RegisterRoutedEvent("ShowingEditor", RoutingStrategy.Direct, typeof(ShowingEditorEventHandler), forType);
            HiddenEditorEvent = EventManager.RegisterRoutedEvent("HiddenEditor", RoutingStrategy.Direct, typeof(HiddenEditorEventHandler), forType);
            ExceptionMessageEvent = EventManager.RegisterRoutedEvent("ExceptionMessage", RoutingStrategy.Direct, typeof(ExceptionMessageEventHandler), forType);
        }

        public PdfViewerControl()
        {
            base.DefaultStyleKey = typeof(PdfViewerControl);
            this.InteractionProvider = this.CreateInteractionProvider();
            this.InteractionProvider.DocumentViewer = this;
            this.ActualAttachmentsViewerSettings = this.CreateDefaultAttachmentsViewerSettings();
            this.ActualThumbnailsViewerSettings = this.CreateDefaultThumbnailsSettings();
            this.viewerBackend = new PdfViewerBackend(this.InteractionProvider);
            this.viewerBackend.UseDirectX = CompatibilitySettings.RenderPDFPageContentWithDirectX;
            About.CheckLicenseShowNagScreen(typeof(PdfViewerControl));
        }

        protected virtual void ActualAttachmentsViewerSettingsChanged(PdfAttachmentsViewerSettings oldValue, PdfAttachmentsViewerSettings newValue)
        {
            Action<PdfAttachmentsViewerSettings> action = <>c.<>9__378_0;
            if (<>c.<>9__378_0 == null)
            {
                Action<PdfAttachmentsViewerSettings> local1 = <>c.<>9__378_0;
                action = <>c.<>9__378_0 = x => x.Release();
            }
            oldValue.Do<PdfAttachmentsViewerSettings>(action);
            newValue.Do<PdfAttachmentsViewerSettings>(x => x.Initialize(this));
        }

        private void ActualThumbnailsViewerSettingsChanged(PdfThumbnailsViewerSettings oldValue, PdfThumbnailsViewerSettings newValue)
        {
            Action<PdfThumbnailsViewerSettings> action = <>c.<>9__523_0;
            if (<>c.<>9__523_0 == null)
            {
                Action<PdfThumbnailsViewerSettings> local1 = <>c.<>9__523_0;
                action = <>c.<>9__523_0 = x => x.Release();
            }
            oldValue.Do<PdfThumbnailsViewerSettings>(action);
            newValue.Do<PdfThumbnailsViewerSettings>(x => x.Initialize(this));
        }

        private void AddDocumentToRecentFiles(object documentSource)
        {
            string documentName = this.GetDocumentName(documentSource);
            if (!string.IsNullOrEmpty(documentName))
            {
                RecentFileViewModel item = new RecentFileViewModel {
                    Name = documentName,
                    DocumentSource = (documentSource is FileStream) ? ((FileStream) documentSource).Name : documentSource,
                    Command = this.OpenRecentDocumentCommand
                };
                if (this.RecentFiles == null)
                {
                    this.RecentFiles = new ObservableCollection<RecentFileViewModel>();
                }
                if (this.RecentFiles.Contains(item))
                {
                    this.RecentFiles.Remove(item);
                }
                this.RecentFiles.Add(item);
            }
        }

        protected internal bool AllowAccessToPublicHyperlink(Uri uri)
        {
            UriOpeningEventArgs e = new UriOpeningEventArgs(uri);
            base.RaiseEvent(e);
            if (e.Handled)
            {
                return !e.Cancel;
            }
            string str = uri.IsAbsoluteUri ? uri.AbsoluteUri : uri.OriginalString;
            string getString = string.Format(PdfViewerLocalizer.GetString(PdfViewerStringId.MessageSecurityWarningUriOpening), str);
            return (DXMessageBoxHelper.Show(this, getString, PdfViewerLocalizer.GetString(PdfViewerStringId.MessageSecurityWarningCaption), MessageBoxButton.YesNo, MessageBoxImage.Asterisk) == MessageBoxResult.Yes);
        }

        protected void AllowCachePagesChanged(bool newValue)
        {
            this.Document.Do<IPdfDocument>(x => x.ImageCacheSize = newValue ? ((long) (this.CacheSize / 0x100000)) : ((long) 1));
        }

        protected override void AssignDocumentPresenterProperties()
        {
            base.AssignDocumentPresenterProperties();
            this.DocumentPresenter.Do<PdfPresenterControl>(delegate (PdfPresenterControl x) {
                switch (this.PageLayout)
                {
                    case PdfPageLayout.SinglePage:
                    case PdfPageLayout.OneColumn:
                        x.PageDisplayMode = PageDisplayMode.Single;
                        x.ColumnsCount = 1;
                        x.ShowCoverPage = false;
                        break;

                    case PdfPageLayout.TwoColumnLeft:
                    case PdfPageLayout.TwoPageLeft:
                        x.PageDisplayMode = PageDisplayMode.Columns;
                        x.ColumnsCount = 2;
                        x.ShowCoverPage = false;
                        break;

                    case PdfPageLayout.TwoColumnRight:
                    case PdfPageLayout.TwoPageRight:
                        x.PageDisplayMode = PageDisplayMode.Columns;
                        x.ColumnsCount = 2;
                        x.ShowCoverPage = true;
                        break;

                    default:
                        break;
                }
                x.ShowSingleItem = ((this.PageLayout == PdfPageLayout.TwoPageLeft) || (this.PageLayout == PdfPageLayout.TwoPageRight)) || (this.PageLayout == PdfPageLayout.SinglePage);
            });
        }

        protected virtual void AttachmentsViewerSettingsChanged(PdfAttachmentsViewerSettings oldValue, PdfAttachmentsViewerSettings newValue)
        {
            PdfAttachmentsViewerSettings settings1 = newValue;
            if (newValue == null)
            {
                PdfAttachmentsViewerSettings local1 = newValue;
                settings1 = this.CreateDefaultAttachmentsViewerSettings();
            }
            this.ActualAttachmentsViewerSettings = settings1;
        }

        protected virtual void CacheSizeChanged(int newValue)
        {
            this.Document.Do<IPdfDocument>(x => x.ImageCacheSize = this.AllowCachePages ? ((long) (newValue / 0x100000)) : ((long) 1));
        }

        private bool CalcHasAttachments(IPdfDocument model) => 
            !this.ActualAttachmentsViewerSettings.HideAttachmentsViewer;

        private bool CalcHasOutlines(IPdfDocument model)
        {
            if (this.ActualDocumentMapSettings.HideOutlinesViewer)
            {
                return false;
            }
            Func<IPdfDocument, bool> evaluator = <>c.<>9__393_0;
            if (<>c.<>9__393_0 == null)
            {
                Func<IPdfDocument, bool> local1 = <>c.<>9__393_0;
                evaluator = <>c.<>9__393_0 = x => x.HasOutlines;
            }
            return model.If<IPdfDocument>(evaluator).ReturnSuccess<IPdfDocument>();
        }

        private bool CalcHasThumbnails(IPdfDocument model) => 
            !this.ActualThumbnailsViewerSettings.HideThumbnailsViewer;

        protected virtual bool CanCopy() => 
            this.HasSelection;

        private bool CanExportFormData()
        {
            Func<IPdfDocument, bool> evaluator = <>c.<>9__426_0;
            if (<>c.<>9__426_0 == null)
            {
                Func<IPdfDocument, bool> local1 = <>c.<>9__426_0;
                evaluator = <>c.<>9__426_0 = x => x.IsLoaded && x.HasInteractiveForm;
            }
            return this.Document.Return<IPdfDocument, bool>(evaluator, (<>c.<>9__426_1 ??= () => false));
        }

        private bool CanImportFormData()
        {
            Func<bool> fallback = <>c.<>9__425_1;
            if (<>c.<>9__425_1 == null)
            {
                Func<bool> local1 = <>c.<>9__425_1;
                fallback = <>c.<>9__425_1 = () => false;
            }
            return this.Document.Return<IPdfDocument, bool>(x => (x.IsLoaded && (x.HasInteractiveForm && !this.IsReadOnly)), fallback);
        }

        protected virtual bool CanNavigate(PdfOutlineTreeListItem item)
        {
            if (item == null)
            {
                return false;
            }
            Func<IPdfDocument, bool> evaluator = <>c.<>9__424_0;
            if (<>c.<>9__424_0 == null)
            {
                Func<IPdfDocument, bool> local1 = <>c.<>9__424_0;
                evaluator = <>c.<>9__424_0 = x => x.IsLoaded;
            }
            return this.Document.Return<IPdfDocument, bool>(evaluator, (<>c.<>9__424_1 ??= () => false));
        }

        protected virtual bool CanNavigate(PdfTarget target)
        {
            if (target == null)
            {
                return false;
            }
            Func<IPdfDocument, bool> evaluator = <>c.<>9__423_0;
            if (<>c.<>9__423_0 == null)
            {
                Func<IPdfDocument, bool> local1 = <>c.<>9__423_0;
                evaluator = <>c.<>9__423_0 = x => x.IsLoaded;
            }
            return this.Document.Return<IPdfDocument, bool>(evaluator, (<>c.<>9__423_1 ??= () => false));
        }

        public virtual bool CanNavigate(PdfDocumentArea area)
        {
            if (area == null)
            {
                return false;
            }
            Func<IPdfDocument, bool> evaluator = <>c.<>9__428_0;
            if (<>c.<>9__428_0 == null)
            {
                Func<IPdfDocument, bool> local1 = <>c.<>9__428_0;
                evaluator = <>c.<>9__428_0 = x => x.IsLoaded;
            }
            return this.Document.Return<IPdfDocument, bool>(evaluator, (<>c.<>9__428_1 ??= () => false));
        }

        protected virtual bool CanNavigate(PdfDocumentPosition position)
        {
            if (position == null)
            {
                return false;
            }
            Func<IPdfDocument, bool> evaluator = <>c.<>9__427_0;
            if (<>c.<>9__427_0 == null)
            {
                Func<IPdfDocument, bool> local1 = <>c.<>9__427_0;
                evaluator = <>c.<>9__427_0 = x => x.IsLoaded;
            }
            return this.Document.Return<IPdfDocument, bool>(evaluator, (<>c.<>9__427_1 ??= () => false));
        }

        protected virtual bool CanOpenAttachment(object parameter) => 
            parameter != null;

        private bool CanOpenRecentDocument(object document) => 
            true;

        private bool CanPrint()
        {
            if (new PrinterItemContainer().Items.Count <= 0)
            {
                return false;
            }
            Func<IPdfDocument, bool> func1 = <>c.<>9__491_0;
            if (<>c.<>9__491_0 == null)
            {
                Func<IPdfDocument, bool> local1 = <>c.<>9__491_0;
                func1 = <>c.<>9__491_0 = x => x.IsLoaded;
            }
            return (((IPdfDocument) func1).Return<IPdfDocument, bool>(((Func<IPdfDocument, bool>) (<>c.<>9__491_1 ??= () => false)), (<>c.<>9__491_1 ??= () => false)) && this.Document.Pages.Any<IPdfPage>());
        }

        protected virtual bool CanSaveAs()
        {
            Func<IPdfDocument, bool> evaluator = <>c.<>9__402_0;
            if (<>c.<>9__402_0 == null)
            {
                Func<IPdfDocument, bool> local1 = <>c.<>9__402_0;
                evaluator = <>c.<>9__402_0 = x => x.IsLoaded;
            }
            return this.Document.Return<IPdfDocument, bool>(evaluator, (<>c.<>9__402_1 ??= () => false));
        }

        protected virtual bool CanSaveAttachment(object parameter) => 
            parameter != null;

        private bool CanSelect(PdfSelectionCommand command)
        {
            Func<IPdfDocument, bool> evaluator = <>c.<>9__497_0;
            if (<>c.<>9__497_0 == null)
            {
                Func<IPdfDocument, bool> local1 = <>c.<>9__497_0;
                evaluator = <>c.<>9__497_0 = x => x.IsLoaded;
            }
            return this.Document.Return<IPdfDocument, bool>(evaluator, (<>c.<>9__497_1 ??= () => false));
        }

        private bool CanSelectAll()
        {
            Func<IPdfDocument, bool> evaluator = <>c.<>9__498_0;
            if (<>c.<>9__498_0 == null)
            {
                Func<IPdfDocument, bool> local1 = <>c.<>9__498_0;
                evaluator = <>c.<>9__498_0 = x => x.IsLoaded;
            }
            return this.Document.Return<IPdfDocument, bool>(evaluator, (<>c.<>9__498_1 ??= () => false));
        }

        protected virtual bool CanSetCommentCursorMode(CursorModeType cursorMode)
        {
            Func<IPdfDocument, bool> evaluator = <>c.<>9__400_0;
            if (<>c.<>9__400_0 == null)
            {
                Func<IPdfDocument, bool> local1 = <>c.<>9__400_0;
                evaluator = <>c.<>9__400_0 = x => x.IsLoaded;
            }
            return this.Document.Return<IPdfDocument, bool>(evaluator, (<>c.<>9__400_1 ??= () => false));
        }

        protected virtual bool CanSetCursorMode(CursorModeType cursorMode)
        {
            Func<IPdfDocument, bool> evaluator = <>c.<>9__398_0;
            if (<>c.<>9__398_0 == null)
            {
                Func<IPdfDocument, bool> local1 = <>c.<>9__398_0;
                evaluator = <>c.<>9__398_0 = x => x.IsLoaded;
            }
            return this.Document.Return<IPdfDocument, bool>(evaluator, (<>c.<>9__398_1 ??= () => false));
        }

        protected virtual bool CanSetPageLayout(PdfPageLayout pageLayout)
        {
            Func<IPdfDocument, bool> evaluator = <>c.<>9__405_0;
            if (<>c.<>9__405_0 == null)
            {
                Func<IPdfDocument, bool> local1 = <>c.<>9__405_0;
                evaluator = <>c.<>9__405_0 = x => x.IsLoaded;
            }
            return this.Document.Return<IPdfDocument, bool>(evaluator, (<>c.<>9__405_1 ??= () => false));
        }

        protected virtual bool CanShowCoverPage()
        {
            Func<IPdfDocument, bool> func1 = <>c.<>9__407_0;
            if (<>c.<>9__407_0 == null)
            {
                Func<IPdfDocument, bool> local1 = <>c.<>9__407_0;
                func1 = <>c.<>9__407_0 = x => x.IsLoaded;
            }
            return (((IPdfDocument) func1).Return<IPdfDocument, bool>(((Func<IPdfDocument, bool>) (<>c.<>9__407_1 ??= () => false)), (<>c.<>9__407_1 ??= () => false)) && ((this.PageLayout != PdfPageLayout.OneColumn) && (this.PageLayout != PdfPageLayout.SinglePage)));
        }

        private bool CanShowProperties()
        {
            Func<IPdfDocument, bool> evaluator = <>c.<>9__489_0;
            if (<>c.<>9__489_0 == null)
            {
                Func<IPdfDocument, bool> local1 = <>c.<>9__489_0;
                evaluator = <>c.<>9__489_0 = x => x.IsLoaded;
            }
            return this.Document.Return<IPdfDocument, bool>(evaluator, (<>c.<>9__489_1 ??= () => false));
        }

        private bool CanUnselectAll()
        {
            Func<IPdfDocument, bool> evaluator = <>c.<>9__499_0;
            if (<>c.<>9__499_0 == null)
            {
                Func<IPdfDocument, bool> local1 = <>c.<>9__499_0;
                evaluator = <>c.<>9__499_0 = x => x.IsLoaded;
            }
            return this.Document.Return<IPdfDocument, bool>(evaluator, (<>c.<>9__499_1 ??= () => false));
        }

        private void CheckOperationAvailability()
        {
            Func<IPdfDocument, bool> evaluator = <>c.<>9__492_0;
            if (<>c.<>9__492_0 == null)
            {
                Func<IPdfDocument, bool> local1 = <>c.<>9__492_0;
                evaluator = <>c.<>9__492_0 = x => x.IsLoaded;
            }
            if (!this.Document.Return<IPdfDocument, bool>(evaluator, (<>c.<>9__492_1 ??= () => false)))
            {
                throw new InvalidOperationException(PdfCoreLocalizer.GetString(PdfCoreStringId.MsgUnavailableOperation));
            }
        }

        protected override void CloseDocument()
        {
            Action<DevExpress.Xpf.PdfViewer.Internal.InteractionProvider> action = <>c.<>9__388_0;
            if (<>c.<>9__388_0 == null)
            {
                Action<DevExpress.Xpf.PdfViewer.Internal.InteractionProvider> local1 = <>c.<>9__388_0;
                action = <>c.<>9__388_0 = x => x.CommitEditor();
            }
            this.InteractionProvider.Do<DevExpress.Xpf.PdfViewer.Internal.InteractionProvider>(action);
            base.CloseDocument();
            if (base.DocumentSource == null)
            {
                this.HasOutlines = false;
                this.HasAttachments = false;
                this.HasThumbnails = false;
                this.ActualThumbnailsViewerSettings.Release();
                this.ActualThumbnailsViewerSettings.RaiseInvalidate();
            }
        }

        public System.Windows.Point ConvertDocumentPositionToPixel(PdfDocumentPosition position)
        {
            Func<System.Windows.Point> fallback = <>c.<>9__465_1;
            if (<>c.<>9__465_1 == null)
            {
                Func<System.Windows.Point> local1 = <>c.<>9__465_1;
                fallback = <>c.<>9__465_1 = () => new System.Windows.Point(0.0, 0.0);
            }
            return this.DocumentPresenter.Return<PdfPresenterControl, System.Windows.Point>(x => x.ConvertDocumentPositionToPoint(position), fallback);
        }

        public PdfDocumentPosition ConvertPixelToDocumentPosition(System.Windows.Point point) => 
            this.ConvertPixelToDocumentPosition(point, false);

        public PdfDocumentPosition ConvertPixelToDocumentPosition(System.Windows.Point point, bool inPageBounds) => 
            this.DocumentPresenter.With<PdfPresenterControl, PdfDocumentPosition>(x => x.ConvertPointToDocumentPosition(point, inPageBounds));

        protected virtual void Copy()
        {
            Func<IPdfDocument, IPdfDocumentSelectionResults> evaluator = <>c.<>9__419_0;
            if (<>c.<>9__419_0 == null)
            {
                Func<IPdfDocument, IPdfDocumentSelectionResults> local1 = <>c.<>9__419_0;
                evaluator = <>c.<>9__419_0 = x => x.SelectionResults;
            }
            if (this.Document.With<IPdfDocument, IPdfDocumentSelectionResults>(evaluator) != null)
            {
                if (this.Document.SelectionResults.ContentType == PdfDocumentContentType.Text)
                {
                    Clipboard.SetText(this.Document.SelectionResults.Text);
                }
                else if (this.Document.SelectionResults.ContentType == PdfDocumentContentType.Image)
                {
                    BitmapSource image = this.Document.SelectionResults.GetImage((int) (((Rotation) 90) * base.PageRotation));
                    if (image != null)
                    {
                        Clipboard.SetImage(image);
                    }
                }
            }
        }

        protected override BehaviorProvider CreateBehaviorProvider() => 
            new PdfBehaviorProvider();

        public BitmapSource CreateBitmap(int index)
        {
            if (!base.IsDocumentContainPages())
            {
                return null;
            }
            IPdfPage page = this.Document.Pages.ElementAt<IPdfPage>(index);
            return this.CreateBitmap(index, (int) Math.Max(page.PageSize.Width, page.PageSize.Height));
        }

        public BitmapSource CreateBitmap(int index, int largestEdgeLength) => 
            !base.IsDocumentContainPages() ? null : this.Document.CreateBitmap(index, largestEdgeLength);

        protected override CommandProvider CreateCommandProvider() => 
            new PdfCommandProvider();

        protected virtual PdfAttachmentsViewerSettings CreateDefaultAttachmentsViewerSettings() => 
            new PdfAttachmentsViewerSettings();

        protected override DocumentMapSettings CreateDefaultDocumentMapSettings() => 
            new PdfOutlinesViewerSettings();

        private DialogService CreateDefaultPrintPreviewDialogService()
        {
            PdfDialogService service1 = new PdfDialogService();
            service1.DialogWindowStartupLocation = WindowStartupLocation.CenterScreen;
            PdfViewerThemeKeyExtension resourceKey = new PdfViewerThemeKeyExtension();
            resourceKey.ResourceKey = PdfViewerThemeKeys.PdfPrintEditorTemplate;
            resourceKey.ThemeName = ThemeHelper.GetEditorThemeName(this);
            service1.ViewTemplate = (DataTemplate) base.FindResource(resourceKey);
            PdfViewerThemeKeyExtension extension2 = new PdfViewerThemeKeyExtension();
            extension2.ResourceKey = PdfViewerThemeKeys.PdfPrintDialogStyle;
            extension2.ThemeName = ThemeHelper.GetEditorThemeName(this);
            service1.DialogStyle = (Style) base.FindResource(extension2);
            return service1;
        }

        protected virtual SaveFileDialogService CreateDefaultSaveFileDialogService()
        {
            SaveFileDialogService service1 = new SaveFileDialogService();
            service1.DefaultExt = PdfViewerLocalizer.GetString(PdfViewerStringId.PdfFileExtension);
            service1.Filter = PdfViewerLocalizer.GetString(PdfViewerStringId.PdfFileFilter);
            return service1;
        }

        protected virtual PdfThumbnailsViewerSettings CreateDefaultThumbnailsSettings() => 
            new PdfThumbnailsViewerSettings();

        protected override DevExpress.Xpf.DocumentViewer.IDocument CreateDocument(object source)
        {
            PdfDocumentViewModel model = new PdfDocumentViewModel(this.ViewerController, this.viewerBackend);
            model.RequestPassword += new EventHandler<RequestPasswordEventArgs>(this.OnDocumentRequestPassword);
            model.DocumentProgressChanged += new EventHandler<DocumentProgressChangedEventArgs>(this.OnDocumentProgressChanged);
            return model;
        }

        protected virtual DevExpress.Xpf.PdfViewer.Internal.InteractionProvider CreateInteractionProvider() => 
            new DevExpress.Xpf.PdfViewer.Internal.InteractionProvider();

        private PdfPrintDialogViewModel CreatePrintViewModel(PdfDocumentViewModel documentViewModel, string title, PdfPrinterSettings printerSettings) => 
            new PdfPrintDialogViewModel(this.viewerBackend, new System.Drawing.Size(0x1ab, 0x206), base.CurrentPageNumber, this.MaxPrintingDpi, msg => DXMessageBoxHelper.Show(this, msg, title, MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK, printerSettings);

        protected override PropertyProvider CreatePropertyProvider() => 
            new PdfPropertyProvider();

        public void CreateTiff(Stream stream, int largestEdgeLength)
        {
            this.CheckOperationAvailability();
            Func<PdfDocumentViewModel, PdfViewerBackend> evaluator = <>c.<>9__479_0;
            if (<>c.<>9__479_0 == null)
            {
                Func<PdfDocumentViewModel, PdfViewerBackend> local1 = <>c.<>9__479_0;
                evaluator = <>c.<>9__479_0 = x => x.ViewerBackend;
            }
            (this.Document as PdfDocumentViewModel).With<PdfDocumentViewModel, PdfViewerBackend>(evaluator).Do<PdfViewerBackend>(x => x.CreateTiff(stream, largestEdgeLength));
        }

        public void CreateTiff(string fileName, int largestEdgeLength)
        {
            this.CheckOperationAvailability();
            using (Stream stream = File.Create(fileName))
            {
                Func<PdfDocumentViewModel, PdfViewerBackend> evaluator = <>c.<>9__476_0;
                if (<>c.<>9__476_0 == null)
                {
                    Func<PdfDocumentViewModel, PdfViewerBackend> local1 = <>c.<>9__476_0;
                    evaluator = <>c.<>9__476_0 = x => x.ViewerBackend;
                }
                (this.Document as PdfDocumentViewModel).With<PdfDocumentViewModel, PdfViewerBackend>(evaluator).Do<PdfViewerBackend>(x => x.CreateTiff(stream, largestEdgeLength));
            }
        }

        public void CreateTiff(Stream stream, IEnumerable<int> pageNumbers, float imageDpi)
        {
            this.CheckOperationAvailability();
            Func<PdfDocumentViewModel, PdfViewerBackend> evaluator = <>c.<>9__481_0;
            if (<>c.<>9__481_0 == null)
            {
                Func<PdfDocumentViewModel, PdfViewerBackend> local1 = <>c.<>9__481_0;
                evaluator = <>c.<>9__481_0 = x => x.ViewerBackend;
            }
            (this.Document as PdfDocumentViewModel).With<PdfDocumentViewModel, PdfViewerBackend>(evaluator).Do<PdfViewerBackend>(x => x.CreateTiff(stream, pageNumbers, imageDpi));
        }

        public void CreateTiff(Stream stream, int largestEdgeLength, IEnumerable<int> pageNumbers)
        {
            this.CheckOperationAvailability();
            Func<PdfDocumentViewModel, PdfViewerBackend> evaluator = <>c.<>9__480_0;
            if (<>c.<>9__480_0 == null)
            {
                Func<PdfDocumentViewModel, PdfViewerBackend> local1 = <>c.<>9__480_0;
                evaluator = <>c.<>9__480_0 = x => x.ViewerBackend;
            }
            (this.Document as PdfDocumentViewModel).With<PdfDocumentViewModel, PdfViewerBackend>(evaluator).Do<PdfViewerBackend>(x => x.CreateTiff(stream, largestEdgeLength, pageNumbers));
        }

        public void CreateTiff(string fileName, IEnumerable<int> pageNumbers, float imageDpi)
        {
            this.CheckOperationAvailability();
            using (Stream stream = File.Create(fileName))
            {
                Func<PdfDocumentViewModel, PdfViewerBackend> evaluator = <>c.<>9__478_0;
                if (<>c.<>9__478_0 == null)
                {
                    Func<PdfDocumentViewModel, PdfViewerBackend> local1 = <>c.<>9__478_0;
                    evaluator = <>c.<>9__478_0 = x => x.ViewerBackend;
                }
                (this.Document as PdfDocumentViewModel).With<PdfDocumentViewModel, PdfViewerBackend>(evaluator).Do<PdfViewerBackend>(x => x.CreateTiff(stream, pageNumbers, imageDpi));
            }
        }

        public void CreateTiff(string fileName, int largestEdgeLength, IEnumerable<int> pageNumbers)
        {
            this.CheckOperationAvailability();
            using (Stream stream = File.Create(fileName))
            {
                Func<PdfDocumentViewModel, PdfViewerBackend> evaluator = <>c.<>9__477_0;
                if (<>c.<>9__477_0 == null)
                {
                    Func<PdfDocumentViewModel, PdfViewerBackend> local1 = <>c.<>9__477_0;
                    evaluator = <>c.<>9__477_0 = x => x.ViewerBackend;
                }
                (this.Document as PdfDocumentViewModel).With<PdfDocumentViewModel, PdfViewerBackend>(evaluator).Do<PdfViewerBackend>(x => x.CreateTiff(stream, largestEdgeLength, pageNumbers));
            }
        }

        protected virtual void DeleteAnnotation()
        {
            Func<PdfDocumentViewModel, PdfDocumentStateController> evaluator = <>c.<>9__408_0;
            if (<>c.<>9__408_0 == null)
            {
                Func<PdfDocumentViewModel, PdfDocumentStateController> local1 = <>c.<>9__408_0;
                evaluator = <>c.<>9__408_0 = x => x.DocumentStateController;
            }
            Action<PdfDocumentStateController> action = <>c.<>9__408_1;
            if (<>c.<>9__408_1 == null)
            {
                Action<PdfDocumentStateController> local2 = <>c.<>9__408_1;
                action = <>c.<>9__408_1 = x => x.RemoveSelectedAnnotation();
            }
            (this.Document as PdfDocumentViewModel).With<PdfDocumentViewModel, PdfDocumentStateController>(evaluator).Do<PdfDocumentStateController>(action);
        }

        PdfDocumentProcessorHelper IPdfViewer.GetDocumentProcessorHelper()
        {
            if ((this.Document != null) && (((PdfDocumentViewModel) this.Document).DocumentState == null))
            {
                return null;
            }
            Func<PdfDocumentViewModel, PdfFormData> evaluator = <>c.<>9__518_0;
            if (<>c.<>9__518_0 == null)
            {
                Func<PdfDocumentViewModel, PdfFormData> local1 = <>c.<>9__518_0;
                evaluator = <>c.<>9__518_0 = x => x.GetFormData();
            }
            return new PdfDocumentProcessorHelper(new Func<Stream, PdfSaveOptions, bool>(this.SaveDocument), new Func<string, PdfSaveOptions, bool>(this.SaveDocument), ((PdfDocumentViewModel) this.Document).With<PdfDocumentViewModel, PdfFormData>(evaluator));
        }

        private void DoWithDocumentProgress(Action action)
        {
            PdfDocumentViewModel document = this.Document as PdfDocumentViewModel;
            try
            {
                Action<ISplashScreenService> action1 = <>c.<>9__418_0;
                if (<>c.<>9__418_0 == null)
                {
                    Action<ISplashScreenService> local1 = <>c.<>9__418_0;
                    action1 = <>c.<>9__418_0 = x => x.ShowSplashScreen();
                }
                this.SplashScreenService.Do<ISplashScreenService>(action1);
                action();
            }
            finally
            {
                Action<ISplashScreenService> action2 = <>c.<>9__418_1;
                if (<>c.<>9__418_1 == null)
                {
                    Action<ISplashScreenService> local2 = <>c.<>9__418_1;
                    action2 = <>c.<>9__418_1 = x => x.HideSplashScreen();
                }
                this.SplashScreenService.Do<ISplashScreenService>(action2);
            }
        }

        protected override void ExecuteNavigate(object parameter)
        {
            base.ExecuteNavigate(parameter);
            PdfTarget target = parameter as PdfTarget;
            if (target != null)
            {
                this.Navigate(target);
            }
            else
            {
                PdfOutlineTreeListItem item = parameter as PdfOutlineTreeListItem;
                if (item != null)
                {
                    this.Navigate(item);
                }
            }
        }

        public void ExportFormData()
        {
            if (this.CanExportFormData())
            {
                this.InteractionProvider.CommitEditor();
                SaveFileDialogService saveFileDialogService = new SaveFileDialogService {
                    Filter = PdfViewerLocalizer.GetString(PdfViewerStringId.FormDataFileFilter),
                    RestoreDirectory = true
                };
                Func<PdfDocumentViewModel, string> evaluator = <>c.<>9__450_0;
                if (<>c.<>9__450_0 == null)
                {
                    Func<PdfDocumentViewModel, string> local1 = <>c.<>9__450_0;
                    evaluator = <>c.<>9__450_0 = x => x.FilePath;
                }
                string str = (this.Document as PdfDocumentViewModel).With<PdfDocumentViewModel, string>(evaluator);
                if (!string.IsNullOrEmpty(str))
                {
                    saveFileDialogService.DefaultFileName = Path.GetFileNameWithoutExtension(str);
                }
                if (saveFileDialogService.ShowDialog(null, null))
                {
                    this.PerformExceptionMessageAction(() => ((PdfDocumentViewModel) this.Document).SaveFormData(saveFileDialogService.GetFullFileName(), saveFileDialogService.GetFullFileName().EndsWith("xml") ? PdfFormDataFormat.Xml : PdfFormDataFormat.Fdf), string.Format(PdfViewerLocalizer.GetString(PdfViewerStringId.MessageExportError), saveFileDialogService.GetFullFileName()), ExceptionMessageOrigin.ExportFormData, MessageBoxImage.Exclamation, null);
                }
            }
        }

        protected override void FindNextText(TextSearchParameter parameter)
        {
            parameter.CurrentPage = base.CurrentPageNumber;
            PdfTextSearchResults results = (PdfTextSearchResults) this.FindTextCore(parameter);
            if (results.Status == PdfTextSearchStatus.Finished)
            {
                DXMessageBoxHelper.Show(this, PdfViewerLocalizer.GetString(PdfViewerStringId.MessageSearchFinished), PdfViewerLocalizer.GetString(PdfViewerStringId.MessageSearchCaption), MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
            else if (results.Status == PdfTextSearchStatus.NotFound)
            {
                DXMessageBoxHelper.Show(this, PdfViewerLocalizer.GetString(PdfViewerStringId.MessageSearchFinishedNoMatchesFound), PdfViewerLocalizer.GetString(PdfViewerStringId.MessageSearchCaption), MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
        }

        public virtual PdfTextSearchResults FindText(TextSearchParameter parameter) => 
            (PdfTextSearchResults) this.FindTextCore(parameter);

        protected override object FindTextCore(TextSearchParameter search) => 
            this.Document.With<IPdfDocument, PdfTextSearchResults>(x => x.PerformSearch(search));

        private string GetDocumentName(object documentSource)
        {
            if (documentSource is string)
            {
                return Path.GetFileName((string) documentSource);
            }
            if (!(documentSource is Uri))
            {
                return (!(documentSource is FileStream) ? string.Empty : Path.GetFileName((documentSource as FileStream).Name));
            }
            char[] separator = new char[] { '/' };
            return (documentSource as Uri).AbsolutePath.Split(separator).Last<string>();
        }

        protected override string GetOpenFileFilter() => 
            PdfViewerLocalizer.GetString(PdfViewerStringId.PdfFileFilter);

        public System.Windows.Size GetPageSize(int pageNumber)
        {
            this.CheckOperationAvailability();
            Func<PdfDocumentViewModel, IList<PdfPageViewModel>> evaluator = <>c.<>9__430_0;
            if (<>c.<>9__430_0 == null)
            {
                Func<PdfDocumentViewModel, IList<PdfPageViewModel>> local1 = <>c.<>9__430_0;
                evaluator = <>c.<>9__430_0 = x => x.Pages;
            }
            Func<PdfPageViewModel, System.Windows.Size> func2 = <>c.<>9__430_2;
            if (<>c.<>9__430_2 == null)
            {
                Func<PdfPageViewModel, System.Windows.Size> local2 = <>c.<>9__430_2;
                func2 = <>c.<>9__430_2 = x => x.InchPageSize;
            }
            return (this.Document as PdfDocumentViewModel).With<PdfDocumentViewModel, IList<PdfPageViewModel>>(evaluator).With<IList<PdfPageViewModel>, PdfPageViewModel>(x => x.ElementAtOrDefault<PdfPageViewModel>((pageNumber - 1))).Return<PdfPageViewModel, System.Windows.Size>(func2, (<>c.<>9__430_3 ??= () => System.Windows.Size.Empty));
        }

        private SecureString GetPassword(string path, int tryNumber)
        {
            string documentName = string.IsNullOrEmpty(path) ? PdfCoreLocalizer.GetString(PdfCoreStringId.DefaultDocumentName) : Path.GetFileName(path);
            if (documentName.Length > 0x19)
            {
                documentName = documentName.Substring(0, 0x19) + "...";
            }
            if (tryNumber > 1)
            {
                DXMessageBoxHelper.Show(this, PdfViewerLocalizer.GetString(PdfViewerStringId.MessageIncorrectPassword), documentName, MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
            GetDocumentPasswordEventArgs e = new GetDocumentPasswordEventArgs(path);
            base.RaiseEvent(e);
            if (e.Handled)
            {
                return e.Password;
            }
            PasswordViewModel model1 = new PasswordViewModel();
            model1.Password = null;
            PasswordViewModel passwordModel = model1;
            this.ShowGetPasswordDialog(passwordModel, documentName);
            return passwordModel.Password;
        }

        public PdfSelectionContent GetSelectionContent()
        {
            this.CheckOperationAvailability();
            Func<PdfDocumentViewModel, PdfDocumentSelectionResults> evaluator = <>c.<>9__431_0;
            if (<>c.<>9__431_0 == null)
            {
                Func<PdfDocumentViewModel, PdfDocumentSelectionResults> local1 = <>c.<>9__431_0;
                evaluator = <>c.<>9__431_0 = x => x.SelectionResults as PdfDocumentSelectionResults;
            }
            PdfDocumentSelectionResults results = (this.Document as PdfDocumentViewModel).With<PdfDocumentViewModel, PdfDocumentSelectionResults>(evaluator);
            return ((results != null) ? ((results.ContentType != PdfDocumentContentType.Text) ? ((results.ContentType == PdfDocumentContentType.Image) ? new PdfSelectionContent(PdfSelectionContentType.Image, results.GetImage((int) (((Rotation) 90) * base.PageRotation)), null) : new PdfSelectionContent(PdfSelectionContentType.None, null, null)) : new PdfSelectionContent(PdfSelectionContentType.Text, null, results.Text)) : new PdfSelectionContent(PdfSelectionContentType.None, null, null));
        }

        public string GetText(PdfDocumentArea area)
        {
            this.CheckOperationAvailability();
            return this.Document.GetText(area);
        }

        public string GetText(PdfDocumentPosition start, PdfDocumentPosition end)
        {
            this.CheckOperationAvailability();
            return this.Document.GetText(start, end);
        }

        protected virtual void HighlightedFormFieldColorChanged(System.Windows.Media.Color newValue)
        {
            Func<PdfDocumentViewModel, PdfDocumentState> evaluator = <>c.<>9__533_0;
            if (<>c.<>9__533_0 == null)
            {
                Func<PdfDocumentViewModel, PdfDocumentState> local1 = <>c.<>9__533_0;
                evaluator = <>c.<>9__533_0 = x => x.DocumentState;
            }
            (this.Document as PdfDocumentViewModel).With<PdfDocumentViewModel, PdfDocumentState>(evaluator).Do<PdfDocumentState>(x => x.HighlightedFormFieldColor = new PdfRgbaColor(((double) newValue.R) / 255.0, ((double) newValue.G) / 255.0, ((double) newValue.B) / 255.0, ((double) newValue.A) / 255.0));
        }

        protected virtual void HighlightFormFieldsChanged(bool newValue)
        {
            Func<PdfDocumentViewModel, PdfDocumentState> evaluator = <>c.<>9__532_0;
            if (<>c.<>9__532_0 == null)
            {
                Func<PdfDocumentViewModel, PdfDocumentState> local1 = <>c.<>9__532_0;
                evaluator = <>c.<>9__532_0 = x => x.DocumentState;
            }
            (this.Document as PdfDocumentViewModel).With<PdfDocumentViewModel, PdfDocumentState>(evaluator).Do<PdfDocumentState>(x => x.HighlightFormFields = newValue);
        }

        public void HighlightSelectedText()
        {
            Func<PdfDocumentViewModel, PdfDocumentStateController> evaluator = <>c.<>9__432_0;
            if (<>c.<>9__432_0 == null)
            {
                Func<PdfDocumentViewModel, PdfDocumentStateController> local1 = <>c.<>9__432_0;
                evaluator = <>c.<>9__432_0 = x => x.DocumentStateController;
            }
            Action<PdfDocumentStateController> action = <>c.<>9__432_1;
            if (<>c.<>9__432_1 == null)
            {
                Action<PdfDocumentStateController> local2 = <>c.<>9__432_1;
                action = <>c.<>9__432_1 = x => x.AddTextMarkupAnnotation(PdfTextMarkupAnnotationType.Highlight);
            }
            (this.Document as PdfDocumentViewModel).With<PdfDocumentViewModel, PdfDocumentStateController>(evaluator).Do<PdfDocumentStateController>(action);
        }

        public void HighlightSelectedText(string comment)
        {
            Func<PdfDocumentViewModel, PdfDocumentStateController> evaluator = <>c.<>9__438_0;
            if (<>c.<>9__438_0 == null)
            {
                Func<PdfDocumentViewModel, PdfDocumentStateController> local1 = <>c.<>9__438_0;
                evaluator = <>c.<>9__438_0 = x => x.DocumentStateController;
            }
            (this.Document as PdfDocumentViewModel).With<PdfDocumentViewModel, PdfDocumentStateController>(evaluator).Do<PdfDocumentStateController>(x => x.AddTextMarkupAnnotation(PdfTextMarkupAnnotationType.Highlight, comment));
        }

        public void HighlightSelectedText(System.Windows.Media.Color color)
        {
            System.Drawing.Color drawingColor = System.Drawing.Color.FromArgb(color.A, color.R, color.G, color.B);
            Func<PdfDocumentViewModel, PdfDocumentStateController> evaluator = <>c.<>9__435_0;
            if (<>c.<>9__435_0 == null)
            {
                Func<PdfDocumentViewModel, PdfDocumentStateController> local1 = <>c.<>9__435_0;
                evaluator = <>c.<>9__435_0 = x => x.DocumentStateController;
            }
            (this.Document as PdfDocumentViewModel).With<PdfDocumentViewModel, PdfDocumentStateController>(evaluator).Do<PdfDocumentStateController>(x => x.AddTextMarkupAnnotation(PdfTextMarkupAnnotationType.Highlight, null, drawingColor));
        }

        public void HighlightSelectedText(string comment, System.Windows.Media.Color color)
        {
            System.Drawing.Color drawingColor = System.Drawing.Color.FromArgb(color.A, color.R, color.G, color.B);
            Func<PdfDocumentViewModel, PdfDocumentStateController> evaluator = <>c.<>9__441_0;
            if (<>c.<>9__441_0 == null)
            {
                Func<PdfDocumentViewModel, PdfDocumentStateController> local1 = <>c.<>9__441_0;
                evaluator = <>c.<>9__441_0 = x => x.DocumentStateController;
            }
            (this.Document as PdfDocumentViewModel).With<PdfDocumentViewModel, PdfDocumentStateController>(evaluator).Do<PdfDocumentStateController>(x => x.AddTextMarkupAnnotation(PdfTextMarkupAnnotationType.Highlight, comment, drawingColor));
        }

        public PdfHitTestResult HitTest(System.Windows.Point point)
        {
            this.CheckOperationAvailability();
            return this.DocumentPresenter.With<PdfPresenterControl, PdfHitTestResult>(x => x.HitTest(point));
        }

        public void ImportFormData()
        {
            if (this.CanImportFormData())
            {
                this.InteractionProvider.CommitEditor();
                OpenFileDialogService service = new OpenFileDialogService {
                    Filter = PdfViewerLocalizer.GetString(PdfViewerStringId.FormDataFileFilter),
                    RestoreDirectory = true
                };
                if (service.ShowDialog())
                {
                    string path = service.GetFullFileName();
                    bool flag = path.EndsWith("xml", true, CultureInfo.InvariantCulture);
                    if (!string.IsNullOrEmpty(path))
                    {
                        string messageHeader = string.Format(PdfViewerLocalizer.GetString(PdfViewerStringId.MessageImportError), path, flag ? "XML" : "FDF");
                        this.PerformExceptionMessageAction(delegate {
                            PdfFormData formData = new PdfFormData(path);
                            ((PdfDocumentViewModel) this.Document).ApplyFormData(formData);
                        }, messageHeader, ExceptionMessageOrigin.ImportFormData, MessageBoxImage.Exclamation, null);
                    }
                }
            }
        }

        protected override void InitializeCommands()
        {
            base.InitializeCommands();
            this.PrintDocumentCommand = DelegateCommandFactory.Create(new Action(this.Print), new Func<bool>(this.CanPrint));
            this.ShowPropertiesCommand = DelegateCommandFactory.Create(new Action(this.ShowProperties), new Func<bool>(this.CanShowProperties));
            this.SetCursorModeCommand = DelegateCommandFactory.Create<CursorModeType>(new Action<CursorModeType>(this.SetCursorMode), new Func<CursorModeType, bool>(this.CanSetCursorMode));
            this.SelectionCommand = DelegateCommandFactory.Create<PdfSelectionCommand>(new Action<PdfSelectionCommand>(this.Select), new Func<PdfSelectionCommand, bool>(this.CanSelect));
            this.OpenRecentDocumentCommand = DelegateCommandFactory.Create<object>(new Action<object>(this.OpenRecentDocument), new Func<object, bool>(this.CanOpenRecentDocument));
            this.SelectAllCommand = DelegateCommandFactory.Create(new Action(this.SelectAll), new Func<bool>(this.CanSelectAll));
            this.UnselectAllCommand = DelegateCommandFactory.Create(new Action(this.UnselectAll), new Func<bool>(this.CanUnselectAll));
            this.SaveAsCommand = DelegateCommandFactory.Create(new Action(this.SaveAs), new Func<bool>(this.CanSaveAs));
            this.CopyCommand = DelegateCommandFactory.Create(new Action(this.Copy), new Func<bool>(this.CanCopy));
            this.OpenDocumentFromWebCommand = DelegateCommandFactory.Create(new Action(this.OpenDocumentFromWeb));
            this.ImportFormDataCommand = DelegateCommandFactory.Create(new Action(this.ImportFormData), new Func<bool>(this.CanImportFormData));
            this.ExportFormDataCommand = DelegateCommandFactory.Create(new Action(this.ExportFormData), new Func<bool>(this.CanExportFormData));
            this.SetPageLayoutCommand = DelegateCommandFactory.Create<PdfPageLayout>(new Action<PdfPageLayout>(this.SetPageLayout), new Func<PdfPageLayout, bool>(this.CanSetPageLayout));
            this.ShowCoverPageCommand = DelegateCommandFactory.Create(new Action(this.ShowCoverPage), new Func<bool>(this.CanShowCoverPage));
            this.OpenAttachmentCommand = DelegateCommandFactory.Create<object>(new Action<object>(this.OpenAttachment), new Func<object, bool>(this.CanOpenAttachment));
            this.SaveAttachmentCommand = DelegateCommandFactory.Create<object>(new Action<object>(this.SaveAttachment), new Func<object, bool>(this.CanSaveAttachment));
            this.HighlightTextCommand = DelegateCommandFactory.Create(new Action(this.HighlightSelectedText));
            this.StrikethroughTextCommand = DelegateCommandFactory.Create(new Action(this.StrikethroughSelectedText));
            this.UnderlineTextCommand = DelegateCommandFactory.Create(new Action(this.UnderlineSelectedText));
            this.DeleteAnnotationCommand = DelegateCommandFactory.Create(new Action(this.DeleteAnnotation));
            this.ShowAnnotationPropertiesCommand = DelegateCommandFactory.Create(new Action(this.ShowAnnotationProperties));
            this.SetCommentCursorModeCommand = DelegateCommandFactory.Create<CursorModeType>(new Action<CursorModeType>(this.SetCommentCursorMode), new Func<CursorModeType, bool>(this.CanSetCommentCursorMode));
        }

        public virtual bool IsDocumentPositionInView(PdfDocumentPosition position)
        {
            if ((this.DocumentPresenter == null) || (position == null))
            {
                return false;
            }
            Rect rect = this.DocumentPresenter.CalcRect(position.PageIndex, position.Point, position.Point);
            return this.DocumentPresenter.IsRectangleInView(position.PageIndex, rect);
        }

        protected override void LoadDocument(object source)
        {
            base.LoadDocument(source);
            PdfDocumentViewModel document = (PdfDocumentViewModel) this.Document;
            if (!this.AsyncDocumentLoad)
            {
                if (base.ThrowOpenFileExceptionLocker.IsLocked)
                {
                    this.PerformExceptionMessageAction(() => document.LoadDocumentSync(source, this.DetachStreamOnLoadComplete), string.Format(PdfViewerLocalizer.GetString(PdfViewerStringId.MessageLoadingError), document.FilePath), ExceptionMessageOrigin.OpenFile, MessageBoxImage.Hand, new Action(this.CloseDocument));
                }
                else
                {
                    document.LoadDocumentSync(source, this.DetachStreamOnLoadComplete);
                    base.ThrowOpenFileExceptionLocker.Unlock();
                }
            }
            else
            {
                Action<ISplashScreenService> action = <>c.<>9__387_0;
                if (<>c.<>9__387_0 == null)
                {
                    Action<ISplashScreenService> local1 = <>c.<>9__387_0;
                    action = <>c.<>9__387_0 = x => x.ShowSplashScreen();
                }
                this.SplashScreenService.Do<ISplashScreenService>(action);
                this.PerformExceptionMessageAction(() => document.LoadDocument(source, this.DetachStreamOnLoadComplete), string.Format(PdfViewerLocalizer.GetString(PdfViewerStringId.MessageLoadingError), document.FilePath), ExceptionMessageOrigin.OpenFile, MessageBoxImage.Hand, new Action(this.CloseDocument));
            }
        }

        public virtual void Navigate(PdfOutlineTreeListItem item)
        {
            if (this.CanNavigate(item))
            {
                this.Document.NavigateToOutline(item);
                if (this.ActualDocumentMapSettings.ActualHideAfterUse)
                {
                    this.ActualDocumentMapSettings.OutlinesViewerState = PdfOutlinesViewerState.Collapsed;
                }
            }
        }

        protected internal virtual void Navigate(PdfTarget target)
        {
            if (this.CanNavigate(target))
            {
                this.DocumentPresenter.Do<PdfPresenterControl>(x => x.ScrollIntoView(target));
            }
        }

        protected virtual void NavigationPanelsLayoutChanged(DevExpress.Xpf.PdfViewer.NavigationPanelsLayout newValue)
        {
        }

        protected virtual void OnAnnotationDeleting(AnnotationDeletingEventArgs args)
        {
            base.RaiseEvent(args);
        }

        protected virtual void OnCursorModeChanged(CursorModeType cursorModeType)
        {
            base.RaiseEvent(new RoutedEventArgs(CursorModeChangedEvent));
        }

        protected override void OnDocumentChanged(DevExpress.Xpf.DocumentViewer.IDocument oldValue, DevExpress.Xpf.DocumentViewer.IDocument newValue)
        {
            base.OnDocumentChanged(oldValue, newValue);
            this.UpdateStartScreenVisible();
            this.ActualDocumentMapSettings.RaiseInvalidate();
            this.ActualAttachmentsViewerSettings.RaiseInvalidate();
            this.ActualThumbnailsViewerSettings.RaiseInvalidate();
            CommandManager.InvalidateRequerySuggested();
        }

        protected virtual void OnDocumentLoaded()
        {
        }

        private void OnDocumentLoadedInternal(PdfDocumentViewModel model)
        {
            Action<ISplashScreenService> action = <>c.<>9__390_0;
            if (<>c.<>9__390_0 == null)
            {
                Action<ISplashScreenService> local1 = <>c.<>9__390_0;
                action = <>c.<>9__390_0 = x => x.HideSplashScreen();
            }
            this.SplashScreenService.Do<ISplashScreenService>(action);
            IList<PdfPageViewModel> evaluator = model.With<PdfDocumentViewModel, IList<PdfPageViewModel>>(<>c.<>9__390_1 ??= x => x.Pages);
            if (<>c.<>9__390_2 == null)
            {
                IList<PdfPageViewModel> local3 = model.With<PdfDocumentViewModel, IList<PdfPageViewModel>>(<>c.<>9__390_1 ??= x => x.Pages);
                evaluator = (IList<PdfPageViewModel>) (<>c.<>9__390_2 = x => x.Count<PdfPageViewModel>());
            }
            this.PageCount = ((IList<PdfPageViewModel>) <>c.<>9__390_2).Return<IList<PdfPageViewModel>, int>(evaluator, () => base.PageCount);
            base.SetCurrentPageNumber(1);
            this.HasOutlines = this.CalcHasOutlines(model);
            this.HasAttachments = this.CalcHasAttachments(model);
            this.HasThumbnails = this.CalcHasThumbnails(model);
            this.ActualDocumentMapSettings.RaiseInvalidate();
            this.ActualAttachmentsViewerSettings.RaiseInvalidate();
            this.ActualThumbnailsViewerSettings.Initialize(this);
            this.ActualThumbnailsViewerSettings.RaiseInvalidate();
            ((PdfPropertyProvider) base.PropertyProvider).IsFormDataPageVisible = this.CanExportFormData();
            CommandManager.InvalidateRequerySuggested();
            Func<PdfDocumentViewModel, PdfDocumentState> func1 = <>c.<>9__390_4;
            if (<>c.<>9__390_4 == null)
            {
                Func<PdfDocumentViewModel, PdfDocumentState> local4 = <>c.<>9__390_4;
                func1 = <>c.<>9__390_4 = x => x.DocumentState;
            }
            model.With<PdfDocumentViewModel, PdfDocumentState>(func1).Do<PdfDocumentState>(x => x.HighlightFormFields = this.HighlightFormFields);
            Func<PdfDocumentViewModel, PdfDocumentState> func2 = <>c.<>9__390_6;
            if (<>c.<>9__390_6 == null)
            {
                Func<PdfDocumentViewModel, PdfDocumentState> local5 = <>c.<>9__390_6;
                func2 = <>c.<>9__390_6 = x => x.DocumentState;
            }
            model.With<PdfDocumentViewModel, PdfDocumentState>(func2).Do<PdfDocumentState>(x => x.HighlightedFormFieldColor = new PdfRgbaColor(((double) this.HighlightedFormFieldColor.R) / 255.0, ((double) this.HighlightedFormFieldColor.G) / 255.0, ((double) this.HighlightedFormFieldColor.B) / 255.0, ((double) this.HighlightedFormFieldColor.A) / 255.0));
            Func<PdfDocumentViewModel, PdfDocument> func3 = <>c.<>9__390_8;
            if (<>c.<>9__390_8 == null)
            {
                Func<PdfDocumentViewModel, PdfDocument> local6 = <>c.<>9__390_8;
                func3 = <>c.<>9__390_8 = x => x.PdfDocument;
            }
            Func<PdfDocument, PdfInteractiveForm> func4 = <>c.<>9__390_9;
            if (<>c.<>9__390_9 == null)
            {
                Func<PdfDocument, PdfInteractiveForm> local7 = <>c.<>9__390_9;
                func4 = <>c.<>9__390_9 = x => x.AcroForm;
            }
            model.With<PdfDocumentViewModel, PdfDocument>(func3).With<PdfDocument, PdfInteractiveForm>(func4).Do<PdfInteractiveForm>(delegate (PdfInteractiveForm x) {
                x.FormFieldValueChanging += new PdfInteractiveFormFieldValueChangingEventHandler(this.OnFormFieldValueChanging);
            });
            Func<PdfDocumentViewModel, PdfDocument> func5 = <>c.<>9__390_11;
            if (<>c.<>9__390_11 == null)
            {
                Func<PdfDocumentViewModel, PdfDocument> local8 = <>c.<>9__390_11;
                func5 = <>c.<>9__390_11 = x => x.PdfDocument;
            }
            Func<PdfDocument, PdfInteractiveForm> func6 = <>c.<>9__390_12;
            if (<>c.<>9__390_12 == null)
            {
                Func<PdfDocument, PdfInteractiveForm> local9 = <>c.<>9__390_12;
                func6 = <>c.<>9__390_12 = x => x.AcroForm;
            }
            model.With<PdfDocumentViewModel, PdfDocument>(func5).With<PdfDocument, PdfInteractiveForm>(func6).Do<PdfInteractiveForm>(delegate (PdfInteractiveForm x) {
                x.FormFieldValueChanged += new PdfInteractiveFormFieldValueChangedEventHandler(this.OnFormFieldValueChanged);
            });
            model.Do<PdfDocumentViewModel>(x => x.ImageCacheSize = this.AllowCachePages ? ((long) (this.CacheSize / 0x100000)) : ((long) 1));
            base.RaiseEvent(new RoutedEventArgs(DocumentLoadedEvent));
            this.OnDocumentLoaded();
        }

        private void OnDocumentProgressChanged(object sender, DocumentProgressChangedEventArgs args)
        {
            PdfDocumentViewModel documentViewModel = sender as PdfDocumentViewModel;
            if (documentViewModel != null)
            {
                if (documentViewModel.IsLoadingFailed && !documentViewModel.IsCancelled)
                {
                    Action<ISplashScreenService> action = <>c.<>9__389_0;
                    if (<>c.<>9__389_0 == null)
                    {
                        Action<ISplashScreenService> local1 = <>c.<>9__389_0;
                        action = <>c.<>9__389_0 = x => x.HideSplashScreen();
                    }
                    this.SplashScreenService.Do<ISplashScreenService>(action);
                    this.PerformExceptionMessageAction(delegate {
                        if (documentViewModel.LoadingException != null)
                        {
                            throw (!(documentViewModel.LoadingException is AggregateException) || (documentViewModel.LoadingException.InnerException == null)) ? ((Action) documentViewModel.LoadingException) : ((Action) documentViewModel.LoadingException.InnerException);
                        }
                    }, string.Format(PdfViewerLocalizer.GetString(PdfViewerStringId.MessageLoadingError), documentViewModel.FilePath), ExceptionMessageOrigin.OpenFile, MessageBoxImage.Hand, null);
                    this.CloseDocument();
                }
                else if (!documentViewModel.IsCancelled)
                {
                    if (args.IsCompleted)
                    {
                        this.OnDocumentLoadedInternal(documentViewModel);
                    }
                    else
                    {
                        this.SplashScreenService.Do<ISplashScreenService>(x => x.SetSplashScreenProgress((double) args.Progress, (double) args.TotalProgress));
                    }
                }
                else
                {
                    Action<ISplashScreenService> action = <>c.<>9__389_2;
                    if (<>c.<>9__389_2 == null)
                    {
                        Action<ISplashScreenService> local2 = <>c.<>9__389_2;
                        action = <>c.<>9__389_2 = x => x.HideSplashScreen();
                    }
                    this.SplashScreenService.Do<ISplashScreenService>(action);
                    this.documentClosingLocker.DoIfNotLocked(new Action(this.CloseDocument));
                }
            }
        }

        private void OnDocumentRequestPassword(object sender, RequestPasswordEventArgs args)
        {
            Action<ISplashScreenService> action = <>c.<>9__511_0;
            if (<>c.<>9__511_0 == null)
            {
                Action<ISplashScreenService> local1 = <>c.<>9__511_0;
                action = <>c.<>9__511_0 = x => x.HideSplashScreen();
            }
            this.SplashScreenService.Do<ISplashScreenService>(action);
            args.Password = this.GetPassword(args.Path, args.TryNumber);
            args.Handled = true;
            Func<PdfDocumentViewModel, bool> evaluator = <>c.<>9__511_1;
            if (<>c.<>9__511_1 == null)
            {
                Func<PdfDocumentViewModel, bool> local2 = <>c.<>9__511_1;
                evaluator = <>c.<>9__511_1 = x => !x.IsCancelled;
            }
            if ((this.Document as PdfDocumentViewModel).Return<PdfDocumentViewModel, bool>(evaluator, <>c.<>9__511_2 ??= () => false))
            {
                Action<ISplashScreenService> action2 = <>c.<>9__511_3;
                if (<>c.<>9__511_3 == null)
                {
                    Action<ISplashScreenService> local4 = <>c.<>9__511_3;
                    action2 = <>c.<>9__511_3 = x => x.ShowSplashScreen();
                }
                this.SplashScreenService.Do<ISplashScreenService>(action2);
            }
        }

        protected override void OnDocumentSourceChanged(object oldValue, object newValue)
        {
            if (!this.documentClosingLocker.IsLocked)
            {
                this.AddDocumentToRecentFiles(newValue);
                if ((oldValue == null) || this.ignoreSaveChangesLocker.IsLocked)
                {
                    base.OnDocumentSourceChanged(oldValue, newValue);
                }
                else
                {
                    DocumentClosingEventArgs args = this.RaiseDocumentClosing();
                    if (args.Cancel)
                    {
                        this.documentClosingLocker.DoLockedAction<object>(delegate {
                            object obj2;
                            this.DocumentSource = obj2 = oldValue;
                            return obj2;
                        });
                    }
                    else
                    {
                        PdfDocumentViewModel document = this.Document as PdfDocumentViewModel;
                        Func<PdfDocumentViewModel, bool> evaluator = <>c.<>9__506_1;
                        if (<>c.<>9__506_1 == null)
                        {
                            Func<PdfDocumentViewModel, bool> local1 = <>c.<>9__506_1;
                            evaluator = <>c.<>9__506_1 = x => x.IsLoaded && x.DocumentState.IsDocumentModified;
                        }
                        if (document.Return<PdfDocumentViewModel, bool>(evaluator, <>c.<>9__506_2 ??= () => false))
                        {
                            MessageBoxResult? saveDialogResult = args.SaveDialogResult;
                            if (saveDialogResult == null)
                            {
                                saveDialogResult = new MessageBoxResult?(DXMessageBoxHelper.Show(this, PdfViewerLocalizer.GetString(PdfViewerStringId.SaveChangesMessage), PdfViewerLocalizer.GetString(PdfViewerStringId.SaveChangesCaption), MessageBoxButton.YesNoCancel, MessageBoxImage.Asterisk));
                            }
                            if (((MessageBoxResult) saveDialogResult.Value) == MessageBoxResult.Yes)
                            {
                                string fileName = this.GetDocumentName(oldValue);
                                this.updateDocumentSourceLocker.DoLockedAction(() => this.SaveDocumentInternal(fileName));
                            }
                            else if (((MessageBoxResult) saveDialogResult.Value) == MessageBoxResult.Cancel)
                            {
                                this.documentClosingLocker.DoLockedAction<object>(delegate {
                                    object obj2;
                                    this.DocumentSource = obj2 = oldValue;
                                    return obj2;
                                });
                                return;
                            }
                        }
                        Func<PdfDocumentViewModel, bool> func2 = <>c.<>9__506_5;
                        if (<>c.<>9__506_5 == null)
                        {
                            Func<PdfDocumentViewModel, bool> local3 = <>c.<>9__506_5;
                            func2 = <>c.<>9__506_5 = x => x.IsLoaded || x.IsLoadingFailed;
                        }
                        if (!document.Return<PdfDocumentViewModel, bool>(func2, (<>c.<>9__506_6 ??= () => true)))
                        {
                            this.documentClosingLocker.DoLockedAction(new Action(document.CancelLoadDocument));
                        }
                        base.OnDocumentSourceChanged(oldValue, newValue);
                    }
                }
            }
        }

        private void OnFormFieldValueChanged(object sender, PdfInteractiveFormFieldValueChangedEventArgs e)
        {
            FormFieldValueChangedEventArgs args = new FormFieldValueChangedEventArgs(e.FieldName, e.OldValue, e.NewValue);
            base.RaiseEvent(args);
        }

        private void OnFormFieldValueChanging(object sender, PdfInteractiveFormFieldValueChangingEventArgs e)
        {
            FormFieldValueChangingEventArgs args = new FormFieldValueChangingEventArgs(e.FieldName, e.OldValue, e.NewValue);
            base.RaiseEvent(args);
            e.Cancel = args.Cancel;
            e.NewValue = args.NewValue;
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            this.UpdateStartScreenVisible();
            this.RecentFiles ??= new ObservableCollection<RecentFileViewModel>();
        }

        protected override void OnLoaded(object sender, RoutedEventArgs e)
        {
            base.OnLoaded(sender, e);
            this.InteractionProvider.OnLoaded();
            ThemeManager.AddThemeChangedHandler(this, new ThemeChangedRoutedEventHandler(this.OnThemeChanged));
        }

        protected virtual void OnPageLayoutChanged(PdfPageLayout newValue)
        {
            this.AssignDocumentPresenterProperties();
            base.RaiseEvent(new RoutedEventArgs(PageLayoutChangedEvent));
        }

        protected override void OnPageRotationChanged(Rotation oldValue, Rotation newValue)
        {
            base.OnPageRotationChanged(oldValue, newValue);
            this.Document.Do<IPdfDocument>(x => x.UpdateDocumentRotateAngle((double) (((Rotation) 90) * base.PageRotation)));
        }

        protected virtual void OnPopupMenuShowing(PopupMenuShowingEventArgs args)
        {
            base.RaiseEvent(args);
        }

        protected virtual void OnShowStartScreenChanged(bool? newValue)
        {
            this.UpdateStartScreenVisible();
        }

        protected virtual void OnTextMarkupAnnotationCreating(PdfTextMarkupAnnotationCreatingEventArgs args)
        {
            base.RaiseEvent(args);
        }

        private void OnThemeChanged(object sender, EventArgs e)
        {
            base.CurrentPageNumber = 1;
        }

        protected override void OnUnloaded(object sender, RoutedEventArgs e)
        {
            base.OnUnloaded(sender, e);
            this.InteractionProvider.OnUnloaded();
            ThemeManager.RemoveThemeChangedHandler(this, new ThemeChangedRoutedEventHandler(this.OnThemeChanged));
        }

        protected override void OnZoomFactorChanged(double oldValue, double newValue)
        {
            base.OnZoomFactorChanged(oldValue, newValue);
            Action<PdfPresenterControl> action = <>c.<>9__503_0;
            if (<>c.<>9__503_0 == null)
            {
                Action<PdfPresenterControl> local1 = <>c.<>9__503_0;
                action = <>c.<>9__503_0 = x => x.Update();
            }
            this.DocumentPresenter.Do<PdfPresenterControl>(action);
        }

        protected virtual void OpenAttachment(object parameter)
        {
            PdfFileAttachmentListItem fileAttachment = parameter as PdfFileAttachmentListItem;
            Func<PdfDocumentViewModel, PdfDocumentStateController> evaluator = <>c.<>9__379_0;
            if (<>c.<>9__379_0 == null)
            {
                Func<PdfDocumentViewModel, PdfDocumentStateController> local1 = <>c.<>9__379_0;
                evaluator = <>c.<>9__379_0 = x => x.DocumentStateController;
            }
            (this.Document as PdfDocumentViewModel).With<PdfDocumentViewModel, PdfDocumentStateController>(evaluator).Do<PdfDocumentStateController>(delegate (PdfDocumentStateController x) {
                Func<PdfFileAttachmentListItem, PdfFileAttachment> func1 = <>c.<>9__379_2;
                if (<>c.<>9__379_2 == null)
                {
                    Func<PdfFileAttachmentListItem, PdfFileAttachment> local1 = <>c.<>9__379_2;
                    func1 = <>c.<>9__379_2 = y => y.FileAttachment;
                }
                x.OpenFileAttachment(fileAttachment.With<PdfFileAttachmentListItem, PdfFileAttachment>(func1));
            });
        }

        protected internal virtual bool OpenAttachmentInternal(PdfFileAttachment attachment)
        {
            AttachmentOpeningEventArgs e = new AttachmentOpeningEventArgs(attachment);
            base.RaiseEvent(e);
            if (e.Handled)
            {
                return !e.Cancel;
            }
            if (this.ActualAttachmentsViewerSettings.ActualHideAfterUse)
            {
                this.ActualAttachmentsViewerSettings.AttachmentsViewerState = PdfAttachmentsViewerState.Collapsed;
            }
            string getString = string.Format(PdfViewerLocalizer.GetString(PdfViewerStringId.MessageFileAttachmentOpening), attachment.FileName);
            return (DXMessageBoxHelper.Show(this, getString, PdfViewerLocalizer.GetString(PdfViewerStringId.MessageSecurityWarningCaption), MessageBoxButton.YesNo, MessageBoxImage.Asterisk) == MessageBoxResult.Yes);
        }

        public override void OpenDocument(string filePath = null)
        {
            Action<DevExpress.Xpf.PdfViewer.Internal.InteractionProvider> action = <>c.<>9__422_0;
            if (<>c.<>9__422_0 == null)
            {
                Action<DevExpress.Xpf.PdfViewer.Internal.InteractionProvider> local1 = <>c.<>9__422_0;
                action = <>c.<>9__422_0 = x => x.CommitEditor();
            }
            this.InteractionProvider.Do<DevExpress.Xpf.PdfViewer.Internal.InteractionProvider>(action);
            base.OpenDocument(filePath);
        }

        protected virtual void OpenDocumentFromWeb()
        {
            MessageBoxResult? defaultButton = null;
            defaultButton = null;
            DXDialogWindow window = new DXDialogWindow(PdfViewerLocalizer.GetString(PdfViewerStringId.OpenDocumentFromWebCaption), MessageBoxButton.OKCancel, defaultButton, defaultButton) {
                SizeToContent = SizeToContent.WidthAndHeight,
                Owner = LayoutHelper.GetTopLevelVisual(this) as Window,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                ResizeMode = ResizeMode.NoResize,
                ShowIcon = false
            };
            AddressControl control = new AddressControl();
            AddressViewModel model = new AddressViewModel();
            control.DataContext = model;
            window.Content = control;
            ThemeManager.SetThemeName(window, ThemeHelper.GetEditorThemeName(this));
            bool? nullable3 = window.ShowDialog();
            bool flag = true;
            if ((nullable3.GetValueOrDefault() == flag) ? (nullable3 != null) : false)
            {
                Uri uri;
                if (Uri.TryCreate(model.Url, UriKind.Absolute, out uri))
                {
                    base.DocumentSource = uri;
                }
                else
                {
                    DXMessageBoxHelper.Show(this, string.Format(PdfViewerLocalizer.GetString(PdfViewerStringId.MessageIncorrectUrl), model.Url), PdfViewerLocalizer.GetString(PdfViewerStringId.MessageErrorCaption), MessageBoxButton.OK, MessageBoxImage.Hand);
                }
            }
        }

        private void OpenRecentDocument(object document)
        {
            base.DocumentSource = document;
        }

        protected virtual void OutlinesViewerSettingsChanged(PdfOutlinesViewerSettings oldValue, PdfOutlinesViewerSettings newValue)
        {
            PdfOutlinesViewerSettings settings1 = newValue;
            if (newValue == null)
            {
                PdfOutlinesViewerSettings local1 = newValue;
                settings1 = (PdfOutlinesViewerSettings) this.CreateDefaultDocumentMapSettings();
            }
            this.ActualDocumentMapSettings = settings1;
        }

        internal void PerformExceptionMessageAction(Action action, string messageHeader, ExceptionMessageOrigin messageOrigin, MessageBoxImage messageImage = 0x10, Action fallbackAction = null)
        {
            try
            {
                action();
            }
            catch (Exception exception)
            {
                Exception innerException = exception;
                if ((exception.InnerException != null) && ((exception is TargetInvocationException) || (exception is AggregateException)))
                {
                    innerException = exception.InnerException;
                }
                string message = innerException.Message;
                ExceptionMessageEventArgs args = this.RaiseExceptionMessage(messageOrigin, innerException, $"{messageHeader}
{message}", ExceptionMessageAction.ShowMessage, messageImage);
                Action<Action> action1 = <>c.<>9__548_0;
                if (<>c.<>9__548_0 == null)
                {
                    Action<Action> local1 = <>c.<>9__548_0;
                    action1 = <>c.<>9__548_0 = x => x();
                }
                fallbackAction.Do<Action>(action1);
                if (args.MessageAction == ExceptionMessageAction.ShowMessage)
                {
                    DXMessageBoxHelper.Show(this, args.Message, PdfViewerLocalizer.GetString(PdfViewerStringId.MessageErrorCaption), MessageBoxButton.OK, args.MessageImage);
                }
                if (args.MessageAction == ExceptionMessageAction.Rethrow)
                {
                    throw innerException;
                }
            }
        }

        public virtual void Print()
        {
            this.CheckOperationAvailability();
            this.PrintInternal(null);
        }

        protected internal void Print(IEnumerable<int> pages)
        {
            this.PrintInternal(pages);
        }

        public virtual void Print(PdfPrinterSettings printerSettings, bool showPrintStatus = true)
        {
            printerSettings ??= new PdfPrinterSettings();
            if (this.CanPrint())
            {
                this.Document.Print(printerSettings, base.CurrentPageNumber, showPrintStatus);
            }
        }

        [Obsolete("Use the Print(PdfPrinterSettings printerSettings, bool showPrintStatus = true) overload of this method instead.")]
        public virtual void Print(PdfPrinterSettings printerSettings, bool showPrintStatus, int maxPrintingDpi)
        {
            this.Print(printerSettings, showPrintStatus);
        }

        [Obsolete("Use the Print(PdfPrinterSettings printerSettings, bool showPrintStatus = true) overload of this method instead.")]
        public virtual void Print(PrinterSettings printerSettings, bool showPrintStatus = true, int maxPrintingDpi = 0)
        {
            this.Print(new PdfPrinterSettings(printerSettings), showPrintStatus);
        }

        private void PrintInternal(IEnumerable<int> pages = null)
        {
            if (this.CanPrint())
            {
                this.InteractionProvider.CloseEditor();
                string title = PdfViewerLocalizer.GetString(PdfViewerStringId.PrintDialogTitle);
                PdfDocumentViewModel documentViewModel = (PdfDocumentViewModel) this.Document;
                Func<PdfPrintDialogViewModel, bool> canExecuteMethod = <>c.<>9__461_1;
                if (<>c.<>9__461_1 == null)
                {
                    Func<PdfPrintDialogViewModel, bool> local1 = <>c.<>9__461_1;
                    canExecuteMethod = <>c.<>9__461_1 = viewModel => viewModel.EnableToPrint;
                }
                bool? useCommandManager = null;
                DelegateCommand<PdfPrintDialogViewModel> printCommand = new DelegateCommand<PdfPrintDialogViewModel>(viewModel => documentViewModel.Print(viewModel.PrinterSettings, this.CurrentPageNumber, this.ShowPrintStatusDialog), canExecuteMethod, useCommandManager);
                DialogService local2 = TemplateHelper.LoadFromTemplate<DialogService>(this.PrintPreviewDialogTemplate);
                DialogService local4 = local2;
                if (local2 == null)
                {
                    DialogService local3 = local2;
                    local4 = this.CreateDefaultPrintPreviewDialogService();
                }
                DialogService service = local4;
                PageSetupDialogShowingEventArgs args1 = new PageSetupDialogShowingEventArgs();
                args1.WindowStartupLocation = service.DialogWindowStartupLocation;
                PageSetupDialogShowingEventArgs args = args1;
                base.RaiseEvent(args);
                service.DialogWindowStartupLocation = args.WindowStartupLocation;
                AssignableServiceHelper2<PdfViewerControl, DialogService>.DoServiceAction(this, service, delegate (DialogService x) {
                    Func<object> <>9__4;
                    Func<object, object> <>9__3;
                    Func<object, object> convertViewModel = <>9__3;
                    if (<>9__3 == null)
                    {
                        Func<object, object> local1 = <>9__3;
                        convertViewModel = <>9__3 = vm => new PrintDialogViewModel(this.CreatePrintViewModel(documentViewModel, title, this.UpdatePrinterSettings((PdfPrinterSettings) vm, pages)));
                    }
                    Func<object> getDefaultContent = <>9__4;
                    if (<>9__4 == null)
                    {
                        Func<object> local2 = <>9__4;
                        getDefaultContent = <>9__4 = () => args.PrinterSettings;
                    }
                    x.ShowDialog(printCommand, PdfViewerLocalizer.GetString(PdfViewerStringId.PrintDialogPrintButtonCaption), null, PdfViewerLocalizer.GetString(PdfViewerStringId.CancelButtonCaption), title, convertViewModel, getDefaultContent);
                });
            }
        }

        protected void RaiseAnnotationChanged()
        {
            Func<PdfDocumentViewModel, PdfDocumentStateController> evaluator = <>c.<>9__545_0;
            if (<>c.<>9__545_0 == null)
            {
                Func<PdfDocumentViewModel, PdfDocumentStateController> local1 = <>c.<>9__545_0;
                evaluator = <>c.<>9__545_0 = x => x.DocumentStateController;
            }
            Func<PdfDocumentStateController, PdfTextMarkupAnnotationState> func2 = <>c.<>9__545_1;
            if (<>c.<>9__545_1 == null)
            {
                Func<PdfDocumentStateController, PdfTextMarkupAnnotationState> local2 = <>c.<>9__545_1;
                func2 = <>c.<>9__545_1 = x => x.GetSelectedAnnotationState();
            }
            PdfTextMarkupAnnotationChangedEventArgs e = new PdfTextMarkupAnnotationChangedEventArgs((this.Document as PdfDocumentViewModel).With<PdfDocumentViewModel, PdfDocumentStateController>(evaluator).Return<PdfDocumentStateController, PdfTextMarkupAnnotationState>(func2, <>c.<>9__545_2 ??= ((Func<PdfTextMarkupAnnotationState>) (() => null))));
            base.RaiseEvent(e);
        }

        protected internal bool RaiseAnnotationDeletingEvent(int pageNumber, PdfRectangle rect, string name)
        {
            AnnotationDeletingEventArgs args = new AnnotationDeletingEventArgs(pageNumber, rect, name);
            this.OnAnnotationDeleting(args);
            return args.Cancel;
        }

        private DocumentClosingEventArgs RaiseDocumentClosing()
        {
            DocumentClosingEventArgs e = new DocumentClosingEventArgs(DocumentClosingEvent);
            base.RaiseEvent(e);
            return e;
        }

        protected virtual ExceptionMessageEventArgs RaiseExceptionMessage(ExceptionMessageOrigin messageOrigin, Exception exception, string message, ExceptionMessageAction messageAction, MessageBoxImage messageImage)
        {
            ExceptionMessageEventArgs e = new ExceptionMessageEventArgs(messageOrigin, exception, message, messageAction, messageImage);
            base.RaiseEvent(e);
            return e;
        }

        protected internal void RaiseHiddenEditorEvent(string fieldName)
        {
            base.RaiseEvent(new PdfEditorEventArgs(HiddenEditorEvent, fieldName));
        }

        protected internal virtual void RaiseMarkupAnnotationGotFocusEvent(PdfMarkupAnnotationInfo info)
        {
            base.RaiseEvent(new MarkupAnnotationGotFocusEventArgs(info));
        }

        protected internal virtual void RaiseMarkupAnnotationLostFocusEvent(PdfMarkupAnnotationInfo info)
        {
            base.RaiseEvent(new MarkupAnnotationLostFocusEventArgs(info));
        }

        protected internal PopupMenuShowingEventArgs RaisePopupMenuShowingEvent()
        {
            PopupMenuShowingEventArgs args = new PopupMenuShowingEventArgs();
            this.OnPopupMenuShowing(args);
            return args;
        }

        protected internal void RaisePrintPage(DevExpress.Pdf.PdfPrintPageEventArgs args)
        {
            DevExpress.Xpf.PdfViewer.PdfPrintPageEventArgs e = new DevExpress.Xpf.PdfViewer.PdfPrintPageEventArgs(args);
            base.RaiseEvent(e);
            args.Cancel = e.Cancel;
            args.HasMorePages = e.HasMorePages;
        }

        protected internal void RaiseQueryPageSettings(DevExpress.Pdf.PdfQueryPageSettingsEventArgs args)
        {
            DevExpress.Xpf.PdfViewer.PdfQueryPageSettingsEventArgs e = new DevExpress.Xpf.PdfViewer.PdfQueryPageSettingsEventArgs(args);
            base.RaiseEvent(e);
            args.Cancel = e.Cancel;
            args.PrintInGrayscale = e.PrintInGrayscale;
            args.PageSettings = e.PageSettings;
        }

        protected internal bool RaiseRequestOpeningReferencedDocumentSource(string documentPath, bool openInNewWindow, PdfTarget target)
        {
            ReferencedDocumentOpeningEventArgs e = new ReferencedDocumentOpeningEventArgs(documentPath, openInNewWindow);
            base.RaiseEvent(e);
            return (!e.Handled || !e.Cancel);
        }

        protected internal bool RaiseShowingEditorEvent(string fieldName)
        {
            PdfShowingEditorEventArgs e = new PdfShowingEditorEventArgs(fieldName);
            base.RaiseEvent(e);
            return !e.Cancel;
        }

        protected internal void RaiseShownEditorEvent(string fieldName)
        {
            base.RaiseEvent(new PdfEditorEventArgs(ShownEditorEvent, fieldName));
        }

        protected internal virtual void RaiseTextMarkupAnnotationCreatedEvent(string annotationName)
        {
            base.RaiseEvent(new PdfTextMarkupAnnotationCreatedEventArgs(annotationName));
        }

        protected internal bool RaiseTextMarkupAnnotationCreatingEvent(string selectedText, PdfViewerTextMarkupAnnotationBuilder builder)
        {
            PdfTextMarkupAnnotationCreatingEventArgs args = new PdfTextMarkupAnnotationCreatingEventArgs(selectedText, builder);
            this.OnTextMarkupAnnotationCreating(args);
            return !args.Cancel;
        }

        protected override void ReleaseDocument(DevExpress.Xpf.DocumentViewer.IDocument document)
        {
            (document as PdfDocumentViewModel).Do<PdfDocumentViewModel>(delegate (PdfDocumentViewModel x) {
                x.RequestPassword -= new EventHandler<RequestPasswordEventArgs>(this.OnDocumentRequestPassword);
            });
            (document as PdfDocumentViewModel).Do<PdfDocumentViewModel>(delegate (PdfDocumentViewModel x) {
                x.DocumentProgressChanged -= new EventHandler<DocumentProgressChangedEventArgs>(this.OnDocumentProgressChanged);
            });
            Func<PdfDocumentViewModel, PdfDocument> evaluator = <>c.<>9__505_2;
            if (<>c.<>9__505_2 == null)
            {
                Func<PdfDocumentViewModel, PdfDocument> local1 = <>c.<>9__505_2;
                evaluator = <>c.<>9__505_2 = x => x.PdfDocument;
            }
            Func<PdfDocument, PdfInteractiveForm> func2 = <>c.<>9__505_3;
            if (<>c.<>9__505_3 == null)
            {
                Func<PdfDocument, PdfInteractiveForm> local2 = <>c.<>9__505_3;
                func2 = <>c.<>9__505_3 = x => x.AcroForm;
            }
            (document as PdfDocumentViewModel).With<PdfDocumentViewModel, PdfDocument>(evaluator).With<PdfDocument, PdfInteractiveForm>(func2).Do<PdfInteractiveForm>(delegate (PdfInteractiveForm x) {
                x.FormFieldValueChanging -= new PdfInteractiveFormFieldValueChangingEventHandler(this.OnFormFieldValueChanging);
            });
            Func<PdfDocumentViewModel, PdfDocument> func3 = <>c.<>9__505_5;
            if (<>c.<>9__505_5 == null)
            {
                Func<PdfDocumentViewModel, PdfDocument> local3 = <>c.<>9__505_5;
                func3 = <>c.<>9__505_5 = x => x.PdfDocument;
            }
            Func<PdfDocument, PdfInteractiveForm> func4 = <>c.<>9__505_6;
            if (<>c.<>9__505_6 == null)
            {
                Func<PdfDocument, PdfInteractiveForm> local4 = <>c.<>9__505_6;
                func4 = <>c.<>9__505_6 = x => x.AcroForm;
            }
            (document as PdfDocumentViewModel).With<PdfDocumentViewModel, PdfDocument>(func3).With<PdfDocument, PdfInteractiveForm>(func4).Do<PdfInteractiveForm>(delegate (PdfInteractiveForm x) {
                x.FormFieldValueChanged -= new PdfInteractiveFormFieldValueChangedEventHandler(this.OnFormFieldValueChanged);
            });
            Action<IDisposable> action = <>c.<>9__505_8;
            if (<>c.<>9__505_8 == null)
            {
                Action<IDisposable> local5 = <>c.<>9__505_8;
                action = <>c.<>9__505_8 = x => x.Dispose();
            }
            (document as IDisposable).Do<IDisposable>(action);
        }

        protected virtual void SaveAs()
        {
            string fileName = this.GetDocumentName(base.DocumentSource);
            this.PerformExceptionMessageAction(delegate {
                Action <>9__1;
                Action action = <>9__1;
                if (<>9__1 == null)
                {
                    Action local1 = <>9__1;
                    action = <>9__1 = () => this.SaveDocumentInternal(fileName);
                }
                this.ignoreSaveChangesLocker.DoLockedAction(action);
            }, string.Format(PdfViewerLocalizer.GetString(PdfViewerStringId.MessageSaveAsError), fileName), ExceptionMessageOrigin.SaveFile, MessageBoxImage.Hand, null);
        }

        protected virtual void SaveAttachment(object parameter)
        {
            PdfFileAttachmentListItem fileAttachment = parameter as PdfFileAttachmentListItem;
            Func<PdfDocumentViewModel, PdfDocumentStateController> evaluator = <>c.<>9__381_0;
            if (<>c.<>9__381_0 == null)
            {
                Func<PdfDocumentViewModel, PdfDocumentStateController> local1 = <>c.<>9__381_0;
                evaluator = <>c.<>9__381_0 = x => x.DocumentStateController;
            }
            (this.Document as PdfDocumentViewModel).With<PdfDocumentViewModel, PdfDocumentStateController>(evaluator).Do<PdfDocumentStateController>(delegate (PdfDocumentStateController x) {
                Func<PdfFileAttachmentListItem, PdfFileAttachment> func1 = <>c.<>9__381_2;
                if (<>c.<>9__381_2 == null)
                {
                    Func<PdfFileAttachmentListItem, PdfFileAttachment> local1 = <>c.<>9__381_2;
                    func1 = <>c.<>9__381_2 = y => y.FileAttachment;
                }
                x.SaveFileAttachment(fileAttachment.With<PdfFileAttachmentListItem, PdfFileAttachment>(func1));
            });
        }

        protected internal virtual string SaveAttachmentInternal(PdfFileAttachment attachment)
        {
            if (this.ActualAttachmentsViewerSettings.ActualHideAfterUse)
            {
                this.ActualAttachmentsViewerSettings.AttachmentsViewerState = PdfAttachmentsViewerState.Collapsed;
            }
            ISaveFileDialogService service = new SaveFileDialogService {
                DefaultFileName = attachment.FileName
            };
            return (!service.ShowDialog(null, null) ? null : service.GetFullFileName());
        }

        public void SaveDocument(Stream stream)
        {
            PdfSaveOptions options = new PdfSaveOptions();
            this.SaveDocument(stream, options);
        }

        public void SaveDocument(string path)
        {
            PdfSaveOptions options = new PdfSaveOptions();
            this.SaveDocument(path, options);
        }

        private bool SaveDocument(Stream stream, PdfSaveOptions options)
        {
            <>c__DisplayClass414_0 class_;
            this.CheckOperationAvailability();
            this.SaveDocumentAndUpdateProperties(delegate {
                ((PdfDocumentViewModel) this.Document).SaveDocument(stream, this.DetachStreamOnLoadComplete, options, new Action<int>(class_.UpdateProgress));
            });
            base.DocumentSource = stream;
            return true;
        }

        private bool SaveDocument(string path, PdfSaveOptions options)
        {
            <>c__DisplayClass415_0 class_;
            this.CheckOperationAvailability();
            if (string.IsNullOrEmpty(path))
            {
                return false;
            }
            this.SaveDocumentAndUpdateProperties(delegate {
                ((PdfDocumentViewModel) this.Document).SaveDocument(path, this.DetachStreamOnLoadComplete, options, new Action<int>(class_.UpdateProgress));
            });
            if (!this.updateDocumentSourceLocker.IsLocked)
            {
                base.DocumentSource = path;
            }
            return true;
        }

        private void SaveDocumentAndUpdateProperties(Action saveAction)
        {
            this.InteractionProvider.CommitEditor();
            PdfDocument pdfDocument = (this.Document as PdfDocumentViewModel).PdfDocument;
            pdfDocument.Creator = this.DocumentCreator;
            pdfDocument.Producer = this.DocumentProducer;
            this.DoWithDocumentProgress(saveAction);
        }

        private void SaveDocumentInternal(string fileName)
        {
            AssignableServiceHelper2<PdfViewerControl, SaveFileDialogService>.DoServiceAction(this, this.SaveFileDialogTemplate.Return<DataTemplate, SaveFileDialogService>(new Func<DataTemplate, SaveFileDialogService>(TemplateHelper.LoadFromTemplate<SaveFileDialogService>), new Func<SaveFileDialogService>(this.CreateDefaultSaveFileDialogService)), delegate (SaveFileDialogService service) {
                ISaveFileDialogService input = service;
                Func<SaveFileDialogService, bool> evaluator = <>c.<>9__413_1;
                if (<>c.<>9__413_1 == null)
                {
                    Func<SaveFileDialogService, bool> local1 = <>c.<>9__413_1;
                    evaluator = <>c.<>9__413_1 = x => !x.IsPropertySet(FileDialogServiceBase.DefaultFileNameProperty);
                }
                if ((input as SaveFileDialogService).Return<SaveFileDialogService, bool>(evaluator, <>c.<>9__413_2 ??= () => false))
                {
                    Action<ISaveFileDialogService> <>9__3;
                    Action<ISaveFileDialogService> action = <>9__3;
                    if (<>9__3 == null)
                    {
                        Action<ISaveFileDialogService> local3 = <>9__3;
                        action = <>9__3 = x => x.DefaultFileName = fileName;
                    }
                    input.Do<ISaveFileDialogService>(action);
                }
                Func<ISaveFileDialogService, bool> func2 = <>c.<>9__413_4;
                if (<>c.<>9__413_4 == null)
                {
                    Func<ISaveFileDialogService, bool> local4 = <>c.<>9__413_4;
                    func2 = <>c.<>9__413_4 = x => x.ShowDialog(null, null);
                }
                if (input.Return<ISaveFileDialogService, bool>(func2, <>c.<>9__413_5 ??= () => false))
                {
                    IFileInfo file = input.File;
                    string path = Path.Combine(file.DirectoryName, file.Name);
                    this.SaveDocument(path);
                }
            });
        }

        public virtual void ScrollIntoView(PdfDocumentArea area, ScrollIntoViewMode scrollMode)
        {
            if (this.CanNavigate(area))
            {
                this.DocumentPresenter.Do<PdfPresenterControl>(x => this.ScrollIntoView(area.PageIndex, area.Area.TopLeft, area.Area.BottomRight, scrollMode));
            }
        }

        public virtual void ScrollIntoView(PdfDocumentPosition position, ScrollIntoViewMode scrollMode)
        {
            if (this.CanNavigate(position))
            {
                this.DocumentPresenter.Do<PdfPresenterControl>(x => this.ScrollIntoView(position.PageIndex, position.Point, position.Point, scrollMode));
            }
        }

        protected virtual void ScrollIntoView(int pageIndex, PdfPoint topLeft, PdfPoint bottonRight, ScrollIntoViewMode scrollMode)
        {
            if (this.DocumentPresenter != null)
            {
                Rect rect = this.DocumentPresenter.CalcRect(pageIndex, topLeft, bottonRight);
                this.DocumentPresenter.ScrollIntoView(pageIndex, rect, scrollMode);
            }
        }

        public void Select(PdfDocumentArea area)
        {
            this.CheckOperationAvailability();
            PdfDocumentSelectionParameter selectionParameter = new PdfDocumentSelectionParameter();
            selectionParameter.Area = area;
            selectionParameter.SelectionAction = PdfSelectionAction.SelectViaArea;
            this.Document.PerformSelection(selectionParameter);
        }

        private void Select(PdfSelectionCommand command)
        {
            switch (command)
            {
                case PdfSelectionCommand.SelectUp:
                {
                    PdfDocumentSelectionParameter selectionParameter = new PdfDocumentSelectionParameter();
                    selectionParameter.SelectionAction = PdfSelectionAction.SelectUp;
                    this.Document.PerformSelection(selectionParameter);
                    return;
                }
                case PdfSelectionCommand.SelectDown:
                {
                    PdfDocumentSelectionParameter selectionParameter = new PdfDocumentSelectionParameter();
                    selectionParameter.SelectionAction = PdfSelectionAction.SelectDown;
                    this.Document.PerformSelection(selectionParameter);
                    return;
                }
                case PdfSelectionCommand.SelectLeft:
                {
                    PdfDocumentSelectionParameter selectionParameter = new PdfDocumentSelectionParameter();
                    selectionParameter.SelectionAction = PdfSelectionAction.SelectLeft;
                    this.Document.PerformSelection(selectionParameter);
                    return;
                }
                case PdfSelectionCommand.SelectRight:
                {
                    PdfDocumentSelectionParameter selectionParameter = new PdfDocumentSelectionParameter();
                    selectionParameter.SelectionAction = PdfSelectionAction.SelectRight;
                    this.Document.PerformSelection(selectionParameter);
                    return;
                }
                case PdfSelectionCommand.SelectLineStart:
                {
                    PdfDocumentSelectionParameter selectionParameter = new PdfDocumentSelectionParameter();
                    selectionParameter.SelectionAction = PdfSelectionAction.SelectLineStart;
                    this.Document.PerformSelection(selectionParameter);
                    return;
                }
                case PdfSelectionCommand.SelectLineEnd:
                {
                    PdfDocumentSelectionParameter selectionParameter = new PdfDocumentSelectionParameter();
                    selectionParameter.SelectionAction = PdfSelectionAction.SelectLineEnd;
                    this.Document.PerformSelection(selectionParameter);
                    return;
                }
                case PdfSelectionCommand.SelectDocumentStart:
                {
                    PdfDocumentSelectionParameter selectionParameter = new PdfDocumentSelectionParameter();
                    selectionParameter.SelectionAction = PdfSelectionAction.SelectDocumentStart;
                    this.Document.PerformSelection(selectionParameter);
                    return;
                }
                case PdfSelectionCommand.SelectDocumentEnd:
                {
                    PdfDocumentSelectionParameter selectionParameter = new PdfDocumentSelectionParameter();
                    selectionParameter.SelectionAction = PdfSelectionAction.SelectDocumentEnd;
                    this.Document.PerformSelection(selectionParameter);
                    return;
                }
                case PdfSelectionCommand.SelectNextWord:
                {
                    PdfDocumentSelectionParameter selectionParameter = new PdfDocumentSelectionParameter();
                    selectionParameter.SelectionAction = PdfSelectionAction.SelectNextWord;
                    this.Document.PerformSelection(selectionParameter);
                    return;
                }
                case PdfSelectionCommand.SelectPreviousWord:
                {
                    PdfDocumentSelectionParameter selectionParameter = new PdfDocumentSelectionParameter();
                    selectionParameter.SelectionAction = PdfSelectionAction.SelectPreviousWord;
                    this.Document.PerformSelection(selectionParameter);
                    return;
                }
                case PdfSelectionCommand.MoveUp:
                {
                    PdfDocumentSelectionParameter selectionParameter = new PdfDocumentSelectionParameter();
                    selectionParameter.SelectionAction = PdfSelectionAction.MoveUp;
                    this.Document.PerformSelection(selectionParameter);
                    return;
                }
                case PdfSelectionCommand.MoveDown:
                {
                    PdfDocumentSelectionParameter selectionParameter = new PdfDocumentSelectionParameter();
                    selectionParameter.SelectionAction = PdfSelectionAction.MoveDown;
                    this.Document.PerformSelection(selectionParameter);
                    return;
                }
                case PdfSelectionCommand.MoveLeft:
                {
                    PdfDocumentSelectionParameter selectionParameter = new PdfDocumentSelectionParameter();
                    selectionParameter.SelectionAction = PdfSelectionAction.MoveLeft;
                    this.Document.PerformSelection(selectionParameter);
                    return;
                }
                case PdfSelectionCommand.MoveRight:
                {
                    PdfDocumentSelectionParameter selectionParameter = new PdfDocumentSelectionParameter();
                    selectionParameter.SelectionAction = PdfSelectionAction.MoveRight;
                    this.Document.PerformSelection(selectionParameter);
                    return;
                }
                case PdfSelectionCommand.MoveLineStart:
                {
                    PdfDocumentSelectionParameter selectionParameter = new PdfDocumentSelectionParameter();
                    selectionParameter.SelectionAction = PdfSelectionAction.MoveLineStart;
                    this.Document.PerformSelection(selectionParameter);
                    return;
                }
                case PdfSelectionCommand.MoveLineEnd:
                {
                    PdfDocumentSelectionParameter selectionParameter = new PdfDocumentSelectionParameter();
                    selectionParameter.SelectionAction = PdfSelectionAction.MoveLineEnd;
                    this.Document.PerformSelection(selectionParameter);
                    return;
                }
                case PdfSelectionCommand.MoveDocumentStart:
                {
                    PdfDocumentSelectionParameter selectionParameter = new PdfDocumentSelectionParameter();
                    selectionParameter.SelectionAction = PdfSelectionAction.MoveDocumentStart;
                    this.Document.PerformSelection(selectionParameter);
                    return;
                }
                case PdfSelectionCommand.MoveDocumentEnd:
                {
                    PdfDocumentSelectionParameter selectionParameter = new PdfDocumentSelectionParameter();
                    selectionParameter.SelectionAction = PdfSelectionAction.MoveDocumentEnd;
                    this.Document.PerformSelection(selectionParameter);
                    return;
                }
                case PdfSelectionCommand.MoveNextWord:
                {
                    PdfDocumentSelectionParameter selectionParameter = new PdfDocumentSelectionParameter();
                    selectionParameter.SelectionAction = PdfSelectionAction.MoveNextWord;
                    this.Document.PerformSelection(selectionParameter);
                    return;
                }
                case PdfSelectionCommand.MovePreviousWord:
                {
                    PdfDocumentSelectionParameter selectionParameter = new PdfDocumentSelectionParameter();
                    selectionParameter.SelectionAction = PdfSelectionAction.MovePreviousWord;
                    this.Document.PerformSelection(selectionParameter);
                    return;
                }
            }
            throw new NotImplementedException();
        }

        public void Select(PdfDocumentPosition startPosition, PdfDocumentPosition endPosition)
        {
            this.CheckOperationAvailability();
            PdfDocumentSelectionParameter selectionParameter = new PdfDocumentSelectionParameter();
            selectionParameter.Position = startPosition;
            selectionParameter.EndPosition = endPosition;
            selectionParameter.SelectionAction = PdfSelectionAction.SelectText;
            this.Document.PerformSelection(selectionParameter);
        }

        public void SelectAll()
        {
            this.CheckOperationAvailability();
            PdfDocumentSelectionParameter selectionParameter = new PdfDocumentSelectionParameter();
            selectionParameter.SelectionAction = PdfSelectionAction.SelectAllText;
            this.Document.PerformSelection(selectionParameter);
        }

        public void SelectAnnotation(int pageNumber, string annotationName)
        {
            this.CheckOperationAvailability();
            if ((pageNumber < 1) || ((pageNumber > base.PageCount) || (annotationName == null)))
            {
                throw new InvalidOperationException(PdfCoreLocalizer.GetString(PdfCoreStringId.MsgUnavailableOperation));
            }
            Func<PdfDocumentViewModel, PdfDocumentStateController> evaluator = <>c.<>9__482_0;
            if (<>c.<>9__482_0 == null)
            {
                Func<PdfDocumentViewModel, PdfDocumentStateController> local1 = <>c.<>9__482_0;
                evaluator = <>c.<>9__482_0 = x => x.DocumentStateController;
            }
            (this.Document as PdfDocumentViewModel).With<PdfDocumentViewModel, PdfDocumentStateController>(evaluator).Do<PdfDocumentStateController>(x => x.SelectAnnotation(pageNumber - 1, annotationName));
        }

        public void SelectWord(PdfDocumentPosition position)
        {
            this.CheckOperationAvailability();
            if (position == null)
            {
                throw new ArgumentNullException("position");
            }
            this.UnselectAll();
            PdfDocumentSelectionParameter selectionParameter = new PdfDocumentSelectionParameter();
            selectionParameter.Position = position;
            selectionParameter.SelectionAction = PdfSelectionAction.SelectWord;
            this.Document.PerformSelection(selectionParameter);
        }

        protected virtual void SetCommentCursorMode(CursorModeType cursorMode)
        {
            Func<IPdfDocument, IPdfDocumentSelectionResults> evaluator = <>c.<>9__401_0;
            if (<>c.<>9__401_0 == null)
            {
                Func<IPdfDocument, IPdfDocumentSelectionResults> local1 = <>c.<>9__401_0;
                evaluator = <>c.<>9__401_0 = x => x.SelectionResults;
            }
            Func<IPdfDocumentSelectionResults, bool> func2 = <>c.<>9__401_1;
            if (<>c.<>9__401_1 == null)
            {
                Func<IPdfDocumentSelectionResults, bool> local2 = <>c.<>9__401_1;
                func2 = <>c.<>9__401_1 = x => x.ContentType == PdfDocumentContentType.Text;
            }
            if (!this.Document.With<IPdfDocument, IPdfDocumentSelectionResults>(evaluator).Return<IPdfDocumentSelectionResults, bool>(func2, (<>c.<>9__401_2 ??= () => false)))
            {
                this.CursorMode = (this.CursorMode == cursorMode) ? this.lastCommonCursorMode : cursorMode;
                PdfCommandProvider actualCommandProvider = base.ActualCommandProvider as PdfCommandProvider;
                if (<>c.<>9__401_4 == null)
                {
                    PdfCommandProvider local5 = base.ActualCommandProvider as PdfCommandProvider;
                    actualCommandProvider = (PdfCommandProvider) (<>c.<>9__401_4 = x => x.UpdateCursorModeCheckState());
                }
                ((PdfCommandProvider) <>c.<>9__401_4).Do<PdfCommandProvider>((Action<PdfCommandProvider>) actualCommandProvider);
            }
            else
            {
                switch (cursorMode)
                {
                    case CursorModeType.TextHighlightTool:
                        this.HighlightSelectedText();
                        break;

                    case CursorModeType.TextStrikethroughTool:
                        this.StrikethroughSelectedText();
                        break;

                    case CursorModeType.TextUnderlineTool:
                        this.UnderlineSelectedText();
                        break;

                    default:
                        break;
                }
                Action<PdfCommandProvider> action = <>c.<>9__401_3;
                if (<>c.<>9__401_3 == null)
                {
                    Action<PdfCommandProvider> local4 = <>c.<>9__401_3;
                    action = <>c.<>9__401_3 = x => x.UpdateCursorModeCheckState();
                }
                (base.ActualCommandProvider as PdfCommandProvider).Do<PdfCommandProvider>(action);
            }
        }

        protected virtual void SetCursorMode(CursorModeType cursorMode)
        {
            this.CursorMode = cursorMode;
            if (cursorMode != CursorModeType.MarqueeZoom)
            {
                this.lastCommonCursorMode = cursorMode;
            }
            Action<PdfCommandProvider> action = <>c.<>9__399_0;
            if (<>c.<>9__399_0 == null)
            {
                Action<PdfCommandProvider> local1 = <>c.<>9__399_0;
                action = <>c.<>9__399_0 = x => x.UpdateCursorModeCheckState();
            }
            (base.ActualCommandProvider as PdfCommandProvider).Do<PdfCommandProvider>(action);
        }

        private void SetMarkupToolsDefaultSettings(PdfAnnotationPropertiesFormViewModel viewModel)
        {
            this.MarkupToolsSettings.Author = viewModel.Author;
            System.Windows.Media.Color color = System.Windows.Media.Color.FromArgb((byte) (viewModel.Opacity * 2.55), viewModel.Color.R, viewModel.Color.G, viewModel.Color.B);
            switch (viewModel.MarkupType)
            {
                case PdfTextMarkupAnnotationType.Underline:
                case PdfTextMarkupAnnotationType.Squiggly:
                    this.MarkupToolsSettings.TextUnderlineDefaultSubject = viewModel.Subject;
                    this.MarkupToolsSettings.TextUnderlineColor = color;
                    return;

                case PdfTextMarkupAnnotationType.StrikeOut:
                    this.MarkupToolsSettings.TextStrikethroughDefaultSubject = viewModel.Subject;
                    this.MarkupToolsSettings.TextStrikethroughColor = color;
                    return;
            }
            this.MarkupToolsSettings.TextHighlightDefaultSubject = viewModel.Subject;
            this.MarkupToolsSettings.TextHighlightColor = color;
        }

        protected virtual void SetPageLayout(PdfPageLayout pageLayout)
        {
            if (((pageLayout != PdfPageLayout.TwoColumnLeft) && (pageLayout != PdfPageLayout.TwoPageLeft)) || !this.IsShowCoverPage)
            {
                this.PageLayout = pageLayout;
            }
            else
            {
                this.PageLayout = (pageLayout == PdfPageLayout.TwoPageLeft) ? PdfPageLayout.TwoPageRight : PdfPageLayout.TwoColumnRight;
            }
        }

        protected virtual void ShowAnnotationProperties()
        {
            // Unresolved stack state at '000001F8'
        }

        protected virtual void ShowCoverPage()
        {
            switch (this.PageLayout)
            {
                case PdfPageLayout.TwoColumnLeft:
                    this.PageLayout = PdfPageLayout.TwoColumnRight;
                    return;

                case PdfPageLayout.TwoColumnRight:
                    this.PageLayout = PdfPageLayout.TwoColumnLeft;
                    return;

                case PdfPageLayout.TwoPageLeft:
                    this.PageLayout = PdfPageLayout.TwoPageRight;
                    return;

                case PdfPageLayout.TwoPageRight:
                    this.PageLayout = PdfPageLayout.TwoPageLeft;
                    return;
            }
        }

        private void ShowGetPasswordDialog(PasswordViewModel passwordModel, string documentName)
        {
            DXDialog dialog = new DXDialog(string.Format(PdfViewerLocalizer.GetString(PdfViewerStringId.MessageDocumentIsProtected), documentName)) {
                Owner = LayoutHelper.GetTopLevelVisual(this) as Window,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                SizeToContent = SizeToContent.WidthAndHeight,
                ResizeMode = ResizeMode.NoResize
            };
            ThemeManager.SetThemeName(dialog, ThemeHelper.GetEditorThemeName(this));
            PasswordControl control1 = new PasswordControl();
            control1.DataContext = passwordModel;
            dialog.Content = control1;
            bool? nullable = dialog.ShowDialog();
            bool flag = true;
            if ((nullable.GetValueOrDefault() == flag) ? (nullable == null) : true)
            {
                Action<PdfDocumentViewModel> action = <>c.<>9__509_0;
                if (<>c.<>9__509_0 == null)
                {
                    Action<PdfDocumentViewModel> local1 = <>c.<>9__509_0;
                    action = <>c.<>9__509_0 = x => x.CancelLoadDocument();
                }
                (this.Document as PdfDocumentViewModel).Do<PdfDocumentViewModel>(action);
            }
        }

        private void ShowProperties()
        {
            PdfDocumentProperties documentProperties = (PdfDocumentProperties) this.Document.GetDocumentProperties();
            PdfPageViewModel model = (PdfPageViewModel) this.Document.Pages.ElementAtOrDefault<IPdfPage>((base.CurrentPageNumber - 1));
            if (model != null)
            {
                documentProperties.PageSize = string.Format(PdfViewerLocalizer.GetString(PdfViewerStringId.PageSize), model.InchPageSize.Width, model.InchPageSize.Height);
            }
            DXDialog dialog = new DXDialog(PdfViewerLocalizer.GetString(PdfViewerStringId.PropertiesCaption), DialogButtons.Ok) {
                Owner = LayoutHelper.GetTopLevelVisual(this) as Window,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                SizeToContent = SizeToContent.Height,
                ResizeMode = ResizeMode.NoResize,
                ShowIcon = false,
                Width = 470.0
            };
            ThemeManager.SetThemeName(dialog, ThemeHelper.GetEditorThemeName(this));
            PropertiesControl control1 = new PropertiesControl();
            control1.DataContext = documentProperties;
            dialog.Content = control1;
            dialog.ShowDialog();
        }

        public void StrikethroughSelectedText()
        {
            Func<PdfDocumentViewModel, PdfDocumentStateController> evaluator = <>c.<>9__433_0;
            if (<>c.<>9__433_0 == null)
            {
                Func<PdfDocumentViewModel, PdfDocumentStateController> local1 = <>c.<>9__433_0;
                evaluator = <>c.<>9__433_0 = x => x.DocumentStateController;
            }
            Action<PdfDocumentStateController> action = <>c.<>9__433_1;
            if (<>c.<>9__433_1 == null)
            {
                Action<PdfDocumentStateController> local2 = <>c.<>9__433_1;
                action = <>c.<>9__433_1 = x => x.AddTextMarkupAnnotation(PdfTextMarkupAnnotationType.StrikeOut);
            }
            (this.Document as PdfDocumentViewModel).With<PdfDocumentViewModel, PdfDocumentStateController>(evaluator).Do<PdfDocumentStateController>(action);
        }

        public void StrikethroughSelectedText(string comment)
        {
            Func<PdfDocumentViewModel, PdfDocumentStateController> evaluator = <>c.<>9__439_0;
            if (<>c.<>9__439_0 == null)
            {
                Func<PdfDocumentViewModel, PdfDocumentStateController> local1 = <>c.<>9__439_0;
                evaluator = <>c.<>9__439_0 = x => x.DocumentStateController;
            }
            (this.Document as PdfDocumentViewModel).With<PdfDocumentViewModel, PdfDocumentStateController>(evaluator).Do<PdfDocumentStateController>(x => x.AddTextMarkupAnnotation(PdfTextMarkupAnnotationType.StrikeOut, comment));
        }

        public void StrikethroughSelectedText(System.Windows.Media.Color color)
        {
            System.Drawing.Color drawingColor = System.Drawing.Color.FromArgb(color.A, color.R, color.G, color.B);
            Func<PdfDocumentViewModel, PdfDocumentStateController> evaluator = <>c.<>9__436_0;
            if (<>c.<>9__436_0 == null)
            {
                Func<PdfDocumentViewModel, PdfDocumentStateController> local1 = <>c.<>9__436_0;
                evaluator = <>c.<>9__436_0 = x => x.DocumentStateController;
            }
            (this.Document as PdfDocumentViewModel).With<PdfDocumentViewModel, PdfDocumentStateController>(evaluator).Do<PdfDocumentStateController>(x => x.AddTextMarkupAnnotation(PdfTextMarkupAnnotationType.StrikeOut, null, drawingColor));
        }

        public void StrikethroughSelectedText(string comment, System.Windows.Media.Color color)
        {
            System.Drawing.Color drawingColor = System.Drawing.Color.FromArgb(color.A, color.R, color.G, color.B);
            Func<PdfDocumentViewModel, PdfDocumentStateController> evaluator = <>c.<>9__442_0;
            if (<>c.<>9__442_0 == null)
            {
                Func<PdfDocumentViewModel, PdfDocumentStateController> local1 = <>c.<>9__442_0;
                evaluator = <>c.<>9__442_0 = x => x.DocumentStateController;
            }
            (this.Document as PdfDocumentViewModel).With<PdfDocumentViewModel, PdfDocumentStateController>(evaluator).Do<PdfDocumentStateController>(x => x.AddTextMarkupAnnotation(PdfTextMarkupAnnotationType.StrikeOut, comment, drawingColor));
        }

        private void ThumbnailsViewerSettingsChanged(PdfThumbnailsViewerSettings oldValue, PdfThumbnailsViewerSettings newValue)
        {
            PdfThumbnailsViewerSettings settings1 = newValue;
            if (newValue == null)
            {
                PdfThumbnailsViewerSettings local1 = newValue;
                settings1 = this.CreateDefaultThumbnailsSettings();
            }
            this.ActualThumbnailsViewerSettings = settings1;
        }

        public void UnderlineSelectedText()
        {
            Func<PdfDocumentViewModel, PdfDocumentStateController> evaluator = <>c.<>9__434_0;
            if (<>c.<>9__434_0 == null)
            {
                Func<PdfDocumentViewModel, PdfDocumentStateController> local1 = <>c.<>9__434_0;
                evaluator = <>c.<>9__434_0 = x => x.DocumentStateController;
            }
            Action<PdfDocumentStateController> action = <>c.<>9__434_1;
            if (<>c.<>9__434_1 == null)
            {
                Action<PdfDocumentStateController> local2 = <>c.<>9__434_1;
                action = <>c.<>9__434_1 = x => x.AddTextMarkupAnnotation(PdfTextMarkupAnnotationType.Underline);
            }
            (this.Document as PdfDocumentViewModel).With<PdfDocumentViewModel, PdfDocumentStateController>(evaluator).Do<PdfDocumentStateController>(action);
        }

        public void UnderlineSelectedText(string comment)
        {
            Func<PdfDocumentViewModel, PdfDocumentStateController> evaluator = <>c.<>9__440_0;
            if (<>c.<>9__440_0 == null)
            {
                Func<PdfDocumentViewModel, PdfDocumentStateController> local1 = <>c.<>9__440_0;
                evaluator = <>c.<>9__440_0 = x => x.DocumentStateController;
            }
            (this.Document as PdfDocumentViewModel).With<PdfDocumentViewModel, PdfDocumentStateController>(evaluator).Do<PdfDocumentStateController>(x => x.AddTextMarkupAnnotation(PdfTextMarkupAnnotationType.Underline, comment));
        }

        public void UnderlineSelectedText(System.Windows.Media.Color color)
        {
            System.Drawing.Color drawingColor = System.Drawing.Color.FromArgb(color.A, color.R, color.G, color.B);
            Func<PdfDocumentViewModel, PdfDocumentStateController> evaluator = <>c.<>9__437_0;
            if (<>c.<>9__437_0 == null)
            {
                Func<PdfDocumentViewModel, PdfDocumentStateController> local1 = <>c.<>9__437_0;
                evaluator = <>c.<>9__437_0 = x => x.DocumentStateController;
            }
            (this.Document as PdfDocumentViewModel).With<PdfDocumentViewModel, PdfDocumentStateController>(evaluator).Do<PdfDocumentStateController>(x => x.AddTextMarkupAnnotation(PdfTextMarkupAnnotationType.Underline, null, drawingColor));
        }

        public void UnderlineSelectedText(string comment, System.Windows.Media.Color color)
        {
            System.Drawing.Color drawingColor = System.Drawing.Color.FromArgb(color.A, color.R, color.G, color.B);
            Func<PdfDocumentViewModel, PdfDocumentStateController> evaluator = <>c.<>9__443_0;
            if (<>c.<>9__443_0 == null)
            {
                Func<PdfDocumentViewModel, PdfDocumentStateController> local1 = <>c.<>9__443_0;
                evaluator = <>c.<>9__443_0 = x => x.DocumentStateController;
            }
            (this.Document as PdfDocumentViewModel).With<PdfDocumentViewModel, PdfDocumentStateController>(evaluator).Do<PdfDocumentStateController>(x => x.AddTextMarkupAnnotation(PdfTextMarkupAnnotationType.Underline, comment, drawingColor));
        }

        public void UnselectAll()
        {
            this.CheckOperationAvailability();
            PdfDocumentSelectionParameter selectionParameter = new PdfDocumentSelectionParameter();
            selectionParameter.SelectionAction = PdfSelectionAction.ClearSelection;
            this.Document.PerformSelection(selectionParameter);
            Func<PdfDocumentViewModel, PdfDocumentStateController> evaluator = <>c.<>9__471_0;
            if (<>c.<>9__471_0 == null)
            {
                Func<PdfDocumentViewModel, PdfDocumentStateController> local1 = <>c.<>9__471_0;
                evaluator = <>c.<>9__471_0 = x => x.DocumentStateController;
            }
            Action<PdfDocumentStateController> action = <>c.<>9__471_1;
            if (<>c.<>9__471_1 == null)
            {
                Action<PdfDocumentStateController> local2 = <>c.<>9__471_1;
                action = <>c.<>9__471_1 = x => x.ClearSelection();
            }
            (this.Document as PdfDocumentViewModel).With<PdfDocumentViewModel, PdfDocumentStateController>(evaluator).Do<PdfDocumentStateController>(action);
        }

        protected internal virtual void UpdateHasAttachments(bool newValue)
        {
            this.HasAttachments = this.CalcHasAttachments(this.Document);
        }

        protected internal virtual void UpdateHasOutlines(bool newValue)
        {
            this.HasOutlines = this.CalcHasOutlines(this.Document);
        }

        protected internal virtual void UpdateHasThumbnails(bool newValue)
        {
            this.HasThumbnails = this.CalcHasThumbnails(this.Document);
        }

        private PdfPrinterSettings UpdatePrinterSettings(PdfPrinterSettings settings, IEnumerable<int> pages)
        {
            if ((pages != null) && pages.Any<int>())
            {
                settings.PageNumbers = pages.ToArray<int>();
            }
            return settings;
        }

        private void UpdateProgress(int progressValue)
        {
            this.SplashScreenService.Do<ISplashScreenService>(x => x.SetSplashScreenProgress((double) progressValue, 100.0));
        }

        protected internal virtual void UpdateSelection()
        {
            this.HasSelection = this.Document.HasSelection;
        }

        private void UpdateStartScreenVisible()
        {
            ((PdfPropertyProvider) base.PropertyProvider).IsStartScreenVisible = (this.Document != null) ? false : ((this.ShowStartScreen != null) ? this.ShowStartScreen.Value : (base.CommandBarStyle == CommandBarStyle.None));
        }

        public bool AcceptsTab
        {
            get => 
                (bool) base.GetValue(AcceptsTabProperty);
            set => 
                base.SetValue(AcceptsTabProperty, value);
        }

        public bool HasThumbnails
        {
            get => 
                (bool) base.GetValue(HasThumbnailsProperty);
            private set => 
                base.SetValue(HasThumbnailsPropertyKey, value);
        }

        public PdfThumbnailsViewerSettings ThumbnailsViewerSettings
        {
            get => 
                (PdfThumbnailsViewerSettings) base.GetValue(ThumbnailsViewerSettingsProperty);
            set => 
                base.SetValue(ThumbnailsViewerSettingsProperty, value);
        }

        public PdfThumbnailsViewerSettings ActualThumbnailsViewerSettings
        {
            get => 
                (PdfThumbnailsViewerSettings) base.GetValue(ActualThumbnailsViewerSettingsProperty);
            internal set => 
                base.SetValue(ActualThumbnailsViewerSettingsPropertyKey, value);
        }

        public bool HasAttachments
        {
            get => 
                (bool) base.GetValue(HasAttachmentsProperty);
            private set => 
                base.SetValue(HasAttachmentsPropertyKey, value);
        }

        public bool HasOutlines
        {
            get => 
                (bool) base.GetValue(HasOutlinesProperty);
            private set => 
                base.SetValue(HasOutlinesPropertyKey, value);
        }

        public PdfOutlinesViewerSettings OutlinesViewerSettings
        {
            get => 
                (PdfOutlinesViewerSettings) base.GetValue(OutlinesViewerSettingsProperty);
            set => 
                base.SetValue(OutlinesViewerSettingsProperty, value);
        }

        public DataTemplate SaveFileDialogTemplate
        {
            get => 
                (DataTemplate) base.GetValue(SaveFileDialogTemplateProperty);
            set => 
                base.SetValue(SaveFileDialogTemplateProperty, value);
        }

        public DataTemplate PrintPreviewDialogTemplate
        {
            get => 
                (DataTemplate) base.GetValue(PrintPreviewDialogTemplateProperty);
            set => 
                base.SetValue(PrintPreviewDialogTemplateProperty, value);
        }

        public bool IsReadOnly
        {
            get => 
                (bool) base.GetValue(IsReadOnlyProperty);
            set => 
                base.SetValue(IsReadOnlyProperty, value);
        }

        public bool AsyncDocumentLoad
        {
            get => 
                (bool) base.GetValue(AsyncDocumentLoadProperty);
            set => 
                base.SetValue(AsyncDocumentLoadProperty, value);
        }

        public bool DetachStreamOnLoadComplete
        {
            get => 
                (bool) base.GetValue(DetachStreamOnLoadCompleteProperty);
            set => 
                base.SetValue(DetachStreamOnLoadCompleteProperty, value);
        }

        public CursorModeType CursorMode
        {
            get => 
                (CursorModeType) base.GetValue(CursorModeProperty);
            set => 
                base.SetValue(CursorModeProperty, value);
        }

        [DefaultValue(0x11e1a300)]
        public int CacheSize
        {
            get => 
                (int) base.GetValue(CacheSizeProperty);
            set => 
                base.SetValue(CacheSizeProperty, value);
        }

        public bool AllowCachePages
        {
            get => 
                (bool) base.GetValue(AllowCachePagesProperty);
            set => 
                base.SetValue(AllowCachePagesProperty, value);
        }

        public System.Windows.Media.Color HighlightSelectionColor
        {
            get => 
                (System.Windows.Media.Color) base.GetValue(HighlightSelectionColorProperty);
            set => 
                base.SetValue(HighlightSelectionColorProperty, value);
        }

        public System.Windows.Media.Color CaretColor
        {
            get => 
                (System.Windows.Media.Color) base.GetValue(CaretColorProperty);
            set => 
                base.SetValue(CaretColorProperty, value);
        }

        public Thickness PagePadding
        {
            get => 
                (Thickness) base.GetValue(PagePaddingProperty);
            set => 
                base.SetValue(PagePaddingProperty, value);
        }

        public bool AllowCurrentPageHighlighting
        {
            get => 
                (bool) base.GetValue(AllowCurrentPageHighlightingProperty);
            set => 
                base.SetValue(AllowCurrentPageHighlightingProperty, value);
        }

        public ObservableCollection<RecentFileViewModel> RecentFiles
        {
            get => 
                (ObservableCollection<RecentFileViewModel>) base.GetValue(RecentFilesProperty);
            set => 
                base.SetValue(RecentFilesProperty, value);
        }

        public int NumberOfRecentFiles
        {
            get => 
                (int) base.GetValue(NumberOfRecentFilesProperty);
            set => 
                base.SetValue(NumberOfRecentFilesProperty, value);
        }

        public bool ShowOpenFileOnStartScreen
        {
            get => 
                (bool) base.GetValue(ShowOpenFileOnStartScreenProperty);
            set => 
                base.SetValue(ShowOpenFileOnStartScreenProperty, value);
        }

        public bool HasSelection
        {
            get => 
                (bool) base.GetValue(HasSelectionProperty);
            internal set => 
                base.SetValue(HasSelectionPropertyKey, value);
        }

        public bool? ShowStartScreen
        {
            get => 
                (bool?) base.GetValue(ShowStartScreenProperty);
            set => 
                base.SetValue(ShowStartScreenProperty, value);
        }

        public int MaxPrintingDpi
        {
            get => 
                (int) base.GetValue(MaxPrintingDpiProperty);
            set => 
                base.SetValue(MaxPrintingDpiProperty, value);
        }

        public string DocumentCreator
        {
            get => 
                (string) base.GetValue(DocumentCreatorProperty);
            set => 
                base.SetValue(DocumentCreatorProperty, value);
        }

        public string DocumentProducer
        {
            get => 
                (string) base.GetValue(DocumentProducerProperty);
            set => 
                base.SetValue(DocumentProducerProperty, value);
        }

        public PdfPageLayout PageLayout
        {
            get => 
                (PdfPageLayout) base.GetValue(PageLayoutProperty);
            set => 
                base.SetValue(PageLayoutProperty, value);
        }

        public DevExpress.Xpf.PdfViewer.NavigationPanelsLayout NavigationPanelsLayout
        {
            get => 
                (DevExpress.Xpf.PdfViewer.NavigationPanelsLayout) base.GetValue(NavigationPanelsLayoutProperty);
            set => 
                base.SetValue(NavigationPanelsLayoutProperty, value);
        }

        public PdfAttachmentsViewerSettings ActualAttachmentsViewerSettings
        {
            get => 
                (PdfAttachmentsViewerSettings) base.GetValue(ActualAttachmentsViewerSettingsProperty);
            protected set => 
                base.SetValue(ActualAttachmentsViewerSettingsPropertyKey, value);
        }

        public PdfAttachmentsViewerSettings AttachmentsViewerSettings
        {
            get => 
                (PdfAttachmentsViewerSettings) base.GetValue(AttachmentsViewerSettingsProperty);
            set => 
                base.SetValue(AttachmentsViewerSettingsProperty, value);
        }

        public bool HighlightFormFields
        {
            get => 
                (bool) base.GetValue(HighlightFormFieldsProperty);
            set => 
                base.SetValue(HighlightFormFieldsProperty, value);
        }

        public PdfMarkupToolsSettings MarkupToolsSettings
        {
            get => 
                (PdfMarkupToolsSettings) base.GetValue(MarkupToolsSettingsProperty);
            set => 
                base.SetValue(MarkupToolsSettingsProperty, value);
        }

        public bool ShowPrintStatusDialog
        {
            get => 
                (bool) base.GetValue(ShowPrintStatusDialogProperty);
            set => 
                base.SetValue(ShowPrintStatusDialogProperty, value);
        }

        public System.Windows.Media.Color HighlightedFormFieldColor
        {
            get => 
                (System.Windows.Media.Color) base.GetValue(HighlightedFormFieldColorProperty);
            set => 
                base.SetValue(HighlightedFormFieldColorProperty, value);
        }

        public PdfContinueSearchFrom ContinueSearchFrom
        {
            get => 
                (PdfContinueSearchFrom) base.GetValue(ContinueSearchFromProperty);
            set => 
                base.SetValue(ContinueSearchFromProperty, value);
        }

        public ICommand PrintDocumentCommand { get; private set; }

        public ICommand ShowPropertiesCommand { get; private set; }

        public ICommand SetCursorModeCommand { get; private set; }

        public ICommand SelectionCommand { get; private set; }

        public ICommand OpenRecentDocumentCommand { get; private set; }

        public ICommand SelectAllCommand { get; private set; }

        public ICommand UnselectAllCommand { get; private set; }

        public ICommand SaveAsCommand { get; private set; }

        public ICommand CopyCommand { get; private set; }

        public ICommand OpenDocumentFromWebCommand { get; private set; }

        public ICommand ImportFormDataCommand { get; private set; }

        public ICommand ExportFormDataCommand { get; private set; }

        public ICommand SetPageLayoutCommand { get; private set; }

        public ICommand ShowCoverPageCommand { get; private set; }

        public ICommand OpenAttachmentCommand { get; private set; }

        public ICommand SaveAttachmentCommand { get; private set; }

        public ICommand HighlightTextCommand { get; private set; }

        public ICommand StrikethroughTextCommand { get; private set; }

        public ICommand UnderlineTextCommand { get; private set; }

        public ICommand DeleteAnnotationCommand { get; private set; }

        public ICommand ShowAnnotationPropertiesCommand { get; private set; }

        public ICommand SetCommentCursorModeCommand { get; private set; }

        public IPdfDocument Document =>
            (IPdfDocument) base.GetValue(DocumentViewerControl.DocumentProperty);

        private IPdfViewerController ViewerController =>
            this.InteractionProvider;

        private PdfOutlinesViewerSettings ActualDocumentMapSettings
        {
            get => 
                base.ActualDocumentMapSettings as PdfOutlinesViewerSettings;
            set => 
                base.ActualDocumentMapSettings = value;
        }

        internal PdfPresenterControl DocumentPresenter =>
            base.DocumentPresenter as PdfPresenterControl;

        internal bool IsShowCoverPage =>
            (this.PageLayout == PdfPageLayout.TwoColumnRight) || (this.PageLayout == PdfPageLayout.TwoPageRight);

        internal DevExpress.Xpf.PdfViewer.Internal.InteractionProvider InteractionProvider { get; private set; }

        internal PdfViewerBackend ViewerBackend =>
            this.viewerBackend;

        protected internal ISplashScreenService SplashScreenService =>
            base.ActualCommandProvider.ServiceContainer.GetService<ISplashScreenService>(ServiceSearchMode.PreferLocal);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PdfViewerControl.<>c <>9 = new PdfViewerControl.<>c();
            public static Action<PdfAttachmentsViewerSettings> <>9__378_0;
            public static Func<PdfDocumentViewModel, PdfDocumentStateController> <>9__379_0;
            public static Func<PdfFileAttachmentListItem, PdfFileAttachment> <>9__379_2;
            public static Func<PdfDocumentViewModel, PdfDocumentStateController> <>9__381_0;
            public static Func<PdfFileAttachmentListItem, PdfFileAttachment> <>9__381_2;
            public static Action<ISplashScreenService> <>9__387_0;
            public static Action<InteractionProvider> <>9__388_0;
            public static Action<ISplashScreenService> <>9__389_0;
            public static Action<ISplashScreenService> <>9__389_2;
            public static Action<ISplashScreenService> <>9__390_0;
            public static Func<PdfDocumentViewModel, IList<PdfPageViewModel>> <>9__390_1;
            public static Func<IList<PdfPageViewModel>, int> <>9__390_2;
            public static Func<PdfDocumentViewModel, PdfDocumentState> <>9__390_4;
            public static Func<PdfDocumentViewModel, PdfDocumentState> <>9__390_6;
            public static Func<PdfDocumentViewModel, PdfDocument> <>9__390_8;
            public static Func<PdfDocument, PdfInteractiveForm> <>9__390_9;
            public static Func<PdfDocumentViewModel, PdfDocument> <>9__390_11;
            public static Func<PdfDocument, PdfInteractiveForm> <>9__390_12;
            public static Func<IPdfDocument, bool> <>9__393_0;
            public static Func<IPdfDocument, bool> <>9__398_0;
            public static Func<bool> <>9__398_1;
            public static Action<PdfCommandProvider> <>9__399_0;
            public static Func<IPdfDocument, bool> <>9__400_0;
            public static Func<bool> <>9__400_1;
            public static Func<IPdfDocument, IPdfDocumentSelectionResults> <>9__401_0;
            public static Func<IPdfDocumentSelectionResults, bool> <>9__401_1;
            public static Func<bool> <>9__401_2;
            public static Action<PdfCommandProvider> <>9__401_3;
            public static Action<PdfCommandProvider> <>9__401_4;
            public static Func<IPdfDocument, bool> <>9__402_0;
            public static Func<bool> <>9__402_1;
            public static Func<IPdfDocument, bool> <>9__405_0;
            public static Func<bool> <>9__405_1;
            public static Func<IPdfDocument, bool> <>9__407_0;
            public static Func<bool> <>9__407_1;
            public static Func<PdfDocumentViewModel, PdfDocumentStateController> <>9__408_0;
            public static Action<PdfDocumentStateController> <>9__408_1;
            public static Func<PdfDocumentViewModel, PdfDocumentStateController> <>9__409_0;
            public static Func<PdfDocumentStateController, PdfAnnotationPropertiesFormViewModel> <>9__409_1;
            public static Func<PdfAnnotationPropertiesFormViewModel> <>9__409_2;
            public static Action<PdfThumbnailsViewerSettings> <>9__409_7;
            public static Action<PdfPresenterControl> <>9__409_8;
            public static Func<SaveFileDialogService, bool> <>9__413_1;
            public static Func<bool> <>9__413_2;
            public static Func<ISaveFileDialogService, bool> <>9__413_4;
            public static Func<bool> <>9__413_5;
            public static Action<ISplashScreenService> <>9__418_0;
            public static Action<ISplashScreenService> <>9__418_1;
            public static Func<IPdfDocument, IPdfDocumentSelectionResults> <>9__419_0;
            public static Action<InteractionProvider> <>9__422_0;
            public static Func<IPdfDocument, bool> <>9__423_0;
            public static Func<bool> <>9__423_1;
            public static Func<IPdfDocument, bool> <>9__424_0;
            public static Func<bool> <>9__424_1;
            public static Func<bool> <>9__425_1;
            public static Func<IPdfDocument, bool> <>9__426_0;
            public static Func<bool> <>9__426_1;
            public static Func<IPdfDocument, bool> <>9__427_0;
            public static Func<bool> <>9__427_1;
            public static Func<IPdfDocument, bool> <>9__428_0;
            public static Func<bool> <>9__428_1;
            public static Func<PdfDocumentViewModel, IList<PdfPageViewModel>> <>9__430_0;
            public static Func<PdfPageViewModel, System.Windows.Size> <>9__430_2;
            public static Func<System.Windows.Size> <>9__430_3;
            public static Func<PdfDocumentViewModel, PdfDocumentSelectionResults> <>9__431_0;
            public static Func<PdfDocumentViewModel, PdfDocumentStateController> <>9__432_0;
            public static Action<PdfDocumentStateController> <>9__432_1;
            public static Func<PdfDocumentViewModel, PdfDocumentStateController> <>9__433_0;
            public static Action<PdfDocumentStateController> <>9__433_1;
            public static Func<PdfDocumentViewModel, PdfDocumentStateController> <>9__434_0;
            public static Action<PdfDocumentStateController> <>9__434_1;
            public static Func<PdfDocumentViewModel, PdfDocumentStateController> <>9__435_0;
            public static Func<PdfDocumentViewModel, PdfDocumentStateController> <>9__436_0;
            public static Func<PdfDocumentViewModel, PdfDocumentStateController> <>9__437_0;
            public static Func<PdfDocumentViewModel, PdfDocumentStateController> <>9__438_0;
            public static Func<PdfDocumentViewModel, PdfDocumentStateController> <>9__439_0;
            public static Func<PdfDocumentViewModel, PdfDocumentStateController> <>9__440_0;
            public static Func<PdfDocumentViewModel, PdfDocumentStateController> <>9__441_0;
            public static Func<PdfDocumentViewModel, PdfDocumentStateController> <>9__442_0;
            public static Func<PdfDocumentViewModel, PdfDocumentStateController> <>9__443_0;
            public static Func<PdfDocumentViewModel, string> <>9__450_0;
            public static Func<PdfPrintDialogViewModel, bool> <>9__461_1;
            public static Func<System.Windows.Point> <>9__465_1;
            public static Func<PdfDocumentViewModel, PdfDocumentStateController> <>9__471_0;
            public static Action<PdfDocumentStateController> <>9__471_1;
            public static Func<PdfDocumentViewModel, PdfViewerBackend> <>9__476_0;
            public static Func<PdfDocumentViewModel, PdfViewerBackend> <>9__477_0;
            public static Func<PdfDocumentViewModel, PdfViewerBackend> <>9__478_0;
            public static Func<PdfDocumentViewModel, PdfViewerBackend> <>9__479_0;
            public static Func<PdfDocumentViewModel, PdfViewerBackend> <>9__480_0;
            public static Func<PdfDocumentViewModel, PdfViewerBackend> <>9__481_0;
            public static Func<PdfDocumentViewModel, PdfDocumentStateController> <>9__482_0;
            public static Func<IPdfDocument, bool> <>9__489_0;
            public static Func<bool> <>9__489_1;
            public static Func<IPdfDocument, bool> <>9__491_0;
            public static Func<bool> <>9__491_1;
            public static Func<IPdfDocument, bool> <>9__492_0;
            public static Func<bool> <>9__492_1;
            public static Func<IPdfDocument, bool> <>9__497_0;
            public static Func<bool> <>9__497_1;
            public static Func<IPdfDocument, bool> <>9__498_0;
            public static Func<bool> <>9__498_1;
            public static Func<IPdfDocument, bool> <>9__499_0;
            public static Func<bool> <>9__499_1;
            public static Action<PdfPresenterControl> <>9__503_0;
            public static Func<PdfDocumentViewModel, PdfDocument> <>9__505_2;
            public static Func<PdfDocument, PdfInteractiveForm> <>9__505_3;
            public static Func<PdfDocumentViewModel, PdfDocument> <>9__505_5;
            public static Func<PdfDocument, PdfInteractiveForm> <>9__505_6;
            public static Action<IDisposable> <>9__505_8;
            public static Func<PdfDocumentViewModel, bool> <>9__506_1;
            public static Func<bool> <>9__506_2;
            public static Func<PdfDocumentViewModel, bool> <>9__506_5;
            public static Func<bool> <>9__506_6;
            public static Action<PdfDocumentViewModel> <>9__509_0;
            public static Action<ISplashScreenService> <>9__511_0;
            public static Func<PdfDocumentViewModel, bool> <>9__511_1;
            public static Func<bool> <>9__511_2;
            public static Action<ISplashScreenService> <>9__511_3;
            public static Func<PdfDocumentViewModel, PdfFormData> <>9__518_0;
            public static Action<PdfThumbnailsViewerSettings> <>9__523_0;
            public static Func<PdfDocumentViewModel, PdfDocumentState> <>9__532_0;
            public static Func<PdfDocumentViewModel, PdfDocumentState> <>9__533_0;
            public static Func<PdfDocumentViewModel, PdfDocumentStateController> <>9__545_0;
            public static Func<PdfDocumentStateController, PdfTextMarkupAnnotationState> <>9__545_1;
            public static Func<PdfTextMarkupAnnotationState> <>9__545_2;
            public static Action<Action> <>9__548_0;

            internal void <.cctor>b__69_0(DependencyObject d, DependencyPropertyChangedEventArgs args)
            {
                ((PdfViewerControl) d).OnCursorModeChanged((CursorModeType) args.NewValue);
            }

            internal void <.cctor>b__69_1(DependencyObject obj, DependencyPropertyChangedEventArgs args)
            {
                ((PdfViewerControl) obj).OnShowStartScreenChanged((bool?) args.NewValue);
            }

            internal void <.cctor>b__69_10(PdfViewerControl owner, PdfThumbnailsViewerSettings oldValue, PdfThumbnailsViewerSettings newValue)
            {
                owner.ActualThumbnailsViewerSettingsChanged(oldValue, newValue);
            }

            internal void <.cctor>b__69_11(PdfViewerControl control, PdfThumbnailsViewerSettings oldValue, PdfThumbnailsViewerSettings newValue)
            {
                control.ThumbnailsViewerSettingsChanged(oldValue, newValue);
            }

            internal void <.cctor>b__69_12(PdfViewerControl owner, bool oldValue, bool newValue)
            {
                owner.HighlightFormFieldsChanged(newValue);
            }

            internal void <.cctor>b__69_13(PdfViewerControl owner, System.Windows.Media.Color oldValue, System.Windows.Media.Color newValue)
            {
                owner.HighlightedFormFieldColorChanged(newValue);
            }

            internal object <.cctor>b__69_2(DependencyObject o, object value) => 
                (((int) value) < 0) ? 0 : value;

            internal void <.cctor>b__69_3(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((PdfViewerControl) d).AllowCachePagesChanged((bool) e.NewValue);
            }

            internal void <.cctor>b__69_4(DependencyObject obj, DependencyPropertyChangedEventArgs args)
            {
                ((PdfViewerControl) obj).CacheSizeChanged((int) args.NewValue);
            }

            internal void <.cctor>b__69_5(PdfViewerControl control, PdfOutlinesViewerSettings oldValue, PdfOutlinesViewerSettings newValue)
            {
                control.OutlinesViewerSettingsChanged(oldValue, newValue);
            }

            internal void <.cctor>b__69_6(PdfViewerControl d, PdfPageLayout oldValue, PdfPageLayout newValue)
            {
                d.OnPageLayoutChanged(newValue);
            }

            internal void <.cctor>b__69_7(PdfViewerControl d, NavigationPanelsLayout oldValue, NavigationPanelsLayout newValue)
            {
                d.NavigationPanelsLayoutChanged(newValue);
            }

            internal void <.cctor>b__69_8(PdfViewerControl owner, PdfAttachmentsViewerSettings oldValue, PdfAttachmentsViewerSettings newValue)
            {
                owner.ActualAttachmentsViewerSettingsChanged(oldValue, newValue);
            }

            internal void <.cctor>b__69_9(PdfViewerControl control, PdfAttachmentsViewerSettings oldValue, PdfAttachmentsViewerSettings newValue)
            {
                control.AttachmentsViewerSettingsChanged(oldValue, newValue);
            }

            internal void <ActualAttachmentsViewerSettingsChanged>b__378_0(PdfAttachmentsViewerSettings x)
            {
                x.Release();
            }

            internal void <ActualThumbnailsViewerSettingsChanged>b__523_0(PdfThumbnailsViewerSettings x)
            {
                x.Release();
            }

            internal bool <CalcHasOutlines>b__393_0(IPdfDocument x) => 
                x.HasOutlines;

            internal bool <CanExportFormData>b__426_0(IPdfDocument x) => 
                x.IsLoaded && x.HasInteractiveForm;

            internal bool <CanExportFormData>b__426_1() => 
                false;

            internal bool <CanImportFormData>b__425_1() => 
                false;

            internal bool <CanNavigate>b__423_0(IPdfDocument x) => 
                x.IsLoaded;

            internal bool <CanNavigate>b__423_1() => 
                false;

            internal bool <CanNavigate>b__424_0(IPdfDocument x) => 
                x.IsLoaded;

            internal bool <CanNavigate>b__424_1() => 
                false;

            internal bool <CanNavigate>b__427_0(IPdfDocument x) => 
                x.IsLoaded;

            internal bool <CanNavigate>b__427_1() => 
                false;

            internal bool <CanNavigate>b__428_0(IPdfDocument x) => 
                x.IsLoaded;

            internal bool <CanNavigate>b__428_1() => 
                false;

            internal bool <CanPrint>b__491_0(IPdfDocument x) => 
                x.IsLoaded;

            internal bool <CanPrint>b__491_1() => 
                false;

            internal bool <CanSaveAs>b__402_0(IPdfDocument x) => 
                x.IsLoaded;

            internal bool <CanSaveAs>b__402_1() => 
                false;

            internal bool <CanSelect>b__497_0(IPdfDocument x) => 
                x.IsLoaded;

            internal bool <CanSelect>b__497_1() => 
                false;

            internal bool <CanSelectAll>b__498_0(IPdfDocument x) => 
                x.IsLoaded;

            internal bool <CanSelectAll>b__498_1() => 
                false;

            internal bool <CanSetCommentCursorMode>b__400_0(IPdfDocument x) => 
                x.IsLoaded;

            internal bool <CanSetCommentCursorMode>b__400_1() => 
                false;

            internal bool <CanSetCursorMode>b__398_0(IPdfDocument x) => 
                x.IsLoaded;

            internal bool <CanSetCursorMode>b__398_1() => 
                false;

            internal bool <CanSetPageLayout>b__405_0(IPdfDocument x) => 
                x.IsLoaded;

            internal bool <CanSetPageLayout>b__405_1() => 
                false;

            internal bool <CanShowCoverPage>b__407_0(IPdfDocument x) => 
                x.IsLoaded;

            internal bool <CanShowCoverPage>b__407_1() => 
                false;

            internal bool <CanShowProperties>b__489_0(IPdfDocument x) => 
                x.IsLoaded;

            internal bool <CanShowProperties>b__489_1() => 
                false;

            internal bool <CanUnselectAll>b__499_0(IPdfDocument x) => 
                x.IsLoaded;

            internal bool <CanUnselectAll>b__499_1() => 
                false;

            internal bool <CheckOperationAvailability>b__492_0(IPdfDocument x) => 
                x.IsLoaded;

            internal bool <CheckOperationAvailability>b__492_1() => 
                false;

            internal void <CloseDocument>b__388_0(InteractionProvider x)
            {
                x.CommitEditor();
            }

            internal System.Windows.Point <ConvertDocumentPositionToPixel>b__465_1() => 
                new System.Windows.Point(0.0, 0.0);

            internal IPdfDocumentSelectionResults <Copy>b__419_0(IPdfDocument x) => 
                x.SelectionResults;

            internal PdfViewerBackend <CreateTiff>b__476_0(PdfDocumentViewModel x) => 
                x.ViewerBackend;

            internal PdfViewerBackend <CreateTiff>b__477_0(PdfDocumentViewModel x) => 
                x.ViewerBackend;

            internal PdfViewerBackend <CreateTiff>b__478_0(PdfDocumentViewModel x) => 
                x.ViewerBackend;

            internal PdfViewerBackend <CreateTiff>b__479_0(PdfDocumentViewModel x) => 
                x.ViewerBackend;

            internal PdfViewerBackend <CreateTiff>b__480_0(PdfDocumentViewModel x) => 
                x.ViewerBackend;

            internal PdfViewerBackend <CreateTiff>b__481_0(PdfDocumentViewModel x) => 
                x.ViewerBackend;

            internal PdfDocumentStateController <DeleteAnnotation>b__408_0(PdfDocumentViewModel x) => 
                x.DocumentStateController;

            internal void <DeleteAnnotation>b__408_1(PdfDocumentStateController x)
            {
                x.RemoveSelectedAnnotation();
            }

            internal PdfFormData <DevExpress.Pdf.IPdfViewer.GetDocumentProcessorHelper>b__518_0(PdfDocumentViewModel x) => 
                x.GetFormData();

            internal void <DoWithDocumentProgress>b__418_0(ISplashScreenService x)
            {
                x.ShowSplashScreen();
            }

            internal void <DoWithDocumentProgress>b__418_1(ISplashScreenService x)
            {
                x.HideSplashScreen();
            }

            internal string <ExportFormData>b__450_0(PdfDocumentViewModel x) => 
                x.FilePath;

            internal IList<PdfPageViewModel> <GetPageSize>b__430_0(PdfDocumentViewModel x) => 
                x.Pages;

            internal System.Windows.Size <GetPageSize>b__430_2(PdfPageViewModel x) => 
                x.InchPageSize;

            internal System.Windows.Size <GetPageSize>b__430_3() => 
                System.Windows.Size.Empty;

            internal PdfDocumentSelectionResults <GetSelectionContent>b__431_0(PdfDocumentViewModel x) => 
                x.SelectionResults as PdfDocumentSelectionResults;

            internal PdfDocumentState <HighlightedFormFieldColorChanged>b__533_0(PdfDocumentViewModel x) => 
                x.DocumentState;

            internal PdfDocumentState <HighlightFormFieldsChanged>b__532_0(PdfDocumentViewModel x) => 
                x.DocumentState;

            internal PdfDocumentStateController <HighlightSelectedText>b__432_0(PdfDocumentViewModel x) => 
                x.DocumentStateController;

            internal void <HighlightSelectedText>b__432_1(PdfDocumentStateController x)
            {
                x.AddTextMarkupAnnotation(PdfTextMarkupAnnotationType.Highlight);
            }

            internal PdfDocumentStateController <HighlightSelectedText>b__435_0(PdfDocumentViewModel x) => 
                x.DocumentStateController;

            internal PdfDocumentStateController <HighlightSelectedText>b__438_0(PdfDocumentViewModel x) => 
                x.DocumentStateController;

            internal PdfDocumentStateController <HighlightSelectedText>b__441_0(PdfDocumentViewModel x) => 
                x.DocumentStateController;

            internal void <LoadDocument>b__387_0(ISplashScreenService x)
            {
                x.ShowSplashScreen();
            }

            internal void <OnDocumentLoadedInternal>b__390_0(ISplashScreenService x)
            {
                x.HideSplashScreen();
            }

            internal IList<PdfPageViewModel> <OnDocumentLoadedInternal>b__390_1(PdfDocumentViewModel x) => 
                x.Pages;

            internal PdfDocument <OnDocumentLoadedInternal>b__390_11(PdfDocumentViewModel x) => 
                x.PdfDocument;

            internal PdfInteractiveForm <OnDocumentLoadedInternal>b__390_12(PdfDocument x) => 
                x.AcroForm;

            internal int <OnDocumentLoadedInternal>b__390_2(IList<PdfPageViewModel> x) => 
                x.Count<PdfPageViewModel>();

            internal PdfDocumentState <OnDocumentLoadedInternal>b__390_4(PdfDocumentViewModel x) => 
                x.DocumentState;

            internal PdfDocumentState <OnDocumentLoadedInternal>b__390_6(PdfDocumentViewModel x) => 
                x.DocumentState;

            internal PdfDocument <OnDocumentLoadedInternal>b__390_8(PdfDocumentViewModel x) => 
                x.PdfDocument;

            internal PdfInteractiveForm <OnDocumentLoadedInternal>b__390_9(PdfDocument x) => 
                x.AcroForm;

            internal void <OnDocumentProgressChanged>b__389_0(ISplashScreenService x)
            {
                x.HideSplashScreen();
            }

            internal void <OnDocumentProgressChanged>b__389_2(ISplashScreenService x)
            {
                x.HideSplashScreen();
            }

            internal void <OnDocumentRequestPassword>b__511_0(ISplashScreenService x)
            {
                x.HideSplashScreen();
            }

            internal bool <OnDocumentRequestPassword>b__511_1(PdfDocumentViewModel x) => 
                !x.IsCancelled;

            internal bool <OnDocumentRequestPassword>b__511_2() => 
                false;

            internal void <OnDocumentRequestPassword>b__511_3(ISplashScreenService x)
            {
                x.ShowSplashScreen();
            }

            internal bool <OnDocumentSourceChanged>b__506_1(PdfDocumentViewModel x) => 
                x.IsLoaded && x.DocumentState.IsDocumentModified;

            internal bool <OnDocumentSourceChanged>b__506_2() => 
                false;

            internal bool <OnDocumentSourceChanged>b__506_5(PdfDocumentViewModel x) => 
                x.IsLoaded || x.IsLoadingFailed;

            internal bool <OnDocumentSourceChanged>b__506_6() => 
                true;

            internal void <OnZoomFactorChanged>b__503_0(PdfPresenterControl x)
            {
                x.Update();
            }

            internal PdfDocumentStateController <OpenAttachment>b__379_0(PdfDocumentViewModel x) => 
                x.DocumentStateController;

            internal PdfFileAttachment <OpenAttachment>b__379_2(PdfFileAttachmentListItem y) => 
                y.FileAttachment;

            internal void <OpenDocument>b__422_0(InteractionProvider x)
            {
                x.CommitEditor();
            }

            internal void <PerformExceptionMessageAction>b__548_0(Action x)
            {
                x();
            }

            internal bool <PrintInternal>b__461_1(PdfPrintDialogViewModel viewModel) => 
                viewModel.EnableToPrint;

            internal PdfDocumentStateController <RaiseAnnotationChanged>b__545_0(PdfDocumentViewModel x) => 
                x.DocumentStateController;

            internal PdfTextMarkupAnnotationState <RaiseAnnotationChanged>b__545_1(PdfDocumentStateController x) => 
                x.GetSelectedAnnotationState();

            internal PdfTextMarkupAnnotationState <RaiseAnnotationChanged>b__545_2() => 
                null;

            internal PdfDocument <ReleaseDocument>b__505_2(PdfDocumentViewModel x) => 
                x.PdfDocument;

            internal PdfInteractiveForm <ReleaseDocument>b__505_3(PdfDocument x) => 
                x.AcroForm;

            internal PdfDocument <ReleaseDocument>b__505_5(PdfDocumentViewModel x) => 
                x.PdfDocument;

            internal PdfInteractiveForm <ReleaseDocument>b__505_6(PdfDocument x) => 
                x.AcroForm;

            internal void <ReleaseDocument>b__505_8(IDisposable x)
            {
                x.Dispose();
            }

            internal PdfDocumentStateController <SaveAttachment>b__381_0(PdfDocumentViewModel x) => 
                x.DocumentStateController;

            internal PdfFileAttachment <SaveAttachment>b__381_2(PdfFileAttachmentListItem y) => 
                y.FileAttachment;

            internal bool <SaveDocumentInternal>b__413_1(SaveFileDialogService x) => 
                !x.IsPropertySet(FileDialogServiceBase.DefaultFileNameProperty);

            internal bool <SaveDocumentInternal>b__413_2() => 
                false;

            internal bool <SaveDocumentInternal>b__413_4(ISaveFileDialogService x) => 
                x.ShowDialog(null, null);

            internal bool <SaveDocumentInternal>b__413_5() => 
                false;

            internal PdfDocumentStateController <SelectAnnotation>b__482_0(PdfDocumentViewModel x) => 
                x.DocumentStateController;

            internal IPdfDocumentSelectionResults <SetCommentCursorMode>b__401_0(IPdfDocument x) => 
                x.SelectionResults;

            internal bool <SetCommentCursorMode>b__401_1(IPdfDocumentSelectionResults x) => 
                x.ContentType == PdfDocumentContentType.Text;

            internal bool <SetCommentCursorMode>b__401_2() => 
                false;

            internal void <SetCommentCursorMode>b__401_3(PdfCommandProvider x)
            {
                x.UpdateCursorModeCheckState();
            }

            internal void <SetCommentCursorMode>b__401_4(PdfCommandProvider x)
            {
                x.UpdateCursorModeCheckState();
            }

            internal void <SetCursorMode>b__399_0(PdfCommandProvider x)
            {
                x.UpdateCursorModeCheckState();
            }

            internal PdfDocumentStateController <ShowAnnotationProperties>b__409_0(PdfDocumentViewModel x) => 
                x.DocumentStateController;

            internal PdfAnnotationPropertiesFormViewModel <ShowAnnotationProperties>b__409_1(PdfDocumentStateController x) => 
                x.CreateSelectedAnnotationViewModel();

            internal PdfAnnotationPropertiesFormViewModel <ShowAnnotationProperties>b__409_2() => 
                null;

            internal void <ShowAnnotationProperties>b__409_7(PdfThumbnailsViewerSettings x)
            {
                x.RaiseInvalidate();
            }

            internal void <ShowAnnotationProperties>b__409_8(PdfPresenterControl x)
            {
                x.Update();
            }

            internal void <ShowGetPasswordDialog>b__509_0(PdfDocumentViewModel x)
            {
                x.CancelLoadDocument();
            }

            internal PdfDocumentStateController <StrikethroughSelectedText>b__433_0(PdfDocumentViewModel x) => 
                x.DocumentStateController;

            internal void <StrikethroughSelectedText>b__433_1(PdfDocumentStateController x)
            {
                x.AddTextMarkupAnnotation(PdfTextMarkupAnnotationType.StrikeOut);
            }

            internal PdfDocumentStateController <StrikethroughSelectedText>b__436_0(PdfDocumentViewModel x) => 
                x.DocumentStateController;

            internal PdfDocumentStateController <StrikethroughSelectedText>b__439_0(PdfDocumentViewModel x) => 
                x.DocumentStateController;

            internal PdfDocumentStateController <StrikethroughSelectedText>b__442_0(PdfDocumentViewModel x) => 
                x.DocumentStateController;

            internal PdfDocumentStateController <UnderlineSelectedText>b__434_0(PdfDocumentViewModel x) => 
                x.DocumentStateController;

            internal void <UnderlineSelectedText>b__434_1(PdfDocumentStateController x)
            {
                x.AddTextMarkupAnnotation(PdfTextMarkupAnnotationType.Underline);
            }

            internal PdfDocumentStateController <UnderlineSelectedText>b__437_0(PdfDocumentViewModel x) => 
                x.DocumentStateController;

            internal PdfDocumentStateController <UnderlineSelectedText>b__440_0(PdfDocumentViewModel x) => 
                x.DocumentStateController;

            internal PdfDocumentStateController <UnderlineSelectedText>b__443_0(PdfDocumentViewModel x) => 
                x.DocumentStateController;

            internal PdfDocumentStateController <UnselectAll>b__471_0(PdfDocumentViewModel x) => 
                x.DocumentStateController;

            internal void <UnselectAll>b__471_1(PdfDocumentStateController x)
            {
                x.ClearSelection();
            }
        }
    }
}

