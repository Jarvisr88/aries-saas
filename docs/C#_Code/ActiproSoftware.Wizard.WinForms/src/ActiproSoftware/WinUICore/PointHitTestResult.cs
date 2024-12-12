namespace ActiproSoftware.WinUICore
{
    using System;
    using System.Drawing;

    public class PointHitTestResult : HitTestResult
    {
        private System.Drawing.Point #Ei;

        public PointHitTestResult(IUIElement element, System.Drawing.Point point) : base(element)
        {
            this.#Ei = point;
        }

        public System.Drawing.Point Point =>
            this.#Ei;
    }
}

