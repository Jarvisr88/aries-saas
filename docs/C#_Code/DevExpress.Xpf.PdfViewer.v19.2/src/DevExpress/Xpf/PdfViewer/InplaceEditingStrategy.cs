namespace DevExpress.Xpf.PdfViewer
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Pdf;
    using DevExpress.Pdf.Drawing;
    using DevExpress.Pdf.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Editors.Helpers;
    using System;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class InplaceEditingStrategy
    {
        private readonly PdfPresenterControl presenter;
        private PdfEditorSettings currentEditorSettings;

        public InplaceEditingStrategy(PdfPresenterControl presenter)
        {
            this.presenter = presenter;
        }

        private Rect CalcCellEditorPosition(PdfPageViewModel page, PdfPageWrapper pageWrapper, PdfDocumentArea area)
        {
            Rect pageRect = pageWrapper.GetPageRect(page);
            Rect rect2 = new Rect(page.GetPoint(area.Area.TopLeft, this.presenter.PdfBehaviorProvider.ZoomFactor, this.presenter.PdfBehaviorProvider.RotateAngle), page.GetPoint(area.Area.BottomRight, this.presenter.PdfBehaviorProvider.ZoomFactor, this.presenter.PdfBehaviorProvider.RotateAngle));
            rect2.Offset(pageRect.X, pageRect.Y);
            return rect2;
        }

        private void EditorOnFontSizeChanged(object sender, EventArgs e)
        {
            Func<PdfPresenterControl, CellEditorOwner> evaluator = <>c.<>9__4_0;
            if (<>c.<>9__4_0 == null)
            {
                Func<PdfPresenterControl, CellEditorOwner> local1 = <>c.<>9__4_0;
                evaluator = <>c.<>9__4_0 = x => x.ActiveEditorOwner;
            }
            Func<CellEditorOwner, CellEditor> func2 = <>c.<>9__4_1;
            if (<>c.<>9__4_1 == null)
            {
                Func<CellEditorOwner, CellEditor> local2 = <>c.<>9__4_1;
                func2 = <>c.<>9__4_1 = x => x.CurrentCellEditor as CellEditor;
            }
            Action<CellEditor> action = <>c.<>9__4_2;
            if (<>c.<>9__4_2 == null)
            {
                Action<CellEditor> local3 = <>c.<>9__4_2;
                action = <>c.<>9__4_2 = x => x.InvalidateEditor();
            }
            this.presenter.With<PdfPresenterControl, CellEditorOwner>(evaluator).With<CellEditorOwner, CellEditor>(func2).Do<CellEditor>(action);
        }

        public void EndEditing()
        {
            Action<CellEditorOwner> action = <>c.<>9__7_0;
            if (<>c.<>9__7_0 == null)
            {
                Action<CellEditorOwner> local1 = <>c.<>9__7_0;
                action = <>c.<>9__7_0 = x => x.CurrentCellEditor.CommitEditor(true);
            }
            this.presenter.ActiveEditorOwner.Do<CellEditorOwner>(action);
            this.presenter.DetachEditorFromTree();
            this.presenter.Focus();
            this.currentEditorSettings.Do<PdfEditorSettings>(delegate (PdfEditorSettings x) {
                x.OnFontSizeChanged -= new EventHandler(this.EditorOnFontSizeChanged);
            });
            this.currentEditorSettings = null;
        }

        public CellEditor GenerateEditor(PdfEditorSettings editorSettings)
        {
            CellEditorOwner owner = new CellEditorOwner(this.presenter);
            CellEditor editor = new CellEditor(owner, null, new CellEditorColumn(this.presenter.PdfBehaviorProvider, (PdfPageViewModel) this.presenter.Document.Pages.ElementAt<IPdfPage>(editorSettings.DocumentArea.PageIndex), editorSettings), editorSettings.FieldName, false);
            owner.CurrentCellEditor = editor;
            PdfDocumentArea documentArea = editorSettings.DocumentArea;
            PdfPageViewModel page = (PdfPageViewModel) this.presenter.Document.Pages.ElementAt<IPdfPage>(documentArea.PageIndex);
            int pageWrapperIndex = this.presenter.NavigationStrategy.PositionCalculator.GetPageWrapperIndex(documentArea.PageIndex);
            ThemeManager.SetThemeName(editor, ThemeHelper.GetEditorThemeName(this.presenter.ActualPdfViewer));
            Rect rect = this.CalcCellEditorPosition(page, (PdfPageWrapper) this.presenter.Pages.ElementAt<PageWrapper>(pageWrapperIndex), documentArea);
            editor.ShowEditorIfNotVisible(true);
            owner.VisualHost.Measure(rect.Size);
            owner.VisualHost.Arrange(new Rect(rect.Size));
            return editor;
        }

        public void StartEditing(PdfEditorSettings editorSettings, IPdfViewerValueEditingCallBack valueEditing)
        {
            CellEditorOwner owner = new CellEditorOwner(this.presenter);
            this.currentEditorSettings.Do<PdfEditorSettings>(delegate (PdfEditorSettings x) {
                x.OnFontSizeChanged -= new EventHandler(this.EditorOnFontSizeChanged);
            });
            this.currentEditorSettings = editorSettings;
            this.currentEditorSettings.Do<PdfEditorSettings>(delegate (PdfEditorSettings x) {
                x.OnFontSizeChanged += new EventHandler(this.EditorOnFontSizeChanged);
            });
            CellEditor editor = new CellEditor(owner, valueEditing, new CellEditorColumn(this.presenter.PdfBehaviorProvider, (PdfPageViewModel) this.presenter.Document.Pages.ElementAt<IPdfPage>(editorSettings.DocumentArea.PageIndex), editorSettings), editorSettings.FieldName, true);
            owner.CurrentCellEditor = editor;
            PdfDocumentArea area = editorSettings.DocumentArea;
            PdfPageViewModel page = (PdfPageViewModel) this.presenter.Document.Pages.ElementAt<IPdfPage>(area.PageIndex);
            int pageWrapperIndex = this.presenter.NavigationStrategy.PositionCalculator.GetPageWrapperIndex(area.PageIndex);
            PdfPageWrapper pageWrapper = (PdfPageWrapper) this.presenter.Pages.ElementAt<PageWrapper>(pageWrapperIndex);
            this.presenter.AttachEditorToTree(owner, area.PageIndex, () => this.CalcCellEditorPosition(page, pageWrapper, area), editorSettings.RotationAngle);
            editor.ShowEditorIfNotVisible(true);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly InplaceEditingStrategy.<>c <>9 = new InplaceEditingStrategy.<>c();
            public static Func<PdfPresenterControl, CellEditorOwner> <>9__4_0;
            public static Func<CellEditorOwner, CellEditor> <>9__4_1;
            public static Action<CellEditor> <>9__4_2;
            public static Action<CellEditorOwner> <>9__7_0;

            internal CellEditorOwner <EditorOnFontSizeChanged>b__4_0(PdfPresenterControl x) => 
                x.ActiveEditorOwner;

            internal CellEditor <EditorOnFontSizeChanged>b__4_1(CellEditorOwner x) => 
                x.CurrentCellEditor as CellEditor;

            internal void <EditorOnFontSizeChanged>b__4_2(CellEditor x)
            {
                x.InvalidateEditor();
            }

            internal void <EndEditing>b__7_0(CellEditorOwner x)
            {
                x.CurrentCellEditor.CommitEditor(true);
            }
        }
    }
}

