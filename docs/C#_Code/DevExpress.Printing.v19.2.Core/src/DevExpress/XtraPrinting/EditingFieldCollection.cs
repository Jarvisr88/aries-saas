namespace DevExpress.XtraPrinting
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Runtime.InteropServices;

    public class EditingFieldCollection : Collection<EditingField>
    {
        private IPageRepository pages;
        private Action<EditingField> editValueChangedAtion;
        private Action<EditingField> readOnlyChangedAtion;
        private Dictionary<VisualBrick, EditingField> dictionary;

        internal EditingFieldCollection(IPageRepository pages, Action<EditingField> editValueChangedAtion, Action<EditingField> readOnlyChangedAtion) : base(new List<EditingField>())
        {
            this.pages = pages;
            this.editValueChangedAtion = editValueChangedAtion;
            this.readOnlyChangedAtion = readOnlyChangedAtion;
            this.dictionary = new Dictionary<VisualBrick, EditingField>();
        }

        protected override void ClearItems()
        {
            foreach (EditingField field in this)
            {
                field.EditValueChanged -= new EventHandler<EventArgs>(this.Item_EditValueChanged);
                field.ReadOnlyChanged -= new EventHandler<EventArgs>(this.Item_ReadOnlyChanged);
            }
            base.ClearItems();
            this.dictionary.Clear();
        }

        protected override void InsertItem(int index, EditingField item)
        {
            base.InsertItem(index, item);
            item.EditValueChanged += new EventHandler<EventArgs>(this.Item_EditValueChanged);
            item.ReadOnlyChanged += new EventHandler<EventArgs>(this.Item_ReadOnlyChanged);
            item.Pages = this.pages;
        }

        private void Item_EditValueChanged(object sender, EventArgs e)
        {
            this.editValueChangedAtion((EditingField) sender);
        }

        private void Item_ReadOnlyChanged(object sender, EventArgs e)
        {
            this.readOnlyChangedAtion((EditingField) sender);
        }

        protected override void RemoveItem(int index)
        {
            base[index].EditValueChanged -= new EventHandler<EventArgs>(this.Item_EditValueChanged);
            base[index].ReadOnlyChanged -= new EventHandler<EventArgs>(this.Item_ReadOnlyChanged);
            if (this.dictionary.ContainsKey(base[index].Brick))
            {
                this.dictionary.Remove(base[index].Brick);
            }
            base.RemoveItem(index);
        }

        internal void Sort(Comparison<EditingField> comparition)
        {
            ((List<EditingField>) base.Items).Sort(comparition);
        }

        public bool TryGetEditingField(VisualBrick brick, out EditingField value)
        {
            if (brick == null)
            {
                value = null;
                return false;
            }
            if (!this.dictionary.TryGetValue(brick, out value))
            {
                value = this.FirstOrDefault<EditingField>(item => ReferenceEquals(item.Brick, brick));
                if (value == null)
                {
                    return false;
                }
                this.dictionary.Add(brick, value);
            }
            return true;
        }
    }
}

