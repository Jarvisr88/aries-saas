namespace DevExpress.XtraPrinting
{
    using System.Collections;

    public interface IPanelBrick : IVisualBrick, IBaseBrick, IBrick
    {
        IList Bricks { get; }
    }
}

