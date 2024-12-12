namespace DevExpress.Xpf.Printing.PreviewControl
{
    using System.Collections.Generic;

    public interface IPagesPresenter
    {
        IEnumerable<Pair<Page, RectangleF>> GetPages();
    }
}

