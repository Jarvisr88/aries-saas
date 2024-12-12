namespace DevExpress.Office.Export.Binary
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using DevExpress.Office.Model;
    using DevExpress.Office.Utils;
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public static class BinaryDrawingExportHelper
    {
        private static Dictionary<DrawingPatternType, byte[]> patternsData;
        private static Dictionary<DrawingPatternType, byte> tagsByPatterns;
        public const int DefaultLineOpacity = 0x10000;

        static BinaryDrawingExportHelper()
        {
            Dictionary<DrawingPatternType, byte[]> dictionary = new Dictionary<DrawingPatternType, byte[]>();
            byte[] buffer1 = new byte[] { 
                40, 0, 0, 0, 8, 0, 0, 0, 8, 0, 0, 0, 1, 0, 1, 0,
                0, 0, 0, 0, 0x20, 0, 0, 0, 0x89, 11, 0, 0, 0x89, 11, 0, 0,
                2, 0, 0, 0, 2, 0, 0, 0, 0xff, 0xff, 0xff, 0, 0, 0, 0, 0,
                0xff, 0, 0, 0, 0xff, 0, 0, 0, 0xff, 0, 0, 0, 0xf7, 0, 0, 0,
                0xff, 0, 0, 0, 0xff, 0, 0, 0, 0xff, 0, 0, 0, 0x7f, 0, 0, 0
            };
            dictionary.Add(DrawingPatternType.Percent5, buffer1);
            byte[] buffer2 = new byte[] { 
                40, 0, 0, 0, 8, 0, 0, 0, 8, 0, 0, 0, 1, 0, 1, 0,
                0, 0, 0, 0, 0x20, 0, 0, 0, 0x89, 11, 0, 0, 0x89, 11, 0, 0,
                2, 0, 0, 0, 2, 0, 0, 0, 0xff, 0xff, 0xff, 0, 0, 0, 0, 0,
                170, 0, 0, 0, 0x55, 0, 0, 0, 170, 0, 0, 0, 0x55, 0, 0, 0,
                170, 0, 0, 0, 0x55, 0, 0, 0, 170, 0, 0, 0, 0x55, 0, 0, 0
            };
            dictionary.Add(DrawingPatternType.Percent50, buffer2);
            byte[] buffer3 = new byte[] { 
                40, 0, 0, 0, 8, 0, 0, 0, 8, 0, 0, 0, 1, 0, 1, 0,
                0, 0, 0, 0, 0x20, 0, 0, 0, 0x89, 11, 0, 0, 0x89, 11, 0, 0,
                2, 0, 0, 0, 2, 0, 0, 0, 0xff, 0xff, 0xff, 0, 0, 0, 0, 0,
                0xee, 0, 0, 0, 0xdd, 0, 0, 0, 0xbb, 0, 0, 0, 0x77, 0, 0, 0,
                0xee, 0, 0, 0, 0xdd, 0, 0, 0, 0xbb, 0, 0, 0, 0x77, 0, 0, 0
            };
            dictionary.Add(DrawingPatternType.LightDownwardDiagonal, buffer3);
            byte[] buffer4 = new byte[] { 
                40, 0, 0, 0, 8, 0, 0, 0, 8, 0, 0, 0, 1, 0, 1, 0,
                0, 0, 0, 0, 0x20, 0, 0, 0, 0x89, 11, 0, 0, 0x89, 11, 0, 0,
                2, 0, 0, 0, 2, 0, 0, 0, 0xff, 0xff, 0xff, 0, 0, 0, 0, 0,
                0x77, 0, 0, 0, 0x77, 0, 0, 0, 0x77, 0, 0, 0, 0x77, 0, 0, 0,
                0x77, 0, 0, 0, 0x77, 0, 0, 0, 0x77, 0, 0, 0, 0x77, 0, 0, 0
            };
            dictionary.Add(DrawingPatternType.LightVertical, buffer4);
            byte[] buffer5 = new byte[] { 
                40, 0, 0, 0, 8, 0, 0, 0, 8, 0, 0, 0, 1, 0, 1, 0,
                0, 0, 0, 0, 0x20, 0, 0, 0, 0x89, 11, 0, 0, 0x89, 11, 0, 0,
                2, 0, 0, 0, 2, 0, 0, 0, 0xff, 0xff, 0xff, 0, 0, 0, 0, 0,
                0xff, 0, 0, 0, 0xff, 0, 0, 0, 0xee, 0, 0, 0, 0xdd, 0, 0, 0,
                0xbb, 0, 0, 0, 0x77, 0, 0, 0, 0xff, 0, 0, 0, 0xff, 0, 0, 0
            };
            dictionary.Add(DrawingPatternType.DashedDownwardDiagonal, buffer5);
            byte[] buffer6 = new byte[] { 
                40, 0, 0, 0, 8, 0, 0, 0, 8, 0, 0, 0, 1, 0, 1, 0,
                0, 0, 0, 0, 0x20, 0, 0, 0, 0x89, 11, 0, 0, 0x89, 11, 0, 0,
                2, 0, 0, 0, 2, 0, 0, 0, 0xff, 0xff, 0xff, 0, 0, 0, 0, 0,
                0xe7, 0, 0, 0, 0xdb, 0, 0, 0, 0xbd, 0, 0, 0, 0x7e, 0, 0, 0,
                0xe7, 0, 0, 0, 0xdb, 0, 0, 0, 0xbd, 0, 0, 0, 0x7e, 0, 0, 0
            };
            dictionary.Add(DrawingPatternType.ZigZag, buffer6);
            byte[] buffer7 = new byte[] { 
                40, 0, 0, 0, 8, 0, 0, 0, 8, 0, 0, 0, 1, 0, 1, 0,
                0, 0, 0, 0, 0x20, 0, 0, 0, 0x89, 11, 0, 0, 0x89, 11, 0, 0,
                2, 0, 0, 0, 2, 0, 0, 0, 0xff, 0xff, 0xff, 0, 0, 0, 0, 0,
                0x7f, 0, 0, 0, 0xfe, 0, 0, 0, 0x7f, 0, 0, 0, 0xff, 0, 0, 0,
                0xef, 0, 0, 0, 0xf7, 0, 0, 0, 0xef, 0, 0, 0, 0xff, 0, 0, 0
            };
            dictionary.Add(DrawingPatternType.Divot, buffer7);
            byte[] buffer8 = new byte[] { 
                40, 0, 0, 0, 8, 0, 0, 0, 8, 0, 0, 0, 1, 0, 1, 0,
                0, 0, 0, 0, 0x20, 0, 0, 0, 0x89, 11, 0, 0, 0x89, 11, 0, 0,
                2, 0, 0, 0, 2, 0, 0, 0, 0xff, 0xff, 0xff, 0, 0, 0, 0, 0,
                0x77, 0, 0, 0, 0x77, 0, 0, 0, 0x77, 0, 0, 0, 0, 0, 0, 0,
                0x77, 0, 0, 0, 0x77, 0, 0, 0, 0x77, 0, 0, 0, 0, 0, 0, 0
            };
            dictionary.Add(DrawingPatternType.SmallGrid, buffer8);
            byte[] buffer9 = new byte[] { 
                40, 0, 0, 0, 8, 0, 0, 0, 8, 0, 0, 0, 1, 0, 1, 0,
                0, 0, 0, 0, 0x20, 0, 0, 0, 0x89, 11, 0, 0, 0x89, 11, 0, 0,
                2, 0, 0, 0, 2, 0, 0, 0, 0xff, 0xff, 0xff, 0, 0, 0, 0, 0,
                0xff, 0, 0, 0, 0xf7, 0, 0, 0, 0xff, 0, 0, 0, 0x7f, 0, 0, 0,
                0xff, 0, 0, 0, 0xf7, 0, 0, 0, 0xff, 0, 0, 0, 0x7f, 0, 0, 0
            };
            dictionary.Add(DrawingPatternType.Percent10, buffer9);
            byte[] buffer10 = new byte[] { 
                40, 0, 0, 0, 8, 0, 0, 0, 8, 0, 0, 0, 1, 0, 1, 0,
                0, 0, 0, 0, 0x20, 0, 0, 0, 0x89, 11, 0, 0, 0x89, 11, 0, 0,
                2, 0, 0, 0, 2, 0, 0, 0, 0xff, 0xff, 0xff, 0, 0, 0, 0, 0,
                170, 0, 0, 0, 0x44, 0, 0, 0, 170, 0, 0, 0, 0x11, 0, 0, 0,
                170, 0, 0, 0, 0x44, 0, 0, 0, 170, 0, 0, 0, 0x11, 0, 0, 0
            };
            dictionary.Add(DrawingPatternType.Percent60, buffer10);
            byte[] buffer11 = new byte[] { 
                40, 0, 0, 0, 8, 0, 0, 0, 8, 0, 0, 0, 1, 0, 1, 0,
                0, 0, 0, 0, 0x20, 0, 0, 0, 0x89, 11, 0, 0, 0x89, 11, 0, 0,
                2, 0, 0, 0, 2, 0, 0, 0, 0xff, 0xff, 0xff, 0, 0, 0, 0, 0,
                0x77, 0, 0, 0, 0xbb, 0, 0, 0, 0xdd, 0, 0, 0, 0xee, 0, 0, 0,
                0x77, 0, 0, 0, 0xbb, 0, 0, 0, 0xdd, 0, 0, 0, 0xee, 0, 0, 0
            };
            dictionary.Add(DrawingPatternType.LightUpwardDiagonal, buffer11);
            byte[] buffer12 = new byte[] { 
                40, 0, 0, 0, 8, 0, 0, 0, 8, 0, 0, 0, 1, 0, 1, 0,
                0, 0, 0, 0, 0x20, 0, 0, 0, 0x89, 11, 0, 0, 0x89, 11, 0, 0,
                2, 0, 0, 0, 2, 0, 0, 0, 0xff, 0xff, 0xff, 0, 0, 0, 0, 0,
                0xff, 0, 0, 0, 0xff, 0, 0, 0, 0xff, 0, 0, 0, 0, 0, 0, 0,
                0xff, 0, 0, 0, 0xff, 0, 0, 0, 0xff, 0, 0, 0, 0, 0, 0, 0
            };
            dictionary.Add(DrawingPatternType.LightHorizontal, buffer12);
            byte[] buffer13 = new byte[] { 
                40, 0, 0, 0, 8, 0, 0, 0, 8, 0, 0, 0, 1, 0, 1, 0,
                0, 0, 0, 0, 0x20, 0, 0, 0, 0x89, 11, 0, 0, 0x89, 11, 0, 0,
                2, 0, 0, 0, 2, 0, 0, 0, 0xff, 0xff, 0xff, 0, 0, 0, 0, 0,
                0xff, 0, 0, 0, 0xff, 0, 0, 0, 0x77, 0, 0, 0, 0xbb, 0, 0, 0,
                0xdd, 0, 0, 0, 0xee, 0, 0, 0, 0xff, 0, 0, 0, 0xff, 0, 0, 0
            };
            dictionary.Add(DrawingPatternType.DashedUpwardDiagonal, buffer13);
            byte[] buffer14 = new byte[] { 
                40, 0, 0, 0, 8, 0, 0, 0, 8, 0, 0, 0, 1, 0, 1, 0,
                0, 0, 0, 0, 0x20, 0, 0, 0, 0x89, 11, 0, 0, 0x89, 11, 0, 0,
                2, 0, 0, 0, 2, 0, 0, 0, 0xff, 0xff, 0xff, 0, 0, 0, 0, 0,
                0x3f, 0, 0, 0, 0xda, 0, 0, 0, 0xe7, 0, 0, 0, 0xff, 0, 0, 0,
                0x3f, 0, 0, 0, 0xda, 0, 0, 0, 0xe7, 0, 0, 0, 0xff, 0, 0, 0
            };
            dictionary.Add(DrawingPatternType.Wave, buffer14);
            byte[] buffer15 = new byte[] { 
                40, 0, 0, 0, 8, 0, 0, 0, 8, 0, 0, 0, 1, 0, 1, 0,
                0, 0, 0, 0, 0x20, 0, 0, 0, 0x89, 11, 0, 0, 0x89, 11, 0, 0,
                2, 0, 0, 0, 2, 0, 0, 0, 0xff, 0xff, 0xff, 0, 0, 0, 0, 0,
                0xff, 0, 0, 0, 0x7f, 0, 0, 0, 0xff, 0, 0, 0, 0x7f, 0, 0, 0,
                0xff, 0, 0, 0, 0x7f, 0, 0, 0, 0xff, 0, 0, 0, 0x55, 0, 0, 0
            };
            dictionary.Add(DrawingPatternType.DottedGrid, buffer15);
            byte[] buffer16 = new byte[] { 
                40, 0, 0, 0, 8, 0, 0, 0, 8, 0, 0, 0, 1, 0, 1, 0,
                0, 0, 0, 0, 0x20, 0, 0, 0, 0x89, 11, 0, 0, 0x89, 11, 0, 0,
                2, 0, 0, 0, 2, 0, 0, 0, 0xff, 0xff, 0xff, 0, 0, 0, 0, 0,
                0x7f, 0, 0, 0, 0x7f, 0, 0, 0, 0x7f, 0, 0, 0, 0x7f, 0, 0, 0,
                0x7f, 0, 0, 0, 0x7f, 0, 0, 0, 0x7f, 0, 0, 0, 0, 0, 0, 0
            };
            dictionary.Add(DrawingPatternType.LargeGrid, buffer16);
            byte[] buffer17 = new byte[] { 
                40, 0, 0, 0, 8, 0, 0, 0, 8, 0, 0, 0, 1, 0, 1, 0,
                0, 0, 0, 0, 0x20, 0, 0, 0, 0x89, 11, 0, 0, 0x89, 11, 0, 0,
                2, 0, 0, 0, 2, 0, 0, 0, 0xff, 0xff, 0xff, 0, 0, 0, 0, 0,
                0xff, 0, 0, 0, 0xdd, 0, 0, 0, 0xff, 0, 0, 0, 0x77, 0, 0, 0,
                0xff, 0, 0, 0, 0xdd, 0, 0, 0, 0xff, 0, 0, 0, 0x77, 0, 0, 0
            };
            dictionary.Add(DrawingPatternType.Percent20, buffer17);
            byte[] buffer18 = new byte[] { 
                40, 0, 0, 0, 8, 0, 0, 0, 8, 0, 0, 0, 1, 0, 1, 0,
                0, 0, 0, 0, 0x20, 0, 0, 0, 0x89, 11, 0, 0, 0x89, 11, 0, 0,
                2, 0, 0, 0, 2, 0, 0, 0, 0xff, 0xff, 0xff, 0, 0, 0, 0, 0,
                0x22, 0, 0, 0, 0x88, 0, 0, 0, 0x22, 0, 0, 0, 0x88, 0, 0, 0,
                0x22, 0, 0, 0, 0x88, 0, 0, 0, 0x22, 0, 0, 0, 0x88, 0, 0, 0
            };
            dictionary.Add(DrawingPatternType.Percent70, buffer18);
            byte[] buffer19 = new byte[] { 
                40, 0, 0, 0, 8, 0, 0, 0, 8, 0, 0, 0, 1, 0, 1, 0,
                0, 0, 0, 0, 0x20, 0, 0, 0, 0x89, 11, 0, 0, 0x89, 11, 0, 0,
                2, 0, 0, 0, 2, 0, 0, 0, 0xff, 0xff, 0xff, 0, 0, 0, 0, 0,
                0x66, 0, 0, 0, 0xcc, 0, 0, 0, 0x99, 0, 0, 0, 0x33, 0, 0, 0,
                0x66, 0, 0, 0, 0xcc, 0, 0, 0, 0x99, 0, 0, 0, 0x33, 0, 0, 0
            };
            dictionary.Add(DrawingPatternType.DarkDownwardDiagonal, buffer19);
            byte[] buffer20 = new byte[] { 
                40, 0, 0, 0, 8, 0, 0, 0, 8, 0, 0, 0, 1, 0, 1, 0,
                0, 0, 0, 0, 0x20, 0, 0, 0, 0x89, 11, 0, 0, 0x89, 11, 0, 0,
                2, 0, 0, 0, 2, 0, 0, 0, 0xff, 0xff, 0xff, 0, 0, 0, 0, 0,
                170, 0, 0, 0, 170, 0, 0, 0, 170, 0, 0, 0, 170, 0, 0, 0,
                170, 0, 0, 0, 170, 0, 0, 0, 170, 0, 0, 0, 170, 0, 0, 0
            };
            dictionary.Add(DrawingPatternType.NarrowVertical, buffer20);
            byte[] buffer21 = new byte[] { 
                40, 0, 0, 0, 8, 0, 0, 0, 8, 0, 0, 0, 1, 0, 1, 0,
                0, 0, 0, 0, 0x20, 0, 0, 0, 0x89, 11, 0, 0, 0x89, 11, 0, 0,
                2, 0, 0, 0, 2, 0, 0, 0, 0xff, 0xff, 0xff, 0, 0, 0, 0, 0,
                0xff, 0, 0, 0, 0xff, 0, 0, 0, 0xff, 0, 0, 0, 240, 0, 0, 0,
                0xff, 0, 0, 0, 0xff, 0, 0, 0, 0xff, 0, 0, 0, 15, 0, 0, 0
            };
            dictionary.Add(DrawingPatternType.DashedHorizontal, buffer21);
            byte[] buffer22 = new byte[] { 
                40, 0, 0, 0, 8, 0, 0, 0, 8, 0, 0, 0, 1, 0, 1, 0,
                0, 0, 0, 0, 0x20, 0, 0, 0, 0x89, 11, 0, 0, 0x89, 11, 0, 0,
                2, 0, 0, 0, 2, 0, 0, 0, 0xff, 0xff, 0xff, 0, 0, 0, 0, 0,
                0x7e, 0, 0, 0, 0xbd, 0, 0, 0, 0xdb, 0, 0, 0, 0xe7, 0, 0, 0,
                0xf7, 0, 0, 0, 0xfb, 0, 0, 0, 0xfd, 0, 0, 0, 0xfe, 0, 0, 0
            };
            dictionary.Add(DrawingPatternType.DiagonalBrick, buffer22);
            byte[] buffer23 = new byte[] { 
                40, 0, 0, 0, 8, 0, 0, 0, 8, 0, 0, 0, 1, 0, 1, 0,
                0, 0, 0, 0, 0x20, 0, 0, 0, 0x89, 11, 0, 0, 0x89, 11, 0, 0,
                2, 0, 0, 0, 2, 0, 0, 0, 0xff, 0xff, 0xff, 0, 0, 0, 0, 0,
                0xff, 0, 0, 0, 0xdd, 0, 0, 0, 0xff, 0, 0, 0, 0xf7, 0, 0, 0,
                0xff, 0, 0, 0, 0xdd, 0, 0, 0, 0xff, 0, 0, 0, 0x7f, 0, 0, 0
            };
            dictionary.Add(DrawingPatternType.DottedDiamond, buffer23);
            byte[] buffer24 = new byte[] { 
                40, 0, 0, 0, 8, 0, 0, 0, 8, 0, 0, 0, 1, 0, 1, 0,
                0, 0, 0, 0, 0x20, 0, 0, 0, 0x89, 11, 0, 0, 0x89, 11, 0, 0,
                2, 0, 0, 0, 2, 0, 0, 0, 0xff, 0xff, 0xff, 0, 0, 0, 0, 0,
                0x66, 0, 0, 0, 0x99, 0, 0, 0, 0x99, 0, 0, 0, 0x66, 0, 0, 0,
                0x66, 0, 0, 0, 0x99, 0, 0, 0, 0x99, 0, 0, 0, 0x66, 0, 0, 0
            };
            dictionary.Add(DrawingPatternType.SmallCheckerBoard, buffer24);
            byte[] buffer25 = new byte[] { 
                40, 0, 0, 0, 8, 0, 0, 0, 8, 0, 0, 0, 1, 0, 1, 0,
                0, 0, 0, 0, 0x20, 0, 0, 0, 0x89, 11, 0, 0, 0x89, 11, 0, 0,
                2, 0, 0, 0, 2, 0, 0, 0, 0xff, 0xff, 0xff, 0, 0, 0, 0, 0,
                0xdd, 0, 0, 0, 0x77, 0, 0, 0, 0xdd, 0, 0, 0, 0x77, 0, 0, 0,
                0xdd, 0, 0, 0, 0x77, 0, 0, 0, 0xdd, 0, 0, 0, 0x77, 0, 0, 0
            };
            dictionary.Add(DrawingPatternType.Percent25, buffer25);
            byte[] buffer26 = new byte[] { 
                40, 0, 0, 0, 8, 0, 0, 0, 8, 0, 0, 0, 1, 0, 1, 0,
                0, 0, 0, 0, 0x20, 0, 0, 0, 0x89, 11, 0, 0, 0x89, 11, 0, 0,
                2, 0, 0, 0, 2, 0, 0, 0, 0xff, 0xff, 0xff, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0x22, 0, 0, 0, 0, 0, 0, 0, 0x88, 0, 0, 0,
                0, 0, 0, 0, 0x22, 0, 0, 0, 0, 0, 0, 0, 0x88, 0, 0, 0
            };
            dictionary.Add(DrawingPatternType.Percent75, buffer26);
            byte[] buffer27 = new byte[] { 
                40, 0, 0, 0, 8, 0, 0, 0, 8, 0, 0, 0, 1, 0, 1, 0,
                0, 0, 0, 0, 0x20, 0, 0, 0, 0x89, 11, 0, 0, 0x89, 11, 0, 0,
                2, 0, 0, 0, 2, 0, 0, 0, 0xff, 0xff, 0xff, 0, 0, 0, 0, 0,
                0x66, 0, 0, 0, 0x33, 0, 0, 0, 0x99, 0, 0, 0, 0xcc, 0, 0, 0,
                0x66, 0, 0, 0, 0x33, 0, 0, 0, 0x99, 0, 0, 0, 0xcc, 0, 0, 0
            };
            dictionary.Add(DrawingPatternType.DarkUpwardDiagonal, buffer27);
            byte[] buffer28 = new byte[] { 
                40, 0, 0, 0, 8, 0, 0, 0, 8, 0, 0, 0, 1, 0, 1, 0,
                0, 0, 0, 0, 0x20, 0, 0, 0, 0x89, 11, 0, 0, 0x89, 11, 0, 0,
                2, 0, 0, 0, 2, 0, 0, 0, 0xff, 0xff, 0xff, 0, 0, 0, 0, 0,
                0xff, 0, 0, 0, 0, 0, 0, 0, 0xff, 0, 0, 0, 0, 0, 0, 0,
                0xff, 0, 0, 0, 0, 0, 0, 0, 0xff, 0, 0, 0, 0, 0, 0, 0
            };
            dictionary.Add(DrawingPatternType.NarrowHorizontal, buffer28);
            byte[] buffer29 = new byte[] { 
                40, 0, 0, 0, 8, 0, 0, 0, 8, 0, 0, 0, 1, 0, 1, 0,
                0, 0, 0, 0, 0x20, 0, 0, 0, 0x89, 11, 0, 0, 0x89, 11, 0, 0,
                2, 0, 0, 0, 2, 0, 0, 0, 0xff, 0xff, 0xff, 0, 0, 0, 0, 0,
                0xf7, 0, 0, 0, 0xf7, 0, 0, 0, 0xf7, 0, 0, 0, 0xf7, 0, 0, 0,
                0x7f, 0, 0, 0, 0x7f, 0, 0, 0, 0x7f, 0, 0, 0, 0x7f, 0, 0, 0
            };
            dictionary.Add(DrawingPatternType.DashedVertical, buffer29);
            byte[] buffer30 = new byte[] { 
                40, 0, 0, 0, 8, 0, 0, 0, 8, 0, 0, 0, 1, 0, 1, 0,
                0, 0, 0, 0, 0x20, 0, 0, 0, 0x89, 11, 0, 0, 0x89, 11, 0, 0,
                2, 0, 0, 0, 2, 0, 0, 0, 0xff, 0xff, 0xff, 0, 0, 0, 0, 0,
                0xf7, 0, 0, 0, 0xf7, 0, 0, 0, 0xf7, 0, 0, 0, 0, 0, 0, 0,
                0x7f, 0, 0, 0, 0x7f, 0, 0, 0, 0x7f, 0, 0, 0, 0, 0, 0, 0
            };
            dictionary.Add(DrawingPatternType.HorizontalBrick, buffer30);
            byte[] buffer31 = new byte[] { 
                40, 0, 0, 0, 8, 0, 0, 0, 8, 0, 0, 0, 1, 0, 1, 0,
                0, 0, 0, 0, 0x20, 0, 0, 0, 0x89, 11, 0, 0, 0x89, 11, 0, 0,
                2, 0, 0, 0, 2, 0, 0, 0, 0xff, 0xff, 0xff, 0, 0, 0, 0, 0,
                0xfe, 0, 0, 0, 0xfe, 0, 0, 0, 0xfd, 0, 0, 0, 0xf3, 0, 0, 0,
                0xcf, 0, 0, 0, 0xb7, 0, 0, 0, 0x7b, 0, 0, 0, 0xfc, 0, 0, 0
            };
            dictionary.Add(DrawingPatternType.Shingle, buffer31);
            byte[] buffer32 = new byte[] { 
                40, 0, 0, 0, 8, 0, 0, 0, 8, 0, 0, 0, 1, 0, 1, 0,
                0, 0, 0, 0, 0x20, 0, 0, 0, 0x89, 11, 0, 0, 0x89, 11, 0, 0,
                2, 0, 0, 0, 2, 0, 0, 0, 0xff, 0xff, 0xff, 0, 0, 0, 0, 0,
                240, 0, 0, 0, 240, 0, 0, 0, 240, 0, 0, 0, 240, 0, 0, 0,
                15, 0, 0, 0, 15, 0, 0, 0, 15, 0, 0, 0, 15, 0, 0, 0
            };
            dictionary.Add(DrawingPatternType.LargeCheckerBoard, buffer32);
            byte[] buffer33 = new byte[] { 
                40, 0, 0, 0, 8, 0, 0, 0, 8, 0, 0, 0, 1, 0, 1, 0,
                0, 0, 0, 0, 0x20, 0, 0, 0, 0x89, 11, 0, 0, 0x89, 11, 0, 0,
                2, 0, 0, 0, 2, 0, 0, 0, 0xff, 0xff, 0xff, 0, 0, 0, 0, 0,
                0xee, 0, 0, 0, 0x55, 0, 0, 0, 0xbb, 0, 0, 0, 0x55, 0, 0, 0,
                0xee, 0, 0, 0, 0x55, 0, 0, 0, 0xbb, 0, 0, 0, 0x55, 0, 0, 0
            };
            dictionary.Add(DrawingPatternType.Percent30, buffer33);
            byte[] buffer34 = new byte[] { 
                40, 0, 0, 0, 8, 0, 0, 0, 8, 0, 0, 0, 1, 0, 1, 0,
                0, 0, 0, 0, 0x20, 0, 0, 0, 0x89, 11, 0, 0, 0x89, 11, 0, 0,
                2, 0, 0, 0, 2, 0, 0, 0, 0xff, 0xff, 0xff, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0x10, 0, 0, 0,
                0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0x10, 0, 0, 0
            };
            dictionary.Add(DrawingPatternType.Percent80, buffer34);
            byte[] buffer35 = new byte[] { 
                40, 0, 0, 0, 8, 0, 0, 0, 8, 0, 0, 0, 1, 0, 1, 0,
                0, 0, 0, 0, 0x20, 0, 0, 0, 0x89, 11, 0, 0, 0x89, 11, 0, 0,
                2, 0, 0, 0, 2, 0, 0, 0, 0xff, 0xff, 0xff, 0, 0, 0, 0, 0,
                0x7c, 0, 0, 0, 0xf8, 0, 0, 0, 0xf1, 0, 0, 0, 0xe3, 0, 0, 0,
                0xc7, 0, 0, 0, 0x8f, 0, 0, 0, 0x1f, 0, 0, 0, 0x3e, 0, 0, 0
            };
            dictionary.Add(DrawingPatternType.WideDownwardDiagonal, buffer35);
            byte[] buffer36 = new byte[] { 
                40, 0, 0, 0, 8, 0, 0, 0, 8, 0, 0, 0, 1, 0, 1, 0,
                0, 0, 0, 0, 0x20, 0, 0, 0, 0x89, 11, 0, 0, 0x89, 11, 0, 0,
                2, 0, 0, 0, 2, 0, 0, 0, 0xff, 0xff, 0xff, 0, 0, 0, 0, 0,
                0x33, 0, 0, 0, 0x33, 0, 0, 0, 0x33, 0, 0, 0, 0x33, 0, 0, 0,
                0x33, 0, 0, 0, 0x33, 0, 0, 0, 0x33, 0, 0, 0, 0x33, 0, 0, 0
            };
            dictionary.Add(DrawingPatternType.DarkVertical, buffer36);
            byte[] buffer37 = new byte[] { 
                40, 0, 0, 0, 8, 0, 0, 0, 8, 0, 0, 0, 1, 0, 1, 0,
                0, 0, 0, 0, 0x20, 0, 0, 0, 0x89, 11, 0, 0, 0x89, 11, 0, 0,
                2, 0, 0, 0, 2, 0, 0, 0, 0xff, 0xff, 0xff, 0, 0, 0, 0, 0,
                0xfb, 0, 0, 0, 0xdf, 0, 0, 0, 0xfe, 0, 0, 0, 0xef, 0, 0, 0,
                0xfd, 0, 0, 0, 0xbf, 0, 0, 0, 0xf7, 0, 0, 0, 0x7f, 0, 0, 0
            };
            dictionary.Add(DrawingPatternType.SmallConfetti, buffer37);
            byte[] buffer38 = new byte[] { 
                40, 0, 0, 0, 8, 0, 0, 0, 8, 0, 0, 0, 1, 0, 1, 0,
                0, 0, 0, 0, 0x20, 0, 0, 0, 0x89, 11, 0, 0, 0x89, 11, 0, 0,
                2, 0, 0, 0, 2, 0, 0, 0, 0xff, 0xff, 0xff, 0, 0, 0, 0, 0,
                0xae, 0, 0, 0, 0xdd, 0, 0, 0, 0xeb, 0, 0, 0, 0x77, 0, 0, 0,
                0xba, 0, 0, 0, 0xdd, 0, 0, 0, 0xab, 0, 0, 0, 0x77, 0, 0, 0
            };
            dictionary.Add(DrawingPatternType.Weave, buffer38);
            byte[] buffer39 = new byte[] { 
                40, 0, 0, 0, 8, 0, 0, 0, 8, 0, 0, 0, 1, 0, 1, 0,
                0, 0, 0, 0, 0x20, 0, 0, 0, 0x89, 11, 0, 0, 0x89, 11, 0, 0,
                2, 0, 0, 0, 2, 0, 0, 0, 0xff, 0xff, 0xff, 0, 0, 0, 0, 0,
                0x66, 0, 0, 0, 0, 0, 0, 0, 0x99, 0, 0, 0, 0, 0, 0, 0,
                0x66, 0, 0, 0, 0, 0, 0, 0, 0x99, 0, 0, 0, 0, 0, 0, 0
            };
            dictionary.Add(DrawingPatternType.Trellis, buffer39);
            byte[] buffer40 = new byte[] { 
                40, 0, 0, 0, 8, 0, 0, 0, 8, 0, 0, 0, 1, 0, 1, 0,
                0, 0, 0, 0, 0x20, 0, 0, 0, 0x89, 11, 0, 0, 0x89, 11, 0, 0,
                2, 0, 0, 0, 2, 0, 0, 0, 0xff, 0xff, 0xff, 0, 0, 0, 0, 0,
                0xfe, 0, 0, 0, 0x7d, 0, 0, 0, 0xbb, 0, 0, 0, 0xd7, 0, 0, 0,
                0xef, 0, 0, 0, 0xd7, 0, 0, 0, 0xbb, 0, 0, 0, 0x7d, 0, 0, 0
            };
            dictionary.Add(DrawingPatternType.OpenDiamond, buffer40);
            byte[] buffer41 = new byte[] { 
                40, 0, 0, 0, 8, 0, 0, 0, 8, 0, 0, 0, 1, 0, 1, 0,
                0, 0, 0, 0, 0x20, 0, 0, 0, 0x89, 11, 0, 0, 0x89, 11, 0, 0,
                2, 0, 0, 0, 2, 0, 0, 0, 0xff, 0xff, 0xff, 0, 0, 0, 0, 0,
                0xea, 0, 0, 0, 0x55, 0, 0, 0, 170, 0, 0, 0, 0x55, 0, 0, 0,
                0xae, 0, 0, 0, 0x55, 0, 0, 0, 170, 0, 0, 0, 0x55, 0, 0, 0
            };
            dictionary.Add(DrawingPatternType.Percent40, buffer41);
            byte[] buffer42 = new byte[] { 
                40, 0, 0, 0, 8, 0, 0, 0, 8, 0, 0, 0, 1, 0, 1, 0,
                0, 0, 0, 0, 0x20, 0, 0, 0, 0x89, 11, 0, 0, 0x89, 11, 0, 0,
                2, 0, 0, 0, 2, 0, 0, 0, 0xff, 0xff, 0xff, 0, 0, 0, 0, 0,
                0x80, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                8, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
            };
            dictionary.Add(DrawingPatternType.Percent90, buffer42);
            byte[] buffer43 = new byte[] { 
                40, 0, 0, 0, 8, 0, 0, 0, 8, 0, 0, 0, 1, 0, 1, 0,
                0, 0, 0, 0, 0x20, 0, 0, 0, 0x89, 11, 0, 0, 0x89, 11, 0, 0,
                2, 0, 0, 0, 2, 0, 0, 0, 0xff, 0xff, 0xff, 0, 0, 0, 0, 0,
                0x3e, 0, 0, 0, 0x1f, 0, 0, 0, 0x8f, 0, 0, 0, 0xc7, 0, 0, 0,
                0xe3, 0, 0, 0, 0xf1, 0, 0, 0, 0xf8, 0, 0, 0, 0x7c, 0, 0, 0
            };
            dictionary.Add(DrawingPatternType.WideUpwardDiagonal, buffer43);
            byte[] buffer44 = new byte[] { 
                40, 0, 0, 0, 8, 0, 0, 0, 8, 0, 0, 0, 1, 0, 1, 0,
                0, 0, 0, 0, 0x20, 0, 0, 0, 0x89, 11, 0, 0, 0x89, 11, 0, 0,
                2, 0, 0, 0, 2, 0, 0, 0, 0xff, 0xff, 0xff, 0, 0, 0, 0, 0,
                0xff, 0, 0, 0, 0xff, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                0xff, 0, 0, 0, 0xff, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
            };
            dictionary.Add(DrawingPatternType.DarkHorizontal, buffer44);
            byte[] buffer45 = new byte[] { 
                40, 0, 0, 0, 8, 0, 0, 0, 8, 0, 0, 0, 1, 0, 1, 0,
                0, 0, 0, 0, 0x20, 0, 0, 0, 0x89, 11, 0, 0, 0x89, 11, 0, 0,
                2, 0, 0, 0, 2, 0, 0, 0, 0xff, 0xff, 0xff, 0, 0, 0, 0, 0,
                0x72, 0, 0, 0, 0xf3, 0, 0, 0, 0x3f, 0, 0, 0, 0x27, 0, 0, 0,
                0xe4, 0, 0, 0, 0xfc, 0, 0, 0, 0xcf, 0, 0, 0, 0x4e, 0, 0, 0
            };
            dictionary.Add(DrawingPatternType.LargeConfetti, buffer45);
            byte[] buffer46 = new byte[] { 
                40, 0, 0, 0, 8, 0, 0, 0, 8, 0, 0, 0, 1, 0, 1, 0,
                0, 0, 0, 0, 0x20, 0, 0, 0, 0x89, 11, 0, 0, 0x89, 11, 0, 0,
                2, 0, 0, 0, 2, 0, 0, 0, 0xff, 0xff, 0xff, 0, 0, 0, 0, 0,
                15, 0, 0, 0, 15, 0, 0, 0, 15, 0, 0, 0, 15, 0, 0, 0,
                170, 0, 0, 0, 0x55, 0, 0, 0, 170, 0, 0, 0, 0x55, 0, 0, 0
            };
            dictionary.Add(DrawingPatternType.Plaid, buffer46);
            byte[] buffer47 = new byte[] { 
                40, 0, 0, 0, 8, 0, 0, 0, 8, 0, 0, 0, 1, 0, 1, 0,
                0, 0, 0, 0, 0x20, 0, 0, 0, 0x89, 11, 0, 0, 0x89, 11, 0, 0,
                2, 0, 0, 0, 2, 0, 0, 0, 0xff, 0xff, 0xff, 0, 0, 0, 0, 0,
                7, 0, 0, 0, 7, 0, 0, 0, 0x67, 0, 0, 0, 0x88, 0, 0, 0,
                0x70, 0, 0, 0, 0x70, 0, 0, 0, 0x76, 0, 0, 0, 0x88, 0, 0, 0
            };
            dictionary.Add(DrawingPatternType.Sphere, buffer47);
            patternsData = dictionary;
            Dictionary<DrawingPatternType, byte> dictionary1 = new Dictionary<DrawingPatternType, byte>();
            dictionary1.Add(DrawingPatternType.Percent5, 0xc4);
            dictionary1.Add(DrawingPatternType.Percent50, 0xc5);
            dictionary1.Add(DrawingPatternType.LightDownwardDiagonal, 0xc6);
            dictionary1.Add(DrawingPatternType.LightVertical, 0xc7);
            dictionary1.Add(DrawingPatternType.DashedDownwardDiagonal, 200);
            dictionary1.Add(DrawingPatternType.ZigZag, 0xc9);
            dictionary1.Add(DrawingPatternType.Divot, 0xca);
            dictionary1.Add(DrawingPatternType.SmallGrid, 0xcb);
            dictionary1.Add(DrawingPatternType.Percent10, 0xcc);
            dictionary1.Add(DrawingPatternType.Percent60, 0xcd);
            dictionary1.Add(DrawingPatternType.LightUpwardDiagonal, 0xce);
            dictionary1.Add(DrawingPatternType.LightHorizontal, 0xcf);
            dictionary1.Add(DrawingPatternType.DashedUpwardDiagonal, 0xd0);
            dictionary1.Add(DrawingPatternType.Wave, 0xd1);
            dictionary1.Add(DrawingPatternType.DottedGrid, 210);
            dictionary1.Add(DrawingPatternType.LargeGrid, 0xd3);
            dictionary1.Add(DrawingPatternType.Percent20, 0xd4);
            dictionary1.Add(DrawingPatternType.Percent70, 0xd5);
            dictionary1.Add(DrawingPatternType.DarkDownwardDiagonal, 0xd6);
            dictionary1.Add(DrawingPatternType.NarrowVertical, 0xd7);
            dictionary1.Add(DrawingPatternType.DashedHorizontal, 0xd8);
            dictionary1.Add(DrawingPatternType.DiagonalBrick, 0xd9);
            dictionary1.Add(DrawingPatternType.DottedDiamond, 0xda);
            dictionary1.Add(DrawingPatternType.SmallCheckerBoard, 0xdb);
            dictionary1.Add(DrawingPatternType.Percent25, 220);
            dictionary1.Add(DrawingPatternType.Percent75, 0xdd);
            dictionary1.Add(DrawingPatternType.DarkUpwardDiagonal, 0xde);
            dictionary1.Add(DrawingPatternType.NarrowHorizontal, 0xdf);
            dictionary1.Add(DrawingPatternType.DashedVertical, 0xe0);
            dictionary1.Add(DrawingPatternType.HorizontalBrick, 0xe1);
            dictionary1.Add(DrawingPatternType.Shingle, 0xe2);
            dictionary1.Add(DrawingPatternType.LargeCheckerBoard, 0xe3);
            dictionary1.Add(DrawingPatternType.Percent30, 0xe4);
            dictionary1.Add(DrawingPatternType.Percent80, 0xe5);
            dictionary1.Add(DrawingPatternType.WideDownwardDiagonal, 230);
            dictionary1.Add(DrawingPatternType.DarkVertical, 0xe7);
            dictionary1.Add(DrawingPatternType.SmallConfetti, 0xe8);
            dictionary1.Add(DrawingPatternType.Weave, 0xe9);
            dictionary1.Add(DrawingPatternType.Trellis, 0xea);
            dictionary1.Add(DrawingPatternType.OpenDiamond, 0xeb);
            dictionary1.Add(DrawingPatternType.Percent40, 0xec);
            dictionary1.Add(DrawingPatternType.Percent90, 0xed);
            dictionary1.Add(DrawingPatternType.WideUpwardDiagonal, 0xee);
            dictionary1.Add(DrawingPatternType.DarkHorizontal, 0xef);
            dictionary1.Add(DrawingPatternType.LargeConfetti, 240);
            dictionary1.Add(DrawingPatternType.Plaid, 0xf1);
            dictionary1.Add(DrawingPatternType.Sphere, 0xf2);
            dictionary1.Add(DrawingPatternType.SolidDiamond, 0xf3);
            tagsByPatterns = dictionary1;
        }

        private static void AddAdjustValuesToProperties(OfficeArtPropertiesBase artProperties, int?[] adjustValues)
        {
            for (int i = 0; i < adjustValues.Length; i++)
            {
                if (adjustValues[i] != null)
                {
                    DrawingGeometryAdjustValue item = DrawingGeometryAdjustValueFactory.CreateAdjustValue(i);
                    if (item != null)
                    {
                        item.Value = adjustValues[i].Value;
                        artProperties.Properties.Add(item);
                    }
                }
            }
        }

        private static int CalculateScalingFactor(int factor)
        {
            double num = 0.65536;
            return (int) Math.Round((double) (factor * num));
        }

        private static int CalculateSkewAngle(int angle, int scalingFactor) => 
            (int) Math.Round((double) (Math.Tan(((((double) angle) / 60000.0) * 3.1415926535897931) / 180.0) * scalingFactor));

        private static BlackWhiteMode ConvertBlackWhiteMode(OpenXmlBlackWhiteMode mode)
        {
            switch (mode)
            {
                case OpenXmlBlackWhiteMode.Black:
                    return BlackWhiteMode.Black;

                case OpenXmlBlackWhiteMode.BlackGray:
                    return BlackWhiteMode.BlackTextLine;

                case OpenXmlBlackWhiteMode.BlackWhite:
                    return BlackWhiteMode.HighContrast;

                case OpenXmlBlackWhiteMode.Clr:
                    return BlackWhiteMode.Normal;

                case OpenXmlBlackWhiteMode.Gray:
                    return BlackWhiteMode.GrayScale;

                case OpenXmlBlackWhiteMode.GrayWhite:
                    return BlackWhiteMode.GrayOutline;

                case OpenXmlBlackWhiteMode.Hidden:
                    return BlackWhiteMode.DontShow;

                case OpenXmlBlackWhiteMode.InvGray:
                    return BlackWhiteMode.InverseGray;

                case OpenXmlBlackWhiteMode.LtGray:
                    return BlackWhiteMode.LightGrayScale;

                case OpenXmlBlackWhiteMode.White:
                    return BlackWhiteMode.White;
            }
            return BlackWhiteMode.Automatic;
        }

        public static void CreateAdjustValues(IOfficeShape shape, OfficeArtPropertiesBase artProperties)
        {
            CreateAdjustValues(shape.ShapeProperties, shape.Width, shape.Height, artProperties);
        }

        public static void CreateAdjustValues(ShapeProperties shapeProperties, float width, float height, OfficeArtPropertiesBase artProperties)
        {
            DocumentModelUnitConverter unitConverter = shapeProperties.DocumentModel.UnitConverter;
            int shapeHeight = unitConverter.CeilingModelUnitsToEmu(height);
            int shapeWidth = unitConverter.CeilingModelUnitsToEmu(width);
            ModelShapeCustomGeometry shapeGeometry = shapeProperties.GetShapeGeometry();
            int?[] adjustValues = GetAdjustValues(shapeGeometry, new ShapeGuideCalculator(shapeGeometry, (double) shapeWidth, (double) shapeHeight, shapeProperties.PresetAdjustList));
            AdjustValuesConverterToBinaryFormat.Convert(shapeWidth, shapeHeight, shapeProperties.ShapeType, adjustValues);
            AddAdjustValuesToProperties(artProperties, adjustValues);
        }

        private static void CreateAutomaticFillProperties(IDrawingColor defaultColor, List<IOfficeDrawingProperty> properties)
        {
            if (defaultColor != null)
            {
                CreateFillColor(defaultColor, properties);
            }
        }

        public static void CreateBlackAndWhiteMode(ShapePropertiesBase shapeProperties, OfficeArtPropertiesBase artProperties)
        {
            if (shapeProperties.BlackAndWhiteMode != OpenXmlBlackWhiteMode.Auto)
            {
                DrawingBlackWhiteMode item = new DrawingBlackWhiteMode {
                    Mode = ConvertBlackWhiteMode(shapeProperties.BlackAndWhiteMode)
                };
                artProperties.Properties.Add(item);
            }
        }

        public static OfficeArtChildAnchor CreateChildAnchor(Transform2D transform2D)
        {
            OfficeArtChildAnchor anchor = new OfficeArtChildAnchor();
            SetupChildAnchor(transform2D, anchor);
            return anchor;
        }

        private static void CreateColors(GradientStopCollection gradientStops, List<IOfficeDrawingProperty> properties, bool useFocus)
        {
            DrawingFillColor colorProperty = new DrawingFillColor();
            DrawingFillBackColor color2 = new DrawingFillBackColor();
            SetFillColor(colorProperty, gradientStops.First.Color);
            CreateFillOpacity(gradientStops.First.Color, properties);
            DrawingColor color = useFocus ? gradientStops[1].Color : gradientStops.Last.Color;
            SetFillColor(color2, color);
            CreateFillBackOpacity(color, properties);
            properties.Add(colorProperty);
            properties.Add(color2);
        }

        private static void CreateCommonDrawingProtectionProperties(ICommonDrawingLocks locks, DrawingBooleanProtectionProperties protectionProperties)
        {
            protectionProperties.LockGroup = locks.NoGroup;
            protectionProperties.LockSelect = locks.NoSelect;
            protectionProperties.LockAspectRatio = locks.NoChangeAspect;
            protectionProperties.LockPosition = locks.NoMove;
            protectionProperties.UseLockGroup = true;
            protectionProperties.UseLockSelect = true;
            protectionProperties.UseLockAspectRatio = true;
            protectionProperties.UseLockPosition = true;
        }

        private static void CreateConnectionSites(ShapeProperties shapeProperties, float height, float width, List<IOfficeDrawingProperty> properties)
        {
            CreateConnectionSitesCore(shapeProperties.GetShapeGeometry(), shapeProperties.DocumentModel.UnitConverter.ModelUnitsToEmuF(width), shapeProperties.DocumentModel.UnitConverter.ModelUnitsToEmuF(height), shapeProperties.PresetAdjustList, properties);
        }

        private static void CreateConnectionSitesCore(ModelShapeCustomGeometry geometry, int widthEmu, int heightEmu, ModelShapeGuideList adjustValues, List<IOfficeDrawingProperty> properties)
        {
            List<DrawingGeometryPoint> list = new List<DrawingGeometryPoint>();
            List<FixedPoint> list2 = new List<FixedPoint>();
            ShapeGuideCalculator calculator = new ShapeGuideCalculator(geometry, (double) widthEmu, (double) heightEmu, adjustValues);
            ModelShapeConnectionList connectionSites = geometry.ConnectionSites;
            if (connectionSites.Count != 0)
            {
                foreach (ModelShapeConnection connection in connectionSites)
                {
                    double num = connection.Angle.Evaluate(calculator) / 60000.0;
                    int x = (int) Math.Round(connection.X.Evaluate(calculator));
                    int y = (int) Math.Round(connection.Y.Evaluate(calculator));
                    list.Add(new DrawingGeometryPoint(x, y));
                    FixedPoint point1 = new FixedPoint();
                    point1.Value = num;
                    list2.Add(point1);
                }
                DrawingGeometryConnectionSites item = new DrawingGeometryConnectionSites();
                item.SetElements(list.ToArray());
                DrawingGeometryConnectionSitesDir dir = new DrawingGeometryConnectionSitesDir();
                dir.SetElements(list2.ToArray());
                properties.Add(item);
                properties.Add(dir);
                DrawingConnectionPointsType type = new DrawingConnectionPointsType(ConnectionPointsType.Custom);
                properties.Add(type);
            }
        }

        public static void CreateConnectionStyleProperty(ShapeProperties shapeProperties, OfficeArtPropertiesBase artProperties)
        {
            MsoCxStyle connectionStyle = GetConnectionStyle(shapeProperties.ShapeType);
            if (connectionStyle != MsoCxStyle.None)
            {
                DrawingCxStyle item = new DrawingCxStyle();
                item.Style = connectionStyle;
                artProperties.Properties.Add(item);
            }
        }

        public static void CreateCustomGeometryProperties(IOfficeShape shape, OfficeArtPropertiesBase artProperties)
        {
            CreateCustomGeometryProperties(shape.ShapeProperties, shape.Height, shape.Width, artProperties.Properties);
        }

        public static void CreateCustomGeometryProperties(ShapeProperties shapeProperties, float height, float width, List<IOfficeDrawingProperty> properties)
        {
            CreateShapePath(shapeProperties, height, width, properties);
            CreateConnectionSites(shapeProperties, height, width, properties);
        }

        public static void CreateDrawingProtectionProperties(IDrawingLocks locks, DrawingBooleanProtectionProperties protectionProperties)
        {
            CreateCommonDrawingProtectionProperties(locks, protectionProperties);
            protectionProperties.LockAdjustHandles = locks.NoAdjustHandles;
            protectionProperties.LockVertices = locks.NoEditPoints;
            protectionProperties.LockRotation = locks.NoRotate;
            protectionProperties.UseLockAdjustHandles = true;
            protectionProperties.UseLockVertices = true;
            protectionProperties.UseLockCropping = true;
            protectionProperties.UseLockRotation = true;
        }

        private static void CreateFillAngle(DrawingGradientFill drawingGradientFill, List<IOfficeDrawingProperty> properties)
        {
            if (drawingGradientFill.GradientType == GradientType.Linear)
            {
                double gradientAngle = GetGradientAngle(drawingGradientFill.Angle);
                if (gradientAngle != 0.0)
                {
                    properties.Add(new DrawingFillAngle(gradientAngle));
                }
            }
        }

        private static void CreateFillBackOpacity(IDrawingColor color, List<IOfficeDrawingProperty> properties)
        {
            double fillOpacity = GetFillOpacity(color);
            if (fillOpacity != 1.0)
            {
                properties.Add(new DrawingFillBackOpacity(fillOpacity));
            }
        }

        private static void CreateFillColor(IDrawingColor color, List<IOfficeDrawingProperty> properties)
        {
            DrawingFillColor colorProperty = new DrawingFillColor();
            SetFillColor(colorProperty, color);
            properties.Add(colorProperty);
        }

        private static void CreateFillFocus(GradientStopCollection gradientStops, List<IOfficeDrawingProperty> properties, bool useFocus)
        {
            int focus = useFocus ? GetFillFocus(gradientStops[1]) : 100;
            if (focus != 0)
            {
                properties.Add(new DrawingFillFocus(focus));
            }
        }

        private static void CreateFillOpacity(IDrawingColor color, List<IOfficeDrawingProperty> properties)
        {
            double fillOpacity = GetFillOpacity(color);
            if (fillOpacity != 1.0)
            {
                properties.Add(new DrawingFillOpacity(fillOpacity));
            }
        }

        public static void CreateFillProperties(IOfficeShape shape, OfficeArtPropertiesBase artProperties, Dictionary<DrawingPatternType, DrawingTableInfo> patternBlipTable)
        {
            CreateFillProperties(shape.ShapeProperties.Fill, shape.ShapeProperties.ShapeType, shape.ShapeStyle, artProperties, patternBlipTable);
        }

        public static void CreateFillProperties(IDrawingFill fill, OfficeArtPropertiesBase artProperties, IDrawingColor defaultColor, Dictionary<DrawingPatternType, DrawingTableInfo> patternBlipTable)
        {
            CreateFillProperties(fill, artProperties.Properties, defaultColor, patternBlipTable);
        }

        public static void CreateFillProperties(IDrawingFill fill, List<IOfficeDrawingProperty> properties, IDrawingColor defaultColor, Dictionary<DrawingPatternType, DrawingTableInfo> patternBlipTable)
        {
            switch (fill.FillType)
            {
                case DrawingFillType.Automatic:
                    CreateAutomaticFillProperties(defaultColor, properties);
                    return;

                case DrawingFillType.None:
                    return;

                case DrawingFillType.Solid:
                    CreateSolidFillProperties((DrawingSolidFill) fill, properties);
                    return;

                case DrawingFillType.Gradient:
                    CreateGradientFillProperties((DrawingGradientFill) fill, properties);
                    return;

                case DrawingFillType.Group:
                    CreateGroupFillProperties(properties);
                    return;

                case DrawingFillType.Pattern:
                    CreatePatternFillProperties((DrawingPatternFill) fill, properties, patternBlipTable);
                    return;

                case DrawingFillType.Picture:
                    return;
            }
            throw new ArgumentOutOfRangeException();
        }

        public static void CreateFillProperties(IDrawingFill fill, ShapePreset shapeType, ShapeStyle shapeStyle, OfficeArtPropertiesBase artProperties, Dictionary<DrawingPatternType, DrawingTableInfo> patternBlipTable)
        {
            CreateFillProperties(fill, shapeType, shapeStyle, artProperties.Properties, patternBlipTable);
        }

        public static void CreateFillProperties(IDrawingFill fill, ShapePreset shapeType, ShapeStyle shapeStyle, List<IOfficeDrawingProperty> properties, Dictionary<DrawingPatternType, DrawingTableInfo> patternBlipTable)
        {
            DrawingColor defaultColor = null;
            DrawingFillType fillType = fill.FillType;
            bool filled = fillType != DrawingFillType.None;
            if (fillType == DrawingFillType.Automatic)
            {
                if ((shapeStyle != null) && (shapeStyle.FillReferenceIdx > 0))
                {
                    defaultColor = shapeStyle.FillColor;
                }
                else
                {
                    filled = false;
                }
            }
            if (!IsConnector(shapeType))
            {
                CreateFillProperties(fill, properties, defaultColor, patternBlipTable);
            }
            CreateFillStyleBooleanProperties(fill, properties, filled);
        }

        private static void CreateFillRect(RectangleOffset rect, List<IOfficeDrawingProperty> properties)
        {
            double num = ((double) rect.LeftOffset) / 100000.0;
            double num2 = ((double) rect.TopOffset) / 100000.0;
            double num3 = 1.0 - (((double) rect.RightOffset) / 100000.0);
            double num4 = 1.0 - (((double) rect.BottomOffset) / 100000.0);
            if (num != 0.0)
            {
                properties.Add(new DrawingFillToLeft(num));
            }
            if (num2 != 0.0)
            {
                properties.Add(new DrawingFillToTop(num2));
            }
            if (num3 != 0.0)
            {
                properties.Add(new DrawingFillToRight(num3));
            }
            if (num4 != 0.0)
            {
                properties.Add(new DrawingFillToBottom(num4));
            }
        }

        public static void CreateFillStyleBooleanProperties(IDrawingFill fill, OfficeArtPropertiesBase artProperties, bool filled)
        {
            CreateFillStyleBooleanProperties(fill, artProperties.Properties, filled);
        }

        public static void CreateFillStyleBooleanProperties(IDrawingFill fill, List<IOfficeDrawingProperty> properties, bool filled)
        {
            DrawingFillStyleBooleanProperties item = new DrawingFillStyleBooleanProperties {
                UseFilled = true,
                Filled = filled
            };
            DrawingGradientFill fill2 = fill as DrawingGradientFill;
            if ((fill2 != null) && fill2.RotateWithShape)
            {
                item.UseShapeAnchor = true;
                item.ShapeAnchor = true;
            }
            properties.Add(item);
        }

        private static void CreateFillType(DrawingGradientFill drawingGradientFill, List<IOfficeDrawingProperty> properties)
        {
            OfficeFillType officeFillType = GetOfficeFillType(drawingGradientFill);
            if (officeFillType != OfficeFillType.Solid)
            {
                OfficeDrawingFillType item = new OfficeDrawingFillType();
                item.FillType = officeFillType;
                properties.Add(item);
            }
        }

        private static void CreateGeometrySpaceProperties(List<IOfficeDrawingProperty> properties, int geometrySpaceRight, int geometrySpaceBottom)
        {
            int num = 0;
            int num2 = 0;
            DrawingGeometryLeft item = new DrawingGeometryLeft();
            item.Value = num;
            properties.Add(item);
            DrawingGeometryTop top1 = new DrawingGeometryTop();
            top1.Value = num2;
            properties.Add(top1);
            DrawingGeometryRight right1 = new DrawingGeometryRight();
            right1.Value = geometrySpaceRight;
            properties.Add(right1);
            DrawingGeometryBottom bottom1 = new DrawingGeometryBottom();
            bottom1.Value = geometrySpaceBottom;
            properties.Add(bottom1);
        }

        private static void CreateGradientFillProperties(DrawingGradientFill drawingGradientFill, List<IOfficeDrawingProperty> properties)
        {
            GradientStopCollection gradientStops = drawingGradientFill.GradientStops;
            if ((gradientStops != null) && (gradientStops.Count != 0))
            {
                CreateFillType(drawingGradientFill, properties);
                bool useFocus = GradientWithFocus(gradientStops);
                CreateColors(gradientStops, properties, useFocus);
                CreateFillAngle(drawingGradientFill, properties);
                CreateFillFocus(gradientStops, properties, useFocus);
                CreateFillRect(drawingGradientFill.FillRect, properties);
                CreateShadeColors(gradientStops, properties, useFocus);
            }
        }

        private static void CreateGroupFillProperties(List<IOfficeDrawingProperty> properties)
        {
            OfficeDrawingFillType item = new OfficeDrawingFillType();
            item.FillType = OfficeFillType.Background;
            properties.Add(item);
        }

        public static void CreateGroupShapeProtectionProperties(IGroupLocks locks, OfficeArtPropertiesBase artProperties)
        {
            DrawingBooleanProtectionProperties item = new DrawingBooleanProtectionProperties {
                LockText = true,
                UseLockText = true
            };
            artProperties.Properties.Add(item);
            if ((locks != null) && !locks.IsEmpty)
            {
                CreateCommonDrawingProtectionProperties(locks, item);
                item.LockUngroup = locks.NoUngroup;
                item.LockRotation = locks.NoRotate;
                item.UseLockUngroup = true;
                item.UseLockRotation = true;
            }
        }

        private static void CreateLineArrowheadProperties(OutlineInfo outline, OfficeArtPropertiesBase artProperties)
        {
            MsoLineEnd msoLineEnd = OutlineHeadTailTypeConverter.GetMsoLineEnd(outline.HeadType);
            if (msoLineEnd != MsoLineEnd.NoEnd)
            {
                DrawingLineStartArrowhead item = new DrawingLineStartArrowhead();
                item.Arrowhead = msoLineEnd;
                artProperties.Properties.Add(item);
            }
            MsoLineEnd end2 = OutlineHeadTailTypeConverter.GetMsoLineEnd(outline.TailType);
            if (end2 != MsoLineEnd.NoEnd)
            {
                DrawingLineEndArrowhead item = new DrawingLineEndArrowhead();
                item.Arrowhead = end2;
                artProperties.Properties.Add(item);
            }
            if (outline.HeadWidth != OutlineHeadTailSize.Medium)
            {
                DrawingLineStartArrowWidth item = new DrawingLineStartArrowWidth();
                item.HeadTailSize = outline.HeadWidth;
                artProperties.Properties.Add(item);
            }
            if (outline.HeadLength != OutlineHeadTailSize.Medium)
            {
                DrawingLineStartArrowLength item = new DrawingLineStartArrowLength();
                item.HeadTailSize = outline.HeadLength;
                artProperties.Properties.Add(item);
            }
            if (outline.TailWidth != OutlineHeadTailSize.Medium)
            {
                DrawingLineEndArrowWidth item = new DrawingLineEndArrowWidth();
                item.HeadTailSize = outline.TailWidth;
                artProperties.Properties.Add(item);
            }
            if (outline.TailLength != OutlineHeadTailSize.Medium)
            {
                DrawingLineEndArrowLength item = new DrawingLineEndArrowLength();
                item.HeadTailSize = outline.TailLength;
                artProperties.Properties.Add(item);
            }
        }

        public static void CreateLineColorProperties(DrawingColor outlineColor, OfficeArtPropertiesBase artProperties)
        {
            if (!outlineColor.Info.IsEmpty)
            {
                DrawingLineColor item = new DrawingLineColor();
                if (outlineColor.ColorType != DrawingColorType.System)
                {
                    item.ColorRecord.Color = outlineColor.FinalColor;
                }
                else
                {
                    int systemColor = (int) outlineColor.Info.SystemColor;
                    if (systemColor >= 0)
                    {
                        item.ColorRecord.SystemColorIndex = systemColor;
                    }
                    else
                    {
                        item.ColorRecord.Color = outlineColor.FinalColor;
                    }
                }
                artProperties.Properties.Add(item);
            }
            foreach (ColorTransformBase base2 in outlineColor.Transforms)
            {
                AlphaColorTransform transform = base2 as AlphaColorTransform;
                if (transform != null)
                {
                    int lineOpacity = GetLineOpacity(transform.Value);
                    if (lineOpacity != 0x10000)
                    {
                        DrawingLineOpacity item = new DrawingLineOpacity(lineOpacity);
                        artProperties.Properties.Add(item);
                        break;
                    }
                }
            }
        }

        public static void CreateLineStyleBooleanProperties(OfficeArtPropertiesBase artProperties, bool useLine, bool line)
        {
            DrawingLineStyleBooleanProperties item = new DrawingLineStyleBooleanProperties {
                UseLine = useLine,
                Line = line
            };
            artProperties.Properties.Add(item);
        }

        private static OfficeArtTertiaryProperties CreateOfficeArtTertiaryProperties(CompositeOfficeDrawingPartBase shape)
        {
            OfficeArtTertiaryProperties item = new OfficeArtTertiaryProperties();
            item.Properties.Clear();
            shape.Items.Add(item);
            return item;
        }

        public static void CreateOutlineProperties(IOfficeShape shape, OfficeArtPropertiesBase artProperties)
        {
            IDocumentModel documentModel = shape.DocumentModel;
            ShapeProperties shapeProperties = shape.ShapeProperties;
            ShapeStyle shapeStyle = shape.ShapeStyle;
            Outline shapeOutline = shapeProperties.Outline;
            OutlineInfo outline = ActualOutlineHelper.MergeOutline(shapeOutline, documentModel.OfficeTheme.FormatScheme.GetOutline(shapeStyle.LineReferenceIdx));
            DrawingFillType fillType = shapeOutline.Fill.FillType;
            DrawingColor outlineColor = (fillType == DrawingFillType.Automatic) ? shapeStyle.LineColor : shapeProperties.OutlineColor;
            if (fillType != DrawingFillType.None)
            {
                CreateLineColorProperties(outlineColor, artProperties);
                FillOutlineProperties(outline, artProperties, documentModel);
            }
            CreateLineStyleBooleanProperties(artProperties, true, fillType != DrawingFillType.None);
        }

        public static void CreateOutlineProperties(Outline outline, OutlineType outlineType, DrawingColor color, OfficeArtPropertiesBase artProperties, DrawingColor defaultColor)
        {
            bool flag = outline.Fill.FillType == DrawingFillType.Automatic;
            bool line = (outlineType == OutlineType.Solid) | flag;
            if (line)
            {
                CreateLineColorProperties(flag ? defaultColor : color, artProperties);
                FillOutlineProperties(outline.Info, artProperties, outline.DocumentModel);
            }
            CreateLineStyleBooleanProperties(artProperties, true, line);
        }

        private static void CreatePatternFillProperties(DrawingPatternFill drawingPatternFill, List<IOfficeDrawingProperty> properties, Dictionary<DrawingPatternType, DrawingTableInfo> patternBlipTable)
        {
            DrawingTableInfo info;
            if ((patternBlipTable != null) && patternBlipTable.TryGetValue(drawingPatternFill.PatternType, out info))
            {
                DrawingFillBlipIdentifier item = new DrawingFillBlipIdentifier {
                    Value = info.Index
                };
                properties.Add(item);
                OfficeDrawingFillType type1 = new OfficeDrawingFillType();
                type1.FillType = OfficeFillType.Pattern;
                properties.Add(type1);
                DrawingFillColor colorProperty = new DrawingFillColor();
                SetFillColor(colorProperty, drawingPatternFill.ForegroundColor);
                properties.Add(colorProperty);
                double fillOpacity = GetFillOpacity(drawingPatternFill.ForegroundColor);
                if (fillOpacity != 1.0)
                {
                    properties.Add(new DrawingFillOpacity(fillOpacity));
                }
                DrawingFillBackColor color2 = new DrawingFillBackColor();
                SetFillColor(color2, drawingPatternFill.BackgroundColor);
                properties.Add(color2);
                double opacity = GetFillOpacity(drawingPatternFill.BackgroundColor);
                if (opacity != 1.0)
                {
                    properties.Add(new DrawingFillBackOpacity(opacity));
                }
            }
        }

        private static void CreateShadeColors(GradientStopCollection gradientStops, List<IOfficeDrawingProperty> properties, bool useFocus)
        {
            if (!useFocus && (gradientStops.Count > 2))
            {
                properties.Add(GetDrawingFillShadeColors(gradientStops));
            }
        }

        private static void CreateShadowBooleanProperties(List<IOfficeDrawingProperty> properties)
        {
            DrawingShadowStyleBooleanProperties item = new DrawingShadowStyleBooleanProperties {
                UseShadow = true,
                Shadow = true
            };
            properties.Add(item);
        }

        private static void CreateShadowColorProperty(DrawingColor drawingColor, List<IOfficeDrawingProperty> properties)
        {
            DrawingShadowColor colorProperty = new DrawingShadowColor();
            SetFillColor(colorProperty, drawingColor);
            properties.Add(colorProperty);
        }

        private static void CreateShadowOffsetProperties(OffsetShadowInfo offsetShadowInfo, List<IOfficeDrawingProperty> properties)
        {
            double d = ShapeGuideCalculator.EMUDegreeToRadian((double) offsetShadowInfo.Direction);
            int offset = (int) Math.Round((double) (offsetShadowInfo.Distance * Math.Cos(d)));
            int num3 = (int) Math.Round((double) (offsetShadowInfo.Distance * Math.Sin(d)));
            if (offset != 0x6338)
            {
                DrawingShadowOffsetX item = new DrawingShadowOffsetX(offset);
                properties.Add(item);
            }
            if (num3 != 0x6338)
            {
                DrawingShadowOffsetY item = new DrawingShadowOffsetY(num3);
                properties.Add(item);
            }
        }

        private static void CreateShadowOpacityProperty(DrawingColor shadowColor, List<IOfficeDrawingProperty> properties)
        {
            AlphaColorTransform transform = null;
            foreach (ColorTransformBase base2 in shadowColor.Transforms)
            {
                transform = base2 as AlphaColorTransform;
                if (transform != null)
                {
                    break;
                }
            }
            if (transform != null)
            {
                double num = ((double) transform.Value) / 100000.0;
                if (num != 1.0)
                {
                    DrawingShadowOpacity item = new DrawingShadowOpacity(num);
                    properties.Add(item);
                }
            }
        }

        private static void CreateShadowOriginProperties(RectangleAlignType alignment, List<IOfficeDrawingProperty> properties)
        {
            double shadowOriginXByAlignment = GetShadowOriginXByAlignment(alignment);
            double shadowOriginYByAlignment = GetShadowOriginYByAlignment(alignment);
            if (shadowOriginXByAlignment != 0.0)
            {
                DrawingShadowOriginX item = new DrawingShadowOriginX(shadowOriginXByAlignment);
                properties.Add(item);
            }
            if (shadowOriginYByAlignment != 0.0)
            {
                DrawingShadowOriginY item = new DrawingShadowOriginY(shadowOriginYByAlignment);
                properties.Add(item);
            }
        }

        public static void CreateShadowProperties(DrawingEffectCollection drawingEffects, CompositeOfficeDrawingPartBase shapeContainer)
        {
            OfficeArtPropertiesBase base2 = OfficeArtPropertiesHelper.FindPart(shapeContainer, 0xf00b) as OfficeArtPropertiesBase;
            if (base2 != null)
            {
                foreach (IDrawingEffect effect in drawingEffects)
                {
                    OuterShadowEffect shadowEffect = effect as OuterShadowEffect;
                    if (shadowEffect != null)
                    {
                        CreateShadowProperties(shadowEffect, ((OfficeArtPropertiesHelper.FindPart(shapeContainer, 0xf122) as OfficeArtTertiaryProperties) ?? CreateOfficeArtTertiaryProperties(shapeContainer)).Properties, base2.Properties);
                        break;
                    }
                }
            }
        }

        public static void CreateShadowProperties(OuterShadowEffect shadowEffect, List<IOfficeDrawingProperty> tertiaryArtProperties, List<IOfficeDrawingProperty> properties)
        {
            CreateShadowPropertiesCore(shadowEffect, properties);
            CreateShadowSoftnessProperty(shadowEffect.Info.BlurRadius, tertiaryArtProperties);
        }

        private static void CreateShadowPropertiesCore(OuterShadowEffect shadowEffect, List<IOfficeDrawingProperty> properties)
        {
            CreateShadowTypeProperty(shadowEffect.Info, properties);
            CreateShadowColorProperty(shadowEffect.Color, properties);
            CreateShadowOpacityProperty(shadowEffect.Color, properties);
            CreateShadowOffsetProperties(shadowEffect.Info.OffsetShadow, properties);
            CreateShadowScalingFactorAndSkewAnglesProperties(shadowEffect.Info, properties);
            CreateShadowOriginProperties(shadowEffect.Info.ShadowAlignment, properties);
            CreateShadowBooleanProperties(properties);
        }

        private static void CreateShadowScalingFactorAndSkewAnglesProperties(OuterShadowEffectInfo shadowEffectInfo, List<IOfficeDrawingProperty> properties)
        {
            ScalingFactorInfo scalingFactor = shadowEffectInfo.ScalingFactor;
            int num = CalculateScalingFactor(scalingFactor.Horizontal);
            int num2 = CalculateScalingFactor(scalingFactor.Vertical);
            if (num != 0x10000)
            {
                DrawingShadowHorizontalScalingFactor item = new DrawingShadowHorizontalScalingFactor(num);
                properties.Add(item);
            }
            if (num2 != 0x10000)
            {
                DrawingShadowVerticalScalingFactor item = new DrawingShadowVerticalScalingFactor(num2);
                properties.Add(item);
            }
            CreateShadowSkewAnglesProperties(shadowEffectInfo.SkewAngles, properties, num, num2);
        }

        private static void CreateShadowSkewAnglesProperties(SkewAnglesInfo skewAnglesInfo, List<IOfficeDrawingProperty> properties, int horizontalScalingFactor, int verticalScalingFactor)
        {
            int num = CalculateSkewAngle(skewAnglesInfo.Horizontal, horizontalScalingFactor);
            int num2 = CalculateSkewAngle(skewAnglesInfo.Vertical, verticalScalingFactor);
            if (num != 0)
            {
                DrawingShadowHorizontalSkewAngle item = new DrawingShadowHorizontalSkewAngle(num);
                properties.Add(item);
            }
            if (num2 != 0)
            {
                DrawingShadowVerticalSkewAngle item = new DrawingShadowVerticalSkewAngle(num2);
                properties.Add(item);
            }
        }

        private static void CreateShadowSoftnessProperty(long blurRadius, List<IOfficeDrawingProperty> properties)
        {
            if (blurRadius != 0)
            {
                DrawingShadowSoftness item = new DrawingShadowSoftness((blurRadius > 0x7fffffffL) ? 0x7fffffff : ((int) blurRadius));
                properties.Add(item);
            }
        }

        private static void CreateShadowTypeProperty(OuterShadowEffectInfo shadowEffectInfo, List<IOfficeDrawingProperty> properties)
        {
            MsoShadowType shadowType = GetShadowType(shadowEffectInfo);
            if (shadowType != MsoShadowType.MsoShadowOffset)
            {
                DrawingShadowType item = new DrawingShadowType(shadowType);
                properties.Add(item);
            }
        }

        private static void CreateShapePath(ShapeProperties shapeProperties, float height, float width, List<IOfficeDrawingProperty> properties)
        {
            CreateShapePathCore(shapeProperties.GetShapeGeometry(), shapeProperties.DocumentModel.UnitConverter.ModelUnitsToEmuF(width), shapeProperties.DocumentModel.UnitConverter.ModelUnitsToEmuF(height), shapeProperties.PresetAdjustList, properties);
        }

        private static void CreateShapePathCore(ModelShapeCustomGeometry geometry, int widthEmu, int heightEmu, ModelShapeGuideList adjustValues, List<IOfficeDrawingProperty> properties)
        {
            ShapePathType shapePathType = GetShapePathType(geometry);
            ShapeGuideCalculator calculator = new ShapeGuideCalculator(geometry, (double) widthEmu, (double) heightEmu, adjustValues);
            PathInstructionExportWalker walker = new PathInstructionExportWalker(geometry.Paths, calculator);
            walker.Walk();
            int geometrySpaceRight = walker.GeometrySpaceRight;
            geometrySpaceRight ??= widthEmu;
            int geometrySpaceBottom = walker.GeometrySpaceBottom;
            geometrySpaceBottom ??= heightEmu;
            CreateGeometrySpaceProperties(properties, geometrySpaceRight, geometrySpaceBottom);
            DrawingGeometryShapePath path1 = new DrawingGeometryShapePath();
            path1.ShapePath = shapePathType;
            DrawingGeometryShapePath item = path1;
            properties.Add(item);
            DrawingGeometryVertices vertices = new DrawingGeometryVertices();
            vertices.SetElements(walker.Points);
            properties.Add(vertices);
            DrawingGeometrySegmentInfo info = new DrawingGeometrySegmentInfo();
            info.SetElements(walker.MsoPathInfos);
            properties.Add(info);
        }

        public static void CreateShapeProtectionProperties(IDrawingLocks locks, OfficeArtPropertiesBase artProperties)
        {
            if ((locks != null) && !locks.IsEmpty)
            {
                DrawingBooleanProtectionProperties protectionProperties = new DrawingBooleanProtectionProperties();
                IShapeLocks locks2 = locks as IShapeLocks;
                if (locks2 != null)
                {
                    protectionProperties.LockText = locks2.NoTextEdit;
                }
                protectionProperties.UseLockText = true;
                CreateDrawingProtectionProperties(locks, protectionProperties);
                artProperties.Properties.Add(protectionProperties);
            }
        }

        private static void CreateSolidFillProperties(DrawingSolidFill drawingSolidFill, List<IOfficeDrawingProperty> properties)
        {
            CreateFillColor(drawingSolidFill.Color, properties);
            CreateFillOpacity(drawingSolidFill.Color, properties);
        }

        public static void CreateTransformProperties(Transform2D transform2D, OfficeArtPropertiesBase artProperties, OfficeArtShapeRecord shapeRecord)
        {
            shapeRecord.FlipH = transform2D.FlipH;
            shapeRecord.FlipV = transform2D.FlipV;
            if (transform2D.Rotation != 0)
            {
                float num = transform2D.DocumentModel.UnitConverter.ModelUnitsToDegreeF(transform2D.Rotation);
                if (transform2D.FlipH)
                {
                    num = 360f - num;
                }
                if (transform2D.FlipV)
                {
                    num = 360f - num;
                }
                DrawingRotation item = new DrawingRotation {
                    Value = num
                };
                artProperties.Properties.Add(item);
            }
        }

        public static void CreateWordArtAdjustValues(IOfficeShape shape, DrawingPresetTextWarp presetTextWarp, ModelShapeGuideList wordArtAdjustValues, OfficeArtPropertiesBase artProperties)
        {
            CreateWordArtAdjustValuesCore(shape.Height, shape.Width, shape.DocumentModel, presetTextWarp, wordArtAdjustValues, artProperties);
        }

        public static void CreateWordArtAdjustValues(float width, float height, DrawingTextBodyProperties bodyProperties, OfficeArtPropertiesBase artProperties)
        {
            CreateWordArtAdjustValuesCore(height, width, bodyProperties.DocumentModel, bodyProperties.PresetTextWarp, bodyProperties.PresetAdjustValues, artProperties);
        }

        private static void CreateWordArtAdjustValuesCore(float height, float width, IDocumentModel documentModel, DrawingPresetTextWarp presetTextWarp, ModelShapeGuideList wordArtAdjustValues, OfficeArtPropertiesBase artProperties)
        {
            int heightEmu = documentModel.UnitConverter.ModelUnitsToEmuF(height);
            int widthEmu = documentModel.UnitConverter.ModelUnitsToEmuF(width);
            ModelShapeCustomGeometry wordArtFakeGeometry = GetWordArtFakeGeometry(presetTextWarp, documentModel);
            int?[] adjustValues = GetAdjustValues(wordArtFakeGeometry, new ShapeGuideCalculator(wordArtFakeGeometry, (double) widthEmu, (double) heightEmu, wordArtAdjustValues));
            AdjustValuesConverterToBinaryFormat.ConvertWordArt(widthEmu, heightEmu, presetTextWarp, adjustValues);
            AddAdjustValuesToProperties(artProperties, adjustValues);
        }

        public static void FillCoordinateSystem(Transform2D childTransform2D, OfficeArtShapeGroupCoordinateSystem coordinateSystem)
        {
            DocumentModelUnitConverter unitConverter = childTransform2D.DocumentModel.UnitConverter;
            coordinateSystem.Left = unitConverter.ModelUnitsToEmuF(childTransform2D.OffsetX);
            coordinateSystem.Top = unitConverter.ModelUnitsToEmuF(childTransform2D.OffsetY);
            coordinateSystem.Right = coordinateSystem.Left + unitConverter.ModelUnitsToEmuF(childTransform2D.Cx);
            coordinateSystem.Bottom = coordinateSystem.Top + unitConverter.ModelUnitsToEmuF(childTransform2D.Cy);
        }

        public static void FillOutlineProperties(OutlineInfo outline, OfficeArtPropertiesBase artProperties, IDocumentModel documentModel)
        {
            DrawingLineWidth item = new DrawingLineWidth {
                Value = documentModel.UnitConverter.ModelUnitsToEmu(outline.Width)
            };
            artProperties.Properties.Add(item);
            if (outline.MiterLimit != OutlineInfo.DefaultInfo.MiterLimit)
            {
                DrawingLineMiterLimit limit = new DrawingLineMiterLimit {
                    Value = DrawingValueConverter.FromPercentage(outline.MiterLimit)
                };
                artProperties.Properties.Add(limit);
            }
            if (outline.CompoundType != OutlineCompoundType.Single)
            {
                DrawingLineCompoundType type = new DrawingLineCompoundType {
                    CompoundType = outline.CompoundType
                };
                artProperties.Properties.Add(type);
            }
            if (outline.Dashing != OutlineDashing.Solid)
            {
                DrawingLineDashing dashing = new DrawingLineDashing {
                    Dashing = outline.Dashing
                };
                artProperties.Properties.Add(dashing);
            }
            CreateLineArrowheadProperties(outline, artProperties);
            DrawingLineJoinStyle style = new DrawingLineJoinStyle {
                Style = outline.JoinStyle
            };
            artProperties.Properties.Add(style);
            if (outline.EndCapStyle != OutlineEndCapStyle.Flat)
            {
                DrawingLineCapStyle style2 = new DrawingLineCapStyle {
                    Style = outline.EndCapStyle
                };
                artProperties.Properties.Add(style2);
            }
        }

        public static void FillPatternBlips(OfficeArtBlipStoreContainer blipContainer, Dictionary<DrawingPatternType, DrawingTableInfo> patternBlipTable)
        {
            if (patternBlipTable != null)
            {
                foreach (KeyValuePair<DrawingPatternType, DrawingTableInfo> pair in patternBlipTable)
                {
                    byte[] bytesByPattern = GetBytesByPattern(pair.Key);
                    PatternBlip blip = new PatternBlip(null);
                    byte tagByPatter = GetTagByPatter(pair.Key);
                    blip.SetData(bytesByPattern, tagByPatter);
                    FileBlipStoreEntry item = new FileBlipStoreEntry(blip, false) {
                        ReferenceCount = pair.Value.RefCount
                    };
                    blipContainer.Blips.Add(item);
                    pair.Value.Index = blipContainer.Blips.Count;
                }
            }
        }

        private static int?[] GetAdjustValues(ModelShapeCustomGeometry geometry, ShapeGuideCalculator calculator)
        {
            int?[] nullableArray = new int?[8];
            ModelShapeGuideList adjustValues = geometry.AdjustValues;
            for (int i = 0; i < adjustValues.Count; i++)
            {
                ModelShapeGuide guide = adjustValues[i];
                nullableArray[i] = new int?((int) Math.Round(calculator.GetGuideValue(guide.Name)));
            }
            return nullableArray;
        }

        private static byte[] GetBytesByPattern(DrawingPatternType pattern)
        {
            byte[] buffer;
            if (patternsData.TryGetValue(pattern, out buffer))
            {
                return buffer;
            }
            if (pattern > DrawingPatternType.DownwardDiagonal)
            {
                if (pattern == DrawingPatternType.Horizontal)
                {
                    return patternsData[DrawingPatternType.LightHorizontal];
                }
                else if (pattern == DrawingPatternType.UpwardDiagonal)
                {
                    return patternsData[DrawingPatternType.LightUpwardDiagonal];
                }
                else if (pattern == DrawingPatternType.Vertical)
                {
                    return patternsData[DrawingPatternType.LightVertical];
                }
            }
            else if (pattern == DrawingPatternType.Cross)
            {
                return patternsData[DrawingPatternType.SmallGrid];
            }
            else if (pattern == DrawingPatternType.DiagonalCross)
            {
                return patternsData[DrawingPatternType.OpenDiamond];
            }
            else if (pattern == DrawingPatternType.DownwardDiagonal)
            {
                return patternsData[DrawingPatternType.LightDownwardDiagonal];
            }
            return patternsData[DrawingPatternType.Percent5];
        }

        private static MsoCxStyle GetConnectionStyle(ShapePreset shapeType)
        {
            switch (shapeType)
            {
                case ShapePreset.StraightConnector1:
                    return MsoCxStyle.Straight;

                case ShapePreset.BentConnector2:
                case ShapePreset.BentConnector3:
                case ShapePreset.BentConnector4:
                case ShapePreset.BentConnector5:
                    return MsoCxStyle.Bent;

                case ShapePreset.CurvedConnector2:
                case ShapePreset.CurvedConnector3:
                case ShapePreset.CurvedConnector4:
                case ShapePreset.CurvedConnector5:
                    return MsoCxStyle.Curved;
            }
            return MsoCxStyle.None;
        }

        private static double GetDrawingFillShadeColorPosition(int gradientStopPosition) => 
            Math.Min((double) (((double) gradientStopPosition) / 100000.0), (double) 1.0);

        private static DrawingFillShadeColors GetDrawingFillShadeColors(GradientStopCollection gradientStopCollection)
        {
            DrawingFillShadeColors colors = new DrawingFillShadeColors();
            colors.SetElements(GetDrawingFillShadeColorsCore(gradientStopCollection).ToArray());
            return colors;
        }

        public static List<OfficeShadeColor> GetDrawingFillShadeColorsCore(GradientStopCollection gradientStopCollection)
        {
            List<OfficeShadeColor> list = new List<OfficeShadeColor>();
            foreach (DrawingGradientStop stop in gradientStopCollection)
            {
                OfficeShadeColor item = new OfficeShadeColor(stop.Color.FinalColor, GetDrawingFillShadeColorPosition(stop.Position));
                list.Add(item);
            }
            return list;
        }

        private static int GetFillFocus(DrawingGradientStop gradientStop) => 
            Math.Min((int) Math.Round((double) ((((double) gradientStop.Position) / 100000.0) * 100.0)), 100);

        private static double GetFillOpacity(IDrawingColor color)
        {
            double num2;
            using (IEnumerator<ColorTransformBase> enumerator = color.Transforms.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        ColorTransformBase current = enumerator.Current;
                        AlphaColorTransform transform = current as AlphaColorTransform;
                        if (transform == null)
                        {
                            continue;
                        }
                        num2 = Math.Min((double) (((double) transform.Value) / 100000.0), (double) 1.0);
                    }
                    else
                    {
                        return 1.0;
                    }
                    break;
                }
            }
            return num2;
        }

        public static double GetGradientAngle(int gradientAngleEmu)
        {
            double num = ((double) gradientAngleEmu) / 60000.0;
            num = (num < 0.0) ? -(num + 90.0) : (90.0 - num);
            if (num < 0.0)
            {
                num += 360.0;
            }
            if (num > 360.0)
            {
                num -= 360.0;
            }
            return num;
        }

        private static int GetLineOpacity(int alphaTrasfromValue)
        {
            int num = 0x10000;
            int num2 = (int) Math.Round((double) ((alphaTrasfromValue * num) / 100000.0));
            num2 = (num2 / 0x101) * 0x101;
            if ((num2 % 0x101) > 0x80)
            {
                num2 += 0x101;
            }
            return num2;
        }

        private static OfficeFillType GetOfficeFillType(DrawingGradientFill drawingGradientFill)
        {
            switch (drawingGradientFill.GradientType)
            {
                case GradientType.Linear:
                    return OfficeFillType.ShadeScale;

                case GradientType.Rectangle:
                    return OfficeFillType.ShadeCenter;

                case GradientType.Circle:
                case GradientType.Shape:
                    return OfficeFillType.ShadeShape;
            }
            return OfficeFillType.Solid;
        }

        private static double GetShadowOriginXByAlignment(RectangleAlignType alignment)
        {
            switch (alignment)
            {
                case RectangleAlignType.TopLeft:
                case RectangleAlignType.Left:
                case RectangleAlignType.BottomLeft:
                    return -0.5;

                case RectangleAlignType.Top:
                case RectangleAlignType.Center:
                case RectangleAlignType.Bottom:
                    return 0.0;

                case RectangleAlignType.TopRight:
                case RectangleAlignType.Right:
                case RectangleAlignType.BottomRight:
                    return 0.5;
            }
            throw new ArgumentOutOfRangeException();
        }

        private static double GetShadowOriginYByAlignment(RectangleAlignType alignment)
        {
            switch (alignment)
            {
                case RectangleAlignType.TopLeft:
                case RectangleAlignType.Top:
                case RectangleAlignType.TopRight:
                    return -0.5;

                case RectangleAlignType.Left:
                case RectangleAlignType.Center:
                case RectangleAlignType.Right:
                    return 0.0;

                case RectangleAlignType.BottomLeft:
                case RectangleAlignType.Bottom:
                case RectangleAlignType.BottomRight:
                    return 0.5;
            }
            throw new ArgumentOutOfRangeException();
        }

        private static MsoShadowType GetShadowType(OuterShadowEffectInfo shadowEffectInfo) => 
            (((shadowEffectInfo.ScalingFactor.Horizontal == 0x186a0) && ((shadowEffectInfo.ScalingFactor.Vertical == 0x186a0) && (shadowEffectInfo.SkewAngles.Horizontal == 0))) && (shadowEffectInfo.SkewAngles.Vertical == 0)) ? MsoShadowType.MsoShadowOffset : MsoShadowType.MsoShadowRich;

        private static ShapePathType GetShapePathType(ModelShapeCustomGeometry geometry)
        {
            ModelShapePathsList paths = geometry.Paths;
            if (paths.Count == 1)
            {
                ModelShapePath path = paths[0];
                if ((path.Instructions.Count > 1) && (path.Instructions[0] is PathMove))
                {
                    ModelPathInstructionList instructions = path.Instructions;
                    bool closed = instructions[instructions.Count - 1] is PathClose;
                    IPathInstruction instruction = instructions[1];
                    if (IsPathLine(instruction) && InstructionsIdentical(instructions, closed, new IsSomeModelPathType(BinaryDrawingExportHelper.IsPathLine)))
                    {
                        return (closed ? ShapePathType.LinesClosed : ShapePathType.Lines);
                    }
                    if (IsPathCubicBezier(instruction) && InstructionsIdentical(instructions, closed, new IsSomeModelPathType(BinaryDrawingExportHelper.IsPathCubicBezier)))
                    {
                        return (closed ? ShapePathType.CurvesClosed : ShapePathType.Curves);
                    }
                }
            }
            return ShapePathType.Complex;
        }

        public static int GetShapeType(ShapePreset shapeType)
        {
            switch (shapeType)
            {
                case ShapePreset.None:
                    return 0;

                case ShapePreset.Line:
                    return 20;

                case ShapePreset.Triangle:
                    return 5;

                case ShapePreset.RtTriangle:
                    return 6;

                case ShapePreset.Rect:
                    return 1;

                case ShapePreset.Diamond:
                    return 4;

                case ShapePreset.Parallelogram:
                    return 7;

                case ShapePreset.Pentagon:
                    return 0x38;

                case ShapePreset.Hexagon:
                    return 9;

                case ShapePreset.Octagon:
                    return 10;

                case ShapePreset.Star4:
                    return 0xbb;

                case ShapePreset.Star5:
                    return 12;

                case ShapePreset.Star8:
                    return 0x3a;

                case ShapePreset.Star16:
                    return 0x12;

                case ShapePreset.Star24:
                    return 0x5c;

                case ShapePreset.Star32:
                    return 60;

                case ShapePreset.RoundRect:
                    return 2;

                case ShapePreset.Plaque:
                    return 0x15;

                case ShapePreset.Ellipse:
                    return 3;

                case ShapePreset.HomePlate:
                    return 15;

                case ShapePreset.Chevron:
                    return 0x37;

                case ShapePreset.RightArrow:
                    return 13;

                case ShapePreset.LeftArrow:
                    return 0x42;

                case ShapePreset.UpArrow:
                    return 0x44;

                case ShapePreset.DownArrow:
                    return 0x43;

                case ShapePreset.NotchedRightArrow:
                    return 0x5e;

                case ShapePreset.LeftRightArrow:
                    return 0x45;

                case ShapePreset.UpDownArrow:
                    return 70;

                case ShapePreset.LeftArrowCallout:
                    return 0x4d;

                case ShapePreset.RightArrowCallout:
                    return 0x4e;

                case ShapePreset.UpArrowCallout:
                    return 0x4f;

                case ShapePreset.DownArrowCallout:
                    return 80;

                case ShapePreset.LeftRightArrowCallout:
                    return 0x51;

                case ShapePreset.UpDownArrowCallout:
                    return 0x52;

                case ShapePreset.CurvedRightArrow:
                    return 0x66;

                case ShapePreset.CurvedLeftArrow:
                    return 0x67;

                case ShapePreset.CurvedUpArrow:
                    return 0x68;

                case ShapePreset.CurvedDownArrow:
                    return 0x69;

                case ShapePreset.Cube:
                    return 0x10;

                case ShapePreset.Can:
                    return 0x16;

                case ShapePreset.LightningBolt:
                    return 0x49;

                case ShapePreset.Sun:
                    return 0xb7;

                case ShapePreset.Moon:
                    return 0xb8;

                case ShapePreset.SmileyFace:
                    return 0x60;

                case ShapePreset.IrregularSeal1:
                    return 0x47;

                case ShapePreset.IrregularSeal2:
                    return 0x48;

                case ShapePreset.FoldedCorner:
                    return 0x41;

                case ShapePreset.Bevel:
                    return 0x54;

                case ShapePreset.LeftBracket:
                    return 0x55;

                case ShapePreset.RightBracket:
                    return 0x56;

                case ShapePreset.LeftBrace:
                    return 0x57;

                case ShapePreset.RightBrace:
                    return 0x58;

                case ShapePreset.BracketPair:
                    return 0xb9;

                case ShapePreset.BracePair:
                    return 0xba;

                case ShapePreset.StraightConnector1:
                    return 0x20;

                case ShapePreset.BentConnector2:
                    return 0x21;

                case ShapePreset.BentConnector3:
                    return 0x22;

                case ShapePreset.BentConnector4:
                    return 0x23;

                case ShapePreset.BentConnector5:
                    return 0x24;

                case ShapePreset.CurvedConnector2:
                    return 0x25;

                case ShapePreset.CurvedConnector3:
                    return 0x26;

                case ShapePreset.CurvedConnector4:
                    return 0x27;

                case ShapePreset.CurvedConnector5:
                    return 40;

                case ShapePreset.Callout1:
                    return 0x29;

                case ShapePreset.Callout2:
                    return 0x2a;

                case ShapePreset.Callout3:
                    return 0x2b;

                case ShapePreset.AccentCallout1:
                    return 0x2c;

                case ShapePreset.AccentCallout2:
                    return 0x2d;

                case ShapePreset.AccentCallout3:
                    return 0x2e;

                case ShapePreset.BorderCallout1:
                    return 0x2f;

                case ShapePreset.BorderCallout2:
                    return 0x30;

                case ShapePreset.BorderCallout3:
                    return 0x31;

                case ShapePreset.AccentBorderCallout1:
                    return 50;

                case ShapePreset.AccentBorderCallout2:
                    return 0x33;

                case ShapePreset.AccentBorderCallout3:
                    return 0x34;

                case ShapePreset.WedgeRectCallout:
                    return 0x3d;

                case ShapePreset.WedgeRoundRectCallout:
                    return 0x3e;

                case ShapePreset.WedgeEllipseCallout:
                    return 0x3f;

                case ShapePreset.CloudCallout:
                    return 0x6a;

                case ShapePreset.Ribbon:
                    return 0x35;

                case ShapePreset.Ribbon2:
                    return 0x36;

                case ShapePreset.EllipseRibbon:
                    return 0x6b;

                case ShapePreset.EllipseRibbon2:
                    return 0x6c;

                case ShapePreset.VerticalScroll:
                    return 0x61;

                case ShapePreset.HorizontalScroll:
                    return 0x62;

                case ShapePreset.Wave:
                    return 0x40;

                case ShapePreset.DoubleWave:
                    return 0xbc;

                case ShapePreset.Plus:
                    return 11;

                case ShapePreset.FlowChartProcess:
                    return 0x6d;

                case ShapePreset.FlowChartDecision:
                    return 110;

                case ShapePreset.FlowChartInputOutput:
                    return 0x6f;

                case ShapePreset.FlowChartPredefinedProcess:
                    return 0x70;

                case ShapePreset.FlowChartInternalStorage:
                    return 0x71;

                case ShapePreset.FlowChartDocument:
                    return 0x72;

                case ShapePreset.FlowChartMultidocument:
                    return 0x73;

                case ShapePreset.FlowChartTerminator:
                    return 0x74;

                case ShapePreset.FlowChartPreparation:
                    return 0x75;

                case ShapePreset.FlowChartManualInput:
                    return 0x76;

                case ShapePreset.FlowChartManualOperation:
                    return 0x77;

                case ShapePreset.FlowChartConnector:
                    return 120;

                case ShapePreset.FlowChartPunchedCard:
                    return 0x79;

                case ShapePreset.FlowChartPunchedTape:
                    return 0x7a;

                case ShapePreset.FlowChartSummingJunction:
                    return 0x7b;

                case ShapePreset.FlowChartOr:
                    return 0x7c;

                case ShapePreset.FlowChartCollate:
                    return 0x7d;

                case ShapePreset.FlowChartSort:
                    return 0x7e;

                case ShapePreset.FlowChartExtract:
                    return 0x7f;

                case ShapePreset.FlowChartMerge:
                    return 0x80;

                case ShapePreset.FlowChartOfflineStorage:
                    return 0x81;

                case ShapePreset.FlowChartOnlineStorage:
                    return 130;

                case ShapePreset.FlowChartMagneticTape:
                    return 0x83;

                case ShapePreset.FlowChartMagneticDisk:
                    return 0x84;

                case ShapePreset.FlowChartMagneticDrum:
                    return 0x85;

                case ShapePreset.FlowChartDisplay:
                    return 0x86;

                case ShapePreset.FlowChartDelay:
                    return 0x87;

                case ShapePreset.FlowChartAlternateProcess:
                    return 0xb0;

                case ShapePreset.FlowChartOffpageConnector:
                    return 0xb1;

                case ShapePreset.ActionButtonBlank:
                    return 0xbd;

                case ShapePreset.ActionButtonHome:
                    return 190;

                case ShapePreset.ActionButtonHelp:
                    return 0xbf;

                case ShapePreset.ActionButtonInformation:
                    return 0xc0;

                case ShapePreset.ActionButtonForwardNext:
                    return 0xc1;

                case ShapePreset.ActionButtonBackPrevious:
                    return 0xc2;

                case ShapePreset.ActionButtonEnd:
                    return 0xc3;

                case ShapePreset.ActionButtonBeginning:
                    return 0xc4;

                case ShapePreset.ActionButtonReturn:
                    return 0xc5;

                case ShapePreset.ActionButtonDocument:
                    return 0xc6;

                case ShapePreset.ActionButtonSound:
                    return 0xc7;

                case ShapePreset.ActionButtonMovie:
                    return 200;
            }
            return 0;
        }

        private static byte GetTagByPatter(DrawingPatternType pattern)
        {
            byte num;
            if (tagsByPatterns.TryGetValue(pattern, out num))
            {
                return num;
            }
            if (pattern > DrawingPatternType.DownwardDiagonal)
            {
                if (pattern == DrawingPatternType.Horizontal)
                {
                    return tagsByPatterns[DrawingPatternType.LightHorizontal];
                }
                else if (pattern == DrawingPatternType.UpwardDiagonal)
                {
                    return tagsByPatterns[DrawingPatternType.LightUpwardDiagonal];
                }
                else if (pattern == DrawingPatternType.Vertical)
                {
                    return tagsByPatterns[DrawingPatternType.LightVertical];
                }
            }
            else if (pattern == DrawingPatternType.Cross)
            {
                return tagsByPatterns[DrawingPatternType.SmallGrid];
            }
            else if (pattern == DrawingPatternType.DiagonalCross)
            {
                return tagsByPatterns[DrawingPatternType.OpenDiamond];
            }
            else if (pattern == DrawingPatternType.DownwardDiagonal)
            {
                return tagsByPatterns[DrawingPatternType.LightDownwardDiagonal];
            }
            return tagsByPatterns[DrawingPatternType.Percent5];
        }

        private static ModelShapeCustomGeometry GetWordArtFakeGeometry(DrawingPresetTextWarp presetTextWarp, IDocumentModel documentModel)
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(documentModel.MainPart);
            switch (presetTextWarp)
            {
                case DrawingPresetTextWarp.NoShape:
                    break;

                case DrawingPresetTextWarp.ArchDown:
                    geometry.AdjustValues.Add(new ModelShapeGuide("adj", "val 0"));
                    break;

                case DrawingPresetTextWarp.ArchDownPour:
                    geometry.AdjustValues.Add(new ModelShapeGuide("adj1", "val 0"));
                    geometry.AdjustValues.Add(new ModelShapeGuide("adj2", "val 25000"));
                    break;

                case DrawingPresetTextWarp.ArchUp:
                    geometry.AdjustValues.Add(new ModelShapeGuide("adj", "val cd2"));
                    break;

                case DrawingPresetTextWarp.ArchUpPour:
                    geometry.AdjustValues.Add(new ModelShapeGuide("adj1", "val cd2"));
                    geometry.AdjustValues.Add(new ModelShapeGuide("adj2", "val 50000"));
                    break;

                case DrawingPresetTextWarp.Button:
                    geometry.AdjustValues.Add(new ModelShapeGuide("adj", "val 10800000"));
                    break;

                case DrawingPresetTextWarp.ButtonPour:
                    geometry.AdjustValues.Add(new ModelShapeGuide("adj1", "val cd2"));
                    geometry.AdjustValues.Add(new ModelShapeGuide("adj2", "val 50000"));
                    break;

                case DrawingPresetTextWarp.CanDown:
                    geometry.AdjustValues.Add(new ModelShapeGuide("adj", "val 14286"));
                    break;

                case DrawingPresetTextWarp.CanUp:
                    geometry.AdjustValues.Add(new ModelShapeGuide("adj", "val 85714"));
                    break;

                case DrawingPresetTextWarp.CascadeDown:
                    geometry.AdjustValues.Add(new ModelShapeGuide("adj", "val 44444"));
                    break;

                case DrawingPresetTextWarp.CascadeUp:
                    geometry.AdjustValues.Add(new ModelShapeGuide("adj", "val 44444"));
                    break;

                case DrawingPresetTextWarp.Chevron:
                    geometry.AdjustValues.Add(new ModelShapeGuide("adj", "val 25000"));
                    break;

                case DrawingPresetTextWarp.ChevronInverted:
                    geometry.AdjustValues.Add(new ModelShapeGuide("adj", "val 75000"));
                    break;

                case DrawingPresetTextWarp.Circle:
                    geometry.AdjustValues.Add(new ModelShapeGuide("adj", "val 10800000"));
                    break;

                case DrawingPresetTextWarp.CirclePour:
                    geometry.AdjustValues.Add(new ModelShapeGuide("adj1", "val cd2"));
                    geometry.AdjustValues.Add(new ModelShapeGuide("adj2", "val 50000"));
                    break;

                case DrawingPresetTextWarp.CurveDown:
                    geometry.AdjustValues.Add(new ModelShapeGuide("adj", "val 45977"));
                    break;

                case DrawingPresetTextWarp.CurveUp:
                    geometry.AdjustValues.Add(new ModelShapeGuide("adj", "val 45977"));
                    break;

                case DrawingPresetTextWarp.Deflate:
                    geometry.AdjustValues.Add(new ModelShapeGuide("adj", "val 18750"));
                    break;

                case DrawingPresetTextWarp.DeflateBottom:
                    geometry.AdjustValues.Add(new ModelShapeGuide("adj", "val 50000"));
                    break;

                case DrawingPresetTextWarp.DeflateInflate:
                    geometry.AdjustValues.Add(new ModelShapeGuide("adj", "val 35000"));
                    break;

                case DrawingPresetTextWarp.InflateDeflate:
                    geometry.AdjustValues.Add(new ModelShapeGuide("adj", "val 25000"));
                    break;

                case DrawingPresetTextWarp.DeflateTop:
                    geometry.AdjustValues.Add(new ModelShapeGuide("adj", "val 50000"));
                    break;

                case DrawingPresetTextWarp.DoubleWave1:
                    geometry.AdjustValues.Add(new ModelShapeGuide("adj1", "val 6250"));
                    geometry.AdjustValues.Add(new ModelShapeGuide("adj2", "val 0"));
                    break;

                case DrawingPresetTextWarp.FadeDown:
                    geometry.AdjustValues.Add(new ModelShapeGuide("adj", "val 33333"));
                    break;

                case DrawingPresetTextWarp.FadeLeft:
                    geometry.AdjustValues.Add(new ModelShapeGuide("adj", "val 33333"));
                    break;

                case DrawingPresetTextWarp.FadeRight:
                    geometry.AdjustValues.Add(new ModelShapeGuide("adj", "val 33333"));
                    break;

                case DrawingPresetTextWarp.FadeUp:
                    geometry.AdjustValues.Add(new ModelShapeGuide("adj", "val 33333"));
                    break;

                case DrawingPresetTextWarp.Inflate:
                    geometry.AdjustValues.Add(new ModelShapeGuide("adj", "val 18750"));
                    break;

                case DrawingPresetTextWarp.InflateBottom:
                    geometry.AdjustValues.Add(new ModelShapeGuide("adj", "val 60000"));
                    break;

                case DrawingPresetTextWarp.InflateTop:
                    geometry.AdjustValues.Add(new ModelShapeGuide("adj", "val 40000"));
                    break;

                case DrawingPresetTextWarp.Plain:
                    geometry.AdjustValues.Add(new ModelShapeGuide("adj", "val 50000"));
                    break;

                case DrawingPresetTextWarp.RingInside:
                    geometry.AdjustValues.Add(new ModelShapeGuide("adj", "val 60000"));
                    break;

                case DrawingPresetTextWarp.RingOutside:
                    geometry.AdjustValues.Add(new ModelShapeGuide("adj", "val 60000"));
                    break;

                case DrawingPresetTextWarp.SlantDown:
                    geometry.AdjustValues.Add(new ModelShapeGuide("adj", "val 44445"));
                    break;

                case DrawingPresetTextWarp.SlantUp:
                    geometry.AdjustValues.Add(new ModelShapeGuide("adj", "val 55555"));
                    break;

                case DrawingPresetTextWarp.Stop:
                    geometry.AdjustValues.Add(new ModelShapeGuide("adj", "val 25000"));
                    break;

                case DrawingPresetTextWarp.Triangle:
                    geometry.AdjustValues.Add(new ModelShapeGuide("adj", "val 50000"));
                    break;

                case DrawingPresetTextWarp.TriangleInverted:
                    geometry.AdjustValues.Add(new ModelShapeGuide("adj", "val 50000"));
                    break;

                case DrawingPresetTextWarp.Wave1:
                    geometry.AdjustValues.Add(new ModelShapeGuide("adj1", "val 12500"));
                    geometry.AdjustValues.Add(new ModelShapeGuide("adj2", "val 0"));
                    break;

                case DrawingPresetTextWarp.Wave2:
                    geometry.AdjustValues.Add(new ModelShapeGuide("adj1", "val 12500"));
                    geometry.AdjustValues.Add(new ModelShapeGuide("adj2", "val 0"));
                    break;

                case DrawingPresetTextWarp.Wave4:
                    geometry.AdjustValues.Add(new ModelShapeGuide("adj1", "val 6250"));
                    geometry.AdjustValues.Add(new ModelShapeGuide("adj2", "val 0"));
                    break;

                default:
                    throw new ArgumentOutOfRangeException("presetTextWarp", presetTextWarp, null);
            }
            return geometry;
        }

        public static int GetWordArtPreset(DrawingPresetTextWarp presetTextWarp)
        {
            switch (presetTextWarp)
            {
                case DrawingPresetTextWarp.ArchDown:
                    return 0x91;

                case DrawingPresetTextWarp.ArchDownPour:
                    return 0x95;

                case DrawingPresetTextWarp.ArchUp:
                    return 0x90;

                case DrawingPresetTextWarp.ArchUpPour:
                    return 0x94;

                case DrawingPresetTextWarp.Button:
                    return 0x93;

                case DrawingPresetTextWarp.ButtonPour:
                    return 0x97;

                case DrawingPresetTextWarp.CanDown:
                    return 0xaf;

                case DrawingPresetTextWarp.CanUp:
                    return 0xae;

                case DrawingPresetTextWarp.CascadeDown:
                    return 0x9b;

                case DrawingPresetTextWarp.CascadeUp:
                    return 0x9a;

                case DrawingPresetTextWarp.Chevron:
                    return 140;

                case DrawingPresetTextWarp.ChevronInverted:
                    return 0x8d;

                case DrawingPresetTextWarp.Circle:
                    return 0x92;

                case DrawingPresetTextWarp.CirclePour:
                    return 150;

                case DrawingPresetTextWarp.CurveDown:
                    return 0x99;

                case DrawingPresetTextWarp.CurveUp:
                    return 0x98;

                case DrawingPresetTextWarp.Deflate:
                    return 0xa1;

                case DrawingPresetTextWarp.DeflateBottom:
                    return 0xa3;

                case DrawingPresetTextWarp.DeflateInflate:
                    return 0xa6;

                case DrawingPresetTextWarp.InflateDeflate:
                    return 0xa7;

                case DrawingPresetTextWarp.DeflateTop:
                    return 0xa5;

                case DrawingPresetTextWarp.DoubleWave1:
                    return 0x9e;

                case DrawingPresetTextWarp.FadeDown:
                    return 0xab;

                case DrawingPresetTextWarp.FadeLeft:
                    return 0xa9;

                case DrawingPresetTextWarp.FadeRight:
                    return 0xa8;

                case DrawingPresetTextWarp.FadeUp:
                    return 170;

                case DrawingPresetTextWarp.Inflate:
                    return 160;

                case DrawingPresetTextWarp.InflateBottom:
                    return 0xa2;

                case DrawingPresetTextWarp.InflateTop:
                    return 0xa4;

                case DrawingPresetTextWarp.Plain:
                    return 0x88;

                case DrawingPresetTextWarp.RingInside:
                    return 0x8e;

                case DrawingPresetTextWarp.RingOutside:
                    return 0x8f;

                case DrawingPresetTextWarp.SlantDown:
                    return 0xad;

                case DrawingPresetTextWarp.SlantUp:
                    return 0xac;

                case DrawingPresetTextWarp.Stop:
                    return 0x89;

                case DrawingPresetTextWarp.Triangle:
                    return 0x8a;

                case DrawingPresetTextWarp.TriangleInverted:
                    return 0x8b;

                case DrawingPresetTextWarp.Wave1:
                    return 0x9c;

                case DrawingPresetTextWarp.Wave2:
                    return 0x9d;

                case DrawingPresetTextWarp.Wave4:
                    return 0x9f;
            }
            return 0;
        }

        public static bool GradientWithFocus(GradientStopCollection gradientStops) => 
            (gradientStops.Count == 3) && gradientStops.First.Color.Equals(gradientStops.Last.Color);

        private static bool InstructionsIdentical(ModelPathInstructionList instructions, bool closed, IsSomeModelPathType condition)
        {
            int num = closed ? (instructions.Count - 1) : instructions.Count;
            if (num == 1)
            {
                return false;
            }
            for (int i = 1; i < num; i++)
            {
                if (!condition(instructions[i]))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool IsConnector(ShapePreset shapeType)
        {
            if (shapeType != ShapePreset.Line)
            {
                switch (shapeType)
                {
                    case ShapePreset.StraightConnector1:
                    case ShapePreset.BentConnector2:
                    case ShapePreset.BentConnector3:
                    case ShapePreset.BentConnector4:
                    case ShapePreset.BentConnector5:
                    case ShapePreset.CurvedConnector2:
                    case ShapePreset.CurvedConnector3:
                    case ShapePreset.CurvedConnector4:
                    case ShapePreset.CurvedConnector5:
                        break;

                    default:
                        return false;
                }
            }
            return true;
        }

        private static bool IsPathCubicBezier(IPathInstruction instruction) => 
            instruction is PathCubicBezier;

        private static bool IsPathLine(IPathInstruction instruction) => 
            instruction is PathLine;

        public static void RegisterPatternFillBlip(ShapeProperties shapeProperties, Dictionary<DrawingPatternType, DrawingTableInfo> patternBlipTable)
        {
            if ((shapeProperties.Fill.FillType == DrawingFillType.Pattern) && (patternBlipTable != null))
            {
                DrawingPatternFill fill = shapeProperties.Fill as DrawingPatternFill;
                if (fill != null)
                {
                    DrawingTableInfo info;
                    DrawingPatternType patternType = fill.PatternType;
                    if (patternBlipTable.TryGetValue(patternType, out info))
                    {
                        info.RefCount++;
                    }
                    else
                    {
                        patternBlipTable.Add(patternType, new DrawingTableInfo(0, 1));
                    }
                }
            }
        }

        public static void SetFillColor(OfficeDrawingColorPropertyBase colorProperty, IDrawingColor color)
        {
            if ((color == null) || color.IsEmpty)
            {
                colorProperty.ColorRecord = new OfficeColorRecord(DXColor.FromArgb(0xff, 0xff, 0xff));
            }
            else if (color.ColorType == DrawingColorType.System)
            {
                SetSystemColor(colorProperty, color);
            }
            else
            {
                colorProperty.ColorRecord = new OfficeColorRecord(color.FinalColor);
            }
        }

        private static void SetSystemColor(OfficeDrawingColorPropertyBase colorProperty, IDrawingColor color)
        {
            OfficeColorRecord record1 = new OfficeColorRecord();
            record1.SystemColorIndex = (int) color.System;
            OfficeColorRecord record = record1;
            colorProperty.ColorRecord = record;
        }

        public static void SetupChildAnchor(Transform2D transform2D, OfficeArtChildAnchor anchor)
        {
            if ((anchor != null) && (transform2D != null))
            {
                DocumentModelUnitConverter unitConverter = transform2D.DocumentModel.UnitConverter;
                anchor.Left = unitConverter.ModelUnitsToEmuF(transform2D.OffsetX);
                anchor.Top = unitConverter.ModelUnitsToEmuF(transform2D.OffsetY);
                anchor.Right = anchor.Left + unitConverter.ModelUnitsToEmuF(transform2D.Cx);
                anchor.Bottom = anchor.Top + unitConverter.ModelUnitsToEmuF(transform2D.Cy);
            }
        }

        private delegate bool IsSomeModelPathType(IPathInstruction instruction);

        private class PathInstructionExportWalker : IPathInstructionWalker
        {
            private readonly List<DrawingGeometryPoint> points;
            private readonly List<MsoPathInfo> msoPathInfos;
            private readonly ModelShapePathsList paths;
            private readonly IShapeGuideCalculator calculator;
            private int lastX;
            private int lastY;
            private int geometrySpaceBottom;
            private int geometrySpaceRight;

            public PathInstructionExportWalker(ModelShapePathsList paths, IShapeGuideCalculator calculator)
            {
                this.paths = paths;
                this.calculator = calculator;
                this.points = new List<DrawingGeometryPoint>();
                this.msoPathInfos = new List<MsoPathInfo>();
            }

            private static double DegreeToRadian(double degree) => 
                (degree / 180.0) * 3.1415926535897931;

            private static double EmuAngleToDegree(double value) => 
                value / 60000.0;

            private static Point GetIntersectionPoint(int left, int top, double wR, double hR, double angle)
            {
                double a = DegreeToRadian(angle);
                double num2 = Math.Atan2(wR * Math.Sin(a), hR * Math.Cos(a));
                double num4 = ((hR * Math.Sin(num2)) + hR) + top;
                return new Point(Round(((wR * Math.Cos(num2)) + wR) + left), Round(num4));
            }

            private Point GetTopLeftPoint(double wR, double hR, double angle)
            {
                double d = DegreeToRadian(angle);
                double num2 = Math.Cos(d);
                double num3 = Math.Sin(d);
                double num4 = (hR * wR) / Math.Sqrt((((hR * hR) * num2) * num2) + (((wR * wR) * num3) * num3));
                if (double.IsNaN(num4))
                {
                    return new Point(Round(this.lastX - wR), Round(this.lastY - hR));
                }
                double num6 = this.lastY - (num4 * num3);
                return new Point(Round((this.lastX - (num4 * num2)) - wR), Round(num6 - hR));
            }

            private static double NormalizeAngle(double angle) => 
                (angle >= 0.0) ? angle : (360.0 - Math.Abs(angle));

            private static int Round(double value) => 
                (int) Math.Round(value);

            public void Visit(PathArc pathArc)
            {
                int num = Round(pathArc.HeightRadius.Evaluate(this.calculator));
                int num2 = Round(pathArc.WidthRadius.Evaluate(this.calculator));
                double num4 = pathArc.SwingAngle.Evaluate(this.calculator);
                bool flag = num4 > 0.0;
                double angle = NormalizeAngle(EmuAngleToDegree(pathArc.StartAngle.Evaluate(this.calculator)));
                double num6 = (angle + EmuAngleToDegree(num4)) % 360.0;
                DrawingGeometryPoint item = new DrawingGeometryPoint(this.lastX, this.lastY);
                DrawingGeometryPoint point2 = new DrawingGeometryPoint(this.GetTopLeftPoint((double) num2, (double) num, angle));
                DrawingGeometryPoint point3 = new DrawingGeometryPoint(point2.X.Value + (num2 * 2), point2.Y.Value + (num * 2));
                DrawingGeometryPoint point4 = new DrawingGeometryPoint(GetIntersectionPoint(point2.X.Value, point2.Y.Value, (double) num2, (double) num, num6));
                this.points.Add(point2);
                this.points.Add(point3);
                this.points.Add(item);
                this.points.Add(point4);
                this.lastX = point4.X.Value;
                this.lastY = point4.Y.Value;
                this.msoPathInfos.Add(new MsoPathInfo(flag ? MsoPathEscape.ClockwiseArcTo : MsoPathEscape.ArcTo, 4));
            }

            public void Visit(PathClose value)
            {
                this.msoPathInfos.Add(new MsoPathInfo(MsoPathType.Close, 1));
            }

            public void Visit(PathCubicBezier pathCubicBezier)
            {
                foreach (AdjustablePoint point2 in pathCubicBezier.Points)
                {
                    int x = Round(point2.X.Evaluate(this.calculator));
                    int y = Round(point2.Y.Evaluate(this.calculator));
                    this.points.Add(new DrawingGeometryPoint(x, y));
                }
                AdjustablePoint last = pathCubicBezier.Points.Last;
                if (last != null)
                {
                    this.lastX = Round(last.X.Evaluate(this.calculator));
                    this.lastY = Round(last.Y.Evaluate(this.calculator));
                }
                this.msoPathInfos.Add(new MsoPathInfo(MsoPathType.CurveTo, 1));
            }

            public void Visit(PathLine pathLine)
            {
                int x = Round(pathLine.Point.X.Evaluate(this.calculator));
                int y = Round(pathLine.Point.Y.Evaluate(this.calculator));
                this.points.Add(new DrawingGeometryPoint(x, y));
                this.lastX = x;
                this.lastY = y;
                this.msoPathInfos.Add(new MsoPathInfo(MsoPathType.LineTo, 1));
            }

            public void Visit(PathMove pathMove)
            {
                int x = Round(pathMove.Point.X.Evaluate(this.calculator));
                int y = Round(pathMove.Point.Y.Evaluate(this.calculator));
                this.points.Add(new DrawingGeometryPoint(x, y));
                this.lastX = x;
                this.lastY = y;
                this.msoPathInfos.Add(new MsoPathInfo(MsoPathType.MoveTo, 0));
            }

            public void Visit(PathQuadraticBezier pathQuadraticBezier)
            {
                double[] numArray = new double[3];
                double[] numArray2 = new double[3];
                int[] numArray3 = new int[3];
                int[] numArray4 = new int[3];
                numArray[0] = this.lastX;
                numArray2[0] = this.lastY;
                for (int i = 1; i < 3; i++)
                {
                    numArray[i] = pathQuadraticBezier.Points[i - 1].X.Evaluate(this.calculator);
                    numArray2[i] = pathQuadraticBezier.Points[i - 1].Y.Evaluate(this.calculator);
                }
                numArray3[0] = Round(numArray[0] + ((2.0 * (numArray[1] - numArray[0])) / 3.0));
                numArray4[0] = Round(numArray2[0] + ((2.0 * (numArray2[1] - numArray2[0])) / 3.0));
                numArray3[1] = Round(numArray[2] + ((2.0 * (numArray[1] - numArray[2])) / 3.0));
                numArray4[1] = Round(numArray2[2] + ((2.0 * (numArray2[1] - numArray2[2])) / 3.0));
                numArray3[2] = Round(numArray[2]);
                numArray4[2] = Round(numArray2[2]);
                for (int j = 0; j < 3; j++)
                {
                    this.points.Add(new DrawingGeometryPoint(numArray3[j], numArray4[j]));
                }
                this.lastX = numArray3[2];
                this.lastY = numArray4[2];
                this.msoPathInfos.Add(new MsoPathInfo(MsoPathType.CurveTo, 1));
            }

            public void Walk()
            {
                foreach (ModelShapePath path in this.paths)
                {
                    foreach (IPathInstruction instruction in path.Instructions)
                    {
                        instruction.Visit(this);
                        if (instruction is PathMove)
                        {
                            if (path.FillMode == PathFillMode.None)
                            {
                                this.msoPathInfos.Add(new MsoPathInfo(MsoPathEscape.NoFill, 0));
                            }
                            if (!path.Stroke)
                            {
                                this.msoPathInfos.Add(new MsoPathInfo(MsoPathEscape.NoLine, 0));
                            }
                        }
                    }
                    this.msoPathInfos.Add(new MsoPathInfo(MsoPathType.End, -65536));
                    if (path.Width > this.geometrySpaceRight)
                    {
                        this.geometrySpaceRight = (int) path.Width;
                    }
                    if (path.Height > this.geometrySpaceBottom)
                    {
                        this.geometrySpaceBottom = (int) path.Height;
                    }
                }
            }

            public DrawingGeometryPoint[] Points =>
                this.points.ToArray();

            public MsoPathInfo[] MsoPathInfos =>
                this.msoPathInfos.ToArray();

            public int GeometrySpaceBottom =>
                this.geometrySpaceBottom;

            public int GeometrySpaceRight =>
                this.geometrySpaceRight;
        }
    }
}

