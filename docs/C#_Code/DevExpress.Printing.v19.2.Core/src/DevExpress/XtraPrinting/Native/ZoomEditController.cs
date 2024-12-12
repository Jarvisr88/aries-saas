namespace DevExpress.XtraPrinting.Native
{
    using System;

    public class ZoomEditController
    {
        private int minZoomFactor;
        private int maxZoomFactor;
        private string zoomStringFormat;

        public ZoomEditController(int minZoomFactor, int maxZoomFactor, string zoomStringFormat);
        public string GetDigits(object input);
        public float GetZoomValue(object input);
        private bool IsValidInput(object input);
        private bool IsValidRange(object input);
        public bool IsValidZoomValue(object value, ref string message);
    }
}

