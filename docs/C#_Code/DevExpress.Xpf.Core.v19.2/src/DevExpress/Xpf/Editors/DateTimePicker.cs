namespace DevExpress.Xpf.Editors
{
    using DevExpress.Data.Mask;
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors.Validation.Native;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Windows.Controls;
    using System.Windows.Input;

    public class DateTimePicker : DateEditCalendarBase
    {
        public event EventHandler<DateTimeChangedEventArgs> DateTimeChanged;

        public DateTimePicker()
        {
            base.DefaultStyleKey = typeof(DateTimePicker);
            this.UpdateValueOnAnimationCompletedLocker = new Locker();
            this.UpdateValueOnAnimationCompletedLocker.Unlocked += new EventHandler(this.UpdateValueOnAnimationCompleted);
            this.MaskManagerEditTextChangedLocker = new Locker();
            this.OnPreviewTextInputLocker = new Locker();
            this.IsItemsControlInitialized = false;
            this.ItemsControlInitializeAction = new PostponedAction(() => ReferenceEquals(this.ItemsControl, null));
            this.SetDateTimeLocker = new Locker();
        }

        private DateTimePickerData CreateDateTimePickerData(DateTimeElementEditor editor, DateTimeMaskFormatElementEditable format)
        {
            DateTimePickerData data1 = new DateTimePickerData();
            data1.Text = editor.DisplayText;
            data1.Value = this.GetCurrentValueFromMaskManager();
            data1.DateTimePart = this.GetDateTimePartFromEditor(format);
            return data1;
        }

        protected virtual DateTimeMaskManager CreateMaskManager(string mask) => 
            new DateTimeMaskManager(mask, true, CultureInfo.CurrentCulture, false);

        private void FixDateTimeByMask()
        {
            DateTimeMaskManager manager = this.CreateMaskManager("yy");
            int year = DateTime.MaxValue.Year;
            int num2 = DateTime.MinValue.Year;
            manager.SetInitialEditValue(base.DateTime);
            for (int i = 0; i < 0x63; i++)
            {
                manager.Insert(i.ToString("D2"));
                DateTime currentEditValue = (DateTime) manager.GetCurrentEditValue();
                int num4 = currentEditValue.Year;
                if (num4 < year)
                {
                    year = num4;
                }
                if (num4 > num2)
                {
                    num2 = num4;
                }
            }
            if (base.DateTime.Year < year)
            {
                base.DateTime = new DateTime(year, base.DateTime.Month, base.DateTime.Day, base.DateTime.Hour, base.DateTime.Minute, base.DateTime.Second, base.DateTime.Millisecond, base.DateTime.Kind);
            }
            else if (base.DateTime.Year > num2)
            {
                base.DateTime = new DateTime(num2, base.DateTime.Month, base.DateTime.Day, base.DateTime.Hour, base.DateTime.Minute, base.DateTime.Second, base.DateTime.Millisecond, base.DateTime.Kind);
            }
        }

        private void GeneratePickers()
        {
            this.Pickers = new ObservableCollection<DateTimePickerPart>();
            Func<DateTimeMaskFormatElement, bool> predicate = <>c.<>9__70_0;
            if (<>c.<>9__70_0 == null)
            {
                Func<DateTimeMaskFormatElement, bool> local1 = <>c.<>9__70_0;
                predicate = <>c.<>9__70_0 = format => format.Editable;
            }
            foreach (DateTimeMaskFormatElement element in this.Formats.Where<DateTimeMaskFormatElement>(predicate))
            {
                DateTimePickerPart item = new DateTimePickerPart {
                    IsEnabled = (this.OwnerDateEdit == null) || !this.OwnerDateEdit.IsReadOnly
                };
                item.AnimatedChanged += new EventHandler<AnimatedChangedEventArgs>(this.PickerAnimatedChanged);
                item.ExpandedChanged += new EventHandler<ExpandedChangedEventArgs>(this.PickerExpandedChanged);
                this.Pickers.Add(item);
            }
        }

        private DateTime GetCurrentValueFromMaskManager()
        {
            this.MaskManager.FlushPendingEditActions();
            return (DateTime) this.MaskManager.GetCurrentEditValue();
        }

        private int GetCursorPosition()
        {
            int num3;
            if (this.MaskManagerEditTextChangedLocker.IsLocked)
            {
                this.IndexMaskManager.SetInitialEditValue(this.MaskManager.GetCurrentEditValue());
                this.MaskManagerEditTextChangedLocker.Unlock();
            }
            int displaySelectionStart = this.MaskManager.DisplaySelectionStart;
            int num2 = 0;
            this.IndexMaskManager.CursorHome(false);
            Func<DateTimeMaskFormatElement, bool> predicate = <>c.<>9__84_0;
            if (<>c.<>9__84_0 == null)
            {
                Func<DateTimeMaskFormatElement, bool> local1 = <>c.<>9__84_0;
                predicate = <>c.<>9__84_0 = x => x.Editable;
            }
            using (IEnumerator<DateTimeMaskFormatElement> enumerator = this.Formats.Where<DateTimeMaskFormatElement>(predicate).GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        DateTimeMaskFormatElement current = enumerator.Current;
                        if (this.IndexMaskManager.DisplaySelectionStart != displaySelectionStart)
                        {
                            this.IndexMaskManager.CursorRight(false);
                            num2++;
                            continue;
                        }
                        num3 = num2;
                    }
                    else
                    {
                        return -1;
                    }
                    break;
                }
            }
            return num3;
        }

        private DevExpress.Xpf.Editors.DateTimePart GetDateTimePartFromEditor(DateTimeMaskFormatElementEditable format) => 
            !(format is DateTimeMaskFormatElement_Year) ? (!(format is DateTimeMaskFormatElement_Month) ? (!(format is DateTimeMaskFormatElement_d) ? (!(format is DateTimeMaskFormatElement_H24) ? (!(format is DateTimeMaskFormatElement_h12) ? (!(format is DateTimeMaskFormatElement_Min) ? (!(format is DateTimeMaskFormatElement_s) ? (!(format is DateTimeMaskFormatElement_Millisecond) ? (!(format is DateTimeMaskFormatElement_AmPm) ? DevExpress.Xpf.Editors.DateTimePart.None : DevExpress.Xpf.Editors.DateTimePart.AmPm) : DevExpress.Xpf.Editors.DateTimePart.Millisecond) : DevExpress.Xpf.Editors.DateTimePart.Second) : DevExpress.Xpf.Editors.DateTimePart.Minute) : DevExpress.Xpf.Editors.DateTimePart.Hour12) : DevExpress.Xpf.Editors.DateTimePart.Hour24) : DevExpress.Xpf.Editors.DateTimePart.Day) : DevExpress.Xpf.Editors.DateTimePart.Month) : DevExpress.Xpf.Editors.DateTimePart.Year;

        private void GetSelectionRangeByIndex(int index, out int selectionStart, out int selectionLength)
        {
            if (this.GetCursorPosition() == index)
            {
                selectionStart = this.MaskManager.DisplaySelectionStart;
                selectionLength = this.MaskManager.DisplaySelectionLength;
            }
            else
            {
                int num = index;
                this.IndexMaskManager.SetInitialEditValue(this.MaskManager.GetCurrentEditValue());
                Func<DateTimeMaskFormatElement, bool> predicate = <>c.<>9__59_0;
                if (<>c.<>9__59_0 == null)
                {
                    Func<DateTimeMaskFormatElement, bool> local1 = <>c.<>9__59_0;
                    predicate = <>c.<>9__59_0 = x => x.Editable;
                }
                DateTimeMaskFormatElement element = this.Formats.Where<DateTimeMaskFormatElement>(predicate).ElementAt<DateTimeMaskFormatElement>(index);
                this.IndexMaskManager.CursorHome(false);
                while (num-- > 0)
                {
                    this.IndexMaskManager.CursorRight(false);
                }
                selectionStart = this.IndexMaskManager.DisplaySelectionStart;
                selectionLength = this.IndexMaskManager.DisplaySelectionLength;
            }
        }

        private void Initialize()
        {
            this.UpdateMaskManager();
            this.ResetPickers();
            this.IsItemsControlInitialized = false;
            this.ItemsControlInitializeAction.PerformPostpone(new Action(this.InitializeItemsControl));
        }

        private void InitializeItemsControl()
        {
            if (this.ItemsControl != null)
            {
                int formatterIndex = 0;
                bool flag = false;
                Func<DateTimeMaskFormatElement, bool> predicate = <>c.<>9__66_0;
                if (<>c.<>9__66_0 == null)
                {
                    Func<DateTimeMaskFormatElement, bool> local1 = <>c.<>9__66_0;
                    predicate = <>c.<>9__66_0 = x => x.Editable;
                }
                foreach (DateTimeMaskFormatElement element in this.Formats.Where<DateTimeMaskFormatElement>(predicate))
                {
                    this.MaskManager.SetInitialEditValue(base.DateTime);
                    DateTimeMaskFormatElementEditable format = element as DateTimeMaskFormatElementEditable;
                    if (format != null)
                    {
                        DateTimeElementEditor editor = format.CreateElementEditor((DateTime) this.MaskManager.GetCurrentEditValue());
                        if (editor != null)
                        {
                            this.UpdatePickerPartFromElementEditor(this.Pickers[formatterIndex], editor, format, formatterIndex);
                            flag = flag || format.Mask.Equals("yy");
                            formatterIndex++;
                        }
                    }
                }
                this.MaskManager.SetInitialEditValue(base.DateTime);
                this.ItemsControl.ItemsSource = this.Pickers;
                this.IsItemsControlInitialized = true;
                if (flag)
                {
                    this.FixDateTimeByMask();
                }
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            if (this.OwnerDateEdit != null)
            {
                this.OwnerDateEdit.OnApplyCalendarTemplate(this);
            }
            this.ItemsControl = base.GetTemplateChild("ItemsControl") as System.Windows.Controls.ItemsControl;
            this.ItemsControlInitializeAction.Perform();
        }

        protected override void OnDateTimeChanged()
        {
            if (base.IsInitialized)
            {
                if ((this.OwnerDateEdit != null) && ((this.OwnerDateEdit.PropertyProvider.GetPopupFooterButtons() != PopupFooterButtons.OkCancel) && this.SetDateTimeLocker.IsLocked))
                {
                    this.OwnerDateEdit.SetDateTime(base.DateTime, UpdateEditorSource.ValueChanging);
                }
                this.RaiseDateTimeChanged(base.DateTime);
                this.UpdateDaysCount();
                if (this.SelectedPicker != null)
                {
                    this.SetCursorPosition(this.Pickers.IndexOf(this.SelectedPicker));
                }
                this.UpdateSelectedItems();
                this.OnPreviewTextInputLocker.Unlock();
            }
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            this.Initialize();
        }

        protected override void OnMaskChanged(string newValue)
        {
            this.Initialize();
        }

        protected virtual void OnMaskManagerEditTextChanged(object sender, EventArgs e)
        {
            if (this.IsItemsControlInitialized && (this.OnPreviewTextInputLocker.IsLocked && !this.MaskManagerEditTextChangedLocker.IsLocked))
            {
                this.MaskManagerEditTextChangedLocker.Lock();
            }
        }

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            base.OnPreviewKeyDown(e);
            int index = this.Pickers.IndexOf(this.SelectedPicker);
            Func<DateTimePickerPart, int> evaluator = <>c.<>9__64_0;
            if (<>c.<>9__64_0 == null)
            {
                Func<DateTimePickerPart, int> local1 = <>c.<>9__64_0;
                evaluator = <>c.<>9__64_0 = x => x.VisibleItemsCount / 2;
            }
            int count = this.SelectedPicker.Return<DateTimePickerPart, int>(evaluator, <>c.<>9__64_1 ??= () => 0);
            DateTimePickerSelector selector = null;
            if (index >= 0)
            {
                int items = -1;
                selector = (DateTimePickerSelector) LayoutHelper.FindElement(this.ItemsControl, delegate (FrameworkElement x) {
                    if (x is DateTimePickerSelector)
                    {
                        items++;
                    }
                    return (items == index) && (x is DateTimePickerSelector);
                });
            }
            switch (e.Key)
            {
                case Key.Prior:
                    if (selector == null)
                    {
                        break;
                    }
                    selector.Spin(-count);
                    e.Handled = true;
                    return;

                case Key.Next:
                    if (selector == null)
                    {
                        break;
                    }
                    selector.Spin(count);
                    e.Handled = true;
                    return;

                case Key.End:
                    if (selector != null)
                    {
                        selector.SpinToIndex(selector.GetItemsCount() - 1);
                        e.Handled = true;
                    }
                    break;

                case Key.Home:
                    if (selector == null)
                    {
                        break;
                    }
                    selector.SpinToIndex(0);
                    e.Handled = true;
                    return;

                case Key.Left:
                    if (index == -1)
                    {
                        this.SelectedPicker = this.Pickers.FirstOrDefault<DateTimePickerPart>();
                        this.SelectedPicker.IsExpanded = true;
                        e.Handled = true;
                        return;
                    }
                    index = (index == 0) ? (this.Pickers.Count - 1) : (index - 1);
                    this.SelectPicker(index);
                    e.Handled = true;
                    return;

                case Key.Up:
                    if (selector == null)
                    {
                        break;
                    }
                    selector.Spin(-1);
                    e.Handled = true;
                    return;

                case Key.Right:
                    if (index == -1)
                    {
                        this.SelectedPicker = this.Pickers.FirstOrDefault<DateTimePickerPart>();
                        this.SelectedPicker.IsExpanded = true;
                        e.Handled = true;
                        return;
                    }
                    index = (index == (this.Pickers.Count - 1)) ? 0 : (index + 1);
                    this.SelectPicker(index);
                    e.Handled = true;
                    return;

                case Key.Down:
                    if (selector == null)
                    {
                        break;
                    }
                    selector.Spin(1);
                    e.Handled = true;
                    return;

                default:
                    return;
            }
        }

        protected override void OnPreviewTextInput(TextCompositionEventArgs e)
        {
            if ((this.SelectedPicker != null) && ((this.OwnerDateEdit == null) || !this.OwnerDateEdit.IsReadOnly))
            {
                Action<DateTimePickerPart> action = <>c.<>9__57_0;
                if (<>c.<>9__57_0 == null)
                {
                    Action<DateTimePickerPart> local1 = <>c.<>9__57_0;
                    action = <>c.<>9__57_0 = x => x.UseTransitions = false;
                }
                this.Pickers.ForEach<DateTimePickerPart>(action);
                if (!this.OnPreviewTextInputLocker.IsLocked)
                {
                    this.OnPreviewTextInputLocker.Lock();
                }
                int cursorPosition = this.GetCursorPosition();
                this.MaskManager.Insert(e.Text);
                int index = this.GetCursorPosition();
                DateTime currentEditValue = (DateTime) this.MaskManager.GetCurrentEditValue();
                this.UpdatePickerOnInput(cursorPosition);
                this.SelectPicker(index);
                if (currentEditValue != base.DateTime)
                {
                    this.SetDateTime(this.GetCurrentValueFromMaskManager());
                }
                base.OnPreviewTextInput(e);
            }
        }

        private void PickerAnimatedChanged(object sender, AnimatedChangedEventArgs e)
        {
            DateTimePickerPart item = (DateTimePickerPart) sender;
            DateTimePickerData selectedItem = item.SelectedItem;
            int index = this.Pickers.IndexOf(item);
            if (e.IsAnimated)
            {
                Action<DateTimePickerPart> action = <>c.<>9__79_0;
                if (<>c.<>9__79_0 == null)
                {
                    Action<DateTimePickerPart> local1 = <>c.<>9__79_0;
                    action = <>c.<>9__79_0 = x => x.UseTransitions = false;
                }
                this.Pickers.ForEach<DateTimePickerPart>(action);
            }
            if (e.IsAnimated)
            {
                this.SelectedPicker = item;
                foreach (DateTimePickerPart part3 in this.Pickers)
                {
                    if (!part3.IsAnimated && !Equals(this.SelectedPicker, part3))
                    {
                        part3.IsExpanded = false;
                    }
                }
                item.IsExpanded = true;
                this.UpdateValueOnAnimationCompletedLocker.Lock();
            }
            else
            {
                this.SetCursorPosition(index);
                if ((this.OwnerDateEdit == null) || !this.OwnerDateEdit.IsReadOnly)
                {
                    if ((selectedItem.DateTimePart == DevExpress.Xpf.Editors.DateTimePart.AmPm) && !selectedItem.Value.Equals(base.DateTime))
                    {
                        this.MaskManager.SpinUp();
                    }
                    else
                    {
                        this.MaskManager.Insert(selectedItem.Text);
                    }
                    this.MaskManager.FlushPendingEditActions();
                }
                foreach (DateTimePickerPart part2 in this.Pickers)
                {
                    if (!part2.IsAnimated && !Equals(this.SelectedPicker, part2))
                    {
                        part2.IsExpanded = false;
                    }
                }
                this.UpdateValueOnAnimationCompletedLocker.Unlock();
            }
        }

        private void PickerExpandedChanged(object sender, ExpandedChangedEventArgs e)
        {
            DateTimePickerPart pickerPart = (DateTimePickerPart) sender;
            Action<DateTimePickerPart> action = <>c.<>9__78_1;
            if (<>c.<>9__78_1 == null)
            {
                Action<DateTimePickerPart> local1 = <>c.<>9__78_1;
                action = <>c.<>9__78_1 = x => x.UseTransitions = true;
            }
            (from x in this.Pickers
                where !ReferenceEquals(x, pickerPart)
                select x).ForEach<DateTimePickerPart>(action);
            if (e.IsExpanded)
            {
                foreach (DateTimePickerPart part in this.Pickers)
                {
                    if (part.IsExpanded && !Equals(part, pickerPart))
                    {
                        part.IsExpanded = false;
                    }
                }
                this.SelectedPicker = pickerPart;
                this.SetDateTime(this.GetCurrentValueFromMaskManager());
                this.SetCursorPosition(this.Pickers.IndexOf(this.SelectedPicker));
            }
        }

        protected internal override bool ProcessKeyDown(KeyEventArgs e)
        {
            this.OnPreviewKeyDown(e);
            return true;
        }

        private void RaiseDateTimeChanged(DateTime value)
        {
            if (this.DateTimeChanged != null)
            {
                this.DateTimeChanged(this, new DateTimeChangedEventArgs(value));
            }
        }

        private void ResetPickers()
        {
            if (this.Pickers != null)
            {
                foreach (DateTimePickerPart part in this.Pickers)
                {
                    part.IsEnabled = (this.OwnerDateEdit == null) || !this.OwnerDateEdit.IsReadOnly;
                    part.AnimatedChanged -= new EventHandler<AnimatedChangedEventArgs>(this.PickerAnimatedChanged);
                    part.ExpandedChanged -= new EventHandler<ExpandedChangedEventArgs>(this.PickerExpandedChanged);
                }
                this.Pickers = null;
            }
            this.GeneratePickers();
        }

        private void SelectPicker(int index)
        {
            if ((this.SelectedPicker == null) || (index != this.Pickers.IndexOf(this.SelectedPicker)))
            {
                Action<DateTimePickerPart> action = <>c.<>9__85_0;
                if (<>c.<>9__85_0 == null)
                {
                    Action<DateTimePickerPart> local1 = <>c.<>9__85_0;
                    action = <>c.<>9__85_0 = x => x.IsExpanded = false;
                }
                this.SelectedPicker.Do<DateTimePickerPart>(action);
                this.SelectedPicker = this.Pickers[index];
                this.SelectedPicker.IsExpanded = true;
            }
        }

        private void SetCursorPosition(int rightClickCount)
        {
            this.MaskManager.CursorHome(false);
            for (int i = 0; i < rightClickCount; i++)
            {
                this.MaskManager.CursorRight(false);
            }
        }

        internal void SetDateTime(DateTime dateTime)
        {
            if ((this.OwnerDateEdit == null) || ((this.OwnerDateEdit != null) && !this.OwnerDateEdit.AllowRoundOutOfRangeValue))
            {
                DateTime? minValue;
                DateTime time;
                if (base.MinValue != null)
                {
                    time = dateTime;
                    minValue = base.MinValue;
                    if ((minValue != null) ? (time < minValue.GetValueOrDefault()) : false)
                    {
                        dateTime = base.MinValue.Value;
                    }
                }
                if (base.MaxValue != null)
                {
                    time = dateTime;
                    minValue = base.MaxValue;
                    if ((minValue != null) ? (time > minValue.GetValueOrDefault()) : false)
                    {
                        dateTime = base.MaxValue.Value;
                    }
                }
            }
            this.SetDateTimeLocker.DoLockedAction<DateTime>(delegate {
                DateTime time;
                this.DateTime = time = dateTime;
                return time;
            });
            if ((this.OwnerDateEdit != null) && ((this.OwnerDateEdit.EditValue == null) && (this.OwnerDateEdit.PropertyProvider.GetPopupFooterButtons() != PopupFooterButtons.OkCancel)))
            {
                this.OwnerDateEdit.SetDateTime(dateTime, UpdateEditorSource.ValueChanging);
            }
            this.MaskManager.SetInitialEditValue(base.DateTime);
            this.IndexMaskManager.SetInitialEditValue(base.DateTime);
        }

        private void UpdateAmPmEditor(DateTimePickerPart part, ObservableCollection<DateTimePickerData> list)
        {
            if (base.DateTime.Hour >= 12)
            {
                DateTimePickerData item = new DateTimePickerData();
                item.DateTimePart = DevExpress.Xpf.Editors.DateTimePart.AmPm;
                item.Text = base.DateTime.AddHours(-12.0).ToString("tt");
                item.Value = base.DateTime.AddHours(-12.0);
                list.Add(item);
                DateTimePickerData data2 = new DateTimePickerData();
                data2.DateTimePart = DevExpress.Xpf.Editors.DateTimePart.AmPm;
                data2.Text = base.DateTime.ToString("tt");
                data2.Value = base.DateTime;
                list.Add(data2);
            }
            else
            {
                DateTimePickerData item = new DateTimePickerData();
                item.DateTimePart = DevExpress.Xpf.Editors.DateTimePart.AmPm;
                item.Text = base.DateTime.ToString("tt");
                item.Value = base.DateTime;
                list.Add(item);
                DateTimePickerData data4 = new DateTimePickerData();
                data4.DateTimePart = DevExpress.Xpf.Editors.DateTimePart.AmPm;
                data4.Text = base.DateTime.AddHours(12.0).ToString("tt");
                data4.Value = base.DateTime.AddHours(12.0);
                list.Add(data4);
            }
            part.IsLooped = false;
        }

        private void UpdateDaysCount()
        {
            if (this.IsItemsControlInitialized)
            {
                int formatterIndex = 0;
                Func<DateTimeMaskFormatElement, bool> predicate = <>c.<>9__68_0;
                if (<>c.<>9__68_0 == null)
                {
                    Func<DateTimeMaskFormatElement, bool> local1 = <>c.<>9__68_0;
                    predicate = <>c.<>9__68_0 = x => x.Editable;
                }
                foreach (DateTimeMaskFormatElement element in this.Formats.Where<DateTimeMaskFormatElement>(predicate))
                {
                    this.MaskManager.SetInitialEditValue(base.DateTime);
                    DateTimeMaskFormatElementEditable format = element as DateTimeMaskFormatElementEditable;
                    if (format != null)
                    {
                        DateTimeElementEditor editor = format.CreateElementEditor((DateTime) this.MaskManager.GetCurrentEditValue());
                        if (editor != null)
                        {
                            if (this.GetDateTimePartFromEditor(format) == DevExpress.Xpf.Editors.DateTimePart.Day)
                            {
                                this.UpdatePickerPartFromElementEditor(this.Pickers[formatterIndex], editor, format, formatterIndex);
                            }
                            formatterIndex++;
                        }
                    }
                }
                this.MaskManager.SetInitialEditValue(base.DateTime);
            }
        }

        private void UpdateEditor(DateTimePickerPart part, DateTimeElementEditor editor, DateTimeMaskFormatElementEditable format, int formatterIndex, ObservableCollection<DateTimePickerData> list)
        {
            if (editor is DateTimeNumericRangeElementEditor)
            {
                this.UpdateNumericRangeEditor(part, editor, format, formatterIndex, list);
            }
            else if (editor is DateTimeElementEditorAmPm)
            {
                this.UpdateAmPmEditor(part, list);
            }
            else
            {
                this.UpdateOtherEditors(part, editor, format, formatterIndex, list);
            }
        }

        private void UpdateMaskManager()
        {
            if (this.MaskManager != null)
            {
                this.MaskManager.EditTextChanged -= new EventHandler(this.OnMaskManagerEditTextChanged);
            }
            this.MaskManager = this.CreateMaskManager(base.Mask);
            this.IndexMaskManager = this.CreateMaskManager(base.Mask);
            this.MaskManager.SetInitialEditValue(base.DateTime);
            this.IndexMaskManager.SetInitialEditValue(base.DateTime);
            this.MaskManager.EditTextChanged += new EventHandler(this.OnMaskManagerEditTextChanged);
        }

        private void UpdateNumericRangeEditor(DateTimePickerPart part, DateTimeElementEditor editor, DateTimeMaskFormatElementEditable format, int formatterIndex, ObservableCollection<DateTimePickerData> list)
        {
            IList<int> list2 = DateTimeHelper.GetAvailableValuesByMask(format.Mask, base.DateTime, DateTime.MinValue, DateTime.MaxValue);
            string str = (format.Mask.Length > 3) ? "D4" : "D2";
            foreach (int num in list2)
            {
                string insertion = num.ToString(str);
                this.MaskManager.SetInitialEditValue(base.DateTime);
                this.SetCursorPosition(formatterIndex);
                this.MaskManager.Insert(insertion);
                editor.Insert(insertion);
                list.Add(this.CreateDateTimePickerData(editor, format));
            }
            editor.Insert(DateTimeHelper.GetValueByMask(format.Mask, base.DateTime).ToString(str));
            part.IsLooped = true;
        }

        private void UpdateOtherEditors(DateTimePickerPart part, DateTimeElementEditor editor, DateTimeMaskFormatElementEditable format, int formatterIndex, ObservableCollection<DateTimePickerData> list)
        {
            while (list.Count<DateTimePickerData>(item => (item.Text == editor.DisplayText)) == 0)
            {
                this.SetCursorPosition(formatterIndex);
                list.Add(this.CreateDateTimePickerData(editor, format));
                editor.SpinUp();
            }
            part.IsLooped = true;
        }

        private void UpdatePickerOnInput(int pickerIndex)
        {
            int intValue;
            int num;
            int num2;
            this.GetSelectionRangeByIndex(pickerIndex, out num, out num2);
            string pickerValue = this.MaskManager.DisplayText.Substring(num, num2);
            DateTimePickerPart part = this.Pickers[pickerIndex];
            DateTimePickerData data = null;
            data = !int.TryParse(pickerValue, out intValue) ? part.Items.SingleOrDefault<DateTimePickerData>(x => x.Text.Equals(pickerValue)) : part.Items.SingleOrDefault<DateTimePickerData>(delegate (DateTimePickerData x) {
                int num;
                return (int.TryParse(x.Text, out num) && (num == intValue));
            });
            if (data != null)
            {
                part.SelectedItem = data;
            }
        }

        private void UpdatePickerPartFromElementEditor(DateTimePickerPart part, DateTimeElementEditor editor, DateTimeMaskFormatElementEditable format, int formatterIndex)
        {
            ObservableCollection<DateTimePickerData> list = new ObservableCollection<DateTimePickerData>();
            this.UpdateEditor(part, editor, format, formatterIndex, list);
            Func<DateTimePickerData, DateTime> keySelector = <>c.<>9__71_0;
            if (<>c.<>9__71_0 == null)
            {
                Func<DateTimePickerData, DateTime> local1 = <>c.<>9__71_0;
                keySelector = <>c.<>9__71_0 = data => data.Value;
            }
            part.Items = list.OrderBy<DateTimePickerData, DateTime>(keySelector).ToList<DateTimePickerData>();
            part.SelectedItem = list.FirstOrDefault<DateTimePickerData>(data => data.Value == base.DateTime);
            part.IsExpanded = ReferenceEquals(part, this.SelectedPicker);
        }

        private void UpdateSelectedItems()
        {
            if (this.IsItemsControlInitialized)
            {
                int num = 0;
                Func<DateTimeMaskFormatElement, bool> predicate = <>c.<>9__53_0;
                if (<>c.<>9__53_0 == null)
                {
                    Func<DateTimeMaskFormatElement, bool> local1 = <>c.<>9__53_0;
                    predicate = <>c.<>9__53_0 = x => x.Editable;
                }
                foreach (DateTimeMaskFormatElement element in this.Formats.Where<DateTimeMaskFormatElement>(predicate))
                {
                    DateTimeMaskFormatElementEditable format = element as DateTimeMaskFormatElementEditable;
                    if (format != null)
                    {
                        DateTimeElementEditor editor = format.CreateElementEditor((DateTime) this.MaskManager.GetCurrentEditValue());
                        if (editor != null)
                        {
                            DateTimePickerData data = this.CreateDateTimePickerData(editor, format);
                            this.Pickers[num].SelectedItem = data;
                            num++;
                        }
                    }
                }
            }
        }

        private void UpdateValueOnAnimationCompleted(object sender, EventArgs e)
        {
            this.SetDateTime((DateTime) this.MaskManager.GetCurrentEditValue());
            this.UpdateSelectedItems();
        }

        public DateEdit OwnerDateEdit =>
            BaseEdit.GetOwnerEdit(this) as DateEdit;

        private DateTimeMaskManager MaskManager { get; set; }

        private DateTimeMaskManager IndexMaskManager { get; set; }

        private System.Windows.Controls.ItemsControl ItemsControl { get; set; }

        internal IList<DateTimePickerPart> Pickers { get; private set; }

        internal DateTimePickerPart SelectedPicker { get; private set; }

        private Locker UpdateValueOnAnimationCompletedLocker { get; set; }

        private Locker MaskManagerEditTextChangedLocker { get; set; }

        private Locker OnPreviewTextInputLocker { get; set; }

        private Locker SetDateTimeLocker { get; set; }

        internal bool IsItemsControlInitialized { get; set; }

        private PostponedAction ItemsControlInitializeAction { get; set; }

        private IEnumerable<DateTimeMaskFormatElement> Formats =>
            DateTimeMaskManagerHelper.GetFormatInfo(this.MaskManager);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DateTimePicker.<>c <>9 = new DateTimePicker.<>c();
            public static Func<DateTimeMaskFormatElement, bool> <>9__53_0;
            public static Action<DateTimePickerPart> <>9__57_0;
            public static Func<DateTimeMaskFormatElement, bool> <>9__59_0;
            public static Func<DateTimePickerPart, int> <>9__64_0;
            public static Func<int> <>9__64_1;
            public static Func<DateTimeMaskFormatElement, bool> <>9__66_0;
            public static Func<DateTimeMaskFormatElement, bool> <>9__68_0;
            public static Func<DateTimeMaskFormatElement, bool> <>9__70_0;
            public static Func<DateTimePickerData, DateTime> <>9__71_0;
            public static Action<DateTimePickerPart> <>9__78_1;
            public static Action<DateTimePickerPart> <>9__79_0;
            public static Func<DateTimeMaskFormatElement, bool> <>9__84_0;
            public static Action<DateTimePickerPart> <>9__85_0;

            internal bool <GeneratePickers>b__70_0(DateTimeMaskFormatElement format) => 
                format.Editable;

            internal bool <GetCursorPosition>b__84_0(DateTimeMaskFormatElement x) => 
                x.Editable;

            internal bool <GetSelectionRangeByIndex>b__59_0(DateTimeMaskFormatElement x) => 
                x.Editable;

            internal bool <InitializeItemsControl>b__66_0(DateTimeMaskFormatElement x) => 
                x.Editable;

            internal int <OnPreviewKeyDown>b__64_0(DateTimePickerPart x) => 
                x.VisibleItemsCount / 2;

            internal int <OnPreviewKeyDown>b__64_1() => 
                0;

            internal void <OnPreviewTextInput>b__57_0(DateTimePickerPart x)
            {
                x.UseTransitions = false;
            }

            internal void <PickerAnimatedChanged>b__79_0(DateTimePickerPart x)
            {
                x.UseTransitions = false;
            }

            internal void <PickerExpandedChanged>b__78_1(DateTimePickerPart x)
            {
                x.UseTransitions = true;
            }

            internal void <SelectPicker>b__85_0(DateTimePickerPart x)
            {
                x.IsExpanded = false;
            }

            internal bool <UpdateDaysCount>b__68_0(DateTimeMaskFormatElement x) => 
                x.Editable;

            internal DateTime <UpdatePickerPartFromElementEditor>b__71_0(DateTimePickerData data) => 
                data.Value;

            internal bool <UpdateSelectedItems>b__53_0(DateTimeMaskFormatElement x) => 
                x.Editable;
        }

        public static class DateTimeHelper
        {
            static DateTimeHelper()
            {
                Cache = new Dictionary<string, IList<int>>();
            }

            public static IList<int> GetAvailableDaysInPeriod(DateTime currentDateTime)
            {
                List<int> list = new List<int>();
                int daysInMonth = GetDaysInMonth(currentDateTime);
                for (int i = 1; i <= daysInMonth; i++)
                {
                    list.Add(i);
                }
                return list;
            }

            public static IList<int> GetAvailableHours12()
            {
                List<int> list = new List<int>();
                for (int i = 1; i <= 12; i++)
                {
                    list.Add(i);
                }
                return list;
            }

            public static IList<int> GetAvailableHours24()
            {
                List<int> list = new List<int>();
                for (int i = 0; i <= 0x17; i++)
                {
                    list.Add(i);
                }
                return list;
            }

            public static IList<int> GetAvailableMilliseconds()
            {
                List<int> list = new List<int>();
                for (int i = 0; i <= 0x3e7; i++)
                {
                    list.Add(i);
                }
                return list;
            }

            public static IList<int> GetAvailableMinutes()
            {
                List<int> list = new List<int>();
                for (int i = 0; i <= 0x3b; i++)
                {
                    list.Add(i);
                }
                return list;
            }

            public static IList<int> GetAvailableMonths()
            {
                List<int> list = new List<int>();
                for (int i = 1; i <= 12; i++)
                {
                    list.Add(i);
                }
                return list;
            }

            public static IList<int> GetAvailableSeconds()
            {
                List<int> list = new List<int>();
                for (int i = 0; i <= 0x3b; i++)
                {
                    list.Add(i);
                }
                return list;
            }

            public static IList<int> GetAvailableShortYears()
            {
                IList<int> list = new List<int>();
                for (int i = 0; i < 100; i++)
                {
                    list.Add(i);
                }
                return list;
            }

            public static IList<int> GetAvailableValuesByMask(string mask, DateTime value, DateTime startPeriod, DateTime endPeriod)
            {
                string key = $"{mask}_{GetValueByMask(mask, startPeriod)}_{GetValueByMask(mask, endPeriod)}";
                switch (GetDateTimePartByMask(mask))
                {
                    case DevExpress.Xpf.Editors.DateTimePart.Day:
                        return GetAvailableDaysInPeriod(value);

                    case DevExpress.Xpf.Editors.DateTimePart.Month:
                        if (!Cache.ContainsKey(key))
                        {
                            Cache.Add(key, GetAvailableMonths());
                        }
                        break;

                    case DevExpress.Xpf.Editors.DateTimePart.Year:
                        if (!Cache.ContainsKey(key))
                        {
                            Cache.Add(key, (mask.Length > 3) ? GetAvailableYears() : GetAvailableShortYears());
                        }
                        break;

                    case DevExpress.Xpf.Editors.DateTimePart.Hour12:
                        if (!Cache.ContainsKey(key))
                        {
                            Cache.Add(key, GetAvailableHours12());
                        }
                        break;

                    case DevExpress.Xpf.Editors.DateTimePart.Hour24:
                        if (!Cache.ContainsKey(key))
                        {
                            Cache.Add(key, GetAvailableHours24());
                        }
                        break;

                    case DevExpress.Xpf.Editors.DateTimePart.Minute:
                        if (!Cache.ContainsKey(key))
                        {
                            Cache.Add(key, GetAvailableMinutes());
                        }
                        break;

                    case DevExpress.Xpf.Editors.DateTimePart.Second:
                        if (!Cache.ContainsKey(key))
                        {
                            Cache.Add(key, GetAvailableSeconds());
                        }
                        break;

                    case DevExpress.Xpf.Editors.DateTimePart.Millisecond:
                        if (!Cache.ContainsKey(key))
                        {
                            Cache.Add(key, GetAvailableMilliseconds());
                        }
                        break;

                    case DevExpress.Xpf.Editors.DateTimePart.Period:
                        throw new NotImplementedException();

                    case DevExpress.Xpf.Editors.DateTimePart.PeriodOfEra:
                        throw new NotImplementedException();

                    default:
                        break;
                }
                return Cache[key];
            }

            public static IList<int> GetAvailableYears()
            {
                List<int> list = new List<int>();
                for (int i = 1; i <= 0x270f; i++)
                {
                    list.Add(i);
                }
                return list;
            }

            private static DevExpress.Xpf.Editors.DateTimePart GetDateTimePartByMask(string mask)
            {
                int length = mask.Length;
                char ch = mask[0];
                if (ch > 'h')
                {
                    if (ch <= 's')
                    {
                        if (ch == 'm')
                        {
                            return DevExpress.Xpf.Editors.DateTimePart.Minute;
                        }
                        if (ch == 's')
                        {
                            return DevExpress.Xpf.Editors.DateTimePart.Second;
                        }
                    }
                    else
                    {
                        if (ch == 't')
                        {
                            return DevExpress.Xpf.Editors.DateTimePart.Period;
                        }
                        if (ch == 'y')
                        {
                            return DevExpress.Xpf.Editors.DateTimePart.Year;
                        }
                    }
                }
                else
                {
                    if (ch == 'H')
                    {
                        return DevExpress.Xpf.Editors.DateTimePart.Hour24;
                    }
                    if (ch == 'M')
                    {
                        return DevExpress.Xpf.Editors.DateTimePart.Month;
                    }
                    switch (ch)
                    {
                        case 'd':
                            return ((length > 2) ? DevExpress.Xpf.Editors.DateTimePart.None : DevExpress.Xpf.Editors.DateTimePart.Day);

                        case 'f':
                            return DevExpress.Xpf.Editors.DateTimePart.Millisecond;

                        case 'g':
                            return DevExpress.Xpf.Editors.DateTimePart.PeriodOfEra;

                        case 'h':
                            return DevExpress.Xpf.Editors.DateTimePart.Hour12;

                        default:
                            break;
                    }
                }
                return DevExpress.Xpf.Editors.DateTimePart.None;
            }

            private static int GetDaysInMonth(DateTime dateTime) => 
                DateTime.DaysInMonth(dateTime.Year, dateTime.Month);

            public static int GetValueByMask(string mask, DateTime dateTime)
            {
                switch (GetDateTimePartByMask(mask))
                {
                    case DevExpress.Xpf.Editors.DateTimePart.Day:
                        return dateTime.Day;

                    case DevExpress.Xpf.Editors.DateTimePart.Month:
                        return dateTime.Month;

                    case DevExpress.Xpf.Editors.DateTimePart.Year:
                        return dateTime.Year;

                    case DevExpress.Xpf.Editors.DateTimePart.Hour12:
                        return ((dateTime.Hour > 12) ? (dateTime.Hour - 12) : dateTime.Hour);

                    case DevExpress.Xpf.Editors.DateTimePart.Hour24:
                        return dateTime.Hour;

                    case DevExpress.Xpf.Editors.DateTimePart.Minute:
                        return dateTime.Minute;

                    case DevExpress.Xpf.Editors.DateTimePart.Second:
                        return dateTime.Second;

                    case DevExpress.Xpf.Editors.DateTimePart.Millisecond:
                        return dateTime.Millisecond;

                    case DevExpress.Xpf.Editors.DateTimePart.Period:
                        return ((dateTime.Hour >= 12) ? 1 : 0);

                    case DevExpress.Xpf.Editors.DateTimePart.PeriodOfEra:
                        throw new NotImplementedException();
                }
                return -1;
            }

            private static IDictionary<string, IList<int>> Cache { get; set; }
        }
    }
}

