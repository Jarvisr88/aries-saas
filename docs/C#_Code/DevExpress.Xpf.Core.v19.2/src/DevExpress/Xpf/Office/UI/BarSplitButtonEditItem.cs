namespace DevExpress.Xpf.Office.UI
{
    using DevExpress.Mvvm;
    using DevExpress.Office.Internal;
    using DevExpress.Xpf.Bars;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Utils;
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Input;

    [DXToolboxBrowsable(false)]
    public class BarSplitButtonEditItem : BarSplitButtonItem, IEditValueBarItem
    {
        public static readonly DependencyProperty EditValueProperty;
        private bool isEditValueChangedRaised;
        private EditValueChangedEventHandler onEditValueChanged;

        public event EditValueChangedEventHandler EditValueChanged
        {
            add
            {
                this.onEditValueChanged += value;
            }
            remove
            {
                this.onEditValueChanged -= value;
            }
        }

        static BarSplitButtonEditItem()
        {
            Type ownerType = typeof(BarSplitButtonEditItem);
            EditValueProperty = DependencyPropertyManager.Register("EditValue", typeof(object), ownerType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Journal | FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(BarSplitButtonEditItem.OnEditValueChanged), new CoerceValueCallback(BarSplitButtonEditItem.OnCoerceEditValue), true, UpdateSourceTrigger.PropertyChanged));
        }

        protected virtual object CoerceEditValue(DependencyObject d, object value) => 
            value;

        protected static object OnCoerceEditValue(DependencyObject obj, object value) => 
            ((BarSplitButtonEditItem) obj).CoerceEditValue(obj, value);

        protected virtual void OnEditValueChanged(object oldValue, object newValue)
        {
            this.RaiseEditValueChanged(oldValue, newValue);
            this.UpdateLinkControls();
            this.OnItemClick(base.Links.FirstOrDefault<BarItemLinkBase>() as BarItemLink, null);
            this.isEditValueChangedRaised = true;
        }

        protected static void OnEditValueChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((BarSplitButtonEditItem) obj).OnEditValueChanged(e.OldValue, e.NewValue);
        }

        protected internal override void OnPopupControlChanged(IPopupControl oldValue, IPopupControl newValue)
        {
            base.OnPopupControlChanged(oldValue, newValue);
            if (base.PopupControl != null)
            {
                base.PopupControl.Opened += new EventHandler(this.PopupControl_Opened);
            }
        }

        protected internal override void OnPopupControlChanging()
        {
            if (base.PopupControl != null)
            {
                base.PopupControl.Opened -= new EventHandler(this.PopupControl_Opened);
            }
            base.OnPopupControlChanging();
        }

        private void PopupControl_Opened(object sender, EventArgs e)
        {
            PopupControlContainer container = sender as PopupControlContainer;
            if (container != null)
            {
                ColorEdit content = container.Content as ColorEdit;
                if (content != null)
                {
                    bool? useCommandManager = null;
                    ICommand command = new DelegateCommand(() => this.OnEditValueChanged(null, this.EditValue), () => !this.isEditValueChangedRaised, useCommandManager);
                    content.ResetButton.Command = command;
                    content.NoColorButton.Command = command;
                    content.Gallery.ItemClickCommand = command;
                }
            }
            this.isEditValueChangedRaised = false;
        }

        protected internal virtual void RaiseEditValueChanged(object oldValue, object newValue)
        {
            if (this.onEditValueChanged != null)
            {
                this.onEditValueChanged(this, new EditValueChangedEventArgs(oldValue, newValue));
            }
        }

        protected internal virtual void UpdateLinkControl(BarItemLinkBase link)
        {
        }

        protected internal virtual void UpdateLinkControls()
        {
            ReadOnlyLinkCollection links = base.Links;
            int count = links.Count;
            for (int i = 0; i < count; i++)
            {
                this.UpdateLinkControl(links[i]);
            }
        }

        [TypeConverter(typeof(ObjectConverter))]
        public object EditValue
        {
            get => 
                base.GetValue(EditValueProperty);
            set => 
                base.SetValue(EditValueProperty, value);
        }
    }
}

