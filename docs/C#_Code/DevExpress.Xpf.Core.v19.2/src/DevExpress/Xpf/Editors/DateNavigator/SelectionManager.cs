namespace DevExpress.Xpf.Editors.DateNavigator
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class SelectionManager
    {
        private DateTime selectionStart;
        private IList<DateTime> currentSelection;
        private DateTime focusedDate;
        private IList<DateTime> initialSelection;

        public SelectionManager(DevExpress.Xpf.Editors.DateNavigator.DateNavigator navigator)
        {
            this.Navigator = navigator;
            this.Clear();
        }

        protected virtual void Clear()
        {
            this.currentSelection = this.Navigator.SelectedDates;
            this.focusedDate = this.Navigator.FocusedDate;
            this.initialSelection = this.Navigator.SelectedDates;
        }

        public virtual void Flush()
        {
            if (this.IsInContinuousSelection)
            {
                this.selectionStart = this.FocusedDate;
                this.initialSelection = this.currentSelection;
                this.InvalidateSelection();
            }
        }

        protected virtual void InvalidateFocused()
        {
            this.Navigator.InvalidateFocusedDate();
        }

        protected virtual void InvalidateSelection()
        {
            this.Navigator.InvalidateSelection();
        }

        public virtual void Post()
        {
            this.Navigator.FocusedDate = this.FocusedDate;
            if (!SelectedDatesHelper.AreEquals(this.SelectedDates, this.Navigator.SelectedDates))
            {
                this.Navigator.SetSelectedDates(new ObservableCollection<DateTime>(this.Navigator.ValueValidatingService.Validate(this.SelectedDates)));
            }
            this.Reset();
            this.InvalidateSelection();
        }

        protected virtual void Reset()
        {
            this.IsInContinuousSelection = false;
            this.selectionStart = this.Navigator.FocusedDate;
            this.Clear();
        }

        private IList<DateTime> Select(IList<DateTime> selection, bool clearSelection)
        {
            IList<DateTime> selectedDates = clearSelection ? selection : SelectedDatesHelper.Merge(this.initialSelection, selection);
            return this.ValueValidating.Validate(selectedDates);
        }

        public void SetFocusedDate(DateTime date, bool skipDisabledDates = false)
        {
            if (this.DateCalculationService.IsDisabled(date))
            {
                if (!skipDisabledDates && ((this.Navigator.MaxValue == null) || (date.CompareTo(this.Navigator.MaxValue.Value) >= 0)))
                {
                    this.InvalidateFocused();
                }
                else
                {
                    this.SetFocusedDate(date.AddDays(1.0), true);
                }
            }
            else
            {
                if (this.IsInContinuousSelection)
                {
                    this.focusedDate = date;
                }
                else
                {
                    this.Navigator.FocusedDate = date;
                }
                this.InvalidateFocused();
            }
        }

        public void SetSelection(IList<DateTime> selection, bool clearSelection)
        {
            List<DateTime> list = (from x in selection
                where !this.DateCalculationService.IsDisabled(x)
                select x).ToList<DateTime>();
            if (!this.IsInContinuousSelection)
            {
                this.Navigator.SetSelectedDates(new ObservableCollection<DateTime>(this.Select(list, clearSelection)));
            }
            else
            {
                this.currentSelection = new ObservableCollection<DateTime>(this.Select(list, clearSelection));
                if (clearSelection)
                {
                    this.initialSelection = this.currentSelection;
                }
            }
            this.InvalidateSelection();
        }

        public virtual void Snapshot()
        {
            if (!this.IsInContinuousSelection)
            {
                this.IsInContinuousSelection = true;
                this.selectionStart = this.Navigator.FocusedDate;
                this.Clear();
            }
        }

        public DateTime SelectionStart =>
            this.IsInContinuousSelection ? this.selectionStart : this.FocusedDate;

        public DateTime SelectionEnd =>
            (this.SelectedDates.Count > 0) ? this.SelectedDates.Last<DateTime>() : this.SelectionStart;

        public IList<DateTime> SelectedDates =>
            this.IsInContinuousSelection ? this.currentSelection : this.Navigator.SelectedDates;

        public DateTime FocusedDate =>
            this.IsInContinuousSelection ? this.focusedDate : this.Navigator.FocusedDate;

        private DevExpress.Xpf.Editors.DateNavigator.DateNavigator Navigator { get; set; }

        public bool IsInContinuousSelection { get; private set; }

        private IValueValidatingService ValueValidating =>
            this.Navigator.ValueValidatingService;

        private IDateCalculationService DateCalculationService =>
            (IDateCalculationService) ((IServiceProvider) this.Navigator).GetService(typeof(IDateCalculationService));
    }
}

