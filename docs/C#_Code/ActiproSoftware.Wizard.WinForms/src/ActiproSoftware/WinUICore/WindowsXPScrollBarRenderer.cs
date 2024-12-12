namespace ActiproSoftware.WinUICore
{
    using ActiproSoftware.Drawing;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using System.Windows.Forms.VisualStyles;

    [ToolboxItem(true)]
    public class WindowsXPScrollBarRenderer : ActiproSoftware.WinUICore.ScrollBarRenderer
    {
        private bool #Yve = true;

        public override void DrawScrollBarBackground(PaintEventArgs e, Rectangle bounds, ActiproSoftware.WinUICore.ScrollBar scrollBar)
        {
            if (!this.#Yve || !VisualStyleRenderer.IsSupported)
            {
                HatchedBackgroundFill.Draw(e.Graphics, bounds, SystemColors.ControlLightLight, SystemColors.ScrollBar);
            }
            else if (!scrollBar.Enabled)
            {
                if (scrollBar.Orientation == Orientation.Horizontal)
                {
                    new VisualStyleRenderer(VisualStyleElement.ScrollBar.RightTrackHorizontal.Disabled).DrawBackground(e.Graphics, bounds, e.ClipRectangle);
                }
                else
                {
                    new VisualStyleRenderer(VisualStyleElement.ScrollBar.LowerTrackVertical.Disabled).DrawBackground(e.Graphics, bounds, e.ClipRectangle);
                }
            }
            else
            {
                Rectangle barrelBeforeThumbBounds = scrollBar.BarrelBeforeThumbBounds;
                if ((barrelBeforeThumbBounds.Width > 0) && (barrelBeforeThumbBounds.Height > 0))
                {
                    bool flag = ReferenceEquals(scrollBar.CurrentScrollCommand, scrollBar.DecreaseLargeCommand);
                    if (scrollBar.Orientation == Orientation.Horizontal)
                    {
                        new VisualStyleRenderer(flag ? VisualStyleElement.ScrollBar.LeftTrackHorizontal.Pressed : VisualStyleElement.ScrollBar.LeftTrackHorizontal.Normal).DrawBackground(e.Graphics, barrelBeforeThumbBounds, e.ClipRectangle);
                    }
                    else
                    {
                        new VisualStyleRenderer(flag ? VisualStyleElement.ScrollBar.UpperTrackVertical.Pressed : VisualStyleElement.ScrollBar.UpperTrackVertical.Normal).DrawBackground(e.Graphics, barrelBeforeThumbBounds, e.ClipRectangle);
                    }
                }
                barrelBeforeThumbBounds = scrollBar.BarrelAfterThumbBounds;
                if ((barrelBeforeThumbBounds.Width > 0) && (barrelBeforeThumbBounds.Height > 0))
                {
                    bool flag2 = ReferenceEquals(scrollBar.CurrentScrollCommand, scrollBar.IncreaseLargeCommand);
                    if (scrollBar.Orientation == Orientation.Horizontal)
                    {
                        new VisualStyleRenderer(flag2 ? VisualStyleElement.ScrollBar.RightTrackHorizontal.Pressed : VisualStyleElement.ScrollBar.RightTrackHorizontal.Normal).DrawBackground(e.Graphics, barrelBeforeThumbBounds, e.ClipRectangle);
                    }
                    else
                    {
                        new VisualStyleRenderer(flag2 ? VisualStyleElement.ScrollBar.LowerTrackVertical.Pressed : VisualStyleElement.ScrollBar.LowerTrackVertical.Normal).DrawBackground(e.Graphics, barrelBeforeThumbBounds, e.ClipRectangle);
                    }
                }
            }
        }

        public override void DrawScrollBarButton(PaintEventArgs e, Rectangle bounds, ScrollBarButton button)
        {
            if (!this.#Yve || !VisualStyleRenderer.IsSupported)
            {
                ButtonState normal = ButtonState.Normal;
                UIElementDrawState drawState = button.GetDrawState();
                if ((drawState & UIElementDrawState.Disabled) == UIElementDrawState.Disabled)
                {
                    normal = ButtonState.Inactive;
                }
                else if ((drawState & (UIElementDrawState.Pressed | UIElementDrawState.Hot)) == (UIElementDrawState.Pressed | UIElementDrawState.Hot))
                {
                    normal = ButtonState.Pushed;
                }
                if (ReferenceEquals(button.CommandLink.Command, button.ScrollBar.DecreaseSmallCommand))
                {
                    ControlPaint.DrawScrollButton(e.Graphics, bounds, (button.ScrollBar.Orientation == Orientation.Horizontal) ? ScrollButton.Left : ScrollButton.Up, normal);
                }
                else if (ReferenceEquals(button.CommandLink.Command, button.ScrollBar.IncreaseSmallCommand))
                {
                    ControlPaint.DrawScrollButton(e.Graphics, bounds, (button.ScrollBar.Orientation == Orientation.Horizontal) ? ScrollButton.Right : ScrollButton.Down, normal);
                }
            }
            else
            {
                UIElementDrawState drawState = button.GetDrawState();
                if (ReferenceEquals(button.CommandLink.Command, button.ScrollBar.DecreaseSmallCommand))
                {
                    if (button.ScrollBar.Orientation == Orientation.Horizontal)
                    {
                        if ((drawState & UIElementDrawState.Disabled) == UIElementDrawState.Disabled)
                        {
                            new VisualStyleRenderer(VisualStyleElement.ScrollBar.ArrowButton.LeftDisabled).DrawBackground(e.Graphics, bounds, e.ClipRectangle);
                        }
                        else if ((drawState & UIElementDrawState.Pressed) == UIElementDrawState.Pressed)
                        {
                            new VisualStyleRenderer(VisualStyleElement.ScrollBar.ArrowButton.LeftPressed).DrawBackground(e.Graphics, bounds, e.ClipRectangle);
                        }
                        else if ((drawState & UIElementDrawState.Hot) == UIElementDrawState.Hot)
                        {
                            new VisualStyleRenderer(VisualStyleElement.ScrollBar.ArrowButton.LeftHot).DrawBackground(e.Graphics, bounds, e.ClipRectangle);
                        }
                        else
                        {
                            new VisualStyleRenderer(VisualStyleElement.ScrollBar.ArrowButton.LeftNormal).DrawBackground(e.Graphics, bounds, e.ClipRectangle);
                        }
                    }
                    else if ((drawState & UIElementDrawState.Disabled) == UIElementDrawState.Disabled)
                    {
                        new VisualStyleRenderer(VisualStyleElement.ScrollBar.ArrowButton.UpDisabled).DrawBackground(e.Graphics, bounds, e.ClipRectangle);
                    }
                    else if ((drawState & UIElementDrawState.Pressed) == UIElementDrawState.Pressed)
                    {
                        new VisualStyleRenderer(VisualStyleElement.ScrollBar.ArrowButton.UpPressed).DrawBackground(e.Graphics, bounds, e.ClipRectangle);
                    }
                    else if ((drawState & UIElementDrawState.Hot) == UIElementDrawState.Hot)
                    {
                        new VisualStyleRenderer(VisualStyleElement.ScrollBar.ArrowButton.UpHot).DrawBackground(e.Graphics, bounds, e.ClipRectangle);
                    }
                    else
                    {
                        new VisualStyleRenderer(VisualStyleElement.ScrollBar.ArrowButton.UpNormal).DrawBackground(e.Graphics, bounds, e.ClipRectangle);
                    }
                }
                else if (ReferenceEquals(button.CommandLink.Command, button.ScrollBar.IncreaseSmallCommand))
                {
                    if (button.ScrollBar.Orientation == Orientation.Horizontal)
                    {
                        if ((drawState & UIElementDrawState.Disabled) == UIElementDrawState.Disabled)
                        {
                            new VisualStyleRenderer(VisualStyleElement.ScrollBar.ArrowButton.RightDisabled).DrawBackground(e.Graphics, bounds, e.ClipRectangle);
                        }
                        else if ((drawState & UIElementDrawState.Pressed) == UIElementDrawState.Pressed)
                        {
                            new VisualStyleRenderer(VisualStyleElement.ScrollBar.ArrowButton.RightPressed).DrawBackground(e.Graphics, bounds, e.ClipRectangle);
                        }
                        else if ((drawState & UIElementDrawState.Hot) == UIElementDrawState.Hot)
                        {
                            new VisualStyleRenderer(VisualStyleElement.ScrollBar.ArrowButton.RightHot).DrawBackground(e.Graphics, bounds, e.ClipRectangle);
                        }
                        else
                        {
                            new VisualStyleRenderer(VisualStyleElement.ScrollBar.ArrowButton.RightNormal).DrawBackground(e.Graphics, bounds, e.ClipRectangle);
                        }
                    }
                    else if ((drawState & UIElementDrawState.Disabled) == UIElementDrawState.Disabled)
                    {
                        new VisualStyleRenderer(VisualStyleElement.ScrollBar.ArrowButton.DownDisabled).DrawBackground(e.Graphics, bounds, e.ClipRectangle);
                    }
                    else if ((drawState & UIElementDrawState.Pressed) == UIElementDrawState.Pressed)
                    {
                        new VisualStyleRenderer(VisualStyleElement.ScrollBar.ArrowButton.DownPressed).DrawBackground(e.Graphics, bounds, e.ClipRectangle);
                    }
                    else if ((drawState & UIElementDrawState.Hot) == UIElementDrawState.Hot)
                    {
                        new VisualStyleRenderer(VisualStyleElement.ScrollBar.ArrowButton.DownHot).DrawBackground(e.Graphics, bounds, e.ClipRectangle);
                    }
                    else
                    {
                        new VisualStyleRenderer(VisualStyleElement.ScrollBar.ArrowButton.DownNormal).DrawBackground(e.Graphics, bounds, e.ClipRectangle);
                    }
                }
            }
        }

        public override void DrawScrollBarThumb(PaintEventArgs e, Rectangle bounds, ScrollBarThumb thumb)
        {
            if (thumb.ScrollBar.Enabled)
            {
                if (!this.#Yve || !VisualStyleRenderer.IsSupported)
                {
                    e.Graphics.FillRectangle(SystemBrushes.Control, bounds);
                    e.Graphics.DrawLine(SystemPens.ControlLight, bounds.Left, bounds.Bottom - 2, bounds.Left, bounds.Top);
                    e.Graphics.DrawLine(SystemPens.ControlLight, bounds.Left, bounds.Top, bounds.Right - 2, bounds.Top);
                    e.Graphics.DrawLine(SystemPens.ControlDarkDark, bounds.Right - 1, bounds.Top, bounds.Right - 1, bounds.Bottom - 1);
                    e.Graphics.DrawLine(SystemPens.ControlDarkDark, bounds.Right - 1, bounds.Bottom - 1, bounds.Left, bounds.Bottom - 1);
                    e.Graphics.DrawLine(SystemPens.ControlLightLight, (int) (bounds.Left + 1), (int) (bounds.Bottom - 3), (int) (bounds.Left + 1), (int) (bounds.Top + 1));
                    e.Graphics.DrawLine(SystemPens.ControlLightLight, (int) (bounds.Left + 1), (int) (bounds.Top + 1), (int) (bounds.Right - 3), (int) (bounds.Top + 1));
                    e.Graphics.DrawLine(SystemPens.ControlDark, (int) (bounds.Right - 2), (int) (bounds.Top + 1), (int) (bounds.Right - 2), (int) (bounds.Bottom - 2));
                    e.Graphics.DrawLine(SystemPens.ControlDark, (int) (bounds.Right - 2), (int) (bounds.Bottom - 2), (int) (bounds.Left + 1), (int) (bounds.Bottom - 2));
                }
                else
                {
                    UIElementDrawState drawState = thumb.GetDrawState();
                    if (thumb.ScrollBar.Orientation == Orientation.Horizontal)
                    {
                        if ((drawState & UIElementDrawState.Pressed) == UIElementDrawState.Pressed)
                        {
                            new VisualStyleRenderer(VisualStyleElement.ScrollBar.ThumbButtonHorizontal.Pressed).DrawBackground(e.Graphics, bounds, e.ClipRectangle);
                        }
                        else if ((drawState & UIElementDrawState.Hot) == UIElementDrawState.Hot)
                        {
                            new VisualStyleRenderer(VisualStyleElement.ScrollBar.ThumbButtonHorizontal.Hot).DrawBackground(e.Graphics, bounds, e.ClipRectangle);
                        }
                        else
                        {
                            new VisualStyleRenderer(VisualStyleElement.ScrollBar.ThumbButtonHorizontal.Normal).DrawBackground(e.Graphics, bounds, e.ClipRectangle);
                        }
                    }
                    else if ((drawState & UIElementDrawState.Pressed) == UIElementDrawState.Pressed)
                    {
                        new VisualStyleRenderer(VisualStyleElement.ScrollBar.ThumbButtonVertical.Pressed).DrawBackground(e.Graphics, bounds, e.ClipRectangle);
                    }
                    else if ((drawState & UIElementDrawState.Hot) == UIElementDrawState.Hot)
                    {
                        new VisualStyleRenderer(VisualStyleElement.ScrollBar.ThumbButtonVertical.Hot).DrawBackground(e.Graphics, bounds, e.ClipRectangle);
                    }
                    else
                    {
                        new VisualStyleRenderer(VisualStyleElement.ScrollBar.ThumbButtonVertical.Normal).DrawBackground(e.Graphics, bounds, e.ClipRectangle);
                    }
                    if ((bounds.Width > 10) && (bounds.Height > 10))
                    {
                        if (thumb.ScrollBar.Orientation == Orientation.Horizontal)
                        {
                            new VisualStyleRenderer(VisualStyleElement.ScrollBar.GripperHorizontal.Normal).DrawBackground(e.Graphics, bounds, e.ClipRectangle);
                        }
                        else
                        {
                            new VisualStyleRenderer(VisualStyleElement.ScrollBar.GripperVertical.Normal).DrawBackground(e.Graphics, bounds, e.ClipRectangle);
                        }
                    }
                }
            }
        }

        [Category("Appearance"), Description("Whether themes should be used when drawing the control."), DefaultValue(true)]
        public bool UseThemes
        {
            get => 
                this.#Yve;
            set
            {
                if (this.#Yve != value)
                {
                    this.#Yve = value;
                    this.OnPropertyChanged(EventArgs.Empty);
                }
            }
        }
    }
}

