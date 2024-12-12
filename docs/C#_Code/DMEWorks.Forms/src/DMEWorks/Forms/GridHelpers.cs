namespace DMEWorks.Forms
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public static class GridHelpers
    {
        public static IEnumerable<T> Get<T>(this IEnumerable<DataGridViewRow> rows) where T: class
        {
            if (rows == null)
            {
                return null;
            }
            Func<DataGridViewRow, T> selector = <>c__1<T>.<>9__1_0;
            if (<>c__1<T>.<>9__1_0 == null)
            {
                Func<DataGridViewRow, T> local1 = <>c__1<T>.<>9__1_0;
                selector = <>c__1<T>.<>9__1_0 = r => r.Get<T>();
            }
            Func<T, bool> predicate = <>c__1<T>.<>9__1_1;
            if (<>c__1<T>.<>9__1_1 == null)
            {
                Func<T, bool> local2 = <>c__1<T>.<>9__1_1;
                predicate = <>c__1<T>.<>9__1_1 = r => r != null;
            }
            return rows.Select<DataGridViewRow, T>(selector).Where<T>(predicate);
        }

        public static T Get<T>(this DataGridViewRow row) where T: class
        {
            if (row != null)
            {
                return (row.DataBoundItem as T);
            }
            return default(T);
        }

        public static DataRow GetDataRow(this DataGridViewRow row)
        {
            DataRow dataBoundItem = row.DataBoundItem as DataRow;
            if (dataBoundItem != null)
            {
                return dataBoundItem;
            }
            DataRowView view = row.DataBoundItem as DataRowView;
            return view?.Row;
        }

        public static IEnumerable<DataRow> GetDataRows(this IEnumerable<DataGridViewRow> rows)
        {
            if (rows == null)
            {
                return null;
            }
            Func<DataGridViewRow, DataRow> selector = <>c.<>9__3_0;
            if (<>c.<>9__3_0 == null)
            {
                Func<DataGridViewRow, DataRow> local1 = <>c.<>9__3_0;
                selector = <>c.<>9__3_0 = r => r.GetDataRow();
            }
            Func<DataRow, bool> predicate = <>c.<>9__3_1;
            if (<>c.<>9__3_1 == null)
            {
                Func<DataRow, bool> local2 = <>c.<>9__3_1;
                predicate = <>c.<>9__3_1 = r => r != null;
            }
            return rows.Select<DataGridViewRow, DataRow>(selector).Where<DataRow>(predicate);
        }

        public static Table GetTableSource<Table>(this GridBase grid) where Table: DataTable
        {
            if (grid != null)
            {
                DataTableGridSource gridSource = grid.GridSource as DataTableGridSource;
                if (gridSource != null)
                {
                    return (gridSource.Table as Table);
                }
            }
            return default(Table);
        }

        public static DataTableGridSource ToGridSource(this DataTable table) => 
            (table != null) ? new DataTableGridSource(table) : null;

        public static ArrayGridSource<T> ToGridSource<T>(this IEnumerable<T> collection, params string[] properties) => 
            (collection != null) ? new ArrayGridSource<T>(collection, properties) : null;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly GridHelpers.<>c <>9 = new GridHelpers.<>c();
            public static Func<DataGridViewRow, DataRow> <>9__3_0;
            public static Func<DataRow, bool> <>9__3_1;

            internal DataRow <GetDataRows>b__3_0(DataGridViewRow r) => 
                r.GetDataRow();

            internal bool <GetDataRows>b__3_1(DataRow r) => 
                r != null;
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__1<T> where T: class
        {
            public static readonly GridHelpers.<>c__1<T> <>9;
            public static Func<DataGridViewRow, T> <>9__1_0;
            public static Func<T, bool> <>9__1_1;

            static <>c__1()
            {
                GridHelpers.<>c__1<T>.<>9 = new GridHelpers.<>c__1<T>();
            }

            internal T <Get>b__1_0(DataGridViewRow r) => 
                r.Get<T>();

            internal bool <Get>b__1_1(T r) => 
                r != null;
        }
    }
}

