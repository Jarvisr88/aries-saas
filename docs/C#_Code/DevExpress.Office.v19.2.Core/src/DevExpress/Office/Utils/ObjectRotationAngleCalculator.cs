namespace DevExpress.Office.Utils
{
    using DevExpress.Utils.KeyboardHandler;
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public static class ObjectRotationAngleCalculator
    {
        public static float CalculateAngle(Point point, Rectangle objectBounds, float initialRotationAngle) => 
            CalculateAngle(point, objectBounds, initialRotationAngle, null);

        public static unsafe float CalculateAngle(Point point, Rectangle objectBounds, float initialRotationAngle, Matrix transformMatrix)
        {
            if (transformMatrix != null)
            {
                point = transformMatrix.TransformPoint(point);
            }
            Point point2 = RectangleUtils.CenterPoint(objectBounds);
            Point* pointPtr1 = &point;
            pointPtr1.X -= point2.X;
            Point* pointPtr2 = &point;
            pointPtr2.Y -= point2.Y;
            if ((point.X == 0) && (point.Y == 0))
            {
                return initialRotationAngle;
            }
            float num = (float) ((180.0 * Math.Atan((double) (((float) point.X) / ((float) point.Y)))) / 3.1415926535897931);
            if (point.Y > 0)
            {
                num += 180f;
            }
            if (Math.Abs(num) == 90f)
            {
                num = -num;
            }
            return SnapAngle(initialRotationAngle - num);
        }

        private static float CalculateSnap(float angle, float baseAngle, float delta) => 
            (((baseAngle - delta) > angle) || (angle >= (baseAngle + delta))) ? angle : baseAngle;

        private static float NormalizeAngle(float angle)
        {
            angle = angle % 360f;
            if (angle < 0f)
            {
                angle += 360f;
            }
            return angle;
        }

        private static float SnapAngle(float angle) => 
            !DevExpress.Utils.KeyboardHandler.KeyboardHandler.IsShiftPressed ? SnapAngle(angle, 90, 3f) : SnapAngle(angle, 15, 7.5f);

        private static float SnapAngle(float angle, int step, float delta)
        {
            angle = NormalizeAngle(angle);
            for (int i = 0; i <= 360; i += step)
            {
                angle = CalculateSnap(angle, (float) i, delta);
            }
            return NormalizeAngle(angle);
        }
    }
}

