namespace DevExpress.Xpf.Editors.DateNavigator.Internal
{
    using DevExpress.Xpf.Editors.DateNavigator;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public class DateNavigatorOptionsProviderService : IOptionsProviderService
    {
        private readonly DevExpress.Xpf.Editors.DateNavigator.DateNavigator navigator;

        public event EventHandler OptionsChanged;

        public DateNavigatorOptionsProviderService(DevExpress.Xpf.Editors.DateNavigator.DateNavigator navigator)
        {
            this.navigator = navigator;
        }

        internal void RaiseOnOptionsChanged()
        {
            if (this.OptionsChanged != null)
            {
                this.OptionsChanged(this, EventArgs.Empty);
            }
        }

        public void Start()
        {
            this.RaiseOnOptionsChanged();
        }

        public void Stop()
        {
        }

        DateTime IOptionsProviderService.Today =>
            DateTime.Today;

        public DayOfWeek FirstDayOfWeek
        {
            get
            {
                DateTimeFormatInfo dateTimeFormat = Thread.CurrentThread.CurrentCulture.DateTimeFormat;
                return ((this.navigator.FirstDayOfWeek == null) ? ((DayOfWeek) new DayOfWeek?(dateTimeFormat.FirstDayOfWeek)) : ((DayOfWeek) this.navigator.FirstDayOfWeek)).Value;
            }
        }

        public bool HighlightSpecialDates { get; set; }

        public IList<DateTime> ExactWorkdays =>
            (this.navigator.ExactWorkdays == null) ? null : new ObservableCollection<DateTime>(this.navigator.ExactWorkdays);

        public IList<DateTime> Holidays =>
            (this.navigator.Holidays == null) ? null : new ObservableCollection<DateTime>(this.navigator.Holidays);

        public bool ScrollSelection =>
            false;

        public IList<DayOfWeek> Workdays =>
            (this.navigator.Workdays == null) ? null : new ObservableCollection<DayOfWeek>(this.navigator.Workdays);

        public IList<DateTime> DisabledDates =>
            (this.navigator.DisabledDates == null) ? null : new ObservableCollection<DateTime>(this.navigator.DisabledDates);

        public IList<DateTime> SpecialDates =>
            (this.navigator.SpecialDates == null) ? null : new ObservableCollection<DateTime>(this.navigator.SpecialDates);
    }
}

