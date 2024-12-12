namespace DevExpress.XtraPrinting.Export.Text
{
    using DevExpress.XtraPrinting.Export;
    using System;
    using System.Collections;
    using System.Reflection;

    internal abstract class TextLayoutMap
    {
        private ArrayList list = new ArrayList();

        protected TextLayoutMap(LayoutControlCollection layoutControls)
        {
            if (layoutControls.Count != 0)
            {
                ArrayList list = new ArrayList(layoutControls);
                list.Sort(this.PrimaryControlComparer);
                this.FillLayoutMap(list);
                this.DecomposeLayoutGroups();
                this.SortLayoutGroups();
            }
        }

        private TextLayoutGroup Add()
        {
            TextLayoutGroup group = new TextLayoutGroup(false);
            this.list.Add(group);
            return group;
        }

        protected abstract void DecomposeCompoundControl(int groupIndex, ref int index, TextBrickViewData control);
        private void DecomposeLayoutGroups()
        {
            for (int i = 0; i < this.Count; i++)
            {
                TextLayoutGroup group = this[i];
                if (!group.Decomposed)
                {
                    for (int j = 0; j < group.Count; j++)
                    {
                        TextBrickViewData owner = group[j].Owner;
                        if (owner.IsCompoundControl)
                        {
                            this.DecomposeCompoundControl(i, ref j, owner);
                        }
                    }
                }
            }
        }

        private bool FillLayoutGroup(TextLayoutGroup group, ArrayList layoutControls, ref int baseIndex)
        {
            TextBrickViewData data = (TextBrickViewData) layoutControls[baseIndex];
            group.Add(data.GetLayoutItem(0));
            for (int i = baseIndex + 1; i < layoutControls.Count; i++)
            {
                TextBrickViewData control = (TextBrickViewData) layoutControls[i];
                if (!this.ItemInGroup(control, group))
                {
                    baseIndex = i;
                    return false;
                }
                group.Add(control.GetLayoutItem(0));
            }
            return true;
        }

        private void FillLayoutMap(ArrayList layoutControls)
        {
            TextLayoutGroup group;
            int baseIndex = 0;
            for (bool flag = false; !flag; flag = this.FillLayoutGroup(group, layoutControls, ref baseIndex))
            {
                group = this.Add();
            }
        }

        protected TextLayoutGroup GetGroup(int groupIndex) => 
            ((groupIndex >= this.Count) || !this[groupIndex].Decomposed) ? this.InsertGroup(groupIndex) : this[groupIndex];

        private TextLayoutGroup InsertGroup(int groupIndex)
        {
            TextLayoutGroup group = new TextLayoutGroup(true);
            if (groupIndex < this.Count)
            {
                this.list.Insert(groupIndex, group);
            }
            else
            {
                this.list.Add(group);
            }
            return group;
        }

        protected abstract bool ItemInGroup(TextBrickViewData control, TextLayoutGroup group);
        private void SortLayoutGroups()
        {
            for (int i = 0; i < this.Count; i++)
            {
                this[i].Sort(this.SecondaryItemComparer);
            }
        }

        protected abstract IComparer PrimaryControlComparer { get; }

        protected abstract IComparer SecondaryItemComparer { get; }

        public int Count =>
            this.list.Count;

        public TextLayoutGroup this[int index] =>
            (TextLayoutGroup) this.list[index];
    }
}

