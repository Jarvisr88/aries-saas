namespace DevExpress.Xpf.Core.FilteringUI
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public sealed class MenuItemTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container) => 
            !(item is MenuItemGroup) ? (!(item is DevExpress.Xpf.Core.FilteringUI.MenuItem) ? (!(item is MenuSeparator) ? base.SelectTemplate(item, container) : this.Separator) : ((((DevExpress.Xpf.Core.FilteringUI.MenuItem) item).FormatCondition == null) ? this.Button : this.FormatConditionButton)) : this.Group;

        public DataTemplate Button { get; set; }

        public DataTemplate FormatConditionButton { get; set; }

        public DataTemplate Separator { get; set; }

        public DataTemplate Group { get; set; }
    }
}

