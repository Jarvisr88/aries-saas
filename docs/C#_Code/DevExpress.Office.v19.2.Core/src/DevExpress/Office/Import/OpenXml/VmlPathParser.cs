namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office.Drawing;
    using DevExpress.Office.Utils;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    internal class VmlPathParser
    {
        public const string DefaultGuideName = "G";
        private static readonly string[] Commands;
        private readonly Dictionary<string, Action<string>> parsers;
        private readonly IShapeGuideCalculator calculator;
        private readonly List<MsoPathInfo> msoPathInfos;
        private readonly List<Point> vertices;
        private int lastX;
        private int lastY;

        static VmlPathParser()
        {
            string[] textArray1 = new string[0x13];
            textArray1[0] = "m";
            textArray1[1] = "l";
            textArray1[2] = "c";
            textArray1[3] = "x";
            textArray1[4] = "e";
            textArray1[5] = "t";
            textArray1[6] = "r";
            textArray1[7] = "v";
            textArray1[8] = "nf";
            textArray1[9] = "ns";
            textArray1[10] = "ae";
            textArray1[11] = "al";
            textArray1[12] = "at";
            textArray1[13] = "ar";
            textArray1[14] = "wa";
            textArray1[15] = "wr";
            textArray1[0x10] = "qx";
            textArray1[0x11] = "qy";
            textArray1[0x12] = "qb";
            Commands = textArray1;
        }

        public VmlPathParser(IShapeGuideCalculator calculator)
        {
            this.calculator = calculator;
            this.parsers = this.CreateParserTable();
            this.msoPathInfos = new List<MsoPathInfo>();
            this.vertices = new List<Point>();
        }

        private void AddMsoPathInfo(MsoPathEscape escape, int segments)
        {
            if ((this.msoPathInfos.Count == 0) || (this.msoPathInfos[this.msoPathInfos.Count - 1].PathEscape != escape))
            {
                this.msoPathInfos.Add(new MsoPathInfo(escape, segments));
            }
            else
            {
                MsoPathInfo local1 = this.msoPathInfos[this.msoPathInfos.Count - 1];
                local1.Segments += segments;
            }
        }

        private void AddMsoPathInfo(MsoPathType type, int segments)
        {
            if ((this.msoPathInfos.Count == 0) || (this.msoPathInfos[this.msoPathInfos.Count - 1].PathType != type))
            {
                this.msoPathInfos.Add(new MsoPathInfo(type, segments));
            }
            else
            {
                MsoPathInfo local1 = this.msoPathInfos[this.msoPathInfos.Count - 1];
                local1.Segments += segments;
            }
        }

        private bool CheckCommand(string path, string command, int startIndex)
        {
            for (int i = 0; i < command.Length; i++)
            {
                if (path[startIndex + i] != command[i])
                {
                    return false;
                }
            }
            return true;
        }

        private bool CheckLength(int pathLength, int startIndex, int commandLength) => 
            (startIndex + commandLength) <= pathLength;

        private Dictionary<string, Action<string>> CreateParserTable() => 
            new Dictionary<string, Action<string>> { 
                { 
                    "m",
                    new Action<string>(this.ParseMoveTo)
                },
                { 
                    "l",
                    new Action<string>(this.ParseLineTo)
                },
                { 
                    "c",
                    new Action<string>(this.ParseCurveTo)
                },
                { 
                    "x",
                    new Action<string>(this.ParseClose)
                },
                { 
                    "e",
                    new Action<string>(this.ParseEnd)
                },
                { 
                    "t",
                    new Action<string>(this.ParseRMoveTo)
                },
                { 
                    "r",
                    new Action<string>(this.ParseRLineTo)
                },
                { 
                    "v",
                    new Action<string>(this.ParseRCurveTo)
                },
                { 
                    "nf",
                    new Action<string>(this.ParseNoFill)
                },
                { 
                    "ns",
                    new Action<string>(this.ParseNoStroke)
                },
                { 
                    "ae",
                    new Action<string>(this.ParseAngleEllipseTo)
                },
                { 
                    "al",
                    new Action<string>(this.ParseAngleEllipse)
                },
                { 
                    "at",
                    new Action<string>(this.ParseArcTo)
                },
                { 
                    "ar",
                    new Action<string>(this.ParseArc)
                },
                { 
                    "wa",
                    new Action<string>(this.ParseClockwiseArcTo)
                },
                { 
                    "wr",
                    new Action<string>(this.ParseClockwiseArc)
                },
                { 
                    "qx",
                    new Action<string>(this.ParseEllipticalQuadrantX)
                },
                { 
                    "qy",
                    new Action<string>(this.ParseEllipticalQuadrantY)
                },
                { 
                    "qb",
                    new Action<string>(this.ParseQuadraticBezier)
                }
            };

        private CommandInfo GetNextCommand(string path, int startIndex)
        {
            int num = startIndex;
            while (num < path.Length)
            {
                string[] commands = Commands;
                int index = 0;
                while (true)
                {
                    if (index >= commands.Length)
                    {
                        num++;
                        break;
                    }
                    string command = commands[index];
                    if (this.CheckLength(path.Length, num, command.Length) && this.CheckCommand(path, command, num))
                    {
                        return new CommandInfo(command, num);
                    }
                    index++;
                }
            }
            return CommandInfo.Empty;
        }

        public void OffsetPath(int dX, int dY)
        {
            for (int i = 0; i < this.vertices.Count; i++)
            {
                Point point = this.vertices[i];
                this.vertices[i] = new Point(this.vertices[i].X - dX, point.Y - dY);
            }
        }

        public void Parse(string path)
        {
            this.msoPathInfos.Clear();
            this.vertices.Clear();
            if (!string.IsNullOrEmpty(path))
            {
                CommandInfo nextCommand;
                int startIndex = 0;
                for (CommandInfo info = this.GetNextCommand(path, startIndex); (startIndex < path.Length) && !ReferenceEquals(info, CommandInfo.Empty); info = nextCommand)
                {
                    startIndex += info.Command.Length;
                    nextCommand = this.GetNextCommand(path, startIndex);
                    string content = ReferenceEquals(nextCommand, CommandInfo.Empty) ? path.Substring(startIndex) : path.Substring(startIndex, nextCommand.Position - startIndex);
                    this.ParseCommand(info.Command, content);
                    startIndex = nextCommand.Position;
                }
                if ((this.msoPathInfos.Count > 0) && (this.msoPathInfos[this.msoPathInfos.Count - 1].PathType != MsoPathType.End))
                {
                    this.msoPathInfos.Add(new MsoPathInfo(MsoPathType.End, -65536));
                }
            }
        }

        private void ParseAngleEllipse(string content)
        {
            this.ParseAngleEllipseCore(content, MsoPathEscape.AngleEllipse);
        }

        private void ParseAngleEllipseCore(string content, MsoPathEscape command)
        {
            List<int> list = this.PrepareParameters(content);
            int segments = 0;
            for (int i = 0; i < list.Count; i += 6)
            {
                int x = list[i];
                this.vertices.Add(new Point(x, ((i + 1) < list.Count) ? list[i + 1] : 0));
                int num5 = ((i + 2) < list.Count) ? list[i + 2] : 0;
                this.vertices.Add(new Point(num5, ((i + 3) < list.Count) ? list[i + 3] : 0));
                int num7 = ((i + 4) < list.Count) ? list[i + 4] : 0;
                int num8 = ((i + 5) < list.Count) ? list[i + 5] : 0;
                this.lastX = num7;
                this.lastY = num8;
                this.vertices.Add(new Point(this.lastX, this.lastY));
                segments += 3;
            }
            if (segments > 0)
            {
                this.AddMsoPathInfo(command, segments);
            }
        }

        private void ParseAngleEllipseTo(string content)
        {
            this.ParseAngleEllipseCore(content, MsoPathEscape.AngleEllipseTo);
        }

        private void ParseArc(string content)
        {
            this.ParseArcCore(content, MsoPathEscape.Arc);
        }

        private void ParseArcCore(string content, MsoPathEscape command)
        {
            List<int> list = this.PrepareParameters(content);
            int segments = 0;
            for (int i = 0; i < list.Count; i += 8)
            {
                this.vertices.Add(new Point(list[i], ((i + 1) < list.Count) ? list[i + 1] : 0));
                this.vertices.Add(new Point(((i + 2) < list.Count) ? list[i + 2] : 0, ((i + 3) < list.Count) ? list[i + 3] : 0));
                this.vertices.Add(new Point(((i + 4) < list.Count) ? list[i + 4] : 0, ((i + 5) < list.Count) ? list[i + 5] : 0));
                this.lastX = ((i + 6) < list.Count) ? list[i + 6] : 0;
                this.lastY = ((i + 7) < list.Count) ? list[i + 7] : 0;
                this.vertices.Add(new Point(this.lastX, this.lastY));
                segments += 4;
            }
            if (segments > 0)
            {
                this.AddMsoPathInfo(command, segments);
            }
        }

        private void ParseArcTo(string content)
        {
            this.ParseArcCore(content, MsoPathEscape.ArcTo);
        }

        private void ParseClockwiseArc(string content)
        {
            this.ParseArcCore(content, MsoPathEscape.ClockwiseArc);
        }

        private void ParseClockwiseArcTo(string content)
        {
            this.ParseArcCore(content, MsoPathEscape.ClockwiseArcTo);
        }

        private void ParseClose(string content)
        {
            this.msoPathInfos.Add(new MsoPathInfo(MsoPathType.Close, 1));
        }

        private void ParseCommand(string command, string content)
        {
            Action<string> action;
            if (this.parsers.TryGetValue(command, out action))
            {
                action(content);
            }
        }

        private void ParseCurveTo(string content)
        {
            List<int> list = this.PrepareParameters(content);
            int segments = 0;
            for (int i = 0; i < list.Count; i += 6)
            {
                this.vertices.Add(new Point(list[i], ((i + 1) < list.Count) ? list[i + 1] : 0));
                this.vertices.Add(new Point(((i + 2) < list.Count) ? list[i + 2] : 0, ((i + 3) < list.Count) ? list[i + 3] : 0));
                this.lastX = ((i + 4) < list.Count) ? list[i + 4] : 0;
                this.lastY = ((i + 5) < list.Count) ? list[i + 5] : 0;
                this.vertices.Add(new Point(this.lastX, this.lastY));
                segments++;
            }
            if (segments > 0)
            {
                this.AddMsoPathInfo(MsoPathType.CurveTo, segments);
            }
        }

        private void ParseEllipticalQuadrantCore(string content, MsoPathEscape command)
        {
            List<int> list = this.PrepareParameters(content);
            int segments = 0;
            for (int i = 0; i < list.Count; i += 2)
            {
                this.lastX = list[i];
                this.lastY = ((i + 1) < list.Count) ? list[i + 1] : 0;
                this.vertices.Add(new Point(this.lastX, this.lastY));
                segments++;
            }
            if (segments > 0)
            {
                this.AddMsoPathInfo(command, segments);
            }
        }

        private void ParseEllipticalQuadrantX(string content)
        {
            this.ParseEllipticalQuadrantCore(content, MsoPathEscape.EllipticalQuadrantX);
        }

        private void ParseEllipticalQuadrantY(string content)
        {
            this.ParseEllipticalQuadrantCore(content, MsoPathEscape.EllipticalQuadrantY);
        }

        private void ParseEnd(string content)
        {
            this.msoPathInfos.Add(new MsoPathInfo(MsoPathType.End, -65536));
        }

        private void ParseLineTo(string content)
        {
            List<int> list = this.PrepareParameters(content);
            int segments = 0;
            for (int i = 0; i < list.Count; i += 2)
            {
                this.lastX = list[i];
                this.lastY = ((i + 1) < list.Count) ? list[i + 1] : 0;
                this.vertices.Add(new Point(this.lastX, this.lastY));
                segments++;
            }
            if (segments > 0)
            {
                this.AddMsoPathInfo(MsoPathType.LineTo, segments);
            }
        }

        private void ParseMoveTo(string content)
        {
            List<int> list = this.PrepareParameters(content);
            for (int i = 0; i < list.Count; i += 2)
            {
                this.lastX = list[i];
                this.lastY = ((i + 1) < list.Count) ? list[i + 1] : 0;
                this.vertices.Add(new Point(this.lastX, this.lastY));
                this.msoPathInfos.Add(new MsoPathInfo(MsoPathType.MoveTo, 0));
            }
        }

        private void ParseNoFill(string content)
        {
            this.msoPathInfos.Add(new MsoPathInfo(MsoPathEscape.NoFill, 0));
        }

        private void ParseNoStroke(string content)
        {
            this.msoPathInfos.Add(new MsoPathInfo(MsoPathEscape.NoLine, 0));
        }

        private int ParseParameter(string value)
        {
            int num;
            return (!value.StartsWith("G") ? (!int.TryParse(value, out num) ? 0 : num) : ((int) Math.Round(this.calculator.GetGuideValue(value))));
        }

        private void ParseQuadraticBezier(string content)
        {
            List<int> list = this.PrepareParameters(content);
            int segments = 0;
            for (int i = 0; i < list.Count; i += 2)
            {
                this.lastX = list[i];
                this.lastY = ((i + 1) < list.Count) ? list[i + 1] : 0;
                this.vertices.Add(new Point(this.lastX, this.lastY));
                segments++;
            }
            if (segments > 0)
            {
                this.AddMsoPathInfo(MsoPathEscape.QuadraticBezier, segments);
            }
        }

        private void ParseRCurveTo(string content)
        {
            List<int> list = this.PrepareParameters(content);
            int segments = 0;
            for (int i = 0; i < list.Count; i += 6)
            {
                this.vertices.Add(new Point(this.lastX + list[i], this.lastY + (((i + 1) < list.Count) ? list[i + 1] : 0)));
                this.vertices.Add(new Point(this.lastX + (((i + 2) < list.Count) ? list[i + 2] : 0), this.lastY + (((i + 3) < list.Count) ? list[i + 3] : 0)));
                this.lastX += ((i + 4) < list.Count) ? list[i + 4] : 0;
                this.lastY += ((i + 5) < list.Count) ? list[i + 5] : 0;
                this.vertices.Add(new Point(this.lastX, this.lastY));
                segments++;
            }
            if (segments > 0)
            {
                this.AddMsoPathInfo(MsoPathType.CurveTo, segments);
            }
        }

        private void ParseRLineTo(string content)
        {
            List<int> list = this.PrepareParameters(content);
            int segments = 0;
            for (int i = 0; i < list.Count; i += 2)
            {
                this.lastX += list[i];
                this.lastY += ((i + 1) < list.Count) ? list[i + 1] : 0;
                this.vertices.Add(new Point(this.lastX, this.lastY));
                segments++;
            }
            if (segments > 0)
            {
                this.AddMsoPathInfo(MsoPathType.LineTo, segments);
            }
        }

        private void ParseRMoveTo(string content)
        {
            List<int> list = this.PrepareParameters(content);
            for (int i = 0; i < list.Count; i += 2)
            {
                this.lastX += list[i];
                this.lastY += ((i + 1) < list.Count) ? list[i + 1] : 0;
                this.vertices.Add(new Point(this.lastX, this.lastY));
                this.msoPathInfos.Add(new MsoPathInfo(MsoPathType.MoveTo, 0));
            }
        }

        internal List<int> PrepareParameters(string content)
        {
            List<int> list = new List<int>();
            if (!string.IsNullOrEmpty(content))
            {
                content = content.Trim();
                int num = 0;
                string str = string.Empty;
                while (num < content.Length)
                {
                    char ch = content[num];
                    if (ch == ' ')
                    {
                        while (true)
                        {
                            if (content[num + 1] != ' ')
                            {
                                if (!string.IsNullOrEmpty(str) && (str != "G"))
                                {
                                    list.Add(this.ParseParameter(str));
                                    str = string.Empty;
                                }
                                break;
                            }
                            num++;
                        }
                    }
                    else if (ch == ',')
                    {
                        if (!string.IsNullOrEmpty(str) && (str != "G"))
                        {
                            list.Add(this.ParseParameter(str));
                        }
                        else
                        {
                            list.Add(0);
                        }
                        str = string.Empty;
                    }
                    else if (ch != '@')
                    {
                        str = str + content[num].ToString();
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(str) && (str != "G"))
                        {
                            list.Add(this.ParseParameter(str));
                        }
                        str = "G";
                    }
                    num++;
                }
                if (!string.IsNullOrEmpty(str) && (str != "G"))
                {
                    list.Add(this.ParseParameter(str));
                }
                else
                {
                    list.Add(0);
                }
            }
            return list;
        }

        public MsoPathInfo[] MsoPathInfos =>
            this.msoPathInfos.ToArray();

        public Point[] Vertices =>
            this.vertices.ToArray();

        private class CommandInfo
        {
            public static readonly VmlPathParser.CommandInfo Empty = new VmlPathParser.CommandInfo(null, -1);

            public CommandInfo(string command, int position)
            {
                this.Command = command;
                this.Position = position;
            }

            public string Command { get; private set; }

            public int Position { get; private set; }
        }
    }
}

