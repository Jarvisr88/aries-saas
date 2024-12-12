namespace DevExpress.Office.Drawing
{
    using DevExpress.Data.Extensions;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Security;
    using System.Threading;
    using System.Threading.Tasks;

    public class BitmapOutlineFinder
    {
        private static Dictionary<Direction, Point> pointOffsets = CreatePointOffsets();
        private byte[] visited;
        private int width;
        private int height;
        private OutlineData outlineData;
        private readonly Point zeroPoint = new Point(-1, -1);
        private const int directionsCount = 8;

        private static Dictionary<Direction, Point> CreatePointOffsets() => 
            new Dictionary<Direction, Point> { 
                { 
                    Direction.Top,
                    new Point(0, -1)
                },
                { 
                    Direction.TopRight,
                    new Point(1, -1)
                },
                { 
                    Direction.Right,
                    new Point(1, 0)
                },
                { 
                    Direction.BottomRight,
                    new Point(1, 1)
                },
                { 
                    Direction.Bottom,
                    new Point(0, 1)
                },
                { 
                    Direction.BottomLeft,
                    new Point(-1, 1)
                },
                { 
                    Direction.Left,
                    new Point(-1, 0)
                },
                { 
                    Direction.TopLeft,
                    new Point(-1, -1)
                }
            };

        private unsafe List<Point[]> FindRegions(int size)
        {
            List<Point[]> list = new List<Point[]>();
            while (this.outlineData.Count > 0)
            {
                bool flag = true;
                this.visited = new byte[size];
                List<Point> list2 = new List<Point>();
                Point firstPoint = this.outlineData.GetFirstPoint();
                list2.Add(firstPoint);
                Point current = firstPoint;
                int position = (current.Y * this.width) + current.X;
                while (true)
                {
                    Point point3 = this.GetNextOrBackPoint(current, firstPoint, position);
                    if (point3 == this.zeroPoint)
                    {
                        flag = false;
                    }
                    else
                    {
                        current = point3;
                        position = (current.Y * this.width) + current.X;
                        byte* numPtr1 = &(this.visited[position]);
                        numPtr1[0] = (byte) (numPtr1[0] + 1);
                        list2.Add(current);
                    }
                    if (!(!current.Equals(firstPoint) & flag))
                    {
                        foreach (Point point4 in list2)
                        {
                            this.outlineData[point4.X, point4.Y] = false;
                        }
                        if (flag)
                        {
                            list.Add(list2.ToArray());
                        }
                        break;
                    }
                }
            }
            return list;
        }

        private Point GetNeighbourPoint(Point point, int direction)
        {
            Point point2 = pointOffsets[(Direction) direction];
            return new Point(point.X + point2.X, point.Y + point2.Y);
        }

        private Point GetNextOrBackPoint(Point current, Point firstPoint, int position)
        {
            Point nextPointBackwards;
            if (this.visited[position] >= 2)
            {
                nextPointBackwards = this.GetNextPointBackwards(current);
            }
            else
            {
                nextPointBackwards = this.GetNextPoint(current, firstPoint);
                if (nextPointBackwards == this.zeroPoint)
                {
                    nextPointBackwards = this.GetNextPointBackwards(current);
                }
            }
            return nextPointBackwards;
        }

        private Point GetNextPoint(Point currentPoint, Point firstPoint)
        {
            Point zeroPoint = this.zeroPoint;
            int num = 0x7fffffff;
            for (int i = 0; i < 8; i++)
            {
                Point neighbourPoint = this.GetNeighbourPoint(currentPoint, i);
                if (this.IsNotVisitedOutlinePoint(neighbourPoint))
                {
                    if (neighbourPoint.Equals(firstPoint))
                    {
                        if (zeroPoint.Equals(this.zeroPoint))
                        {
                            zeroPoint = firstPoint;
                        }
                    }
                    else
                    {
                        int notVisitedNeigboursCount = this.GetNotVisitedNeigboursCount(neighbourPoint);
                        if (notVisitedNeigboursCount < num)
                        {
                            num = notVisitedNeigboursCount;
                            zeroPoint = neighbourPoint;
                        }
                    }
                }
            }
            return zeroPoint;
        }

        private Point GetNextPointBackwards(Point position)
        {
            Point zeroPoint = this.zeroPoint;
            int num = 0;
            for (int i = 7; i >= 0; i--)
            {
                Point neighbourPoint = this.GetNeighbourPoint(position, i);
                int x = neighbourPoint.X;
                int y = neighbourPoint.Y;
                if (this.IsOutlinePoint(x, y))
                {
                    int num5 = this.visited[(y * this.width) + x];
                    if (num5 == 0)
                    {
                        return neighbourPoint;
                    }
                    if ((zeroPoint == this.zeroPoint) || (num > num5))
                    {
                        zeroPoint = neighbourPoint;
                        num = num5;
                    }
                }
            }
            return zeroPoint;
        }

        private int GetNotVisitedNeigboursCount(Point point)
        {
            int num = 0;
            for (int i = 0; i < 8; i++)
            {
                Point neighbourPoint = this.GetNeighbourPoint(point, i);
                if (this.IsNotVisitedOutlinePoint(neighbourPoint))
                {
                    num++;
                }
            }
            return num;
        }

        private OutlineData GetOutlineData(byte[] bytes, int stride)
        {
            OutlineData result = new OutlineData(this.width, this.height);
            Parallel.For(0, this.height, delegate (int y) {
                bool flag = false;
                bool flag2 = false;
                for (int i = 0; i < this.width; i++)
                {
                    flag2 = bytes[((y * stride) + (i * 4)) + 3] == 0;
                    if ((((i == 0) || (i == (this.width - 1))) | flag) && !flag2)
                    {
                        result[i, y] = true;
                    }
                    if (!flag && (flag2 && (i != 0)))
                    {
                        result[i - 1, y] = true;
                    }
                    flag = flag2;
                }
            });
            Parallel.For(0, this.width, delegate (int x) {
                bool flag = false;
                bool flag2 = false;
                for (int i = 0; i < this.height; i++)
                {
                    flag2 = bytes[((i * stride) + (x * 4)) + 3] == 0;
                    if ((((i == 0) || (i == (this.height - 1))) | flag) && !flag2)
                    {
                        result[x, i] = true;
                    }
                    if (!flag && (flag2 && (i != 0)))
                    {
                        result[x, i - 1] = true;
                    }
                    flag = flag2;
                }
            });
            return result;
        }

        [SecuritySafeCritical]
        private void InitializeOutlineData(Bitmap bitmap, int size)
        {
            BitmapData bitmapdata = bitmap.LockBits(new Rectangle(0, 0, this.width, this.height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            byte[] destination = new byte[size * 4];
            Marshal.Copy(bitmapdata.Scan0, destination, 0, destination.Length);
            this.outlineData = this.GetOutlineData(destination, bitmapdata.Stride);
            bitmap.UnlockBits(bitmapdata);
        }

        private bool IsNotVisitedOutlinePoint(Point point)
        {
            int x = point.X;
            int y = point.Y;
            return (this.IsOutlinePoint(x, y) && (this.visited[(y * this.width) + x] == 0));
        }

        private bool IsOutlinePoint(int x, int y) => 
            (((x >= 0) && ((y >= 0) && (x < this.width))) && (y < this.height)) && this.outlineData[x, y];

        public List<Point[]> Process(Bitmap bitmap)
        {
            this.width = bitmap.Width;
            this.height = bitmap.Height;
            int size = this.width * this.height;
            this.InitializeOutlineData(bitmap, size);
            return this.FindRegions(size);
        }

        private enum Direction
        {
            Top,
            TopRight,
            Right,
            BottomRight,
            Bottom,
            BottomLeft,
            Left,
            TopLeft
        }

        private class OutlineData
        {
            private bool[] points;
            private readonly int width;
            private int count;

            public OutlineData(int width, int height)
            {
                this.width = width;
                this.points = new bool[width * height];
            }

            public Point GetFirstPoint()
            {
                Predicate<bool> predicate = <>c.<>9__9_0;
                if (<>c.<>9__9_0 == null)
                {
                    Predicate<bool> local1 = <>c.<>9__9_0;
                    predicate = <>c.<>9__9_0 = x => x;
                }
                int num = this.points.FindIndex<bool>(predicate);
                return ((num >= 0) ? new Point(num % this.width, num / this.width) : new Point(-1, -1));
            }

            public int Count =>
                this.count;

            public bool this[int x, int y]
            {
                get => 
                    this.points[(y * this.width) + x];
                set
                {
                    int index = (y * this.width) + x;
                    if (this.points[index] != value)
                    {
                        this.points[index] = value;
                        if (value)
                        {
                            Interlocked.Increment(ref this.count);
                        }
                        else
                        {
                            this.count--;
                        }
                    }
                }
            }

            [Serializable, CompilerGenerated]
            private sealed class <>c
            {
                public static readonly BitmapOutlineFinder.OutlineData.<>c <>9 = new BitmapOutlineFinder.OutlineData.<>c();
                public static Predicate<bool> <>9__9_0;

                internal bool <GetFirstPoint>b__9_0(bool x) => 
                    x;
            }
        }
    }
}

