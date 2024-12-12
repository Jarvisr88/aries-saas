namespace DevExpress.XtraPrinting.Native
{
    using System;

    public class ValueInfo
    {
        private float fValue;
        private bool fActive;

        public ValueInfo(float value, bool active);

        public float Value { get; set; }

        public bool Active { get; set; }
    }
}

