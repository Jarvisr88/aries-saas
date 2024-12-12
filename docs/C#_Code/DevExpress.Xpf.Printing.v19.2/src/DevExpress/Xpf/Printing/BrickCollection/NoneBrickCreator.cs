namespace DevExpress.Xpf.Printing.BrickCollection
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Windows;

    internal class NoneBrickCreator : IBrickCreator
    {
        public VisualBrick Create(UIElement source, UIElement parent) => 
            null;
    }
}

