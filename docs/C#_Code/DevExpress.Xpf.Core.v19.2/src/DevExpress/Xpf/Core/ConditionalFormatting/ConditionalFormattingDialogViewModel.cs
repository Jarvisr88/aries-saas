namespace DevExpress.Xpf.Core.ConditionalFormatting
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.DataAnnotations;
    using DevExpress.Mvvm.UI.Native.ViewGenerator.Model;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.ConditionalFormatting.Native;
    using DevExpress.Xpf.Core.ConditionalFormattingManager;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public abstract class ConditionalFormattingDialogViewModel : IConditionalFormattingDialogViewModel
    {
        public const string FormatServiceKey = "customFormatService";
        protected readonly IFormatsOwner owner;
        private IList<FormatInfo> formats;
        private Locker formatInfoLocker = new Locker();
        private object _value;

        protected ConditionalFormattingDialogViewModel(IFormatsOwner owner, ConditionalFormattingStringId titleId, ConditionalFormattingStringId descriptionId, ConditionalFormattingStringId connectorId)
        {
            this.owner = owner;
            this.Title = ConditionalFormattingLocalizer.GetString(titleId);
            this.Description = ConditionalFormattingLocalizer.GetString(descriptionId);
            this.ConnectorText = ConditionalFormattingLocalizer.GetString(connectorId);
            this.formats = this.CreateFormats();
            this.SelectedFormatInfo = this.Formats.FirstOrDefault<FormatInfo>();
            this.ApplyFormatToWholeRowText = ConditionalFormattingLocalizer.GetString(ConditionalFormattingStringId.ConditionalFormatting_Dialog_ApplyFormatToWholeRowText);
            this.ApplyFormatToWholeRowEnabled = true;
        }

        public virtual IModelItem CreateCondition(IEditingContext context, string fieldName) => 
            this.CreateEditUnit(fieldName).BuildCondition(this.Context.Builder);

        protected abstract BaseEditUnit CreateEditUnit(string fieldName);
        protected virtual IList<FormatInfo> CreateFormats()
        {
            List<FormatInfo> list = new List<FormatInfo>(this.owner.PredefinedFormats);
            FormatInfo item = new FormatInfo();
            item.DisplayName = ConditionalFormattingLocalizer.GetString(ConditionalFormattingStringId.ConditionalFormatting_Manager_CustomFormat);
            list.Add(item);
            return list;
        }

        internal abstract object GetInitialValue();
        public virtual void Initialize(IDialogContext context)
        {
            this.Context = context;
            this.Value = this.GetInitialValue();
        }

        protected void OnSelectedFormatInfoChanged(FormatInfo oldValue)
        {
            if ((this.SelectedFormatInfo != null) && (this.SelectedFormatInfo.IsCustom && !this.formatInfoLocker))
            {
                this.SetCustomFormatInfo(oldValue);
            }
        }

        protected virtual void OnValueChanged()
        {
        }

        private void SetCustomFormatInfo(FormatInfo oldValue)
        {
            FormatEditorViewModel viewModel = FormatEditorViewModel.Factory(this.Context, this.AllowTextDecorations);
            if (ManagerHelperBase.ShowDialog(viewModel, ConditionalFormattingLocalizer.GetString(ConditionalFormattingStringId.ConditionalFormatting_Manager_CustomFormat), this.FormatService, null))
            {
                oldValue = (FormatInfo) this.SelectedFormatInfo.Clone();
                oldValue.Format = viewModel.CreateFormat();
                oldValue.Freeze();
                this.formats.Remove(this.SelectedFormatInfo);
                this.formats.Add(oldValue);
            }
            this.formatInfoLocker.DoLockedAction<FormatInfo>(delegate {
                FormatInfo info;
                this.SelectedFormatInfo = info = oldValue;
                return info;
            });
        }

        public void SetFormatProperty(IModelItem condition)
        {
            ConditionEditUnit unit = new ConditionEditUnit();
            if (this.SelectedFormatInfo.IsCustom)
            {
                unit.Format = this.SelectedFormatInfo.Format as Format;
            }
            else
            {
                unit.PredefinedFormatName = this.SelectedFormatInfo.FormatName;
            }
            unit.BuildCondition(this.Context.Builder, condition);
        }

        public virtual bool TryClose() => 
            true;

        public virtual IMessageBoxService MessageBox =>
            null;

        [ServiceProperty(Key="customFormatService")]
        protected virtual IDialogService FormatService =>
            null;

        public string Title { get; private set; }

        public string Description { get; private set; }

        public string ConnectorText { get; set; }

        public string ApplyFormatToWholeRowText { get; set; }

        public bool ApplyFormatToWholeRowEnabled { get; set; }

        public IEnumerable<FormatInfo> Formats =>
            this.formats;

        public virtual FormatInfo SelectedFormatInfo { get; set; }

        public virtual object Value
        {
            get => 
                this._value;
            set
            {
                this._value = value;
                this.OnValueChanged();
            }
        }

        public bool ApplyFormatToWholeRow { get; set; }

        public abstract DevExpress.Xpf.Core.ConditionalFormatting.ConditionValueType ConditionValueType { get; }

        protected IDialogContext Context { get; private set; }

        protected virtual bool AllowTextDecorations =>
            true;
    }
}

