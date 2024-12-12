namespace DevExpress.Xpf.Printing.PreviewControl.Native.Rendering
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.DocumentViewer;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Printing;
    using DevExpress.Xpf.Printing.PreviewControl;
    using DevExpress.Xpf.Printing.PreviewControl.Native;
    using DevExpress.Xpf.Printing.PreviewControl.Native.Models;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Drawing;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Media;

    public class DocumentPreviewRenderer : DocumentViewerRenderer
    {
        private DevExpress.Xpf.Printing.PreviewControl.Native.Rendering.INativeRendererImpl nativeRenderer;
        private readonly Locker locker;

        public DocumentPreviewRenderer(DevExpress.Xpf.Printing.DocumentPresenterControl presenter) : base(presenter)
        {
            this.locker = new Locker();
            this.UpdateInnerRenderer();
        }

        private GdiPlusImageRenderHook GetGdiPlusImageRenderHook()
        {
            Func<DevExpress.Xpf.Printing.DocumentPresenterControl, DocumentViewModel> evaluator = <>c.<>9__7_0;
            if (<>c.<>9__7_0 == null)
            {
                Func<DevExpress.Xpf.Printing.DocumentPresenterControl, DocumentViewModel> local1 = <>c.<>9__7_0;
                evaluator = <>c.<>9__7_0 = x => x.Document as DocumentViewModel;
            }
            Func<DocumentViewModel, GdiPlusImageRenderHook> func2 = <>c.<>9__7_1;
            if (<>c.<>9__7_1 == null)
            {
                Func<DocumentViewModel, GdiPlusImageRenderHook> local2 = <>c.<>9__7_1;
                func2 = <>c.<>9__7_1 = x => x.PrintingSystem.GetService<GdiPlusImageRenderHook>();
            }
            return this.Presenter.With<DevExpress.Xpf.Printing.DocumentPresenterControl, DocumentViewModel>(evaluator).With<DocumentViewModel, GdiPlusImageRenderHook>(func2);
        }

        public sealed override bool RenderToGraphics(Graphics graphics, INativeImageRenderer renderer, Rect invalidateRect, System.Windows.Size totalSize)
        {
            bool flag = false;
            IDocumentViewModel model = this.Presenter.Document;
            if (this.locker.IsLocked || (this.Presenter.BehaviorProvider == null))
            {
                return false;
            }
            Func<IDocumentViewModel, bool> evaluator = <>c.<>9__6_0;
            if (<>c.<>9__6_0 == null)
            {
                Func<IDocumentViewModel, bool> local1 = <>c.<>9__6_0;
                evaluator = <>c.<>9__6_0 = x => !x.Pages.Any<PageViewModel>();
            }
            if (model.Return<IDocumentViewModel, bool>(evaluator, <>c.<>9__6_1 ??= () => true))
            {
                base.SetRenderMask(new DrawingBrush(new GeometryDrawing()));
                return true;
            }
            RenderedContent content1 = new RenderedContent();
            content1.RenderedPages = this.Presenter.GetDrawingContent();
            content1.SelectionService = this.Presenter.SelectionService;
            RenderedContent renderedContent = content1;
            base.SetRenderMask(this.Presenter.GenerateRenderMask(renderedContent.RenderedPages));
            model.PrintingSystem.ReplaceService<IEditBrickServiceBase>(this.Presenter.EditingStrategy);
            model.GetPaintService().Do<IBrickPaintService>(delegate (IBrickPaintService x) {
                x.EditingFieldsHighlighted = this.Presenter.ActualDocumentViewer.HighlightEditingFields;
                model.PrintingSystem.ReplaceService<IBrickPaintService>(x);
            });
            double scaleX = ScreenHelper.GetScaleX(this.Presenter);
            flag = true;
            PrintingSystemBase actualPS = this.Presenter.Document.PrintingSystem;
            if (!renderedContent.RenderedPages.All<RenderItem>(x => ReferenceEquals(x.Page.Page.Document.PrintingSystem, actualPS)))
            {
                return false;
            }
            bool needToRefresh = false;
            this.locker.DoLockedAction(delegate {
                needToRefresh = this.nativeRenderer.RenderToGraphics(graphics, renderedContent, this.Presenter.BehaviorProvider.ZoomFactor, scaleX, this.Presenter.BehaviorProvider.RotateAngle);
            });
            foreach (RenderItem item in renderedContent.RenderedPages)
            {
                item.Page.NeedsInvalidate = item.NeedsInvalidate;
                item.Page.ForceInvalidate = item.ForceInvalidate;
            }
            if (needToRefresh)
            {
                this.Presenter.ImmediateActionsManager.EnqueueAction(new InvalidateDocumentPreviewRenderingAction(this.Presenter));
            }
            model.GetPaintService().Do<IBrickPaintService>(delegate (IBrickPaintService x) {
                model.PrintingSystem.RemoveService<IBrickPaintService>();
            });
            model.PrintingSystem.RemoveService<IEditBrickServiceBase>();
            return flag;
        }

        public void UpdateInnerRenderer()
        {
            // Unresolved stack state at '000000C9'
        }

        private DevExpress.Xpf.Printing.DocumentPresenterControl Presenter =>
            base.Presenter as DevExpress.Xpf.Printing.DocumentPresenterControl;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DocumentPreviewRenderer.<>c <>9 = new DocumentPreviewRenderer.<>c();
            public static Func<DocumentPreviewControl, bool> <>9__5_0;
            public static Func<bool> <>9__5_1;
            public static Func<DocumentPreviewControl, TextureCache> <>9__5_2;
            public static Func<TextureCache> <>9__5_3;
            public static Func<IDocumentViewModel, bool> <>9__6_0;
            public static Func<bool> <>9__6_1;
            public static Func<DevExpress.Xpf.Printing.DocumentPresenterControl, DocumentViewModel> <>9__7_0;
            public static Func<DocumentViewModel, GdiPlusImageRenderHook> <>9__7_1;

            internal DocumentViewModel <GetGdiPlusImageRenderHook>b__7_0(DevExpress.Xpf.Printing.DocumentPresenterControl x) => 
                x.Document as DocumentViewModel;

            internal GdiPlusImageRenderHook <GetGdiPlusImageRenderHook>b__7_1(DocumentViewModel x) => 
                x.PrintingSystem.GetService<GdiPlusImageRenderHook>();

            internal bool <RenderToGraphics>b__6_0(IDocumentViewModel x) => 
                !x.Pages.Any<PageViewModel>();

            internal bool <RenderToGraphics>b__6_1() => 
                true;

            internal bool <UpdateInnerRenderer>b__5_0(DocumentPreviewControl x) => 
                x.AllowCachePages;

            internal bool <UpdateInnerRenderer>b__5_1() => 
                true;

            internal TextureCache <UpdateInnerRenderer>b__5_2(DocumentPreviewControl x) => 
                x.Cache;

            internal TextureCache <UpdateInnerRenderer>b__5_3() => 
                new TextureCache();
        }
    }
}

