namespace DevExpress.Xpf.Docking.Platform
{
    using System;

    public static class DockHintElementFactory
    {
        public static DockHintElement Make(DockVisualizerElement type)
        {
            switch (type)
            {
                case DockVisualizerElement.Left:
                case DockVisualizerElement.Right:
                case DockVisualizerElement.Top:
                case DockVisualizerElement.Bottom:
                    return new SideDockHintElement(type);

                case DockVisualizerElement.Center:
                    return new CenterDockHintElement();

                case DockVisualizerElement.Selection:
                    return new SelectionHint();

                case DockVisualizerElement.DockZone:
                    return new RectangleDockHint();

                case DockVisualizerElement.RenameHint:
                    return new RenameHint();
            }
            return null;
        }
    }
}

