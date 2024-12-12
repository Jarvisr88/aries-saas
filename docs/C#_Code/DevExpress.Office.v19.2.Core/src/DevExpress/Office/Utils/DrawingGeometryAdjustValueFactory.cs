namespace DevExpress.Office.Utils
{
    using System;

    public static class DrawingGeometryAdjustValueFactory
    {
        public static DrawingGeometryAdjustValue CreateAdjustValue(int index)
        {
            switch (index)
            {
                case 0:
                    return new DrawingGeometryAdjustValue1();

                case 1:
                    return new DrawingGeometryAdjustValue2();

                case 2:
                    return new DrawingGeometryAdjustValue3();

                case 3:
                    return new DrawingGeometryAdjustValue4();

                case 4:
                    return new DrawingGeometryAdjustValue5();

                case 5:
                    return new DrawingGeometryAdjustValue6();

                case 6:
                    return new DrawingGeometryAdjustValue7();

                case 7:
                    return new DrawingGeometryAdjustValue8();
            }
            return null;
        }
    }
}

