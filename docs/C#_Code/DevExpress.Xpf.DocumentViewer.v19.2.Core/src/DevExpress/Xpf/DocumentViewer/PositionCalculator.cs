namespace DevExpress.Xpf.DocumentViewer
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class PositionCalculator
    {
        private readonly Func<IEnumerable<PageWrapper>> getPagesHandler;

        public PositionCalculator(Func<IEnumerable<PageWrapper>> getPagesHandler)
        {
            this.getPagesHandler = getPagesHandler;
        }

        public double GetMaxPageWidth()
        {
            double num = 0.0;
            IEnumerable<PageWrapper> enumerable = this.getPagesHandler();
            if (enumerable == null)
            {
                return 0.0;
            }
            foreach (PageWrapper wrapper in enumerable)
            {
                Size renderSize = wrapper.RenderSize;
                num = Math.Max(num, renderSize.Width);
            }
            return num;
        }

        public double GetPageHorizontalOffset(double relativeOffset) => 
            this.GetMaxPageWidth() * relativeOffset;

        public int GetPageIndex(double verticalOffset)
        {
            IEnumerable<PageWrapper> source = this.getPagesHandler();
            if (source != null)
            {
                double num = 0.0;
                for (int i = 0; i < source.Count<PageWrapper>(); i++)
                {
                    Size renderSize = source.ElementAt<PageWrapper>(i).RenderSize;
                    num += renderSize.Height;
                    if ((num - verticalOffset).GreaterThan(0.0))
                    {
                        return source.ElementAt<PageWrapper>(i).Pages.First<IPage>().PageIndex;
                    }
                }
            }
            return 0;
        }

        public int GetPageIndex(double verticalOffset, double horizontalOffset, Func<double, double> getPageHorizontalOffsetHandler)
        {
            int num4;
            IEnumerable<PageWrapper> source = this.getPagesHandler();
            if (source == null)
            {
                return 0;
            }
            int pageIndex = this.GetPageIndex(verticalOffset);
            int pageWrapperIndex = this.GetPageWrapperIndex(pageIndex);
            PageWrapper wrapper = source.ElementAt<PageWrapper>(pageWrapperIndex);
            double pageWrapperVerticalOffset = this.GetPageWrapperVerticalOffset(pageWrapperIndex);
            using (IEnumerator<IPage> enumerator = wrapper.Pages.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        IPage current = enumerator.Current;
                        Rect pageRect = wrapper.GetPageRect(current);
                        Size renderSize = wrapper.RenderSize;
                        if (!pageRect.IsInside(new Point(horizontalOffset - getPageHorizontalOffsetHandler(renderSize.Width), verticalOffset - pageWrapperVerticalOffset)))
                        {
                            continue;
                        }
                        num4 = current.PageIndex;
                    }
                    else
                    {
                        return -1;
                    }
                    break;
                }
            }
            return num4;
        }

        public int GetPageIndexFromWrapper(int wrapperIndex)
        {
            IEnumerable<PageWrapper> source = this.getPagesHandler();
            return ((source != null) ? (((wrapperIndex < 0) || (wrapperIndex >= source.Count<PageWrapper>())) ? -1 : source.ElementAt<PageWrapper>(wrapperIndex).Pages.First<IPage>().PageIndex) : 0);
        }

        public Size GetPageRenderSize(int pageIndex)
        {
            int pageWrapperIndex = this.GetPageWrapperIndex(pageIndex);
            return this.GetWrapperRenderSize(pageWrapperIndex);
        }

        public double GetPageVerticalOffset(int pageIndex, double relativeOffset)
        {
            double num = 0.0;
            int pageWrapperIndex = this.GetPageWrapperIndex(pageIndex);
            for (int i = 0; i < pageWrapperIndex; i++)
            {
                Size wrapperRenderSize = this.GetWrapperRenderSize(i);
                num += wrapperRenderSize.Height;
            }
            Size pageRenderSize = this.GetPageRenderSize(pageIndex);
            return (!pageRenderSize.IsEmpty ? (num + (pageRenderSize.Height * relativeOffset)) : 0.0);
        }

        public int GetPageWrapperIndex(int pageIndex)
        {
            IEnumerable<PageWrapper> source = this.getPagesHandler();
            if (source != null)
            {
                for (int i = 0; i < source.Count<PageWrapper>(); i++)
                {
                    Func<IPage, bool> <>9__0;
                    Func<IPage, bool> predicate = <>9__0;
                    if (<>9__0 == null)
                    {
                        Func<IPage, bool> local1 = <>9__0;
                        predicate = <>9__0 = page => page.PageIndex == pageIndex;
                    }
                    if (source.ElementAt<PageWrapper>(i).Pages.Any<IPage>(predicate))
                    {
                        return i;
                    }
                }
            }
            return 0;
        }

        public double GetPageWrapperOffset(int pageIndex)
        {
            int pageWrapperIndex = this.GetPageWrapperIndex(pageIndex);
            IEnumerable<PageWrapper> source = this.getPagesHandler();
            if (source == null)
            {
                return 0.0;
            }
            PageWrapper wrapper = source.ElementAt<PageWrapper>(pageWrapperIndex);
            return wrapper.GetPageRect(wrapper.Pages.Single<IPage>(x => x.PageIndex == pageIndex)).Left;
        }

        public double GetPageWrapperVerticalOffset(int pageWrapperIndex)
        {
            IEnumerable<PageWrapper> source = this.getPagesHandler();
            if (source == null)
            {
                return 0.0;
            }
            double num = 0.0;
            for (int i = 0; i < pageWrapperIndex; i++)
            {
                Size renderSize = source.ElementAt<PageWrapper>(i).RenderSize;
                num += renderSize.Height;
            }
            return num;
        }

        public double GetRelativeOffsetX(double horizontalOffset, double extentWidth) => 
            !extentWidth.AreClose(0.0) ? (horizontalOffset / extentWidth) : 0.0;

        public double GetRelativeOffsetY(double verticalOffset)
        {
            int pageIndex = this.GetPageIndex(verticalOffset);
            double pageVerticalOffset = this.GetPageVerticalOffset(pageIndex, 0.0);
            double num3 = verticalOffset - pageVerticalOffset;
            Size pageRenderSize = this.GetPageRenderSize(pageIndex);
            return (!pageRenderSize.IsEmpty ? (num3 / pageRenderSize.Height) : 0.0);
        }

        public Size GetWrapperRenderSize(int wrapperIndex)
        {
            IEnumerable<PageWrapper> source = this.getPagesHandler();
            if (source == null)
            {
                return Size.Empty;
            }
            Func<PageWrapper, Size> evaluator = <>c.<>9__6_0;
            if (<>c.<>9__6_0 == null)
            {
                Func<PageWrapper, Size> local1 = <>c.<>9__6_0;
                evaluator = <>c.<>9__6_0 = x => x.RenderSize;
            }
            return source.ElementAtOrDefault<PageWrapper>(wrapperIndex).Return<PageWrapper, Size>(evaluator, (<>c.<>9__6_1 ??= () => Size.Empty));
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PositionCalculator.<>c <>9 = new PositionCalculator.<>c();
            public static Func<PageWrapper, Size> <>9__6_0;
            public static Func<Size> <>9__6_1;

            internal Size <GetWrapperRenderSize>b__6_0(PageWrapper x) => 
                x.RenderSize;

            internal Size <GetWrapperRenderSize>b__6_1() => 
                Size.Empty;
        }
    }
}

