namespace DevExpress.Office.Utils
{
    using DevExpress.Office;
    using System;
    using System.Drawing;

    public class RotatedTextUtils
    {
        public static Size ApplyRotation(Size nonRotatedSize, int angleInDegrees) => 
            ApplyRotationRelativeHorizon(nonRotatedSize, GetDegreesRelativeHorizon(angleInDegrees));

        public static Size ApplyRotationRelativeHorizon(Size nonRotatedSize, int angleRelativeHorizon)
        {
            if (!IsRotated(angleRelativeHorizon))
            {
                return nonRotatedSize;
            }
            double d = (((double) Math.Abs(angleRelativeHorizon)) / 180.0) * 3.1415926535897931;
            return new Size { 
                Height = (int) ((Math.Cos(d) * nonRotatedSize.Height) + (Math.Sin(d) * nonRotatedSize.Width)),
                Width = (int) ((Math.Sin(d) * nonRotatedSize.Height) + (Math.Cos(d) * nonRotatedSize.Width))
            };
        }

        public static unsafe Point ArrangeRotatedTextToBounds(Rectangle bounds, Size nonRotatedSize, Point position, int angleRelativeHorizon, StringFormat stringFormat)
        {
            Size size = ApplyRotationRelativeHorizon(nonRotatedSize, angleRelativeHorizon);
            if (size.Height >= bounds.Height)
            {
                int num = Math.Sign(angleRelativeHorizon);
                if (stringFormat.Alignment == StringAlignment.Far)
                {
                    num *= -1;
                }
                int num2 = num * (size.Height - bounds.Height);
                if ((((stringFormat.Alignment != StringAlignment.Center) || (stringFormat.LineAlignment != StringAlignment.Center)) && ((stringFormat.LineAlignment != StringAlignment.Near) || (num != -1))) && ((stringFormat.LineAlignment != StringAlignment.Far) || (num != 1)))
                {
                    if (stringFormat.LineAlignment == StringAlignment.Center)
                    {
                        Point* pointPtr1 = &position;
                        pointPtr1.Y -= num2 / 2;
                    }
                    else
                    {
                        Point* pointPtr2 = &position;
                        pointPtr2.Y -= num2;
                    }
                }
                if ((stringFormat.Alignment == StringAlignment.Center) && (stringFormat.LineAlignment != StringAlignment.Center))
                {
                    double a = ConvertDegreesToRadian(angleRelativeHorizon);
                    Point* pointPtr3 = &position;
                    pointPtr3.X += (int) (((double) (num2 / 2)) / Math.Tan(a));
                }
            }
            return position;
        }

        public static int ConvertDegreesRelativeHorizon(int angle) => 
            !IsVertical(angle) ? ((angle >= 0) ? angle : (90 - angle)) : 0;

        public static double ConvertDegreesToRadian(int angle) => 
            (3.1415926535897931 * angle) / 180.0;

        public static AngleQuarter GetAngleQuarter(int angle)
        {
            int degreesRelativeHorizon = GetDegreesRelativeHorizon(angle);
            AngleQuarter quarter = (degreesRelativeHorizon >= 0) ? AngleQuarter.First : AngleQuarter.Fourth;
            if (Math.Abs(degreesRelativeHorizon) == 90)
            {
                quarter |= AngleQuarter.RightAngle;
            }
            return quarter;
        }

        public static int GetBoundRotatedWidth(int fontHeight, int maxHeight, int width, int angleRelativeHorizon)
        {
            if (!IsRotated(angleRelativeHorizon))
            {
                throw new ArgumentException();
            }
            double a = ConvertDegreesToRadian(Math.Abs(angleRelativeHorizon));
            double num2 = Math.Sin(a) * fontHeight;
            double num5 = Math.Tan(a) * (width - num2);
            return ((maxHeight < num5) ? ((int) ((((double) maxHeight) / Math.Tan(a)) + num2)) : width);
        }

        public static int GetDegreesRelativeHorizon(int angle) => 
            !IsVertical(angle) ? ((angle <= 90) ? angle : (90 - angle)) : 0;

        public static int GetMaxRotatedWidth(int fontHeight, int maxHeight, int angleRelativeHorizon)
        {
            if (!IsRotated(angleRelativeHorizon))
            {
                throw new ArgumentException();
            }
            maxHeight = Math.Max(maxHeight, fontHeight);
            double a = Math.Abs(ConvertDegreesToRadian(angleRelativeHorizon));
            return (int) ((((double) maxHeight) / Math.Sin(a)) - (((double) fontHeight) / Math.Tan(a)));
        }

        public static Point GetRotatedTextPosition(Rectangle bounds, Size nonRotatedSize, int angleRelativeHorizon, StringFormat stringFormat)
        {
            if (!IsRotated(angleRelativeHorizon))
            {
                throw new ArgumentException();
            }
            double a = ConvertDegreesToRadian(angleRelativeHorizon);
            double num2 = Math.Sin(a) * nonRotatedSize.Height;
            double num3 = Math.Cos(a) * nonRotatedSize.Width;
            double num4 = Math.Sin(a) * nonRotatedSize.Width;
            double num5 = Math.Cos(a) * nonRotatedSize.Height;
            double left = 0.0;
            double top = bounds.Top;
            switch (stringFormat.Alignment)
            {
                case StringAlignment.Near:
                    left = bounds.Left;
                    if (stringFormat.LineAlignment == StringAlignment.Near)
                    {
                        if (angleRelativeHorizon > 0)
                        {
                            top += num4;
                        }
                        else
                        {
                            left -= num2;
                        }
                    }
                    else if (stringFormat.LineAlignment == StringAlignment.Center)
                    {
                        top += (bounds.Height / 2) + ((num4 - num5) / 2.0);
                        if (angleRelativeHorizon < 0)
                        {
                            left -= num2;
                        }
                    }
                    else
                    {
                        top += bounds.Height;
                        if (angleRelativeHorizon > 0)
                        {
                            left += num2;
                        }
                        else
                        {
                            top += num4;
                        }
                    }
                    break;

                case StringAlignment.Center:
                    left = (bounds.Left + bounds.Right) / 2;
                    if (stringFormat.LineAlignment == StringAlignment.Near)
                    {
                        left -= num2 / 2.0;
                        top += Math.Abs(num4) / 2.0;
                    }
                    else if (stringFormat.LineAlignment == StringAlignment.Center)
                    {
                        left -= num2 / 2.0;
                        top += (bounds.Height / 2) - (num5 / 2.0);
                    }
                    else
                    {
                        left += num2 / 2.0;
                        top += bounds.Height - (Math.Abs(num4) / 2.0);
                    }
                    break;

                case StringAlignment.Far:
                    left = bounds.Right;
                    if (stringFormat.LineAlignment == StringAlignment.Near)
                    {
                        if (angleRelativeHorizon > 0)
                        {
                            left -= num2;
                        }
                        else
                        {
                            top -= num4;
                        }
                    }
                    else if (stringFormat.LineAlignment == StringAlignment.Center)
                    {
                        top += (bounds.Height / 2) - ((num4 + num5) / 2.0);
                        if (angleRelativeHorizon > 0)
                        {
                            left -= num2;
                        }
                    }
                    else
                    {
                        top += bounds.Height;
                        if (angleRelativeHorizon < 0)
                        {
                            left += num2;
                        }
                        else
                        {
                            top -= num4;
                        }
                    }
                    break;

                default:
                    break;
            }
            Point position = new Point((int) left, (int) top);
            return ArrangeRotatedTextToBounds(bounds, nonRotatedSize, position, angleRelativeHorizon, stringFormat);
        }

        public static bool IsRotated(int textRotation) => 
            (textRotation != 0) && (textRotation != 0xff);

        public static bool IsRotated(int textRotationInModelUnits, DocumentModelUnitConverter unitConverter) => 
            IsRotated(unitConverter.ModelUnitsToDegree(textRotationInModelUnits));

        public static bool IsVertical(int textRotation) => 
            textRotation == 0xff;
    }
}

