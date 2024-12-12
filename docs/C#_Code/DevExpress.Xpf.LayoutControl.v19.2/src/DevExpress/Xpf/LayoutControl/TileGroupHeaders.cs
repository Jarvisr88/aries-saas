namespace DevExpress.Xpf.LayoutControl
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public class TileGroupHeaders : ElementPool<TileGroupHeader>
    {
        private bool _AreEditable;
        private DataTemplate _ItemContentTemplate;

        public TileGroupHeaders(Panel container) : base(container)
        {
        }

        protected override TileGroupHeader CreateItem()
        {
            TileGroupHeader item = base.CreateItem();
            if (this.ItemContentTemplate != null)
            {
                this.UpdateItemContentTemplate(item);
            }
            this.UpdateItemState(item);
            return item;
        }

        protected virtual void OnItemContentTemplateChanged()
        {
            base.Items.ForEach(new Action<TileGroupHeader>(this.UpdateItemContentTemplate));
        }

        public void StopEditing()
        {
            if (this.AreEditable)
            {
                foreach (TileGroupHeader header in base.Items)
                {
                    header.StopEditing(true);
                }
            }
        }

        private void UpdateItemContentTemplate(TileGroupHeader item)
        {
            item.SetValueIfNotDefault(ContentControlBase.ContentTemplateProperty, this.ItemContentTemplate);
        }

        private void UpdateItemState(TileGroupHeader item)
        {
            item.State = this.AreEditable ? TileGroupHeaderState.Editable : TileGroupHeaderState.NonEditable;
        }

        public bool AreEditable
        {
            get => 
                this._AreEditable;
            set
            {
                if (this.AreEditable != value)
                {
                    this.StopEditing();
                    this._AreEditable = value;
                    base.Items.ForEach(new Action<TileGroupHeader>(this.UpdateItemState));
                }
            }
        }

        public DataTemplate ItemContentTemplate
        {
            get => 
                this._ItemContentTemplate;
            set
            {
                if (!ReferenceEquals(this.ItemContentTemplate, value))
                {
                    this._ItemContentTemplate = value;
                    this.OnItemContentTemplateChanged();
                }
            }
        }
    }
}

