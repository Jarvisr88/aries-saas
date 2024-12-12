namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Text;

    public static class BrickPagePairHelper
    {
        public static readonly int[] EmptyIndices = new int[0];
        public const char IndexSeparator = ',';

        public static Brick GetBrick(this BrickPagePair pair, IPageRepository pages) => 
            pair.GetPage(pages).GetBrickByIndices(pair.BrickIndices) as Brick;

        public static unsafe RectangleF GetBrickBounds(this Page page, int[] indices)
        {
            if (indices.Length == 0)
            {
                throw new ArgumentException();
            }
            PointF empty = PointF.Empty;
            PointF location = PointF.Empty;
            BrickBase base2 = page;
            int index = 0;
            while (true)
            {
                if (index < indices.Length)
                {
                    int num2 = indices[index];
                    if (num2 < base2.InnerBrickList.Count)
                    {
                        BrickBase base3 = (BrickBase) base2.InnerBrickList[num2];
                        location = base3.Location;
                        if (base2.RightToLeftLayout)
                        {
                            PointF* tfPtr1 = &empty;
                            tfPtr1.X += (base2.InnerBrickListOffset.X + base2.Width) - base3.Rect.Right;
                        }
                        else
                        {
                            PointF* tfPtr2 = &empty;
                            tfPtr2.X += base2.InnerBrickListOffset.X + location.X;
                        }
                        PointF* tfPtr3 = &empty;
                        tfPtr3.Y += base2.InnerBrickListOffset.Y + location.Y;
                        base2 = base3;
                        index++;
                        continue;
                    }
                }
                PointF* tfPtr4 = &empty;
                tfPtr4.X -= location.X;
                PointF* tfPtr5 = &empty;
                tfPtr5.Y -= location.Y;
                RectangleF rect = base2.Rect;
                rect.Offset(empty);
                return rect;
            }
        }

        public static BrickBase GetBrickByIndices(this Page page, int[] indices)
        {
            if (page == null)
            {
                return null;
            }
            BrickBase base2 = page;
            int index = 0;
            while (true)
            {
                if (index < indices.Length)
                {
                    int num2 = indices[index];
                    if (num2 < base2.InnerBrickList.Count)
                    {
                        base2 = (BrickBase) base2.InnerBrickList[num2];
                        index++;
                        continue;
                    }
                }
                return base2;
            }
        }

        public static int[] GetIndicesByBrick(this Page page, Brick brick)
        {
            if (page != null)
            {
                NestedBrickIterator iterator = new NestedBrickIterator(page.InnerBrickList);
                while (iterator.MoveNext())
                {
                    if (iterator.CurrentBrick.Equals(brick))
                    {
                        return iterator.GetCurrentBrickIndices();
                    }
                }
            }
            return EmptyIndices;
        }

        public static string IndicesFromArray(int[] indexArray)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < indexArray.Length; i++)
            {
                if (i > 0)
                {
                    builder.Append(',');
                }
                builder.Append(indexArray[i].ToString());
            }
            return builder.ToString();
        }

        public static int[] ParseIndices(string indices)
        {
            char[] separator = new char[] { ',' };
            string[] strArray = indices.Split(separator);
            if ((strArray.Length == 1) && string.IsNullOrEmpty(strArray[0]))
            {
                return EmptyIndices;
            }
            int[] numArray = new int[strArray.Length];
            for (int i = 0; i < strArray.Length; i++)
            {
                numArray[i] = int.Parse(strArray[i]);
            }
            return numArray;
        }
    }
}

