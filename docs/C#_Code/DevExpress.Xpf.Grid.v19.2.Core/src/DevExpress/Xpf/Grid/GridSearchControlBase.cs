namespace DevExpress.Xpf.Grid
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Data;

    [DXToolboxBrowsable(false)]
    public class GridSearchControlBase : SearchControl
    {
        public static readonly DependencyProperty ViewProperty;

        static GridSearchControlBase()
        {
            Type ownerType = typeof(GridSearchControlBase);
            ViewProperty = DependencyPropertyManager.Register("View", typeof(DataViewBase), ownerType, new FrameworkPropertyMetadata(null, (d, e) => ((GridSearchControlBase) d).ViewChanged((DataViewBase) e.OldValue, (DataViewBase) e.NewValue)));
        }

        protected virtual void BindSearchPanel(DataViewBase view)
        {
            Binding binding7 = new Binding {
                Source = view,
                Path = new PropertyPath(DataViewBase.SearchPanelFindFilterProperty.GetName(), new object[0])
            };
            Binding binding = binding7;
            base.SetBinding(SearchControl.FilterConditionProperty, binding);
            binding7 = new Binding {
                Source = view,
                Path = new PropertyPath(DataViewBase.SearchPanelParseModeProperty.GetName(), new object[0])
            };
            Binding binding2 = binding7;
            base.SetBinding(SearchControl.ParseModeProperty, binding2);
            binding7 = new Binding {
                Source = view,
                Path = new PropertyPath(DataViewBase.SearchPanelFindModeProperty.GetName(), new object[0])
            };
            Binding binding3 = binding7;
            base.SetBinding(SearchControl.FindModeProperty, binding3);
            binding7 = new Binding {
                Source = view,
                Path = new PropertyPath(DataViewBase.SearchDelayProperty.GetName(), new object[0])
            };
            Binding binding4 = binding7;
            base.SetBinding(SearchControl.SearchTextPostDelayProperty, binding4);
            binding7 = new Binding {
                Source = view,
                Path = new PropertyPath(DataViewBase.SearchPanelNullTextProperty.GetName(), new object[0]),
                Mode = BindingMode.TwoWay
            };
            Binding binding5 = binding7;
            base.SetBinding(SearchControl.NullTextProperty, binding5);
            binding7 = new Binding {
                Source = view,
                Path = new PropertyPath(DataViewBase.SearchStringProperty.GetName(), new object[0]),
                Mode = BindingMode.TwoWay
            };
            Binding binding6 = binding7;
            base.SetBinding(SearchControl.SearchTextProperty, binding6);
        }

        internal void FadeSearchPanel(Action completed)
        {
            base.BeginFadeAnimation(completed);
        }

        internal void FindCommandExecuted()
        {
            this.OnFindCommandExecuted();
        }

        internal void InitSearchInfo()
        {
            if ((this.View == null) || !this.View.ActualShowSearchPanelResultInfo)
            {
                this.SearchInfo = null;
            }
            else
            {
                this.SearchInfo = new SortedSet<int>();
                Func<int, bool> func = this.View.CreateFilterFitPredicate();
                if (func == null)
                {
                    base.ResultCount = 0;
                    base.ResultIndex = 0;
                    return;
                }
                int rowHandle = 0;
                while (true)
                {
                    if (rowHandle >= this.View.DataControl.DataProviderBase.DataRowCount)
                    {
                        base.ResultCount = this.SearchInfo.Count;
                        break;
                    }
                    if (!this.View.DataControl.DataProviderBase.IsFilteredByRowHandle(rowHandle) && func(rowHandle))
                    {
                        this.SearchInfo.Add(rowHandle);
                    }
                    rowHandle++;
                }
            }
            this.UpdateResultIndex();
        }

        protected void OnViewChanged(DataViewBase oldView, DataViewBase view)
        {
            if (oldView != null)
            {
                this.UnsubscribeViewEvens(oldView);
            }
            if (view != null)
            {
                view.SearchControl = this;
                this.BindSearchPanel(view);
            }
        }

        internal void SearchInfoChanged(ListChangedType changedType, int newHandle, int? oldRowHandle)
        {
            if ((this.View == null) || ((this.View.DataProviderBase == null) || !this.View.DataProviderBase.IsUpdateLocked))
            {
                if ((this.SearchInfo == null) || (oldRowHandle == null))
                {
                    this.InitSearchInfo();
                }
                else
                {
                    Func<int, bool> func = this.View.CreateFilterFitPredicate();
                    if (func == null)
                    {
                        this.InitSearchInfo();
                    }
                    else
                    {
                        switch (changedType)
                        {
                            case ListChangedType.ItemAdded:
                                if (func(newHandle))
                                {
                                    this.SearchInfo.Add(newHandle);
                                }
                                this.UpdateSearchInfo();
                                return;

                            case ListChangedType.ItemDeleted:
                                this.SearchInfo.Remove(oldRowHandle.Value);
                                this.UpdateSearchInfo();
                                return;

                            case ListChangedType.ItemMoved:
                                this.SearchInfo.Remove(oldRowHandle.Value);
                                if (func(newHandle))
                                {
                                    this.SearchInfo.Add(newHandle);
                                }
                                this.UpdateSearchInfo();
                                return;

                            case ListChangedType.ItemChanged:
                                this.SearchInfo.Remove(oldRowHandle.Value);
                                if (func(newHandle))
                                {
                                    this.SearchInfo.Add(newHandle);
                                }
                                this.UpdateSearchInfo();
                                return;
                        }
                        this.InitSearchInfo();
                    }
                }
            }
        }

        protected virtual void UnsubscribeViewEvens(DataViewBase oldView)
        {
            oldView.SearchControl = null;
        }

        internal void UpdateResultIndex()
        {
            if ((this.View == null) || ((this.SearchInfo == null) || (this.SearchInfo.Count == 0)))
            {
                base.ResultCount = 0;
                base.ResultIndex = 0;
            }
            else
            {
                int firstDataFocusedRowHandle = this.View.GetFirstDataFocusedRowHandle();
                SortedSet<int> viewBetween = this.SearchInfo.GetViewBetween(0, firstDataFocusedRowHandle);
                this.ResultIndex = (viewBetween.Count > 0) ? viewBetween.Count : 0;
            }
        }

        private void UpdateSearchInfo()
        {
            base.ResultCount = this.SearchInfo.Count;
            this.UpdateResultIndex();
        }

        private void ViewChanged(DataViewBase oldView, DataViewBase view)
        {
            this.OnViewChanged(oldView, view);
        }

        protected internal bool ClearButtonIsVisible { get; protected set; }

        internal virtual bool IsLogicControl =>
            true;

        public DataViewBase View
        {
            get => 
                (DataViewBase) base.GetValue(ViewProperty);
            set => 
                base.SetValue(ViewProperty, value);
        }

        protected override bool SaveMRUOnStringChanged =>
            false;

        internal SortedSet<int> SearchInfo { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly GridSearchControlBase.<>c <>9 = new GridSearchControlBase.<>c();

            internal void <.cctor>b__7_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((GridSearchControlBase) d).ViewChanged((DataViewBase) e.OldValue, (DataViewBase) e.NewValue);
            }
        }
    }
}

