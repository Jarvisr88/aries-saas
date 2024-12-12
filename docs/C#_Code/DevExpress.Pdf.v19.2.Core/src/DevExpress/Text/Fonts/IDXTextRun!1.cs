namespace DevExpress.Text.Fonts
{
    using System;

    public interface IDXTextRun<T> : IDXTextRun where T: IDXTextRun
    {
        T Split(int splitOffset);
    }
}

