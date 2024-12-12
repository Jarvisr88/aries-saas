namespace DevExpress.Xpf.Layout.Core.Selection
{
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class SelectionInfo<T> : IDisposable
    {
        private bool isDisposing;
        private SelectionChangingHandler<T> SelectionChanging;
        private SelectionChangedHandler<T> SelectionChanged;
        private RequestSelectionRangeHandler<T> RequestSelectionRange;

        public SelectionInfo(SelectionChangingHandler<T> selectionChanging, SelectionChangedHandler<T> selectionChanged, RequestSelectionRangeHandler<T> requestSelectionRange)
        {
            this.SelectionChanged = selectionChanged;
            this.SelectionChanging = selectionChanging;
            this.RequestSelectionRange = requestSelectionRange;
            this.Mode = SelectionMode.SingleItem;
            this.Selection = new Dictionary<object, T>();
        }

        protected bool CanSelectionChanging(T element, bool selected) => 
            (this.SelectionChanging == null) || this.SelectionChanging(element, selected);

        protected void ClearSelection()
        {
            T[] array = new T[this.Selection.Count];
            this.Selection.Values.CopyTo(array, 0);
            for (int i = 0; i < array.Length; i++)
            {
                T element = array[i];
                if (this.CanSelectionChanging(element, false))
                {
                    this.UnSelectElementCore(element);
                }
            }
        }

        protected void ClearSelection(T element)
        {
            T[] array = new T[this.Selection.Count];
            this.Selection.Values.CopyTo(array, 0);
            for (int i = 0; i < array.Length; i++)
            {
                T local = array[i];
                if (Equals(SelectionHelper.GetItem<T>(local), SelectionHelper.GetItem<T>(element)))
                {
                    this.Selection[SelectionHelper.GetItem<T>(element)] = element;
                }
                else if (this.CanSelectionChanging(local, false))
                {
                    this.UnSelectElementCore(local);
                }
            }
        }

        protected void ClearSelection(T[] range)
        {
            T[] array = new T[this.Selection.Count];
            this.Selection.Values.CopyTo(array, 0);
            object[] items = SelectionHelper.GetItems<T>(range);
            for (int i = 0; i < array.Length; i++)
            {
                T element = array[i];
                int index = Array.IndexOf<object>(items, SelectionHelper.GetItem<T>(element));
                if (index != -1)
                {
                    T local2 = range[index];
                    this.Selection[SelectionHelper.GetItem<T>(local2)] = local2;
                }
                else if (this.CanSelectionChanging(element, false))
                {
                    this.UnSelectElementCore(element);
                }
            }
        }

        public void Dispose()
        {
            if (!this.isDisposing)
            {
                this.isDisposing = true;
                this.SelectionChanged = null;
                this.RequestSelectionRange = null;
                this.ClearSelection();
                this.Selection = null;
            }
            GC.SuppressFinalize(this);
        }

        public bool GetSelectedState(T element)
        {
            object item = SelectionHelper.GetItem<T>(element);
            return ((item != null) ? this.Selection.ContainsKey(item) : true);
        }

        protected T[] RaiseRequestSelection(T first, T last)
        {
            T[] localArray = new T[0];
            if (this.RequestSelectionRange != null)
            {
                localArray = this.RequestSelectionRange(first, last);
            }
            return localArray;
        }

        protected void RaiseSelectionChanged(T element, bool selected)
        {
            if (this.SelectionChanged != null)
            {
                this.SelectionChanged(element, selected);
            }
        }

        public void Select(T element)
        {
            if (element == null)
            {
                this.ClearSelection();
            }
            else
            {
                bool selectedState = this.GetSelectedState(element);
                switch (this.Mode)
                {
                    case SelectionMode.SingleItem:
                        this.ClearSelection(element);
                        if (!selectedState && this.CanSelectionChanging(element, true))
                        {
                            this.SelectElementCore(element);
                        }
                        break;

                    case SelectionMode.MultipleItems:
                        if (this.CanSelectionChanging(element, !selectedState))
                        {
                            if (!selectedState)
                            {
                                this.SelectElementCore(element);
                            }
                            else
                            {
                                this.UnSelectElementCore(element);
                            }
                        }
                        break;

                    case SelectionMode.ItemRange:
                    {
                        T[] range = this.RequestSelectionRange(this.LastSelectedElement, element);
                        this.ClearSelection(range);
                        for (int i = 0; i < range.Length; i++)
                        {
                            T local = range[i];
                            if (!this.GetSelectedState(local) && this.CanSelectionChanging(local, true))
                            {
                                this.SelectElementCore(local);
                            }
                        }
                        break;
                    }
                    default:
                        break;
                }
                this.LastSelectedElement = element;
            }
        }

        private void SelectElementCore(T element)
        {
            this.Selection.Add(SelectionHelper.GetItem<T>(element), element);
            this.RaiseSelectionChanged(element, true);
        }

        private void UnSelectElementCore(T element)
        {
            this.Selection.Remove(SelectionHelper.GetItem<T>(element));
            this.RaiseSelectionChanged(element, false);
        }

        public SelectionMode Mode { get; set; }

        private IDictionary<object, T> Selection { get; set; }

        public T LastSelectedElement { get; private set; }
    }
}

