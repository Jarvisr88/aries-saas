namespace DevExpress.Xpf.PdfViewer
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Pdf;
    using DevExpress.Pdf.Drawing;
    using DevExpress.Pdf.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Editors.Popups;
    using DevExpress.Xpf.Editors.Settings;
    using DevExpress.Xpf.Editors.Validation;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;

    public class CellEditor : InplaceEditorBase
    {
        private readonly bool raiseEvents;
        private readonly string fieldName;
        private readonly CellEditorOwner owner;
        private readonly CellEditorColumn column;
        private readonly Locker updateValueLocker = new Locker();
        private readonly IPdfViewerValueEditingCallBack dataController;
        private readonly CellEditorEditingProvider editingProvider = new CellEditorEditingProvider();

        public CellEditor(CellEditorOwner owner, IPdfViewerValueEditingCallBack dataController, CellEditorColumn column, string fieldName, bool raiseEvents = true)
        {
            this.fieldName = fieldName;
            this.raiseEvents = raiseEvents;
            this.owner = owner;
            this.column = column;
            this.dataController = dataController;
            DevExpress.Xpf.PdfViewer.CellData data1 = new DevExpress.Xpf.PdfViewer.CellData();
            data1.Value = column.InitialValue;
            this.CellData = data1;
            this.Grid = new System.Windows.Controls.Grid();
            this.Border = new System.Windows.Controls.Border();
            this.Grid.Children.Add(this.Border);
            this.Grid.Children.Add(this);
            owner.VisualHost = this.Grid;
            base.OnOwnerChanged(null);
            base.Loaded += new RoutedEventHandler(this.OnLoaded);
        }

        public override bool CanShowEditor() => 
            base.CanShowEditor() && this.RaiseShowingEditor();

        private void cb_PopupOpened(object sender, RoutedEventArgs e)
        {
            ComboBoxEdit edit = (ComboBoxEdit) sender;
            EditorListBox input = (EditorListBox) LayoutHelper.FindElementByType((PopupBorderControl) edit.GetPopup().Child, typeof(PopupListBox));
            input.Do<EditorListBox>(x => x.Background = this.column.Background);
            input.Do<EditorListBox>(x => x.Foreground = this.column.Foreground);
            edit.GetPopup().Placement = PlacementMode.Bottom;
        }

        private object ConvertToEditableValue(object editableValue)
        {
            if (this.column.EditorType != PdfEditorType.ListBox)
            {
                return editableValue;
            }
            Func<IList, List<string>> evaluator = <>c.<>9__39_0;
            if (<>c.<>9__39_0 == null)
            {
                Func<IList, List<string>> local1 = <>c.<>9__39_0;
                evaluator = <>c.<>9__39_0 = x => x.Cast<string>().ToList<string>();
            }
            return new PdfListBoxEditValue(this.GetTopIndexFromListBox(base.editCore), (editableValue as IList).Return<IList, List<string>>(evaluator, delegate {
                List<string> list1 = new List<string>();
                list1.Add((string) editableValue);
                return list1;
            }));
        }

        protected override IBaseEdit CreateEditor(BaseEditSettings settings)
        {
            IBaseEdit baseEdit = base.CreateEditor(settings);
            this.InitializeBorder();
            this.InitializeComboBoxEdit(baseEdit as ComboBoxEdit);
            this.InitializeListBoxEdit(baseEdit as ListBoxEdit);
            this.InitializeTextEdit(baseEdit as TextEdit);
            this.InitializeIBaseEdit(baseEdit);
            this.InitializeBaseEdit((BaseEdit) baseEdit);
            return baseEdit;
        }

        protected override object GetEditableValue() => 
            this.CellData.Value;

        protected override EditableDataObject GetEditorDataContext() => 
            this.CellData;

        private int GetTopIndexFromListBox(IBaseEdit baseEdit)
        {
            Predicate<FrameworkElement> predicate = <>c.<>9__40_0;
            if (<>c.<>9__40_0 == null)
            {
                Predicate<FrameworkElement> local1 = <>c.<>9__40_0;
                predicate = <>c.<>9__40_0 = x => x is ListBox;
            }
            ListBox input = LayoutHelper.FindElement((FrameworkElement) baseEdit, predicate) as ListBox;
            Func<ListBox, VirtualizingStackPanel> evaluator = <>c.<>9__40_1;
            if (<>c.<>9__40_1 == null)
            {
                Func<ListBox, VirtualizingStackPanel> local2 = <>c.<>9__40_1;
                evaluator = <>c.<>9__40_1 = delegate (ListBox x) {
                    Predicate<FrameworkElement> predicate1 = <>c.<>9__40_2;
                    if (<>c.<>9__40_2 == null)
                    {
                        Predicate<FrameworkElement> local1 = <>c.<>9__40_2;
                        predicate1 = <>c.<>9__40_2 = fe => fe is VirtualizingStackPanel;
                    }
                    return (VirtualizingStackPanel) LayoutHelper.FindElement(x, predicate1);
                };
            }
            PdfOptionsFormFieldOption item = input.With<ListBox, VirtualizingStackPanel>(evaluator).With<VirtualizingStackPanel, PdfOptionsFormFieldOption>(delegate (VirtualizingStackPanel x) {
                Func<FrameworkElement, PdfOptionsFormFieldOption> selector = <>c.<>9__40_4;
                if (<>c.<>9__40_4 == null)
                {
                    Func<FrameworkElement, PdfOptionsFormFieldOption> local1 = <>c.<>9__40_4;
                    selector = <>c.<>9__40_4 = element => element.DataContext as PdfOptionsFormFieldOption;
                }
                return x.Children.Cast<FrameworkElement>().Where<FrameworkElement>(new Func<FrameworkElement, bool>(this.IsItemVisible)).Select<FrameworkElement, PdfOptionsFormFieldOption>(selector).FirstOrDefault<PdfOptionsFormFieldOption>();
            });
            return ((item != null) ? input.ItemsSource.Cast<PdfOptionsFormFieldOption>().ToList<PdfOptionsFormFieldOption>().IndexOf(item) : -1);
        }

        protected override void HideEditorInternal()
        {
            base.HideEditorInternal();
            (base.Content as BaseEdit).Do<BaseEdit>(delegate (BaseEdit x) {
                x.EditValueChanging -= new EditValueChangingEventHandler(this.OnEditValueChanging);
            });
        }

        private void InitializeBaseEdit(BaseEdit baseEdit)
        {
            baseEdit.Margin = this.column.BorderThickness;
            baseEdit.Foreground = this.column.Foreground;
            if (this.column.FontSize > 1.0)
            {
                baseEdit.FontSize = this.column.FontSize;
            }
            if (this.column.FontFamily != null)
            {
                baseEdit.FontFamily = this.column.FontFamily;
            }
            (baseEdit as ComboBoxEdit).Do<ComboBoxEdit>(x => x.Background = this.column.Background);
            baseEdit.EditValueChanging += new EditValueChangingEventHandler(this.OnEditValueChanging);
        }

        private void InitializeBorder()
        {
            this.Border.Background = this.column.Background;
            this.Border.BorderBrush = this.column.BorderBrush;
            this.Border.BorderThickness = this.column.BorderThickness;
            this.Border.CornerRadius = this.column.CornerRadius;
        }

        private void InitializeComboBoxEdit(ComboBoxEdit cb)
        {
            if (cb != null)
            {
                cb.PopupOpened += new RoutedEventHandler(this.cb_PopupOpened);
            }
        }

        private void InitializeIBaseEdit(IBaseEdit baseEdit)
        {
            baseEdit.SetInplaceEditingProvider(this.editingProvider);
        }

        private void InitializeListBoxEdit(ListBoxEdit lb)
        {
            ListBoxEdit edit1 = lb;
        }

        private void InitializeTextEdit(TextEdit textEdit)
        {
            if (textEdit != null)
            {
                if (this.column.IsMultiline)
                {
                    textEdit.VerticalContentAlignment = VerticalAlignment.Top;
                    textEdit.AcceptsReturn = true;
                    textEdit.TextWrapping = TextWrapping.Wrap;
                }
                if (this.column.DoNotScroll)
                {
                    textEdit.HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled;
                    textEdit.VerticalScrollBarVisibility = ScrollBarVisibility.Disabled;
                }
                textEdit.MaxLength = this.column.MaxLength;
            }
        }

        internal void InvalidateEditor()
        {
            if (this.column.FontSize > 1.0)
            {
                (base.Content as BaseEdit).Do<BaseEdit>(x => x.FontSize = this.column.FontSize);
            }
        }

        protected override bool IsInactiveEditorButtonVisible() => 
            false;

        private bool IsItemVisible(FrameworkElement element)
        {
            Panel parent = LayoutHelper.FindLayoutOrVisualParentObject<Panel>(element, false, null);
            Rect relativeElementRect = LayoutHelper.GetRelativeElementRect(element, parent);
            Rect rect = LayoutHelper.GetRelativeElementRect(parent, parent);
            relativeElementRect.Union(rect);
            return relativeElementRect.Height.AreClose(rect.Height);
        }

        protected override void OnEditorActivated()
        {
            base.OnEditorActivated();
            if (this.raiseEvents)
            {
                this.owner.Presenter.ActualPdfViewer.RaiseShownEditorEvent(this.fieldName);
            }
        }

        private void OnEditValueChanging(object sender, EditValueChangingEventArgs e)
        {
            Func<bool> fallback = <>c.<>9__57_1;
            if (<>c.<>9__57_1 == null)
            {
                Func<bool> local1 = <>c.<>9__57_1;
                fallback = <>c.<>9__57_1 = () => false;
            }
            e.IsCancel = (this.column.EditorSettings as PdfTextEditSettings).Return<PdfTextEditSettings, bool>(delegate (PdfTextEditSettings x) {
                string text1;
                object newValue = e.NewValue;
                if (newValue != null)
                {
                    text1 = newValue.ToString();
                }
                else
                {
                    object local1 = newValue;
                    text1 = null;
                }
                string local2 = text1;
                string text2 = local2;
                if (local2 == null)
                {
                    string local3 = local2;
                    text2 = "";
                }
                return !x.OnEditValueChanging(text2);
            }, fallback);
            e.Handled = true;
        }

        protected override void OnHiddenEditor(bool closeEditor)
        {
            base.OnHiddenEditor(closeEditor);
            this.dataController.HideEditor();
            if (this.raiseEvents)
            {
                this.owner.Presenter.ActualPdfViewer.RaiseHiddenEditorEvent(this.fieldName);
            }
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            Action<TextBox> action = <>c.<>9__34_0;
            if (<>c.<>9__34_0 == null)
            {
                Action<TextBox> local1 = <>c.<>9__34_0;
                action = <>c.<>9__34_0 = x => EditorMarginHelper.SetMargin(x, "0,0,0,0,0,0,0,0");
            }
            LayoutHelper.FindElementByType<TextBox>(this).Do<TextBox>(action);
        }

        protected override bool PostEditorCore()
        {
            if (!base.HasAccessToCellValue)
            {
                return true;
            }
            if (this.ValidationError == null)
            {
                object obj2 = this.ConvertToEditableValue(base.EditableValue);
                string str = this.dataController.ValidateEditor(obj2);
                if (string.IsNullOrEmpty(str))
                {
                    this.dataController.PostEditor(obj2);
                    return true;
                }
                this.ValidationError = new BaseValidationError(str);
                BaseEditHelper.SetValidationError((DependencyObject) base.editCore, this.ValidationError);
            }
            return false;
        }

        protected override bool RaiseShowingEditor() => 
            !this.raiseEvents ? base.RaiseShowingEditor() : this.owner.Presenter.ActualPdfViewer.RaiseShowingEditorEvent(this.fieldName);

        protected override void UpdateEditValueCore(IBaseEdit editor)
        {
            this.updateValueLocker.DoLockedAction<object>(delegate {
                object obj2;
                editor.EditValue = obj2 = this.GetEditableValue();
                return obj2;
            });
        }

        public override void ValidateEditorCore()
        {
            base.ValidateEditorCore();
            if ((base.editCore != null) && ((base.Edit != null) && !base.Edit.DoValidate()))
            {
                BaseValidationError validationError = BaseEditHelper.GetValidationError((DependencyObject) base.editCore);
                this.ValidationError = validationError;
            }
        }

        public DevExpress.Xpf.PdfViewer.CellData CellData { get; private set; }

        private BaseValidationError ValidationError { get; set; }

        protected override InplaceEditorOwnerBase Owner =>
            this.owner;

        protected override IInplaceEditorColumn EditorColumn =>
            this.column;

        private System.Windows.Controls.Grid Grid { get; set; }

        private System.Windows.Controls.Border Border { get; set; }

        protected override bool IsCellFocused =>
            true;

        protected override bool IsReadOnly =>
            this.column.IsReadOnly;

        protected override bool OverrideCellTemplate =>
            true;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly CellEditor.<>c <>9 = new CellEditor.<>c();
            public static Action<TextBox> <>9__34_0;
            public static Func<IList, List<string>> <>9__39_0;
            public static Predicate<FrameworkElement> <>9__40_0;
            public static Predicate<FrameworkElement> <>9__40_2;
            public static Func<ListBox, VirtualizingStackPanel> <>9__40_1;
            public static Func<FrameworkElement, PdfOptionsFormFieldOption> <>9__40_4;
            public static Func<bool> <>9__57_1;

            internal List<string> <ConvertToEditableValue>b__39_0(IList x) => 
                x.Cast<string>().ToList<string>();

            internal bool <GetTopIndexFromListBox>b__40_0(FrameworkElement x) => 
                x is ListBox;

            internal VirtualizingStackPanel <GetTopIndexFromListBox>b__40_1(ListBox x)
            {
                Predicate<FrameworkElement> predicate = <>9__40_2;
                if (<>9__40_2 == null)
                {
                    Predicate<FrameworkElement> local1 = <>9__40_2;
                    predicate = <>9__40_2 = fe => fe is VirtualizingStackPanel;
                }
                return (VirtualizingStackPanel) LayoutHelper.FindElement(x, predicate);
            }

            internal bool <GetTopIndexFromListBox>b__40_2(FrameworkElement fe) => 
                fe is VirtualizingStackPanel;

            internal PdfOptionsFormFieldOption <GetTopIndexFromListBox>b__40_4(FrameworkElement element) => 
                element.DataContext as PdfOptionsFormFieldOption;

            internal bool <OnEditValueChanging>b__57_1() => 
                false;

            internal void <OnLoaded>b__34_0(TextBox x)
            {
                EditorMarginHelper.SetMargin(x, "0,0,0,0,0,0,0,0");
            }
        }
    }
}

