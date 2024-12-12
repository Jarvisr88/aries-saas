namespace #Qqe
{
    using ActiproSoftware.Drawing;
    using ActiproSoftware.MarkupLabel;
    using ActiproSoftware.WinUICore;
    using System;
    using System.Collections;
    using System.Drawing;

    internal class #Pqe
    {
        private int #Lte = 2;
        private int #Mte = 2;

        private void #nxe(Rectangle #Bo, IList #vo)
        {
            #Bo.Inflate(-this.#Lte, -this.#Mte);
            int left = #Bo.Left;
            int top = #Bo.Top;
            int num3 = 0;
            int descent = 0;
            int num5 = 0;
            int num6 = 0;
            for (int i = 0; i < #vo.Count; i++)
            {
                MarkupLabelUIElement element = (MarkupLabelUIElement) #vo[i];
                if ((element != null) && (element.Visibility != Visibility.Collapsed))
                {
                    if (!element.HardLineBreakBefore && ((left == #Bo.Left) || ((left + element.DesiredSize.Width) <= #Bo.Right)))
                    {
                        descent = element.Descent;
                        num6 = Math.Max(num6, descent);
                        left += element.DesiredSize.Width;
                        num5 = Math.Max(num5, element.DesiredSize.Height - descent);
                    }
                    else
                    {
                        num5 += num6;
                        this.#oxe(new Rectangle(#Bo.Left, top, left - #Bo.Left, num5), #vo, num3, i - 1, num6);
                        descent = element.Descent;
                        num6 = Math.Max(0, descent);
                        left = !(element is MarkupLabelWhitespaceUIElement) ? (#Bo.Left + element.DesiredSize.Width) : #Bo.Left;
                        top += num5;
                        num5 = element.DesiredSize.Height - descent;
                        num3 = i;
                    }
                }
            }
            num5 += num6;
            this.#oxe(new Rectangle(#Bo.Left, top, left - #Bo.Left, num5), #vo, num3, #vo.Count - 1, num6);
        }

        private void #oxe(Rectangle #Bo, IList #vo, int #9Zf, int #a0f, int #b0f)
        {
            int left = #Bo.Left;
            for (int i = #9Zf; i <= #a0f; i++)
            {
                MarkupLabelUIElement element = (MarkupLabelUIElement) #vo[i];
                if ((element != null) && (element.Visibility != Visibility.Collapsed))
                {
                    int descent = element.Descent;
                    element.Arrange(new Rectangle(left, (#Bo.Bottom - #b0f) - (element.DesiredSize.Height - descent), element.DesiredSize.Width, element.DesiredSize.Height));
                    if (!(element is MarkupLabelWhitespaceUIElement) || (i != #9Zf))
                    {
                        left += element.DesiredSize.Width;
                    }
                }
            }
        }

        internal void #pmc(Rectangle #Bo, IList #vo)
        {
            this.#nxe(#Bo, #vo);
        }

        internal unsafe Size GetPreferredSize(Rectangle #Bo, IList #vo)
        {
            #Bo.Inflate(-this.#Lte, -this.#Mte);
            int left = #Bo.Left;
            int top = #Bo.Top;
            int descent = 0;
            int num4 = 0;
            ActiproSoftware.Drawing.Range range = new ActiproSoftware.Drawing.Range(0x7fffffff, 0);
            Size size = new Size(0, 0);
            for (int i = 0; i < #vo.Count; i++)
            {
                MarkupLabelUIElement element = (MarkupLabelUIElement) #vo[i];
                if ((element != null) && (element.Visibility != Visibility.Collapsed))
                {
                    if (!element.HardLineBreakBefore && ((left == #Bo.Left) || ((left + element.DesiredSize.Width) <= #Bo.Right)))
                    {
                        descent = element.Descent;
                        if (descent < range.Min)
                        {
                            range.Min = descent;
                        }
                        if (descent > range.Max)
                        {
                            range.Max = descent;
                        }
                        left += element.DesiredSize.Width;
                        num4 = Math.Max(num4, element.DesiredSize.Height - descent);
                    }
                    else
                    {
                        if (range.Min == 0x7fffffff)
                        {
                            range.Min = 0;
                        }
                        num4 += range.Max;
                        size.Width = Math.Max(size.Width, left - #Bo.Left);
                        Size* sizePtr1 = &size;
                        sizePtr1.Height += num4;
                        range = new ActiproSoftware.Drawing.Range(0x7fffffff, 0);
                        descent = element.Descent;
                        if (descent < range.Min)
                        {
                            range.Min = descent;
                        }
                        if (descent > range.Max)
                        {
                            range.Max = descent;
                        }
                        left = #Bo.Left + element.DesiredSize.Width;
                        top += num4;
                        num4 = element.DesiredSize.Height - descent;
                    }
                }
            }
            size.Width = Math.Max(size.Width, left - #Bo.Left);
            Size* sizePtr2 = &size;
            sizePtr2.Height += num4 + range.Max;
            Size* sizePtr3 = &size;
            sizePtr3.Width += 2 * this.#Lte;
            Size* sizePtr4 = &size;
            sizePtr4.Height += 2 * this.#Mte;
            return size;
        }
    }
}

