namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Data;

    public class LegacyDeleteButtonInfo : ButtonInfo
    {
        protected internal override void FindContent(FrameworkElement templatedParent)
        {
            if (base.Template != null)
            {
                if ((base.Template.FindName("PART_Item", templatedParent) is ButtonBase) && ((base.Owner != null) && base.IsDefaultButton))
                {
                    base.Command = base.Owner.SetNullValueCommand;
                }
                Binding binding = new Binding();
                binding.Source = base.Owner.PropertyProvider;
                binding.Path = new PropertyPath(ButtonEditPropertyProvider.IsNullValueButtonVisibleProperty);
                binding.Converter = new BooleanToVisibilityConverter();
                binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                BindingOperations.SetBinding(this, ButtonInfoBase.VisibilityProperty, binding);
            }
        }
    }
}

