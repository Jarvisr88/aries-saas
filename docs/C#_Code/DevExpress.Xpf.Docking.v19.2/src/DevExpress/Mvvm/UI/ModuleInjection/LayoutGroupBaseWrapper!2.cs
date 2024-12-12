namespace DevExpress.Mvvm.UI.ModuleInjection
{
    using DevExpress.Xpf.Docking;
    using System;

    public abstract class LayoutGroupBaseWrapper<TTarget, TChild> : LayoutGroupWrapperBase<TTarget, TChild> where TTarget: LayoutGroup where TChild: LayoutPanel
    {
        private bool isFirstAdding;

        protected LayoutGroupBaseWrapper()
        {
            this.isFirstAdding = true;
        }

        protected override void Add(object viewModel)
        {
            base.Add(viewModel);
            if (this.isFirstAdding && ((base.SelectedItem == null) && base.Target.IsLoaded))
            {
                this.UpdateSelectionOnItemAdded(viewModel);
                this.isFirstAdding = false;
            }
        }

        protected override TChild CreateChild(object viewModel)
        {
            TChild child = (TChild) new LayoutPanel();
            base.ConfigureChild(child, viewModel);
            base.Target.Add(child);
            base.Target.PrepareContainerForItemCore(child);
            return child;
        }

        protected override void RemoveChild(TChild child)
        {
            if (base.Manager.DockController != null)
            {
                base.Manager.DockController.RemovePanel(child);
                base.ClearChild(child);
            }
        }

        protected virtual void UpdateSelectionOnItemAdded(object viewModel)
        {
            base.SelectedItem = viewModel;
        }
    }
}

