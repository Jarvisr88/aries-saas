namespace DevExpress.Xpf.Core.ConditionalFormattingManager
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core.ConditionalFormatting;
    using DevExpress.Xpf.Core.ConditionalFormatting.Native;
    using System;
    using System.Runtime.CompilerServices;

    public abstract class FormatEditorOwnerViewModel : ManagerViewModelBase, IConditionEditor
    {
        protected FormatEditorOwnerViewModel(IDialogContext context) : base(context)
        {
        }

        protected virtual void AddChanges(ExpressionEditUnit unit)
        {
            unit.Format = this.Format;
        }

        protected abstract bool CanInitCore(ExpressionEditUnit unit);
        protected abstract ExpressionEditUnit CreateEditUnit();
        private FormatEditorViewModel CreateFormatViewModel()
        {
            FormatEditorViewModel model = FormatEditorViewModel.Factory(base.Context, this.AllowTextDecorations);
            model.Init(this.Format);
            return model;
        }

        bool IConditionEditor.CanInit(BaseEditUnit unit) => 
            (unit as ExpressionEditUnit).If<ExpressionEditUnit>(x => this.CanInitCore(x)) != null;

        BaseEditUnit IConditionEditor.Edit()
        {
            ExpressionEditUnit unit = this.CreateEditUnit();
            this.AddChanges(unit);
            return unit;
        }

        void IConditionEditor.Init(BaseEditUnit unit)
        {
            (unit as ExpressionEditUnit).Do<ExpressionEditUnit>(x => this.InitCore(x));
        }

        bool IConditionEditor.Validate() => 
            this.ValidateExpression();

        protected virtual void InitCore(ExpressionEditUnit unit)
        {
            this.Format = unit.Format;
        }

        public void ShowFormatEditor()
        {
            FormatEditorViewModel viewModel = this.CreateFormatViewModel();
            if (ManagerHelper.ShowDialog(viewModel, viewModel.Description, this.DialogService, null))
            {
                this.Format = viewModel.CreateFormat();
            }
        }

        protected virtual bool ValidateExpression() => 
            true;

        public virtual DevExpress.Xpf.Core.ConditionalFormatting.Format Format { get; set; }

        public virtual IDialogService DialogService =>
            null;

        protected virtual bool AllowTextDecorations =>
            true;
    }
}

