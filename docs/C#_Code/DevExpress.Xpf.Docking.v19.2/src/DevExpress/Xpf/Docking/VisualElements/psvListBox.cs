namespace DevExpress.Xpf.Docking.VisualElements
{
    using System.Windows;
    using System.Windows.Controls;

    public class psvListBox : ListBox
    {
        protected override DependencyObject GetContainerForItemOverride() => 
            new psvListBoxItem();
    }
}

