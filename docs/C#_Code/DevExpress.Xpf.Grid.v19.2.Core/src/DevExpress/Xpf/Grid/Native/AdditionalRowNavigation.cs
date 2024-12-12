namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Data;
    using DevExpress.Xpf.Grid;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class AdditionalRowNavigation : GridViewCellNavigation
    {
        public AdditionalRowNavigation(DataViewBase view) : base(view)
        {
        }

        [IteratorStateMachine(typeof(<GetAdditionalRows>d__2))]
        private IEnumerable<DependencyObject> GetAdditionalRows()
        {
            IEnumerator enumerator;
            if (!this.View.IsRootView)
            {
                FrameworkElement rowElementByRowHandle = this.View.GetRowElementByRowHandle(-2147483647);
                if (this.View.IsNewItemRowVisible && (rowElementByRowHandle != null))
                {
                    yield return rowElementByRowHandle;
                }
                goto TR_0004;
            }
            else
            {
                AdditionalRowItemsControl additionalRowItemsControl = this.GridView.TableViewBehavior.AdditionalRowItemsControl;
                if (additionalRowItemsControl != null)
                {
                    enumerator = ((IEnumerable) additionalRowItemsControl.Items).GetEnumerator();
                }
            }
        Label_PostSwitchInIterator:;
            if (enumerator.MoveNext())
            {
                DependencyObject current = (DependencyObject) enumerator.Current;
                yield return current;
                goto Label_PostSwitchInIterator;
            }
            else
            {
                enumerator = null;
            }
        TR_0004:
            yield break;
        }

        protected override void UpdateRowsStateCore()
        {
            foreach (DependencyObject obj2 in this.GetAdditionalRows())
            {
                RowHandle rowHandle = DataViewBase.GetRowHandle(obj2);
                this.SetRowFocus(obj2, (rowHandle != null) && (rowHandle.Value == base.View.FocusedRowHandle));
            }
        }

    }
}

