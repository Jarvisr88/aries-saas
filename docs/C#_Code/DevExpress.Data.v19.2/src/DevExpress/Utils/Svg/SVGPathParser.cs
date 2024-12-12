namespace DevExpress.Utils.Svg
{
    using DevExpress.Data.Svg;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    internal static class SVGPathParser
    {
        private static SvgPoint ConvertPoint(SvgPoint point, SvgPoint mirrow) => 
            new SvgPoint(point.X + ((mirrow.X - point.X) * 2.0), point.Y + ((mirrow.Y - point.Y) * 2.0));

        private static void CreatePathSegment(string commandSet, SvgPathSegmentCollection segments)
        {
            SvgPoint[] points;
            char ch = char.ToLower(commandSet[0]);
            bool isRelativeX = char.IsLower(commandSet[0]);
            if (ch <= 'c')
            {
                if (ch == 'a')
                {
                    double[] numbers = CoordinateParser.GetNumbers(commandSet);
                    for (int i = 0; i < numbers.Length; i += 7)
                    {
                        bool largeArc = !(numbers[i + 3] == 0.0);
                        bool sweep = !(numbers[i + 4] == 0.0);
                        SvgPathArcSegment segment = new SvgPathArcSegment(segments.LastPoint, numbers[i], numbers[i + 1], numbers[i + 2], largeArc, sweep, ToAbsolute(numbers[i + 5], numbers[i + 6], segments.LastPoint, isRelativeX, isRelativeX));
                        segments.Add(segment);
                    }
                }
                else if (ch == 'c')
                {
                    points = CoordinateParser.GetPoints(commandSet);
                    for (int i = 0; i < points.Length; i += 3)
                    {
                        segments.Add(new SvgPathCurveToCubicSegment(segments.LastPoint, ToAbsolute(points[i], segments.LastPoint, isRelativeX), ToAbsolute(points[i + 1], segments.LastPoint, isRelativeX), ToAbsolute(points[i + 2], segments.LastPoint, isRelativeX)));
                    }
                }
            }
            else
            {
                SvgPoint lastPoint;
                if (ch == 'h')
                {
                    foreach (double num5 in CoordinateParser.GetNumbers(commandSet))
                    {
                        lastPoint = segments.LastPoint;
                        segments.Add(new SvgPathLineToSegment(segments.LastPoint, ToAbsolute(num5, lastPoint.Y, segments.LastPoint, isRelativeX, false)));
                    }
                }
                else
                {
                    switch (ch)
                    {
                        case 'l':
                            points = CoordinateParser.GetPoints(commandSet);
                            for (int i = 0; i < points.Length; i++)
                            {
                                segments.Add(new SvgPathLineToSegment(segments.LastPoint, ToAbsolute(points[i], segments.LastPoint, isRelativeX)));
                            }
                            return;

                        case 'm':
                            points = CoordinateParser.GetPoints(commandSet);
                            for (int i = 0; i < points.Length; i++)
                            {
                                if (i == 0)
                                {
                                    segments.Add(new SvgPathMoveToSegment(ToAbsolute(points[i], segments.LastPoint, isRelativeX)));
                                }
                                else
                                {
                                    segments.Add(new SvgPathLineToSegment(segments.LastPoint, ToAbsolute(points[i], segments.LastPoint, isRelativeX)));
                                }
                            }
                            return;

                        case 'n':
                        case 'o':
                        case 'p':
                        case 'r':
                        case 'u':
                            break;

                        case 'q':
                            points = CoordinateParser.GetPoints(commandSet);
                            for (int i = 0; i < points.Length; i += 2)
                            {
                                segments.Add(new SvgPathCurveToQuadraticSegment(segments.LastPoint, ToAbsolute(points[i], segments.LastPoint, isRelativeX), ToAbsolute(points[i + 1], segments.LastPoint, isRelativeX)));
                            }
                            return;

                        case 's':
                            points = CoordinateParser.GetPoints(commandSet);
                            for (int i = 0; i < points.Length; i += 2)
                            {
                                SvgPathCurveToCubicSegment last = segments.Last as SvgPathCurveToCubicSegment;
                                SvgPoint firstAdditionalPoint = (last != null) ? ConvertPoint(last.SecondAdditionalPoint, segments.LastPoint) : segments.LastPoint;
                                segments.Add(new SvgPathCurveToCubicSegment(segments.LastPoint, firstAdditionalPoint, ToAbsolute(points[i], segments.LastPoint, isRelativeX), ToAbsolute(points[i + 1], segments.LastPoint, isRelativeX)));
                            }
                            return;

                        case 't':
                            points = CoordinateParser.GetPoints(commandSet);
                            for (int i = 0; i < points.Length; i++)
                            {
                                SvgPoint lastPoint = segments.LastPoint;
                                SvgPathCurveToQuadraticSegment last = segments.Last as SvgPathCurveToQuadraticSegment;
                                if (last != null)
                                {
                                    lastPoint = ConvertPoint(last.AdditionalPoint, segments.LastPoint);
                                }
                                segments.Add(new SvgPathCurveToQuadraticSegment(segments.LastPoint, lastPoint, ToAbsolute(points[i], segments.LastPoint, isRelativeX)));
                            }
                            return;

                        case 'v':
                            foreach (double num7 in CoordinateParser.GetNumbers(commandSet))
                            {
                                lastPoint = segments.LastPoint;
                                segments.Add(new SvgPathLineToSegment(segments.LastPoint, ToAbsolute(lastPoint.X, num7, segments.LastPoint, false, isRelativeX)));
                            }
                            return;

                        default:
                            if (ch != 'z')
                            {
                                return;
                            }
                            segments.Add(new SvgPathCloseSegment());
                            break;
                    }
                }
            }
        }

        [IteratorStateMachine(typeof(<GetCommands>d__5))]
        private static IEnumerable<string> GetCommands(string path)
        {
            int num2;
            int startIndex = 0;
            int <i>5__2 = 0;
            goto TR_000E;
        TR_0004:
            num2 = <i>5__2;
            <i>5__2 = num2 + 1;
        TR_000E:
            while (true)
            {
                string str;
                if (<i>5__2 >= path.Length)
                {
                }
                if (!char.IsLetter(path[<i>5__2]) || (path[<i>5__2] == 'e'))
                {
                    if (path.Length == (<i>5__2 + 1))
                    {
                        str = path.Substring(startIndex, (<i>5__2 - startIndex) + 1).Trim();
                        if (!string.IsNullOrEmpty(str))
                        {
                            yield return str;
                        }
                    }
                    goto TR_0004;
                }
                else
                {
                    str = path.Substring(startIndex, <i>5__2 - startIndex).Trim();
                    startIndex = <i>5__2;
                    if (!string.IsNullOrEmpty(str))
                    {
                        yield return str;
                        break;
                    }
                }
                break;
            }
            if (path.Length == (<i>5__2 + 1))
            {
                yield return path[<i>5__2].ToString();
            }
            goto TR_0004;
        }

        public static void Parse(string data, SvgPathSegmentCollection segments)
        {
            if (!string.IsNullOrEmpty(data))
            {
                try
                {
                    foreach (string str in GetCommands(data.TrimEnd(null)))
                    {
                        CreatePathSegment(str, segments);
                    }
                }
                catch
                {
                }
            }
        }

        private static SvgPoint ToAbsolute(SvgPoint point, SvgPoint current, bool isRelative) => 
            ToAbsolute(point.X, point.Y, current, isRelative, isRelative);

        private static SvgPoint ToAbsolute(double x, double y, SvgPoint current, bool isRelativeX, bool isRelativeY)
        {
            SvgPoint point = new SvgPoint(x, y);
            if (isRelativeX)
            {
                point.OffsetX(current.X);
            }
            if (isRelativeY)
            {
                point.OffsetY(current.Y);
            }
            return point;
        }

    }
}

