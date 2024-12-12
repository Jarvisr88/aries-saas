namespace DevExpress.XtraPrinting
{
    using System;
    using System.Runtime.CompilerServices;

    public interface IPrintingSystem
    {
        event ChangeEventHandler AfterChange;

        event ChangeEventHandler BeforeChange;

        IBrick CreateBrick(string typeName);
        IImageBrick CreateImageBrick();
        IPanelBrick CreatePanelBrick();
        IProgressBarBrick CreateProgressBarBrick();
        IRichTextBrick CreateRichTextBrick();
        ITextBrick CreateTextBrick();
        ITrackBarBrick CreateTrackBarBrick();
        void InsertPageBreak(float pos);
        void SetCommandVisibility(PrintingSystemCommand command, bool visible);

        string Version { get; }

        IImagesContainer Images { get; }

        int AutoFitToPagesWidth { get; set; }
    }
}

