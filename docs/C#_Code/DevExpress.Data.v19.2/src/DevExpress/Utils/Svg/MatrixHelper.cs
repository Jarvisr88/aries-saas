namespace DevExpress.Utils.Svg
{
    using System;
    using System.Collections.Generic;
    using System.Drawing.Drawing2D;

    internal static class MatrixHelper
    {
        internal static Matrix CreateRotationMatrix(double angle, double centerX, double centerY, double scale)
        {
            Matrix matrix = new Matrix();
            matrix.Translate((float) (centerX * scale), (float) (centerY * scale));
            matrix.Rotate((float) angle);
            matrix.Translate(-((float) (centerX * scale)), -((float) (centerY * scale)));
            return matrix;
        }

        internal static Matrix CreateScaleMatrix(double scaleX, double scaleY)
        {
            Matrix matrix = new Matrix();
            matrix.Scale((float) scaleX, (float) scaleY);
            return matrix;
        }

        internal static Matrix CreateSkewMatrix(double angleX, double angleY, double scale)
        {
            Matrix matrix = new Matrix();
            matrix.Shear((float) Math.Tan((angleX / 180.0) * 3.1415926535897931), (float) Math.Tan((angleY / 180.0) * 3.1415926535897931));
            return matrix;
        }

        internal static Matrix CreateTranslateMatrix(double offsetX, double offsetY, double scale)
        {
            Matrix matrix = new Matrix();
            matrix.Translate((float) (offsetX * scale), (float) (offsetY * scale));
            return matrix;
        }

        internal static Matrix FromPoints(List<double> points, double scale) => 
            new Matrix((float) points[0], (float) points[1], (float) points[2], (float) points[3], (float) (points[4] * scale), (float) (points[5] * scale));
    }
}

