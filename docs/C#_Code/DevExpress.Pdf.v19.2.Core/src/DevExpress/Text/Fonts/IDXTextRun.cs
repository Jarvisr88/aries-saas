namespace DevExpress.Text.Fonts
{
    using System;

    public interface IDXTextRun
    {
        DXLineBreakpoint GetBreakpoint(int index);

        int Offset { get; }

        int Length { get; }

        byte BidiLevel { get; }
    }
}

