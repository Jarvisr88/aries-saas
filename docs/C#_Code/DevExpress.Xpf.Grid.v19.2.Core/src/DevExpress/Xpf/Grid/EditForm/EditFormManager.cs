namespace DevExpress.Xpf.Grid.EditForm
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Grid;
    using DevExpress.Xpf.Grid.Native;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Input;

    public class EditFormManager : IEditFormManager
    {
        private readonly ITableView view;
        private bool isPopupEditFormVisibleCore;
        private IMessageBoxService messageBoxServiceCore = new DXMessageBoxService();
        private readonly Locker editFormOpenLocker = new Locker();
        private EditFormRowData inplaceEditFormDataCore;
        private int? editFormInplaceRowHandle;
        private bool isEditFormModifiedCore;
        private Locker scrollLocker = new Locker();
        private EditFormCommitter committerCore;

        public EditFormManager(ITableView view)
        {
            if (view == null)
            {
                throw new ArgumentNullException("view");
            }
            this.view = view;
        }

        private void BeforeShowEditForm()
        {
            this.View.ViewBehavior.BeforeShowEditForm();
        }

        private UIUpdateResultWrapper CalcUIUpdateResult() => 
            ((this.InplaceEditFormData == null) || (this.scrollLocker.IsLocked || this.Committer.IsActive)) ? null : (!this.InplaceEditFormData.IsModified ? (this.View.IsNewItemRowFocused ? new UIUpdateResultWrapper(delegate {
                this.HideEditForm();
                return false;
            }) : new UIUpdateResultWrapper(delegate {
                this.CloseActiveInplaceForm();
                return false;
            })) : this.GetModifiedRowResult());

        internal bool CanShowEditForm() => 
            (this.View.DataControl != null) && (!this.IsEditFormVisible && ((this.View.FocusedRowHandle != -2147483648) && (this.View.IsFocusedRowInCurrentPageBounds() && (!this.View.DataControl.IsGroupRowHandleCore(this.View.FocusedRowHandle) && (this.View.FocusedRowHandle != -2147483645)))));

        private void CloseActiveInplaceForm()
        {
            Action<EditFormRowData> closeAction = <>c.<>9__35_0;
            if (<>c.<>9__35_0 == null)
            {
                Action<EditFormRowData> local1 = <>c.<>9__35_0;
                closeAction = <>c.<>9__35_0 = x => x.Close();
            }
            this.CloseActiveInplaceFormCore(closeAction);
        }

        private void CloseActiveInplaceFormCore(Action<EditFormRowData> closeAction)
        {
            if (this.InplaceEditFormData != null)
            {
                closeAction(this.InplaceEditFormData);
            }
        }

        public bool CloseEditForm()
        {
            bool result = false;
            this.CloseActiveInplaceFormCore(delegate (EditFormRowData x) {
                result = x.TryCommit();
            });
            return !result;
        }

        private string CreateDialogTitle() => 
            this.GetBindingValue(this.TableView.EditFormCaptionBinding, this.View.GetRowData(this.View.FocusedRowHandle));

        private EditFormCommitter CreateEditFormCommitter() => 
            new EditFormCommitter(this.View);

        protected internal virtual EditFormRowData CreateEditFormData()
        {
            EditFormRowData data = EditFormRowData.Factory();
            data.EditFormOwner = this.CreateEditFormOwner();
            data.Committer = this.Committer;
            return data;
        }

        internal IEditFormOwner CreateEditFormOwner() => 
            this.View.CreateEditFormOwner();

        private RowData GetActiveInplaceRowData() => 
            (this.editFormInplaceRowHandle == null) ? null : this.View.GetRowData(this.editFormInplaceRowHandle.Value);

        private string GetBindingValue(BindingBase binding, object content)
        {
            if ((binding == null) || (content == null))
            {
                return null;
            }
            BindingBase base2 = BindingCloneHelper.Clone(binding, content);
            if (base2 == null)
            {
                return string.Empty;
            }
            BindingValueEvaluator evaluator = new BindingValueEvaluator(base2);
            return ((evaluator.Value != null) ? evaluator.Value.ToString() : string.Empty);
        }

        private UIUpdateResultWrapper GetCancelDialogResult(MessageResult messageResult) => 
            (messageResult == MessageResult.Yes) ? new UIUpdateResultWrapper(delegate {
                this.HideEditForm();
                return false;
            }) : ((messageResult == MessageResult.No) ? new UIUpdateResultWrapper(<>c.<>9__55_1 ??= () => true) : null);

        public EditFormRowData GetInplaceData(int rowHandle) => 
            ((this.editFormInplaceRowHandle == null) || (this.editFormInplaceRowHandle.Value != rowHandle)) ? null : this.InplaceEditFormData;

        private UIUpdateResultWrapper GetModifiedRowResult()
        {
            switch (this.TableView.EditFormPostConfirmation)
            {
                case PostConfirmationMode.YesNoCancel:
                    return this.GetSaveDialogResult(this.ShowRowChangeMessage(Localize(GridControlStringId.EditForm_Modified), MessageButton.YesNoCancel));

                case PostConfirmationMode.YesNo:
                    return this.GetCancelDialogResult(this.ShowRowChangeMessage(Localize(GridControlStringId.EditForm_Cancel), MessageButton.YesNo));

                case PostConfirmationMode.None:
                    return new UIUpdateResultWrapper(<>c.<>9__53_0 ??= () => true);
            }
            return null;
        }

        private UIUpdateResultWrapper GetSaveDialogResult(MessageResult messageResult)
        {
            switch (messageResult)
            {
                case MessageResult.Cancel:
                    return new UIUpdateResultWrapper(<>c.<>9__54_2 ??= () => true);

                case MessageResult.Yes:
                    return new UIUpdateResultWrapper(() => this.CloseEditForm());

                case MessageResult.No:
                    return new UIUpdateResultWrapper(delegate {
                        this.HideEditForm();
                        return false;
                    });
            }
            return null;
        }

        public void HideEditForm()
        {
            Action<EditFormRowData> closeAction = <>c.<>9__33_0;
            if (<>c.<>9__33_0 == null)
            {
                Action<EditFormRowData> local1 = <>c.<>9__33_0;
                closeAction = <>c.<>9__33_0 = x => x.Cancel();
            }
            this.CloseActiveInplaceFormCore(closeAction);
        }

        public bool IsInlineFormChild(DependencyObject source)
        {
            if ((source == null) || !this.IsInlineEditFormVisible)
            {
                return false;
            }
            Func<RowData, FrameworkElement> evaluator = <>c.<>9__63_0;
            if (<>c.<>9__63_0 == null)
            {
                Func<RowData, FrameworkElement> local1 = <>c.<>9__63_0;
                evaluator = <>c.<>9__63_0 = x => x.RowElement;
            }
            DependencyObject root = this.GetActiveInplaceRowData().With<RowData, FrameworkElement>(evaluator);
            return ((root != null) ? LayoutHelper.IsChildElementEx(root, source, false) : false);
        }

        private static string Localize(GridControlStringId id) => 
            GridControlLocalizer.GetString(id);

        public void OnAfterScroll()
        {
            this.scrollLocker.Unlock();
        }

        public void OnBeforeScroll(int targetRowHandle)
        {
            if ((this.editFormInplaceRowHandle != null) && (this.editFormInplaceRowHandle.Value == targetRowHandle))
            {
                this.scrollLocker.Lock();
            }
        }

        public void OnDoubleClick(MouseButtonEventArgs e)
        {
            if (this.TableView.ShowEditFormOnDoubleClick)
            {
                this.ShowEditForm();
            }
        }

        public void OnInlineFormClosed(bool success)
        {
            if (this.View != null)
            {
                this.View.IsEditing = false;
            }
            if (success && (this.isEditFormModifiedCore && this.Committer.IsLocked))
            {
                int? editFormInplaceRowHandle = this.editFormInplaceRowHandle;
                this.View.OnComittingEditFormValue((editFormInplaceRowHandle != null) ? editFormInplaceRowHandle.GetValueOrDefault() : this.View.FocusedRowHandle);
            }
            this.isEditFormModifiedCore = false;
            this.InplaceEditFormData = null;
            Action<RowData> action = <>c.<>9__37_0;
            if (<>c.<>9__37_0 == null)
            {
                Action<RowData> local1 = <>c.<>9__37_0;
                action = <>c.<>9__37_0 = x => x.UpdateClientInlineEditForm();
            }
            this.GetActiveInplaceRowData().Do<RowData>(action);
            this.editFormInplaceRowHandle = null;
            if (success)
            {
                this.Committer.Commit();
            }
            else
            {
                this.View.CancelRowEdit();
            }
            this.View.ProcessFocusedElement();
        }

        private void OnInplaceEditFormDataChanged()
        {
            Action<ColumnBase> updateColumnDelegate = <>c.<>9__30_0;
            if (<>c.<>9__30_0 == null)
            {
                Action<ColumnBase> local1 = <>c.<>9__30_0;
                updateColumnDelegate = <>c.<>9__30_0 = column => column.UpdateEditFormViewInfo();
            }
            this.View.UpdateColumns(updateColumnDelegate);
            this.View.UpdateCellMergingPanels(false);
            this.UpdateIndicatorState();
        }

        public void OnIsModifiedChanged(bool isModified)
        {
            this.isEditFormModifiedCore = isModified;
            this.UpdateIndicatorState();
        }

        public void OnPreviewKeyDown(KeyEventArgs e)
        {
            Key key = e.Key;
            if ((key == Key.Return) && ModifierKeysHelper.IsCtrlPressed(Keyboard.Modifiers))
            {
                this.CloseEditForm();
            }
            else if (((key == Key.F2) && this.TableView.ShowEditFormOnF2Key) || ((key == Key.Return) && this.TableView.ShowEditFormOnEnterKey))
            {
                this.ShowEditForm();
            }
            else if ((key == Key.Escape) && this.View.IsEditFormVisible)
            {
                this.HideEditForm();
                e.Handled = true;
            }
        }

        public virtual bool RequestUIUpdate()
        {
            UIUpdateResultWrapper wrapper1 = this.CalcUIUpdateResult();
            UIUpdateResultWrapper wrapper2 = wrapper1;
            if (wrapper1 == null)
            {
                UIUpdateResultWrapper local1 = wrapper1;
                Func<bool> executeAction = <>c.<>9__51_0;
                if (<>c.<>9__51_0 == null)
                {
                    Func<bool> local2 = <>c.<>9__51_0;
                    executeAction = <>c.<>9__51_0 = () => false;
                }
                wrapper2 = new UIUpdateResultWrapper(executeAction);
            }
            return !wrapper2.Execute();
        }

        public void ShowDialogEditForm()
        {
            this.ShowEditForm(() => AssignableServiceHelper2<FrameworkElement, IDialogService>.DoServiceAction(this.View.RootView, this.TableView.EditFormDialogServiceTemplate, delegate (IDialogService service) {
                this.IsPopupEditFormVisible = true;
                EditFormRowData data = this.CreateEditFormData();
                data.CanShowUpdateCancelButtons = false;
                MessageBoxResult? defaultButton = null;
                defaultButton = null;
                List<UICommand> dialogCommands = UICommand.GenerateFromMessageBoxButton(MessageBoxButton.OKCancel, new EditFormPopupButtonLocalizer(), defaultButton, defaultButton);
                dialogCommands[0].Command = new DelegateCommand<CancelEventArgs>(x => x.Cancel = !data.TryCommit());
                UICommand command = service.ShowDialog(dialogCommands, this.CreateDialogTitle(), data);
                this.IsPopupEditFormVisible = false;
                if (command == dialogCommands[1])
                {
                    data.Cancel();
                }
            }));
        }

        public void ShowEditForm()
        {
            switch (this.TableView.EditFormShowMode)
            {
                case EditFormShowMode.Dialog:
                    this.ShowDialogEditForm();
                    return;

                case EditFormShowMode.Inline:
                case EditFormShowMode.InlineHideRow:
                    this.ShowInlineEditForm();
                    return;
            }
        }

        private void ShowEditForm(Action showAction)
        {
            this.EditFormOpenLocker.DoLockedAction(delegate {
                if (this.CanShowEditForm())
                {
                    EditFormShowingEventArgs args = new EditFormShowingEventArgs(this.View.FocusedRowHandle);
                    this.TableView.RaiseEditFormShowing(args);
                    if (args.Allow)
                    {
                        this.BeforeShowEditForm();
                        showAction();
                    }
                }
            });
        }

        public void ShowInlineEditForm()
        {
            if (!this.View.IsTopNewItemRowFocused || ((this.TableView.UseLightweightTemplates == null) || this.TableView.UseLightweightTemplates.Value.HasFlag(UseLightweightTemplates.NewItemRow)))
            {
                this.ShowEditForm(delegate {
                    int focusedRowHandle = this.View.FocusedRowHandle;
                    this.editFormInplaceRowHandle = new int?(focusedRowHandle);
                    this.InplaceEditFormData = this.CreateEditFormData();
                    this.InplaceEditFormData.CanShowUpdateCancelButtons = true;
                    RowData activeInplaceRowData = this.GetActiveInplaceRowData();
                    if (activeInplaceRowData != null)
                    {
                        activeInplaceRowData.UpdateClientInlineEditForm();
                        this.View.ScrollIntoView(focusedRowHandle);
                    }
                    if (this.View != null)
                    {
                        this.View.IsEditing = true;
                    }
                });
            }
        }

        private MessageResult ShowRowChangeMessage(string message, MessageButton button) => 
            this.MessageBoxService.ShowMessage(message, Localize(GridControlStringId.EditForm_Warning), button, MessageIcon.None, MessageResult.Yes);

        private void UpdateIndicatorState()
        {
            Action<RowData> action = <>c.<>9__31_0;
            if (<>c.<>9__31_0 == null)
            {
                Action<RowData> local1 = <>c.<>9__31_0;
                action = <>c.<>9__31_0 = x => x.UpdateIndicatorState();
            }
            this.View.GetRowData(this.View.FocusedRowHandle).Do<RowData>(action);
        }

        private bool IsPopupEditFormVisible
        {
            get => 
                this.isPopupEditFormVisibleCore;
            set
            {
                if (this.isPopupEditFormVisibleCore != value)
                {
                    this.isPopupEditFormVisibleCore = value;
                    if (this.View != null)
                    {
                        this.View.IsEditing = value;
                    }
                    this.UpdateIndicatorState();
                }
            }
        }

        protected ITableView TableView =>
            this.view;

        protected DataViewBase View =>
            this.TableView.ViewBase;

        public IMessageBoxService MessageBoxService
        {
            get => 
                this.messageBoxServiceCore;
            set
            {
                if (!ReferenceEquals(this.messageBoxServiceCore, value))
                {
                    this.messageBoxServiceCore = value;
                }
            }
        }

        public Locker EditFormOpenLocker =>
            this.editFormOpenLocker;

        private EditFormRowData InplaceEditFormData
        {
            get => 
                this.inplaceEditFormDataCore;
            set
            {
                if (!ReferenceEquals(this.inplaceEditFormDataCore, value))
                {
                    this.inplaceEditFormDataCore = value;
                    this.OnInplaceEditFormDataChanged();
                }
            }
        }

        private bool IsInlineEditFormVisible =>
            this.InplaceEditFormData != null;

        public bool IsEditFormVisible =>
            this.IsPopupEditFormVisible || this.IsInlineEditFormVisible;

        public bool IsEditFormModified =>
            this.isEditFormModifiedCore;

        public bool AllowEditForm =>
            this.TableView.EditFormShowMode != EditFormShowMode.None;

        private EditFormCommitter Committer
        {
            get
            {
                this.committerCore ??= this.CreateEditFormCommitter();
                return this.committerCore;
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly EditFormManager.<>c <>9 = new EditFormManager.<>c();
            public static Action<ColumnBase> <>9__30_0;
            public static Action<RowData> <>9__31_0;
            public static Action<EditFormRowData> <>9__33_0;
            public static Action<EditFormRowData> <>9__35_0;
            public static Action<RowData> <>9__37_0;
            public static Func<bool> <>9__51_0;
            public static Func<bool> <>9__53_0;
            public static Func<bool> <>9__54_2;
            public static Func<bool> <>9__55_1;
            public static Func<RowData, FrameworkElement> <>9__63_0;

            internal void <CloseActiveInplaceForm>b__35_0(EditFormRowData x)
            {
                x.Close();
            }

            internal bool <GetCancelDialogResult>b__55_1() => 
                true;

            internal bool <GetModifiedRowResult>b__53_0() => 
                true;

            internal bool <GetSaveDialogResult>b__54_2() => 
                true;

            internal void <HideEditForm>b__33_0(EditFormRowData x)
            {
                x.Cancel();
            }

            internal FrameworkElement <IsInlineFormChild>b__63_0(RowData x) => 
                x.RowElement;

            internal void <OnInlineFormClosed>b__37_0(RowData x)
            {
                x.UpdateClientInlineEditForm();
            }

            internal void <OnInplaceEditFormDataChanged>b__30_0(ColumnBase column)
            {
                column.UpdateEditFormViewInfo();
            }

            internal bool <RequestUIUpdate>b__51_0() => 
                false;

            internal void <UpdateIndicatorState>b__31_0(RowData x)
            {
                x.UpdateIndicatorState();
            }
        }

        private class UIUpdateResultWrapper
        {
            private readonly Func<bool> executeAction;

            public UIUpdateResultWrapper(Func<bool> executeAction)
            {
                this.executeAction = executeAction;
            }

            public bool Execute() => 
                (this.executeAction == null) ? false : this.executeAction();
        }
    }
}

