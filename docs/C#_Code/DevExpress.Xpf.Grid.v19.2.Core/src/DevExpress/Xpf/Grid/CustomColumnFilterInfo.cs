namespace DevExpress.Xpf.Grid
{
    using DevExpress.Data.Filtering;
    using DevExpress.Xpf.Editors;
    using System;
    using System.Windows.Controls;
    using System.Windows.Markup;

    public class CustomColumnFilterInfo : ColumnFilterInfoBase
    {
        private CriteriaOperator customColumnFilter;

        public CustomColumnFilterInfo(ColumnBase column) : base(column)
        {
        }

        public override bool CanShowFilterPopup() => 
            true;

        protected override void ClearPopupData(PopupBaseEdit popup)
        {
        }

        internal override PopupBaseEdit CreateColumnFilterPopup()
        {
            PopupBaseEdit edit1 = new PopupBaseEdit();
            edit1.ShowNullText = false;
            edit1.IsTextEditable = false;
            return edit1;
        }

        private ControlTemplate CreatePopupTemplate() => 
            XamlReader.Parse("<ControlTemplate xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\" xmlns:dxg=\"http://schemas.devexpress.com/winfx/2008/xaml/grid\">\r\n                                        <dxg:CustomColumnFilterContentPresenter ColumnFilterInfo=\"{Binding Path=DataContext, RelativeSource={RelativeSource TemplatedParent}}\"/>\r\n                                    </ControlTemplate>") as ControlTemplate;

        protected internal override CriteriaOperator GetFilterCriteria() => 
            this.CustomColumnFilter;

        protected override void UpdatePopupData(PopupBaseEdit popup)
        {
            this.CustomColumnFilter = base.View.DataControl.GetColumnFilterCriteria(base.Column);
            popup.DataContext = this;
            popup.PopupContentTemplate = this.CreatePopupTemplate();
        }

        internal CriteriaOperator CustomColumnFilter
        {
            get => 
                this.customColumnFilter;
            set
            {
                this.customColumnFilter = value;
                this.UpdateColumnFilterIfNeeded(() => this.CustomColumnFilter);
            }
        }
    }
}

