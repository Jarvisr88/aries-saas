namespace DevExpress.XtraPrinting.Export
{
    using DevExpress.XtraPrinting;
    using System;

    public interface IBrickExportVisitor
    {
        void ExportBrick(double horizontalOffset, double verticalOffset, Brick brick);
    }
}

