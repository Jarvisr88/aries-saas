namespace DevExpress.Office.Drawing
{
    using System;

    public interface IDrawingTextBullets
    {
        IDrawingBullet Common { get; set; }

        IDrawingBullet Color { get; set; }

        IDrawingBullet Typeface { get; set; }

        IDrawingBullet Size { get; set; }
    }
}

