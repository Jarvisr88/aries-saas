namespace DevExpress.XtraPrinting.Export.Text
{
    using DevExpress.XtraPrinting.Export;
    using System;
    using System.Collections;

    internal class TextLayoutMapX : TextLayoutMap
    {
        private int[] columnWidthArray;

        public TextLayoutMapX(LayoutControlCollection layoutControls) : base(layoutControls)
        {
            this.columnWidthArray = new int[base.Count];
            int index = 0;
            while (index < base.Count)
            {
                this.columnWidthArray[index] = 0;
                int num2 = 0;
                while (true)
                {
                    if (num2 >= base[index].Count)
                    {
                        index++;
                        break;
                    }
                    this.columnWidthArray[index] = Math.Max(this.columnWidthArray[index], base[index][num2].Text.Length);
                    num2++;
                }
            }
        }

        protected override void DecomposeCompoundControl(int groupIndex, ref int index, TextBrickViewData control)
        {
            for (int i = 1; i < control.Texts.Count; i++)
            {
                int num2 = index + 1;
                index = num2;
                base[groupIndex].Insert(num2, control.GetLayoutItem(i));
            }
        }

        public int FindAndRemove(TextBrickViewData control)
        {
            for (int i = 0; i < base.Count; i++)
            {
                if (base[i].Count > 0)
                {
                    int index = this.FindInGroup(base[i], control);
                    if (index != -1)
                    {
                        base[i].RemoveAt(index);
                        return i;
                    }
                }
            }
            throw new TextException();
        }

        private int FindInGroup(TextLayoutGroup group, TextBrickViewData control)
        {
            for (int i = group.StartIndex; i < group.Count; i++)
            {
                if (group[i] != null)
                {
                    TextBrickViewData owner = group[i].Owner;
                    if (this.InGroupComparer.Compare(owner, control) > 0)
                    {
                        break;
                    }
                    if (ReferenceEquals(owner, control))
                    {
                        return i;
                    }
                }
            }
            return -1;
        }

        protected override bool ItemInGroup(TextBrickViewData control, TextLayoutGroup group) => 
            control.Bounds.X <= group.MinCenterX;

        public int[] ColumnWidthArray =>
            this.columnWidthArray;

        private IComparer InGroupComparer =>
            TextBrickViewData.YComparer.Instance;

        protected override IComparer PrimaryControlComparer =>
            TextBrickViewData.XComparer.Instance;

        protected override IComparer SecondaryItemComparer =>
            TextLayoutItem.YComparer.Instance;
    }
}

