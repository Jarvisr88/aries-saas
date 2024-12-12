namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Grid;
    using System;

    public class SkipInvisibleGridRowsEnumerator : GridRowsEnumerator
    {
        public SkipInvisibleGridRowsEnumerator(DataViewBase view, NodeContainer containerItem) : base(view, containerItem)
        {
        }

        public override bool MoveNext()
        {
            if (!base.en.MoveNext())
            {
                return false;
            }
            bool flag2 = false;
            while (true)
            {
                while (true)
                {
                    if (base.en.Current != null)
                    {
                        RowNode current = base.en.Current;
                        if (!current.IsRowVisible)
                        {
                            if (base.en.MoveNext())
                            {
                                break;
                            }
                            return false;
                        }
                    }
                    flag2 = true;
                    break;
                }
                if (flag2)
                {
                    return base.IsCurrentRowInTree();
                }
            }
        }
    }
}

