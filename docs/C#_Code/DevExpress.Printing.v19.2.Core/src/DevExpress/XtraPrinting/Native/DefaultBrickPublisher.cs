namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting;
    using System;

    public class DefaultBrickPublisher : IBrickPublisher
    {
        bool IBrickPublisher.CanPublish(Brick brick, IPrintingSystemContext context) => 
            brick.IsVisible;
    }
}

