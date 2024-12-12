namespace #Xqe
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    internal class #Zqe
    {
        private Rectangle #Bo;
        private int #Pve;
        private int #Qve;

        internal #Zqe(Rectangle bounds, int xAmount, int yAmount)
        {
            this.#Bo = bounds;
            this.#Pve = xAmount;
            this.#Qve = yAmount;
        }

        internal #Zqe(Rectangle bounds, Orientation orientation, int amount)
        {
            this.#Bo = bounds;
            if (orientation == Orientation.Horizontal)
            {
                this.#Pve = amount;
            }
            else
            {
                this.#Qve = amount;
            }
        }

        internal Point CopyDestinationLocation
        {
            get
            {
                Point location;
                if (0 != 0)
                {
                    Point location = this.#Bo.Location;
                }
                else
                {
                    location = this.#Bo.Location;
                }
                location.Offset(Math.Max(0, this.XAmount), Math.Max(0, this.YAmount));
                return location;
            }
        }

        internal Rectangle CopySourceBounds
        {
            get
            {
                Rectangle rectangle = Rectangle.Intersect(this.#Bo, this.DestinationBounds);
                if ((rectangle.Width == 0) || (rectangle.Height == 0))
                {
                    return Rectangle.Empty;
                }
                rectangle.Offset(-1 * this.XAmount, -1 * this.YAmount);
                return rectangle;
            }
        }

        internal Rectangle DestinationBounds =>
            new Rectangle(this.#Bo.Left + this.XAmount, this.#Bo.Top + this.YAmount, this.#Bo.Width, this.#Bo.Height);

        internal Rectangle SourceBounds =>
            this.#Bo;

        internal int XAmount =>
            this.#Pve;

        internal int YAmount =>
            this.#Qve;
    }
}

