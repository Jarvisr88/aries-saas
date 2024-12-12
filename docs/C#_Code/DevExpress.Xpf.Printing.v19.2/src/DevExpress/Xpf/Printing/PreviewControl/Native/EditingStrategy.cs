namespace DevExpress.Xpf.Printing.PreviewControl.Native
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Printing;
    using DevExpress.Xpf.Printing.Native;
    using DevExpress.Xpf.Printing.PreviewControl;
    using DevExpress.Xpf.Printing.PreviewControl.Native.Editing;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Forms;

    public class EditingStrategy : IEditBrickServiceBase
    {
        private bool suppressCheckEditChanged;

        public EditingStrategy(DocumentPresenterControl presenter)
        {
            this.DocumentPresenter = presenter;
        }

        private Rect CalcCellEditorPosition(PageViewModel pageModel, EditingField editingField)
        {
            Rect pageRect = this.DocumentPresenter.NavigationStrategy.GetPageWrapper(pageModel.PageIndex).GetPageRect(pageModel);
            RectangleF ef = PSUnitConverter.DocToPixel(pageModel.Page.GetBrickBounds(editingField.Brick), (float) this.DocumentPresenter.BehaviorProvider.ZoomFactor);
            double scaleX = this.DocumentPresenter.GetScaleX();
            return new Rect(pageRect.Left + (((double) ef.Left) / scaleX), pageRect.Top + (((double) ef.Top) / scaleX), ((double) ef.Width) / scaleX, ((double) ef.Height) / scaleX);
        }

        private void EditCheckBox(CheckEditingField editingField)
        {
            Func<CheckState, CheckState> getNewValue = <>c.<>9__13_0 ??= delegate (CheckState state) {
                if (state == CheckState.Indeterminate)
                {
                    return CheckState.Checked;
                }
                CheckState[] first = new CheckState[2];
                first[1] = CheckState.Checked;
                return first.Concat<CheckState>(CheckState.Unchecked.Yield<CheckState>()).SkipWhile<CheckState>(_ => (_ != state)).Skip<CheckState>(1).First<CheckState>();
            };
            if (!string.IsNullOrEmpty(editingField.GroupID))
            {
                this.EditRadioCheckBox(editingField, getNewValue);
            }
            else
            {
                this.EditCheckBoxCore(editingField, getNewValue);
            }
            this.DocumentPresenter.Update();
        }

        private void EditCheckBoxCore(EditingField editingField, Func<CheckState, CheckState> getNewValue)
        {
            editingField.EditValue = getNewValue((CheckState) editingField.EditValue);
        }

        private void EditRadioCheckBox(CheckEditingField editingField, Func<CheckState, CheckState> getNewValue)
        {
            IEnumerable<CheckEditingField> first = from x in this.DocumentPresenter.Document.EditingFields.OfType<CheckEditingField>()
                where (x.Brick is ICheckBoxBrick) && (x.GroupID == editingField.GroupID)
                select x;
            editingField.EditValue = CheckState.Checked;
            first.Except<CheckEditingField>(editingField.Yield<CheckEditingField>()).ForEach<CheckEditingField>(x => x.EditValue = getNewValue(CheckState.Checked));
        }

        public bool EndEditing()
        {
            if (this.ActiveField == null)
            {
                return true;
            }
            Action<IDocumentPage> action = <>c.<>9__20_2;
            if (<>c.<>9__20_2 == null)
            {
                Action<IDocumentPage> local1 = <>c.<>9__20_2;
                action = <>c.<>9__20_2 = delegate (IDocumentPage x) {
                    x.ForceInvalidate = true;
                };
            }
            this.DocumentPresenter.Document.With<IDocumentViewModel, PageViewModel>(d => d.Pages.SingleOrDefault<PageViewModel>(x => (x.PageIndex == this.ActiveField.PageIndex))).Do<IDocumentPage>(action);
            EditingField activeField = this.ActiveField;
            this.ActiveField = null;
            Func<DocumentInplaceEditorOwner, bool> evaluator = <>c.<>9__20_3;
            if (<>c.<>9__20_3 == null)
            {
                Func<DocumentInplaceEditorOwner, bool> local2 = <>c.<>9__20_3;
                evaluator = <>c.<>9__20_3 = x => x.CurrentCellEditor.CommitEditor(true);
            }
            bool flag = this.DocumentPresenter.ActiveEditorOwner.Return<DocumentInplaceEditorOwner, bool>(evaluator, <>c.<>9__20_4 ??= () => true);
            if (!flag)
            {
                this.ActiveField = activeField;
            }
            this.DocumentPresenter.Update();
            return flag;
        }

        private EditingField GetNextEnabledField(EditingField currentField, bool isForwardDirection)
        {
            EditingField field = currentField;
            EditingFieldCollection editingFields = this.DocumentPresenter.Document.PrintingSystem.EditingFields;
            for (int i = 0; i < editingFields.Count; i++)
            {
                field = isForwardDirection ? this.DocumentPresenter.Document.PrintingSystem.EditingFields.GetNextField(field) : this.DocumentPresenter.Document.PrintingSystem.EditingFields.GetPreviousField(field);
                if (!field.ReadOnly)
                {
                    return field;
                }
            }
            return null;
        }

        public void NavigateToNextField(bool isForwardDirection)
        {
            EditingField nextField = this.GetNextEnabledField(this.ActiveField, isForwardDirection);
            this.suppressCheckEditChanged = true;
            this.StartEditing(this.DocumentPresenter.Document.Pages.Single<PageViewModel>(x => x.PageIndex == nextField.PageIndex), nextField);
        }

        public bool StartEditing(PageViewModel pageModel, EditingField editingField)
        {
            if (!this.DocumentPresenter.ActualDocumentViewer.AllowDocumentEditing)
            {
                return false;
            }
            if (!this.EndEditing())
            {
                return false;
            }
            this.ActiveField = editingField;
            BrickPagePair brickPagePair = BrickPagePair.Create(editingField.BrickIndices, editingField.PageIndex, editingField.PageID, editingField.GetBounds());
            this.DocumentPresenter.NavigationStrategy.ShowBrick(brickPagePair, 2);
            DocumentInplaceEditorOwner owner = new DocumentInplaceEditorOwner(this.DocumentPresenter);
            if ((editingField is CheckEditingField) && !this.suppressCheckEditChanged)
            {
                this.EditCheckBox((CheckEditingField) editingField);
            }
            this.suppressCheckEditChanged = false;
            DocumentInplaceEditorBase base2 = null;
            if (editingField is TextEditingField)
            {
                base2 = new TextInplaceEditor(owner, TextInplaceEditorColumn.Create(this.DocumentPresenter.BehaviorProvider, (TextEditingField) editingField, this.DocumentPresenter.ActualDocumentViewer.EditingFieldTemplateSelector, () => this.DocumentPresenter.GetScaleX()));
            }
            else if (editingField is CheckEditingField)
            {
                base2 = new CheckInplaceEditor(owner, new CheckInplaceEditorColumn((CheckEditingField) editingField));
            }
            else if (editingField is ImageEditingField)
            {
                base2 = new ImageInplaceEditor(owner, ImageInplaceEditorColumn.Create(this.DocumentPresenter.BehaviorProvider, (ImageEditingField) editingField, this.DocumentPresenter.ActualDocumentViewer.EditingFieldTemplateSelector, () => this.DocumentPresenter.GetScaleX()));
            }
            if (base2 == null)
            {
                return false;
            }
            owner.CurrentCellEditor = base2;
            this.DocumentPresenter.AttachEditorToTree(owner, pageModel.PageIndex, () => this.CalcCellEditorPosition(pageModel, editingField), 0.0);
            this.DocumentPresenter.Update();
            base2.ShowEditorIfNotVisible(true);
            return true;
        }

        private DocumentPresenterControl DocumentPresenter { get; set; }

        public EditingField ActiveField { get; private set; }

        EditingField IEditBrickServiceBase.EditingField
        {
            get => 
                this.ActiveField;
            set => 
                this.ActiveField = value;
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly EditingStrategy.<>c <>9 = new EditingStrategy.<>c();
            public static Func<CheckState, CheckState> <>9__13_0;
            public static Action<IDocumentPage> <>9__20_2;
            public static Func<DocumentInplaceEditorOwner, bool> <>9__20_3;
            public static Func<bool> <>9__20_4;

            internal CheckState <EditCheckBox>b__13_0(CheckState state)
            {
                if (state == CheckState.Indeterminate)
                {
                    return CheckState.Checked;
                }
                CheckState[] first = new CheckState[2];
                first[1] = CheckState.Checked;
                return first.Concat<CheckState>(CheckState.Unchecked.Yield<CheckState>()).SkipWhile<CheckState>(_ => (_ != state)).Skip<CheckState>(1).First<CheckState>();
            }

            internal void <EndEditing>b__20_2(IDocumentPage x)
            {
                x.ForceInvalidate = true;
            }

            internal bool <EndEditing>b__20_3(DocumentInplaceEditorOwner x) => 
                x.CurrentCellEditor.CommitEditor(true);

            internal bool <EndEditing>b__20_4() => 
                true;
        }
    }
}

