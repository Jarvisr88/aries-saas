namespace DevExpress.Xpf.Printing.BrickCollection
{
    using DevExpress.XtraPrinting;
    using System.Windows;

    internal interface IBrickCreator
    {
        VisualBrick Create(UIElement source, UIElement parent);
    }
}

