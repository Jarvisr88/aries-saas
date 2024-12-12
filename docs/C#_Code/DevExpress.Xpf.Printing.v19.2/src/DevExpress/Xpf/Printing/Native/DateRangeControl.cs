namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Mvvm.UI.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.XtraReports.Parameters;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;

    public class DateRangeControl : Control<DateRangeControl>
    {
        public static readonly DependencyProperty StartDateProperty;
        public static readonly DependencyProperty EndDateProperty;
        public static readonly DependencyProperty MinDateProperty;
        public static readonly DependencyProperty MaxDateProperty;
        public static readonly DependencyProperty RangeProperty;
        public static readonly DependencyProperty PredefinedRangesProperty;
        public static readonly DependencyProperty SelectedPredefinedRangeProperty;
        public static RoutedEvent RangeChangedEvent;
        private readonly Locker locker = new Locker();

        public event RoutedEventHandler RangeChanged
        {
            add
            {
                base.AddHandler(RangeChangedEvent, value);
            }
            remove
            {
                base.RemoveHandler(RangeChangedEvent, value);
            }
        }

        static DateRangeControl()
        {
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(DateRangeControl), "d");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            FrameworkPropertyMetadataOptions? frameworkOptions = null;
            DependencyPropertyRegistrator<DateRangeControl> registrator1 = DependencyPropertyRegistrator<DateRangeControl>.New().Register<DateTime>(System.Linq.Expressions.Expression.Lambda<Func<DateRangeControl, DateTime>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DateRangeControl.get_MinValue)), parameters), out MinDateProperty, DateTime.MinValue, d => d.OnMinDateChanged(), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(DateRangeControl), "d");
            ParameterExpression[] expressionArray2 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<DateRangeControl> registrator2 = registrator1.Register<DateTime>(System.Linq.Expressions.Expression.Lambda<Func<DateRangeControl, DateTime>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DateRangeControl.get_MaxValue)), expressionArray2), out MaxDateProperty, DateTime.MaxValue, d => d.OnMaxDateChanged(), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(DateRangeControl), "d");
            ParameterExpression[] expressionArray3 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<DateRangeControl> registrator3 = registrator2.Register<DateTime>(System.Linq.Expressions.Expression.Lambda<Func<DateRangeControl, DateTime>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DateRangeControl.get_StartDate)), expressionArray3), out StartDateProperty, DateTime.Now.Date, d => d.OnStartDateChanged(), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(DateRangeControl), "d");
            ParameterExpression[] expressionArray4 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<DateRangeControl> registrator4 = registrator3.Register<DateTime>(System.Linq.Expressions.Expression.Lambda<Func<DateRangeControl, DateTime>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DateRangeControl.get_EndDate)), expressionArray4), out EndDateProperty, DateTime.Now.Date, d => d.OnEndDateChanged(), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(DateRangeControl), "d");
            ParameterExpression[] expressionArray5 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<DateRangeControl> registrator5 = registrator4.Register<IDictionary<string, Range<DateTime>>>(System.Linq.Expressions.Expression.Lambda<Func<DateRangeControl, IDictionary<string, Range<DateTime>>>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DateRangeControl.get_PredefinedRanges)), expressionArray5), out PredefinedRangesProperty, null, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(DateRangeControl), "d");
            ParameterExpression[] expressionArray6 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<DateRangeControl> registrator6 = registrator5.Register<Range<DateTime>>(System.Linq.Expressions.Expression.Lambda<Func<DateRangeControl, Range<DateTime>>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DateRangeControl.get_Range)), expressionArray6), out RangeProperty, new Range<DateTime>(DateTime.Now, DateTime.Now), d => d.OnRangeChanged(), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(DateRangeControl), "d");
            ParameterExpression[] expressionArray7 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            registrator6.Register<string>(System.Linq.Expressions.Expression.Lambda<Func<DateRangeControl, string>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DateRangeControl.get_SelectedPredefinedRange)), expressionArray7), out SelectedPredefinedRangeProperty, null, d => d.OnSelectedPredefinedRangeChanged(), frameworkOptions);
            RangeChangedEvent = EventManager.RegisterRoutedEvent("SelectionEnded", RoutingStrategy.Direct, typeof(RoutedEventHandler), typeof(DateRangeControl));
        }

        public DateRangeControl()
        {
            Func<KeyValuePair<string, Func<Range<DateTime>>>, string> keySelector = <>c.<>9__34_0;
            if (<>c.<>9__34_0 == null)
            {
                Func<KeyValuePair<string, Func<Range<DateTime>>>, string> local1 = <>c.<>9__34_0;
                keySelector = <>c.<>9__34_0 = x => x.Key;
            }
            this.PredefinedRanges = RangeParameterEditorOptions.PredefinedDateRanges.ToDictionary<KeyValuePair<string, Func<Range<DateTime>>>, string, Range<DateTime>>(keySelector, <>c.<>9__34_1 ??= x => x.Value());
            this.SelectedPredefinedRange = null;
        }

        private void OnEndDateChanged()
        {
            this.locker.DoLockedActionIfNotLocked(delegate {
                this.StartDate = new DateTime(Math.Min(this.StartDate.Ticks, this.EndDate.Ticks));
                this.Range = new Range<DateTime>(this.StartDate, this.EndDate);
                this.UpdatePredefinedRangeState();
                this.RaiseRangeChanged();
            });
        }

        private void OnMaxDateChanged()
        {
            this.EndDate = new DateTime(Math.Min(this.EndDate.Ticks, this.MaxValue.Ticks));
        }

        private void OnMinDateChanged()
        {
            this.StartDate = new DateTime(Math.Max(this.StartDate.Ticks, this.MinValue.Ticks));
        }

        protected virtual void OnRangeChanged()
        {
            this.SetRange(this.Range);
        }

        private void OnSelectedPredefinedRangeChanged()
        {
            Range<DateTime> range;
            if ((this.SelectedPredefinedRange != null) && this.PredefinedRanges.TryGetValue(this.SelectedPredefinedRange, out range))
            {
                this.SetRange(range);
            }
        }

        private void OnStartDateChanged()
        {
            this.locker.DoLockedActionIfNotLocked(delegate {
                this.EndDate = new DateTime(Math.Max(this.StartDate.Ticks, this.EndDate.Ticks));
                this.Range = new Range<DateTime>(this.StartDate, this.EndDate);
                this.UpdatePredefinedRangeState();
                this.RaiseRangeChanged();
            });
        }

        private void RaiseRangeChanged()
        {
            base.RaiseEvent(new RoutedEventArgs(RangeChangedEvent));
        }

        private void SetRange(Range<DateTime> range)
        {
            this.locker.DoLockedActionIfNotLocked(delegate {
                this.Range = range;
                this.StartDate = range.Start;
                this.EndDate = range.End;
                this.UpdatePredefinedRangeState();
                this.RaiseRangeChanged();
            });
        }

        private bool TryGetRangeFromPredefinedRanges(out string name)
        {
            bool flag;
            using (IEnumerator<KeyValuePair<string, Range<DateTime>>> enumerator = this.PredefinedRanges.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        KeyValuePair<string, Range<DateTime>> current = enumerator.Current;
                        Range<DateTime> range = current.Value;
                        if (!range.Equals(this.Range))
                        {
                            continue;
                        }
                        name = current.Key;
                        flag = true;
                    }
                    else
                    {
                        name = null;
                        return false;
                    }
                    break;
                }
            }
            return flag;
        }

        private void UpdatePredefinedRangeState()
        {
            string str;
            this.SelectedPredefinedRange = this.TryGetRangeFromPredefinedRanges(out str) ? str : null;
        }

        public DateTime StartDate
        {
            get => 
                (DateTime) base.GetValue(StartDateProperty);
            set => 
                base.SetValue(StartDateProperty, value);
        }

        public DateTime EndDate
        {
            get => 
                (DateTime) base.GetValue(EndDateProperty);
            set => 
                base.SetValue(EndDateProperty, value);
        }

        public DateTime MinValue
        {
            get => 
                (DateTime) base.GetValue(MinDateProperty);
            set => 
                base.SetValue(MinDateProperty, value);
        }

        public DateTime MaxValue
        {
            get => 
                (DateTime) base.GetValue(MaxDateProperty);
            set => 
                base.SetValue(MaxDateProperty, value);
        }

        public Range<DateTime> Range
        {
            get => 
                (Range<DateTime>) base.GetValue(RangeProperty);
            set => 
                base.SetValue(RangeProperty, value);
        }

        public IDictionary<string, Range<DateTime>> PredefinedRanges
        {
            get => 
                (IDictionary<string, Range<DateTime>>) base.GetValue(PredefinedRangesProperty);
            set => 
                base.SetValue(PredefinedRangesProperty, value);
        }

        public string SelectedPredefinedRange
        {
            get => 
                (string) base.GetValue(SelectedPredefinedRangeProperty);
            set => 
                base.SetValue(SelectedPredefinedRangeProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DateRangeControl.<>c <>9 = new DateRangeControl.<>c();
            public static Func<KeyValuePair<string, Func<Range<DateTime>>>, string> <>9__34_0;
            public static Func<KeyValuePair<string, Func<Range<DateTime>>>, Range<DateTime>> <>9__34_1;

            internal void <.cctor>b__35_0(DateRangeControl d)
            {
                d.OnMinDateChanged();
            }

            internal void <.cctor>b__35_1(DateRangeControl d)
            {
                d.OnMaxDateChanged();
            }

            internal void <.cctor>b__35_2(DateRangeControl d)
            {
                d.OnStartDateChanged();
            }

            internal void <.cctor>b__35_3(DateRangeControl d)
            {
                d.OnEndDateChanged();
            }

            internal void <.cctor>b__35_4(DateRangeControl d)
            {
                d.OnRangeChanged();
            }

            internal void <.cctor>b__35_5(DateRangeControl d)
            {
                d.OnSelectedPredefinedRangeChanged();
            }

            internal string <.ctor>b__34_0(KeyValuePair<string, Func<Range<DateTime>>> x) => 
                x.Key;

            internal Range<DateTime> <.ctor>b__34_1(KeyValuePair<string, Func<Range<DateTime>>> x) => 
                x.Value();
        }
    }
}

