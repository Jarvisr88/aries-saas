namespace DevExpress.Xpf.Grid.EditForm
{
    using System;
    using System.Runtime.CompilerServices;

    public class EditFormCaptionData : EditFormCellDataBase
    {
        protected internal override void Assign(EditFormColumnSource source)
        {
            base.Assign(source);
            this.Caption = source.Caption;
        }

        public object Caption { get; internal set; }

        protected override EditFormLayoutItemType ItemTypeCore =>
            EditFormLayoutItemType.Caption;
    }
}

