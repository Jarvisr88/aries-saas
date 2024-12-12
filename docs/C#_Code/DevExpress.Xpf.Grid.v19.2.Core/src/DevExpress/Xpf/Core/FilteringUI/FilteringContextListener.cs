namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.Runtime.CompilerServices;

    internal class FilteringContextListener
    {
        public readonly Action FilterChanged;
        public readonly Action DataSourceChanged;
        public readonly Action ColumnUnboundChanged;
        public readonly Action ListChanged;
        public readonly Action EndDataUpdate;
        public readonly Action<string> ColumnAddedRemoved;
        public readonly Action ColumnsReset;
        public readonly Action<string> RoundDateChanged;
        public readonly Action<string> GroupFieldsChanged;
        public readonly Action<string> EditSettingsChanged;
        public readonly Action FormatConditionsChanged;

        public FilteringContextListener(Action filterChanged) : this(filterChanged, action1, <>c.<>9__11_1 ??= delegate {
        }, <>c.<>9__11_2 ??= delegate {
        }, <>c.<>9__11_3 ??= delegate {
        }, <>c.<>9__11_4 ??= delegate (string _) {
        }, <>c.<>9__11_5 ??= delegate {
        }, <>c.<>9__11_6 ??= delegate (string _) {
        }, <>c.<>9__11_7 ??= delegate (string _) {
        }, <>c.<>9__11_8 ??= delegate (string _) {
        }, <>c.<>9__11_9 ??= delegate {
        })
        {
            Action action1 = <>c.<>9__11_0;
            if (<>c.<>9__11_0 == null)
            {
                Action local1 = <>c.<>9__11_0;
                action1 = <>c.<>9__11_0 = delegate {
                };
            }
        }

        public FilteringContextListener(Action filterChanged, Action dataSourceChanged, Action columnUnboundChanged, Action listChanged, Action endDataUpdate, Action<string> columnAddedRemoved, Action columnsReset, Action<string> roundDateChanged, Action<string> groupFieldsChanged, Action<string> editSettingsChanged, Action formatConditionsChanged)
        {
            GuardHelper.ArgumentNotNull(filterChanged, "filterChanged");
            GuardHelper.ArgumentNotNull(dataSourceChanged, "dataSourceChanged");
            GuardHelper.ArgumentNotNull(listChanged, "listChanged");
            GuardHelper.ArgumentNotNull(columnAddedRemoved, "columnAddedRemoved");
            GuardHelper.ArgumentNotNull(columnsReset, "columnsReset");
            GuardHelper.ArgumentNotNull(columnUnboundChanged, "columnUnboundChanged");
            GuardHelper.ArgumentNotNull(roundDateChanged, "roundDateChanged");
            GuardHelper.ArgumentNotNull(groupFieldsChanged, "groupFieldsChanged");
            GuardHelper.ArgumentNotNull(editSettingsChanged, "editSettingsChanged");
            GuardHelper.ArgumentNotNull(formatConditionsChanged, "formatConditionsChanged");
            this.FilterChanged = filterChanged;
            this.DataSourceChanged = dataSourceChanged;
            this.ListChanged = listChanged;
            this.EndDataUpdate = endDataUpdate;
            this.ColumnAddedRemoved = columnAddedRemoved;
            this.ColumnsReset = columnsReset;
            this.ColumnUnboundChanged = columnUnboundChanged;
            this.RoundDateChanged = roundDateChanged;
            this.GroupFieldsChanged = groupFieldsChanged;
            this.EditSettingsChanged = editSettingsChanged;
            this.FormatConditionsChanged = formatConditionsChanged;
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly FilteringContextListener.<>c <>9 = new FilteringContextListener.<>c();
            public static Action <>9__11_0;
            public static Action <>9__11_1;
            public static Action <>9__11_2;
            public static Action <>9__11_3;
            public static Action<string> <>9__11_4;
            public static Action <>9__11_5;
            public static Action<string> <>9__11_6;
            public static Action<string> <>9__11_7;
            public static Action<string> <>9__11_8;
            public static Action <>9__11_9;

            internal void <.ctor>b__11_0()
            {
            }

            internal void <.ctor>b__11_1()
            {
            }

            internal void <.ctor>b__11_2()
            {
            }

            internal void <.ctor>b__11_3()
            {
            }

            internal void <.ctor>b__11_4(string _)
            {
            }

            internal void <.ctor>b__11_5()
            {
            }

            internal void <.ctor>b__11_6(string _)
            {
            }

            internal void <.ctor>b__11_7(string _)
            {
            }

            internal void <.ctor>b__11_8(string _)
            {
            }

            internal void <.ctor>b__11_9()
            {
            }
        }
    }
}

