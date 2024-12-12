namespace DevExpress.Xpf.Core
{
    using System;
    using System.Runtime.InteropServices;

    public class ColumnPanelLayoutCalculator : IColumnPanelLayoutCalculator
    {
        private void CheckInputParameters(Size<int>[] childSizes, int columnCount, ref int[] columnOffsets, ref int[] rowOffsets)
        {
            int num;
            int num2;
            this.GetFieldSize(childSizes, columnCount, out num, out num2);
            this.CheckOffsetParameter(num, ref columnOffsets);
            this.CheckOffsetParameter(num2, ref rowOffsets);
        }

        private void CheckOffsetParameter(int itemCount, ref int[] itemOffsets)
        {
            if ((itemOffsets == null) || ((itemOffsets.Length == 0) || ((itemOffsets.Length != (itemCount - 1)) && (itemOffsets.Length != 1))))
            {
                throw new Exception();
            }
            if (itemOffsets.Length < (itemCount - 1))
            {
                int num = itemOffsets[0];
                itemOffsets = new int[itemCount - 1];
                for (int i = 0; i < (itemCount - 1); i++)
                {
                    itemOffsets[i] = num;
                }
            }
        }

        void IColumnPanelLayoutCalculator.CalcLayout(Size<int>[] childSizes, int columnCount, int[] columnOffsets, int[] rowOffsets, out Size<int> panelSize, out Point<int>[] childPositions)
        {
            int num;
            int num2;
            this.CheckInputParameters(childSizes, columnCount, ref columnOffsets, ref rowOffsets);
            Size<int> size = new Size<int>();
            for (int i = 0; i < childSizes.Length; i++)
            {
                size.Width = Math.Max(childSizes[i].Width, size.Width);
                size.Height = Math.Max(childSizes[i].Height, size.Height);
            }
            this.GetFieldSize(childSizes, columnCount, out num, out num2);
            panelSize = new Size<int>(this.GetItemSequenceSize(size.Width, columnOffsets, num), this.GetItemSequenceSize(size.Height, rowOffsets, num2));
            childPositions = new Point<int>[childSizes.Length];
            int num3 = 0;
            int num4 = 0;
            int num5 = 0;
            for (int j = 0; j < childSizes.Length; j++)
            {
                if (childSizes[j].IsEmpty)
                {
                    childPositions[j] = new Point<int>();
                }
                else
                {
                    childPositions[j] = new Point<int>(num3 + ((size.Width - childSizes[j].Width) / 2), num4 + ((size.Height - childSizes[j].Height) / 2));
                    if (num > 0)
                    {
                        if ((num5 % num) != (num - 1))
                        {
                            num3 += size.Width + columnOffsets[num5 % num];
                        }
                        else
                        {
                            num3 = 0;
                            num4 += size.Height;
                            if (num5 < (childSizes.Length - 1))
                            {
                                num4 += rowOffsets[num5 / num];
                            }
                        }
                    }
                    num5++;
                }
            }
        }

        private void GetFieldSize(Size<int>[] childSizes, int columnCount, out int realColumnCount, out int rowCount)
        {
            int visibleChildCount = this.GetVisibleChildCount(childSizes);
            realColumnCount = Math.Min(visibleChildCount, columnCount);
            if (realColumnCount == 0)
            {
                rowCount = 0;
            }
            else
            {
                rowCount = ((visibleChildCount + realColumnCount) - 1) / realColumnCount;
            }
        }

        private int GetItemSequenceSize(int itemSize, int[] itemOffsets, int itemCount)
        {
            int num = itemSize * itemCount;
            for (int i = 0; i < (itemCount - 1); i++)
            {
                num += itemOffsets[i];
            }
            return num;
        }

        private int GetVisibleChildCount(Size<int>[] childSizes)
        {
            int num = 0;
            foreach (Size<int> size in childSizes)
            {
                if (!size.IsEmpty)
                {
                    num++;
                }
            }
            return num;
        }
    }
}

