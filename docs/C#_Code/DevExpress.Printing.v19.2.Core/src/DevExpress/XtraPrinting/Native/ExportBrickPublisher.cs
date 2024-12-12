namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting;
    using System;

    internal class ExportBrickPublisher : IBrickPublisher
    {
        bool IBrickPublisher.CanPublish(Brick brick, IPrintingSystemContext context);
    }
}

