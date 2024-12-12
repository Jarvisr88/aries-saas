namespace ActiproSoftware.WinUICore
{
    using #H;
    using ActiproSoftware.Drawing;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Drawing.Text;
    using System.Windows.Forms;

    public class OwnerDrawMenuItem : MenuItem
    {
        private const int #5ue = 6;
        private const int #6ue = 7;
        private const int #7ue = 4;
        private const int #8ue = 20;
        private bool #qgl;
        private System.Drawing.Image #M0d;
        private int #N0d;

        private void #Sxe(Graphics #nYf, WindowsColorScheme #w2d, int #Zn, int #0n, float #mLk)
        {
            PointF[] tfArray1 = new PointF[6];
            PointF[] tfArray2 = new PointF[6];
            tfArray2[0] = new PointF(#Zn + (0f * #mLk), #0n + (2f * #mLk));
            PointF[] local1 = tfArray2;
            local1[1] = new PointF(#Zn + (2f * #mLk), #0n + (4f * #mLk));
            local1[2] = new PointF(#Zn + (6f * #mLk), #0n + (0f * #mLk));
            local1[3] = new PointF(#Zn + (6f * #mLk), #0n + (1f * #mLk));
            local1[4] = new PointF(#Zn + (2f * #mLk), #0n + (5f * #mLk));
            local1[5] = new PointF(#Zn + (0f * #mLk), #0n + (3f * #mLk));
            PointF[] points = local1;
            Brush brush = new SolidBrush(WindowsColorScheme.WindowsDefault.BarButtonText);
            #nYf.FillPolygon(brush, points);
            brush.Dispose();
            Pen pen = new Pen(WindowsColorScheme.WindowsDefault.BarButtonText);
            #nYf.DrawPolygon(pen, points);
            pen.Dispose();
        }

        public OwnerDrawMenuItem() : this(string.Empty, -1, null, Shortcut.None, null)
        {
        }

        public OwnerDrawMenuItem(string text) : this(text, -1, null, Shortcut.None, null)
        {
        }

        public OwnerDrawMenuItem(string text, EventHandler onClick) : this(text, -1, onClick, Shortcut.None, null)
        {
        }

        public OwnerDrawMenuItem(string text, int imageIndex) : this(text, imageIndex, null, Shortcut.None, null)
        {
        }

        public OwnerDrawMenuItem(string text, Shortcut shortcut) : this(text, -1, null, shortcut, null)
        {
        }

        public OwnerDrawMenuItem(string text, bool isChecked, EventHandler onClick, Shortcut shortcut, object tag) : this(text, -1, onClick, shortcut, tag)
        {
            base.Checked = isChecked;
        }

        public OwnerDrawMenuItem(string text, int imageIndex, EventHandler onClick, Shortcut shortcut, object tag) : base(text, onClick, shortcut)
        {
            this.#qgl = true;
            this.#N0d = -1;
            this.#N0d = imageIndex;
            base.Tag = tag;
            base.OwnerDraw = true;
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            Rectangle bounds = e.Bounds;
            if (!bounds.IsEmpty)
            {
                Graphics g = e.Graphics;
                WindowsColorScheme colorScheme = this.ColorScheme;
                bool flag = (e.State & DrawItemState.Checked) == DrawItemState.Checked;
                bool flag2 = (e.State & DrawItemState.Disabled) != DrawItemState.Disabled;
                bool flag3 = (e.State & DrawItemState.Selected) == DrawItemState.Selected;
                if (base.Parent is MainMenu)
                {
                    if (!(flag2 & flag3))
                    {
                        g.FillRectangle(SystemBrushes.Control, e.Bounds.Left, e.Bounds.Top, e.Bounds.Width, e.Bounds.Height);
                    }
                    else
                    {
                        SolidBrush brush = new SolidBrush(colorScheme.BarButtonHotBack);
                        g.FillRectangle(brush, e.Bounds);
                        brush.Dispose();
                        Pen pen = new Pen(colorScheme.BarButtonHotBorder);
                        g.DrawRectangle(pen, e.Bounds.Left, e.Bounds.Top, e.Bounds.Width - 1, e.Bounds.Height - 1);
                        pen.Dispose();
                    }
                    StringFormat format = DrawingHelper.GetStringFormat(StringAlignment.Center, StringAlignment.Center, StringTrimming.None, false, false);
                    format.HotkeyPrefix = HotkeyPrefix.Show;
                    if (flag2)
                    {
                        DrawingHelper.DrawString(g, base.Text, SystemInformation.MenuFont, colorScheme.BarButtonText, e.Bounds, format);
                    }
                    else
                    {
                        DrawingHelper.DrawString(g, base.Text, SystemInformation.MenuFont, colorScheme.BarButtonTextDisabled, e.Bounds, format);
                    }
                    format.Dispose();
                }
                else
                {
                    ImageList imageList = null;
                    System.Drawing.Image image = null;
                    int width = 0x10;
                    int height = 0x10;
                    if (this.#M0d != null)
                    {
                        image = this.#M0d;
                        width = this.#M0d.Width;
                        height = this.#M0d.Height;
                    }
                    else
                    {
                        MainMenu mainMenu = base.GetMainMenu();
                        if (mainMenu != null)
                        {
                            if (mainMenu is OwnerDrawMainMenu)
                            {
                                imageList = ((OwnerDrawMainMenu) mainMenu).ImageList;
                                if (imageList != null)
                                {
                                    if (this.#N0d != -1)
                                    {
                                        image = imageList.Images[this.#N0d];
                                    }
                                    width = imageList.ImageSize.Width;
                                    height = imageList.ImageSize.Height;
                                }
                            }
                        }
                        else
                        {
                            ContextMenu contextMenu = base.GetContextMenu();
                            if ((contextMenu != null) && (contextMenu is OwnerDrawContextMenu))
                            {
                                imageList = ((OwnerDrawContextMenu) contextMenu).ImageList;
                                if (imageList != null)
                                {
                                    if (this.#N0d != -1)
                                    {
                                        image = imageList.Images[this.#N0d];
                                    }
                                    width = imageList.ImageSize.Width;
                                    height = imageList.ImageSize.Height;
                                }
                            }
                        }
                    }
                    float num3 = this.CanAutoScaleImage ? (e.Graphics.DpiX / 96f) : 1f;
                    width = (int) (width * num3);
                    height = (int) (height * num3);
                    int num4 = width + 8;
                    Point point = new Point(e.Bounds.Left + ((int) (((double) (num4 - width)) / 2.0)), e.Bounds.Top + ((int) (((double) (e.Bounds.Height - height)) / 2.0)));
                    if (base.Text == #G.#eg(0x5c))
                    {
                        TwoColorLinearGradient.Draw(g, new Rectangle(e.Bounds.Left, e.Bounds.Top, e.Bounds.Left + num4, e.Bounds.Height), new Rectangle(e.Bounds.Left, e.Bounds.Top, e.Bounds.Left + num4, e.Bounds.Height), colorScheme.MenuIconColumnBackGradientBegin, colorScheme.MenuIconColumnBackGradientEnd, 0f, TwoColorLinearGradientStyle.Normal);
                        SolidBrush brush = new SolidBrush(colorScheme.MenuBack);
                        g.FillRectangle(brush, e.Bounds.Left + num4, e.Bounds.Top, e.Bounds.Width - num4, e.Bounds.Height);
                        brush.Dispose();
                        Pen pen = new Pen(colorScheme.MenuBorder);
                        g.DrawLine(pen, (int) ((e.Bounds.Left + num4) + 8), (int) (e.Bounds.Top + 1), (int) (e.Bounds.Left + e.Bounds.Width), (int) (e.Bounds.Top + 1));
                        pen.Dispose();
                    }
                    else
                    {
                        if (!(flag2 & flag3))
                        {
                            TwoColorLinearGradient.Draw(g, new Rectangle(e.Bounds.Left, e.Bounds.Top, e.Bounds.Left + num4, e.Bounds.Height), new Rectangle(e.Bounds.Left, e.Bounds.Top, e.Bounds.Left + num4, e.Bounds.Height), colorScheme.MenuIconColumnBackGradientBegin, colorScheme.MenuIconColumnBackGradientEnd, 0f, TwoColorLinearGradientStyle.Normal);
                            SolidBrush brush = new SolidBrush(colorScheme.MenuBack);
                            g.FillRectangle(brush, e.Bounds.Left + num4, e.Bounds.Top, e.Bounds.Width - num4, e.Bounds.Height);
                            brush.Dispose();
                        }
                        else
                        {
                            SolidBrush brush = new SolidBrush(colorScheme.BarButtonHotBack);
                            g.FillRectangle(brush, e.Bounds);
                            brush.Dispose();
                            Pen pen = new Pen(colorScheme.BarButtonHotBorder);
                            g.DrawRectangle(pen, e.Bounds.Left, e.Bounds.Top, e.Bounds.Width - 1, e.Bounds.Height - 1);
                            pen.Dispose();
                        }
                        if (flag)
                        {
                            SolidBrush brush = new SolidBrush(flag3 ? colorScheme.BarButtonPressedBack : colorScheme.BarButtonCheckedBack);
                            g.FillRectangle(brush, (int) (e.Bounds.Left + 1), (int) (e.Bounds.Top + 1), (int) (num4 - 4), (int) (e.Bounds.Height - 3));
                            brush.Dispose();
                            Pen pen = new Pen(colorScheme.BarButtonCheckedBorder);
                            g.DrawRectangle(pen, (int) (e.Bounds.Left + 1), (int) (e.Bounds.Top + 1), (int) (num4 - 4), (int) (e.Bounds.Height - 3));
                            pen.Dispose();
                            if (image != null)
                            {
                                g.DrawImage(image, point.X, point.Y, width, height);
                            }
                            else
                            {
                                int num6 = (int) (6f * num3);
                                this.#Sxe(g, colorScheme, e.Bounds.Left + ((num4 - ((int) (7f * num3))) / 2), e.Bounds.Top + ((e.Bounds.Height - num6) / 2), num3);
                            }
                        }
                        else if (image != null)
                        {
                            if (!(!flag2 | flag3))
                            {
                                if (colorScheme.ColorSchemeType != WindowsColorSchemeType.WindowsClassic)
                                {
                                    g.DrawImage(image, point.X, point.Y, width, height);
                                }
                                else
                                {
                                    ColorMatrix newColorMatrix = new ColorMatrix {
                                        Matrix00 = 1f,
                                        Matrix11 = 1f,
                                        Matrix22 = 1f,
                                        Matrix33 = 0.7f,
                                        Matrix44 = 1f
                                    };
                                    ImageAttributes imageAttr = new ImageAttributes();
                                    imageAttr.SetColorMatrix(newColorMatrix);
                                    g.DrawImage(image, new Rectangle(point.X, point.Y, width, height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, imageAttr);
                                }
                            }
                            else
                            {
                                ColorMatrix newColorMatrix = new ColorMatrix {
                                    Matrix00 = 0f,
                                    Matrix11 = 0f,
                                    Matrix22 = 0f,
                                    Matrix33 = 0.3f,
                                    Matrix44 = 1f
                                };
                                ImageAttributes imageAttr = new ImageAttributes();
                                imageAttr.SetColorMatrix(newColorMatrix);
                                if (!flag2)
                                {
                                    ControlPaint.DrawImageDisabled(g, image, e.Bounds.Left + 4, e.Bounds.Top + 4, Color.White);
                                }
                                else if (colorScheme.ColorSchemeType != WindowsColorSchemeType.WindowsClassic)
                                {
                                    g.DrawImage(image, point.X, point.Y, width, height);
                                }
                                else
                                {
                                    g.DrawImage(image, new Rectangle(point.X + 1, point.Y + 1, width, height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, imageAttr);
                                    g.DrawImage(image, point.X - 1, point.Y - 1, width, height);
                                }
                            }
                        }
                        Rectangle rectangle2 = new Rectangle((e.Bounds.Left + num4) + 8, e.Bounds.Top, (e.Bounds.Width - (2 * num4)) - 8, e.Bounds.Height);
                        StringFormat format = DrawingHelper.GetStringFormat(StringAlignment.Near, StringAlignment.Center, StringTrimming.None, false, false);
                        format.HotkeyPrefix = HotkeyPrefix.Show;
                        if (flag2)
                        {
                            DrawingHelper.DrawString(g, base.Text, SystemInformation.MenuFont, colorScheme.BarButtonText, rectangle2, format);
                            if (base.ShowShortcut && (base.Shortcut != Shortcut.None))
                            {
                                format.Alignment = StringAlignment.Far;
                                DrawingHelper.DrawString(g, this.ShortcutText, SystemInformation.MenuFont, colorScheme.BarButtonText, rectangle2, format);
                            }
                        }
                        else
                        {
                            DrawingHelper.DrawString(g, base.Text, SystemInformation.MenuFont, colorScheme.BarButtonTextDisabled, rectangle2, format);
                            if (base.ShowShortcut && (base.Shortcut != Shortcut.None))
                            {
                                format.Alignment = StringAlignment.Far;
                                DrawingHelper.DrawString(g, this.ShortcutText, SystemInformation.MenuFont, colorScheme.BarButtonTextDisabled, rectangle2, format);
                            }
                        }
                        format.Dispose();
                    }
                }
            }
        }

        protected override void OnMeasureItem(MeasureItemEventArgs e)
        {
            this.OnMeasureItem(e);
            StringFormat format = DrawingHelper.GetStringFormat(StringAlignment.Near, StringAlignment.Near, StringTrimming.None, false, false);
            format.HotkeyPrefix = HotkeyPrefix.Show;
            if (base.Parent is MainMenu)
            {
                e.ItemHeight = SystemInformation.MenuFont.Height + 8;
                e.ItemWidth = DrawingHelper.MeasureString(e.Graphics, base.Text, SystemInformation.MenuFont, format).Width + 6;
            }
            else if (base.Text == #G.#eg(0x5c))
            {
                e.ItemHeight = 3;
                e.ItemWidth = 20;
            }
            else
            {
                ImageList imageList = null;
                int width = 0x10;
                int height = 0x10;
                if (this.#M0d != null)
                {
                    width = this.#M0d.Width;
                    height = this.#M0d.Height;
                }
                else
                {
                    try
                    {
                        MainMenu mainMenu = base.GetMainMenu();
                        if (mainMenu != null)
                        {
                            if (mainMenu is OwnerDrawMainMenu)
                            {
                                imageList = ((OwnerDrawMainMenu) mainMenu).ImageList;
                                width = imageList.ImageSize.Width;
                                height = imageList.ImageSize.Height;
                            }
                        }
                        else
                        {
                            ContextMenu contextMenu = base.GetContextMenu();
                            if ((contextMenu != null) && (contextMenu is OwnerDrawContextMenu))
                            {
                                imageList = ((OwnerDrawContextMenu) contextMenu).ImageList;
                                if (imageList != null)
                                {
                                    width = imageList.ImageSize.Width;
                                    height = imageList.ImageSize.Height;
                                }
                            }
                        }
                    }
                    catch
                    {
                    }
                }
                float num3 = this.CanAutoScaleImage ? (e.Graphics.DpiX / 96f) : 1f;
                width = (int) (width * num3);
                height = (int) (height * num3);
                e.ItemHeight = Math.Max((int) (SystemInformation.MenuFont.Height + 4), (int) (height + 8));
                e.ItemWidth = ((DrawingHelper.MeasureString(e.Graphics, base.Text, SystemInformation.MenuFont, format).Width + width) + 0x10) + 20;
                if (base.ShowShortcut && (base.Shortcut != Shortcut.None))
                {
                    e.ItemWidth += 10 + DrawingHelper.MeasureString(e.Graphics, this.ShortcutText, SystemInformation.MenuFont, format).Width;
                }
            }
            format.Dispose();
        }

        private WindowsColorScheme ColorScheme
        {
            get
            {
                WindowsColorScheme colorSchemeResolved;
                try
                {
                    MainMenu mainMenu = this.GetMainMenu();
                    if ((mainMenu == null) || !(mainMenu is OwnerDrawMainMenu))
                    {
                        ContextMenu contextMenu = base.GetContextMenu();
                        if ((contextMenu != null) && (contextMenu is OwnerDrawContextMenu))
                        {
                            colorSchemeResolved = ((OwnerDrawContextMenu) contextMenu).ColorSchemeResolved;
                        }
                        else
                        {
                            goto TR_0000;
                        }
                    }
                    else
                    {
                        colorSchemeResolved = ((OwnerDrawMainMenu) mainMenu).ColorSchemeResolved;
                    }
                }
                catch
                {
                    goto TR_0000;
                }
                return colorSchemeResolved;
            TR_0000:
                return WindowsColorScheme.WindowsDefault;
            }
        }

        [Category("Appearance"), Description("Whether drawn images can auto-scale based on DPI."), DefaultValue(true)]
        public bool CanAutoScaleImage
        {
            get => 
                this.#qgl;
            set => 
                this.#qgl = value;
        }

        [Category("Appearance"), Description("The image to display on the menu item."), DefaultValue((string) null)]
        public System.Drawing.Image Image
        {
            get => 
                this.#M0d;
            set => 
                this.#M0d = value;
        }

        [Category("Appearance"), Description("The index of an image within the ImageList."), DefaultValue(-1)]
        public int ImageIndex
        {
            get => 
                this.#N0d;
            set => 
                this.#N0d = value;
        }

        public string ShortcutText =>
            DrawingHelper.GetShortcutTextFromShortcut(this.Shortcut);
    }
}

