namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting;
    using System;

    public interface IBrickPublisher
    {
        bool CanPublish(Brick brick, IPrintingSystemContext context);
    }
}

