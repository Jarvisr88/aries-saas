namespace DevExpress.XtraPrinting.Export.Web
{
    using DevExpress.XtraPrinting.HtmlExport.Controls;
    using System;
    using System.Drawing;

    public class HtmlHelper
    {
        public const string WatermarkMouseDownScript = "if(!event || !event.target) { return; } event.target.style.display = 'none'; var lowerDiv = document.elementFromPoint(event.clientX, event.clientY); event.target.style.display = 'block'; if(!lowerDiv) { return; } var newEvent = document.createEvent('MouseEvent'); newEvent.initMouseEvent('mousedown', true, true, window, 0, event.screenX, event.screenY, event.clientX, event.clientY, false, false, false, false, 0, null); lowerDiv.dispatchEvent(newEvent);";

        public static string GetHtmlUrl(string url) => 
            "#" + url;

        public static ClipControl SetClip(DXHtmlControl parent, Point offset, Size outerControlSize)
        {
            ClipControl child = (parent.Controls.Count == 1) ? (parent.Controls[0] as ClipControl) : null;
            if (child != null)
            {
                child.SetClipSize(outerControlSize);
                child.AddOffset(offset);
            }
            else
            {
                child = new ClipControl(parent, offset, outerControlSize);
                int num = parent.Controls.Count - 1;
                while (true)
                {
                    if (num < 0)
                    {
                        parent.Controls.Add(child);
                        break;
                    }
                    child.InnerContainer.Controls.AddAt(0, parent.Controls[num]);
                    num--;
                }
            }
            return child;
        }

        public static ClipControl SetClip(DXHtmlControl parent, Point offset, Size clipSize, Size originalSize)
        {
            ClipControl control = SetClip(parent, offset, clipSize);
            control.SetOriginalSize(originalSize);
            return control;
        }

        public static void SetStyleHeight(DXCssStyleCollection style, int height)
        {
            style.Add("height", HtmlConvert.ToHtml(height));
        }

        public static void SetStyleSize(DXCssStyleCollection style, Size size)
        {
            SetStyleWidth(style, size.Width);
            SetStyleHeight(style, size.Height);
        }

        public static void SetStyleWidth(DXCssStyleCollection style, int width)
        {
            style.Add("width", HtmlConvert.ToHtml(width));
        }
    }
}

