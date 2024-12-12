namespace DevExpress.Xpf.Core.FilteringUI
{
    using System;

    internal sealed class FilterSelector<T>
    {
        public readonly Tree<T, string>[] Available;
        public readonly Func<CriteriaOperator, T> SelectItem;

        public FilterSelector(Tree<T, string>[] available, Func<CriteriaOperator, T> selectItem)
        {
            if ((available == null) || (selectItem == null))
            {
                throw new ArgumentNullException();
            }
            this.Available = available;
            this.SelectItem = selectItem;
        }
    }
}

