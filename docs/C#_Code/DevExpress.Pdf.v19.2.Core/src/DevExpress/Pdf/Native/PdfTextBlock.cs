namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Collections.Generic;

    public class PdfTextBlock
    {
        private readonly PdfCharacter[] characters;
        private readonly PdfFont font;
        private readonly double angle;
        private readonly PdfPoint[] charLocations;
        private readonly double characterSpacing;
        private readonly double minSpaceWidth;
        private readonly double maxSpaceWidth;

        public PdfTextBlock(PdfStringData data, PdfGraphicsState graphicsState)
        {
            PdfTransformationMatrix fontMatrix;
            PdfTextState textState = graphicsState.TextState;
            double[] advances = data.Advances;
            this.font = textState.Font;
            PdfTransformationMatrix matrix = PdfTransformationMatrix.Multiply(textState.TextMatrix, graphicsState.TransformationMatrix);
            double sy = matrix.Sy;
            double fontSize = textState.AbsoluteFontSize * sy;
            double num5 = (textState.HorizontalScaling / 100.0) * matrix.Sx;
            double num6 = (textState.FontSize * this.font.WidthFactor) * num5;
            this.characterSpacing = textState.CharacterSpacing * num5;
            this.charLocations = new PdfPoint[advances.Length];
            double num7 = Math.Abs((double) (this.font.Metrics.EmSize * num6));
            this.minSpaceWidth = num7 / 6.0;
            this.maxSpaceWidth = num7 / 5.5;
            byte[][] charCodes = data.CharCodes;
            short[] str = data.Str;
            int length = charCodes.Length;
            this.characters = new PdfCharacter[length];
            PdfType3Font font = this.font as PdfType3Font;
            if (font != null)
            {
                fontMatrix = font.FontMatrix;
            }
            else
            {
                PdfType3Font local1 = font;
                fontMatrix = null;
            }
            PdfTransformationMatrix local2 = fontMatrix;
            PdfTransformationMatrix matrix6 = local2;
            if (local2 == null)
            {
                PdfTransformationMatrix local3 = local2;
                matrix6 = new PdfTransformationMatrix(1.0 / this.font.Metrics.EmSize, 0.0, 0.0, 1.0 / this.font.Metrics.EmSize, 0.0, 0.0);
            }
            PdfTransformationMatrix matrix3 = PdfTransformationMatrix.Multiply(matrix6, textState.TextSpaceMatrix);
            PdfTransformationMatrix matrix4 = PdfTransformationMatrix.Multiply(matrix3, matrix);
            this.angle = Math.Atan2(matrix4.B, matrix4.A);
            int index = (Math.Sign(matrix4.A) == Math.Sign(matrix4.D)) ? 0 : ((matrix4.D < 0.0) ? 2 : 1);
            double y = Math.Max(1.0, this.font.Metrics.Ascent);
            double e = 0.0;
            int num12 = 0;
            for (int i = 0; num12 < length; i++)
            {
                PdfTransformationMatrix matrix5 = PdfTransformationMatrix.Multiply(matrix3, PdfTransformationMatrix.Multiply(new PdfTransformationMatrix(1.0, 0.0, 0.0, 1.0, e, 0.0), matrix));
                this.charLocations[num12] = matrix5.Transform(PdfPoint.Empty);
                e += advances[i];
                double characterWidth = this.font.GetCharacterWidth((ushort) str[num12]);
                PdfPoint[] points = new PdfPoint[] { new PdfPoint(0.0, y), new PdfPoint(characterWidth, y), new PdfPoint(0.0, this.font.Metrics.Descent) };
                PdfPoint[] pointArray = matrix5.Transform(points);
                double width = PdfPoint.Distance(pointArray[0], pointArray[1]);
                double height = PdfPoint.Distance(pointArray[0], pointArray[2]);
                PdfOrientedRectangle rectangle = new PdfOrientedRectangle(pointArray[index], width, height, this.angle);
                this.characters[num12] = new PdfCharacter(this.font.GetCharacterUnicode(charCodes[num12]), this.font, fontSize, rectangle);
                num12++;
            }
        }

        public IList<PdfCharacter> Characters =>
            this.characters;

        public PdfPoint Location =>
            this.charLocations[0];

        public double Angle =>
            this.angle;

        public PdfPoint[] CharLocations =>
            this.charLocations;

        public double CharacterSpacing =>
            this.characterSpacing;

        public double MinSpaceWidth =>
            this.minSpaceWidth;

        public double MaxSpaceWidth =>
            this.maxSpaceWidth;

        public PdfFont Font =>
            this.font;
    }
}

