namespace ActiproSoftware.WinUICore
{
    using #Xqe;
    using ActiproSoftware.Drawing;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows.Forms;

    [DefaultEvent("SelectedColorChanged"), DefaultProperty("SelectedColor")]
    public class ColorPalettePicker : UIControl
    {
        private #Wqe #0ue;

        [Category("Action"), Description("Occurs after the value of the SelectedColor property changes.")]
        public event EventHandler SelectedColorChanged;

        public ColorPalettePicker()
        {
            base.SetStyle(ControlStyles.Selectable, true);
            Color[] standardCustomColors = UIColor.GetStandardCustomColors();
            this.InitializeColors(standardCustomColors);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            if (this.Children.Count != 0)
            {
                Size desiredSize = ((#Wqe) this.Children[0]).DesiredSize;
                int num = base.ClientRectangle.Width / desiredSize.Width;
                int num2 = (int) Math.Ceiling((double) (((float) this.Children.Count) / ((float) num)));
                int num3 = base.ClientRectangle.Left + ((base.ClientRectangle.Width - (desiredSize.Width * num)) / 2);
                int x = num3;
                int y = base.ClientRectangle.Top + ((base.ClientRectangle.Height - (desiredSize.Height * num2)) / 2);
                for (int i = 0; i < this.Children.Count; i++)
                {
                    if ((i > 0) && ((i % num) == 0))
                    {
                        x = num3;
                        y += desiredSize.Height;
                    }
                    ((#Wqe) this.Children[i]).Arrange(new Rectangle(x, y, desiredSize.Width, desiredSize.Height));
                    x += desiredSize.Width;
                }
            }
            return finalSize;
        }

        protected override IList CreateChildren() => 
            new UIElementCollection(this);

        public void InitializeColors(Color[] colors)
        {
            this.Children.Clear();
            foreach (Color color in colors)
            {
                this.Children.Add(new #Wqe(color));
            }
        }

        protected override Size MeasureOverride(Graphics g, Size availableSize)
        {
            using (IEnumerator enumerator = this.Children.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    ((#Wqe) enumerator.Current).Measure(g, availableSize);
                }
            }
            return availableSize;
        }

        protected virtual void OnSelectedColorChanged(EventArgs e)
        {
            if (this.#1ue != null)
            {
                this.#1ue(this, e);
            }
        }

        protected override bool ProcessCmdKey(ref Message m, Keys keyData)
        {
            if (this.Children.Count != 0)
            {
                Size desiredSize = ((#Wqe) this.Children[0]).DesiredSize;
                int num = base.ClientRectangle.Width / desiredSize.Width;
                int num2 = (int) Math.Ceiling((double) (((float) this.Children.Count) / ((float) num)));
                int num1 = (base.ClientRectangle.Width - (desiredSize.Width * num)) / 2;
                int top = base.ClientRectangle.Top;
                int index = this.Children.IndexOf(this.#0ue);
                bool flag = (index == -1) || ((index % num) == 0);
                bool flag2 = (index != -1) && ((index % num) == (num - 1));
                bool flag3 = (index == -1) || ((index / num) == 0);
                bool flag4 = (index != -1) && ((index / num) == (num2 - 1));
                switch (keyData)
                {
                    case Keys.Left:
                        if (!flag)
                        {
                            this.SelectedColorElement = (#Wqe) this.Children[index - 1];
                        }
                        return true;

                    case Keys.Up:
                        if (!flag3)
                        {
                            this.SelectedColorElement = (#Wqe) this.Children[index - num];
                        }
                        return true;

                    case Keys.Right:
                        if (!flag2)
                        {
                            this.SelectedColorElement = (#Wqe) this.Children[index + 1];
                        }
                        return true;

                    case Keys.Down:
                        if (index == -1)
                        {
                            this.SelectedColorElement = (#Wqe) this.Children[0];
                        }
                        else if (!flag4)
                        {
                            this.SelectedColorElement = (#Wqe) this.Children[index + num];
                        }
                        return true;
                }
            }
            return base.ProcessCmdKey(ref m, keyData);
        }

        internal #Wqe SelectedColorElement
        {
            get => 
                this.#0ue;
            set
            {
                if (!ReferenceEquals(this.#0ue, value))
                {
                    if (this.#0ue != null)
                    {
                        this.#0ue.Invalidate();
                    }
                    this.#0ue = value;
                    if (this.#0ue != null)
                    {
                        this.#0ue.Invalidate();
                    }
                    this.OnSelectedColorChanged(EventArgs.Empty);
                }
            }
        }

        protected override Size DefaultSize =>
            new Size(0xc0, 0x90);

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color SelectedColor
        {
            get => 
                (this.#0ue == null) ? Color.Empty : this.#0ue.Color;
            set
            {
                #Wqe wqe;
                if (2 == 0)
                {
                }
                else
                {
                    wqe = null;
                }
                foreach (#Wqe wqe2 in this.Children)
                {
                    if (wqe2.Color == value)
                    {
                        wqe = wqe2;
                        break;
                    }
                }
                this.SelectedColorElement = wqe;
            }
        }
    }
}

