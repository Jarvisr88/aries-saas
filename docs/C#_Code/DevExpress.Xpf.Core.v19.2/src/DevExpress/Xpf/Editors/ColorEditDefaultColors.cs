namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Media;

    public class ColorEditDefaultColors
    {
        private static Dictionary<Color, string> predefinedColors = new Dictionary<Color, string>();
        public static readonly Color White = Colors.White;
        public static readonly Color Black = Colors.Black;
        public static readonly Color DarkRed = Color.FromArgb(0xff, 0x8b, 0, 0);
        public static readonly Color Red = Colors.Red;
        public static readonly Color Orange = Colors.Orange;
        public static readonly Color Yellow = Colors.Yellow;
        public static readonly Color LightGreen = Color.FromArgb(0xff, 0x90, 0xee, 0x90);
        public static readonly Color Green = Colors.Green;
        public static readonly Color LightBlue = Color.FromArgb(0xff, 0xad, 0xd8, 230);
        public static readonly Color Blue = Colors.Blue;
        public static readonly Color DarkBlue = Color.FromArgb(0xff, 0, 0, 0x8b);
        public static readonly Color Purple = Colors.Purple;

        static ColorEditDefaultColors()
        {
            predefinedColors.Add(White, EditorLocalizer.GetString(EditorStringId.ColorEdit_DefaultColors_White));
            predefinedColors.Add(Black, EditorLocalizer.GetString(EditorStringId.ColorEdit_DefaultColors_Black));
            predefinedColors.Add(DarkRed, EditorLocalizer.GetString(EditorStringId.ColorEdit_DefaultColors_DarkRed));
            predefinedColors.Add(Red, EditorLocalizer.GetString(EditorStringId.ColorEdit_DefaultColors_Red));
            predefinedColors.Add(Orange, EditorLocalizer.GetString(EditorStringId.ColorEdit_DefaultColors_Orange));
            predefinedColors.Add(Yellow, EditorLocalizer.GetString(EditorStringId.ColorEdit_DefaultColors_Yellow));
            predefinedColors.Add(LightGreen, EditorLocalizer.GetString(EditorStringId.ColorEdit_DefaultColors_LightGreen));
            predefinedColors.Add(Green, EditorLocalizer.GetString(EditorStringId.ColorEdit_DefaultColors_Green));
            predefinedColors.Add(LightBlue, EditorLocalizer.GetString(EditorStringId.ColorEdit_DefaultColors_LightBlue));
            predefinedColors.Add(Blue, EditorLocalizer.GetString(EditorStringId.ColorEdit_DefaultColors_Blue));
            predefinedColors.Add(DarkBlue, EditorLocalizer.GetString(EditorStringId.ColorEdit_DefaultColors_DarkBlue));
            predefinedColors.Add(Purple, EditorLocalizer.GetString(EditorStringId.ColorEdit_DefaultColors_Purple));
        }

        public static string GetColorName(Color color)
        {
            string str;
            return (!predefinedColors.TryGetValue(color, out str) ? null : str);
        }
    }
}

