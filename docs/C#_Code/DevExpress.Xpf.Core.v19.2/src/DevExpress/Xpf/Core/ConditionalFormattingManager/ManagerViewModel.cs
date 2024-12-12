namespace DevExpress.Xpf.Core.ConditionalFormattingManager
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.DataAnnotations;
    using DevExpress.Mvvm.POCO;
    using DevExpress.Xpf.Core.ConditionalFormatting;
    using DevExpress.Xpf.Core.ConditionalFormatting.Native;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public class ManagerViewModel : ManagerViewModelBase
    {
        private ManagerController controller;

        protected ManagerViewModel(IDialogContext context) : base(context)
        {
            this.controller = new ManagerController(context);
            this.FieldNames = FieldNameWrapper.Create(context.ColumnInfo);
            this.Items = new ManagerItemsCollection();
            this.FilterFieldName = new FieldNameWrapper(null, base.GetLocalizedString(ConditionalFormattingStringId.ConditionalFormatting_Manager_FilterAll));
            this.FilterFieldNames = new List<FieldNameWrapper>(this.FieldNames);
            List<FieldNameWrapper> list = new List<FieldNameWrapper>();
            if (context.PivotSpecialFieldNames != null)
            {
                list.AddRange(context.PivotSpecialFieldNames);
            }
            list.AddRange(this.FieldNames);
            this.PivotSpecialFieldNames = list;
            this.FilterFieldNames.Insert(0, this.FilterFieldName);
            this.IsPivot = context.IsPivot;
            this.ApplyToFieldNameCaption = context.ApplyToFieldNameCaption;
            this.ApplyToPivotColumnCaption = context.ApplyToPivotColumnCaption;
            this.ApplyToPivotRowCaption = context.ApplyToPivotRowCaption;
            this.UpdateItems();
        }

        public void ApplyChanges()
        {
            this.controller.ApplyChanges();
            this.UpdateItems();
            this.CanApply = false;
        }

        public bool CanMoveDown(ManagerItemViewModel item) => 
            this.HasSelectedItem(item) && (item != this.Items[this.Items.Count - 1]);

        public bool CanMoveUp(ManagerItemViewModel item) => 
            this.HasSelectedItem(item) && (item != this.Items[0]);

        public bool HasSelectedItem(ManagerItemViewModel item) => 
            (item != null) && (this.Items.Count > 0);

        private void MoveCore(ManagerItemViewModel item, int delta)
        {
            this.controller.Swap(item, this.Items[this.Items.IndexOf(item) + delta]);
            this.UpdateItems();
            this.SelectedItem = item;
            this.CanApply = true;
        }

        public void MoveDown(ManagerItemViewModel item)
        {
            this.MoveCore(item, 1);
        }

        public void MoveUp(ManagerItemViewModel item)
        {
            this.MoveCore(item, -1);
        }

        [Command(CanExecuteMethodName="HasSelectedItem")]
        public void RemoveFormatCondition(ManagerItemViewModel item)
        {
            this.controller.Remove(item);
            this.UpdateItems();
            this.CanApply = true;
        }

        public void ShowAddDialog()
        {
            IConditionEditor vm = AddConditionViewModel.Factory(base.Context);
            if (this.ShowDialogCore(vm))
            {
                this.controller.Add(vm.Edit());
                this.UpdateItems();
                this.SelectedItem = this.Items.LastOrDefault<ManagerItemViewModel>();
                this.CanApply = true;
            }
        }

        private bool ShowDialogCore(IConditionEditor vm) => 
            ManagerHelper.ShowDialog(vm, vm.Description, this.DialogService, delegate (CancelEventArgs x) {
                x.Cancel = !vm.Validate();
            });

        [Command(CanExecuteMethodName="HasSelectedItem")]
        public void ShowEditDialog(ManagerItemViewModel item)
        {
            IDialogContext arg = base.Context;
            string fieldName = item.EditUnit.FieldName;
            if (!string.IsNullOrEmpty(fieldName))
            {
                arg = base.Context.Find(fieldName) ?? base.Context;
            }
            IConditionEditor vm = AddConditionViewModel.Factory(arg);
            vm.Init(item.EditUnit);
            if (this.ShowDialogCore(vm))
            {
                this.controller.Edit(item, vm.Edit());
                this.UpdateItems();
                this.CanApply = true;
            }
        }

        protected void UpdateItems()
        {
            this.Items.Assign((from x in this.controller.GetDisplayItems(this.FilterFieldName.FieldName) select x.Init(this)).ToList<ManagerItemViewModel>());
        }

        public static Func<IDialogContext, ManagerViewModel> Factory
        {
            get
            {
                ParameterExpression expression = Expression.Parameter(typeof(IDialogContext), "x");
                Expression[] expressionArray1 = new Expression[] { expression };
                ParameterExpression[] parameters = new ParameterExpression[] { expression };
                return ViewModelSource.Factory<IDialogContext, ManagerViewModel>(Expression.Lambda<Func<IDialogContext, ManagerViewModel>>(Expression.New((ConstructorInfo) methodof(ManagerViewModel..ctor), (IEnumerable<Expression>) expressionArray1), parameters));
            }
        }

        [BindableProperty(OnPropertyChangedMethodName="UpdateItems")]
        public virtual FieldNameWrapper FilterFieldName { get; set; }

        public ManagerItemsCollection Items { get; private set; }

        public virtual ManagerItemViewModel SelectedItem { get; set; }

        public virtual IList<FieldNameWrapper> FieldNames { get; protected set; }

        public virtual IList<FieldNameWrapper> PivotSpecialFieldNames { get; protected set; }

        public virtual IList<FieldNameWrapper> FilterFieldNames { get; protected set; }

        public virtual IDialogService DialogService =>
            null;

        public bool CanApply { get; internal set; }

        public bool IsPivot { get; private set; }

        public string ApplyToFieldNameCaption { get; private set; }

        public string ApplyToPivotRowCaption { get; private set; }

        public string ApplyToPivotColumnCaption { get; private set; }

        public override string Description =>
            base.GetLocalizedString(ConditionalFormattingStringId.ConditionalFormatting_Manager_Title);
    }
}

