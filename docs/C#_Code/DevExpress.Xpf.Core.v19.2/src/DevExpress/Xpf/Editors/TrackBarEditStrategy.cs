namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors.EditStrategy;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Editors.Validation.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Data;
    using System.Windows.Input;
    using System.Windows.Media;

    public class TrackBarEditStrategy : RangeEditStrategyBase
    {
        private readonly int magicRoundNumber;
        private readonly PostponedAction updateThumbPositionAction;
        private double prevDragPoint;
        private readonly Locker selectionStartLocker;
        private readonly Locker selectionEndLocker;

        public TrackBarEditStrategy(TrackBarEdit editor) : base(editor)
        {
            this.magicRoundNumber = 10;
            this.selectionStartLocker = new Locker();
            this.selectionEndLocker = new Locker();
            this.ReservedSpaceCalculator = new DevExpress.Xpf.Editors.ReservedSpaceCalculator();
            this.updateThumbPositionAction = new PostponedAction(new Func<bool>(this.ShouldPostponeUpdateThumbPosition));
        }

        protected override void AfterApplyStyleSettings()
        {
            base.AfterApplyStyleSettings();
            base.SyncWithValue();
        }

        private double CalcMoveOffset(UIElement element, MouseButtonEventArgs e, bool increment)
        {
            Point position = e.GetPosition(element);
            double normalizedValue = this.GetNormalizedValue(position);
            double num2 = this.Editor.IsSnapToTickEnabled ? this.GetSnapToTickRealValue(normalizedValue) : base.GetRealValue(normalizedValue);
            if (!this.Editor.IsRange)
            {
                return (increment ? (num2 - base.ValueContainer.EditValue.TryConvertToDouble()) : (base.ValueContainer.EditValue.TryConvertToDouble() - num2));
            }
            TrackBarEditRange range = DevExpress.Xpf.Editors.ObjectToTrackBarRangeConverter.TryConvert(base.ValueContainer.EditValue);
            return (increment ? (num2 - range.SelectionEnd) : (range.SelectionStart - num2));
        }

        private void CenterThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            if (base.AllowEditing)
            {
                double delta = this.GetDelta(this.CenterThumb);
                if (Math.Abs(delta) > 0.0)
                {
                    this.SpinSelectionRange(Math.Abs(delta), Math.Sign(delta) == 1);
                }
            }
        }

        public virtual double CoerceSelectionEnd(double value)
        {
            double selectionStart = value;
            if (this.selectionStartLocker.IsLocked && (value < this.Editor.SelectionStart))
            {
                selectionStart = this.Editor.SelectionStart;
            }
            TrackBarEditRange range = DevExpress.Xpf.Editors.ObjectToTrackBarRangeConverter.TryConvert(base.PropertyUpdater.SyncValue);
            range.SelectionEnd = selectionStart;
            this.CoerceValue(TrackBarEdit.SelectionEndProperty, range);
            return selectionStart;
        }

        public double CoerceSelectionStart(double value)
        {
            double selectionEnd = value;
            if (this.selectionEndLocker.IsLocked && (value > this.Editor.SelectionEnd))
            {
                selectionEnd = this.Editor.SelectionEnd;
            }
            TrackBarEditRange range = DevExpress.Xpf.Editors.ObjectToTrackBarRangeConverter.TryConvert(base.PropertyUpdater.SyncValue);
            range.SelectionStart = selectionEnd;
            this.CoerceValue(TrackBarEdit.SelectionStartProperty, range);
            return selectionEnd;
        }

        protected override RangeEditBaseInfo CreateEditInfo() => 
            new TrackBarEditInfo();

        private Binding CreateThumbLengthBinding(System.Windows.Controls.Primitives.Thumb thumb) => 
            new Binding { 
                Path = new PropertyPath((this.Editor.Orientation == Orientation.Horizontal) ? FrameworkElement.ActualWidthProperty : FrameworkElement.ActualHeightProperty),
                Source = thumb,
                Mode = BindingMode.OneWay
            };

        public virtual void DecrementLarge()
        {
            this.IncrementDecrement(this.Editor.LargeStep, false, true);
        }

        public override void DecrementLarge(object parameter)
        {
            this.IncrementDecrement(this.Editor.LargeStep, false, true, this.GetSpinMode(parameter));
        }

        public override void DecrementSmall(object parameter)
        {
            this.IncrementDecrement(this.Editor.SmallStep, false, false, this.GetSpinMode(parameter));
        }

        private void FindButtons()
        {
            this.LeftStepButton = LayoutHelper.FindElementByName(this.Editor, "PART_LeftStepButton") as ButtonBase;
            this.RightStepButton = LayoutHelper.FindElementByName(this.Editor, "PART_RightStepButton") as ButtonBase;
            this.LeftSideButton = LayoutHelper.FindElementByName(this.Editor, "left") as ButtonBase;
            this.RightSideButton = LayoutHelper.FindElementByName(this.Editor, "right") as ButtonBase;
        }

        private void FindElements()
        {
            this.FindButtons();
            this.FindThumb();
            this.UpdateReservedSpaceCalculator();
        }

        private void FindThumb()
        {
            this.UnsubscribeEvents();
            this.Thumb = LayoutHelper.FindElementByName(this.Editor, "PART_Thumb") as System.Windows.Controls.Primitives.Thumb;
            this.LeftThumb = LayoutHelper.FindElementByName(this.Editor, "PART_LeftThumb") as System.Windows.Controls.Primitives.Thumb;
            this.RightThumb = LayoutHelper.FindElementByName(this.Editor, "PART_RightThumb") as System.Windows.Controls.Primitives.Thumb;
            this.CenterThumb = LayoutHelper.FindElementByName(this.Editor, "PART_CenterThumb") as System.Windows.Controls.Primitives.Thumb;
            this.SubscribeEvents();
        }

        private double GetDelta(System.Windows.Controls.Primitives.Thumb thumb) => 
            this.Editor.IsSnapToTickEnabled ? this.GetSnapToTicksDelta(thumb) : this.GetNormalDelta(thumb);

        private double GetDragValue(double value, System.Windows.Controls.Primitives.Thumb thumb)
        {
            double normalDragValue = this.GetNormalDragValue(value, thumb);
            return (this.Editor.IsSnapToTickEnabled ? this.GetSnapToTickValue(normalDragValue) : normalDragValue);
        }

        private double GetLength(FrameworkElement element) => 
            (element != null) ? ((this.Editor.Orientation == Orientation.Horizontal) ? element.ActualWidth : element.ActualHeight) : 0.0;

        private double GetMousePosition(FrameworkElement element)
        {
            Point position = Mouse.GetPosition(element);
            return ((this.Editor.Orientation == Orientation.Horizontal) ? position.X : (this.GetOrientationSign() * position.Y));
        }

        private double GetNearestTickValue(double realValue, double snapLength)
        {
            if ((this.Editor.Maximum - realValue) < snapLength)
            {
                double num4 = (Math.Ceiling((double) (Math.Max((double) 0.0, (double) (base.GetRange() - snapLength)) / snapLength)) * snapLength) + this.Editor.Minimum;
                return (((this.Editor.Maximum - realValue) >= ((this.Editor.Maximum - num4) / 2.0)) ? num4 : this.Editor.Maximum);
            }
            double num2 = Math.Round((double) ((realValue - this.Editor.Minimum) / snapLength));
            return Math.Min(this.Editor.Maximum, (num2 * snapLength) + this.Editor.Minimum);
        }

        protected override double GetNextValue(double value, double step, bool increment, bool isLarge)
        {
            if (!this.Editor.IsSnapToTickEnabled)
            {
                return base.GetNextValue(value, step, increment, isLarge);
            }
            double snapToTickValue = this.GetSnapToTickValue(this.ToRange(base.GetNextValue(value, step, increment, isLarge)));
            if ((snapToTickValue.AreClose(value) && (!increment || !value.AreClose(this.Editor.Maximum))) && (increment || !value.AreClose(this.Editor.Minimum)))
            {
                DoubleCollection steps = this.Editor.Steps;
                if ((steps == null) || (steps.Count <= 0))
                {
                    if (step.GreaterThan(0.0))
                    {
                        double num4 = Math.Round((double) ((value - this.Editor.Minimum) / step));
                        snapToTickValue = this.Editor.Minimum + ((!increment ? (num4 - 1.0) : (num4 + 1.0)) * step);
                    }
                }
                else
                {
                    for (int i = 0; i < steps.Count; i++)
                    {
                        double num3 = steps[i];
                        if ((increment && (num3.GreaterThan(value) && (num3.LessThan(snapToTickValue) || snapToTickValue.AreClose(value)))) || (!increment && (num3.LessThan(value) && (num3.GreaterThan(snapToTickValue) || snapToTickValue.AreClose(value)))))
                        {
                            snapToTickValue = num3;
                        }
                    }
                }
            }
            return snapToTickValue;
        }

        private double GetNormalDelta(System.Windows.Controls.Primitives.Thumb thumb)
        {
            double mousePosition = this.GetMousePosition(this.Editor);
            double num2 = this.GetMousePosition(thumb) - ((this.GetOrientationSign() * this.GetLength(thumb)) / 2.0);
            int num3 = Math.Sign((double) (mousePosition - this.prevDragPoint));
            this.prevDragPoint = mousePosition;
            return ((num3 != Math.Sign(num2)) ? 0.0 : (this.NormalizeByLength(num2) * (this.Editor.Maximum - this.Editor.Minimum)));
        }

        private double GetNormalDragValue(double value, System.Windows.Controls.Primitives.Thumb thumb)
        {
            double mousePosition = this.GetMousePosition(this.Editor);
            double num2 = this.GetMousePosition(thumb) - ((this.GetOrientationSign() * this.GetLength(thumb)) / 2.0);
            int num3 = Math.Sign((double) (mousePosition - this.prevDragPoint));
            this.prevDragPoint = mousePosition;
            double num4 = (num3 != Math.Sign(num2)) ? 0.0 : (this.NormalizeByLength(num2) * (this.Editor.Maximum - this.Editor.Minimum));
            return (value + num4);
        }

        private double GetNormalizedValue(Point point)
        {
            double length = (((this.Editor.Orientation == Orientation.Horizontal) ? point.X : point.Y) - this.GetLength(this.LeftStepButton)) - (this.Editor.IsRange ? this.ReservedSpaceCalculator.LeftThumbLength : (this.ReservedSpaceCalculator.ThumbLength / 2.0));
            double num2 = this.NormalizeByLength(length);
            return ((this.Editor.Orientation == Orientation.Horizontal) ? num2 : (1.0 - num2));
        }

        private int GetOrientationSign() => 
            (this.Editor.Orientation == Orientation.Horizontal) ? 1 : -1;

        private double GetSmallStep() => 
            (this.Editor.SmallStep.CompareTo((double) 0.0) == 0) ? 1.0 : this.Editor.SmallStep;

        private double GetSmallStepCount(double delta) => 
            Math.Round((double) ((delta * base.GetRange()) / this.GetSmallStep()), 0);

        internal double GetSnapToTickRealValue(double normalizeValue) => 
            this.GetSnapToTickRealValue(normalizeValue, this.GetSmallStep());

        private double GetSnapToTickRealValue(double normalizeValue, double snapLength)
        {
            double realValue = base.GetRealValue(normalizeValue);
            return this.GetNearestTickValue(realValue, snapLength);
        }

        private double GetSnapToTicksDelta(System.Windows.Controls.Primitives.Thumb thumb)
        {
            double mousePosition = this.GetMousePosition(this.Editor);
            double num2 = this.GetMousePosition(thumb) - ((this.GetOrientationSign() * this.GetLength(thumb)) / 2.0);
            int num3 = Math.Sign((double) (mousePosition - this.prevDragPoint));
            this.prevDragPoint = mousePosition;
            return ((num3 != Math.Sign(num2)) ? 0.0 : (this.GetSmallStepCount(this.NormalizeByLength(num2)) * this.Editor.SmallStep));
        }

        private double GetSnapToTickValue(double value)
        {
            double minimum = this.Editor.Minimum;
            double maximum = this.Editor.Maximum;
            DoubleCollection steps = this.Editor.Steps;
            if ((steps == null) || (steps.Count <= 0))
            {
                if (this.Editor.SmallStep.GreaterThan(0.0))
                {
                    minimum = this.Editor.Minimum + (Math.Round((double) ((value - this.Editor.Minimum) / this.Editor.SmallStep)) * this.Editor.SmallStep);
                    maximum = Math.Min(this.Editor.Maximum, minimum + this.Editor.SmallStep);
                }
            }
            else
            {
                for (int i = 0; i < steps.Count; i++)
                {
                    double num4 = steps[i];
                    if (num4.AreClose(value))
                    {
                        return value;
                    }
                    if (num4.LessThan(value) && num4.GreaterThan(minimum))
                    {
                        minimum = num4;
                    }
                    else if (num4.GreaterThan(value) && num4.LessThan(maximum))
                    {
                        maximum = num4;
                    }
                }
            }
            value = value.GreaterThanOrClose(((minimum + maximum) * 0.5)) ? maximum : minimum;
            return value;
        }

        private TrackBarIncrementTargetEnum GetSpinMode(object parameter) => 
            (parameter != null) ? ((TrackBarIncrementTargetEnum) parameter) : TrackBarIncrementTargetEnum.Value;

        public void IncrementDecrement(double value, bool increment, bool isLarge, TrackBarIncrementTargetEnum target)
        {
            if (base.AllowEditing)
            {
                if (!this.Editor.IsRange)
                {
                    this.IncrementDecrement(value, increment, isLarge);
                }
                else
                {
                    if (target == TrackBarIncrementTargetEnum.Value)
                    {
                        this.Spin(value, increment);
                    }
                    if (target == TrackBarIncrementTargetEnum.SelectionStart)
                    {
                        this.SpinSelectionStart(value, increment);
                    }
                    if (target == TrackBarIncrementTargetEnum.SelectionEnd)
                    {
                        this.SpinSelectionEnd(value, increment);
                    }
                    if (target == TrackBarIncrementTargetEnum.SelectionRange)
                    {
                        this.SpinSelectionRange(value, increment);
                    }
                }
            }
        }

        protected override double IncrementDecrementInternal(double step, bool increment, bool isLarge, double result)
        {
            CustomStepEventArgs args = new CustomStepEventArgs(increment, isLarge, result);
            this.PropertyProvider.RaiseCustomStep(args);
            return (args.Handled ? args.Value : base.IncrementDecrementInternal(step, increment, isLarge, result));
        }

        public virtual void IncrementLarge()
        {
            this.IncrementDecrement(this.Editor.LargeStep, true, true);
        }

        public override void IncrementLarge(object parameter)
        {
            this.IncrementDecrement(this.Editor.LargeStep, true, true, this.GetSpinMode(parameter));
        }

        public override void IncrementSmall(object parameter)
        {
            this.IncrementDecrement(this.Editor.SmallStep, true, false, this.GetSpinMode(parameter));
        }

        public virtual void IsSnapToTickEnabledChanged(bool value)
        {
        }

        private void LeftThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            if (base.AllowEditing)
            {
                TrackBarEditRange range = DevExpress.Xpf.Editors.ObjectToTrackBarRangeConverter.TryConvert(base.ValueContainer.EditValue);
                double dragValue = this.GetDragValue(range.SelectionStart, this.LeftThumb);
                if (dragValue > range.SelectionEnd)
                {
                    dragValue = range.SelectionEnd;
                }
                TrackBarEditRange editValue = new TrackBarEditRange();
                editValue.SelectionStart = this.ToRange(dragValue);
                editValue.SelectionEnd = range.SelectionEnd;
                base.ValueContainer.SetEditValue(editValue, UpdateEditorSource.TextInput);
            }
        }

        public void Maximize(TrackBarIncrementTargetEnum target)
        {
            if (!this.Editor.IsRange)
            {
                base.Maximize(target);
            }
            else
            {
                TrackBarEditRange range = DevExpress.Xpf.Editors.ObjectToTrackBarRangeConverter.TryConvert(base.ValueContainer.EditValue);
                if (target == TrackBarIncrementTargetEnum.Value)
                {
                    TrackBarEditRange editValue = new TrackBarEditRange();
                    editValue.SelectionStart = this.Editor.Maximum;
                    editValue.SelectionEnd = this.Editor.Maximum;
                    base.ValueContainer.SetEditValue(editValue, UpdateEditorSource.TextInput);
                }
                if (target == TrackBarIncrementTargetEnum.SelectionStart)
                {
                    TrackBarEditRange editValue = new TrackBarEditRange();
                    editValue.SelectionStart = this.Editor.SelectionEnd;
                    editValue.SelectionEnd = range.SelectionEnd;
                    base.ValueContainer.SetEditValue(editValue, UpdateEditorSource.TextInput);
                }
                if (target == TrackBarIncrementTargetEnum.SelectionEnd)
                {
                    TrackBarEditRange editValue = new TrackBarEditRange();
                    editValue.SelectionStart = range.SelectionStart;
                    editValue.SelectionEnd = this.Editor.Maximum;
                    base.ValueContainer.SetEditValue(editValue, UpdateEditorSource.TextInput);
                }
                if (target == TrackBarIncrementTargetEnum.SelectionRange)
                {
                    double num = range.SelectionEnd - range.SelectionStart;
                    TrackBarEditRange editValue = new TrackBarEditRange();
                    editValue.SelectionStart = this.Editor.Maximum - num;
                    editValue.SelectionEnd = this.Editor.Maximum;
                    base.ValueContainer.SetEditValue(editValue, UpdateEditorSource.TextInput);
                }
            }
        }

        public override void Maximize(object parameter)
        {
            this.Maximize(this.GetSpinMode(parameter));
        }

        public override void MaximumChanged(double value)
        {
            if (!this.Editor.IsRange)
            {
                base.MaximumChanged(value);
            }
            else
            {
                TrackBarEditRange range = DevExpress.Xpf.Editors.ObjectToTrackBarRangeConverter.TryConvert(base.ValueContainer.EditValue);
                TrackBarEditRange editValue = new TrackBarEditRange();
                editValue.SelectionStart = this.ToRange(range.SelectionStart);
                editValue.SelectionEnd = this.ToRange(range.SelectionEnd);
                base.ValueContainer.SetEditValue(editValue, UpdateEditorSource.TextInput);
                this.UpdatePositions();
            }
        }

        public void Minimize(TrackBarIncrementTargetEnum target)
        {
            if (!this.Editor.IsRange)
            {
                base.Minimize(target);
            }
            else
            {
                TrackBarEditRange range = DevExpress.Xpf.Editors.ObjectToTrackBarRangeConverter.TryConvert(base.ValueContainer.EditValue);
                if (target == TrackBarIncrementTargetEnum.Value)
                {
                    TrackBarEditRange editValue = new TrackBarEditRange();
                    editValue.SelectionStart = this.Editor.Minimum;
                    editValue.SelectionEnd = this.Editor.Minimum;
                    base.ValueContainer.SetEditValue(editValue, UpdateEditorSource.TextInput);
                }
                if (target == TrackBarIncrementTargetEnum.SelectionStart)
                {
                    TrackBarEditRange editValue = new TrackBarEditRange();
                    editValue.SelectionStart = this.Editor.Minimum;
                    editValue.SelectionEnd = range.SelectionEnd;
                    base.ValueContainer.SetEditValue(editValue, UpdateEditorSource.TextInput);
                }
                if (target == TrackBarIncrementTargetEnum.SelectionEnd)
                {
                    TrackBarEditRange editValue = new TrackBarEditRange();
                    editValue.SelectionStart = range.SelectionStart;
                    editValue.SelectionEnd = range.SelectionStart;
                    base.ValueContainer.SetEditValue(editValue, UpdateEditorSource.TextInput);
                }
                if (target == TrackBarIncrementTargetEnum.SelectionRange)
                {
                    double num = range.SelectionEnd - range.SelectionStart;
                    TrackBarEditRange editValue = new TrackBarEditRange();
                    editValue.SelectionStart = this.Editor.Minimum;
                    editValue.SelectionEnd = this.Editor.Minimum + num;
                    base.ValueContainer.SetEditValue(editValue, UpdateEditorSource.TextInput);
                }
            }
        }

        public override void Minimize(object parameter)
        {
            this.Minimize(this.GetSpinMode(parameter));
        }

        public override void MinimumChanged(double value)
        {
            if (!this.Editor.IsRange)
            {
                base.MinimumChanged(value);
            }
            else
            {
                TrackBarEditRange range = DevExpress.Xpf.Editors.ObjectToTrackBarRangeConverter.TryConvert(base.ValueContainer.EditValue);
                TrackBarEditRange editValue = new TrackBarEditRange();
                editValue.SelectionStart = this.ToRange(range.SelectionStart);
                editValue.SelectionEnd = this.ToRange(range.SelectionEnd);
                base.ValueContainer.SetEditValue(editValue, UpdateEditorSource.TextInput);
                this.UpdatePositions();
            }
        }

        private double NormalizeByLength(double length)
        {
            double num = ((this.GetLength(base.Panel) - this.GetLength(this.LeftStepButton)) - this.GetLength(this.RightStepButton)) - (this.Editor.IsRange ? (this.ReservedSpaceCalculator.LeftThumbLength + this.ReservedSpaceCalculator.RightThumbLength) : this.ReservedSpaceCalculator.ThumbLength);
            return (length / num);
        }

        protected internal override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.FindElements();
            this.UpdatePositions();
        }

        public override void OnMouseWheel(MouseWheelEventArgs e)
        {
            base.OnMouseWheel(e);
            if (base.AllowEditing)
            {
                int num = e.Delta / 120;
                if (Math.Sign(num) > 0)
                {
                    for (int i = 0; i < Math.Abs(num); i++)
                    {
                        this.IncrementSmall(this.Editor.IsRange ? TrackBarIncrementTargetEnum.SelectionRange : TrackBarIncrementTargetEnum.Value);
                    }
                }
                else
                {
                    for (int i = 0; i < Math.Abs(num); i++)
                    {
                        this.DecrementSmall(this.Editor.IsRange ? TrackBarIncrementTargetEnum.SelectionRange : TrackBarIncrementTargetEnum.Value);
                    }
                }
            }
        }

        public override void PreviewMouseDown(MouseButtonEventArgs e)
        {
            if (this.PropertyProvider.IsMoveToPointEnabled)
            {
                if ((this.LeftSideButton != null) && this.LeftSideButton.IsMouseOver)
                {
                    e.Handled = true;
                }
                if ((this.RightSideButton != null) && this.RightSideButton.IsMouseOver)
                {
                    e.Handled = true;
                }
                base.PreviewMouseDown(e);
            }
        }

        public override void PreviewMouseUp(MouseButtonEventArgs e)
        {
            base.PreviewMouseUp(e);
            if (this.PropertyProvider.IsMoveToPointEnabled)
            {
                double num;
                if ((this.LeftSideButton != null) && this.LeftSideButton.IsMouseOver)
                {
                    num = this.CalcMoveOffset(base.Panel, e, false);
                    this.SpinSelectionRange(num, false);
                    e.Handled = true;
                }
                if ((this.RightSideButton != null) && this.RightSideButton.IsMouseOver)
                {
                    num = this.CalcMoveOffset(base.Panel, e, true);
                    this.SpinSelectionRange(num, true);
                    e.Handled = true;
                }
            }
        }

        protected override void ProcessPreviewKeyDownInternal(KeyEventArgs e)
        {
            base.ProcessPreviewKeyDownInternal(e);
            if ((this.Editor.EditMode != EditMode.InplaceActive) || ModifierKeysHelper.IsCtrlPressed(ModifierKeysHelper.GetKeyboardModifiers(e)))
            {
                if ((e.Key == Key.Left) || (e.Key == Key.Down))
                {
                    this.Editor.DecrementSmall(this.Editor.IsRange ? TrackBarIncrementTargetEnum.SelectionRange : TrackBarIncrementTargetEnum.Value);
                    e.Handled = true;
                }
                if ((e.Key == Key.Right) || (e.Key == Key.Up))
                {
                    this.Editor.IncrementSmall(this.Editor.IsRange ? TrackBarIncrementTargetEnum.SelectionRange : TrackBarIncrementTargetEnum.Value);
                    e.Handled = true;
                }
                if (e.Key == Key.Next)
                {
                    this.Editor.DecrementLarge(this.Editor.IsRange ? TrackBarIncrementTargetEnum.SelectionRange : TrackBarIncrementTargetEnum.Value);
                    e.Handled = true;
                }
                if (e.Key == Key.Prior)
                {
                    this.Editor.IncrementLarge(this.Editor.IsRange ? TrackBarIncrementTargetEnum.SelectionRange : TrackBarIncrementTargetEnum.Value);
                    e.Handled = true;
                }
            }
        }

        protected override void RegisterUpdateCallbacks()
        {
            base.RegisterUpdateCallbacks();
            PropertyCoercionHandler getBaseValueHandler = <>c.<>9__50_0;
            if (<>c.<>9__50_0 == null)
            {
                PropertyCoercionHandler local1 = <>c.<>9__50_0;
                getBaseValueHandler = <>c.<>9__50_0 = baseValue => baseValue;
            }
            base.PropertyUpdater.Register(RangeBaseEdit.ValueProperty, getBaseValueHandler, (PropertyCoercionHandler) (baseValue => (this.Editor.IsRange ? 0.0 : DevExpress.Xpf.Editors.ObjectToDoubleConverter.TryConvert(baseValue))));
            PropertyCoercionHandler handler2 = <>c.<>9__50_2;
            if (<>c.<>9__50_2 == null)
            {
                PropertyCoercionHandler local2 = <>c.<>9__50_2;
                handler2 = <>c.<>9__50_2 = baseValue => baseValue;
            }
            base.PropertyUpdater.Register(TrackBarEdit.SelectionStartProperty, handler2, new PropertyCoercionHandler(this.UpdateSelectionStartInternal));
            PropertyCoercionHandler handler3 = <>c.<>9__50_3;
            if (<>c.<>9__50_3 == null)
            {
                PropertyCoercionHandler local3 = <>c.<>9__50_3;
                handler3 = <>c.<>9__50_3 = baseValue => baseValue;
            }
            base.PropertyUpdater.Register(TrackBarEdit.SelectionEndProperty, handler3, new PropertyCoercionHandler(this.UpdateSelectionEndInternal));
        }

        private void RightThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            if (base.AllowEditing)
            {
                TrackBarEditRange range = DevExpress.Xpf.Editors.ObjectToTrackBarRangeConverter.TryConvert(base.ValueContainer.EditValue);
                double dragValue = this.GetDragValue(range.SelectionEnd, this.RightThumb);
                if (dragValue < range.SelectionStart)
                {
                    dragValue = range.SelectionStart;
                }
                TrackBarEditRange editValue = new TrackBarEditRange();
                editValue.SelectionStart = range.SelectionStart;
                editValue.SelectionEnd = this.ToRange(dragValue);
                base.ValueContainer.SetEditValue(editValue, UpdateEditorSource.TextInput);
            }
        }

        public virtual void SelectionEndChanged(double oldValue, double value)
        {
            if (!base.ShouldLockUpdate)
            {
                TrackBarEditRange range = DevExpress.Xpf.Editors.ObjectToTrackBarRangeConverter.TryConvert(base.ValueContainer.EditValue);
                range.SelectionEnd = oldValue;
                TrackBarEditRange newValue = DevExpress.Xpf.Editors.ObjectToTrackBarRangeConverter.TryConvert(base.ValueContainer.EditValue);
                newValue.SelectionEnd = value;
                base.SyncWithValue(TrackBarEdit.SelectionEndProperty, range, newValue);
                this.UpdatePositions();
            }
        }

        public virtual void SelectionStartChanged(double oldValue, double value)
        {
            if (!base.ShouldLockUpdate)
            {
                TrackBarEditRange range = DevExpress.Xpf.Editors.ObjectToTrackBarRangeConverter.TryConvert(base.ValueContainer.EditValue);
                range.SelectionStart = oldValue;
                TrackBarEditRange newValue = DevExpress.Xpf.Editors.ObjectToTrackBarRangeConverter.TryConvert(base.ValueContainer.EditValue);
                newValue.SelectionStart = value;
                base.SyncWithValue(TrackBarEdit.SelectionStartProperty, range, newValue);
                this.UpdatePositions();
            }
        }

        protected virtual bool ShouldPostponeUpdateThumbPosition() => 
            false;

        private void Spin(double value, bool increment)
        {
            double num = ((this.Editor.SelectionEnd + this.Editor.SelectionStart) / 2.0) + (increment ? value : -value);
            base.ValueContainer.SetEditValue(DevExpress.Xpf.Editors.ObjectToTrackBarRangeConverter.TryConvert(this.ToRange(num)), UpdateEditorSource.TextInput);
        }

        private void SpinSelectionEnd(double value, bool increment)
        {
            TrackBarEditRange range = DevExpress.Xpf.Editors.ObjectToTrackBarRangeConverter.TryConvert(base.ValueContainer.EditValue);
            double selectionStart = range.SelectionEnd + (increment ? value : -value);
            if (selectionStart < range.SelectionStart)
            {
                selectionStart = range.SelectionStart;
            }
            TrackBarEditRange editValue = new TrackBarEditRange();
            editValue.SelectionStart = range.SelectionStart;
            editValue.SelectionEnd = this.ToRange(selectionStart);
            base.ValueContainer.SetEditValue(editValue, UpdateEditorSource.TextInput);
        }

        private void SpinSelectionRange(double value, bool increment)
        {
            if (base.AllowEditing)
            {
                if (!this.Editor.IsRange)
                {
                    if (increment)
                    {
                        base.ValueContainer.SetEditValue(this.ToRange(base.ValueContainer.EditValue.TryConvertToDouble() + value), UpdateEditorSource.TextInput);
                    }
                    else
                    {
                        base.ValueContainer.SetEditValue(this.ToRange(base.ValueContainer.EditValue.TryConvertToDouble() - value), UpdateEditorSource.TextInput);
                    }
                }
                else
                {
                    TrackBarEditRange range = DevExpress.Xpf.Editors.ObjectToTrackBarRangeConverter.TryConvert(base.ValueContainer.EditValue);
                    double num = !increment ? (this.ToRange(range.SelectionStart - value) - range.SelectionStart) : (this.ToRange(range.SelectionEnd + value) - range.SelectionEnd);
                    TrackBarEditRange editValue = new TrackBarEditRange();
                    editValue.SelectionStart = range.SelectionStart + num;
                    editValue.SelectionEnd = range.SelectionEnd + num;
                    base.ValueContainer.SetEditValue(editValue, UpdateEditorSource.TextInput);
                }
            }
        }

        private void SpinSelectionStart(double value, bool increment)
        {
            TrackBarEditRange range = DevExpress.Xpf.Editors.ObjectToTrackBarRangeConverter.TryConvert(base.ValueContainer.EditValue);
            double selectionEnd = range.SelectionStart + (increment ? value : -value);
            if (selectionEnd > range.SelectionEnd)
            {
                selectionEnd = range.SelectionEnd;
            }
            TrackBarEditRange editValue = new TrackBarEditRange();
            editValue.SelectionStart = this.ToRange(selectionEnd);
            editValue.SelectionEnd = range.SelectionEnd;
            base.ValueContainer.SetEditValue(editValue, UpdateEditorSource.TextInput);
        }

        private void SubscribeEvents()
        {
            if (this.Thumb != null)
            {
                this.Thumb.DragStarted += new DragStartedEventHandler(this.Thumb_DragStarted);
                this.Thumb.DragDelta += new DragDeltaEventHandler(this.Thumb_DragDelta);
            }
            if (this.LeftThumb != null)
            {
                this.LeftThumb.DragStarted += new DragStartedEventHandler(this.Thumb_DragStarted);
                this.LeftThumb.DragDelta += new DragDeltaEventHandler(this.LeftThumb_DragDelta);
            }
            if (this.RightThumb != null)
            {
                this.RightThumb.DragStarted += new DragStartedEventHandler(this.Thumb_DragStarted);
                this.RightThumb.DragDelta += new DragDeltaEventHandler(this.RightThumb_DragDelta);
            }
            if (this.CenterThumb != null)
            {
                this.CenterThumb.DragStarted += new DragStartedEventHandler(this.Thumb_DragStarted);
                this.CenterThumb.DragDelta += new DragDeltaEventHandler(this.CenterThumb_DragDelta);
            }
        }

        private void Thumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            if (base.AllowEditing)
            {
                this.updateThumbPositionAction.PerformPostpone(delegate {
                    double num = DevExpress.Xpf.Editors.ObjectToDoubleConverter.TryConvert(base.ValueContainer.EditValue);
                    double dragValue = this.GetDragValue(num, this.Thumb);
                    base.ValueContainer.SetEditValue(this.ToRange(dragValue), UpdateEditorSource.TextInput);
                });
            }
        }

        private void Thumb_DragStarted(object sender, DragStartedEventArgs e)
        {
            this.prevDragPoint = this.GetMousePosition(this.Editor);
        }

        protected override double ToRange(double value)
        {
            double num = base.ToRange(value);
            return (!this.Editor.IsSnapToTickEnabled ? num : Math.Round(num, this.magicRoundNumber));
        }

        private void UnsubscribeEvents()
        {
            if (this.Thumb != null)
            {
                this.Thumb.DragStarted -= new DragStartedEventHandler(this.Thumb_DragStarted);
                this.Thumb.DragDelta -= new DragDeltaEventHandler(this.Thumb_DragDelta);
            }
            if (this.LeftThumb != null)
            {
                this.LeftThumb.DragStarted -= new DragStartedEventHandler(this.Thumb_DragStarted);
                this.LeftThumb.DragDelta -= new DragDeltaEventHandler(this.LeftThumb_DragDelta);
            }
            if (this.RightThumb != null)
            {
                this.RightThumb.DragStarted -= new DragStartedEventHandler(this.Thumb_DragStarted);
                this.RightThumb.DragDelta -= new DragDeltaEventHandler(this.RightThumb_DragDelta);
            }
        }

        public override void UpdatePositions()
        {
            if (!this.Editor.IsRange)
            {
                base.UpdatePositions();
            }
            else
            {
                TrackBarEditRange range = DevExpress.Xpf.Editors.ObjectToTrackBarRangeConverter.TryConvert(base.ValueContainer.EditValue);
                double normalValue = base.GetNormalValue(range.SelectionStart);
                double num2 = base.GetNormalValue(range.SelectionEnd);
                double num3 = num2 - normalValue;
                this.Info.SelectionLength = num3;
                this.Info.LeftSidePosition = normalValue;
                this.Info.RightSidePosition = 1.0 - num2;
                this.UpdateDisplayText();
            }
        }

        private void UpdateReservedSpaceCalculator()
        {
            this.ReservedSpaceCalculator.ClearValue(DevExpress.Xpf.Editors.ReservedSpaceCalculator.ThumbLengthProperty);
            this.ReservedSpaceCalculator.ClearValue(DevExpress.Xpf.Editors.ReservedSpaceCalculator.LeftThumbLengthProperty);
            this.ReservedSpaceCalculator.ClearValue(DevExpress.Xpf.Editors.ReservedSpaceCalculator.RightThumbLengthProperty);
            if (this.Thumb != null)
            {
                BindingOperations.SetBinding(this.ReservedSpaceCalculator, DevExpress.Xpf.Editors.ReservedSpaceCalculator.ThumbLengthProperty, this.CreateThumbLengthBinding(this.Thumb));
            }
            if (this.LeftThumb != null)
            {
                BindingOperations.SetBinding(this.ReservedSpaceCalculator, DevExpress.Xpf.Editors.ReservedSpaceCalculator.LeftThumbLengthProperty, this.CreateThumbLengthBinding(this.LeftThumb));
            }
            if (this.RightThumb != null)
            {
                BindingOperations.SetBinding(this.ReservedSpaceCalculator, DevExpress.Xpf.Editors.ReservedSpaceCalculator.RightThumbLengthProperty, this.CreateThumbLengthBinding(this.RightThumb));
            }
            Binding binding = new Binding();
            binding.Source = this.ReservedSpaceCalculator;
            binding.Path = new PropertyPath(DevExpress.Xpf.Editors.ReservedSpaceCalculator.ReservedSpaceProperty);
            binding.Mode = BindingMode.OneWay;
            BindingOperations.SetBinding(this.Info, TrackBarEditInfo.ReservedSpaceProperty, binding);
        }

        private object UpdateSelectionEndInternal(object value)
        {
            TrackBarEditRange range = value as TrackBarEditRange;
            if (range == null)
            {
                double num = DevExpress.Xpf.Editors.ObjectToDoubleConverter.TryConvert(value);
                TrackBarEditRange range1 = new TrackBarEditRange();
                range1.SelectionStart = num;
                range1.SelectionEnd = num;
                range = range1;
            }
            return (this.Editor.IsRange ? range.SelectionEnd : 0.0);
        }

        private object UpdateSelectionStartInternal(object value)
        {
            TrackBarEditRange range = value as TrackBarEditRange;
            if (range == null)
            {
                double num = DevExpress.Xpf.Editors.ObjectToDoubleConverter.TryConvert(value);
                TrackBarEditRange range1 = new TrackBarEditRange();
                range1.SelectionStart = num;
                range1.SelectionEnd = num;
                range = range1;
            }
            return (this.Editor.IsRange ? range.SelectionStart : 0.0);
        }

        protected internal override bool IsLockedByValueChanging =>
            base.IsLockedByValueChanging || (this.selectionStartLocker.IsLocked || this.selectionEndLocker.IsLocked);

        private TrackBarEdit Editor =>
            base.Editor as TrackBarEdit;

        private TrackBarEditPropertyProvider PropertyProvider =>
            this.Editor.PropertyProvider as TrackBarEditPropertyProvider;

        private ButtonBase LeftStepButton { get; set; }

        private ButtonBase RightStepButton { get; set; }

        private System.Windows.Controls.Primitives.Thumb Thumb { get; set; }

        private System.Windows.Controls.Primitives.Thumb LeftThumb { get; set; }

        private System.Windows.Controls.Primitives.Thumb RightThumb { get; set; }

        private System.Windows.Controls.Primitives.Thumb CenterThumb { get; set; }

        private ButtonBase LeftSideButton { get; set; }

        private ButtonBase RightSideButton { get; set; }

        private TrackBarEditInfo Info =>
            base.Info as TrackBarEditInfo;

        private DevExpress.Xpf.Editors.ReservedSpaceCalculator ReservedSpaceCalculator { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly TrackBarEditStrategy.<>c <>9 = new TrackBarEditStrategy.<>c();
            public static PropertyCoercionHandler <>9__50_0;
            public static PropertyCoercionHandler <>9__50_2;
            public static PropertyCoercionHandler <>9__50_3;

            internal object <RegisterUpdateCallbacks>b__50_0(object baseValue) => 
                baseValue;

            internal object <RegisterUpdateCallbacks>b__50_2(object baseValue) => 
                baseValue;

            internal object <RegisterUpdateCallbacks>b__50_3(object baseValue) => 
                baseValue;
        }
    }
}

