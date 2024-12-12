namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Utils;
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public class ButtonsControl : ItemsControl
    {
        public ButtonsControl()
        {
            this.SetDefaultStyleKey(typeof(ButtonsControl));
        }

        protected override DependencyObject GetContainerForItemOverride() => 
            new ButtonContainer();

        protected override bool IsItemItsOwnContainerOverride(object item) => 
            item is ButtonContainer;
    }
}

