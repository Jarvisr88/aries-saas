namespace DevExpress.Office.Drawing
{
    using DevExpress.Office.Utils;
    using System;

    public static class DrawingValueChecker
    {
        public static void CheckCoordinate(long value, string propertyName)
        {
            ValueChecker.CheckValue(value, -27273042329600L, 0x18cdffffce64L, propertyName);
        }

        public static void CheckCoordinate32(int value, string propertyName)
        {
            ValueChecker.CheckValue(value, -51206400, 0x30d5900, propertyName);
        }

        public static void CheckCoordinate32F(float value, string propertyName)
        {
            ValueChecker.CheckValue((double) value, -51206400.0, 51206400.0, propertyName);
        }

        public static void CheckFixedAngle(int value, string propertyName)
        {
            ValueChecker.CheckValue(value, -5400000, 0x5265c0, propertyName);
        }

        public static void CheckFixedPercentage(int value, string propertyName)
        {
            ValueChecker.CheckValue(value, -100000, 0x186a0, propertyName);
        }

        public static void CheckFOVAngle(int value, string propertyName)
        {
            ValueChecker.CheckValue(value, 0, 0xa4cb80, propertyName);
        }

        public static void CheckOutlineWidth(int value, string propertyName)
        {
            ValueChecker.CheckValue(value, 0, 0x132f540, propertyName);
        }

        public static void CheckPositiveCoordinate(long value, string propertyName)
        {
            ValueChecker.CheckValue(value, 0L, 0x18cdffffce64L, propertyName);
        }

        public static void CheckPositiveCoordinate32(int value, string propertyName)
        {
            ValueChecker.CheckValue(value, 0, 0x30d5900, propertyName);
        }

        public static void CheckPositiveCoordinate32F(float value, string propertyName)
        {
            ValueChecker.CheckValue((double) value, 0.0, 51206400.0, propertyName);
        }

        public static void CheckPositiveFixedAngle(int value, string propertyName)
        {
            ValueChecker.CheckValue(value, 0, 0x1499700, propertyName);
        }

        public static void CheckPositiveFixedPercentage(int value, string propertyName)
        {
            ValueChecker.CheckValue(value, 0, 0x186a0, propertyName);
        }

        public static void CheckTextBulletSizePercentValue(int value)
        {
            ValueChecker.CheckValue(value, 0x61a8, 0x61a80, "TextBulletSizePercent");
        }

        public static void CheckTextBulletSizePointsValue(int value)
        {
            ValueChecker.CheckValue(value, 100, 0x61a80, "TextBulletSizePoints");
        }

        public static void CheckTextBulletStartAtNumValue(int value)
        {
            ValueChecker.CheckValue(value, 1, 0x7fff, "TextBulletStartAtNum");
        }

        public static void CheckTextIndentLevelValue(int value)
        {
            ValueChecker.CheckValue(value, 0, 8, "TextIndentLevel");
        }

        public static void CheckTextSpacingPercentValue(int value)
        {
            ValueChecker.CheckValue(value, 0, 0xc96a80, "TextSpacingPercent");
        }

        public static void CheckTextSpacingPointsValue(int value)
        {
            ValueChecker.CheckValue(value, 0, 0x26ac0, "TextSpacingPoints");
        }
    }
}

