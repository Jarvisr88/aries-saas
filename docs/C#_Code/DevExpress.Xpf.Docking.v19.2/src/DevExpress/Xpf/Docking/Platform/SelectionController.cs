namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Docking.Base;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class SelectionController : IDisposable
    {
        private IList<SelectionHint> selectionHints = new List<SelectionHint>();
        private bool isDisposing;

        public SelectionController(DockingHintAdornerBase adorner)
        {
            this.Adorner = adorner;
            this.Container = DockLayoutManager.GetDockLayoutManager(this.Adorner.AdornedElement);
            this.Container.LayoutItemSizeChanged += new LayoutItemSizeChangedEventHandler(this.OnItemSizeChanged);
            this.Container.LayoutItemSelectionChanged += new LayoutItemSelectionChangedEventHandler(this.OnItemSelectionChanged);
        }

        protected virtual SelectionHint CreateSelectionHint()
        {
            Size size = new Size();
            return (SelectionHint) this.Adorner.CreateDockHintElement(DockVisualizerElement.Selection, size, Alignment.Fill);
        }

        public void Dispose()
        {
            if (!this.isDisposing)
            {
                this.isDisposing = true;
                this.Container.LayoutItemSelectionChanged -= new LayoutItemSelectionChangedEventHandler(this.OnItemSelectionChanged);
                this.Container.LayoutItemSizeChanged -= new LayoutItemSizeChangedEventHandler(this.OnItemSizeChanged);
                this.Container = null;
                Ref.Clear<SelectionHint>(ref this.selectionHints);
            }
        }

        protected virtual bool EnsureSelectionHints()
        {
            if (!this.Container.LayoutController.IsCustomization)
            {
                return false;
            }
            while (this.Hints.Count < this.Container.LayoutController.Selection.Count)
            {
                this.Hints.Add(this.CreateSelectionHint());
            }
            return true;
        }

        public void Focus(BaseLayoutItem layoutItem)
        {
            Action<SelectionHint> action = <>c.<>9__7_1;
            if (<>c.<>9__7_1 == null)
            {
                Action<SelectionHint> local1 = <>c.<>9__7_1;
                action = <>c.<>9__7_1 = x => x.EnqueueFocus();
            }
            this.Hints.FirstOrDefault<SelectionHint>(x => ReferenceEquals(x.Item, layoutItem)).Do<SelectionHint>(action);
        }

        private void OnItemSelectionChanged(object sender, LayoutItemSelectionChangedEventArgs e)
        {
            if (this.Container.IsCustomization)
            {
                this.UpdateHints();
                this.Adorner.Update();
            }
        }

        protected virtual void OnItemSizeChanged(object sender, LayoutItemSizeChangedEventArgs e)
        {
            if (this.Container.IsCustomization)
            {
                this.UpdateHints();
                this.Adorner.Update();
            }
        }

        public virtual void UpdateHints()
        {
            if (this.EnsureSelectionHints())
            {
                Selection selection = this.Container.LayoutController.Selection;
                for (int i = 0; i < this.Hints.Count; i++)
                {
                    BaseLayoutItem item = (i < selection.Count) ? selection[i] : null;
                    this.Hints[i].Item = this.Adorner.ContainsSelection(item) ? item : null;
                }
            }
        }

        protected DockLayoutManager Container { get; private set; }

        protected DockingHintAdornerBase Adorner { get; private set; }

        protected internal IList<SelectionHint> Hints =>
            this.selectionHints;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly SelectionController.<>c <>9 = new SelectionController.<>c();
            public static Action<SelectionHint> <>9__7_1;

            internal void <Focus>b__7_1(SelectionHint x)
            {
                x.EnqueueFocus();
            }
        }
    }
}

