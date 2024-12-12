namespace ActiproSoftware.WinUICore
{
    using ActiproSoftware.Drawing;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class ColorListBox : ListBox
    {
        private const int #Yue = 20;
        private ColorListBoxColorSet #Zue = ColorListBoxColorSet.WebColors;

        public ColorListBox()
        {
            this.DrawMode = DrawMode.OwnerDrawVariable;
            this.ItemHeight = base.FontHeight + 2;
            this.ColorSet = ColorListBoxColorSet.WebColors;
        }

        public void InitializeColors(Color[] colors)
        {
            this.#Zue = ColorListBoxColorSet.Custom;
            this.Items.Clear();
            foreach (Color color in colors)
            {
                base.Items.Add(color);
            }
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            if (e.Index == -1)
            {
                g.FillRectangle(SystemBrushes.Window, e.Bounds);
            }
            else
            {
                Color color = (Color) base.Items[e.Index];
                if ((e.State & DrawItemState.Selected) != DrawItemState.Selected)
                {
                    g.FillRectangle(SystemBrushes.Window, e.Bounds);
                }
                else
                {
                    SolidBrush brush2 = new SolidBrush(WindowsColorScheme.WindowsDefault.BarButtonHotBack);
                    g.FillRectangle(brush2, e.Bounds);
                    brush2.Dispose();
                    Pen pen = new Pen(WindowsColorScheme.WindowsDefault.BarButtonHotBorder);
                    g.DrawRectangle(pen, e.Bounds.Left, e.Bounds.Top, e.Bounds.Width - 1, e.Bounds.Height - 1);
                    pen.Dispose();
                }
                SolidBrush brush = new SolidBrush(color);
                g.FillRectangle(brush, e.Bounds.Left + 2, e.Bounds.Top + 2, 20, e.Bounds.Height - 4);
                brush.Dispose();
                g.DrawRectangle(SystemPens.ControlText, e.Bounds.Left + 2, e.Bounds.Top + 2, 0x13, e.Bounds.Height - 5);
                StringFormat format = DrawingHelper.GetStringFormat(StringAlignment.Near, StringAlignment.Center, StringTrimming.None, false, false);
                DrawingHelper.DrawString(g, color.Name, this.Font, SystemColors.ControlText, new Rectangle((e.Bounds.Left + 20) + 5, e.Bounds.Top, e.Bounds.Width, e.Bounds.Height), format);
                format.Dispose();
            }
        }

        protected override void OnMeasureItem(MeasureItemEventArgs e)
        {
            e.ItemHeight = this.ItemHeight;
            e.ItemWidth = base.ClientSize.Width;
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg != 0x317)
            {
                base.WndProc(ref m);
            }
            else
            {
                m.Result = new IntPtr(1);
                base.Invalidate();
            }
        }

        [Category("Appearance"), Description("The ColorListBoxColorSet that indicates the color set that is loaded."), DefaultValue(1), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ColorListBoxColorSet ColorSet
        {
            get => 
                this.#Zue;
            set
            {
                if (value == ColorListBoxColorSet.SystemColors)
                {
                    this.InitializeColors(UIColor.GetSystemColors());
                }
                else if (value != ColorListBoxColorSet.WebColors)
                {
                    base.Items.Clear();
                }
                else
                {
                    this.InitializeColors(UIColor.GetWebColors());
                }
                this.#Zue = value;
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color SelectedColor
        {
            get => 
                (Color) this.SelectedItem;
            set
            {
                if (this.Items.Contains(value))
                {
                    base.SelectedItem = value;
                }
            }
        }
    }
}

