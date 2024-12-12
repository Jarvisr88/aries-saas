namespace DevExpress.Xpf.Grid.EditForm
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class EditFormLayoutCalculator
    {
        private int columnCountCore = 1;

        public virtual void SetPositions(IEnumerable<IEditFormLayoutItem> items)
        {
            LayoutMatrix layoutMatrix = new LayoutMatrix(this.ColumnCount);
            Row nextRow = layoutMatrix.AddNewRow();
            using (IEnumerator<IEditFormLayoutItem> enumerator = items.GetEnumerator())
            {
                while (true)
                {
                    if (!enumerator.MoveNext())
                    {
                        break;
                    }
                    IEditFormLayoutItem current = enumerator.Current;
                    if (this.ValidateItem(current))
                    {
                        while (true)
                        {
                            if (nextRow.TryAddItem(current))
                            {
                                for (int i = 0; i < (current.RowSpan - 1); i++)
                                {
                                    layoutMatrix.GetNextRow(nextRow.Index + i).FillSlotRange(current);
                                }
                                break;
                            }
                            nextRow = layoutMatrix.GetNextRow(nextRow.Index);
                        }
                        continue;
                    }
                    return;
                }
            }
            this.UpdateLayoutSettings(layoutMatrix);
        }

        private void UpdateLayoutSettings(LayoutMatrix layoutMatrix)
        {
            int count = layoutMatrix.Count;
            if ((this.ColumnCount != this.LayoutSettings.ColumnCount) || (count != this.LayoutSettings.RowCount))
            {
                this.LayoutSettings = new EditFormLayoutSettings(this.ColumnCount, count);
            }
        }

        private bool ValidateItem(IEditFormLayoutItem item) => 
            (item.ColumnSpan > 0) && ((item.RowSpan > 0) && (item.ColumnSpan <= this.ColumnCount));

        public int ColumnCount
        {
            get => 
                this.columnCountCore;
            set
            {
                if (this.columnCountCore != value)
                {
                    this.columnCountCore = value;
                }
            }
        }

        public virtual EditFormLayoutSettings LayoutSettings { get; protected set; }
    }
}

