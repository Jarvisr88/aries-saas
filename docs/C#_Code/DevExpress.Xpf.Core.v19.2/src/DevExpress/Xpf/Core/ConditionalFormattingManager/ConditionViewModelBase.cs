namespace DevExpress.Xpf.Core.ConditionalFormattingManager
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core.ConditionalFormatting.Native;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public abstract class ConditionViewModelBase : ManagerViewModelBase, IConditionEditor
    {
        protected BaseEditUnit source;

        protected ConditionViewModelBase(IDialogContext context) : base(context)
        {
            this.ViewModels = this.CreateChildViewModels().ToArray<IConditionEditor>();
            this.SelectedViewModel = this.ViewModels.First<IConditionEditor>();
        }

        protected abstract IEnumerable<IConditionEditor> CreateChildViewModels();
        bool IConditionEditor.CanInit(BaseEditUnit unit) => 
            this.ViewModels.Any<IConditionEditor>(x => x.CanInit(unit));

        BaseEditUnit IConditionEditor.Edit() => 
            this.SelectedViewModel.Edit();

        void IConditionEditor.Init(BaseEditUnit unit)
        {
            this.source = unit;
            IConditionEditor selectedViewModel = this.SelectedViewModel;
            IConditionEditor local1 = this.ViewModels.FirstOrDefault<IConditionEditor>(x => x.CanInit(unit));
            IConditionEditor local3 = local1;
            if (local1 == null)
            {
                IConditionEditor local2 = local1;
                local3 = this.ViewModels.First<IConditionEditor>();
            }
            this.SelectedViewModel = local3;
            if (ReferenceEquals(this.SelectedViewModel, selectedViewModel))
            {
                this.SelectedViewModel.Init(this.source);
            }
        }

        bool IConditionEditor.Validate() => 
            this.SelectedViewModel.Validate();

        protected void OnSelectedViewModelChanging(IConditionEditor newValue)
        {
            this.source.Do<BaseEditUnit>(x => newValue.Init(x));
        }

        public IEnumerable<IConditionEditor> ViewModels { get; private set; }

        public virtual IConditionEditor SelectedViewModel { get; set; }
    }
}

