namespace DevExpress.Xpf.Editors.Helpers
{
    using DevExpress.Data.Filtering;
    using DevExpress.Mvvm.Native;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public abstract class CurrentDataView : PlainDataView
    {
        private readonly Dictionary<DisplayTextCacheItem, int> editTextToDisplayTextCache;
        private object visibleListWrapper;

        protected CurrentDataView(bool selectNullValue, object listSource, object handle, string valueMember, string displayMember) : base(selectNullValue, listSource, valueMember, displayMember)
        {
            this.editTextToDisplayTextCache = new Dictionary<DisplayTextCacheItem, int>();
            this.<Handle>k__BackingField = handle;
        }

        public virtual void CancelAsyncOperations()
        {
            this.ResetDisplayTextCache();
        }

        protected virtual ListChangedEventArgs ConvertListChangedEventArgs(ListChangedEventArgs e) => 
            e;

        private DisplayTextCacheItem CreateDisplayTextCacheItem(string text, bool autoComplete, int startIndex, bool searchNext, bool ignoreStartIndex)
        {
            DisplayTextCacheItem item1 = new DisplayTextCacheItem();
            item1.DisplayText = text;
            item1.AutoComplete = autoComplete;
            item1.StartIndex = startIndex;
            item1.SearchNext = searchNext;
            item1.IgnoreStartIndex = ignoreStartIndex;
            return item1;
        }

        protected abstract object CreateVisibleListWrapper();
        public void DestroyVisibleList()
        {
            IDisposable visibleListWrapper = this.visibleListWrapper as IDisposable;
            if (visibleListWrapper != null)
            {
                visibleListWrapper.Dispose();
            }
            this.visibleListWrapper = null;
        }

        protected override void DisposeInternal()
        {
            base.DisposeInternal();
            this.DestroyVisibleList();
        }

        public int FindItemIndexByText(string text, bool isCaseSensitive, bool allowTextInputSuggestions, int startItemIndex, bool searchNext, bool ignoreStartIndex)
        {
            int num;
            if (text == null)
            {
                return -1;
            }
            string str = isCaseSensitive ? text : text.ToLower();
            DisplayTextCacheItem key = this.CreateDisplayTextCacheItem(str, allowTextInputSuggestions, startItemIndex, searchNext, ignoreStartIndex);
            if (!this.editTextToDisplayTextCache.TryGetValue(key, out num))
            {
                num = this.FindItemIndexByTextInternal(str, isCaseSensitive, allowTextInputSuggestions, startItemIndex, searchNext, ignoreStartIndex);
                this.UpdateDisplayTextCache(str, allowTextInputSuggestions, startItemIndex, searchNext, num, ignoreStartIndex);
            }
            return ((num == -2147483638) ? -1 : num);
        }

        protected virtual int FindItemIndexByTextInternal(string text, bool isCaseSensitive, bool allowTextInputSuggestions, int startItemIndex, bool searchNext, bool ignoreStartIndex)
        {
            OperandProperty compareOperand = new OperandProperty(base.DataAccessor.DisplayPropertyName);
            return base.View.FindIndexByText(compareOperand, this.GetCompareCriteriaOperator(allowTextInputSuggestions && !string.IsNullOrEmpty(text), compareOperand, new OperandValue(text)), text, isCaseSensitive, startItemIndex, searchNext, ignoreStartIndex);
        }

        protected CriteriaOperator GetCompareCriteriaOperator(bool autoComplete, OperandProperty property, OperandValue value)
        {
            if (!autoComplete)
            {
                return new BinaryOperator(property, value, BinaryOperatorType.Equal);
            }
            CriteriaOperator[] operands = new CriteriaOperator[] { property, value };
            return new FunctionOperator(FunctionOperatorType.StartsWith, operands);
        }

        public override IEnumerator<DataProxy> GetEnumerator() => 
            base.View.GetEnumerator();

        protected internal object GetVisibleListInternal() => 
            this.visibleListWrapper;

        public override bool ProcessAddItem(int index)
        {
            this.ResetDisplayTextCache();
            return base.ProcessAddItem(index);
        }

        public virtual bool ProcessChangeFilter(CriteriaOperator filterCriteria) => 
            false;

        public override bool ProcessChangeItem(int index)
        {
            this.ResetDisplayTextCache();
            return base.ProcessChangeItem(index);
        }

        protected internal override bool ProcessChangeSource(ListChangedEventArgs e)
        {
            bool flag = base.ProcessChangeSource(e);
            if (flag)
            {
                this.RaiseListChanged(this.ConvertListChangedEventArgs(e));
            }
            return flag;
        }

        public override bool ProcessDeleteItem(int index)
        {
            this.ResetDisplayTextCache();
            return base.ProcessDeleteItem(index);
        }

        public virtual void ProcessFindIncrementalCompleted(string text, int startIndex, bool searchNext, bool ignoreStartIndex, object value)
        {
        }

        public virtual bool ProcessInconsistencyDetected()
        {
            this.ResetDisplayTextCache();
            return true;
        }

        public override bool ProcessMoveItem(int oldIndex, int newIndex)
        {
            this.ResetDisplayTextCache();
            return base.ProcessMoveItem(oldIndex, newIndex);
        }

        public virtual void ProcessRefreshed()
        {
            this.ResetDisplayTextCache();
        }

        public override bool ProcessReset()
        {
            this.ResetDisplayTextCache();
            return base.ProcessReset();
        }

        public virtual void ProcessRowLoaded(object value)
        {
        }

        public virtual void ResetDisplayTextCache()
        {
            this.editTextToDisplayTextCache.Clear();
        }

        public virtual void ResetVisibleList()
        {
            Action<IDisposable> action = <>c.<>9__30_0;
            if (<>c.<>9__30_0 == null)
            {
                Action<IDisposable> local1 = <>c.<>9__30_0;
                action = <>c.<>9__30_0 = x => x.Dispose();
            }
            (this.visibleListWrapper as IDisposable).Do<IDisposable>(action);
            this.visibleListWrapper = null;
        }

        protected virtual void UpdateDisplayTextCache(string findText, bool autoComplete, int startIndex, bool searchNext, int listSourceIndex, bool ignoreStartIndex)
        {
            this.editTextToDisplayTextCache[this.CreateDisplayTextCacheItem(findText, autoComplete, startIndex, searchNext, ignoreStartIndex)] = listSourceIndex;
        }

        public object Handle { get; }

        public object VisibleList
        {
            get
            {
                object visibleListWrapper = this.visibleListWrapper;
                if (this.visibleListWrapper == null)
                {
                    object local1 = this.visibleListWrapper;
                    visibleListWrapper = this.visibleListWrapper = this.CreateVisibleListWrapper();
                }
                return visibleListWrapper;
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly CurrentDataView.<>c <>9 = new CurrentDataView.<>c();
            public static Action<IDisposable> <>9__30_0;

            internal void <ResetVisibleList>b__30_0(IDisposable x)
            {
                x.Dispose();
            }
        }
    }
}

