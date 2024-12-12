namespace DevExpress.Xpf.Grid
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    internal static class BandWalker
    {
        public static Func<DataControlBase, BandBase> CreateBandCloneAccessor(BandBase band)
        {
            if ((band == null) || (band.DataControl == null))
            {
                return CreateEmptyAccessor<BandBase>();
            }
            if (band.IsServiceColumn())
            {
                return CreateServiceColumnAccessor<BandBase>(band, band.DataControl);
            }
            int[] indexes = GetIndexes(band);
            return dc => GetBandByIndexes(dc, indexes);
        }

        public static Func<DataControlBase, ColumnBase> CreateColumnCloneAccessor(ColumnBase column)
        {
            if ((column == null) || (column.OwnerControl == null))
            {
                return CreateEmptyAccessor<ColumnBase>();
            }
            if (column.IsServiceColumn())
            {
                return CreateServiceColumnAccessor<ColumnBase>(column, column.OwnerControl);
            }
            BandBase parentBand = column.ParentBand;
            if (parentBand == null)
            {
                return CreateEmptyAccessor<ColumnBase>();
            }
            int index = parentBand.ColumnsCore.GetCachedIndex(column);
            if (index < 0)
            {
                return CreateEmptyAccessor<ColumnBase>();
            }
            Func<DataControlBase, BandBase> bandCloneAccessor = CreateBandCloneAccessor(parentBand);
            return delegate (DataControlBase dc) {
                BandBase base2 = bandCloneAccessor(dc);
                return ((base2 != null) ? (base2.ColumnsCore[index] as ColumnBase) : null);
            };
        }

        private static Func<DataControlBase, T> CreateEmptyAccessor<T>() where T: class => 
            <>c__3<T>.<>9__3_0 ??= dc => default(T);

        private static Func<DataControlBase, T> CreateServiceColumnAccessor<T>(T column, DataControlBase originalControl) where T: BaseColumn => 
            delegate (DataControlBase dc) {
                if (ReferenceEquals(dc, originalControl))
                {
                    return column;
                }
                return default(T);
            };

        private static BandBase GetBandByIndexes(DataControlBase dataControl, int[] indexes)
        {
            Stack<int> stack = new Stack<int>(indexes);
            IBandsCollection bandsCore = dataControl.BandsCore;
            BandBase base2 = null;
            while (stack.Count > 0)
            {
                int num = stack.Pop();
                base2 = (BandBase) bandsCore[num];
                bandsCore = base2.BandsCore;
            }
            return base2;
        }

        internal static int[] GetIndexes(BandBase band)
        {
            List<int> list = new List<int>();
            for (BandBase base2 = band; base2 != null; base2 = base2.ParentBand)
            {
                int item = -1;
                if (base2.Owner != null)
                {
                    item = base2.Owner.BandsCore.GetCachedIndex(base2);
                }
                if (item < 0)
                {
                    return new int[0];
                }
                list.Add(item);
            }
            return list.ToArray();
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__3<T> where T: class
        {
            public static readonly BandWalker.<>c__3<T> <>9;
            public static Func<DataControlBase, T> <>9__3_0;

            static <>c__3()
            {
                BandWalker.<>c__3<T>.<>9 = new BandWalker.<>c__3<T>();
            }

            internal T <CreateEmptyAccessor>b__3_0(DataControlBase dc) => 
                default(T);
        }
    }
}

