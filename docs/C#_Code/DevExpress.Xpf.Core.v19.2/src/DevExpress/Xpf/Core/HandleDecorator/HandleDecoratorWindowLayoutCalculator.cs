namespace DevExpress.Xpf.Core.HandleDecorator
{
    using System;
    using System.Drawing;

    public class HandleDecoratorWindowLayoutCalculator
    {
        private static Size defaultImageSize = new Size(5, 5);

        public static unsafe Rectangle Calculate(HandleDecoratorWindowTypes windowType, Rectangle windowRectagle, ThemeElementPainter painter)
        {
            if (painter != null)
            {
                Rectangle rectangle = windowRectagle;
                int num = (int) (GetDecoratorThick(painter.GetElementImageByWindowType(windowType), windowType) * painter.ScaleFactor);
                int offsetByWindowType = painter.GetOffsetByWindowType(windowType);
                switch (windowType)
                {
                    case HandleDecoratorWindowTypes.Left:
                    {
                        rectangle.X = (rectangle.X - num) + offsetByWindowType;
                        rectangle.Y = (rectangle.Y - num) + offsetByWindowType;
                        Rectangle* rectanglePtr1 = &rectangle;
                        rectanglePtr1.Height += (num - offsetByWindowType) * 2;
                        rectangle.Width = num;
                        return rectangle;
                    }
                    case HandleDecoratorWindowTypes.Top:
                        rectangle.X += offsetByWindowType;
                        rectangle.Y = (rectangle.Y - num) + offsetByWindowType;
                        rectangle.Height = num;
                        rectangle.Width -= 2 * offsetByWindowType;
                        return rectangle;

                    case HandleDecoratorWindowTypes.Right:
                    {
                        rectangle.X = rectangle.Right - offsetByWindowType;
                        rectangle.Y = (rectangle.Y - num) + offsetByWindowType;
                        Rectangle* rectanglePtr2 = &rectangle;
                        rectanglePtr2.Height += (num - offsetByWindowType) * 2;
                        rectangle.Width = num;
                        return rectangle;
                    }
                    case HandleDecoratorWindowTypes.Bottom:
                        rectangle.X += offsetByWindowType;
                        rectangle.Y = rectangle.Bottom - offsetByWindowType;
                        rectangle.Height = num;
                        rectangle.Width -= 2 * offsetByWindowType;
                        return rectangle;

                    case HandleDecoratorWindowTypes.Composite:
                    {
                        Rectangle rectangle2 = Calculate(HandleDecoratorWindowTypes.Left, windowRectagle, painter);
                        Rectangle rectangle3 = Calculate(HandleDecoratorWindowTypes.Right, windowRectagle, painter);
                        rectangle.X = rectangle2.Left;
                        rectangle.Y = rectangle2.Top;
                        rectangle.Height = rectangle2.Height;
                        rectangle.Width = rectangle3.Right - rectangle2.Left;
                        return rectangle;
                    }
                }
            }
            return Rectangle.Empty;
        }

        private static int GetDecoratorThick(ThemeElementImage elementImage, HandleDecoratorWindowTypes windowType) => 
            IsHorizontalWindowType(windowType) ? GetImageSize(elementImage).Width : GetImageSize(elementImage).Height;

        public static Size GetImageSize(ThemeElementImage element) => 
            ((element == null) || (element.Image == null)) ? defaultImageSize : element.GetImageBounds(0).Size;

        private static bool IsHorizontalWindowType(HandleDecoratorWindowTypes windowType) => 
            (windowType == HandleDecoratorWindowTypes.Left) || (windowType == HandleDecoratorWindowTypes.Right);
    }
}

