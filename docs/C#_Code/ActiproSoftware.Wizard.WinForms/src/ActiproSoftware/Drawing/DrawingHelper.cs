namespace ActiproSoftware.Drawing
{
    using #aXd;
    using #H;
    using ActiproSoftware.Products.Shared;
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.Drawing.Text;
    using System.Windows.Forms;

    public class DrawingHelper
    {
        private static StringFormat #bre = GetStringFormat(StringAlignment.Near, StringAlignment.Near, StringTrimming.None, false, false);

        private static GraphicsPath #kwe(Rectangle #Bo, int #LHf, int #MHf)
        {
            GraphicsPath path1 = new GraphicsPath();
            path1.StartFigure();
            path1.AddArc((#Bo.Right - #LHf) - 1, #Bo.Top, #LHf, #MHf, 270f, 90f);
            GraphicsPath local1 = path1;
            local1.AddArc((#Bo.Right - #LHf) - 1, (#Bo.Bottom - #MHf) - 1, #LHf, #MHf, 0f, 90f);
            local1.AddArc(#Bo.Left, (#Bo.Bottom - #MHf) - 1, #LHf, #MHf, 90f, 90f);
            local1.AddArc(#Bo.Left, #Bo.Top, #LHf, #MHf, 180f, 90f);
            local1.CloseFigure();
            return local1;
        }

        public static void DrawImage(Graphics g, Image image, int x, int y, float alpha, RotateFlipType imageRotation)
        {
            DrawImage(g, image, x, y, image.Width, image.Height, alpha, imageRotation);
        }

        public static void DrawImage(Graphics g, Image image, int x, int y, int width, int height, float alpha, RotateFlipType imageRotation)
        {
            if ((width > 0) && (height > 0))
            {
                if ((alpha == 1f) && (imageRotation == RotateFlipType.RotateNoneFlipNone))
                {
                    g.DrawImage(image, new Rectangle(x, y, width, height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);
                }
                else
                {
                    ColorMatrix newColorMatrix = new ColorMatrix {
                        Matrix00 = 1f,
                        Matrix11 = 1f,
                        Matrix22 = 1f,
                        Matrix33 = alpha,
                        Matrix44 = 1f
                    };
                    ImageAttributes imageAttr = new ImageAttributes();
                    imageAttr.SetColorMatrix(newColorMatrix);
                    Image image2 = null;
                    if (imageRotation == RotateFlipType.RotateNoneFlipNone)
                    {
                        image2 = image;
                    }
                    else
                    {
                        Image image1 = (Image) image.Clone();
                        image1.RotateFlip(imageRotation);
                        image2 = image1;
                        if (image2.Width != image.Width)
                        {
                            int num1 = width;
                            width = height;
                            height = num1;
                        }
                    }
                    g.DrawImage(image2, new Rectangle(x, y, width, height), 0, 0, image2.Width, image2.Height, GraphicsUnit.Pixel, imageAttr);
                }
            }
        }

        public static void DrawImageShadow(Graphics g, Image image, int x, int y, float alpha, RotateFlipType imageRotation)
        {
            ColorMatrix newColorMatrix = new ColorMatrix {
                Matrix00 = 0f,
                Matrix11 = 0f,
                Matrix22 = 0f,
                Matrix33 = alpha,
                Matrix44 = 1f
            };
            ImageAttributes imageAttr = new ImageAttributes();
            imageAttr.SetColorMatrix(newColorMatrix);
            Image image2 = null;
            if (imageRotation == RotateFlipType.RotateNoneFlipNone)
            {
                image2 = image;
            }
            else
            {
                Image image1 = (Image) image.Clone();
                image1.RotateFlip(imageRotation);
                image2 = image1;
            }
            g.DrawImage(image2, new Rectangle(x, y, image2.Width, image2.Height), 0, 0, image2.Width, image2.Height, GraphicsUnit.Pixel, imageAttr);
        }

        public static void DrawRectangle(Graphics g, Rectangle bounds, Pen pen)
        {
            g.DrawRectangle(pen, bounds.Left, bounds.Top, bounds.Width - 1, bounds.Height - 1);
        }

        public static void DrawRectangle(Graphics g, Rectangle bounds, Color color, DashStyle dashStyle)
        {
            if ((bounds.Width > 0) && (bounds.Height > 0))
            {
                Pen pen = new Pen(color) {
                    DashStyle = dashStyle
                };
                DrawRectangle(g, bounds, pen);
                pen.Dispose();
            }
        }

        public static void DrawRoundedRectangle(Graphics g, Rectangle bounds, int offsetX, int offsetY, Pen pen)
        {
            if ((bounds.Width > 0) && (bounds.Height > 0))
            {
                GraphicsPath path = #kwe(bounds, offsetX, offsetY);
                g.DrawPath(pen, path);
                path.Dispose();
            }
        }

        public static void DrawString(Graphics g, string text, Font font, Color color, RectangleF bounds, StringFormat format)
        {
            if ((bounds.Width > 0f) && (bounds.Height > 0f))
            {
                if (((format.FormatFlags & StringFormatFlags.DirectionVertical) == StringFormatFlags.DirectionVertical) || (((format.Trimming == StringTrimming.EllipsisPath) && (text.IndexOf('\\') == -1)) && (MeasureString(g, text, font, format).Width > bounds.Width)))
                {
                    SolidBrush brush = new SolidBrush(color);
                    g.DrawString(text, font, brush, bounds, format);
                    brush.Dispose();
                }
                else
                {
                    uint num = 0;
                    if (((format.FormatFlags & StringFormatFlags.NoWrap) == StringFormatFlags.NoWrap) && (text.IndexOf('\n') == -1))
                    {
                        num |= (uint) 0x20;
                    }
                    StringAlignment lineAlignment = format.Alignment;
                    if (lineAlignment == StringAlignment.Center)
                    {
                        num |= 1;
                    }
                    else if (lineAlignment == StringAlignment.Far)
                    {
                        num |= 2;
                    }
                    lineAlignment = format.LineAlignment;
                    if (lineAlignment == StringAlignment.Center)
                    {
                        num |= 4;
                    }
                    else if (lineAlignment == StringAlignment.Far)
                    {
                        num |= 8;
                    }
                    if ((format.FormatFlags & StringFormatFlags.NoWrap) == 0)
                    {
                        num |= (uint) 0x10;
                    }
                    if (format.HotkeyPrefix == HotkeyPrefix.None)
                    {
                        num |= 0x800;
                    }
                    if ((format.FormatFlags & StringFormatFlags.NoClip) == StringFormatFlags.NoClip)
                    {
                        num |= 0x100;
                    }
                    switch (format.Trimming)
                    {
                        case StringTrimming.EllipsisCharacter:
                            num |= 0x8000;
                            break;

                        case StringTrimming.EllipsisWord:
                            num |= 0x40000;
                            break;

                        case StringTrimming.EllipsisPath:
                            num |= 0x4000;
                            break;

                        default:
                            break;
                    }
                    Matrix transform = g.Transform;
                    bounds.Offset(transform.OffsetX, transform.OffsetY);
                    transform.Dispose();
                    IntPtr zero = IntPtr.Zero;
                    IntPtr ptr2 = IntPtr.Zero;
                    if ((format.FormatFlags & StringFormatFlags.NoClip) == 0)
                    {
                        zero = g.Clip.GetHrgn(g);
                    }
                    IntPtr hdc = g.GetHdc();
                    if (zero != IntPtr.Zero)
                    {
                        #Bi.#Axe(hdc, ptr2);
                        #Bi.#ape(hdc, zero);
                    }
                    #Bi.#Fi fi = new #Bi.#Fi(bounds);
                    IntPtr ptr4 = font.ToHfont();
                    #Bi.DrawText(hdc, text, text.Length, fi, num);
                    #Bi.#bpe(hdc, #Bi.#bpe(hdc, ptr4));
                    #Bi.#5oe(ptr4);
                    #Bi.#Bxe(hdc, #Bi.#Bxe(hdc, (((color.B & 0xff) << 0x10) | ((color.G & 0xff) << 8)) | color.R));
                    #Bi.#Cxe(hdc, #Bi.#Cxe(hdc, 1));
                    if (zero != IntPtr.Zero)
                    {
                        #Bi.#5oe(zero);
                        #Bi.#ape(hdc, ptr2);
                        #Bi.#5oe(ptr2);
                    }
                    g.ReleaseHdc(hdc);
                }
            }
        }

        public static void DrawWrappedImage(Graphics g, Rectangle bounds, Rectangle brushBounds, Image image, WrapMode wrapMode)
        {
            if ((bounds.Width > 0) && (bounds.Height > 0))
            {
                TextureBrush brush = new TextureBrush(image, wrapMode);
                brush.TranslateTransform((float) brushBounds.Left, (float) brushBounds.Top);
                g.FillRectangle(brush, bounds);
                brush.Dispose();
            }
        }

        public static void FillRoundedRectangle(Graphics g, Rectangle bounds, int offsetX, int offsetY, Brush brush)
        {
            if ((bounds.Width > 0) && (bounds.Height > 0))
            {
                GraphicsPath path = #kwe(bounds, offsetX, offsetY);
                g.FillPath(brush, path);
                path.Dispose();
            }
        }

        public static Point GetCenteredRectangleLocation(Rectangle bounds, Size size) => 
            new Point { 
                X = bounds.Left + ((bounds.Width - size.Width) / 2),
                Y = bounds.Top + ((bounds.Height - size.Height) / 2)
            };

        public static TextureBrush GetHatchedBrush(Color color1, Color color2)
        {
            Bitmap bitmap1 = new Bitmap(2, 2);
            Bitmap bitmap2 = new Bitmap(2, 2);
            bitmap2.SetPixel(0, 0, color1);
            Bitmap local4 = bitmap2;
            Bitmap local5 = bitmap2;
            local5.SetPixel(1, 0, color2);
            int local2 = (int) local5;
            int local3 = (int) local5;
            local3.SetPixel(0, 1, color2);
            int local1 = local3;
            local1.SetPixel(1, 1, color1);
            return new TextureBrush((Image) local1, WrapMode.Tile);
        }

        public static Sides GetOppositeSides(Sides sides)
        {
            if (sides == (Sides.Left | Sides.Bottom | Sides.Right | Sides.Top))
            {
                throw new ArgumentException(ActiproSoftware.Products.Shared.SR.GetString(#G.#eg(0x25e6)));
            }
            Sides sides2 = ((sides & Sides.Left) != 0) ? (((sides & Sides.Top) != 0) ? (((sides & Sides.Right) != 0) ? Sides.Bottom : Sides.Right) : Sides.Top) : Sides.Left;
            if ((sides & Sides.Left) == 0)
            {
                sides2 |= Sides.Left;
            }
            if ((sides & Sides.Top) == 0)
            {
                sides2 |= Sides.Top;
            }
            if ((sides & Sides.Right) == 0)
            {
                sides2 |= Sides.Right;
            }
            if ((sides & Sides.Bottom) == 0)
            {
                sides2 |= Sides.Bottom;
            }
            return sides2;
        }

        public static string GetShortcutTextFromShortcut(Shortcut shortcut) => 
            (shortcut != Shortcut.None) ? (!shortcut.ToString().StartsWith(#G.#eg(0x2eb4)) ? (!shortcut.ToString().StartsWith(#G.#eg(0x2ec2)) ? (!shortcut.ToString().StartsWith(#G.#eg(0x2ee0)) ? (!shortcut.ToString().StartsWith(#G.#eg(0x2ef2)) ? shortcut.ToString() : (#G.#eg(0x2efb) + shortcut.ToString().Substring(5))) : (#G.#eg(0x2ee9) + shortcut.ToString().Substring(4))) : (#G.#eg(0x2ecf) + shortcut.ToString().Substring(9))) : (#G.#eg(0x2eb9) + shortcut.ToString().Substring(3))) : null;

        public static StringFormat GetStringFormat(StringAlignment hAlign, StringAlignment vAlign, StringTrimming trimming, bool wrap, bool vertical)
        {
            StringFormat format = new StringFormat(StringFormat.GenericTypographic) {
                Alignment = hAlign,
                LineAlignment = vAlign,
                Trimming = trimming,
                FormatFlags = StringFormatFlags.LineLimit
            };
            if (!wrap)
            {
                format.FormatFlags |= StringFormatFlags.NoWrap;
            }
            if (vertical)
            {
                format.FormatFlags |= StringFormatFlags.DirectionVertical;
            }
            format.FormatFlags |= StringFormatFlags.MeasureTrailingSpaces;
            return format;
        }

        public static Size MeasureString(Graphics g, string text, Font font, StringFormat format)
        {
            uint num = 0x400;
            StringAlignment lineAlignment = format.Alignment;
            if (lineAlignment == StringAlignment.Center)
            {
                num |= 1;
            }
            else if (lineAlignment == StringAlignment.Far)
            {
                num |= 2;
            }
            lineAlignment = format.LineAlignment;
            if (lineAlignment == StringAlignment.Center)
            {
                num |= 4;
            }
            else if (lineAlignment == StringAlignment.Far)
            {
                num |= 8;
            }
            if (format.HotkeyPrefix == HotkeyPrefix.None)
            {
                num |= 0x800;
            }
            IntPtr hdc = g.GetHdc();
            IntPtr ptr2 = font.ToHfont();
            #Bi.#Fi fi = new #Bi.#Fi(0, 0, 0, 0);
            #Bi.DrawText(hdc, text, text.Length, fi, num);
            #Bi.#bpe(hdc, #Bi.#bpe(hdc, ptr2));
            #Bi.#5oe(ptr2);
            g.ReleaseHdc(hdc);
            Size size = new Size(fi.#3n - fi.#1n, (fi.#4n - fi.#2n) + 1);
            if ((format.FormatFlags & StringFormatFlags.DirectionVertical) == StringFormatFlags.DirectionVertical)
            {
                size = new Size(size.Height, size.Width);
            }
            return size;
        }
    }
}

