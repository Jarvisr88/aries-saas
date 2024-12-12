namespace DevExpress.Xpf.Core.ConditionalFormattingManager
{
    using DevExpress.Mvvm.UI.Native.ViewGenerator.Model;
    using DevExpress.Xpf.Core.ConditionalFormatting;
    using DevExpress.Xpf.Core.ConditionalFormatting.Native;
    using System;
    using System.Windows;

    public class DataBarEditUnit : IndicatorEditUnit
    {
        private DataBarFormat format;

        public override IModelItem BuildCondition(IConditionModelItemsBuilder builder, IModelItem source) => 
            builder.BuildDataBarCondition(this, source);

        public override string GetDescription(IDialogContext context) => 
            ConditionalFormattingLocalizer.GetString(ConditionalFormattingStringId.ConditionalFormatting_Manager_DataBar);

        public override Freezable GetFormat() => 
            this.Format;

        public DataBarFormat Format
        {
            get => 
                this.format;
            set
            {
                if (!ReferenceEquals(this.format, value))
                {
                    this.format = value;
                }
                base.RegisterPropertyModification("Format");
            }
        }

        public override bool CanApplyToRow =>
            false;
    }
}

