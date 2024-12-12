namespace DevExpress.Xpf.Grid
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    internal class ErrorWatch
    {
        private List<int> errorsHandles = new List<int>();
        private readonly DataViewBase View;

        public ErrorWatch(DataViewBase view)
        {
            this.View = view;
        }

        public static ErrorsWatchMode ActualErrorsWatchMode(DataViewBase view)
        {
            if (IsRootView(view))
            {
                ErrorsWatchMode errorsWatchModeCore = view.ErrorsWatchModeCore;
                return ((errorsWatchModeCore == ErrorsWatchMode.Default) ? ErrorsWatchMode.None : errorsWatchModeCore);
            }
            if (view.ErrorsWatchModeCore != ErrorsWatchMode.Default)
            {
                return view.ErrorsWatchModeCore;
            }
            DataControlBase masterGridCore = view.DataControl?.GetMasterGridCore();
            return (((masterGridCore == null) || (masterGridCore.DataView == null)) ? ErrorsWatchMode.Default : masterGridCore.DataView.ErrorsWatchModeCore);
        }

        private void AddOrDeleteIndexes(bool added, int newHandle, bool newError = false)
        {
            int index = this.errorsHandles.BinarySearch(newHandle);
            if (index < 0)
            {
                index = ~index;
            }
            else if (!added)
            {
                this.errorsHandles.RemoveAt(index);
            }
            for (int i = index; i < this.errorsHandles.Count; i++)
            {
                if (added)
                {
                    int num3 = i;
                    this.errorsHandles[num3] += 1;
                }
                else
                {
                    int num4 = i;
                    this.errorsHandles[num4] -= 1;
                }
            }
            if (newError)
            {
                this.errorsHandles.Add(newHandle);
                this.errorsHandles.Sort();
            }
        }

        public void CurrentRowChanged(ListChangedType changedType, int newHandle, int? oldRowHandle)
        {
            if (((this.View == null) || ((this.View.DataProviderBase == null) || !this.View.DataProviderBase.IsUpdateLocked)) && !this.IsNotWatchError)
            {
                ErrorsWatchMode mode = ActualErrorsWatchMode(this.View);
                if (!IsStop(mode))
                {
                    if (!this.View.HasErrors)
                    {
                        if (this.View.IsValidRowByRowHandle(newHandle, mode, !mode.HasFlag(ErrorsWatchMode.Cells)) != null)
                        {
                            this.errorsHandles.Add(newHandle);
                            this.View.SetHasErrors(true);
                        }
                    }
                    else if (oldRowHandle == null)
                    {
                        UpdateWatchErrorsView(this.View);
                    }
                    else
                    {
                        if (changedType == ListChangedType.ItemDeleted)
                        {
                            newHandle = oldRowHandle.Value;
                        }
                        bool newError = this.View.IsValidRowByRowHandle(newHandle, mode, !mode.HasFlag(ErrorsWatchMode.Cells)) != null;
                        switch (changedType)
                        {
                            case ListChangedType.ItemAdded:
                                if (this.View.DataProviderBase.DataRowCount == 1)
                                {
                                    this.errorsHandles.Clear();
                                }
                                this.AddOrDeleteIndexes(true, newHandle, newError);
                                break;

                            case ListChangedType.ItemDeleted:
                                this.AddOrDeleteIndexes(false, newHandle, false);
                                break;

                            case ListChangedType.ItemMoved:
                            case ListChangedType.ItemChanged:
                                this.AddOrDeleteIndexes(false, oldRowHandle.Value, false);
                                this.AddOrDeleteIndexes(true, newHandle, newError);
                                break;

                            default:
                                UpdateWatchErrorsView(this.View);
                                return;
                        }
                        this.View.SetHasErrors(this.errorsHandles.Count > 0);
                    }
                }
            }
        }

        private static bool IsRootView(DataViewBase view) => 
            ReferenceEquals(view.OriginationView, null);

        public static bool IsStop(ErrorsWatchMode mode) => 
            (mode == ErrorsWatchMode.None) || (mode == ErrorsWatchMode.Default);

        public void UpdateWatchErrors()
        {
            if (!this.IsNotWatchError)
            {
                if (IsStop(ActualErrorsWatchMode(this.View)))
                {
                    this.errorsHandles.Clear();
                    this.View.SetHasErrors(false);
                }
                else if ((this.View.DataControl != null) && (this.View.DataControl.DataProviderBase != null))
                {
                    if (this.View.RootView.DataControl.DetailDescriptorCore == null)
                    {
                        UpdateWatchErrorsView(this.View);
                    }
                    else
                    {
                        Action<DataControlBase> updateOpenDetailMethod = <>c.<>9__15_0;
                        if (<>c.<>9__15_0 == null)
                        {
                            Action<DataControlBase> local1 = <>c.<>9__15_0;
                            updateOpenDetailMethod = <>c.<>9__15_0 = delegate (DataControlBase dataControl) {
                                if ((dataControl != null) && (dataControl.DataView != null))
                                {
                                    UpdateWatchErrorsView(dataControl.DataView);
                                }
                            };
                        }
                        this.View.RootView.DataControl.UpdateAllDetailDataControls(updateOpenDetailMethod, null);
                    }
                }
            }
        }

        private static void UpdateWatchErrorsView(DataViewBase view)
        {
            view.ErrorWatch.errorsHandles.Clear();
            if ((view.DataControl == null) || (view.DataControl.DataProviderBase == null))
            {
                view.SetHasErrors(false);
            }
            else
            {
                ErrorsWatchMode mode = ActualErrorsWatchMode(view);
                if ((mode == ErrorsWatchMode.None) || (mode == ErrorsWatchMode.Default))
                {
                    view.SetHasErrors(false);
                }
                else
                {
                    for (int i = 0; i < view.DataControl.DataProviderBase.DataRowCount; i++)
                    {
                        ErrorsWatchMode? nullable = view.IsValidRowByRowHandle(i, mode, !mode.HasFlag(ErrorsWatchMode.Cells));
                        if (nullable != null)
                        {
                            view.ErrorWatch.errorsHandles.Add(i);
                        }
                    }
                    view.SetHasErrors(view.ErrorWatch.errorsHandles.Count > 0);
                }
            }
        }

        public bool GridLoaded { get; set; }

        public bool HasError =>
            this.errorsHandles.Count > 0;

        private bool IsNotWatchError =>
            (!this.View.IsRootView || this.GridLoaded) ? (this.View.DataProviderBase.IsAsyncServerMode || (this.View.DataProviderBase.IsServerMode || this.View.DataProviderBase.IsVirtualSource)) : true;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ErrorWatch.<>c <>9 = new ErrorWatch.<>c();
            public static Action<DataControlBase> <>9__15_0;

            internal void <UpdateWatchErrors>b__15_0(DataControlBase dataControl)
            {
                if ((dataControl != null) && (dataControl.DataView != null))
                {
                    ErrorWatch.UpdateWatchErrorsView(dataControl.DataView);
                }
            }
        }
    }
}

