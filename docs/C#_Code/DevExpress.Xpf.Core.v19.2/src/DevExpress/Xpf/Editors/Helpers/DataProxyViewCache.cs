namespace DevExpress.Xpf.Editors.Helpers
{
    using DevExpress.Data.Access;
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Filtering.Helpers;
    using DevExpress.Xpf.Editors.Native;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public abstract class DataProxyViewCache : IEnumerable<DataProxy>, IEnumerable
    {
        protected DataProxyViewCache(DevExpress.Xpf.Editors.Helpers.DataAccessor dataAccessor)
        {
            this.DataAccessor = dataAccessor;
        }

        public abstract void Add(int index, DataProxy item);
        protected virtual int FindIndexByItem(DataProxy item) => 
            item.f_visibleIndex;

        public abstract int FindIndexByText(CriteriaOperator compareOperand, CriteriaOperator compareOperator, string text, bool isCaseSensitive, int startItemIndex, bool searchNext, bool ignoreStartIndex);
        public virtual int FindIndexByTextLocal(CriteriaOperator compareOperator, bool isCaseSensitive, IEnumerable<DataProxy> view, int startItemIndex, bool searchNext, bool ignoreStartIndex)
        {
            int num;
            PropertyDescriptor[] properties = new PropertyDescriptor[] { new GetStringFromLookUpValuePropertyDescriptor(DataListDescriptor.GetFastProperty(TypeDescriptor.GetProperties(this.DataAccessor.ElementType).Find(this.DataAccessor.DisplayPropertyName, true))) };
            ExpressionEvaluator evaluator = new ExpressionEvaluator(new PropertyDescriptorCollection(properties), compareOperator, isCaseSensitive);
            using (IEnumerator<DataProxy> enumerator = (searchNext ? view : view.Reverse<DataProxy>()).GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        DataProxy current = enumerator.Current;
                        bool flag = this.Skip(startItemIndex, searchNext, ignoreStartIndex, current);
                        if (flag || !((bool) evaluator.Evaluate(current)))
                        {
                            continue;
                        }
                        num = this.FindIndexByItem(current);
                    }
                    else
                    {
                        return -1;
                    }
                    break;
                }
            }
            return num;
        }

        public abstract int FindIndexByValue(CriteriaOperator compareOperator, object value);
        public abstract IEnumerator<DataProxy> GetEnumerator();
        public abstract void Remove(int index);
        public abstract void Replace(int index, DataProxy item);
        public abstract void Reset();
        private bool Skip(int startItemIndex, bool searchNext, bool ignoreStartIndex, DataProxy item) => 
            this.SkipInternal(startItemIndex, searchNext, ignoreStartIndex, item);

        protected virtual bool SkipInternal(int startItemIndex, bool searchNext, bool ignoreStartIndex, DataProxy item) => 
            searchNext ? (ignoreStartIndex ? (item.f_visibleIndex <= startItemIndex) : (item.f_visibleIndex < startItemIndex)) : (ignoreStartIndex ? (item.f_visibleIndex >= startItemIndex) : (item.f_visibleIndex > startItemIndex));

        IEnumerator IEnumerable.GetEnumerator() => 
            this.GetEnumerator();

        protected DevExpress.Xpf.Editors.Helpers.DataAccessor DataAccessor { get; private set; }

        public abstract DataProxy this[int index] { get; set; }

        public abstract int Count { get; }
    }
}

