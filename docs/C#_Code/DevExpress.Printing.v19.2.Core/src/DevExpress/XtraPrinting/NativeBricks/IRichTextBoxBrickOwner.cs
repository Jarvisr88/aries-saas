namespace DevExpress.XtraPrinting.NativeBricks
{
    using DevExpress.XtraPrinting;
    using System.Windows.Forms;

    public interface IRichTextBoxBrickOwner : IBrickOwner
    {
        System.Windows.Forms.RichTextBox RichTextBox { get; }
    }
}

