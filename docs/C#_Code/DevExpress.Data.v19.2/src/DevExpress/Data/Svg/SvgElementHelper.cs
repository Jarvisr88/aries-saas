namespace DevExpress.Data.Svg
{
    using System;

    public static class SvgElementHelper
    {
        public static double[] GetDoubleFromContent(string content);
        public static Func<SvgCommandBase, T> GetSvgCommandMatcher<T>(Func<SvgCommandMove, T> move, Func<SvgCommandBase, T> line, Func<SvgCommandArc, T> arc, Func<SvgCommandShortCubicBezier, T> shortCubicBezier, Func<SvgCommandCubicBezier, T> cubicBezier, Func<SvgCommandShortQuadraticBezier, T> shortQuadraticBezier, Func<SvgCommandQuadraticBezier, T> quadraticBezier) where T: class;
        private static bool IsLineToCommand(SvgCommandBase svgCommand);
        public static string[] SplitContent(string content);
    }
}

