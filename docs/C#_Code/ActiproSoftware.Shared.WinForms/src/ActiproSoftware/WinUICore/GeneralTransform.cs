namespace ActiproSoftware.WinUICore
{
    using System;
    using System.Drawing;

    public class GeneralTransform
    {
        private Point #Bgb;

        public GeneralTransform(Point translation)
        {
            this.#Bgb = translation;
        }

        public Point Transform(Point pt) => 
            new Point(pt.X + this.#Bgb.X, pt.Y + this.#Bgb.Y);
    }
}

