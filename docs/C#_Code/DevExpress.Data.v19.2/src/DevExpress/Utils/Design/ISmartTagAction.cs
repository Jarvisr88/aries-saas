namespace DevExpress.Utils.Design
{
    using System;
    using System.Drawing;

    public interface ISmartTagAction
    {
        void Execute(object component);

        string Text { get; }

        Image Glyph { get; }
    }
}

