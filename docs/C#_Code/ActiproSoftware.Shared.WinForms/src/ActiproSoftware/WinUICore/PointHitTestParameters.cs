namespace ActiproSoftware.WinUICore
{
    using System;
    using System.Drawing;

    public class PointHitTestParameters : HitTestParameters
    {
        private System.Drawing.Point #Ei;

        public PointHitTestParameters(System.Drawing.Point point)
        {
            this.#Ei = point;
        }

        public System.Drawing.Point Point =>
            this.#Ei;
    }
}

